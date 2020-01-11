using System;
using System.Runtime.InteropServices;

namespace FreeTds
{
    public class TdsObject<T> : IDisposable
        where T : struct
    {
        internal protected IntPtr _ctx;
        readonly Action<IntPtr> _free;

        public TdsObject(Func<object, IntPtr> @new, object newArg, Action<IntPtr> free, IntPtr? ctx = null)
        {
            _ctx = ctx ?? @new(newArg);
            _free = free;
            Value = Marshal.PtrToStructure<T>(_ctx);
        }
        ~TdsObject() => Dispose();

        public void Dispose()
        {
            if (_ctx == IntPtr.Zero)
                return;
            _free(_ctx);
            _ctx = IntPtr.Zero;
        }

        public T Value;
    }

    public class TdsContext : TdsObject<TDSCONTEXT>
    {
        static TdsContext() => Tds.Touch();
        public TdsContext(TdsContext parent = null) : base(arg => NativeMethods.tds_alloc_context((IntPtr)arg), parent != null ? parent._ctx : IntPtr.Zero, NativeMethods.tds_free_context) { }

        // server
        public TdsSocket Listen(int ipPort) { var tds = NativeMethodsServer.tds_listen(_ctx, ipPort); return tds == IntPtr.Zero ? null : new TdsSocket(tds); }
    }

    public class TdsSocket : TdsObject<TDSSOCKET>
    {
        public TdsSocket(TdsContext ctx, int bufSize) : base(arg => NativeMethods.tds_alloc_socket(ctx._ctx, (uint)arg), bufSize, NativeMethods.tds_free_socket) { }
        public TdsSocket(IntPtr ctx) : base(null, null, NativeMethods.tds_free_socket) { }

        // server : login.c
        public int ReadLogin(out TdsLogin login) { var r = NativeMethodsServer.tds_read_login(_ctx, out var loginCtx); login = new TdsLogin(loginCtx); return r; }
        public int ReadLogin7(out TdsLogin login) { var r = NativeMethodsServer.tds7_read_login(_ctx, out var loginCtx); login = new TdsLogin(loginCtx); return r; }
        public TdsLogin AllocReadLogin() { var login = NativeMethodsServer.tds_alloc_read_login(_ctx); return login == IntPtr.Zero ? null : new TdsLogin(login); }
        // server : query.c
        public string GetQuery() => NativeMethodsServer.tds_get_query(_ctx);
        public string GetGenericQuery() => NativeMethodsServer.tds_get_generic_query(_ctx);
        // server : server.c
        public void EnvChange(int type, string oldValue, string newValue) => NativeMethodsServer.tds_env_change(_ctx, type, oldValue, newValue);
        public void SendMsg(int msgno, int msgstate, int severity, string msgText, string srvName, string procName, int line) => NativeMethodsServer.tds_send_msg(_ctx, msgno, msgstate, severity, msgText, srvName, procName, line);
        public void SendLoginAck(string procName) => NativeMethodsServer.tds_send_login_ack(_ctx, procName);
        public void SendEed(int msgno, int msgstate, int severity, string msgText, string srvName, string procName, int line) => NativeMethodsServer.tds_send_eed(_ctx, msgno, msgstate, severity, msgText, srvName, procName, line);
        public void SendErr(int severity, int dbErr, int osErr, string dbErrStr, string osErrStr) => NativeMethodsServer.tds_send_err(_ctx, severity, dbErr, osErr, dbErrStr, osErrStr);
        public void SendCapabilitiesToken() => NativeMethodsServer.tds_send_capabilities_token(_ctx);
        //
        public void SendDoneToken(short flags, int numrows) => NativeMethodsServer.tds_send_done_token(_ctx, flags, numrows);
        public void SendDone(int token, short flags, int numrows) => NativeMethodsServer.tds_send_done(_ctx, token, flags, numrows);
        public void SendControlToken(short numcols) => NativeMethodsServer.tds_send_control_token(_ctx, numcols);
        public void SendColName(TDSRESULTINFO resinfo) => NativeMethodsServer.tds_send_col_name(_ctx, ref resinfo);
        public void SendColInfo(TDSRESULTINFO resinfo) => NativeMethodsServer.tds_send_col_info(_ctx, ref resinfo);
        public void SendResult(TDSRESULTINFO resinfo) => NativeMethodsServer.tds_send_result(_ctx, ref resinfo);
        public void SendResult7(TDSRESULTINFO resinfo) => NativeMethodsServer.tds7_send_result(_ctx, ref resinfo);
        public void SendTableHeader(TDSRESULTINFO resinfo) => NativeMethodsServer.tds_send_table_header(_ctx, ref resinfo);
        public void SendRow(TDSRESULTINFO resinfo) => NativeMethodsServer.tds_send_row(_ctx, ref resinfo);
        public void SendPrelogin71() => NativeMethodsServer.tds71_send_prelogin(_ctx);
    }

    public class TdsLogin : TdsObject<TDSLOGIN>
    {
        public TdsLogin(int useEnvironment) : base(arg => NativeMethods.tds_alloc_login((int)arg), useEnvironment, NativeMethods.tds_free_login) { }
        public TdsLogin(IntPtr ctx) : base(null, null, NativeMethods.tds_free_login) { }
    }
}