#define CORECLR
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FreeTds
{
    public static class NativeLibrary
    {
        public static INativeLibrary Create(string name)
        {
#if CORECLR
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return new WindowsNativeLibrary(name);
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return new LinuxNativeLibrary(name);
            else throw new NotImplementedException("Unmanaged library loading is not implemented on this platform");
#else
            return new WindowsNativeLibrary(name);
#endif
        }
    }

    public interface INativeLibrary : IDisposable
    {
        T MarshalStructure<T>(string name);
        T[] MarshalToPtrArray<T>(string name, int count);
    }

    internal sealed class WindowsNativeLibrary : INativeLibrary
    {
        readonly IntPtr _LibarayHandle;
        bool _IsDisposed = false;

        public WindowsNativeLibrary(string name)
        {
            var dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Environment.Is64BitProcess ? "x64" : "x86");
            Environment.SetEnvironmentVariable("PATH", $"{Environment.GetEnvironmentVariable("PATH")};{dllPath}");
            _LibarayHandle = LoadLibrary(name);
            if (_LibarayHandle == IntPtr.Zero)
                throw new InvalidOperationException($"library {name} not found");
        }

        public void Dispose()
        {
            if (_IsDisposed)
                return;
            FreeLibrary(_LibarayHandle);
            _IsDisposed = true;
        }

        public T MarshalStructure<T>(string name)
        {
            if (_IsDisposed)
                throw new ObjectDisposedException("UnmanagedLibrary");
            var ptr = GetProcAddress(_LibarayHandle, name);
            return ptr != IntPtr.Zero ? Marshal.PtrToStructure<T>(ptr) : throw new InvalidOperationException($"function {name} not found");
        }

        public T[] MarshalToPtrArray<T>(string name, int count)
        {
            if (_IsDisposed)
                throw new ObjectDisposedException("UnmanagedLibrary");
            var ptr = GetProcAddress(_LibarayHandle, name);
            if (ptr == IntPtr.Zero)
                return new T[0];
            var r = new T[count];
            var typeofT = typeof(T);
            if (typeofT == typeof(int)) Marshal.Copy(ptr, (int[])(object)r, 0, r.Length);
            else throw new ArgumentOutOfRangeException(nameof(T), typeofT.Name);
            return r;
        }

        ~WindowsNativeLibrary() { Dispose(); }

        [DllImport("kernel32.dll")] static extern IntPtr LoadLibrary(string fileName);
        [DllImport("kernel32.dll")] static extern int FreeLibrary(IntPtr handle);
        [DllImport("kernel32.dll")] static extern IntPtr GetProcAddress(IntPtr handle, string procedureName);
    }

    internal sealed class LinuxNativeLibrary : INativeLibrary
    {
        readonly IntPtr _LibarayHandle;
        const int RTLD_NOW = 2;
        bool _IsDisposed = false;

        public LinuxNativeLibrary(string name)
        {
            var library = (IntPtr.Size == 8 ? $"x64\\{name}" : $"x86\\{name}");
            _LibarayHandle = dlopen(library, RTLD_NOW);
            if (_LibarayHandle == IntPtr.Zero)
                throw new InvalidOperationException($"library {name} not found");
        }

        public void Dispose()
        {
            if (_IsDisposed)
                return;
            dlclose(_LibarayHandle);
            _IsDisposed = true;
        }

        public T MarshalStructure<T>(string name)
        {
            if (_IsDisposed)
                throw new ObjectDisposedException("UnmanagedLibrary");
            // clear previous errors if any
            dlerror();
            var ptr = dlsym(_LibarayHandle, name);
            var errPtr = dlerror();
            if (errPtr != IntPtr.Zero)
                throw new Exception("dlsym: " + Marshal.PtrToStringAnsi(errPtr));
            return Marshal.PtrToStructure<T>(ptr);
        }

        public T[] MarshalToPtrArray<T>(string name, int count)
        {
            if (_IsDisposed)
                throw new ObjectDisposedException("UnmanagedLibrary");
            // clear previous errors if any
            dlerror();
            var ptr = dlsym(_LibarayHandle, name);
            var errPtr = dlerror();
            if (errPtr != IntPtr.Zero)
                throw new Exception("dlsym: " + Marshal.PtrToStringAnsi(errPtr));
            if (ptr == IntPtr.Zero)
                return new T[0];
            var r = new T[count];
            var typeofT = typeof(T);
            if (typeofT == typeof(int)) Marshal.Copy(ptr, (int[])(object)r, 0, r.Length);
            else throw new ArgumentOutOfRangeException(nameof(T), typeofT.Name);
            return r;
        }

        ~LinuxNativeLibrary() { Dispose(); }

        [DllImport("libdl.so")] static extern IntPtr dlopen(string fileName, int flags);
        [DllImport("libdl.so")] static extern IntPtr dlsym(IntPtr handle, string symbol);
        [DllImport("libdl.so")] static extern int dlclose(IntPtr handle);
        [DllImport("libdl.so")] static extern IntPtr dlerror();
    }

    // The class that does the marshaling. Making it generic is not required, but
    // will make it easier to use the same custom marshaler for multiple array types.
    public class ArrayMarshaler<T> : ICustomMarshaler
    {
        // All custom marshalers require a static factory method with this signature.
        public static ICustomMarshaler GetInstance(string cookie) => new ArrayMarshaler<T>();

        // This is the function that builds the managed type - in this case, the managed
        // array - from a pointer. You can just return null here if only sending the 
        // array as an in-parameter.
        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            // First, sanity check...
            if (pNativeData == IntPtr.Zero) return null;
            // Start by reading the size of the array ("Length" from your ABS_DATA struct)
            var length = Marshal.ReadInt32(pNativeData);
            // Create the managed array that will be returned
            var array = new T[length];
            // For efficiency, only compute the element size once
            var elSiz = Marshal.SizeOf<T>();
            // Populate the array
            for (int i = 0; i < length; i++)
                array[i] = Marshal.PtrToStructure<T>(pNativeData + sizeof(int) + (elSiz * i));
            // Alternate method, for arrays of primitive types only:
            // Marshal.Copy(pNativeData + sizeof(int), array, 0, length);
            return array;
        }

        // This is the function that marshals your managed array to unmanaged memory.
        // If you only ever marshal the array out, not in, you can return IntPtr.Zero
        public IntPtr MarshalManagedToNative(object ManagedObject)
        {
            if (ManagedObject == null) return IntPtr.Zero;
            var array = (T[])ManagedObject;
            var elSiz = Marshal.SizeOf<T>();
            // Get the total size of unmanaged memory that is needed (length + elements)
            var size = sizeof(int) + (elSiz * array.Length);
            // Allocate unmanaged space. For COM, use Marshal.AllocCoTaskMem instead.
            var ptr = Marshal.AllocHGlobal(size);
            // Write the "Length" field first
            Marshal.WriteInt32(ptr, array.Length);
            // Write the array data
            for (int i = 0; i < array.Length; i++)
                Marshal.StructureToPtr<T>(array[i], ptr + sizeof(int) + (elSiz * i), false); // Newly-allocated space has no existing object, so the last param is false
            // If you're only using arrays of primitive types, you could use this instead:
            //Marshal.Copy(array, 0, ptr + sizeof(int), array.Length);
            return ptr;
        }

        // This function is called after completing the call that required marshaling to unmanaged memory. You should use it to free any unmanaged memory you allocated.
        // If you never consume unmanaged memory or other resources, do nothing here.
        public void CleanUpNativeData(IntPtr pNativeData) => Marshal.FreeHGlobal(pNativeData); // Free the unmanaged memory. Use Marshal.FreeCoTaskMem if using COM.

        // If, after marshaling from unmanaged to managed, you have anything that needs to be taken care of when you're done with the object, put it here. Garbage 
        // collection will free the managed object, so I've left this function empty.
        public void CleanUpManagedData(object ManagedObj) { }

        // This function is a lie. It looks like it should be impossible to get the right  value - the whole problem is that the size of each array is variable! 
        // - but in practice the runtime doesn't rely on this and may not even call it.
        // The MSDN example returns -1; I'll try to be a little more realistic.
        public int GetNativeDataSize() => sizeof(int) + Marshal.SizeOf<T>();
    }
}
