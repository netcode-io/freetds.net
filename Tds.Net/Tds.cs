using System;
using System.Runtime.InteropServices;
using TDSCOMPUTEINFO = FreeTds.TDSRESULTINFO;
using TDSPARAMINFO = FreeTds.TDSRESULTINFO;
using Size_t = System.IntPtr;
using TDSRET = System.Int32;

namespace FreeTds
{
    public static class Tds
    {
        static Tds() => NativeMethods.Touch();
        public static void Touch() => NativeMethods.Touch();

        #region Methods

        // tds : config.c
        public static TDS_COMPILETIME_SETTINGS GetCompiletimeSettings() => Marshal.PtrToStructure<TDS_COMPILETIME_SETTINGS>(NativeMethods.tds_get_compiletime_settings());
        public delegate void ConfParse(string option, string value, object param);
        public static bool ReadConfSection(IntPtr @in, string section, ConfParse confParse, IntPtr parseParam) => NativeMethods.tds_read_conf_section(@in, section, (a, b, c) => confParse(a, b, c), parseParam);
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
        public static BcpColData AllocBcpColumnData(uint columnSize) => NativeMethods.tds_alloc_bcp_column_data(columnSize).ToMarshaledObject<BcpColData, BCPCOLDATA>();
        public static string Prtype(int token) => NativeMethods.tds_prtype(token);
        public static TDS_SERVER_TYPE GetCardinalType(TDS_SERVER_TYPE datatype, int usertype) => NativeMethods.tds_get_cardinal_type(datatype, usertype);

        // tds : mem.c
        public static void FreeResults(TdsResultInfo resInfo) => NativeMethods.tds_free_results(resInfo != null ? resInfo.Ptr : IntPtr.Zero);
        public static void FreeBcpColumnData(BcpColData coldata) => NativeMethods.tds_free_bcp_column_data(coldata != null ? coldata.Ptr : IntPtr.Zero); //:Dispose
        public static TdsResultInfo AllocResults(ushort numCols) => NativeMethods.tds_alloc_results(numCols).ToMarshaledObject<TdsResultInfo, TDSRESULTINFO>();
        public static TdsContext AllocContext(TdsContext parent = null) => NativeMethods.tds_alloc_context(parent?.Ptr ?? IntPtr.Zero).ToMarshaledObject<TdsContext, TDSCONTEXT>();
        public static void FreeContext(TdsContext locale) => NativeMethods.tds_free_context(locale != null ? locale.Ptr : IntPtr.Zero); //:Dispose
        public static string AllocClientSqlstate(int msgno) => NativeMethods.tds_alloc_client_sqlstate(msgno);
        public static TdsLogin AllocLogin(int useEnvironment) => NativeMethods.tds_alloc_login(useEnvironment).ToMarshaledObject<TdsLogin, TDSLOGIN>();
        public static void FreeLogin(TdsLogin login) => NativeMethods.tds_free_login(login != null ? login.Ptr : IntPtr.Zero); //:Dispose
        public static TdsLocale AllocLocale() => NativeMethods.tds_alloc_locale().ToMarshaledObject<TdsLocale, TDSLOCALE>();
        public static void FreeLocale(TdsLocale locale) => NativeMethods.tds_free_locale(locale != null ? locale.Ptr : IntPtr.Zero); //:Dispose
        public static IntPtr Realloc(IntPtr p, Size_t newSize) => NativeMethods.tds_realloc(p, newSize);
        //#define TDS_RESIZE(p, n_elem) tds_realloc((void**) &(p), sizeof(*(p)) * (size_t) (n_elem))
        public static TdsPacket AllocPacket(byte[] buf, uint len) => NativeMethods.tds_alloc_packet(buf, len).ToMarshaledObject<TdsPacket, TDSPACKET>();
        public static void FreePackets(TdsPacket packet) => NativeMethods.tds_free_packets(packet != null ? packet.Ptr : IntPtr.Zero); //:Dispose
        public static TdsBcpInfo AllocBcpinfo() => NativeMethods.tds_alloc_bcpinfo().ToMarshaledObject<TdsBcpInfo, TDSBCPINFO>();
        public static void FreeBcpinfo(TdsBcpInfo bcpinfo) => NativeMethods.tds_free_bcpinfo(bcpinfo != null ? bcpinfo.Ptr : IntPtr.Zero); //:Dispose
        public static void DeinitBcpinfo(TdsBcpInfo bcpinfo) => NativeMethods.tds_deinit_bcpinfo(bcpinfo != null ? bcpinfo.Ptr : IntPtr.Zero);

