using System.Runtime.InteropServices;

namespace FreeTds
{
    public static class Tds
    {
        static bool _disposed;
        static readonly TdsFactory _factory;
        static readonly INativeLibrary _library = NativeLibrary.Create(NativeMethods.LibraryName);

        static Tds()
        {
            var lazyFactory = new LazyFactory(() => _disposed, typeof(Tds).Name);
            _factory = new TdsFactory(lazyFactory);
        }
        //~Tds() => Dispose();

        public static void Dispose()
        {
            if (_disposed)
                return;
            _library?.Dispose();
            _disposed = true;
        }

        public static void Touch() { }

        public static T MarshalStructure<T>(string name) => _library.MarshalStructure<T>(name);
        public static T[] MarshalToPtrArray<T>(string name, int count) => _library.MarshalToPtrArray<T>(name, count);
    }

    public static class TdsConfig
    {
        static TdsConfig() => Tds.Touch();

        public static TDS_COMPILETIME_SETTINGS GetCompiletimeSettings() => Marshal.PtrToStructure<TDS_COMPILETIME_SETTINGS>(NativeMethods.tds_get_compiletime_settings());
    }
}