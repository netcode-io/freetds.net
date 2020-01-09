using System;
using System.Runtime.InteropServices;
using TDSCOMPUTEINFO = FreeTds.TDSRESULTINFO;
using TDSPARAMINFO = FreeTds.TDSRESULTINFO;
using Size_t = System.IntPtr;

namespace FreeTds
{
    static partial class G { }

    /// <summary>
    /// A structure to hold all the compile-time settings.
    /// This structure is returned by tds_get_compiletime_settings
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class TDS_COMPILETIME_SETTINGS
    {
        /// <summary>
        /// release version of FreeTDS
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)] public string freetds_version;
        /// <summary>
        /// location of freetds.conf
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)] public string sysconfdir;
        /// <summary>
        /// latest software_version date among the modules
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)] public string last_update;
        /// <summary>
        /// for MS style dblib
        /// </summary>
        public int msdblib;
        /// <summary>
        /// enable increased Open Client binary compatibility
        /// </summary>
        public int sybase_compat;
        /// <summary>
        /// compile for thread safety default=no
        /// </summary>
        public int threadsafe;
        /// <summary>
        /// search for libiconv in DIR/include and DIR/lib
        /// </summary>
        public int libiconv;
        /// <summary>
        /// TDS protocol version (4.2/4.6/5.0/7.0/7.1) 5.0
        /// </summary>
        public string tdsver;
        /// <summary>
        /// build odbc driver against iODBC in DIR
        /// </summary>
        public int iodbc;
        /// <summary>
        /// build odbc driver against unixODBC in DIR
        /// </summary>
        public int unixodbc;
        /// <summary>
        /// build against OpenSSL
        /// </summary>
        public int openssl;
        /// <summary>
        /// build against GnuTLS
        /// </summary>
        public int gnutls;
        /// <summary>
        /// MARS enabled
        /// </summary>
        public int mars;
    }

    /// <summary>
    /// this structure is not directed connected to a TDS protocol but keeps any DATE/TIME information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class TDS_DATETIMEALL
    {
        /// <summary>
        /// time, 7 digit precision
        /// </summary>
        public byte time;
        /// <summary>
        /// date, 0 = 1900-01-01
        /// </summary>
        public int date;
        /// <summary>
        /// time offset
        /// </summary>
        public short offset;
        int _data_;
        public ushort time_prec => 0; //:3;
        public ushort _tds_reserved => 0; //:10;
        public ushort has_time => 0; //:1;
        public ushort has_date => 0; //:1;
        public ushort has_offset => 0; //:1;
    }

    /// <summary>
    /// Used by tds_datecrack 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class TDSDATEREC
    {
        /// <summary>
        /// year
        /// </summary>
        public int year;
        /// <summary>
        /// quarter (0-3)
        /// </summary>
        public int quarter;
        /// <summary>
        /// month number (0-11)
        /// </summary>
        public int month;
        /// <summary>
        /// day of month (1-31)
        /// </summary>
        public int day;
        /// <summary>
        /// day of year  (1-366)
        /// </summary>
        public int dayofyear;
        /// <summary>
        /// day of week  (0-6, 0 = sunday)
        /// </summary>
        public int weekday;
        /// <summary>
        /// 0-23
        /// </summary>
        public int hour;
        /// <summary>
        /// 0-59
        /// </summary>
        public int minute;
        /// <summary>
        /// 0-59
        /// </summary>
        public int second;
        /// <summary>
        /// 0-9999999
        /// </summary>
        public int decimicrosecond;
        /// <summary>
        /// -840 - 840 minutes from UTC
        /// </summary>
        public int timezone;
    }

    partial class G
    {
        /// <summary>
        /// The following little table is indexed by precision and will tell us the number of bytes required to store the specified precision.
        /// </summary>
        public static readonly int[] tds_numeric_bytes_per_prec;
    }

    public enum TDSRET : int //:sky
    {
        TDS_NO_MORE_RESULTS = 1,
        TDS_SUCCESS = 0,
        TDS_FAIL = -1,
        TDS_CANCELLED = -2,
    }

    partial class G
    {
        public static bool TDS_FAILED(this TDSRET rc) => rc < 0;
        public static bool TDS_SUCCEED(this TDSRET rc) => rc >= 0;
        public static TDSRET TDS_PROPAGATE(this TDSRET rc) { do { TDSRET _tds_ret = rc; if (TDS_FAILED(_tds_ret)) return _tds_ret; } while (true); }
    }

    partial class G
    {
        public const int TDS_INT_CONTINUE = 1;
        public const int TDS_INT_CANCEL = 2;
        public const int TDS_INT_TIMEOUT = 3;

        public const int TDS_NO_COUNT = -1;

        public const int TDS_ROW_RESULT = 4040;
        public const int TDS_PARAM_RESULT = 4042;
        public const int TDS_STATUS_RESULT = 4043;
        public const int TDS_MSG_RESULT = 4044;
        public const int TDS_COMPUTE_RESULT = 4045;
        public const int TDS_CMD_DONE = 4046;
        public const int TDS_CMD_SUCCEED = 4047;
        public const int TDS_CMD_FAIL = 4048;
        public const int TDS_ROWFMT_RESULT = 4049;
        public const int TDS_COMPUTEFMT_RESULT = 4050;
        public const int TDS_DESCRIBE_RESULT = 4051;
        public const int TDS_DONE_RESULT = 4052;
        public const int TDS_DONEPROC_RESULT = 4053;
        public const int TDS_DONEINPROC_RESULT = 4054;
        public const int TDS_OTHERS_RESULT = 4055;
    }

    public enum tds_token_results
    {
        TDS_TOKEN_RES_OTHERS,
        TDS_TOKEN_RES_ROWFMT,
        TDS_TOKEN_RES_COMPUTEFMT,
        TDS_TOKEN_RES_PARAMFMT,
        TDS_TOKEN_RES_DONE,
        TDS_TOKEN_RES_ROW,
        TDS_TOKEN_RES_COMPUTE,
        TDS_TOKEN_RES_PROC,
        TDS_TOKEN_RES_MSG,
        TDS_TOKEN_RES_ENV,
    }

    [Flags]
    public enum tds_token_flags
    {
        TDS_HANDLE_ALL = 0,
        TDS_RETURN_OTHERS = 1 << (tds_token_results.TDS_TOKEN_RES_OTHERS * 2), TDS_STOPAT_OTHERS = 2 << (tds_token_results.TDS_TOKEN_RES_OTHERS * 2),
        TDS_RETURN_ROWFMT = 1 << (tds_token_results.TDS_TOKEN_RES_ROWFMT * 2), TDS_STOPAT_ROWFMT = 2 << (tds_token_results.TDS_TOKEN_RES_ROWFMT * 2),
        TDS_RETURN_COMPUTEFMT = 1 << (tds_token_results.TDS_TOKEN_RES_COMPUTEFMT * 2), TDS_STOPAT_COMPUTEFMT = 2 << (tds_token_results.TDS_TOKEN_RES_OTHERS * 2),
        TDS_RETURN_PARAMFMT = 1 << (tds_token_results.TDS_TOKEN_RES_PARAMFMT * 2), TDS_STOPAT_PARAMFMT = 2 << (tds_token_results.TDS_TOKEN_RES_OTHERS * 2),
        TDS_RETURN_DONE = 1 << (tds_token_results.TDS_TOKEN_RES_DONE * 2), TDS_STOPAT_DONE = 2 << (tds_token_results.TDS_TOKEN_RES_OTHERS * 2),
        TDS_RETURN_ROW = 1 << (tds_token_results.TDS_TOKEN_RES_ROW * 2), TDS_STOPAT_ROW = 2 << (tds_token_results.TDS_TOKEN_RES_OTHERS * 2),
        TDS_RETURN_COMPUTE = 1 << (tds_token_results.TDS_TOKEN_RES_COMPUTE * 2), TDS_STOPAT_COMPUTE = 2 << (tds_token_results.TDS_TOKEN_RES_OTHERS * 2),
        TDS_RETURN_PROC = 1 << (tds_token_results.TDS_TOKEN_RES_PROC * 2), TDS_STOPAT_PROC = 2 << (tds_token_results.TDS_TOKEN_RES_OTHERS * 2),
        TDS_RETURN_MSG = 1 << (tds_token_results.TDS_TOKEN_RES_MSG * 2), TDS_STOPAT_MSG = 2 << (tds_token_results.TDS_TOKEN_RES_OTHERS * 2),
        TDS_RETURN_ENV = 1 << (tds_token_results.TDS_TOKEN_RES_ENV * 2), TDS_STOPAT_ENV = 2 << (tds_token_results.TDS_TOKEN_RES_OTHERS * 2),
        TDS_TOKEN_RESULTS = TDS_RETURN_ROWFMT | TDS_RETURN_COMPUTEFMT | TDS_RETURN_DONE | TDS_STOPAT_ROW | TDS_STOPAT_COMPUTE | TDS_RETURN_PROC,
        TDS_TOKEN_TRAILING = TDS_STOPAT_ROWFMT | TDS_STOPAT_COMPUTEFMT | TDS_STOPAT_ROW | TDS_STOPAT_COMPUTE | TDS_STOPAT_MSG | TDS_STOPAT_OTHERS
    }

    /// <summary>
    /// Flags returned in TDS_DONE token
    /// </summary>
    public enum tds_end
    {
        /// <summary>
        /// final result set, command completed successfully.
        /// </summary>
        TDS_DONE_FINAL = 0x00,
        /// <summary>
        /// more results follow
        /// </summary>
        TDS_DONE_MORE_RESULTS = 0x01,
        /// <summary>
        /// error occurred
        /// </summary>
        TDS_DONE_ERROR = 0x02,
        /// <summary>
        /// transaction in progress
        /// </summary>
        TDS_DONE_INXACT = 0x04,
        /// <summary>
        /// results are from a stored procedure
        /// </summary>
        TDS_DONE_PROC = 0x08,
        /// <summary>
        /// count field in packet is valid
        /// </summary>
        TDS_DONE_COUNT = 0x10,
        /// <summary>
        /// acknowledging an attention command (usually a cancel)
        /// </summary>
        TDS_DONE_CANCELLED = 0x20,
        /// <summary>
        /// part of an event notification.
        /// </summary>
        TDS_DONE_EVENT = 0x40,
        /// <summary>
        /// SQL server server error
        /// </summary>
        TDS_DONE_SRVERROR = 0x100,

        /* after the above flags, a TDS_DONE packet has a field describing the state of the transaction */
        /// <summary>
        /// No transaction in effect
        /// </summary>
        TDS_DONE_NO_TRAN = 0,
        /// <summary>
        /// Transaction completed successfully
        /// </summary>
        TDS_DONE_TRAN_SUCCEED = 1,
        /// <summary>
        /// Transaction in progress
        /// </summary>
        TDS_DONE_TRAN_PROGRESS = 2,
        /// <summary>
        /// A statement aborted
        /// </summary>
        TDS_DONE_STMT_ABORT = 3,
        /// <summary>
        /// Transaction aborted
        /// </summary>
        TDS_DONE_TRAN_ABORT = 4
    }

    /// <summary>
    /// TDSERRNO is emitted by libtds to the client library's error handler (which may in turn call the client's error handler).
    /// These match the db-lib msgno, because the same values have the same meaning in db-lib and ODBC.ct-lib maps them to ct-lib numbers(todo). 
    /// </summary>
    public enum TDSERRNO
    {
        TDSEOK = TDSRET.TDS_SUCCESS,
        TDSEVERDOWN = 100,
        TDSEINPROGRESS,
        TDSEICONVIU = 2400,
        TDSEICONVAVAIL = 2401,
        TDSEICONVO = 2402,
        TDSEICONVI = 2403,
        TDSEICONV2BIG = 2404,
        TDSEPORTINSTANCE = 2500,
        TDSESYNC = 20001,
        TDSEFCON = 20002,
        TDSETIME = 20003,
        TDSEREAD = 20004,
        TDSEWRIT = 20006,
        TDSESOCK = 20008,
        TDSECONN = 20009,
        TDSEMEM = 20010,
        /// <summary>
        /// Server name not found in interface file
        /// </summary>
        TDSEINTF = 20012,
        /// <summary>
        /// Unknown host machine name.
        /// </summary>
        TDSEUHST = 20013,
        TDSEPWD = 20014,
        TDSESEOF = 20017,
        TDSERPND = 20019,
        TDSEBTOK = 20020,
        TDSEOOB = 20022,
        TDSECLOS = 20056,
        TDSEUSCT = 20058,
        TDSEUTDS = 20146,
        TDSEEUNR = 20185,
        TDSECAP = 20203,
        TDSENEG = 20210,
        TDSEUMSG = 20212,
        TDSECAPTYP = 20213,
        TDSECONF = 20214,
        TDSEBPROBADTYP = 20250,
        TDSECLOSEIN = 20292
    }

    public enum TDS01_
    {
        TDS_CUR_ISTAT_UNUSED = 0x00,
        TDS_CUR_ISTAT_DECLARED = 0x01,
        TDS_CUR_ISTAT_OPEN = 0x02,
        TDS_CUR_ISTAT_CLOSED = 0x04,
        TDS_CUR_ISTAT_RDONLY = 0x08,
        TDS_CUR_ISTAT_UPDATABLE = 0x10,
        TDS_CUR_ISTAT_ROWCNT = 0x20,
        TDS_CUR_ISTAT_DEALLOC = 0x40
    }

    partial class G
    {
        /// <summary>
        /// string types
        /// </summary>
        public const int TDS_NULLTERM = -9;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct TDS_OPTION_ARG
    {
        [FieldOffset(0)]
        public byte ti;
        [FieldOffset(0)]
        public int i;
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.LPTStr)] public string c;
    }

    public enum TDS_ENCRYPTION_LEVEL
    {
        TDS_ENCRYPTION_DEFAULT,
        TDS_ENCRYPTION_OFF,
        TDS_ENCRYPTION_REQUEST,
        TDS_ENCRYPTION_REQUIRE
    }

    partial class G
    {
        /*
        * TODO use system macros for optimization
        * See mcrypt for reference and linux kernel source for optimization check if unaligned access and use fast write/read when implemented
        */
        public static ushort TDS_BYTE_SWAP16(ushort value) =>
            (ushort)(((((ushort)value) << 8) & 0xFF00u) |
            ((((ushort)value) >> 8) & 0x00FFu));

        public static uint TDS_BYTE_SWAP32(uint value) =>
            (((((uint)value) << 24) & 0xFF000000u) |
            ((((uint)value) << 8) & 0x00FF0000u) |
            ((((uint)value) >> 8) & 0x0000FF00u) |
            ((((uint)value) >> 24) & 0x000000FFu));
    }

    partial class G
    {
        public static bool is_end_token(int x) => x >= P.TDS_DONE_TOKEN && x <= P.TDS_DONEINPROC_TOKEN;
    }

    public enum _TYPEFLAG : ushort
    {
        TDS_TYPEFLAG_INVALID = 0,
        TDS_TYPEFLAG_NULLABLE = 1,
        TDS_TYPEFLAG_FIXED = 2,
        TDS_TYPEFLAG_VARIABLE = 4,
        TDS_TYPEFLAG_COLLATE = 8,
        TDS_TYPEFLAG_ASCII = 16,
        TDS_TYPEFLAG_UNICODE = 32,
        TDS_TYPEFLAG_BINARY = 64,
        TDS_TYPEFLAG_DATETIME = 128,
        TDS_TYPEFLAG_NUMERIC = 256,
    }

    partial class G
    {
        public static readonly _TYPEFLAG[] tds_type_flags_ms; //:sky
    }

    partial class G
    {
        public static bool is_fixed_type(int x) => (tds_type_flags_ms[x] & _TYPEFLAG.TDS_TYPEFLAG_FIXED) != 0;
        public static bool is_nullable_type(int x) => (tds_type_flags_ms[x] & _TYPEFLAG.TDS_TYPEFLAG_NULLABLE) != 0;
        public static bool is_variable_type(int x) => (tds_type_flags_ms[x] & _TYPEFLAG.TDS_TYPEFLAG_VARIABLE) != 0;

        public static bool is_blob_type(TDS_SERVER_TYPE x) => x == TDS_SERVER_TYPE.SYBTEXT || x == TDS_SERVER_TYPE.SYBIMAGE || x == TDS_SERVER_TYPE.SYBNTEXT;
        public static bool is_blob_col(TDSCOLUMN x) => x.column_varint_size > 2;
        ///// <summary>
        ///// large type means it has a two byte size field
        ///// </summary>
        ///// <param name="x"></param>
        ///// <returns></returns>
        //public static bool is_large_type(int x) => x > 128;
        public static bool is_numeric_type(TDS_SERVER_TYPE x) => x == TDS_SERVER_TYPE.SYBNUMERIC || x == TDS_SERVER_TYPE.SYBDECIMAL;
        /// <summary>
        /// return true if type is a datetime (but not date or time)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool is_datetime_type(int x) => (tds_type_flags_ms[x] & _TYPEFLAG.TDS_TYPEFLAG_DATETIME) != 0;
        public static bool is_unicode_type(int x) => (tds_type_flags_ms[x] & _TYPEFLAG.TDS_TYPEFLAG_UNICODE) != 0;
        public static bool is_collate_type(int x) => (tds_type_flags_ms[x] & _TYPEFLAG.TDS_TYPEFLAG_COLLATE) != 0;
        public static bool is_ascii_type(int x) => (tds_type_flags_ms[x] & _TYPEFLAG.TDS_TYPEFLAG_ASCII) != 0;
        public static bool is_binary_type(int x) => (tds_type_flags_ms[x] & _TYPEFLAG.TDS_TYPEFLAG_BINARY) != 0;
        public static bool is_char_type(int x) => (tds_type_flags_ms[x] & (_TYPEFLAG.TDS_TYPEFLAG_ASCII | _TYPEFLAG.TDS_TYPEFLAG_UNICODE)) != 0;
        public static bool is_similar_type(int x, int y) => is_char_type(x) && is_char_type(y);

        public static bool is_tds_type_valid(int type) => (uint)type < 256u && tds_type_flags_ms[type] != 0;

        public const int TDS_MAX_CAPABILITY = 32;
        public const int MAXPRECISION = 77;
        public const int TDS_MAX_CONN = 4096;
        public const int TDS_MAX_DYNID_LEN = 30;

        /* defaults to use if no others are found */
        public const string TDS_DEF_SERVER = "SYBASE";
        public const int TDS_DEF_BLKSZ = 512;
        public const string TDS_DEF_CHARSET = "iso_1";
        public const string TDS_DEF_LANG = "us_english";
#if TDS50
        public const int TDS_DEFAULT_VERSION = 0x500;
        public const int TDS_DEF_PORT = 4000;
#elif TDS71
        public const int TDS_DEFAULT_VERSION = 0x701;
        public const int TDS_DEF_PORT = 1433;
#elif TDS72
        public const int TDS_DEFAULT_VERSION = 0x702;
        public const int TDS_DEF_PORT = 1433;
#elif TDS73
        public const int TDS_DEFAULT_VERSION = 0x703;
        public const int TDS_DEF_PORT = 1433;
#elif TDS74
        public const int TDS_DEFAULT_VERSION = 0x704;
        public const int TDS_DEF_PORT = 1433;
#else
        public const int TDS_DEFAULT_VERSION = 0x000;
        public const int TDS_DEF_PORT = 1433;
#endif

        /* normalized strings from freetds.conf file */
        public const string TDS_STR_VERSION = "tds version";
        public const string TDS_STR_BLKSZ = "initial block size";
        public const string TDS_STR_SWAPDT = "swap broken dates";
        public const string TDS_STR_DUMPFILE = "dump file";
        public const string TDS_STR_DEBUGLVL = "debug level";
        public const string TDS_STR_DEBUGFLAGS = "debug flags";
        public const string TDS_STR_TIMEOUT = "timeout";
        public const string TDS_STR_QUERY_TIMEOUT = "query timeout";
        public const string TDS_STR_CONNTIMEOUT = "connect timeout";
        public const string TDS_STR_HOSTNAME = "hostname";
        public const string TDS_STR_HOST = "host";
        public const string TDS_STR_PORT = "port";
        public const string TDS_STR_TEXTSZ = "text size";
        /* for big endian hosts */
        public const string TDS_STR_EMUL_LE = "emulate little endian";
        public const string TDS_STR_CHARSET = "charset";
        public const string TDS_STR_CLCHARSET = "client charset";
        public const string TDS_STR_USE_UTF_16 = "use utf-16";
        public const string TDS_STR_LANGUAGE = "language";
        public const string TDS_STR_APPENDMODE = "dump file append";
        public const string TDS_STR_DATEFMT = "date format";
        public const string TDS_STR_INSTANCE = "instance";
        public const string TDS_STR_ASA_DATABASE = "asa database";
        public const string TDS_STR_DATABASE = "database";
        public const string TDS_STR_ENCRYPTION = "encryption";
        public const string TDS_STR_USENTLMV2 = "use ntlmv2";
        public const string TDS_STR_USELANMAN = "use lanman";
        /* conf values */
        public const string TDS_STR_ENCRYPTION_OFF = "off";
        public const string TDS_STR_ENCRYPTION_REQUEST = "request";
        public const string TDS_STR_ENCRYPTION_REQUIRE = "require";
        /* Defines to enable optional GSSAPI delegation */
        public const string TDS_GSSAPI_DELEGATION = "enable gssapi delegation";
        /* Kerberos realm name */
        public const string TDS_STR_REALM = "realm";
        /* Kerberos SPN */
        public const string TDS_STR_SPN = "spn";
        /* CA file */
        public const string TDS_STR_CAFILE = "ca file";
        /* CRL file */
        public const string TDS_STR_CRLFILE = "crl file";
        /* check SSL hostname */
        public const string TDS_STR_CHECKSSLHOSTNAME = "check certificate hostname";
        /* database filename to attach on login (MSSQL) */
        public const string TDS_STR_DBFILENAME = "database filename";
        /* Application Intent MSSQL 2012 support */
        public const string TDS_STR_READONLY_INTENT = "read-only intent";
        /* configurable cipher suite to send to openssl's SSL_set_cipher_list() function */
        public const string TLS_STR_OPENSSL_CIPHERS = "openssl ciphers";
        /* enable old TLS v1, required for instance if you are using a really old Windows XP */
        public const string TDS_STR_ENABLE_TLS_V1 = "enable tls v1";
    }

    partial class G
    {
        /* TODO do a better check for alignment than this */
        struct tds_align_struct
        {
            IntPtr p;
            int i;
            long ui;
        }

        public static int TDS_ALIGN_SIZE = Marshal.SizeOf(typeof(tds_align_struct)); /*SKY:TODO*/
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct TDS_CAPABILITY_TYPE
    {
        public byte type;
        public byte len; /* always sizeof(values) */
        public fixed byte values[(int)G.TDS_MAX_CAPABILITY / 2 - 2];
    }

    public unsafe struct TDS_CAPABILITIES
    {
        public fixed int types[2]; //: TDS_CAPABILITY_TYPE
    }

    partial class G
    {
        public const int TDS_MAX_LOGIN_STR_SZ = 128;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSLOGIN
    {
        /// <summary>
        /// server name (in freetds.conf)
        /// </summary>
        [MarshalAs(UnmanagedType.TBStr)] public string server_name;
        /// <summary>
        /// port of database service
        /// </summary>
        public int port;
        /// <summary>
        /// TDS version
        /// </summary>
        public ushort tds_version;
        public int block_size;
        /// <summary>
        /// e.g. us-english
        /// </summary>
        [MarshalAs(UnmanagedType.TBStr)] public string language;
        /// <summary>
        /// charset of server e.g. iso_1
        /// </summary>
        [MarshalAs(UnmanagedType.TBStr)] public string server_charset;
        public int connect_timeout;
        [MarshalAs(UnmanagedType.TBStr)] public string client_host_name;
        [MarshalAs(UnmanagedType.TBStr)] public string server_host_name;
        /// <summary>
        /// server realm name (in freetds.conf)
        /// </summary>
        [MarshalAs(UnmanagedType.TBStr)] public string server_realm_name;
        /// <summary>
        /// server SPN (in freetds.conf)
        /// </summary>
        [MarshalAs(UnmanagedType.TBStr)] public string server_spn;
        /// <summary>
        /// database filename to attach (MSSQL)
        /// </summary>
        [MarshalAs(UnmanagedType.TBStr)] public string db_filename;
        /// <summary>
        /// certificate authorities file
        /// </summary>
        [MarshalAs(UnmanagedType.TBStr)] public string cafile;
        /// <summary>
        /// certificate revocation file
        /// </summary>
        [MarshalAs(UnmanagedType.TBStr)] public string crlfile;
        [MarshalAs(UnmanagedType.TBStr)] public string openssl_ciphers;
        [MarshalAs(UnmanagedType.TBStr)] public string app_name;
        /// <summary>
        /// account for login
        /// </summary>
        [MarshalAs(UnmanagedType.TBStr)] public string user_name;
        /// <summary>
        /// password of account login
        /// </summary>
        [MarshalAs(UnmanagedType.TBStr)] public string password;
        /// <summary>
        /// new password to set (TDS 7.2+)
        /// </summary>
        [MarshalAs(UnmanagedType.TBStr)] public string new_password;

        /// <summary>
        /// Ct-Library, DB-Library,  TDS-Library or ODBC
        /// </summary>
        [MarshalAs(UnmanagedType.TBStr)] public string library;
        public byte encryption_level;

        public int query_timeout;
        public TDS_CAPABILITIES capabilities;
        [MarshalAs(UnmanagedType.TBStr)] public string client_charset;
        [MarshalAs(UnmanagedType.TBStr)] public string database;

        /// <summary>
        /// ip(s) of server
        /// </summary>
        public IntPtr ip_addrs; //:SKY:addrinfo
        [MarshalAs(UnmanagedType.TBStr)] public string instance_name;
        [MarshalAs(UnmanagedType.TBStr)] public string dump_file;
        public int debug_flags;
        public int text_size;
        [MarshalAs(UnmanagedType.TBStr)] public string routing_address;
        public ushort routing_port;

        public byte option_flag2;

        ushort _data_;
        /// <summary>
        /// if bulk copy should be enabled
        /// </summary>
        public uint bulk_copy => (uint)((_data_ >> 0) & 1);
        public uint suppress_language => (uint)((_data_ >> 1) & 1);
        public uint emul_little_endian => (uint)((_data_ >> 2) & 1);
        public uint gssapi_use_delegation => (uint)((_data_ >> 3) & 1);
        public uint use_ntlmv2 => (uint)((_data_ >> 4) & 1);
        public uint use_ntlmv2_specified => (uint)((_data_ >> 5) & 1);
        public uint use_lanman => (uint)((_data_ >> 6) & 1);
        public uint mars => (uint)((_data_ >> 7) & 1);
        public uint use_utf16 => (uint)((_data_ >> 8) & 1);
        public uint use_new_password => (uint)((_data_ >> 9) & 1);
        public uint valid_configuration => (uint)((_data_ >> 10) & 1);
        public uint check_ssl_hostname => (uint)((_data_ >> 11) & 1);
        public uint readonly_intent => (uint)((_data_ >> 12) & 1);
        public uint enable_tls_v1 => (uint)((_data_ >> 13) & 1);
        public uint server_is_valid => (uint)((_data_ >> 14) & 1);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSHEADERS
    {
        [MarshalAs(UnmanagedType.LPTStr)] public string qn_options;
        [MarshalAs(UnmanagedType.LPTStr)] public string qn_msgtext;
        public int qn_timeout;
        /* TDS 7.4+: trace activity ID char[20] */
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSLOCALE
    {
        [MarshalAs(UnmanagedType.LPTStr)] public string language;
        [MarshalAs(UnmanagedType.LPTStr)] public string server_charset;
        [MarshalAs(UnmanagedType.LPTStr)] public string date_fmt;
    }

    /// <summary>
    /// Information about blobs(e.g.text or image).
    /// current_row contains this structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct TDSBLOB
    {
        [MarshalAs(UnmanagedType.LPTStr)] public string textvalue;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] public string textptr;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public string timestamp;
        public byte valid_ptr;
    }

    /// <summary>
    /// Store variant informations
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TDSVARIANT
    {
        /// <summary>
        /// this MUST have same position and place of textvalue in tds_blob
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)] public string data;
        public int size;
        public int data_len;
        public TDS_SERVER_TYPE type;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)] public string collation;
    }

    /// <summary>
    /// Information relevant to libiconv.The name is an iconv name, not the same as found in master..syslanguages.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TDS_ENCODING
    {
        /// <summary>
        /// name of the encoding (ie UTF-8)
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)] public string name;
        public byte min_bytes_per_char;
        public byte max_bytes_per_char;
        /// <summary>
        /// internal numeric index into array of all encodings
        /// </summary>
        public byte canonic;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BCPCOLDATA
    {
        public byte[] data;
        public int datalen;
        public int is_null;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate TDSRET tds_func_get_info(ref TDSSOCKET tds, ref TDSCOLUMN col);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate TDSRET tds_func_get_data(ref TDSSOCKET tds, ref TDSCOLUMN col);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int tds_func_row_len(ref TDSCOLUMN col);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate uint tds_func_put_info_len(ref TDSSOCKET tds, ref TDSCOLUMN col);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate TDSRET tds_func_put_info(ref TDSSOCKET tds, ref TDSCOLUMN col);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate TDSRET tds_func_put_data(ref TDSSOCKET tds, ref TDSCOLUMN col, int bcp7);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int tds_func_check(ref TDSCOLUMN col);

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSCOLUMNFUNCS //:sky
    {
        public tds_func_get_info get_info;
        public tds_func_get_data get_data;
        public tds_func_row_len row_len;
        /// <summary>
        /// Returns metadata column information size.
        /// </summary>
        public tds_func_put_info_len put_info_len;
        /// <summary>
        /// Send metadata column information to server
        /// </summary>
        public tds_func_put_info put_info;
        /// <summary>
        /// Send column data to server.
        /// Usually send parameters unless bcp7 is specified, in this case send BCP for TDS7+ (Sybase use a completely different format for BCP)
        /// </summary>
        public tds_func_put_data put_data;
#if ENABLE_EXTRA_CHECKS
        /// <summary>
        /// Check column is valid.
        /// Some things should be checked:
        /// - column_type and on_server.column_type;
        /// - column_size and on_server.column_size;
        /// - column_cur_size;
        /// - column_prec and column_scale;
        /// - is_XXXX_type macros/functions (nullable/fixed/blob/variable);
        /// - tds_get_size_by_type;
        /// - tds_get_conversion_type.
        /// </summary>
        public tds_func_check check;
#endif
    }

    /// <summary>
    /// Metadata about columns in regular and compute rows 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TDSCOLUMN
    {
        public IntPtr funcs; //:TDSCOLUMNFUNCS
        public int column_usertype;
        public int column_flags;

        /// <summary>
        /// maximun size of data. For fixed is the size.
        /// </summary>
        public int column_size;

        /// <summary>
        /// This type can be different from wire type because conversion (e.g. UCS-2->Ascii) can be applied.
        /// I'm beginning to wonder about the wisdom of this, however.
        /// April 2003 jkl
        /// </summary>
        public TDS_SERVER_TYPE column_type;
        /// <summary>
        /// size of length when reading from wire (0, 1, 2 or 4)
        /// </summary>
        public byte column_varint_size;

        /// <summary>
        /// precision for decimal/numeric
        /// </summary>
        public byte column_prec;
        /// <summary>
        /// scale for decimal/numeric
        /// </summary>
        public byte column_scale;

        [StructLayout(LayoutKind.Sequential)]
        public struct on_server_t
        {
            /// <summary>
            /// type of data, saved from wire
            /// </summary>
            public TDS_SERVER_TYPE column_type;
            public int column_size;
        }
        public on_server_t on_server;

        /// <summary>
        /// refers to previously allocated iconv information
        /// </summary>
        public IntPtr char_conv; //:TDSICONV

        [MarshalAs(UnmanagedType.TBStr)] public string table_name;
        [MarshalAs(UnmanagedType.TBStr)] public string column_name;
        [MarshalAs(UnmanagedType.TBStr)] public string table_column_name;

        public byte[] column_data;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int column_data_free_(TDSCOLUMN column);
        public column_data_free_ column_data_free;
        byte _data_;
        public byte column_nullable => (byte)((_data_ >> 0) & 1);
        public byte column_writeable => (byte)((_data_ >> 1) & 1);
        public byte column_identity => (byte)((_data_ >> 2) & 1);
        public byte column_key => (byte)((_data_ >> 3) & 1);
        public byte column_hidden => (byte)((_data_ >> 4) & 1);
        public byte column_output => (byte)((_data_ >> 5) & 1);
        public byte column_timestamp => (byte)((_data_ >> 6) & 1);
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)] public string column_collation;

        /* additional fields flags for compute results */
        public short column_operand;
        public byte column_operator;

        /// <summary>
        /// FIXME this is data related, not column
        /// size written in variable (ie: char, text, binary). -1 if NULL.
        /// </summary>
        public int column_cur_size;

        /// <summary>
        /// related to binding or info stored by client libraries
        /// FIXME find a best place to store these data, some are unused
        /// </summary>
        public short column_bindtype;
        public short column_bindfmt;
        public uint column_bindlen;
        [MarshalAs(UnmanagedType.LPArray)] public short[] column_nullbind; //:sky
        [MarshalAs(UnmanagedType.LPArray)] public byte[] column_varaddr; //:sky
        [MarshalAs(UnmanagedType.LPArray)] public int[] column_lenbind; //:sky
        public int column_textpos;
        public int column_text_sqlgetdatapos;
        public byte column_text_sqlputdatainfo;
        public byte column_iconv_left;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)] public string column_iconv_buf;

        public BCPCOLDATA bcp_column_data;
        /// <summary>
        /// The length, in bytes, of any length prefix this column may have.
        /// For example, strings in some non-C programming languages are made up of a one-byte length prefix, followed by the string data itself.
        /// If the data do not have a length prefix, set prefixlen to 0.
        /// Currently not very used in code, however do not remove.
        /// </summary>
        public int bcp_prefix_len;
        public int bcp_term_len;
        [MarshalAs(UnmanagedType.LPTStr)] public string bcp_terminator;
    }

    /// <summary>
    /// Hold information for any results
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TDSRESULTINFO
    {
        /* TODO those fields can became a struct */
        public TDSCOLUMN[] columns;
        public ushort num_cols;
        public ushort computeid;
        public int ref_count;
        public IntPtr attached_to; //: TDSSOCKET
        public byte[] current_row;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void row_free_(TDSRESULTINFO result, byte[] row);
        public row_free_ row_free;
        public int row_size;

        public short[] bycolumns;
        public ushort by_cols;
        public bool rows_exist;
        /* TODO remove ?? used only in dblib */
        public bool more_results;
    }

    /// <summary>
    /// values for tds->state
    /// </summary>
    public enum TDS_STATE
    {
        /// <summary>
        /// no data expected
        /// </summary>
        TDS_IDLE,
        /// <summary>
        /// client is writing data
        /// </summary>
        TDS_WRITING,
        /// <summary>
        /// client would send data
        /// </summary>
        TDS_SENDING,
        /// <summary>
        /// cilent is waiting for data
        /// </summary>
        TDS_PENDING,
        /// <summary>
        /// client is reading data
        /// </summary>
        TDS_READING,
        /// <summary>
        /// no connection
        /// </summary>
        TDS_DEAD
    }

    public enum TDS_OPERATION
    {
        TDS_OP_NONE = 0,

        /* mssql operations */
        TDS_OP_CURSOR = P.TDS_SP_CURSOR,
        TDS_OP_CURSOROPEN = P.TDS_SP_CURSOROPEN,
        TDS_OP_CURSORPREPARE = P.TDS_SP_CURSORPREPARE,
        TDS_OP_CURSOREXECUTE = P.TDS_SP_CURSOREXECUTE,
        TDS_OP_CURSORPREPEXEC = P.TDS_SP_CURSORPREPEXEC,
        TDS_OP_CURSORUNPREPARE = P.TDS_SP_CURSORUNPREPARE,
        TDS_OP_CURSORFETCH = P.TDS_SP_CURSORFETCH,
        TDS_OP_CURSOROPTION = P.TDS_SP_CURSOROPTION,
        TDS_OP_CURSORCLOSE = P.TDS_SP_CURSORCLOSE,
        TDS_OP_EXECUTESQL = P.TDS_SP_EXECUTESQL,
        TDS_OP_PREPARE = P.TDS_SP_PREPARE,
        TDS_OP_EXECUTE = P.TDS_SP_EXECUTE,
        TDS_OP_PREPEXEC = P.TDS_SP_PREPEXEC,
        TDS_OP_PREPEXECRPC = P.TDS_SP_PREPEXECRPC,
        TDS_OP_UNPREPARE = P.TDS_SP_UNPREPARE,

        /* sybase operations */
        TDS_OP_DYN_DEALLOC = 100,
    }

    partial class G
    {
        public static void TDS_DBG_LOGIN() => throw new NotImplementedException();    // __FILE__, ((__LINE__ << 4) | 11)
        public static void TDS_DBG_HEADER() => throw new NotImplementedException();   // __FILE__, ((__LINE__ << 4) | 10)
        public static void TDS_DBG_FUNC() => throw new NotImplementedException();     // __FILE__, ((__LINE__ << 4) |  7)
        public static void TDS_DBG_INFO2() => throw new NotImplementedException();    // __FILE__, ((__LINE__ << 4) |  6)
        public static void TDS_DBG_INFO1() => throw new NotImplementedException();    // __FILE__, ((__LINE__ << 4) |  5)
        public static void TDS_DBG_NETWORK() => throw new NotImplementedException();  // __FILE__, ((__LINE__ << 4) |  4)
        public static void TDS_DBG_WARN() => throw new NotImplementedException();     // __FILE__, ((__LINE__ << 4) |  3)
        public static void TDS_DBG_ERROR() => throw new NotImplementedException();    // __FILE__, ((__LINE__ << 4) |  2)
        public static void TDS_DBG_SEVERE() => throw new NotImplementedException();   // __FILE__, ((__LINE__ << 4) |  1)

        public const int TDS_DBGFLAG_FUNC = 0x80;
        public const int TDS_DBGFLAG_INFO2 = 0x40;
        public const int TDS_DBGFLAG_INFO1 = 0x20;
        public const int TDS_DBGFLAG_NETWORK = 0x10;
        public const int TDS_DBGFLAG_WARN = 0x08;
        public const int TDS_DBGFLAG_ERROR = 0x04;
        public const int TDS_DBGFLAG_SEVERE = 0x02;
        public const int TDS_DBGFLAG_ALL = 0xfff;
        public const int TDS_DBGFLAG_LOGIN = 0x0800;
        public const int TDS_DBGFLAG_HEADER = 0x0400;
        public const int TDS_DBGFLAG_PID = 0x1000;
        public const int TDS_DBGFLAG_TIME = 0x2000;
        public const int TDS_DBGFLAG_SOURCE = 0x4000;
        public const int TDS_DBGFLAG_THREAD = 0x8000;
    }

    //typedef TDSRESULTINFO TDSCOMPUTEINFO;
    //typedef TDSRESULTINFO TDSPARAMINFO;

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSMESSAGE
    {
        [MarshalAs(UnmanagedType.LPTStr)] public string server;
        [MarshalAs(UnmanagedType.LPTStr)] public string message;
        [MarshalAs(UnmanagedType.LPTStr)] public string proc_name;
        [MarshalAs(UnmanagedType.LPTStr)] public string sql_state;
        public int msgno;
        public int line_number;
        /// <summary>
        /// -1 .. 255
        /// </summary>
        public short state;
        public byte priv_msg_type;
        public byte severity;
        /// <summary>
        /// for library-generated errors
        /// </summary>
        public int oserr;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSUPDCOL
    {
        public IntPtr next; //:TDSUPDCOL
        public int colnamelength;
        [MarshalAs(UnmanagedType.LPTStr)] public string columnname;
    }

    public enum TDS_CURSOR_STATE
    {
        /// <summary>
        /// initial value
        /// </summary>
        TDS_CURSOR_STATE_UNACTIONED = 0,
        /// <summary>
        /// called by ct_cursor
        /// </summary>
        TDS_CURSOR_STATE_REQUESTED = 1,
        /// <summary>
        /// sent to server
        /// </summary>
        TDS_CURSOR_STATE_SENT = 2,
        /// <summary>
        /// acknowledged by server
        /// </summary>
        TDS_CURSOR_STATE_ACTIONED = 3,
    }

    public struct TDS_CURSOR_STATUS
    {
        TDS_CURSOR_STATE declare;
        TDS_CURSOR_STATE cursor_row;
        TDS_CURSOR_STATE open;
        TDS_CURSOR_STATE fetch;
        TDS_CURSOR_STATE close;
        TDS_CURSOR_STATE dealloc;
    }

    public enum TDS_CURSOR_OPERATION
    {
        TDS_CURSOR_POSITION = 0,
        TDS_CURSOR_UPDATE = 1,
        TDS_CURSOR_DELETE = 2,
        TDS_CURSOR_INSERT = 4
    }

    public enum TDS_CURSOR_FETCH
    {
        TDS_CURSOR_FETCH_NEXT = 1,
        TDS_CURSOR_FETCH_PREV,
        TDS_CURSOR_FETCH_FIRST,
        TDS_CURSOR_FETCH_LAST,
        TDS_CURSOR_FETCH_ABSOLUTE,
        TDS_CURSOR_FETCH_RELATIVE
    }

    /// <summary>
    /// Holds informations about a cursor
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TDSCURSOR
    {
        /// <summary>
        /// next in linked list, keep first
        /// </summary>
        public IntPtr next; //:TDSCURSOR
        /// <summary>
        /// reference counter so client can retain safely a pointer
        /// </summary>
        public int ref_count;
        /// <summary>
        /// name of the cursor
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)] public string cursor_name;
        /// <summary>
        /// cursor id returned by the server after cursor declare
        /// </summary>
        public int cursor_id;
        /// <summary>
        /// read only|updatable TODO use it
        /// </summary>
        public byte options;
        /// <summary>
        /// true if cursor was marker to be closed when connection is idle
        /// </summary>
        public bool defer_close;
        /// <summary>
        /// SQL query
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)] public string query;
        /* TODO for updatable columns */
        ///// <summary>
        ///// number of updatable columns
        ///// </summary>
        //public byte number_upd_cols;
        ///// <summary>
        ///// updatable column list
        ///// </summary>
        //public TDSUPDCOL[] cur_col_list;
        /// <summary>
        /// number of cursor rows to fetch
        /// </summary>
        public int cursor_rows;
        ///// <summary>
        ///// cursor parameter
        ///// </summary>
        //public TDSPARAMINFO @params;
        public TDS_CURSOR_STATUS status;
        public ushort srv_status;
        /// <summary>
        /// row fetched from this cursor
        /// </summary>
        public TDSRESULTINFO res_info;
        public int type, concurrency;
    }

    /// <summary>
    /// Current environment as reported by the server
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TDSENV
    {
        /// <summary>
        /// packet size (512-65535)
        /// </summary>
        public int block_size;
        [MarshalAs(UnmanagedType.LPTStr)] public string language;
        /// <summary>
        /// character set encoding
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)] public string charset;
        /// <summary>
        /// database name
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)] public string database;
    }

    /// <summary>
    /// Holds information for a dynamic (also called prepared) query.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TDSDYNAMIC
    {
        /// <summary>
        /// next in linked list, keep first
        /// </summary>
        public IntPtr next; //:TDSDYNAMIC
        /// <summary>
        /// reference counter so client can retain safely a pointer
        /// </summary>
        public int ref_count;
        /// <summary>
        /// numeric id for mssql7+
        /// </summary>
        public int num_id;
        /// <summary>
        /// id of dynamic.
        /// Usually this id correspond to server one but if not specified is generated automatically by libTDS
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)] public string id;
        /// <summary>
        /// this dynamic query cannot be prepared so libTDS have to construct a simple query.
        /// This can happen for instance is tds protocol doesn't support dynamics or trying to prepare query under Sybase that have BLOBs as parameters.
        /// </summary>
        public byte emulated;
        /// <summary>
        /// true if dynamic was marker to be closed when connection is idle
        /// </summary>
        public bool defer_close;
        //public int dyn_state; /* TODO use it */
        /// <summary>
        /// query results
        /// </summary>
        public IntPtr res_info; //:TDSPARAMINFO
        /// <summary>
        /// query parameters.
        /// Mostly used executing query however is a good idea to prepare query again if parameter type change in an incompatible way (ie different
        /// types or larger size). Is also better to prepare a query knowing parameter types earlier.
        /// </summary>
        public IntPtr @params; //:TDSPARAMINFO
        /// <summary>
        /// saved query, we need to know original query if prepare is impossible
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)] public string query;
    }

    public enum TDS_MULTIPLE_TYPE
    {
        TDS_MULTIPLE_QUERY,
        TDS_MULTIPLE_EXECUTE,
        TDS_MULTIPLE_RPC
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSMULTIPLE
    {
        public TDS_MULTIPLE_TYPE type;
        public uint flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSCONTEXT
    {
        public TDSLOCALE locale;
        public IntPtr parent; //:TDSCONTEXT
        /* handlers */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int msg_handler_t(ref TDSCONTEXT a, ref TDSSOCKET b, ref TDSMESSAGE c);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int err_handler_t(ref TDSCONTEXT a, ref TDSSOCKET b, ref TDSMESSAGE c);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int int_handler_t(IntPtr a);
        public msg_handler_t msg_handler;
        public err_handler_t err_handler;
        public int_handler_t int_handler;
        public bool money_use_2_digits;
    }

    public enum TDS_ICONV_ENTRY
    {
        client2ucs2,
        client2server_chardata,
        /// <summary>
        /// keep last
        /// </summary>
        initial_char_conv_count,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSAUTHENTICATION
    {
        public byte[] packet;
        public int packet_len;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate TDSRET free_t(ref TDSCONNECTION conn, ref TDSAUTHENTICATION auth);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate TDSRET handle_nextr_t(ref TDSSOCKET tds, ref TDSAUTHENTICATION auth, Size_t len);
        public free_t free;
        public handle_nextr_t handle_next;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSPACKET
    {
        public IntPtr next; //:TDSPACKET
        public short sid;
        public uint len, capacity;
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ArrayMarshaler<byte>))]
        public byte[] buf;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSPOLLWAKEUP
    {
        public IntPtr s_signal, s_signaled; //:TDS_SYS_SOCKET
    }

    /// <summary>
    /// field related to connection
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TDSCONNECTION
    {
        public ushort tds_version;
        /// <summary>
        /// version of product (Sybase/MS and full version)
        /// </summary>
        public uint product_version;
        [MarshalAs(UnmanagedType.LPTStr)] public string product_name;

        /// <summary>
        /// tcp socket, INVALID_SOCKET if not connected
        /// </summary>
        public IntPtr s; //:TDS_SYS_SOCKET
        public TDSPOLLWAKEUP wakeup;
        public IntPtr tds_ctx; //:TDSCONTEXT

        /// <summary>
        /// environment is shared between all sessions
        /// </summary>
        public TDSENV env;

        /// <summary>
        /// linked list of cursors allocated for this connection contains only cursors allocated on the server
        /// </summary>
        public IntPtr cursors; //:TDSCURSOR
        /// <summary>
        /// list of dynamic allocated for this connection contains only dynamic allocated on the server
        /// </summary>
        public IntPtr dyns; //:TDSDYNAMIC

        public int char_conv_count;
        public IntPtr[] char_convs; //:TDSICONV

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)] public string collation;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public string tds72_transaction;

        public TDS_CAPABILITIES capabilities;
        byte _data_;
        public uint emul_little_endian => (uint)((_data_ >> 0) & 1);
        public uint use_iconv => (uint)((_data_ >> 1) & 1);
        public uint tds71rev1 => (uint)((_data_ >> 2) & 1);
        /// <summary>
        /// true is connection has pending closing (cursors or dynamic)
        /// </summary>
        public uint pending_close => (uint)((_data_ >> 3) & 1);
        public uint encrypt_single_packet => (uint)((_data_ >> 4) & 1);
#if ENABLE_ODBC_MARS
        public uint mars => (uint)((_data_ >> 5) & 1);

        public IntPtr in_net_tds; //:TDSSOCKET
        public IntPtr packets; //:TDSPACKET
        public IntPtr recv_packet; //:TDSPACKET
        public IntPtr send_packets; //:TDSPACKET
        public uint send_pos, recv_pos;

        public tds_mutex list_mtx;
        //#define BUSY_SOCKET ((TDSSOCKET*)(TDS_UINTPTR)1)
        //#define TDSSOCKET_VALID(tds) (((TDS_UINTPTR)(tds)) > 1)
        public tds_socket[] sessions;
        public uint num_sessions;
        public uint num_cached_packets;
        public IntPtr packet_cache; //:TDSPACKET
#endif

        public int spid;
        public int client_spid;

        public IntPtr tls_session;
#if HAVE_GNUTLS
	public IntPtr tls_credentials;
#elif HAVE_OPENSSL
	public IntPtr tls_ctx;
#else
        public IntPtr tls_dummy;
#endif
        IntPtr authentication; //:TDSAUTHENTICATION
        [MarshalAs(UnmanagedType.LPTStr)] public string server;
    }

    /// <summary>
    /// Information for a server connection
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TDSSOCKET
    {
#if ENABLE_ODBC_MARS
        public TDSCONNECTION conn;
#else
        public TDSCONNECTION conn;
#endif

        /// <summary>
        /// Input buffer.
        /// Points to receiving packet buffer.
        /// As input buffer contains just the raw packet actually this pointer is the address of recv_packet->buf.
        /// </summary>
        public byte[] in_buf;

        /// <summary>
        /// Output buffer.
        /// Points to sending packet buffer.
        /// Output buffer can contain additional data before the raw TDS packet so this buffer can point some bytes after send_packet->buf.
        /// </summary>
        public byte[] out_buf;

        /// <summary>
        /// Maximum size of packet pointed by out_buf.
        /// The buffer is actually a bit larger to make possible to do some optimizations (at least TDS_ADDITIONAL_SPACE bytes).
        /// </summary>
        public uint out_buf_max;
        /// <summary>
        /// current position in in_buf
        /// </summary>
        public uint in_pos;
        /// <summary>
        /// current position in out_buf
        /// </summary>
        public uint out_pos;
        /// <summary>
        /// input buffer length
        /// </summary>
        public uint in_len;
        /// <summary>
        /// input buffer type
        /// </summary>
        public byte in_flag;
        /// <summary>
        /// output buffer type
        /// </summary>
        public byte out_flag;

        public IntPtr parent;

#if ENABLE_ODBC_MARS
        public short sid;
        public tds_condition packet_cond;
        public uint recv_seq;
        public uint send_seq;
        public uint recv_wnd;
        public uint send_wnd;
#endif

        /// <summary>
        /// packet we received
        /// </summary>
        public IntPtr recv_packet; //:TDSPACKET

        /// <summary>
        /// packet we are preparing to send
        /// </summary>
        public IntPtr send_packet; //:TDSPACKET

        /// <summary>
        /// Current query information. 
        /// Contains information in process, both normal and compute results.
        /// This pointer shouldn't be freed; it's just an alias to another structure.
        /// </summary>
        public IntPtr current_results; //:TDSRESULTINFO
        public IntPtr res_info; //:TDSRESULTINFO
        public uint num_comp_info;
        public TDSCOMPUTEINFO[] comp_info;
        public IntPtr param_info; //:TDSPARAMINFO
        /// <summary>
        /// cursor in use
        /// </summary>
        public IntPtr cur_cursor; //:TDSCURSOR
        /// <summary>
        /// true is query sent was a bulk query so we need to switch state to QUERYING
        /// </summary>
        public bool bulk_query;
        /// <summary>
        /// true is ret_status is valid
        /// </summary>
        public bool has_status;
        /// <summary>
        /// true if we are getting rows
        /// </summary>
        public bool in_row;
        /// <summary>
        /// return status from store procedure
        /// </summary>
        public int ret_status;
        public TDS_STATE state;
        /// <summary>
        /// indicate we are waiting a cancel reply; discard tokens till acknowledge; 
        /// 1 mean we have to send cancel packet, 2 already sent.
        /// </summary>
        public volatile byte in_cancel;

        /// <summary>
        /// rows updated/deleted/inserted/selected, TDS_NO_COUNT if not valid
        /// </summary>
        public long rows_affected;
        public int query_timeout;

        /// <summary>
        /// dynamic structure in use
        /// </summary>
        public IntPtr cur_dyn; //:TDSDYNAMIC

        /// <summary>
        /// config for login stuff. After login this field is NULL
        /// </summary>
        public IntPtr login; //:TDSLOGIN
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate TDSRET env_chg_func_t(ref TDSSOCKET tds, int type, [MarshalAs(UnmanagedType.LPTStr)] string oldVal, [MarshalAs(UnmanagedType.LPTStr)] string newval);
        public env_chg_func_t env_chg_func;
        public TDS_OPERATION current_op;

        public int option_value;
        public IntPtr wire_mtx; //:tds_mutex
    }

    partial class G
    {
        public static IntPtr tds_get_ctx(this TDSSOCKET tds) => tds.conn.tds_ctx; //:TDSCONTEXT
        public static void tds_set_ctx(this TDSSOCKET tds, ref TDSCONTEXT val) => tds.conn.tds_ctx = IntPtr.Zero; //:val;
        public static IntPtr tds_get_parent(this TDSSOCKET tds) => IntPtr.Zero; // (TDSSOCKET)tds.parent;
        public static void tds_set_parent(this TDSSOCKET tds, ref TDSSOCKET val) => tds.parent = IntPtr.Zero; //val;
        public static IntPtr tds_get_s(this TDSSOCKET tds) => tds.conn.s;
        public static void tds_set_s(this TDSSOCKET tds, ref TDSCONNECTION val) => tds.conn.s = IntPtr.Zero; //val;
    }

    public static class NativeMethods
    {
        internal const string LibraryName = "tds.dll";

        #region config.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr gumbo_tag_from_original_text();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void TDSCONFPARSE([MarshalAs(UnmanagedType.LPTStr)] string option, [MarshalAs(UnmanagedType.LPTStr)] string value, IntPtr param);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern bool tds_read_conf_section(IntPtr @in, [MarshalAs(UnmanagedType.LPTStr)] string section, TDSCONFPARSE tds_conf_parse, IntPtr parse_param);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern bool tds_read_conf_file(ref TDSLOGIN login, [MarshalAs(UnmanagedType.LPTStr)] string server);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_parse_conf_section([MarshalAs(UnmanagedType.LPTStr)] string option, [MarshalAs(UnmanagedType.LPTStr)] string value, IntPtr param);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSLOGIN tds_read_config_info(ref TDSSOCKET tds, ref TDSLOGIN login, ref TDSLOCALE locale);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_fix_login(ref TDSLOGIN login);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern ushort[] tds_config_verstr([MarshalAs(UnmanagedType.LPTStr)] string tdsver, ref TDSLOGIN login);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_lookup_host([MarshalAs(UnmanagedType.LPTStr)] string servername);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_lookup_host_set([MarshalAs(UnmanagedType.LPTStr)] string servername, IntPtr addr);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.LPTStr)] public static extern string tds_addrinfo2str(IntPtr addr, [MarshalAs(UnmanagedType.LPTStr)] string name, int namemax);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.LPTStr)] public static extern string tds_get_home_file([MarshalAs(UnmanagedType.LPTStr)] string file);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_set_interfaces_file_loc([MarshalAs(UnmanagedType.LPTStr)] string interfloc);
        //[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [MarshalAs(UnmanagedType.LPTStr)] public static extern string STD_DATETIME_FMT;
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_parse_boolean([MarshalAs(UnmanagedType.LPTStr)] string value, int default_value);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_config_boolean([MarshalAs(UnmanagedType.LPTStr)] string option, [MarshalAs(UnmanagedType.LPTStr)] string value, ref TDSLOGIN login);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSLOCALE tds_get_locale();
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_alloc_row(ref TDSRESULTINFO res_info);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_alloc_compute_row(ref TDSCOMPUTEINFO res_info);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_alloc_bcp_column_data(uint column_size);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_lookup_dynamic(ref TDSCONNECTION conn, [MarshalAs(UnmanagedType.LPTStr)] string id);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.LPTStr)] public static extern string tds_prtype(int token);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_get_varint_size(ref TDSCONNECTION conn, int datatype);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDS_SERVER_TYPE tds_get_cardinal_type(ref TDS_SERVER_TYPE datatype, int usertype);
        #endregion

        #region iconv.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_iconv_open(ref TDSCONNECTION conn, [MarshalAs(UnmanagedType.LPTStr)] string charset, int use_utf16);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_iconv_close(ref TDSCONNECTION conn);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_srv_charset_changed(ref TDSCONNECTION conn, [MarshalAs(UnmanagedType.LPTStr)] string charset);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds7_srv_charset_changed(ref TDSCONNECTION conn, int sql_collate, int lcid);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_iconv_alloc(ref TDSCONNECTION conn);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_iconv_free(ref TDSCONNECTION conn);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_iconv_from_collate(ref TDSCONNECTION conn, [MarshalAs(UnmanagedType.LPTStr)] string collate); //:TDSICONV
        #endregion

        #region mem.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_free_socket(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_free_all_results(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_free_results(ref TDSRESULTINFO res_info);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_free_param_results(ref TDSPARAMINFO param_info);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_free_param_result(ref TDSPARAMINFO param_info);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_free_msg(ref TDSMESSAGE message);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_cursor_deallocated(ref TDSCONNECTION conn, ref TDSCURSOR cursor);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_release_cursor(ref TDSCURSOR[] pcursor);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_free_bcp_column_data(ref BCPCOLDATA coldata);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_alloc_results(ushort num_cols); //:TDSRESULTINFO
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr[] tds_alloc_compute_results(ref TDSSOCKET tds, ushort num_cols, ushort by_cols); //:TDSCOMPUTEINFO
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_alloc_context(ref IntPtr parent); //:TDSCONTEXT
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_free_context(ref TDSCONTEXT locale);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_alloc_param_result(ref TDSPARAMINFO old_param); //:TDSPARAMINFO
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_free_input_params(ref TDSDYNAMIC dyn);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_release_dynamic(ref IntPtr dyn); //:TDSDYNAMIC
        public static void tds_release_cur_dyn(ref TDSSOCKET tds) => tds_release_dynamic(ref tds.cur_dyn);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_dynamic_deallocated(ref TDSCONNECTION conn, ref TDSDYNAMIC dyn);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_set_cur_dyn(ref TDSSOCKET tds, ref TDSDYNAMIC dyn);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_realloc_socket(ref TDSSOCKET tds, Size_t bufsize); //:TDSSOCKET
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.LPTStr)] public static extern string tds_alloc_client_sqlstate(int msgno);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.LPTStr)] public static extern string tds_alloc_lookup_sqlstate(ref TDSSOCKET tds, int msgno);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_alloc_login(int use_environment); //:TDSLOGIN
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_alloc_dynamic(ref TDSCONNECTION conn, [MarshalAs(UnmanagedType.LPTStr)] string id); //:TDSDYNAMIC
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_free_login(ref TDSLOGIN login);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_init_login(ref TDSLOGIN login, ref TDSLOCALE locale); //:TDSLOGIN
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_alloc_locale(); //:TDSLOCALE
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_alloc_param_data(ref TDSCOLUMN curparam);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_free_locale(ref TDSLOCALE locale);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_alloc_cursor(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.LPTStr)] string name, int namelen, [MarshalAs(UnmanagedType.LPTStr)] string query, int querylen); //:TDSCURSOR
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_free_row(ref TDSRESULTINFO res_info, IntPtr row);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_alloc_socket(ref TDSCONTEXT context, uint bufsize); //:TDSSOCKET
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_alloc_additional_socket(ref TDSCONNECTION conn); //:TDSSOCKET
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_set_current_results(ref TDSSOCKET tds, ref TDSRESULTINFO info);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_detach_results(ref TDSRESULTINFO info);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_realloc(ref IntPtr pp, Size_t new_size);
        //#define TDS_RESIZE(p, n_elem) tds_realloc((void**) &(p), sizeof(*(p)) * (SizeT) (n_elem))
        //#define tds_new(type, n) ((type *) malloc(sizeof(type) * (n)))
        //#define tds_new0(type, n) ((type *) calloc(n, sizeof(type)))

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_alloc_packet(IntPtr buf, uint len); //:TDSPACKET
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_realloc_packet(ref TDSPACKET packet, uint len); //:TDSPACKET
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_free_packets(ref TDSPACKET packet);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_alloc_bcpinfo(); //:TDSBCPINFO
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_free_bcpinfo(ref TDSBCPINFO bcpinfo);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_deinit_bcpinfo(ref TDSBCPINFO bcpinfo);
        #endregion

        #region login.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_set_packet(ref TDSLOGIN tds_login, int packet_size);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_set_port(ref TDSLOGIN tds_login, int port);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern bool tds_set_passwd(ref TDSLOGIN tds_login, [MarshalAs(UnmanagedType.LPTStr)] string password);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_set_bulk(ref TDSLOGIN tds_login, bool enabled);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern bool tds_set_user(ref TDSLOGIN tds_login, [MarshalAs(UnmanagedType.LPTStr)] string username);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern bool tds_set_app(ref TDSLOGIN tds_login, [MarshalAs(UnmanagedType.LPTStr)] string application);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern bool tds_set_host(ref TDSLOGIN tds_login, [MarshalAs(UnmanagedType.LPTStr)] string hostname);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern bool tds_set_library(ref TDSLOGIN tds_login, [MarshalAs(UnmanagedType.LPTStr)] string library);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern bool tds_set_server(ref TDSLOGIN tds_login, [MarshalAs(UnmanagedType.LPTStr)] string server);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern bool tds_set_client_charset(ref TDSLOGIN tds_login, [MarshalAs(UnmanagedType.LPTStr)] string charset);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern bool tds_set_language(ref TDSLOGIN tds_login, [MarshalAs(UnmanagedType.LPTStr)] string language);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_set_version(ref TDSLOGIN tds_login, byte major_ver, byte minor_ver);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_connect_and_login(ref TDSSOCKET tds, ref TDSLOGIN login);
        #endregion

        #region query.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_start_query(ref TDSSOCKET tds, byte packet_type);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_submit_query(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.LPTStr)] string query);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_submit_query_params(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.LPTStr)] string query, ref TDSPARAMINFO @params, ref TDSHEADERS head);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_submit_queryf(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.LPTStr)] string queryf, params object[] args);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_submit_prepare(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.LPTStr)] string query, [MarshalAs(UnmanagedType.LPTStr)] string id, out TDSDYNAMIC dyn_out, ref TDSPARAMINFO @params);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_submit_execdirect(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.LPTStr)] string query, ref TDSPARAMINFO @params, ref TDSHEADERS head);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds71_submit_prepexec(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.LPTStr)] string query, [MarshalAs(UnmanagedType.LPTStr)] string id, out TDSDYNAMIC dyn_out, ref TDSPARAMINFO @params);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_submit_execute(ref TDSSOCKET tds, ref TDSDYNAMIC dyn);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_send_cancel(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.LPTStr)] public static extern string tds_next_placeholder([MarshalAs(UnmanagedType.LPTStr)] string start);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_count_placeholders([MarshalAs(UnmanagedType.LPTStr)] string query);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_needs_unprepare(ref TDSCONNECTION conn, ref TDSDYNAMIC dyn);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_deferred_unprepare(ref TDSCONNECTION conn, ref TDSDYNAMIC dyn);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_submit_unprepare(ref TDSSOCKET tds, ref TDSDYNAMIC dyn);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_submit_rpc(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.LPTStr)] string rpc_name, ref TDSPARAMINFO @params, ref TDSHEADERS head);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_submit_optioncmd(ref TDSSOCKET tds, TDS_OPTION_CMD command, TDS_OPTION option, ref TDS_OPTION_ARG param, int param_size);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_submit_begin_tran(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_submit_rollback(ref TDSSOCKET tds, int cont);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_submit_commit(ref TDSSOCKET tds, int cont);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_disconnect(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern Size_t tds_quote_id(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.LPTStr)] string buffer, [MarshalAs(UnmanagedType.LPTStr)] string id, int idlen);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern Size_t tds_quote_string(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.LPTStr)] string buffer, [MarshalAs(UnmanagedType.LPTStr)] string str, int len);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.LPTStr)] public static extern string tds_skip_comment([MarshalAs(UnmanagedType.LPTStr)] string s);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.LPTStr)] public static extern string tds_skip_quoted([MarshalAs(UnmanagedType.LPTStr)] string s);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern Size_t tds_fix_column_size(ref TDSSOCKET tds, ref TDSCOLUMN curcol);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.LPTStr)] public static extern string tds_convert_string(ref TDSSOCKET tds, ref TDSICONV char_conv, [MarshalAs(UnmanagedType.LPTStr)] string s, int len, out Size_t out_len);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_convert_string_free([MarshalAs(UnmanagedType.LPTStr)] string original, [MarshalAs(UnmanagedType.LPTStr)] string converted);