        // tds : query.c
        public static string NextPlaceholder(string start) => NativeMethods.tds_next_placeholder(start);
        public static int CountPlaceholders(string query) => NativeMethods.tds_count_placeholders(query);
        public static string SkipComment(string s) => NativeMethods.tds_skip_comment(s);
        public static string SkipQuoted(string s) => NativeMethods.tds_skip_quoted(s);
        public static void ConvertStringFree(string original, string converted) => NativeMethods.tds_convert_string_free(original, converted);

        // tds : token.c
        public static int GetTokenSize(int marker) => NativeMethods.tds_get_token_size(marker);

        // tds : tds_convert.c
        public static TDSRET Datecrack(int datetype, byte[] di, out TDSDATEREC dr) => NativeMethods.tds_datecrack(datetype, di, out dr);
        public static TDS_SERVER_TYPE GetConversionType(TDS_SERVER_TYPE srctype, int colsize) => NativeMethods.tds_get_conversion_type(srctype, colsize);

        // tds : util.c
        public static void SwapBytes(byte[] buf, int bytes) => NativeMethods.tds_swap_bytes(buf, bytes);
        public static uint GettimeMs() => NativeMethods.tds_gettime_ms();
        public static string Strndup(byte[] s, IntPtr len) => NativeMethods.tds_strndup(s, len);

        // tds : log.c
        public static void DumpOff() => NativeMethods.tdsdump_off();
        public static void DumpOn() => NativeMethods.tdsdump_on();
        public static int DumpIsopen() => NativeMethods.tdsdump_isopen();
        public static int DumpOpen(string filename) => NativeMethods.tdsdump_open(filename);
        public static void DumpClose() => NativeMethods.tdsdump_close();
        public static void DumpDumpBuf(string file, uint levelLine, string msg, byte[] buf, int length) => NativeMethods.tdsdump_dump_buf(file, levelLine, msg, buf, (Size_t)length);
        //public static void tdsdump_col(TDSCOLUMN col) => NativeMethods.tdsdump_close();
        //void tdsdump_log(string file, uint level_line, string fmt, ...);
        //#define TDSDUMP_LOG_FAST if (TDS_UNLIKELY(tds_write_dump)) tdsdump_log
        //#define tdsdump_log TDSDUMP_LOG_FAST
        //#define TDSDUMP_BUF_FAST if (TDS_UNLIKELY(tds_write_dump)) tdsdump_dump_buf
        //#define tdsdump_dump_buf TDSDUMP_BUF_FAST
        //extern int tds_write_dump;
        //extern int tds_debug_flags;
        //extern int tds_g_append_mode;

        // tds : net.c
        public static int GetInstancePorts7(IntPtr output, IntPtr addr) => NativeMethods.tds7_get_instance_ports(output, addr);
        public static int GetInstancePort7(IntPtr addr, string instance) => NativeMethods.tds7_get_instance_port(addr, instance);
        public static string Prwsaerror(int erc) => NativeMethods.tds_prwsaerror(erc);
        public static void PrwsaerrorFree(string s) => NativeMethods.tds_prwsaerror_free(s);
        //public static TDSSELREAD() => NativeMethods.TDSSELREAD();
        //public static TDSSELWRITE() => NativeMethods.TDSSELWRITE();
        public static void SocketFlush(IntPtr sock) => NativeMethods.tds_socket_flush(sock);
        public static int SocketSetNonblocking(IntPtr sock) => NativeMethods.tds_socket_set_nonblocking(sock);
        public static int WakeupInit(TDSPOLLWAKEUP wakeup) => NativeMethods.tds_wakeup_init(ref wakeup);
        public static void WakeupClose(TDSPOLLWAKEUP wakeup) => NativeMethods.tds_wakeup_close(ref wakeup);
        public static void WakeupSend(TDSPOLLWAKEUP wakeup, char cancel) => NativeMethods.tds_wakeup_send(ref wakeup, cancel);
        public static IntPtr WakeupGetFd(TDSPOLLWAKEUP wakeup) => NativeMethods.tds_wakeup_get_fd(ref wakeup);

        // tds : vstrbuild.c
        public static TDSRET Vstrbuild(byte[] buffer, ref int resultLen, string text, string formats, params object[] ap) => NativeMethods.tds_vstrbuild(buffer, buffer.Length, ref resultLen, text, text.Length, formats, formats.Length, IntPtr.Zero);

