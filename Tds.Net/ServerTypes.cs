using System;
using System.Runtime.InteropServices;
using TDSCOMPUTEINFO = FreeTds.TDSRESULTINFO;
using TDSPARAMINFO = FreeTds.TDSRESULTINFO;
using Size_t = System.IntPtr;

namespace FreeTds
{
    public static class NativeMethodsServer
    {
        internal const string LibraryName = "tds.dll";

        #region login.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.LPTStr)] public static extern string tds7_decrypt_pass([MarshalAs(UnmanagedType.LPTStr)] string crypt_pass, int len, [MarshalAs(UnmanagedType.LPTStr)] string clear_pass);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_listen(ref TDSCONTEXT ctx, int ip_port); //:TDSSOCKET
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_read_login(ref TDSSOCKET tds, ref TDSLOGIN login);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds7_read_login(ref TDSSOCKET tds, ref TDSLOGIN login);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_alloc_read_login(ref TDSSOCKET tds); //:TDSLOGIN
        #endregion

        #region query.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.LPTStr)] public static extern string tds_get_query(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.LPTStr)] public static extern string tds_get_generic_query(ref TDSSOCKET tds);
        #endregion

        #region server.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_env_change(ref TDSSOCKET tds, int type, [MarshalAs(UnmanagedType.LPTStr)] string oldvalue, [MarshalAs(UnmanagedType.LPTStr)] string newvalue);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_send_msg(ref TDSSOCKET tds, int msgno, int msgstate, int severity, [MarshalAs(UnmanagedType.LPTStr)] string msgtext, [MarshalAs(UnmanagedType.LPTStr)] string srvname, [MarshalAs(UnmanagedType.LPTStr)] string procname, int line);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_send_login_ack(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.LPTStr)] string progname);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_send_eed(ref TDSSOCKET tds, int msgno, int msgstate, int severity, [MarshalAs(UnmanagedType.LPTStr)] string msgtext, [MarshalAs(UnmanagedType.LPTStr)] string srvname, [MarshalAs(UnmanagedType.LPTStr)] string procname, int line);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_send_err(ref TDSSOCKET tds, int severity, int dberr, int oserr, [MarshalAs(UnmanagedType.LPTStr)] string dberrstr, [MarshalAs(UnmanagedType.LPTStr)] string oserrstr);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_send_capabilities_token(ref TDSSOCKET tds);
        /* TODO remove, use tds_send_done */
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_send_done_token(ref TDSSOCKET tds, short flags, int numrows);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_send_done(ref TDSSOCKET tds, int token, short flags, int numrows);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_send_control_token(ref TDSSOCKET tds, short numcols);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_send_col_name(ref TDSSOCKET tds, ref TDSRESULTINFO resinfo);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_send_col_info(ref TDSSOCKET tds, ref TDSRESULTINFO resinfo);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_send_result(ref TDSSOCKET tds, ref TDSRESULTINFO resinfo);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds7_send_result(ref TDSSOCKET tds, ref TDSRESULTINFO resinfo);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_send_table_header(ref TDSSOCKET tds, ref TDSRESULTINFO resinfo);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_send_row(ref TDSSOCKET tds, ref TDSRESULTINFO resinfo);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds71_send_prelogin(ref TDSSOCKET tds);
        #endregion
    }
}