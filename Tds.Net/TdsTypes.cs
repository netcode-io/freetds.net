using System;
using System.Runtime.InteropServices;
using TDSCOMPUTEINFO = FreeTds.TDSRESULTINFO;
using TDSPARAMINFO = FreeTds.TDSRESULTINFO;
using Size_t = System.IntPtr;
using TDSRET = System.Int32;

namespace FreeTds
{
    public static partial class G { }

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
        [MarshalAs(UnmanagedType.LPStr)] public string freetds_version;
        /// <summary>
        /// location of freetds.conf
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)] public string sysconfdir;
        /// <summary>
        /// latest software_version date among the modules
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)] public string last_update;
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
        public ushort time_prec => (ushort)((_data_ >> 0) & 0x7); //:3
        public ushort _tds_reserved => (ushort)((_data_ >> 3) & 1 << 0x3FF); //:10
        public ushort has_time => (ushort)((_data_ >> 13) & 1);
        public ushort has_date => (ushort)((_data_ >> 14) & 1);
        public ushort has_offset => (ushort)((_data_ >> 15) & 1);
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
        public static readonly int[] tds_numeric_bytes_per_prec = NativeMethods.MarshalToPtrArray<int>("tds_numeric_bytes_per_prec", 1);
    }

    partial class G
    {
        public const TDSRET TDS_NO_MORE_RESULTS = 1;
        public const TDSRET TDS_SUCCESS = 0;
        public const TDSRET TDS_FAIL = -1;
        public const TDSRET TDS_CANCELLED = -2;

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
        TDSEOK = G.TDS_SUCCESS,
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
        [MarshalAs(UnmanagedType.LPStr)] public string c;
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
        public static readonly _TYPEFLAG[] tds_type_flags_ms = NativeMethods.MarshalToPtrArray<_TYPEFLAG>("tds_type_flags_ms", 1);
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
        //struct tds_align_struct
        //{
        //    IntPtr p;
        //    int i;
        //    long ui;
        //}
        public static int TDS_ALIGN_SIZE = 10; // Marshal.SizeOf(typeof(tds_align_struct)); /*SKY:TODO*/
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct TDS_CAPABILITY_TYPE
    {
        public byte type;
        public byte len; /* always sizeof(values) */
        public fixed byte values[(int)G.TDS_MAX_CAPABILITY / 2 - 2];
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe class TDS_CAPABILITIES
    {
        public TDS_CAPABILITY_TYPE types0;
        public TDS_CAPABILITY_TYPE types1;
        //public fixed byte types[(int)G.TDS_MAX_CAPABILITY]; //: TDS_CAPABILITY_TYPE
    }

    partial class G
    {
        public const int TDS_MAX_LOGIN_STR_SZ = 128;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class TDSLOGIN
    {
        /// <summary>
        /// server name (in freetds.conf)
        /// </summary>
        public IntPtr server_name; //:DString
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
        public IntPtr language; //:DString
        /// <summary>
        /// charset of server e.g. iso_1
        /// </summary>
        public IntPtr server_charset; //:DString
        public int connect_timeout;
        public IntPtr client_host_name; //:DString
        public IntPtr server_host_name; //:DString
        /// <summary>
        /// server realm name (in freetds.conf)
        /// </summary>
        public IntPtr server_realm_name; //:DString
        /// <summary>
        /// server SPN (in freetds.conf)
        /// </summary>
        public IntPtr server_spn; //:DString
        /// <summary>
        /// database filename to attach (MSSQL)
        /// </summary>
        public IntPtr db_filename; //:DString
        /// <summary>
        /// certificate authorities file
        /// </summary>
        public IntPtr cafile; //:DString
        /// <summary>
        /// certificate revocation file
        /// </summary>
        public IntPtr crlfile; //:DString
        public IntPtr openssl_ciphers; //:DString
        public IntPtr app_name; //:DString
        /// <summary>
        /// account for login
        /// </summary>
        public IntPtr user_name; //:DString
        /// <summary>
        /// password of account login
        /// </summary>
        public IntPtr password; //:DString
        /// <summary>
        /// new password to set (TDS 7.2+)
        /// </summary>
        public IntPtr new_password; //:DString

        /// <summary>
        /// Ct-Library, DB-Library, TDS-Library or ODBC
        /// </summary>
        public IntPtr library; //:DString
        public byte encryption_level;

        public int query_timeout;
        public TDS_CAPABILITIES capabilities;
        public IntPtr client_charset; //:DString
        public IntPtr database; //:DString

        /// <summary>
        /// ip(s) of server
        /// </summary>
        public IntPtr ip_addrs; //:addrinfo
        public IntPtr instance_name; //:DString
        public IntPtr dump_file; //:DString
        public int debug_flags;
        public int text_size;
        public IntPtr routing_address; //:DString
        public ushort routing_port;

        public byte option_flag2;

        ushort _data_;
        /// <summary>
        /// if bulk copy should be enabled
        /// </summary>
        public bool bulk_copy => ((_data_ >> 0) & 1) != 0;
        public bool suppress_language => ((_data_ >> 1) & 1) != 0;
        public bool emul_little_endian => ((_data_ >> 2) & 1) != 0;
        public bool gssapi_use_delegation => ((_data_ >> 3) & 1) != 0;
        public bool use_ntlmv2 => ((_data_ >> 4) & 1) != 0;
        public bool use_ntlmv2_specified => ((_data_ >> 5) & 1) != 0;
        public bool use_lanman => ((_data_ >> 6) & 1) != 0;
        public bool mars => ((_data_ >> 7) & 1) != 0;
        public bool use_utf16 => ((_data_ >> 8) & 1) != 0;
        public bool use_new_password => ((_data_ >> 9) & 1) != 0;
        public bool valid_configuration => ((_data_ >> 10) & 1) != 0;
        public bool check_ssl_hostname => ((_data_ >> 11) & 1) != 0;
        public bool readonly_intent => ((_data_ >> 12) & 1) != 0;
        public bool enable_tls_v1 => ((_data_ >> 13) & 1) != 0;
        public bool server_is_valid => ((_data_ >> 14) & 1) != 0;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSHEADERS
    {
        [MarshalAs(UnmanagedType.LPStr)] public string qn_options;
        [MarshalAs(UnmanagedType.LPStr)] public string qn_msgtext;
        public int qn_timeout;
        /* TDS 7.4+: trace activity ID char[20] */
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSLOCALE
    {
        [MarshalAs(UnmanagedType.LPStr)] public string language;
        [MarshalAs(UnmanagedType.LPStr)] public string server_charset;
        [MarshalAs(UnmanagedType.LPStr)] public string date_fmt;
    }

    /// <summary>
    /// Information about blobs(e.g.text or image).
    /// current_row contains this structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct TDSBLOB
    {
        [MarshalAs(UnmanagedType.LPStr)] public string textvalue;
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
        [MarshalAs(UnmanagedType.LPStr)] public string data;
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
        [MarshalAs(UnmanagedType.LPStr)] public string name;
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

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate TDSRET tds_func_get_info(IntPtr tds, IntPtr col); //:TDSSOCKET:TDSCOLUMN
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate TDSRET tds_func_get_data(IntPtr tds, IntPtr col); //:TDSSOCKET:TDSCOLUMN
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int tds_func_row_len(IntPtr col); //:TDSCOLUMN
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate uint tds_func_put_info_len(IntPtr tds, IntPtr col); //:TDSSOCKET:TDSCOLUMN
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate TDSRET tds_func_put_info(IntPtr tds, IntPtr col); //:TDSSOCKET:TDSCOLUMN
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate TDSRET tds_func_put_data(IntPtr tds, IntPtr col, int bcp7); //:TDSSOCKET:TDSCOLUMN
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int tds_func_check(IntPtr col); //:TDSCOLUMN

    [StructLayout(LayoutKind.Sequential)]
    public class TDSCOLUMNFUNCS
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
    public class TDSCOLUMN
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

        [MarshalAs(UnmanagedType.AnsiBStr)] public string table_name;
        [MarshalAs(UnmanagedType.AnsiBStr)] public string column_name;
        [MarshalAs(UnmanagedType.AnsiBStr)] public string table_column_name;

        public byte[] column_data;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int column_data_free_t(IntPtr column); //:TDSCOLUMN
        public column_data_free_t column_data_free;
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
        [MarshalAs(UnmanagedType.LPStr)] public string bcp_terminator;
    }

    /// <summary>
    /// Hold information for any results
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class TDSRESULTINFO
    {
        /* TODO those fields can became a struct */
        public IntPtr columns; //: TDSCOLUMN[]
        public ushort num_cols;
        public ushort computeid;
        public int ref_count;
        public IntPtr attached_to; //:TDSSOCKET
        [MarshalAs(UnmanagedType.LPStr)] public string current_row;
        public delegate void row_free_t(IntPtr result, byte[] row); //:TDSRESULTINFO
        public row_free_t row_free;
        public int row_size;

        public IntPtr bycolumns; //: short[]
        public ushort by_cols;
        [MarshalAs(UnmanagedType.U1)] public bool rows_exist;
        /* TODO remove ?? used only in dblib */
        [MarshalAs(UnmanagedType.U1)] public bool more_results;
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
    public class TDSMESSAGE
    {
        [MarshalAs(UnmanagedType.LPStr)] public string server;
        [MarshalAs(UnmanagedType.LPStr)] public string message;
        [MarshalAs(UnmanagedType.LPStr)] public string proc_name;
        [MarshalAs(UnmanagedType.LPStr)] public string sql_state;
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
    public class TDSUPDCOL
    {
        public IntPtr next; //:TDSUPDCOL
        public int colnamelength;
        [MarshalAs(UnmanagedType.LPStr)] public string columnname;
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

    [StructLayout(LayoutKind.Sequential)]
    public class TDS_CURSOR_STATUS
    {
        public TDS_CURSOR_STATE declare;
        public TDS_CURSOR_STATE cursor_row;
        public TDS_CURSOR_STATE open;
        public TDS_CURSOR_STATE fetch;
        public TDS_CURSOR_STATE close;
        public TDS_CURSOR_STATE dealloc;
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
    public class TDSCURSOR
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
        [MarshalAs(UnmanagedType.LPStr)] public string cursor_name;
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
        [MarshalAs(UnmanagedType.U1)] public bool defer_close;
        /// <summary>
        /// SQL query
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)] public string query;
        /* TODO for updatable columns */
        ///// <summary>
        ///// number of updatable columns
        ///// </summary>
        //public byte number_upd_cols;
        ///// <summary>
        ///// updatable column list
        ///// </summary>
        //public IntPtr cur_col_list; //:TDSUPDCOL
        /// <summary>
        /// number of cursor rows to fetch
        /// </summary>
        public int cursor_rows;
        ///// <summary>
        ///// cursor parameter
        ///// </summary>
        //public IntPtr @params; //:TDSPARAMINFO
        public TDS_CURSOR_STATUS status;
        public ushort srv_status;
        /// <summary>
        /// row fetched from this cursor
        /// </summary>
        public IntPtr res_info; //:TDSRESULTINFO
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
        [MarshalAs(UnmanagedType.LPStr)] public string language;
        /// <summary>
        /// character set encoding
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)] public string charset;
        /// <summary>
        /// database name
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)] public string database;
    }

    /// <summary>
    /// Holds information for a dynamic (also called prepared) query.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class TDSDYNAMIC
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
        [MarshalAs(UnmanagedType.U1)] public bool defer_close;
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
        [MarshalAs(UnmanagedType.LPStr)] public string query;
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
    public class TDSCONTEXT //:https://stackoverflow.com/questions/50818345/set-c-sharp-callback-on-a-c-struct-obtained-via-p-invoke
    {
        public IntPtr locale; //:TDSLOCALE
        public IntPtr parent; //:TDSCONTEXT
        /* handlers */
        public delegate int msg_handler_t(IntPtr ctx, IntPtr s, IntPtr msg); //:TDSCONTEXT:TDSSOCKET:TDSMESSAGE
        public delegate int err_handler_t(IntPtr ctx, IntPtr s, IntPtr msg); //:TDSCONTEXT:TDSSOCKET:TDSMESSAGE
        public delegate int int_handler_t(IntPtr a);
        public msg_handler_t msg_handler;
        public err_handler_t err_handler;
        public int_handler_t int_handler;
        [MarshalAs(UnmanagedType.U1)] public bool money_use_2_digits;
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
    public class TDSAUTHENTICATION
    {
        public byte[] packet;
        public int packet_len;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate TDSRET free_t(IntPtr conn, IntPtr auth); //:TDSCONNECTION:TDSAUTHENTICATION
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate TDSRET handle_nextr_t(IntPtr tds, IntPtr auth, Size_t len); //:TDSSOCKET:TDSAUTHENTICATION
        public free_t free;
        public handle_nextr_t handle_next;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class TDSPACKET
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

    [StructLayout(LayoutKind.Sequential, Size = 10)]
    public unsafe struct tds_mutex
    {
        public IntPtr @lock;
        public int done;
        public uint thread_id;
        public fixed byte crit[40];
    }

    /// <summary>
    /// field related to connection
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class TDSCONNECTION
    {
        public ushort tds_version;
        /// <summary>
        /// version of product (Sybase/MS and full version)
        /// </summary>
        public uint product_version;
        [MarshalAs(UnmanagedType.LPStr)] public string product_name;

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
        public IntPtr char_convs; //:TDSICONV[]

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
        public IntPtr sessions; //:tds_socket[]
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
        public IntPtr authentication; //:TDSAUTHENTICATION
        [MarshalAs(UnmanagedType.LPStr)] public string server;
    }

    /// <summary>
    /// Information for a server connection
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class TDSSOCKET
    {
#if ENABLE_ODBC_MARS
        public IntPtr conn; public TDSCONNECTION conn__ => conn.ToMarshaled<TDSCONNECTION>(); //:TDSCONNECTION
#else
        public TDSCONNECTION conn; public TDSCONNECTION conn__ => conn; //:TDSCONNECTION
#endif

        /// <summary>
        /// Input buffer.
        /// Points to receiving packet buffer.
        /// As input buffer contains just the raw packet actually this pointer is the address of recv_packet->buf.
        /// </summary>
        public IntPtr in_buf; //:byte[]

        /// <summary>
        /// Output buffer.
        /// Points to sending packet buffer.
        /// Output buffer can contain additional data before the raw TDS packet so this buffer can point some bytes after send_packet->buf.
        /// </summary>
        public IntPtr out_buf; //:byte[]

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
        [MarshalAs(UnmanagedType.U1)] public TDS_PACKET_TYPE out_flag;

        public IntPtr parent;

#if ENABLE_ODBC_MARS
        public short sid;
        public IntPtr packet_cond; //: tds_condition
        public uint recv_seq;
        public uint send_seq;
        public uint recv_wnd;
        public uint send_wnd;
#endif

        /// <summary>
        /// packet we received
        /// </summary>
        public IntPtr recv_packet; public TDSPACKET recv_packet__ => recv_packet.ToMarshaled<TDSPACKET>(); //:TDSPACKET

        /// <summary>
        /// packet we are preparing to send
        /// </summary>
        public IntPtr send_packet; public TDSPACKET send_packet__ => send_packet.ToMarshaled<TDSPACKET>(); //:TDSPACKET

        /// <summary>
        /// Current query information. 
        /// Contains information in process, both normal and compute results.
        /// This pointer shouldn't be freed; it's just an alias to another structure.
        /// </summary>
        public IntPtr current_results; public TDSRESULTINFO current_results__ => current_results.ToMarshaled<TDSRESULTINFO>(); //:TDSRESULTINFO
        public IntPtr res_info; public TDSRESULTINFO res_info__ => res_info.ToMarshaled<TDSRESULTINFO>(); //:TDSRESULTINFO
        public uint num_comp_info;
        public IntPtr comp_info; public TDSCOMPUTEINFO comp_info__ => comp_info.ToMarshaled<TDSCOMPUTEINFO>(); //:TDSCOMPUTEINFO
        public IntPtr param_info; public TDSPARAMINFO param_info__ => param_info.ToMarshaled<TDSPARAMINFO>(); //:TDSPARAMINFO
        /// <summary>
        /// cursor in use
        /// </summary>
        public IntPtr cur_cursor; public TDSCURSOR cur_cursor__ => cur_cursor.ToMarshaled<TDSCURSOR>(); //:TDSCURSOR
        /// <summary>
        /// true is query sent was a bulk query so we need to switch state to QUERYING
        /// </summary>
        [MarshalAs(UnmanagedType.U1)] public bool bulk_query;
        /// <summary>
        /// true is ret_status is valid
        /// </summary>
        [MarshalAs(UnmanagedType.U1)] public bool has_status;
        /// <summary>
        /// true if we are getting rows
        /// </summary>
        [MarshalAs(UnmanagedType.U1)] public bool in_row;
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
        public IntPtr cur_dyn; public TDSDYNAMIC cur_dyn__ => cur_dyn.ToMarshaled<TDSDYNAMIC>(); //:TDSDYNAMIC

        /// <summary>
        /// config for login stuff. After login this field is NULL
        /// </summary>
        public IntPtr login; public TDSLOGIN login__ => login.ToMarshaled<TDSLOGIN>(); //:TDSLOGIN
        public delegate TDSRET env_chg_func_t(IntPtr tds, int type, [MarshalAs(UnmanagedType.LPStr)] string oldVal, [MarshalAs(UnmanagedType.LPStr)] string newval); //:TDSSOCKET
        public env_chg_func_t env_chg_func;
        public TDS_OPERATION current_op;

        public int option_value;
        public tds_mutex wire_mtx;
    }

    public static partial class NativeMethods
    {
        public static IntPtr tds_get_ctx(TDSSOCKET tds) => tds.conn__.tds_ctx; //:->TDSCONTEXT
        public static void tds_set_ctx(TDSSOCKET tds, IntPtr val) => Marshal.WriteIntPtr(tds.conn + Marshal.OffsetOf<TDSCONNECTION>("tds_ctx").ToInt32(), val); //:TDSCONTEXT
        public static IntPtr tds_get_parent(TDSSOCKET tds) => tds.parent; //:->TDSSOCKET
        public static void tds_set_parent(IntPtr tds, IntPtr val) => Marshal.WriteIntPtr(tds + Marshal.OffsetOf<TDSSOCKET>("parent").ToInt32(), val); //:TDSSOCKET:TDSSOCKET
        public static IntPtr tds_get_s(TDSSOCKET tds) => tds.conn__.s;
        public static void tds_set_s(TDSSOCKET tds, IntPtr val) => Marshal.WriteIntPtr(tds.conn + Marshal.OffsetOf<TDSCONNECTION>("s").ToInt32(), val); //:TDSCONNECTION

        #region config.c
        [DllImport(LibraryName)] public static extern IntPtr tds_get_compiletime_settings();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void TDSCONFPARSE([MarshalAs(UnmanagedType.LPStr)] string option, [MarshalAs(UnmanagedType.LPStr)] string value, IntPtr param);
        [DllImport(LibraryName)] public static extern bool tds_read_conf_section(IntPtr @in, [MarshalAs(UnmanagedType.LPStr)] string section, TDSCONFPARSE tds_conf_parse, IntPtr parse_param);
        [DllImport(LibraryName)] public static extern bool tds_read_conf_file(IntPtr login, [MarshalAs(UnmanagedType.LPStr)] string server); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern void tds_parse_conf_section([MarshalAs(UnmanagedType.LPStr)] string option, [MarshalAs(UnmanagedType.LPStr)] string value, IntPtr param);
        [DllImport(LibraryName)] public static extern IntPtr tds_read_config_info(IntPtr tds, IntPtr login, ref TDSLOCALE locale); //:TDSSOCKET:TDSLOGIN->TDSLOGIN
        [DllImport(LibraryName)] public static extern void tds_fix_login(IntPtr login); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern ushort[] tds_config_verstr([MarshalAs(UnmanagedType.LPStr)] string tdsver, IntPtr login); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern IntPtr tds_lookup_host([MarshalAs(UnmanagedType.LPStr)] string servername);
        [DllImport(LibraryName)] public static extern TDSRET tds_lookup_host_set([MarshalAs(UnmanagedType.LPStr)] string servername, IntPtr[] addr);
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.LPStr)] public static extern string tds_addrinfo2str(IntPtr addr, [MarshalAs(UnmanagedType.LPStr)] ref string name, int namemax);
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.LPStr)] public static extern string tds_get_home_file([MarshalAs(UnmanagedType.LPStr)] string file);

        [DllImport(LibraryName)] public static extern TDSRET tds_set_interfaces_file_loc([MarshalAs(UnmanagedType.LPStr)] string interfloc);
        //[DllImport(LibraryName)] [MarshalAs(UnmanagedType.LPStr)] public static extern string STD_DATETIME_FMT;
        [DllImport(LibraryName)] public static extern int tds_parse_boolean([MarshalAs(UnmanagedType.LPStr)] string value, int default_value);
        [DllImport(LibraryName)] public static extern int tds_config_boolean([MarshalAs(UnmanagedType.LPStr)] string option, [MarshalAs(UnmanagedType.LPStr)] string value, IntPtr login); //:TDSLOGIN

        [DllImport(LibraryName)] public static extern TDSLOCALE tds_get_locale();
        [DllImport(LibraryName)] public static extern TDSRET tds_alloc_row(IntPtr res_info); //:TDSRESULTINFO
        [DllImport(LibraryName)] public static extern TDSRET tds_alloc_compute_row(IntPtr res_info); //:TDSCOMPUTEINFO
        [DllImport(LibraryName)] public static extern IntPtr tds_alloc_bcp_column_data(uint column_size);
        [DllImport(LibraryName)] public static extern IntPtr tds_lookup_dynamic(IntPtr conn, [MarshalAs(UnmanagedType.LPStr)] string id); //:TDSCONNECTION
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.LPStr)] public static extern string tds_prtype(int token);
        [DllImport(LibraryName)] public static extern int tds_get_varint_size(IntPtr conn, int datatype); //:TDSCONNECTION 
        [DllImport(LibraryName)] public static extern TDS_SERVER_TYPE tds_get_cardinal_type(TDS_SERVER_TYPE datatype, int usertype);
        #endregion

        #region iconv.c
        [DllImport(LibraryName)] public static extern TDSRET tds_iconv_open(IntPtr conn, [MarshalAs(UnmanagedType.LPStr)] string charset, int use_utf16); //:TDSCONNECTION
        [DllImport(LibraryName)] public static extern void tds_iconv_close(IntPtr conn); //:TDSCONNECTION
        [DllImport(LibraryName)] public static extern void tds_srv_charset_changed(IntPtr conn, [MarshalAs(UnmanagedType.LPStr)] string charset); //:TDSCONNECTION
        [DllImport(LibraryName)] public static extern void tds7_srv_charset_changed(IntPtr conn, int sql_collate, int lcid); //:TDSCONNECTION
        [DllImport(LibraryName)] public static extern int tds_iconv_alloc(IntPtr conn); //:TDSCONNECTION
        [DllImport(LibraryName)] public static extern void tds_iconv_free(IntPtr conn); //:TDSCONNECTION
        [DllImport(LibraryName)] public static extern IntPtr tds_iconv_from_collate(IntPtr conn, [MarshalAs(UnmanagedType.LPStr)] string collate); //:TDSCONNECTION->TDSICONV
        #endregion

        #region mem.c
        [DllImport(LibraryName)] public static extern void tds_free_socket(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern void tds_free_all_results(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern void tds_free_results(IntPtr res_info); //:TDSRESULTINFO
        [DllImport(LibraryName)] public static extern void tds_free_param_results(IntPtr param_info); //:TDSPARAMINFO
        [DllImport(LibraryName)] public static extern void tds_free_param_result(IntPtr param_info); //:TDSPARAMINFO
        [DllImport(LibraryName)] public static extern void tds_free_msg(IntPtr message); //:TDSMESSAGE
        [DllImport(LibraryName)] public static extern void tds_cursor_deallocated(IntPtr conn, IntPtr cursor); //:TDSCONNECTION:TDSCURSOR
        [DllImport(LibraryName)] public static extern void tds_release_cursor(ref IntPtr pcursor); //:TDSCURSOR
        [DllImport(LibraryName)] public static extern void tds_free_bcp_column_data(IntPtr coldata); //:BCPCOLDATA
        [DllImport(LibraryName)] public static extern IntPtr tds_alloc_results(ushort num_cols); //:TDSRESULTINFO
        [DllImport(LibraryName)] public static extern IntPtr[] tds_alloc_compute_results(IntPtr tds, ushort num_cols, ushort by_cols); //:TDSSOCKET->TDSCOMPUTEINFO
        [DllImport(LibraryName)] public static extern IntPtr tds_alloc_context(IntPtr parent); //:->TDSCONTEXT
        [DllImport(LibraryName)] public static extern void tds_free_context(IntPtr locale); //:TDSCONTEXT
        [DllImport(LibraryName)] public static extern IntPtr tds_alloc_param_result(IntPtr old_param); //:TDSPARAMINFO->TDSPARAMINFO
        [DllImport(LibraryName)] public static extern void tds_free_input_params(IntPtr dyn); //:TDSDYNAMIC
        [DllImport(LibraryName)] public static extern void tds_release_dynamic(ref IntPtr dyn); //:TDSDYNAMIC
        public static void tds_release_cur_dyn(TdsSocket tds) => throw new NotImplementedException(); // tds_release_dynamic(ref tds.Value.cur_dyn);
        [DllImport(LibraryName)] public static extern void tds_dynamic_deallocated(IntPtr conn, IntPtr dyn); //:TDSCONNECTION:TDSDYNAMIC
        [DllImport(LibraryName)] public static extern void tds_set_cur_dyn(IntPtr tds, IntPtr dyn); //:TDSSOCKET:TDSDYNAMIC
        [DllImport(LibraryName)] public static extern IntPtr tds_realloc_socket(IntPtr tds, Size_t bufsize); //:TDSSOCKET->TDSSOCKET
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.LPStr)] public static extern string tds_alloc_client_sqlstate(int msgno);
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.LPStr)] public static extern string tds_alloc_lookup_sqlstate(IntPtr tds, int msgno); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern IntPtr tds_alloc_login(int use_environment); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern IntPtr tds_alloc_dynamic(IntPtr conn, [MarshalAs(UnmanagedType.LPStr)] string id); //:TDSCONNECTION->TDSDYNAMIC
        [DllImport(LibraryName)] public static extern void tds_free_login(IntPtr login); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern IntPtr tds_init_login(IntPtr login, IntPtr locale); //:TDSLOGIN:TDSLOCALE->TDSLOGIN
        [DllImport(LibraryName)] public static extern IntPtr tds_alloc_locale(); //:TDSLOCALE
        [DllImport(LibraryName)] public static extern IntPtr tds_alloc_param_data(IntPtr curparam); //:TDSCOLUMN
        [DllImport(LibraryName)] public static extern void tds_free_locale(IntPtr locale); //:TDSLOCALE
        [DllImport(LibraryName)] public static extern IntPtr tds_alloc_cursor(IntPtr tds, [MarshalAs(UnmanagedType.LPStr)] string name, int namelen, [MarshalAs(UnmanagedType.LPStr)] string query, int querylen); //:TDSSOCKET->TDSCURSOR
        [DllImport(LibraryName)] public static extern void tds_free_row(IntPtr res_info, byte[] row); //:TDSRESULTINFO
        [DllImport(LibraryName)] public static extern IntPtr tds_alloc_socket(IntPtr context, uint bufsize); //:TDSCONTEXT->TDSSOCKET
        [DllImport(LibraryName)] public static extern IntPtr tds_alloc_additional_socket(IntPtr conn); //:TDSCONNECTION->TDSSOCKET
        [DllImport(LibraryName)] public static extern void tds_set_current_results(IntPtr tds, IntPtr info); //:TDSSOCKET:TDSRESULTINFO
        [DllImport(LibraryName)] public static extern void tds_detach_results(IntPtr info); //:TDSRESULTINFO 
        [DllImport(LibraryName)] public static extern IntPtr tds_realloc(IntPtr pp, Size_t new_size);
        //#define TDS_RESIZE(p, n_elem) tds_realloc((void**) &(p), sizeof(*(p)) * (SizeT) (n_elem))

        [DllImport(LibraryName)] public static extern IntPtr tds_alloc_packet(byte[] buf, uint len); //:->TDSPACKET
        [DllImport(LibraryName)] public static extern IntPtr tds_realloc_packet(IntPtr packet, uint len); //:TDSPACKET->TDSPACKET
        [DllImport(LibraryName)] public static extern void tds_free_packets(IntPtr packet); //:TDSPACKET
        [DllImport(LibraryName)] public static extern IntPtr tds_alloc_bcpinfo(); //:TDSBCPINFO
        [DllImport(LibraryName)] public static extern void tds_free_bcpinfo(IntPtr bcpinfo); //:TDSBCPINFO
        [DllImport(LibraryName)] public static extern void tds_deinit_bcpinfo(IntPtr bcpinfo); //:TDSBCPINFO
        #endregion

        #region login.c
        [DllImport(LibraryName)] public static extern void tds_set_packet(IntPtr tds_login, int packet_size); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern void tds_set_port(IntPtr tds_login, int port); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern bool tds_set_passwd(IntPtr tds_login, [MarshalAs(UnmanagedType.LPStr)] string password); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern void tds_set_bulk(IntPtr tds_login, bool enabled); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern bool tds_set_user(IntPtr tds_login, [MarshalAs(UnmanagedType.LPStr)] string username); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern bool tds_set_app(IntPtr tds_login, [MarshalAs(UnmanagedType.LPStr)] string application); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern bool tds_set_host(IntPtr tds_login, [MarshalAs(UnmanagedType.LPStr)] string hostname); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern bool tds_set_library(IntPtr tds_login, [MarshalAs(UnmanagedType.LPStr)] string library); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern bool tds_set_server(IntPtr tds_login, [MarshalAs(UnmanagedType.LPStr)] string server); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern bool tds_set_client_charset(IntPtr tds_login, [MarshalAs(UnmanagedType.LPStr)] string charset); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern bool tds_set_language(IntPtr tds_login, [MarshalAs(UnmanagedType.LPStr)] string language); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern void tds_set_version(IntPtr tds_login, byte major_ver, byte minor_ver); //:TDSLOGIN
        [DllImport(LibraryName)] public static extern int tds_connect_and_login(IntPtr tds, IntPtr login); //:TDSSOCKET:TDSLOGIN
        #endregion

        #region query.c
        [DllImport(LibraryName)] public static extern void tds_start_query(IntPtr tds, byte packet_type); //:TDSSOCKET

        [DllImport(LibraryName)] public static extern TDSRET tds_submit_query(IntPtr tds, [MarshalAs(UnmanagedType.LPStr)] string query); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern TDSRET tds_submit_query_params(IntPtr tds, [MarshalAs(UnmanagedType.LPStr)] string query, IntPtr @params, IntPtr head); //:TDSSOCKET:TDSPARAMINFO:TDSHEADERS
        [DllImport(LibraryName)] public static extern TDSRET tds_submit_queryf(IntPtr tds, [MarshalAs(UnmanagedType.LPStr)] string queryf, params object[] args); //:TDSSOCKET 
        [DllImport(LibraryName)] public static extern TDSRET tds_submit_prepare(IntPtr tds, [MarshalAs(UnmanagedType.LPStr)] string query, [MarshalAs(UnmanagedType.LPStr)] string id, IntPtr dyn_out, IntPtr @params); //:TDSSOCKET:TDSDYNAMIC:TDSPARAMINFO
        [DllImport(LibraryName)] public static extern TDSRET tds_submit_execdirect(IntPtr tds, [MarshalAs(UnmanagedType.LPStr)] string query, IntPtr @params, IntPtr head); //:TDSSOCKET:TDSPARAMINFO:TDSHEADERS
        [DllImport(LibraryName)] public static extern TDSRET tds71_submit_prepexec(IntPtr tds, [MarshalAs(UnmanagedType.LPStr)] string query, [MarshalAs(UnmanagedType.LPStr)] string id, IntPtr dyn_out, IntPtr @params); //:TDSSOCKET:TDSDYNAMIC:TDSPARAMINFO
        [DllImport(LibraryName)] public static extern TDSRET tds_submit_execute(IntPtr tds, IntPtr dyn); //:TDSSOCKET:TDSDYNAMIC
        [DllImport(LibraryName)] public static extern TDSRET tds_send_cancel(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.LPStr)] public static extern string tds_next_placeholder([MarshalAs(UnmanagedType.LPStr)] string start);
        [DllImport(LibraryName)] public static extern int tds_count_placeholders([MarshalAs(UnmanagedType.LPStr)] string query);
        [DllImport(LibraryName)] public static extern int tds_needs_unprepare(IntPtr conn, IntPtr dyn); //:TDSCONNECTION:TDSDYNAMIC
        [DllImport(LibraryName)] public static extern TDSRET tds_deferred_unprepare(IntPtr conn, IntPtr dyn); //:TDSCONNECTION:TDSDYNAMIC
        [DllImport(LibraryName)] public static extern TDSRET tds_submit_unprepare(IntPtr tds, IntPtr dyn); //:TDSSOCKET:TDSDYNAMIC
        [DllImport(LibraryName)] public static extern TDSRET tds_submit_rpc(IntPtr tds, [MarshalAs(UnmanagedType.LPStr)] string rpc_name, IntPtr @params, IntPtr head); //:TDSSOCKET:TDSPARAMINFO:TDSHEADERS
        [DllImport(LibraryName)] public static extern TDSRET tds_submit_optioncmd(IntPtr tds, TDS_OPTION_CMD command, TDS_OPTION option, ref TDS_OPTION_ARG param, int param_size); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern TDSRET tds_submit_begin_tran(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern TDSRET tds_submit_rollback(IntPtr tds, int cont); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern TDSRET tds_submit_commit(IntPtr tds, int cont); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern TDSRET tds_disconnect(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern Size_t tds_quote_id(IntPtr tds, byte[] buffer, [MarshalAs(UnmanagedType.LPStr)] string id, int idlen); //:TDSSOCKET 
        [DllImport(LibraryName)] public static extern Size_t tds_quote_string(IntPtr tds, byte[] buffer, [MarshalAs(UnmanagedType.LPStr)] string str, int len); //:TDSSOCKET
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.LPStr)] public static extern string tds_skip_comment([MarshalAs(UnmanagedType.LPStr)] string s);
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.LPStr)] public static extern string tds_skip_quoted([MarshalAs(UnmanagedType.LPStr)] string s);
        [DllImport(LibraryName)] public static extern Size_t tds_fix_column_size(IntPtr tds, IntPtr curcol); //:TDSSOCKET:TDSCOLUMN
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.LPStr)] public static extern string tds_convert_string(IntPtr tds, IntPtr char_conv, [MarshalAs(UnmanagedType.LPStr)] string s, int len, out Size_t out_len); //:TDSSOCKET:TDSICONV 
        public static void tds_convert_string_free(string original, string converted) => throw new NotSupportedException();
        //[DllImport(LibraryName)] public static extern void tds_convert_string_free([MarshalAs(UnmanagedType.LPStr)] string original, [MarshalAs(UnmanagedType.LPStr)] string converted);
        //#if !ENABLE_EXTRA_CHECKS
        //        public static void tds_convert_string_free(IntPtr original, IntPtr converted) { if (original != converted) Marshal.FreeHGlobal(converted); }
        //#endif
        [DllImport(LibraryName)] public static extern TDSRET tds_get_column_declaration(IntPtr tds, IntPtr curcol, [MarshalAs(UnmanagedType.LPStr)] out string @out); //TDSSOCKET:TDSCOLUMN

        [DllImport(LibraryName)] public static extern TDSRET tds_cursor_declare(IntPtr tds, IntPtr cursor, IntPtr @params, out int send); //:TDSSOCKET:TDSCURSOR:TDSPARAMINFO
        [DllImport(LibraryName)] public static extern TDSRET tds_cursor_setrows(IntPtr tds, IntPtr cursor, out int send); //:TDSSOCKET:TDSCURSOR
        [DllImport(LibraryName)] public static extern TDSRET tds_cursor_open(IntPtr tds, IntPtr cursor, IntPtr @params, out int send); //:TDSSOCKET:TDSCURSOR:TDSCURSOR
        [DllImport(LibraryName)] public static extern TDSRET tds_cursor_fetch(IntPtr tds, IntPtr cursor, TDS_CURSOR_FETCH fetch_type, int i_row); //:TDSSOCKET:TDSCURSOR
        [DllImport(LibraryName)] public static extern TDSRET tds_cursor_get_cursor_info(IntPtr tds, IntPtr cursor, out uint row_number, out uint row_count); //:TDSSOCKET:TDSCURSOR
        [DllImport(LibraryName)] public static extern TDSRET tds_cursor_close(IntPtr tds, IntPtr cursor); //:TDSSOCKET:TDSCURSOR
        [DllImport(LibraryName)] public static extern TDSRET tds_cursor_dealloc(IntPtr tds, IntPtr cursor); //:TDSSOCKET:TDSCURSOR
        [DllImport(LibraryName)] public static extern TDSRET tds_deferred_cursor_dealloc(IntPtr conn, IntPtr cursor); //:TDSCONNECTION:TDSCURSOR
        [DllImport(LibraryName)] public static extern TDSRET tds_cursor_update(IntPtr tds, IntPtr cursor, TDS_CURSOR_OPERATION op, int i_row, IntPtr @params); //:TDSSOCKET:TDSCURSOR:TDS_CURSOR_OPERATION:TDSPARAMINFO
        [DllImport(LibraryName)] public static extern TDSRET tds_cursor_setname(IntPtr tds, IntPtr cursor); //:TDSSOCKET:TDSCURSOR

        [DllImport(LibraryName)] public static extern TDSRET tds_multiple_init(IntPtr tds, IntPtr multiple, TDS_MULTIPLE_TYPE type, IntPtr head); //:TDSSOCKET:TDSMULTIPLE:TDSHEADERS
        [DllImport(LibraryName)] public static extern TDSRET tds_multiple_done(IntPtr tds, IntPtr multiple); //:TDSSOCKET:TDSMULTIPLE
        [DllImport(LibraryName)] public static extern TDSRET tds_multiple_query(IntPtr tds, IntPtr multiple, [MarshalAs(UnmanagedType.LPStr)] string query, IntPtr @params); //:TDSSOCKET:TDSMULTIPLE:TDSPARAMINFO
        [DllImport(LibraryName)] public static extern TDSRET tds_multiple_execute(IntPtr tds, IntPtr multiple, IntPtr dyn); //:TDSSOCKET:TDSMULTIPLE:TDSDYNAMIC
        #endregion

        #region token.c
        [DllImport(LibraryName)] public static extern TDSRET tds_process_cancel(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern int tds_get_token_size(int marker);
        [DllImport(LibraryName)] public static extern TDSRET tds_process_login_tokens(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern TDSRET tds_process_simple_query(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern int tds5_send_optioncmd(IntPtr tds, TDS_OPTION_CMD tds_command, TDS_OPTION tds_option, TDS_OPTION_ARG[] tds_argument, int[] tds_argsize); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern TDSRET tds_process_tokens(IntPtr tds, out int result_type, out int done_flags, uint flag); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern int determine_adjusted_size(IntPtr char_conv, int size); //:TDSICONV
        #endregion

        #region data.c
        [DllImport(LibraryName)] public static extern void tds_set_param_type(IntPtr conn, IntPtr curcol, TDS_SERVER_TYPE type); //:TDSCONNECTION:TDSCOLUMN
        [DllImport(LibraryName)] public static extern void tds_set_column_type(IntPtr conn, IntPtr curcol, TDS_SERVER_TYPE type); //:TDSCONNECTION:TDSCOLUMN
        #endregion

        #region tds_convert.c
        [DllImport(LibraryName)] public static extern TDSRET tds_datecrack(int datetype, byte[] di, out TDSDATEREC dr); //:TDSDATEREC
        [DllImport(LibraryName)] public static extern TDS_SERVER_TYPE tds_get_conversion_type(TDS_SERVER_TYPE srctype, int colsize);
        //extern const char[] tds_hex_digits;
        #endregion

        #region write.c
        [DllImport(LibraryName)] public static extern int tds_init_write_buf(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern int tds_put_n(IntPtr tds, byte[] buf, Size_t n); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern int tds_put_string(IntPtr tds, [MarshalAs(UnmanagedType.LPStr)]string buf, int len); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern int tds_put_int(IntPtr tds, int i); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern int tds_put_int8(IntPtr tds, long i); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern int tds_put_smallint(IntPtr tds, short si); //:TDSSOCKET
        /// <summary>
        /// Output a tinyint value
        /// </summary>
        /// <param name="tds"></param>
        /// <param name="ti"></param>
        /// <returns></returns>
        public static int tds_put_tinyint(IntPtr tds, byte ti) => tds_put_byte(tds, ti); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern int tds_put_byte(IntPtr tds, byte c); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern TDSRET tds_flush_packet(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern int tds_put_buf(IntPtr tds, byte[] buf, int dsize, int ssize); //:TDSSOCKET
        #endregion

        #region read.c
        [DllImport(LibraryName)] public static extern byte tds_get_byte(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern void tds_unget_byte(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern byte tds_peek(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern ushort tds_get_usmallint(IntPtr tds); //:TDSSOCKET
        public static short tds_get_smallint(IntPtr tds) => (short)tds_get_usmallint(tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern uint tds_get_uint(IntPtr tds); //:TDSSOCKET
        public static int tds_get_int(IntPtr tds) => (int)tds_get_uint(tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern ulong tds_get_uint8(IntPtr tds); //:TDSSOCKET
        public static long tds_get_int8(IntPtr tds) => (long)tds_get_uint8(tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern Size_t tds_get_string(IntPtr tds, Size_t string_len, [MarshalAs(UnmanagedType.LPStr)] out string dest, Size_t dest_size); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern TDSRET tds_get_char_data(IntPtr tds, [MarshalAs(UnmanagedType.LPStr)] string dest, Size_t wire_size, IntPtr curcol); //:TDSSOCKET:TDSCOLUMN
        [DllImport(LibraryName)] public static extern bool tds_get_n(IntPtr tds, out byte[] dest, Size_t n); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern int tds_get_size_by_type(TDS_SERVER_TYPE servertype);
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.TBStr)] public static extern string tds_dstr_get(IntPtr tds, [MarshalAs(UnmanagedType.AnsiBStr)]  out string s, Size_t len); //:TDSSOCKET
        #endregion

        #region util.c
        [DllImport(LibraryName)] public static extern int tdserror(IntPtr tds_ctx, IntPtr tds, int msgno, int errnum); //:TDSCONTEXT:TDSSOCKET
        [DllImport(LibraryName)] public static extern TDS_STATE tds_set_state(IntPtr tds, TDS_STATE state); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern void tds_swap_bytes(byte[] buf, int bytes);
        [DllImport(LibraryName)] public static extern uint tds_gettime_ms();
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.LPStr)] public static extern string tds_strndup(byte[] s, IntPtr len);
        #endregion

        #region log.c
        [DllImport(LibraryName)] public static extern void tdsdump_off();
        [DllImport(LibraryName)] public static extern void tdsdump_on();
        [DllImport(LibraryName)] public static extern int tdsdump_isopen();
        [DllImport(LibraryName)] public static extern int tdsdump_open([MarshalAs(UnmanagedType.LPStr)] string filename);
        [DllImport(LibraryName)] public static extern void tdsdump_close();
        [DllImport(LibraryName)] public static extern void tdsdump_dump_buf([MarshalAs(UnmanagedType.LPStr)] string file, uint level_line, [MarshalAs(UnmanagedType.LPStr)] string msg, byte[] buf, Size_t length);
        [DllImport(LibraryName)] public static extern void tdsdump_col(IntPtr col); //:TDSCOLUMN
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void tdsdump_log([MarshalAs(UnmanagedType.LPStr)] string file, uint level_line, [MarshalAs(UnmanagedType.LPStr)] string fmt); //, __arglist);

        //public static void TDSDUMP_LOG_FAST() { if (TDS_UNLIKELY(tds_write_dump)) tdsdump_log() }
        //public static void tdsdump_log() => TDSDUMP_LOG_FAST();
        //public static void TDSDUMP_BUF_FAST() { if (TDS_UNLIKELY(tds_write_dump)) tdsdump_dump_buf(); }
        //public static void tdsdump_dump_buf() => TDSDUMP_BUF_FAST();

        //extern int tds_write_dump;
        //extern int tds_debug_flags;
        //extern int tds_g_append_mode;
        #endregion

        #region net.c
        [DllImport(LibraryName)] public static extern TDSERRNO tds_open_socket(IntPtr tds, IntPtr ipaddr, uint port, int timeout, out int p_oserr); //:TDSSOCKET:addrinfo
        [DllImport(LibraryName)] public static extern void tds_close_socket(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern int tds7_get_instance_ports(IntPtr output, IntPtr addr); //:addrinfo
        [DllImport(LibraryName)] public static extern int tds7_get_instance_port(IntPtr addr, [MarshalAs(UnmanagedType.LPStr)] string instance); //:addrinfo
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.LPStr)] public static extern string tds_prwsaerror(int erc);
        [DllImport(LibraryName)] public static extern void tds_prwsaerror_free([MarshalAs(UnmanagedType.LPStr)] string s);
        [DllImport(LibraryName)] public static extern int tds_connection_read(IntPtr tds, byte[] buf, int buflen); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern int tds_connection_write(IntPtr tds, byte[] buf, int buflen, int final); //:TDSSOCKET
                                                                                                                               //public static void TDSSELREAD() => POLLIN();
                                                                                                                               //public static void TDSSELWRITE() => POLLOUT();
        [DllImport(LibraryName)] public static extern int tds_select(IntPtr tds, uint tds_sel, int timeout_seconds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern void tds_connection_close(IntPtr conn); //:TDSCONNECTION
        [DllImport(LibraryName)] public static extern int tds_goodread(IntPtr tds, byte[] buf, int buflen); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern int tds_goodwrite(IntPtr tds, byte[] buffer, Size_t buflen); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern void tds_socket_flush(IntPtr sock); //:TDS_SYS_SOCKET
        [DllImport(LibraryName)] public static extern int tds_socket_set_nonblocking(IntPtr sock); //:TDS_SYS_SOCKET
        [DllImport(LibraryName)] public static extern int tds_wakeup_init(ref TDSPOLLWAKEUP wakeup);
        [DllImport(LibraryName)] public static extern void tds_wakeup_close(ref TDSPOLLWAKEUP wakeup);
        [DllImport(LibraryName)] public static extern void tds_wakeup_send(ref TDSPOLLWAKEUP wakeup, char cancel);
        public static IntPtr tds_wakeup_get_fd(ref TDSPOLLWAKEUP wakeup) => wakeup.s_signaled; //:TDS_SYS_SOCKET
        #endregion

        #region packet.c
        [DllImport(LibraryName)] public static extern int tds_read_packet(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern TDSRET tds_write_packet(IntPtr tds, byte final); //:TDSSOCKET
#if ENABLE_ODBC_MARS
        [DllImport(LibraryName)] public static extern int tds_append_cancel(IntPtr tds); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern TDSRET tds_append_fin(IntPtr tds); //:TDSSOCKET
#else
        [DllImport(LibraryName)] public static extern int tds_put_cancel(IntPtr tds); //:TDSSOCKET
#endif
        #endregion

        #region vstrbuild.c
        [DllImport(LibraryName)] public static extern TDSRET tds_vstrbuild(byte[] buffer, int buflen, ref int resultlen, [MarshalAs(UnmanagedType.LPStr)] string text, int textlen, [MarshalAs(UnmanagedType.LPStr)] string formats, int formatlen, IntPtr ap);
        #endregion

        #region numeric.c
        [DllImport(LibraryName)] [return: MarshalAs(UnmanagedType.LPStr)] public static extern string tds_money_to_string(ref TDS_MONEY money, [MarshalAs(UnmanagedType.LPStr)] string s, bool use_2_digits);
        [DllImport(LibraryName)] public static extern int tds_numeric_to_string(ref TDS_NUMERIC numeric, [MarshalAs(UnmanagedType.LPStr)] string s);
        [DllImport(LibraryName)] public static extern int tds_numeric_change_prec_scale(ref TDS_NUMERIC numeric, byte new_prec, byte new_scale);
        #endregion

        #region getmac.c
        [DllImport(LibraryName)] public static extern void tds_getmac(IntPtr s, byte[] mac); //:TDS_SYS_SOCKET
        #endregion

        #region challenge.c
#if !HAVE_SSPI
        [DllImport(LibraryName)] public static extern IntPtr tds_ntlm_get_auth(IntPtr tds); //:TDSSOCKET->TDSAUTHENTICATION
        [DllImport(LibraryName)] public static extern IntPtr tds_gss_get_auth(IntPtr tds); //:TDSSOCKET->TDSAUTHENTICATION
#else
        [DllImport(LibraryName)] public static extern IntPtr tds_sspi_get_auth(IntPtr tds); //:TDSSOCKET->TDSAUTHENTICATION
#endif
        #endregion

        #region random.c
        [DllImport(LibraryName)] public static extern void tds_random_buffer(byte[] @out, int len);
        #endregion

        #region sec_negotiate.c
        [DllImport(LibraryName)] public static extern IntPtr tds5_negotiate_get_auth(IntPtr tds); //:TDSSOCKET->TDSAUTHENTICATION
        [DllImport(LibraryName)] public static extern void tds5_negotiate_set_msg_type(IntPtr tds, IntPtr auth, uint msg_type); //:TDSSOCKET:TDSAUTHENTICATION
        #endregion
    }

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
        [MarshalAs(UnmanagedType.LPStr)] string hint;
        IntPtr parent;
        [MarshalAs(UnmanagedType.AnsiBStr)] string tablename;
        [MarshalAs(UnmanagedType.LPStr)] string insert_stmt;
        int direction;
        int identity_insert_on;
        int xfer_init;
        int bind_count;
        IntPtr bindinfo; //:TDSRESULTINFO
    }

    partial class NativeMethods
    {
        #region bulk.c
        [DllImport(LibraryName)] public static extern TDSRET tds_bcp_init(IntPtr tds, IntPtr bcpinfo); //:TDSSOCKET:TDSBCPINFO
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate TDSRET tds_bcp_get_col_data(IntPtr bulk, IntPtr bcpcol, int offset); //:TDSBCPINFO:TDSCOLUMN
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void tds_bcp_null_error(IntPtr bulk, int index, int offset); //:TDSBCPINFO
        [DllImport(LibraryName)] public static extern TDSRET tds_bcp_send_record(IntPtr tds, IntPtr bcpinfo, tds_bcp_get_col_data get_col_data, tds_bcp_null_error null_error, int offset); //:TDSSOCKET:TDSBCPINFO
        [DllImport(LibraryName)] public static extern TDSRET tds_bcp_done(IntPtr tds, out int rows_copied); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern TDSRET tds_bcp_start(IntPtr tds, IntPtr bcpinfo); //:TDSSOCKET:TDSBCPINFO
        [DllImport(LibraryName)] public static extern TDSRET tds_bcp_start_copy_in(IntPtr tds, IntPtr bcpinfo); //:TDSSOCKET:TDSBCPINFO

        [DllImport(LibraryName)] public static extern TDSRET tds_bcp_fread(IntPtr tds, IntPtr conv, IntPtr stream, [MarshalAs(UnmanagedType.LPStr)] string terminator, Size_t term_len, out byte[] outbuf, out Size_t outbytes); //:TDSSOCKET:TDSICONV

        [DllImport(LibraryName)] public static extern TDSRET tds_writetext_start(IntPtr tds, [MarshalAs(UnmanagedType.LPStr)] string objname, [MarshalAs(UnmanagedType.LPStr)] string textptr, [MarshalAs(UnmanagedType.LPStr)] string timestamp, int with_log, uint size); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern TDSRET tds_writetext_continue(IntPtr tds, [MarshalAs(UnmanagedType.LPStr)] string text, uint size); //:TDSSOCKET
        [DllImport(LibraryName)] public static extern TDSRET tds_writetext_end(IntPtr tds); //:TDSSOCKET
        #endregion
    }

    partial class G
    {
        //public unsafe static bool tds_capability_enabled(ref TDS_CAPABILITY_TYPE cap, uint cap_num) => cap.values[sizeof(cap.values) - 1 - (cap_num >> 3)] >> (cap_num & 7)) &1;
        //public static bool tds_capability_has_req(ref TDSCONNECTION conn, ref TDS_CAPABILITY_TYPE cap) => tds_capability_enabled(ref conn.capabilities.types[0], cap);

        public static bool IS_TDS42(TDSCONNECTION x) => x.tds_version == 0x402;
        public static bool IS_TDS46(TDSCONNECTION x) => x.tds_version == 0x406;
        public static bool IS_TDS50(TDSCONNECTION x) => x.tds_version == 0x500;
        public static bool IS_TDS70(TDSCONNECTION x) => x.tds_version == 0x700;
        public static bool IS_TDS71(TDSCONNECTION x) => x.tds_version == 0x701;
        public static bool IS_TDS72(TDSCONNECTION x) => x.tds_version == 0x702;
        public static bool IS_TDS73(TDSCONNECTION x) => x.tds_version == 0x703;

        public static bool IS_TDS50_PLUS(TDSCONNECTION x) => x.tds_version >= 0x500;
        public static bool IS_TDS7_PLUS(TDSCONNECTION x) => x.tds_version >= 0x700;
        public static bool IS_TDS71_PLUS(TDSCONNECTION x) => x.tds_version >= 0x701;
        public static bool IS_TDS72_PLUS(TDSCONNECTION x) => x.tds_version >= 0x702;
        public static bool IS_TDS73_PLUS(TDSCONNECTION x) => x.tds_version >= 0x703;
        public static bool IS_TDS74_PLUS(TDSCONNECTION x) => x.tds_version >= 0x704;

        public static byte TDS_MAJOR(TDSLOGIN x) => (byte)(x.tds_version >> 8);
        public static byte TDS_MAJOR(TDSCONNECTION x) => (byte)(x.tds_version >> 8);
        public static byte TDS_MINOR(TDSLOGIN x) => (byte)(x.tds_version & 0xff);
        public static byte TDS_MINOR(TDSCONNECTION x) => (byte)(x.tds_version & 0xff);

        public static bool IS_TDSDEAD(TDSSOCKET x) => x == null || x.state == TDS_STATE.TDS_DEAD;

        /// <summary>
        /// Check if product is Sybase (such as Adaptive Server Enterrprice). x should be a TDSSOCKET*.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool TDS_IS_SYBASE(TDSSOCKET x) => (x.conn__.product_version & 0x80000000u) == 0;
        /// <summary>
        /// Check if product is Microsft SQL Server. x should be a TDSSOCKET*.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool TDS_IS_MSSQL(TDSSOCKET x) => (x.conn__.product_version & 0x80000000u) != 0;

        /// <summary>
        /// Calc a version number for mssql. Use with TDS_MS_VER(7,0,842).
        /// For test for a range of version you can use check like
        /// if (tds.product_version >= TDS_MS_VER(7,0,0) && tds.product_version &lt; TDS_MS_VER(8,0,0))
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static uint TDS_MS_VER(int maj, int min, int x) => (uint)(0x80000000u | (uint)(maj << 24) | (uint)(min << 16) | (uint)x);

        /// <summary>
        /// TODO test if not similar to ms one
        /// Calc a version number for Sybase.
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static ulong TDS_SYB_VER(int maj, int min, int x) => (ulong)((maj << 24) | (min << 16) | x << 8);

        public static void TDS_PUT_INT(IntPtr tds, int v) => NativeMethods.tds_put_int(tds, v); //:TDSSOCKET
        public static void TDS_PUT_SMALLINT(IntPtr tds, short v) => NativeMethods.tds_put_smallint(tds, v); //:TDSSOCKET
        public static void TDS_PUT_BYTE(IntPtr tds, byte v) => NativeMethods.tds_put_byte(tds, v);  //:TDSSOCKET
    }
}