        // tds : numeric.c
        public static string MoneyToString(TDS_MONEY money, string s, bool use2Digits) => NativeMethods.tds_money_to_string(ref money, s, use2Digits);
        public static int NumericToString(TDS_NUMERIC numeric, string s) => NativeMethods.tds_numeric_to_string(ref numeric, s);
        public static int NumericChangePrecScale(TDS_NUMERIC numeric, byte newPrec, byte newScale) => NativeMethods.tds_numeric_change_prec_scale(ref numeric, newPrec, newScale);

        // tds : getmac.c
        public static void Getmac(IntPtr s, byte[] mac) => NativeMethods.tds_getmac(s, mac); //:TDS_SYS_SOCKET

        // tds : random.c
        public static void RandomBuffer(byte[] @out) => NativeMethods.tds_random_buffer(@out, @out.Length);

        #endregion
    }

    public class BcpColData : MarshaledObject<BCPCOLDATA> { }

    public class TdsAuthentication : MarshaledObject<TDSAUTHENTICATION> { }

    public class TdsBcpInfo : MarshaledObject<TDSBCPINFO>
    {
        public TdsBcpInfo() : base(arg => NativeMethods.tds_alloc_bcpinfo(), null, NativeMethods.tds_free_bcpinfo) { }
    }

    public class TdsColumn : MarshaledObject<TDSCOLUMN>
    {
        #region Methods

        // tds : mem.c
        public IntPtr AllocParamData() => NativeMethods.tds_alloc_param_data(Ptr);

        #endregion
    }

    public class TdsConnection : MarshaledObject<TDSCONNECTION>
    {
        #region Properties

        public uint TdsVersion
        {
            get => Value.tds_version;
            set => Marshal.WriteInt32(Ptr + Marshal.OffsetOf<TDSCONNECTION>("tds_version").ToInt32(), (int)value);
        }
        /// <summary>
        /// version of product (Sybase/MS and full version)
        /// </summary>
        public uint ProductVersion => Value.product_version;
        public string ProductName => Value.product_name;
        public IntPtr S => Value.s;
        public TDSPOLLWAKEUP Wakeup => Value.wakeup;
        public TdsContext Context => Value.tds_ctx.ToMarshaledObject<TdsContext, TDSCONTEXT>();
        /// <summary>
        /// environment is shared between all sessions
        /// </summary>
        public TDSENV Env => Value.env;
        /// <summary>
        /// linked list of cursors allocated for this connection contains only cursors allocated on the server
        /// </summary>
        public TdsCursor Cursors => Value.cursors.ToMarshaledObject<TdsCursor, TDSCURSOR>();
        /// <summary>
        /// list of dynamic allocated for this connection contains only dynamic allocated on the server
        /// </summary>
        public TdsDynamic Dyns => Value.dyns.ToMarshaledObject<TdsDynamic, TDSDYNAMIC>();
        public int CharConvCount => Value.char_conv_count;
        public TDSICONV[] CharConvs => Value.char_convs.ToMarshaledArray<TDSICONV>(Value.char_conv_count);
        public string Collation => Value.collation;
        public string Transaction72 => Value.tds72_transaction;
        public TDS_CAPABILITIES Capabilities => Value.capabilities;
        public bool EmulLittleEndian => Value.emul_little_endian != 0;
        public bool UseIconv => Value.use_iconv != 0;
        public bool Tds71Rev1 => Value.tds71rev1 != 0;
        /// <summary>
        /// true is connection has pending closing (cursors or dynamic)
        /// </summary>
        public bool PendingClose => Value.pending_close != 0;
        public bool EncryptSinglePacket => Value.encrypt_single_packet != 0;
#if ENABLE_ODBC_MARS
        public bool Mars => Value.mars != 0;
        public TdsSocket InNetTds => Value.in_net_tds.ToMarshaledObject<TdsSocket, TDSSOCKET>();
        public TdsPacket Packets => Value.packets.ToMarshaledObject<TdsPacket, TDSPACKET>();
        public TdsPacket RecvPacket => Value.recv_packet.ToMarshaledObject<TdsPacket, TDSPACKET>();
        public TdsPacket SendPackets => Value.send_packets.ToMarshaledObject<TdsPacket, TDSPACKET>();
        public int SendPos => (int)Value.send_pos;
        public int RecvPos => (int)Value.recv_pos;
        public tds_mutex ListMtx => Value.list_mtx;
        public IntPtr Sessions => Value.sessions;
        public int NumSessions => (int)Value.num_sessions;
        public int NumCachedPackets => (int)Value.num_cached_packets;
        public TdsPacket PacketCache => Value.packet_cache.ToMarshaledObject<TdsPacket, TDSPACKET>();
#endif
        public int Spid => Value.spid;
        public int ClientSpid => Value.client_spid;
        public IntPtr TlsSession => Value.tls_session;
#if HAVE_GNUTLS
	    public IntPtr TlsCredentials => Value.tls_credentials;
#elif HAVE_OPENSSL
        public IntPtr TlsContext => Value.tls_ctx;
#else
        public IntPtr TlsDummy => Value.tls_dummy;
#endif
        public TdsAuthentication Authentication => Value.authentication.ToMarshaledObject<TdsAuthentication, TDSAUTHENTICATION>();
        public string Server => Value.server;

