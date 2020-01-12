using System;
using System.Runtime.InteropServices;
//using TdsComputeInfo = FreeTds.TdsResultInfo;
using TdsParamInfo = FreeTds.TdsResultInfo;
using Size_t = System.IntPtr;

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

        // tds : config.c
        public static TDS_COMPILETIME_SETTINGS GetCompiletimeSettings() => Marshal.PtrToStructure<TDS_COMPILETIME_SETTINGS>(NativeMethods.tds_get_compiletime_settings());
        public delegate void TdsConfParse(string option, string value, object param);
        public static bool ReadConfSection(IntPtr @in, string section, TdsConfParse confParse, IntPtr parseParam) => NativeMethods.tds_read_conf_section(@in, section, (a, b, c) => confParse(a, b, c), parseParam);
        public static void ParseConfSection(string option, string value, IntPtr @param) => NativeMethods.tds_parse_conf_section(option, value, @param);
        public static ushort[] ConfigVerstr(string tdsVer, TdsLogin login) => NativeMethods.tds_config_verstr(tdsVer, login.Ptr);
        public static IntPtr LookupHost(string serverName) => NativeMethods.tds_lookup_host(serverName);
        public static TDSRET LookupHostSet(string serverName, IntPtr[] addr) => NativeMethods.tds_lookup_host_set(serverName, addr);
        public static string Addrinfo2str(IntPtr addr, ref string name, int namemax) => NativeMethods.tds_addrinfo2str(addr, ref name, namemax);
        public static string GetHomeFile(string file) => NativeMethods.tds_get_home_file(file);
        //
        public static TDSRET SetInterfacesFileLoc(string interfloc) => NativeMethods.tds_set_interfaces_file_loc(interfloc);
        public static string[] STD_DATETIME_FMT = null;
        public static int ParseBoolean(string value, int defaultValue) => NativeMethods.tds_parse_boolean(value, defaultValue);
        public static int ConfigBoolean(string option, string value, TdsLogin login) => NativeMethods.tds_config_boolean(option, value, login.Ptr);
        //
        public static TDSLOCALE GetLocale() => NativeMethods.tds_get_locale();
        public static BcpColData AllocBcpColumnData(uint columnSize) => new BcpColData(NativeMethods.tds_alloc_bcp_column_data(columnSize));
        public static string Prtype(int token) => NativeMethods.tds_prtype(token);
        public static TDS_SERVER_TYPE GetCardinalType(TDS_SERVER_TYPE datatype, int usertype) => NativeMethods.tds_get_cardinal_type(datatype, usertype);

    }

    public class BcpColData : TdsObject<BCPCOLDATA>
    {
        public BcpColData(IntPtr ctx) : base(null, null, null) { }
    }

    public class TdsAuthentication : TdsObject<TDSAUTHENTICATION>
    {
        public TdsAuthentication(IntPtr ctx) : base(null, null, null) { }
    }

    
    public class TdsBcpInfo : TdsObject<TDSBCPINFO>
    {
        public TdsBcpInfo(IntPtr ctx) : base(null, null, NativeMethods.tds_free_bcpinfo) { }

        // tds : mem.c
        //public void FreeBcpinfo() => NativeMethods.tds_free_bcpinfo(_ctx);
        public void DeinitBcpinfo() => NativeMethods.tds_deinit_bcpinfo(Ptr);
    }

    public class TdsColumn : TdsObject<TDSCOLUMN>
    {
        public TdsColumn(IntPtr ctx) : base(null, null, null) { }

        // tds : mem.c
        public IntPtr AllocParamData() => NativeMethods.tds_alloc_param_data(Ptr);
    }

    public class TdsIconv : TdsObject<TDSICONV>
    {
        public TdsIconv(IntPtr ctx) : base(null, null, null) { }
    }

    public class TdsConnection : TdsObject<TDSCONNECTION>
    {
        static TdsConnection() => Tds.Touch();
        public TdsConnection(IntPtr ctx) : base(null, null, NativeMethods.tds_connection_close) { }

        // tds : config.c
        public TdsDynamic LookupDynamic(string id) { var ptr = NativeMethods.tds_lookup_dynamic(Ptr, id); return ptr != IntPtr.Zero ? new TdsDynamic(ptr) : null; }
        public int GetVarintSize(int datatype) => NativeMethods.tds_get_varint_size(Ptr, datatype);

        // tds : iconv.c
        public TDSRET tds_iconv_open(string charset, int use_utf16) => NativeMethods.tds_iconv_open(Ptr, charset, use_utf16);
        public void tds_iconv_close() => NativeMethods.tds_iconv_close(Ptr);
        public void tds_srv_charset_changed(string charset) => NativeMethods.tds_srv_charset_changed(Ptr, charset);
        public void tds7_srv_charset_changed(int sql_collate, int lcid) => NativeMethods.tds7_srv_charset_changed(Ptr, sql_collate, lcid);
        public int tds_iconv_alloc() => NativeMethods.tds_iconv_alloc(Ptr);
        public void IconvFree() => NativeMethods.tds_iconv_free(Ptr);
        public TdsIconv IconvFromCollate(string collate) { var ptr = NativeMethods.tds_iconv_from_collate(Ptr, collate); return ptr != IntPtr.Zero ? new TdsIconv(ptr) : null; }

        // tds : mem.c
        public void CursorDeallocated(TdsCursor cursor) => NativeMethods.tds_cursor_deallocated(Ptr, cursor.Ptr);
        public void DynamicDeallocated(TdsDynamic dyn) => NativeMethods.tds_dynamic_deallocated(Ptr, dyn.Ptr);
        public TdsDynamic AllocDynamic(string id) => new TdsDynamic(NativeMethods.tds_alloc_dynamic(Ptr, id));
        public TdsSocket AllocAdditionalSocket() => new TdsSocket(NativeMethods.tds_alloc_additional_socket(Ptr));

        // tds : query.c
        public int NeedsUnprepare(TdsDynamic dyn) => NativeMethods.tds_needs_unprepare(Ptr, dyn.Ptr);
        public TDSRET DeferredUnprepare(TdsDynamic dyn) => NativeMethods.tds_deferred_unprepare(Ptr, dyn.Ptr);
        public TDSRET DeferredCursorDealloc(TdsCursor cursor) => NativeMethods.tds_deferred_cursor_dealloc(Ptr, cursor.Ptr);

        // tds : data.c
        public void SetParamType(TdsColumn curcol, TDS_SERVER_TYPE type) => NativeMethods.tds_set_param_type(Ptr, curcol.Ptr, type);
        public void SetColumnType(TdsColumn curcol, TDS_SERVER_TYPE type) => NativeMethods.tds_set_column_type(Ptr, curcol.Ptr, type);

        // tds : net .c
        public void ConnectionClose() => NativeMethods.tds_connection_close(Ptr);
    }

    public class TdsContext : TdsObject<TDSCONTEXT>
    {
        static TdsContext() => Tds.Touch();
        public TdsContext(TdsContext parent = null) : base(arg => NativeMethods.tds_alloc_context((IntPtr)arg), parent != null ? parent.Ptr : IntPtr.Zero, NativeMethods.tds_free_context) { }

        // tds : mem.c
        //public void FreeContext() => NativeMethods.tds_free_context(_ctx);
        public TdsSocket AllocSocket(uint bufsize) => new TdsSocket(NativeMethods.tds_alloc_socket(Ptr, bufsize));

        // server : abc.c
        public TdsSocket Listen(int ipPort) { var tds = NativeMethodsServer.tds_listen(Ptr, ipPort); return tds == IntPtr.Zero ? null : new TdsSocket(tds); }
    }

    public class TdsCursor : TdsObject<TDSCURSOR>
    {
        public TdsCursor(IntPtr ctx) : base(null, null, null) { }

        // tds : mem.c
        //void tds_release_cursor(TDSCURSOR** pcursor);
    }

    public class TdsDynamic : TdsObject<TDSDYNAMIC>
    {
        public TdsDynamic(IntPtr ctx) : base(null, null, null) { }
    }

    public class TdsHeaders : TdsObject<TDSHEADERS>
    {
        public TdsHeaders(IntPtr ctx) : base(null, null, null) { }
    }

    public class TdsLogin : TdsObject<TDSLOGIN>
    {
        static TdsLogin() => Tds.Touch();
        public TdsLogin(int useEnvironment) : base(arg => NativeMethods.tds_alloc_login((int)arg), useEnvironment, NativeMethods.tds_free_login) { }
        public TdsLogin(IntPtr ctx) : base(null, null, NativeMethods.tds_free_login) { }

        // tds : config.c
        public bool ReadConfFile(string server) => NativeMethods.tds_read_conf_file(Ptr, server);
        public void FixLogin() => NativeMethods.tds_fix_login(Ptr);

        // tds : login.c
        public void SetPacket(int packet_size) => NativeMethods.tds_set_packet(Ptr, packet_size);
        public void SetPort(int port) => NativeMethods.tds_set_port(Ptr, port);
        public bool SetPasswd(string password) => NativeMethods.tds_set_passwd(Ptr, password);
        public void SetBulk(bool enabled) => NativeMethods.tds_set_bulk(Ptr, enabled);
        public bool SetUser(string username) => NativeMethods.tds_set_user(Ptr, username);
        public bool SetApp(string application) => NativeMethods.tds_set_app(Ptr, application);
        public bool SetHost(string hostname) => NativeMethods.tds_set_host(Ptr, hostname);
        public bool SetLibrary(string library) => NativeMethods.tds_set_library(Ptr, library);
        public bool SetServer(string server) => NativeMethods.tds_set_server(Ptr, server);
        public bool SetClient_charset(string charset) => NativeMethods.tds_set_client_charset(Ptr, charset);
        public bool SetLanguage(string language) => NativeMethods.tds_set_language(Ptr, language);
        public void SetVersion(byte major_ver, byte minor_ver) => NativeMethods.tds_set_version(Ptr, major_ver, minor_ver);
    }

    public class TdsMultiple : TdsObject<TDSMULTIPLE>
    {
        public TdsMultiple(IntPtr ctx) : base(null, null, null) { }
    }

    public class TdsObject<T> : IDisposable where T : struct
    {
        internal protected IntPtr Ptr;
        readonly Action<IntPtr> _free;

        public TdsObject(Func<object, IntPtr> @new, object newArg, Action<IntPtr> free, IntPtr? ptr = null)
        {
            Ptr = ptr ?? @new(newArg);
            if (Ptr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(Ptr));
            _free = free;
            Value = Marshal.PtrToStructure<T>(Ptr);
        }
        ~TdsObject() => Dispose();

        public void Dispose()
        {
            if (Ptr == IntPtr.Zero)
                return;
            _free(Ptr);
            Ptr = IntPtr.Zero;
        }

        public T Value;
    }

    public class TdsResultInfo : TdsObject<TDSRESULTINFO>
    {
        public TdsResultInfo(IntPtr ctx) : base(null, null, null) { }

        // tds : config.c
        public TDSRET AllocRow() => NativeMethods.tds_alloc_row(Ptr);
        public TDSRET AllocComputeRow() => NativeMethods.tds_alloc_compute_row(Ptr);
    }

    public class TdsSocket : TdsObject<TDSSOCKET>
    {
        public TdsSocket(TdsContext ctx, int bufSize) : base(arg => NativeMethods.tds_alloc_socket(ctx.Ptr, (uint)arg), bufSize, NativeMethods.tds_free_socket) { }
        public TdsSocket(IntPtr ctx) : base(null, null, NativeMethods.tds_free_socket) { }

        // tds : config.c
        public TdsLogin ReadConfigInfo(TdsLogin login, TDSLOCALE locale) { var ptr = NativeMethods.tds_read_config_info(Ptr, login.Ptr, ref locale); return ptr != IntPtr.Zero ? new TdsLogin(ptr) : null; }

        // tds : mem.c
        public void FreeAllResults() => NativeMethods.tds_free_all_results(Ptr);
        public object AllocComputeResults(ushort num_cols, ushort by_cols) { var ptr = NativeMethods.tds_alloc_compute_results(Ptr, num_cols, by_cols); throw new NotImplementedException(); }
        public void ReleaseCurDyn() => NativeMethods.tds_release_cur_dyn(this);
        public void SetCurDyn(TdsDynamic dyn) => NativeMethods.tds_set_cur_dyn(Ptr, dyn.Ptr);
        public TdsSocket ReallocSocket(int bufsize) { var ptr = NativeMethods.tds_realloc_socket(Ptr, (Size_t)bufsize); return ptr != IntPtr.Zero ? new TdsSocket(ptr) : null; }
        public string AllocLookupSqlstate(int msgno) => NativeMethods.tds_alloc_lookup_sqlstate(Ptr, msgno);
        public TdsCursor AllocCursor(string name, string query) { var ptr = NativeMethods.tds_alloc_cursor(Ptr, name, name.Length, query, query.Length); return ptr != IntPtr.Zero ? new TdsCursor(ptr) : null; }
        public void SetCurrentResults(TdsResultInfo info) => NativeMethods.tds_set_current_results(Ptr, info.Ptr);

        // tds : login.c
        public int ConnectAndLogin(TdsLogin login) => NativeMethods.tds_connect_and_login(Ptr, login.Ptr);

        // tds : query.c
        public void StartQuery(byte packet_type) => NativeMethods.tds_start_query(Ptr, packet_type);
        //
        public TDSRET SubmitQuery(string query) => NativeMethods.tds_submit_query(Ptr, query);
        public TDSRET SubmitQueryParams(string query, TdsParamInfo @params, TdsHeaders head) => NativeMethods.tds_submit_query_params(Ptr, query, @params.Ptr, head.Ptr);
        public TDSRET SubmitQueryf(string queryf, params object[] args) => NativeMethods.tds_submit_queryf(Ptr, queryf, args);
        public TDSRET SubmitPrepare(string query, string id, TdsDynamic dyn_out, TdsParamInfo @params) => NativeMethods.tds_submit_prepare(Ptr, query, id, dyn_out.Ptr, @params.Ptr);
        public TDSRET SubmitExecdirect(string query, TdsParamInfo @params, TdsHeaders head) => NativeMethods.tds_submit_execdirect(Ptr, query, @params.Ptr, head.Ptr);
        public TDSRET SubmitPrepexec71(string query, string id, TdsDynamic dyn_out, TdsParamInfo @params) => NativeMethods.tds71_submit_prepexec(Ptr, query, id, dyn_out.Ptr, @params.Ptr);
        public TDSRET SubmitExecute(TdsDynamic dyn) => NativeMethods.tds_submit_execute(Ptr, dyn.Ptr);
        public TDSRET SendCancel() => NativeMethods.tds_send_cancel(Ptr);
        public TDSRET SubmitUnprepare(TdsDynamic dyn) => NativeMethods.tds_submit_unprepare(Ptr, dyn.Ptr);
        public TDSRET SubmitRpc(string rpc_name, TdsParamInfo @params, TdsHeaders head) => NativeMethods.tds_submit_rpc(Ptr, rpc_name, @params.Ptr, head.Ptr);
        public TDSRET SubmitOptioncmd(TDS_OPTION_CMD command, TDS_OPTION option, TDS_OPTION_ARG @param, int param_size) => NativeMethods.tds_submit_optioncmd(Ptr, command, option, ref @param, param_size);
        public TDSRET tds_submit_begin_tran() => NativeMethods.tds_submit_begin_tran(Ptr);
        public TDSRET tds_submit_rollback(int cont) => NativeMethods.tds_submit_rollback(Ptr, cont);
        public TDSRET tds_submit_commit(int cont) => NativeMethods.tds_submit_commit(Ptr, cont);
        public TDSRET tds_disconnect() => NativeMethods.tds_disconnect(Ptr);
        public int QuoteId(byte[] buffer, string id) => (int)NativeMethods.tds_quote_id(Ptr, buffer, id, id.Length);
        public int QuoteString(byte[] buffer, string str) => (int)NativeMethods.tds_quote_string(Ptr, buffer, str, str.Length);
        public int tds_fix_column_size(TdsColumn curcol) => (int)NativeMethods.tds_fix_column_size(Ptr, curcol.Ptr);
        //public string ConvertString(TDSICONV char_conv, string s, int len, out int out_len) => NativeMethods.tds_convert_string(_ctx, char_conv, s, len, out out_len);
        public TDSRET GetColumnDeclaration(TdsColumn curcol, out string @out) => NativeMethods.tds_get_column_declaration(Ptr, curcol.Ptr, out @out);
        //
        public TDSRET CursorDeclare(TdsCursor cursor, TdsParamInfo @params, out int send) => NativeMethods.tds_cursor_declare(Ptr, cursor.Ptr, @params.Ptr, out send);
        public TDSRET CursorSetrows(TdsCursor cursor, out int send) => NativeMethods.tds_cursor_setrows(Ptr, cursor.Ptr, out send);
        public TDSRET CursorOpen(TdsCursor cursor, TdsParamInfo @params, out int send) => NativeMethods.tds_cursor_open(Ptr, cursor.Ptr, @params.Ptr, out send);
        public TDSRET CursorFetch(TdsCursor cursor, TDS_CURSOR_FETCH fetch_type, int i_row) => NativeMethods.tds_cursor_fetch(Ptr, cursor.Ptr, fetch_type, i_row);
        public TDSRET CursorGetCursorInfo(TdsCursor cursor, out uint row_number, out uint row_count) => NativeMethods.tds_cursor_get_cursor_info(Ptr, cursor.Ptr, out row_number, out row_count);
        public TDSRET CursorClose(TdsCursor cursor) => NativeMethods.tds_cursor_close(Ptr, cursor.Ptr);
        public TDSRET CursorDealloc(TdsCursor cursor) => NativeMethods.tds_cursor_dealloc(Ptr, cursor.Ptr);
        public TDSRET CursorUpdate(TdsCursor cursor, TDS_CURSOR_OPERATION op, int i_row, TdsParamInfo @params) => NativeMethods.tds_cursor_update(Ptr, cursor.Ptr, op, i_row, @params.Ptr);
        public TDSRET CursorSetname(TdsCursor cursor) => NativeMethods.tds_cursor_setname(Ptr, cursor.Ptr);
        //
        public TDSRET MultipleInit(TdsMultiple multiple, TDS_MULTIPLE_TYPE type, TdsHeaders head) => NativeMethods.tds_multiple_init(Ptr, multiple.Ptr, type, head.Ptr);
        public TDSRET MultipleDone(TdsMultiple multiple) => NativeMethods.tds_multiple_done(Ptr, multiple.Ptr);
        public TDSRET MultipleQuery(TdsMultiple multiple, string query, TdsParamInfo @params) => NativeMethods.tds_multiple_query(Ptr, multiple.Ptr, query, @params.Ptr);
        public TDSRET MultipleExecute(TdsMultiple multiple, TdsDynamic dyn) => NativeMethods.tds_multiple_execute(Ptr, multiple.Ptr, dyn.Ptr);

        // tds : token.c
        public TDSRET ProcessCancel() => NativeMethods.tds_process_cancel(Ptr);
        public TDSRET ProcessLoginTokens() => NativeMethods.tds_process_login_tokens(Ptr);
        public TDSRET ProcessSimpleQuery() => NativeMethods.tds_process_simple_query(Ptr);
        public int SendOptioncmd5(TDS_OPTION_CMD tds_command, TDS_OPTION tds_option, TDS_OPTION_ARG[] tds_argument, int[] tds_argsize) => NativeMethods.tds5_send_optioncmd(Ptr, tds_command, tds_option, tds_argument, tds_argsize);
        public TDSRET ProcessTokens(out int result_type, out int done_flags, uint flag) => NativeMethods.tds_process_tokens(Ptr, out result_type, out done_flags, flag);

        // tds : write.c
        public int InitWriteBuf() => NativeMethods.tds_init_write_buf(Ptr);
        public int PutN(byte[] buf, int n) => NativeMethods.tds_put_n(Ptr, buf, (Size_t)n);
        public int PutString(string buf, int len) => NativeMethods.tds_put_string(Ptr, buf, buf.Length);
        public int PutInt(int i) => NativeMethods.tds_put_int(Ptr, i);
        public int PutInt8(long i) => NativeMethods.tds_put_int8(Ptr, i);
        public int PutSmallint(short si) => NativeMethods.tds_put_smallint(Ptr, si);
        /// <summary>
        /// Output a tinyint value
        /// </summary>
        /// <param name="ti"></param>
        /// <returns></returns>
        public int PutTinyint(byte ti) => NativeMethods.tds_put_tinyint(Ptr, ti);
        public int PutByte(byte c) => NativeMethods.tds_put_byte(Ptr, c);
        public TDSRET FlushPacket() => NativeMethods.tds_flush_packet(Ptr);
        public int PutBuf(byte[] buf, int dsize, int ssize) => NativeMethods.tds_put_buf(Ptr, buf, dsize, ssize);

        // tds : read.c
        public byte GetByte() => NativeMethods.tds_get_byte(Ptr);
        public void UngetByte() => NativeMethods.tds_unget_byte(Ptr);
        public byte Peek() => NativeMethods.tds_peek(Ptr);
        public ushort GetUsmallint() => NativeMethods.tds_get_usmallint(Ptr);
        public short GetSmallint() => NativeMethods.tds_get_smallint(Ptr);
        public uint GetUint() => NativeMethods.tds_get_uint(Ptr);
        public int GetInt() => NativeMethods.tds_get_int(Ptr);
        public ulong GetUint8() => NativeMethods.tds_get_uint8(Ptr);
        public long GetInt8() => NativeMethods.tds_get_int8(Ptr);
        public int GetString(out string value) => throw new NotImplementedException(); //var r = NativeMethods.tds_get_string(_ctx, 100, );
        public TDSRET GetCharData(out string dest, int wire_size, TdsColumn curcol) => throw new NotImplementedException(); // NativeMethods.tds_get_char_data(_ctx);
        public bool tds_get_n(out byte[] dest, int n) => NativeMethods.tds_get_n(Ptr, out dest, (Size_t)n);
        string DstrGet() => throw new NotImplementedException(); //NativeMethods.tds_dstr_get(_ctx);

        // tds : util.c
        public TDS_STATE SetState(TDS_STATE state) => NativeMethods.tds_set_state(Ptr, state);

        // tds : net.c
        public TDSERRNO OpenSocket(IntPtr ipaddr, uint port, int timeout, out int p_oserr) => NativeMethods.tds_open_socket(Ptr, ipaddr, port, timeout, out p_oserr);
        public void CloseSocket() => NativeMethods.tds_close_socket(Ptr);
        public int ConnectionRead(byte[] buf, int buflen) => NativeMethods.tds_connection_read(Ptr, buf, buf.Length);
        public int ConnectionWrite(byte[] buf, int final) => NativeMethods.tds_connection_write(Ptr, buf, buf.Length, final);
        public int Select(uint tds_sel, int timeout_seconds) => NativeMethods.tds_select(Ptr, tds_sel, timeout_seconds);
        public int Goodread(byte[] buf, int buflen) => NativeMethods.tds_goodread(Ptr, buf, buflen);
        public int Goodwrite(byte[] buffer, int buflen) => NativeMethods.tds_goodwrite(Ptr, buffer, (Size_t)buflen);

        // tds : packet.c
        public int ReadPacket() => NativeMethods.tds_read_packet(Ptr);
        public TDSRET WritePacket(byte final) => NativeMethods.tds_write_packet(Ptr, final);
#if ENABLE_ODBC_MARS
        public int AppendCancel() => NativeMethods.tds_append_cancel(_ctx);
        public TDSRET AppendFin() => NativeMethods.tds_append_fin(_ctx);
#else
        public int tds_put_cancel() => NativeMethods.tds_put_cancel(Ptr);
#endif

        // tds : challenge.c
#if !HAVE_SSPI
        TdsAuthentication tds_ntlm_get_auth() { var ptr = NativeMethods.tds_ntlm_get_auth(Ptr); return ptr != IntPtr.Zero ? new TdsAuthentication(Ptr) : null; }
        TdsAuthentication tds_gss_get_auth() { var ptr = NativeMethods.tds_gss_get_auth(Ptr); return ptr != IntPtr.Zero ? new TdsAuthentication(Ptr) : null; }
#else
        TdsAuthentication tds_sspi_get_auth() { var ptr = NativeMethods.tds_sspi_get_auth(_ctx); return ptr != IntPtr.Zero ? new TdsAuthentication(_ctx) : null; }
#endif

        // tds : sec_negotiate.c
        public TdsAuthentication NegotiateGetAuth5() { var ptr = NativeMethods.tds5_negotiate_get_auth(Ptr); return ptr != IntPtr.Zero ? new TdsAuthentication(Ptr) : null; }
        public void NegotiateSetMsgType5(TdsAuthentication auth, uint msg_type) => NativeMethods.tds5_negotiate_set_msg_type(Ptr, auth.Ptr, msg_type);

        // tds : bulk.c
        public TDSRET tds_bcp_init(TdsBcpInfo bcpinfo) => NativeMethods.tds_bcp_init(Ptr, bcpinfo.Ptr);
        public delegate TDSRET BcpGetColData(TdsBcpInfo bulk, TdsColumn bcpcol, int offset);
        public delegate void BcpNullError(TdsBcpInfo bulk, int index, int offset);
        public TDSRET BcpSendRecord(TdsBcpInfo bcpinfo, BcpGetColData getColData, BcpNullError nullError, int offset) => NativeMethods.tds_bcp_send_record(Ptr, bcpinfo.Ptr, (a, b, c) => getColData(null, null, c), (a, b, c) => nullError(null, b, c), offset);
        public TDSRET BcpDone(out int rows_copied) => NativeMethods.tds_bcp_done(Ptr, out rows_copied);
        public TDSRET BcpStart(TdsBcpInfo bcpinfo) => NativeMethods.tds_bcp_start(Ptr, bcpinfo.Ptr);
        public TDSRET BcpStartCopyIn(TdsBcpInfo bcpinfo) => NativeMethods.tds_bcp_start_copy_in(Ptr, bcpinfo.Ptr);
        //
        //public TDSRET BcpFread(TDSICONV conv, FILE stream, string terminator, byte[] outbuf, out int outbytes) => NativeMethods.tds_bcp_fread(_ctx);
        //
        public TDSRET WritetextStart(string objName, string textPtr, string timestamp, int with_log, uint size) => NativeMethods.tds_writetext_start(Ptr, objName, textPtr, timestamp, with_log, size);
        public TDSRET WritetextContinue(string text, uint size) => NativeMethods.tds_writetext_continue(Ptr, text, size);
        public TDSRET WritetextEnd() => NativeMethods.tds_writetext_end(Ptr);


        // server : login.c
        public int ReadLogin(out TdsLogin login) { var r = NativeMethodsServer.tds_read_login(Ptr, out var loginPtr); login = new TdsLogin(loginPtr); return r; }
        public int ReadLogin7(out TdsLogin login) { var r = NativeMethodsServer.tds7_read_login(Ptr, out var loginPtr); login = new TdsLogin(loginPtr); return r; }
        public TdsLogin AllocReadLogin() { var ptr = NativeMethodsServer.tds_alloc_read_login(Ptr); return ptr != IntPtr.Zero ? new TdsLogin(ptr) : null; }

        // server : query.c
        public string GetQuery() => NativeMethodsServer.tds_get_query(Ptr);
        public string GetGenericQuery() => NativeMethodsServer.tds_get_generic_query(Ptr);

        // server : server.c
        public void EnvChange(int type, string oldValue, string newValue) => NativeMethodsServer.tds_env_change(Ptr, type, oldValue, newValue);
        public void SendMsg(int msgno, int msgstate, int severity, string msgText, string srvName, string procName, int line) => NativeMethodsServer.tds_send_msg(Ptr, msgno, msgstate, severity, msgText, srvName, procName, line);
        public void SendLoginAck(string procName) => NativeMethodsServer.tds_send_login_ack(Ptr, procName);
        public void SendEed(int msgno, int msgstate, int severity, string msgText, string srvName, string procName, int line) => NativeMethodsServer.tds_send_eed(Ptr, msgno, msgstate, severity, msgText, srvName, procName, line);
        public void SendErr(int severity, int dbErr, int osErr, string dbErrStr, string osErrStr) => NativeMethodsServer.tds_send_err(Ptr, severity, dbErr, osErr, dbErrStr, osErrStr);
        public void SendCapabilitiesToken() => NativeMethodsServer.tds_send_capabilities_token(Ptr);
        //
        public void SendDoneToken(short flags, int numrows) => NativeMethodsServer.tds_send_done_token(Ptr, flags, numrows);
        public void SendDone(int token, short flags, int numrows) => NativeMethodsServer.tds_send_done(Ptr, token, flags, numrows);
        public void SendControlToken(short numcols) => NativeMethodsServer.tds_send_control_token(Ptr, numcols);
        public void SendColName(TDSRESULTINFO resinfo) => NativeMethodsServer.tds_send_col_name(Ptr, ref resinfo);
        public void SendColInfo(TDSRESULTINFO resinfo) => NativeMethodsServer.tds_send_col_info(Ptr, ref resinfo);
        public void SendResult(TDSRESULTINFO resinfo) => NativeMethodsServer.tds_send_result(Ptr, ref resinfo);
        public void SendResult7(TDSRESULTINFO resinfo) => NativeMethodsServer.tds7_send_result(Ptr, ref resinfo);
        public void SendTableHeader(TDSRESULTINFO resinfo) => NativeMethodsServer.tds_send_table_header(Ptr, ref resinfo);
        public void SendRow(TDSRESULTINFO resinfo) => NativeMethodsServer.tds_send_row(Ptr, ref resinfo);
        public void SendPrelogin71() => NativeMethodsServer.tds71_send_prelogin(Ptr);
    }
}