#if !ENABLE_EXTRA_CHECKS
        //[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_convert_string_free(object original, object converted) { if (original != converted) free((char*)converted); }
#endif
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_get_column_declaration(ref TDSSOCKET tds, ref TDSCOLUMN curcol, out byte @out);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_cursor_declare(ref TDSSOCKET tds, ref TDSCURSOR cursor, ref TDSPARAMINFO @params, out int send);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_cursor_setrows(ref TDSSOCKET tds, ref TDSCURSOR cursor, out int send);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_cursor_open(ref TDSSOCKET tds, ref TDSCURSOR cursor, ref TDSPARAMINFO @params, out int send);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_cursor_fetch(ref TDSSOCKET tds, ref TDSCURSOR cursor, TDS_CURSOR_FETCH fetch_type, int i_row);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_cursor_get_cursor_info(ref TDSSOCKET tds, ref TDSCURSOR cursor, out uint row_number, out uint row_count);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_cursor_close(ref TDSSOCKET tds, ref TDSCURSOR cursor);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_cursor_dealloc(ref TDSSOCKET tds, ref TDSCURSOR cursor);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_deferred_cursor_dealloc(ref TDSCONNECTION conn, ref TDSCURSOR cursor);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_cursor_update(ref TDSSOCKET tds, ref TDSCURSOR cursor, TDS_CURSOR_OPERATION op, int i_row, ref TDSPARAMINFO @params);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_cursor_setname(ref TDSSOCKET tds, ref TDSCURSOR cursor);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_multiple_init(ref TDSSOCKET tds, ref TDSMULTIPLE multiple, TDS_MULTIPLE_TYPE type, ref TDSHEADERS head);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_multiple_done(ref TDSSOCKET tds, ref TDSMULTIPLE multiple);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_multiple_query(ref TDSSOCKET tds, ref TDSMULTIPLE multiple, [MarshalAs(UnmanagedType.LPTStr)] string query, ref TDSPARAMINFO @params);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_multiple_execute(ref TDSSOCKET tds, ref TDSMULTIPLE multiple, ref TDSDYNAMIC dyn);

        #endregion

        #region token.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_process_cancel(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_get_token_size(int marker);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_process_login_tokens(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_process_simple_query(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds5_send_optioncmd(ref TDSSOCKET tds, TDS_OPTION_CMD tds_command, TDS_OPTION tds_option, ref TDS_OPTION_ARG tds_argument, ref int tds_argsize);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_process_tokens(ref TDSSOCKET tds, out int result_type, out int done_flags, uint flag);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int determine_adjusted_size(ref TDSICONV char_conv, int size);
        #endregion

        #region data.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_set_param_type(ref TDSCONNECTION conn, ref TDSCOLUMN curcol, TDS_SERVER_TYPE type);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_set_column_type(ref TDSCONNECTION conn, ref TDSCOLUMN curcol, TDS_SERVER_TYPE type);
        #endregion

        #region tds_convert.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_datecrack(int datetype, IntPtr di, ref TDSDATEREC dr);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDS_SERVER_TYPE tds_get_conversion_type(TDS_SERVER_TYPE srctype, int colsize);
        //extern const char[] tds_hex_digits;
        #endregion

        #region write.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_init_write_buf(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_put_n(ref TDSSOCKET tds, byte[] buf, IntPtr n);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_put_string(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.LPTStr)]string buf, int len);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_put_int(ref TDSSOCKET tds, int i);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_put_int8(ref TDSSOCKET tds, long i);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_put_smallint(ref TDSSOCKET tds, short si);
        /// <summary>
        /// Output a tinyint value
        /// </summary>
        /// <param name="tds"></param>
        /// <param name="ti"></param>
        /// <returns></returns>
        public static int tds_put_tinyint(ref TDSSOCKET tds, byte ti) => tds_put_byte(ref tds, ti);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_put_byte(ref TDSSOCKET tds, byte c);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_flush_packet(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_put_buf(ref TDSSOCKET tds, byte[] buf, int dsize, int ssize);
        #endregion

        #region read.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern byte tds_get_byte(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_unget_byte(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern byte tds_peek(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern ushort tds_get_usmallint(ref TDSSOCKET tds);
        public static ushort tds_get_smallint(ref TDSSOCKET tds) => (ushort)tds_get_usmallint(ref tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern uint tds_get_uint(ref TDSSOCKET tds);
        public static int tds_get_int(ref TDSSOCKET tds) => (int)tds_get_uint(ref tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern long tds_get_uint8(ref TDSSOCKET tds);
        public static long tds_get_int8(ref TDSSOCKET tds) => (long)tds_get_uint8(ref tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern Size_t tds_get_string(ref TDSSOCKET tds, Size_t string_len, [MarshalAs(UnmanagedType.LPTStr)] string dest, Size_t dest_size);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_get_char_data(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.LPTStr)] string dest, Size_t wire_size, ref TDSCOLUMN curcol);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern bool tds_get_n(ref TDSSOCKET tds, out IntPtr dest, Size_t n);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_get_size_by_type(TDS_SERVER_TYPE servertype);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.TBStr)] public static extern string tds_dstr_get(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.TBStr)] string s, Size_t len);
        #endregion

        #region util.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tdserror(ref TDSCONTEXT tds_ctx, ref TDSSOCKET tds, int msgno, int errnum);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDS_STATE tds_set_state(ref TDSSOCKET tds, TDS_STATE state);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_swap_bytes(byte[] buf, int bytes);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern uint tds_gettime_ms();
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.LPTStr)] public static extern string tds_strndup(byte[] s, IntPtr len);
        #endregion

        #region log.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tdsdump_off();
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tdsdump_on();
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tdsdump_isopen();
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tdsdump_open([MarshalAs(UnmanagedType.LPTStr)] string filename);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tdsdump_close();
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tdsdump_dump_buf([MarshalAs(UnmanagedType.LPTStr)] string file, uint level_line, [MarshalAs(UnmanagedType.LPTStr)] string msg, byte[] buf, Size_t length);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tdsdump_col(ref TDSCOLUMN col);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void tdsdump_log([MarshalAs(UnmanagedType.LPTStr)] string file, uint level_line, [MarshalAs(UnmanagedType.LPTStr)] string fmt, params object[] args);

        //public static void TDSDUMP_LOG_FAST() { if (TDS_UNLIKELY(tds_write_dump)) tdsdump_log() }
        //public static void tdsdump_log() => TDSDUMP_LOG_FAST();
        //public static void TDSDUMP_BUF_FAST() { if (TDS_UNLIKELY(tds_write_dump)) tdsdump_dump_buf(); }
        //public static void tdsdump_dump_buf() => TDSDUMP_BUF_FAST();

        //extern int tds_write_dump;
        //extern int tds_debug_flags;
        //extern int tds_g_append_mode;
        #endregion

        #region net.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSERRNO tds_open_socket(ref TDSSOCKET tds, IntPtr ipaddr, uint port, int timeout, out int p_oserr); //:addrinfo
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_close_socket(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds7_get_instance_ports(IntPtr output, IntPtr addr); //:addrinfo
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds7_get_instance_port(IntPtr addr, [MarshalAs(UnmanagedType.LPTStr)] string instance); //:addrinfo
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.LPTStr)] public static extern string tds_prwsaerror(int erc);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_prwsaerror_free([MarshalAs(UnmanagedType.LPTStr)] string s);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_connection_read(ref TDSSOCKET tds, byte[] buf, int buflen);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_connection_write(ref TDSSOCKET tds, byte[] buf, int buflen, int final);
        //public static void TDSSELREAD() => POLLIN();
        //public static void TDSSELWRITE() => POLLOUT();
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_select(ref TDSSOCKET tds, uint tds_sel, int timeout_seconds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_connection_close(ref TDSCONNECTION conn);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_goodread(ref TDSSOCKET tds, byte[] buf, int buflen);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_goodwrite(ref TDSSOCKET tds, byte[] buffer, Size_t buflen);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_socket_flush(IntPtr sock); //:TDS_SYS_SOCKET
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_socket_set_nonblocking(IntPtr sock); //:TDS_SYS_SOCKET
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_wakeup_init(ref TDSPOLLWAKEUP wakeup);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_wakeup_close(ref TDSPOLLWAKEUP wakeup);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_wakeup_send(ref TDSPOLLWAKEUP wakeup, char cancel);
        public static IntPtr tds_wakeup_get_fd(ref TDSPOLLWAKEUP wakeup) => wakeup.s_signaled; //:TDS_SYS_SOCKET
        #endregion

        #region packet.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_read_packet(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_write_packet(ref TDSSOCKET tds, byte final);
#if ENABLE_ODBC_MARS
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_append_cancel(ref TDSSOCKET tds);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_append_fin(ref TDSSOCKET tds);
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_put_cancel(ref TDSSOCKET tds);
#endif
        #endregion

        #region vstrbuild.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_vstrbuild(byte[] buffer, int buflen, ref int resultlen, [MarshalAs(UnmanagedType.LPTStr)] string text, int textlen, [MarshalAs(UnmanagedType.LPTStr)] string formats, int formatlen, IntPtr ap);
        #endregion

        #region numeric.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] [return: MarshalAs(UnmanagedType.LPTStr)] public static extern string tds_money_to_string(ref TDS_MONEY money, [MarshalAs(UnmanagedType.LPTStr)] string s, bool use_2_digits);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_numeric_to_string(ref TDS_NUMERIC numeric, [MarshalAs(UnmanagedType.LPTStr)] string s);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern int tds_numeric_change_prec_scale(ref TDS_NUMERIC numeric, byte new_prec, byte new_scale);
        #endregion

        #region getmac.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_getmac(IntPtr s, byte[] mac); //:TDS_SYS_SOCKET
        #endregion

        #region challenge.c
#if !HAVE_SSPI
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_ntlm_get_auth(ref TDSSOCKET tds); //:TDSAUTHENTICATION
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_gss_get_auth(ref TDSSOCKET tds); //:TDSAUTHENTICATION
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds_sspi_get_auth(ref TDSSOCKET tds); //:TDSAUTHENTICATION
#endif
        #endregion

        #region random.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds_random_buffer(byte[] @out, int len);
        #endregion

        #region sec_negotiate.c
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr tds5_negotiate_get_auth(ref TDSSOCKET tds); //:TDSAUTHENTICATION
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tds5_negotiate_set_msg_type(ref TDSSOCKET tds, ref TDSAUTHENTICATION auth, uint msg_type);
        #endregion

        #region bulk.c
        /// <summary>
        /// bcp direction
        /// </summary>
        public enum tds_bcp_directions
        {
            TDS_BCP_IN = 1,
            TDS_BCP_OUT = 2,
            TDS_BCP_QUERYOUT = 3
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TDSBCPINFO
        {
            [MarshalAs(UnmanagedType.LPTStr)] string hint;
            IntPtr parent;
            [MarshalAs(UnmanagedType.TBStr)] string tablename;
            [MarshalAs(UnmanagedType.LPTStr)] string insert_stmt;
            int direction;
            int identity_insert_on;
            int xfer_init;
            int bind_count;
            IntPtr bindinfo; //:TDSRESULTINFO
        }

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_bcp_init(ref TDSSOCKET tds, ref TDSBCPINFO bcpinfo);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate TDSRET tds_bcp_get_col_data(ref TDSBCPINFO bulk, ref TDSCOLUMN bcpcol, int offset);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void tds_bcp_null_error(ref TDSBCPINFO bulk, int index, int offset);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_bcp_send_record(ref TDSSOCKET tds, ref TDSBCPINFO bcpinfo, tds_bcp_get_col_data get_col_data, tds_bcp_null_error null_error, int offset);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_bcp_done(ref TDSSOCKET tds, out int rows_copied);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_bcp_start(ref TDSSOCKET tds, ref TDSBCPINFO bcpinfo);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_bcp_start_copy_in(ref TDSSOCKET tds, ref TDSBCPINFO bcpinfo);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_bcp_fread(ref TDSSOCKET tds, ref TDSICONV conv, IntPtr stream, [MarshalAs(UnmanagedType.LPTStr)] string terminator, Size_t term_len, out byte[] outbuf, out Size_t outbytes);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_writetext_start(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.LPTStr)] string objname, [MarshalAs(UnmanagedType.LPTStr)] string textptr, [MarshalAs(UnmanagedType.LPTStr)] string timestamp, int with_log, uint size);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_writetext_continue(ref TDSSOCKET tds, [MarshalAs(UnmanagedType.LPTStr)] string text, uint size);
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern TDSRET tds_writetext_end(ref TDSSOCKET tds);
        #endregion

        //public unsafe static bool tds_capability_enabled(ref TDS_CAPABILITY_TYPE cap, uint cap_num) => cap.values[sizeof(cap.values) - 1 - (cap_num >> 3)] >> (cap_num & 7)) &1;
        //public static bool tds_capability_has_req(ref TDSCONNECTION conn, ref TDS_CAPABILITY_TYPE cap) => tds_capability_enabled(ref conn.capabilities.types[0], cap);

        public static bool IS_TDS42(ref TDSCONNECTION x) => x.tds_version == 0x402;
        public static bool IS_TDS46(ref TDSCONNECTION x) => x.tds_version == 0x406;
        public static bool IS_TDS50(ref TDSCONNECTION x) => x.tds_version == 0x500;
        public static bool IS_TDS70(ref TDSCONNECTION x) => x.tds_version == 0x700;
        public static bool IS_TDS71(ref TDSCONNECTION x) => x.tds_version == 0x701;
        public static bool IS_TDS72(ref TDSCONNECTION x) => x.tds_version == 0x702;
        public static bool IS_TDS73(ref TDSCONNECTION x) => x.tds_version == 0x703;

        public static bool IS_TDS50_PLUS(ref TDSCONNECTION x) => x.tds_version >= 0x500;
        public static bool IS_TDS7_PLUS(ref TDSCONNECTION x) => x.tds_version >= 0x700;
        public static bool IS_TDS71_PLUS(ref TDSCONNECTION x) => x.tds_version >= 0x701;
        public static bool IS_TDS72_PLUS(ref TDSCONNECTION x) => x.tds_version >= 0x702;
        public static bool IS_TDS73_PLUS(ref TDSCONNECTION x) => x.tds_version >= 0x703;
        public static bool IS_TDS74_PLUS(ref TDSCONNECTION x) => x.tds_version >= 0x704;

        public static int TDS_MAJOR(ref TDSCONNECTION x) => x.tds_version >> 8;
        public static int TDS_MINOR(ref TDSCONNECTION x) => x.tds_version & 0xff;

        public static bool IS_TDSDEAD(TDSSOCKET? x) => x == null || x.Value.state == TDS_STATE.TDS_DEAD;

        /// <summary>
        /// Check if product is Sybase (such as Adaptive Server Enterrprice). x should be a TDSSOCKET*.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool TDS_IS_SYBASE(ref TDSSOCKET x) => (x.conn.product_version & 0x80000000u) == 0;
        /// <summary>
        /// Check if product is Microsft SQL Server. x should be a TDSSOCKET*.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool TDS_IS_MSSQL(ref TDSSOCKET x) => (x.conn.product_version & 0x80000000u) != 0;

        /// <summary>
        /// Calc a version number for mssql. Use with TDS_MS_VER(7,0,842).
        /// For test for a range of version you can use check like
        /// if (tds.product_version >= TDS_MS_VER(7,0,0) && tds.product_version &lt; TDS_MS_VER(8,0,0))
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static ulong TDS_MS_VER(int maj, int min, int x) => (ulong)(0x80000000u | (maj << 24) | (min << 16) | x);

        /// <summary>
        /// TODO test if not similar to ms one
        /// Calc a version number for Sybase.
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static ulong TDS_SYB_VER(int maj, int min, int x) => (ulong)((maj << 24) | (min << 16) | x << 8);

        public static void TDS_PUT_INT(ref TDSSOCKET tds, int v) => tds_put_int(ref tds, v);
        public static void TDS_PUT_SMALLINT(ref TDSSOCKET tds, short v) => tds_put_smallint(ref tds, v);
        public static void TDS_PUT_BYTE(ref TDSSOCKET tds, byte v) => tds_put_byte(ref tds, v);
    }
}