        #endregion

        #region Methods

        // tds : config.c
        public TdsDynamic LookupDynamic(string id) => NativeMethods.tds_lookup_dynamic(Ptr, id).ToMarshaledObject<TdsDynamic, TDSDYNAMIC>();
        public int GetVarintSize(int datatype) => NativeMethods.tds_get_varint_size(Ptr, datatype);

        // tds : iconv.c
        public TDSRET IconvOpen(string charset, int use_utf16) => NativeMethods.tds_iconv_open(Ptr, charset, use_utf16);
        public void IconvClose() => NativeMethods.tds_iconv_close(Ptr);
        public void SrvCharsetChanged(string charset) => NativeMethods.tds_srv_charset_changed(Ptr, charset);
        public void SrvCharsetChanged7(int sql_collate, int lcid) => NativeMethods.tds7_srv_charset_changed(Ptr, sql_collate, lcid);
        public int IconvAlloc() => NativeMethods.tds_iconv_alloc(Ptr);
        public void IconvFree() => NativeMethods.tds_iconv_free(Ptr);
        public TdsIconv IconvFromCollate(string collate) => NativeMethods.tds_iconv_from_collate(Ptr, collate).ToMarshaledObject<TdsIconv, TDSICONV>();

        // tds : mem.c
        public void CursorDeallocated(TdsCursor cursor) => NativeMethods.tds_cursor_deallocated(Ptr, cursor.Ptr);
        public void DynamicDeallocated(TdsDynamic dyn) => NativeMethods.tds_dynamic_deallocated(Ptr, dyn.Ptr);
        public TdsDynamic AllocDynamic(string id) => NativeMethods.tds_alloc_dynamic(Ptr, id).ToMarshaledObject<TdsDynamic, TDSDYNAMIC>();
        public TdsSocket AllocAdditionalSocket() => NativeMethods.tds_alloc_additional_socket(Ptr).ToMarshaledObject<TdsSocket, TDSSOCKET>();

        // tds : query.c
        public int NeedsUnprepare(TdsDynamic dyn) => NativeMethods.tds_needs_unprepare(Ptr, dyn.Ptr);
        public TDSRET DeferredUnprepare(TdsDynamic dyn) => NativeMethods.tds_deferred_unprepare(Ptr, dyn.Ptr);
        public TDSRET DeferredCursorDealloc(TdsCursor cursor) => NativeMethods.tds_deferred_cursor_dealloc(Ptr, cursor.Ptr);

        // tds : data.c
        public void SetParamType(TdsColumn curcol, TDS_SERVER_TYPE type) => NativeMethods.tds_set_param_type(Ptr, curcol.Ptr, type);
        public void SetColumnType(TdsColumn curcol, TDS_SERVER_TYPE type) => NativeMethods.tds_set_column_type(Ptr, curcol.Ptr, type);

        // tds : net.c
        public void ConnectionClose() => NativeMethods.tds_connection_close(Ptr);

        #endregion
    }

    public class TdsContext : MarshaledObject<TDSCONTEXT>
    {
        static TdsContext() => NativeMethods.Touch();
        public TdsContext() : base(arg => NativeMethods.tds_alloc_context(IntPtr.Zero), null, NativeMethods.tds_free_context) { }
        public TdsContext(TdsContext parent = null) : base(arg => NativeMethods.tds_alloc_context((IntPtr)arg), parent != null ? parent.Ptr : IntPtr.Zero, NativeMethods.tds_free_context) { }

        #region Properties

        public TDSLOCALE Locale => Value.locale.ToMarshaled<TDSLOCALE>();

