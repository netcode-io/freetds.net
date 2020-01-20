using System;
using System.Runtime.InteropServices;

namespace FreeTds
{
    public static class NativeMethodsServer
    {
        internal const string LibraryName = "tds.dll";

        #region login.c
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.LPStr)] public static extern string tds7_decrypt_pass([MarshalAs(UnmanagedType.LPStr)] string crypt_pass, int len, [MarshalAs(UnmanagedType.LPStr)] string clear_pass);
        [DllImport(LibraryName)] public static extern IntPtr tds_listen(IntPtr ctx, int ip_port); //:TDSCONTEXT->TDSSOCKET
        [DllImport(LibraryName)] public static extern int tds_read_login(IntPtr tds, out IntPtr login); //:TDSSOCKET:TDSLOGIN
        [DllImport(LibraryName)] public static extern int tds7_read_login(IntPtr tds, out IntPtr login); //:TDSSOCKET:TDSLOGIN
        [DllImport(LibraryName)] public static extern IntPtr tds_alloc_read_login(IntPtr tds); //:TDSSOCKET->TDSLOGIN
        #endregion

        #region query.c
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.LPStr)] public static extern string tds_get_query(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.LPStr)] public static extern string tds_get_generic_query(IntPtr tds); //:TDSSOCKET
        #endregion

        #region server.c
        [DllImport(LibraryName)] public static extern void tds_env_change(IntPtr tds, int type, [MarshalAs(UnmanagedType.LPStr)] string oldvalue, [MarshalAs(UnmanagedType.LPStr)] string newvalue); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern void tds_send_msg(IntPtr tds, int msgno, int msgstate, int severity, [MarshalAs(UnmanagedType.LPStr)] string msgtext, [MarshalAs(UnmanagedType.LPStr)] string srvname, [MarshalAs(UnmanagedType.LPStr)] string procname, int line); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern void tds_send_login_ack(IntPtr tds, [MarshalAs(UnmanagedType.LPStr)] string progname); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern void tds_send_eed(IntPtr tds, int msgno, int msgstate, int severity, [MarshalAs(UnmanagedType.LPStr)] string msgtext, [MarshalAs(UnmanagedType.LPStr)] string srvname, [MarshalAs(UnmanagedType.LPStr)] string procname, int line); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern void tds_send_err(IntPtr tds, int severity, int dberr, int oserr, [MarshalAs(UnmanagedType.LPStr)] string dberrstr, [MarshalAs(UnmanagedType.LPStr)] string oserrstr); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern void tds_send_capabilities_token(IntPtr tds); //:TDSSOCKET
        /* TODO remove, use tds_send_done */
        [DllImport(LibraryName)] public static extern void tds_send_done_token(IntPtr tds, short flags, int numrows); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern void tds_send_done(IntPtr tds, int token, short flags, int numrows); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern void tds_send_control_token(IntPtr tds, short numcols); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern void tds_send_col_name(IntPtr tds, IntPtr resinfo); //:TDSSOCKET:TDSRESULTINFO
        [DllImport(LibraryName)] public static extern void tds_send_col_info(IntPtr tds, IntPtr resinfo); //:TDSSOCKET:TDSRESULTINFO
        [DllImport(LibraryName)] public static extern void tds_send_result(IntPtr tds, IntPtr resinfo); //:TDSSOCKET:TDSRESULTINFO
        [DllImport(LibraryName)] public static extern void tds7_send_result(IntPtr tds, IntPtr resinfo); //:TDSSOCKET:TDSRESULTINFO
        [DllImport(LibraryName)] public static extern void tds_send_table_header(IntPtr tds, IntPtr resinfo); //:TDSSOCKET:TDSRESULTINFO
        [DllImport(LibraryName)] public static extern void tds_send_row(IntPtr tds, IntPtr resinfo); //:TDSSOCKET:TDSRESULTINFO
        [DllImport(LibraryName)] public static extern void tds71_send_prelogin(IntPtr tds); //:TDSSOCKET
        #endregion
    }
}