        public TdsContext Parent => Value.parent.ToMarshaledObject<TdsContext, TDSCONTEXT>();

        //public delegate int MsgHandler_t(TdsContext ctx, TdsSocket s, TdsMessage msg); //:TDSCONTEXT:TDSSOCKET:TDSMESSAGE
        //public delegate int ErrHandler_t(TdsContext ctx, TdsSocket s, TdsMessage msg); //:TDSCONTEXT:TDSSOCKET:TDSMESSAGE
        //public delegate int IntHandler_t(IntPtr a);
        public TDSCONTEXT.msg_handler_t MsgHandler
        {
            get => Value.msg_handler;
            set => Marshal.WriteIntPtr(Ptr, Marshal.OffsetOf<TDSCONTEXT>("msg_handler").ToInt32(), Marshal.GetFunctionPointerForDelegate(value));
        }
        public TDSCONTEXT.err_handler_t ErrHandler
        {
            get => Value.err_handler;
            set => Marshal.WriteIntPtr(Ptr, Marshal.OffsetOf<TDSCONTEXT>("err_handler").ToInt32(), Marshal.GetFunctionPointerForDelegate(value));
        }
        public TDSCONTEXT.int_handler_t IntHandler
        {
            get => Value.int_handler;
            set => Marshal.WriteIntPtr(Ptr, Marshal.OffsetOf<TDSCONTEXT>("int_handler").ToInt32(), Marshal.GetFunctionPointerForDelegate(value));
        }
        public bool MoneyUse2Digits
        {
            get => Value.money_use_2_digits;
            set => Marshal.WriteByte(Ptr + Marshal.OffsetOf<TDSCONTEXT>("money_use_2_digits").ToInt32(), value ? (byte)1 : (byte)0);
        }

        #endregion

        #region Methods

        // tds : mem.c
        public static void FreeSocket(TdsSocket ptr) => NativeMethods.tds_free_socket(ptr != null ? ptr.Ptr : IntPtr.Zero); //:Dispose
        public static void FreeAllResults(TdsSocket ptr) => NativeMethods.tds_free_all_results(ptr != null ? ptr.Ptr : IntPtr.Zero);
        public TdsSocket AllocSocket(int bufsize = 4096)
        {
            var r = NativeMethods.tds_alloc_socket(Ptr, (uint)bufsize).ToMarshaledObject<TdsSocket, TDSSOCKET>();
            NativeMethods.tds_set_parent(r.Ptr, IntPtr.Zero);
            return r;
        }

        // tds : util.c
        public int Error(TdsSocket tds, int msgno, int errnum) => NativeMethods.tdserror(Ptr, tds.Ptr, msgno, errnum);

        // server : login.c
        public TdsSocket Listen(int ipPort = 1433) { var r = NativeMethodsServer.tds_listen(Ptr, ipPort).ToMarshaledObject<TdsSocket, TDSSOCKET>(); r.SetState(TDS_STATE.TDS_IDLE); return r; }

        #endregion
    }

    public class TdsCursor : MarshaledObject<TDSCURSOR>
    {
        public TdsCursor() : base(null, null, ptr => NativeMethods.tds_release_cursor(ref ptr)) { }

        #region Methods

        // tds : mem.c
        public void ReleaseCursor() => this.WithPtr(ptr => NativeMethods.tds_release_cursor(ref ptr)); //:Dispose

        #endregion
    }

    public class TdsDynamic : MarshaledObject<TDSDYNAMIC>
    {
        public TdsDynamic() : base(null, null, ptr => NativeMethods.tds_release_dynamic(ref ptr)) { }

        #region Methods

        // tds : mem.c
        public void FreeInputParams() => NativeMethods.tds_free_input_params(Ptr);
        public void ReleaseDynamic() => this.WithPtr(ptr => NativeMethods.tds_release_dynamic(ref ptr)); //:Dispose

        #endregion
    }

    public class TdsHeaders : MarshaledObject<TDSHEADERS> { }

    public class TdsIconv : MarshaledObject<TDSICONV>
    {
        #region Methods

        // tds : token.c
        public int DetermineAdjustedSize(int size) => NativeMethods.determine_adjusted_size(Ptr, size);

        #endregion
    }

    public class TdsLocale : MarshaledObject<TDSLOCALE>
    {
        public TdsLocale() : base(null, null, NativeMethods.tds_free_locale) { }
    }

    public class TdsLogin : MarshaledObject<TDSLOGIN>
    {
        public TdsLogin() : base() { }
        public TdsLogin(bool useEnvironment) : base(arg => NativeMethods.tds_alloc_login((bool)arg ? 1 : 0), useEnvironment, NativeMethods.tds_free_login) { }

        #region Methods

        // tds : config.c
        public bool ReadConfFile(string server) => NativeMethods.tds_read_conf_file(Ptr, server);
        public void FixLogin() => NativeMethods.tds_fix_login(Ptr);

        // tds : mem.c
        public TdsLogin InitLogin(TdsLocale locale) => NativeMethods.tds_init_login(Ptr, locale.Ptr).ToMarshaledObject<TdsLogin, TDSLOGIN>();

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
        public bool SetClientCharset(string charset) => NativeMethods.tds_set_client_charset(Ptr, charset);
        public bool SetLanguage(string language) => NativeMethods.tds_set_language(Ptr, language);
        public void SetVersion(byte major_ver, byte minor_ver) => NativeMethods.tds_set_version(Ptr, major_ver, minor_ver);

        #endregion
    }

    public class TdsMessage : MarshaledObject<TDSMESSAGE>
    {
        public TdsMessage() : base(null, null, NativeMethods.tds_free_msg) { }

        #region Methods

        // tds : mem.c
        public void FreeMsg() => NativeMethods.tds_free_msg(Ptr); //:Dispose

        #endregion
    }

    public class TdsMultiple : MarshaledObject<TDSMULTIPLE>
    {
        public TdsMultiple() : base(null, null, null) { }
    }

    public class TdsPacket : MarshaledObject<TDSPACKET>
    {
        public TdsPacket() : base(null, null, NativeMethods.tds_free_packets) { }

        #region Methods

        // tds : mem.c
        public TdsPacket ReallocPacket(uint len) => NativeMethods.tds_realloc_packet(Ptr, len).ToMarshaledObject<TdsPacket, TDSPACKET>();

        #endregion
    }

    public class TdsResultInfo : MarshaledObject<TDSRESULTINFO>
    {
        public TdsResultInfo() : base(null, null, NativeMethods.tds_free_results) { }
        public TdsResultInfo(int numColums) : base(arg => NativeMethods.tds_alloc_results((ushort)arg), (ushort)numColums, NativeMethods.tds_free_results) { }

        public TdsColumn[] Columns;

        #region Methods

        // tds : config.c
        public TDSRET AllocRow() => NativeMethods.tds_alloc_row(Ptr);

        // tds : mem.c
        public void FreeRow(byte[] row) => NativeMethods.tds_free_row(Ptr, row);
        public void DetachResults() => NativeMethods.tds_detach_results(Ptr);

        #endregion
    }

    public class TdsComputeInfo : TdsResultInfo
    {
        #region Methods

        // tds : config.c
        public TDSRET AllocComputeRow() => NativeMethods.tds_alloc_compute_row(Ptr);

        #endregion
    }

    public class TdsParamInfo : TdsResultInfo
    {
        #region Methods

        // tds : mem.c
        public static void FreeParamResults(TdsParamInfo paramInfo) => NativeMethods.tds_free_param_results(paramInfo != null ? paramInfo.Ptr : IntPtr.Zero);
        public static void FreeParamResult(TdsParamInfo paramInfo) => NativeMethods.tds_free_param_result(paramInfo != null ? paramInfo.Ptr : IntPtr.Zero);
        public TdsParamInfo AllocParamResult() => NativeMethods.tds_alloc_param_result(Ptr).ToMarshaledObject<TdsParamInfo, TDSRESULTINFO>();

        #endregion
    }

    public class TdsSocket : MarshaledObject<TDSSOCKET>
    {
        public TdsSocket() : base(null, null, NativeMethods.tds_free_socket) { }
        public TdsSocket(TdsContext ctx, int bufSize = 4096) : base(arg => NativeMethods.tds_alloc_socket(ctx.Ptr, (uint)arg), bufSize, NativeMethods.tds_free_socket) { }

        #region Properties

        public TdsContext Context // Special
        {
            get => NativeMethods.tds_get_ctx(Value).ToMarshaledObject<TdsContext, TDSCONTEXT>();
            set => NativeMethods.tds_set_ctx(Value, value != null ? value.Ptr : IntPtr.Zero);
        }
        public IntPtr Socket // Special
        {
            get => NativeMethods.tds_get_s(Value);
            set => NativeMethods.tds_set_s(Value, value);
        }

        public TdsConnection Conn => Value.conn.ToMarshaledObject<TdsConnection, TDSCONNECTION>();
        public int[] InBuf => null; // in_buf,int_len
        public int[] OutBuf => null; // out_buf, out_buf_max
        public int InPos => (int)Value.in_pos;
        public int OutPos => (int)Value.out_pos;
        public byte InFlag => Value.in_flag;
        public TDS_PACKET_TYPE OutFlag
        {
            get => Value.out_flag;
            set => Marshal.WriteByte(Ptr + Marshal.OffsetOf<TDSSOCKET>("out_flag").ToInt32(), (byte)value);
        }
        public TdsSocket Parent // Special
        {
            get => NativeMethods.tds_get_parent(Value).ToMarshaled<TdsSocket>();
            set => NativeMethods.tds_set_parent(Ptr, value != null ? value.Ptr : IntPtr.Zero);
        }
#if ENABLE_ODBC_MARS
        //public short sid;
        //public IntPtr packet_cond; //: tds_condition
        //public uint recv_seq;
        //public uint send_seq;
        //public uint recv_wnd;
        //public uint send_wnd;
#endif
        public TdsPacket RecvPacket => Value.recv_packet.ToMarshaledObject<TdsPacket, TDSPACKET>();
        public TdsPacket SendPacket => Value.send_packet.ToMarshaledObject<TdsPacket, TDSPACKET>();
        public TdsResultInfo CurrentResults => Value.current_results.ToMarshaledObject<TdsResultInfo, TDSRESULTINFO>();
        public TdsResultInfo ResInfo => Value.res_info.ToMarshaledObject<TdsResultInfo, TDSRESULTINFO>();
        public int NumCompInfo => (int)Value.num_comp_info;
        public TdsComputeInfo CompInfo => Value.comp_info.ToMarshaledObject<TdsComputeInfo, TDSCOMPUTEINFO>();
        public TdsParamInfo ParamInfo => Value.param_info.ToMarshaledObject<TdsParamInfo, TDSPARAMINFO>();
        public TdsCursor CurCursor => Value.cur_cursor.ToMarshaledObject<TdsCursor, TDSCURSOR>();
        public bool BulkQuery => Value.bulk_query;
        public bool HasStatus => Value.has_status;
        public bool InRow => Value.in_row;
        public int RetStatus => Value.ret_status;
        public TDS_STATE State => Value.state;
        public byte InCancel => Value.in_cancel;
        public long RowsAffected => Value.rows_affected;
        public int QueryTimeout
        {
            get => Value.query_timeout;
            set => Marshal.WriteInt32(Ptr + Marshal.OffsetOf<TDSSOCKET>("query_timeout").ToInt32(), value);
        }
        public TdsDynamic CurDyn => Value.cur_dyn.ToMarshaledObject<TdsDynamic, TDSDYNAMIC>();
        public TdsLogin Login => Value.login.ToMarshaledObject<TdsLogin, TDSLOGIN>();

        public delegate TDSRET EnvChgFunc_t(TdsSocket tds, int type, string oldValue, string newValue);
        public TDSSOCKET.env_chg_func_t EnvChgFunc
        {
            get => Value.env_chg_func;
            set => Marshal.WriteIntPtr(Ptr, Marshal.OffsetOf<TDSSOCKET>("env_chg_func").ToInt32(), Marshal.GetFunctionPointerForDelegate(value));
        }
        public TDS_OPERATION CurrentOp => Value.current_op;
        public int OptionValue => Value.option_value;
        public tds_mutex WireMtx => Value.wire_mtx;

        #endregion

        #region Methods

        // tds : config.c
        public TdsLogin ReadConfigInfo(ref TdsLogin login, TDSLOCALE locale) => NativeMethods.tds_read_config_info(Ptr, login.Ptr, ref locale).ToMarshaledObject<TdsLogin, TDSLOGIN>();

        // tds : mem.c
        public TdsComputeInfo[] AllocComputeResults(ushort num_cols, ushort by_cols) { var ptr = NativeMethods.tds_alloc_compute_results(Ptr, num_cols, by_cols); throw new NotImplementedException(); }
        public void ReleaseCurDyn() => NativeMethods.tds_release_cur_dyn(this);
        public void SetCurDyn(TdsDynamic dyn) => NativeMethods.tds_set_cur_dyn(Ptr, dyn.Ptr);
        public TdsSocket ReallocSocket(int bufsize) => NativeMethods.tds_realloc_socket(Ptr, (Size_t)bufsize).ToMarshaledObject<TdsSocket, TDSSOCKET>();
        public string AllocLookupSqlstate(int msgno) => NativeMethods.tds_alloc_lookup_sqlstate(Ptr, msgno);
        public TdsCursor AllocCursor(string name, string query) => NativeMethods.tds_alloc_cursor(Ptr, name, name.Length, query, query.Length).ToMarshaledObject<TdsCursor, TDSCURSOR>();
        public void SetCurrentResults(TdsResultInfo info) => NativeMethods.tds_set_current_results(Ptr, info.Ptr);

        // tds : login.c
        public TDSRET ConnectAndLogin(TdsLogin login) => (TDSRET)NativeMethods.tds_connect_and_login(Ptr, login.Ptr);

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
        public TDSRET SubmitBeginTran() => NativeMethods.tds_submit_begin_tran(Ptr);
        public TDSRET SubmitRollback(int cont) => NativeMethods.tds_submit_rollback(Ptr, cont);
        public TDSRET SubmitCommit(int cont) => NativeMethods.tds_submit_commit(Ptr, cont);
        public TDSRET Disconnect() => NativeMethods.tds_disconnect(Ptr);
        public int QuoteId(byte[] buffer, string id) => (int)NativeMethods.tds_quote_id(Ptr, buffer, id, id.Length);
        public int QuoteString(byte[] buffer, string str) => (int)NativeMethods.tds_quote_string(Ptr, buffer, str, str.Length);
        public int FixColumnSize(TdsColumn curcol) => (int)NativeMethods.tds_fix_column_size(Ptr, curcol.Ptr);
        public string ConvertString(TdsIconv char_conv, string s, int len, out int out_len) { var r = NativeMethods.tds_convert_string(Ptr, char_conv.Ptr, s, len, out var out_len_); out_len = (int)out_len_; return r; }
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
        public TDSRET ProcessTokens(out int result_type, out int done_flags, tds_token_flags flag) => NativeMethods.tds_process_tokens(Ptr, out result_type, out done_flags, (uint)flag);

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
        public bool GetN(out byte[] dest, int n) => NativeMethods.tds_get_n(Ptr, out dest, (Size_t)n);
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
        public int AppendCancel() => NativeMethods.tds_append_cancel(Ptr);
        public TDSRET AppendFin() => NativeMethods.tds_append_fin(Ptr);
#else
        public int PutCancel() => NativeMethods.tds_put_cancel(Ptr);
#endif

        // tds : challenge.c
#if !HAVE_SSPI
        TdsAuthentication NtlmGetAuth() => NativeMethods.tds_ntlm_get_auth(Ptr).ToMarshaledObject<TdsAuthentication, TDSAUTHENTICATION>();
        TdsAuthentication GssGetAuth() => NativeMethods.tds_gss_get_auth(Ptr).ToMarshaledObject<TdsAuthentication, TDSAUTHENTICATION>();
#else
        TdsAuthentication SspiGetAuth() => NativeMethods.tds_sspi_get_auth(_ctx).ToMarshaledObject<TdsAuthentication, TDSAUTHENTICATION>();
#endif

        // tds : sec_negotiate.c
        public TdsAuthentication NegotiateGetAuth5() => NativeMethods.tds5_negotiate_get_auth(Ptr).ToMarshaledObject<TdsAuthentication, TDSAUTHENTICATION>();
        public void NegotiateSetMsgType5(TdsAuthentication auth, uint msg_type) => NativeMethods.tds5_negotiate_set_msg_type(Ptr, auth.Ptr, msg_type);

        // tds : bulk.c
        public TDSRET BcpInit(TdsBcpInfo bcpinfo) => NativeMethods.tds_bcp_init(Ptr, bcpinfo.Ptr);
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
        public int ReadLogin(out TdsLogin login) { var r = NativeMethodsServer.tds_read_login(Ptr, out var loginPtr); login = new TdsLogin { Ptr = loginPtr }; return r; }
        public int ReadLogin7(out TdsLogin login) { var r = NativeMethodsServer.tds7_read_login(Ptr, out var loginPtr); login = new TdsLogin { Ptr = loginPtr }; return r; }
        public TdsLogin AllocReadLogin() => NativeMethodsServer.tds_alloc_read_login(Ptr).ToMarshaledObject<TdsLogin, TDSLOGIN>();

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

        #endregion
    }
}