using System;
using System.Runtime.InteropServices;

namespace FreeTds
{
    static class G { }

    /// <summary>
    /// A structure to hold all the compile-time settings.
    /// This structure is returned by tds_get_compiletime_settings
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class TDS_COMPILETIME_SETTINGS
    {
        public string freetds_version; /* release version of FreeTDS */
        public string sysconfdir; /* location of freetds.conf */
        public string last_update; /* latest software_version date among the modules */
        public int msdblib; /* for MS style dblib */
        public int sybase_compat; /* enable increased Open Client binary compatibility */
        public int threadsafe; /* compile for thread safety default=no */
        public int libiconv; /* search for libiconv in DIR/include and DIR/lib */
        public string tdsver; /* TDS protocol version (4.2/4.6/5.0/7.0/7.1) 5.0 */
        public int iodbc; /* build odbc driver against iODBC in DIR */
        public int unixodbc; /* build odbc driver against unixODBC in DIR */
        public int openssl; /* build against OpenSSL */
        public int gnutls; /* build against GnuTLS */
        public int mars; /* MARS enabled */
    }

    /// <summary>
    /// this structure is not directed connected to a TDS protocol but keeps any DATE/TIME information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class TDS_DATETIMEALL
    {
    }

    /// <summary>
    /// Used by tds_datecrack 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class TDSDATEREC
    {
    }

    partial class G
    {
        public static readonly int[] tds_numeric_bytes_per_prec;
    }

    public enum TDSRET : int
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
        public static TDSRET TDS_PROPAGATE(this TDSRET rc)
        {
            do { TDSRET _tds_ret = rc; if (TDS_FAILED(_tds_ret)) return _tds_ret; } while (true);
        }
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
        TDS_DONE_FINAL = 0x00,   /* < final result set, command completed successfully. */
        TDS_DONE_MORE_RESULTS = 0x01,  /* < more results follow */
        TDS_DONE_ERROR = 0x02, /* < error occurred */
        TDS_DONE_INXACT = 0x04,    /* < transaction in progress */
        TDS_DONE_PROC = 0x08,  /* < results are from a stored procedure */
        TDS_DONE_COUNT = 0x10, /* < count field in packet is valid */
        TDS_DONE_CANCELLED = 0x20, /* < acknowledging an attention command (usually a cancel) */
        TDS_DONE_EVENT = 0x40, /* part of an event notification. */
        TDS_DONE_SRVERROR = 0x100, /* < SQL server server error */

        /* after the above flags, a TDS_DONE packet has a field describing the state of the transaction */
        TDS_DONE_NO_TRAN = 0, /* No transaction in effect */
        TDS_DONE_TRAN_SUCCEED = 1, /* Transaction completed successfully */
        TDS_DONE_TRAN_PROGRESS = 2, /* Transaction in progress */
        TDS_DONE_STMT_ABORT = 3,   /* A statement aborted */
        TDS_DONE_TRAN_ABORT = 4   /* Transaction aborted */
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
        TDSEINTF = 20012,   /* Server name not found in interface file */
        TDSEUHST = 20013,   /* Unknown host machine name. */
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
        public const int TDS_NULLTERM = -9; /* string types */
    }


    struct TDS_OPTION_ARG
    {
        /*SKY:TODO*/
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
        public static bool is_end_token(int x) => (x >= TDS_DONE_TOKEN && x <= TDS_DONEINPROC_TOKEN);
    }

    public enum TDS02_
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
        public static readonly ushort[] tds_type_flags_ms;
    }

    partial class G
    {
        public static bool is_fixed_type(int x) => (tds_type_flags_ms[x] & G.TDS_TYPEFLAG_FIXED) != 0;
        public static bool is_nullable_type(int x) => (tds_type_flags_ms[x] & G.TDS_TYPEFLAG_NULLABLE) != 0;
        public static bool is_variable_type(int x) => (tds_type_flags_ms[x] & G.TDS_TYPEFLAG_VARIABLE) != 0;

        public static bool is_blob_type(int x) => x == G.SYBTEXT || x == G.SYBIMAGE || x == G.SYBNTEXT;
        public static bool is_blob_col(int x) => x.column_varint_size > 2;
        /* large type means it has a two byte size field */
        /* define is_large_type(x) (x>128) */
        public static bool is_numeric_type(int x) => x == G.SYBNUMERIC || x == SYBDECIMAL;
        /* return true if type is a datetime (but not date or time) */
        public static bool is_datetime_type(int x) => (tds_type_flags_ms[x] & G.TDS_TYPEFLAG_DATETIME) != 0;
        public static bool is_unicode_type(int x) => (tds_type_flags_ms[x] & G.TDS_TYPEFLAG_UNICODE) != 0;
        public static bool is_collate_type(int x) => (tds_type_flags_ms[x] & G.TDS_TYPEFLAG_COLLATE) != 0;
        public static bool is_ascii_type(int x) => (tds_type_flags_ms[x] & G.TDS_TYPEFLAG_ASCII) != 0;
        public static bool is_binary_type(int x) => (tds_type_flags_ms[x] & G.TDS_TYPEFLAG_BINARY) != 0;
        public static bool is_char_type(int x) => (tds_type_flags_ms[x] & (G.TDS_TYPEFLAG_ASCII | G.TDS_TYPEFLAG_UNICODE)) != 0;
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
        public const int TDS_ALIGN_SIZE = -1;/*SKY:TODO*/
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
        [MarshalAs(UnmanagedType.TBStr)] public string server_name;       /* < server name (in freetds.conf) */
        public int port;           /* < port of database service */
        public ushort tds_version;  /* < TDS version */
        public int block_size;
        [MarshalAs(UnmanagedType.TBStr)] public string language;          /* e.g. us-english */
        [MarshalAs(UnmanagedType.TBStr)] public string server_charset;        /* < charset of server e.g. iso_1 */
        public int connect_timeout;
        [MarshalAs(UnmanagedType.TBStr)] public string client_host_name;
        [MarshalAs(UnmanagedType.TBStr)] public string server_host_name;
        [MarshalAs(UnmanagedType.TBStr)] public string server_realm_name;     /* < server realm name (in freetds.conf) */
        [MarshalAs(UnmanagedType.TBStr)] public string server_spn;        /* < server SPN (in freetds.conf) */
        [MarshalAs(UnmanagedType.TBStr)] public string db_filename;       /* < database filename to attach (MSSQL) */
        [MarshalAs(UnmanagedType.TBStr)] public string cafile;            /* < certificate authorities file */
        [MarshalAs(UnmanagedType.TBStr)] public string crlfile;           /* < certificate revocation file */
        [MarshalAs(UnmanagedType.TBStr)] public string openssl_ciphers;
        [MarshalAs(UnmanagedType.TBStr)] public string app_name;
        [MarshalAs(UnmanagedType.TBStr)] public string user_name;         /* < account for login */
        [MarshalAs(UnmanagedType.TBStr)] public string password;          /* < password of account login */
        [MarshalAs(UnmanagedType.TBStr)] public string new_password;          /* < new password to set (TDS 7.2+) */

        [MarshalAs(UnmanagedType.TBStr)] public string library;   /* Ct-Library, DB-Library,  TDS-Library or ODBC */
        public byte encryption_level;

        public int query_timeout;
        public TDS_CAPABILITIES capabilities;
        [MarshalAs(UnmanagedType.TBStr)] public string client_charset;
        [MarshalAs(UnmanagedType.TBStr)] public string database;

        public IntPtr ip_addrs;            /* < ip(s) of server */ //: SKY:addrinfo
        [MarshalAs(UnmanagedType.TBStr)] public string instance_name;
        [MarshalAs(UnmanagedType.TBStr)] public string dump_file;
        public int debug_flags;
        public int text_size;
        [MarshalAs(UnmanagedType.TBStr)] public string routing_address;
        public ushort routing_port;

        public byte option_flag2;

        uint _data_;
        public uint bulk_copy => 0; //:1;   /* < if bulk copy should be enabled */
        public uint suppress_language => 0; //:1;
        public uint emul_little_endian => 0; //:1;
        public uint gssapi_use_delegation => 0; //:1;
        public uint use_ntlmv2 => 0; //:1;
        public uint use_ntlmv2_specified => 0; //:1;
        public uint use_lanman => 0; //:1;
        public uint mars => 0; //:1;
        public uint use_utf16 => 0; //:1;
        public uint use_new_password => 0; //:1;
        public uint valid_configuration => 0; //:1;
        public uint check_ssl_hostname => 0; //:1;
        public uint readonly_intent => 0; //:1;
        public uint enable_tls_v1 => 0; //:1;
        public uint server_is_valid => 0; //:1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSHEADERS
    {
        [MarshalAs(UnmanagedType.LPStr)] public string qn_options;
        [MarshalAs(UnmanagedType.LPStr)] public string qn_msgtext;
        int qn_timeout;
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
        /* this MUST have same position and place of textvalue in tds_blob */
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
        /* name of the encoding (ie UTF-8) */
        [MarshalAs(UnmanagedType.LPStr)] public string name;
        public byte min_bytes_per_char;
        public byte max_bytes_per_char;
        /* internal numeric index into array of all encodings */
        public byte canonic;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BCPCOLDATA
    {
        public IntPtr data;
        public int datalen;
        public int is_null;
    }

    //typedef TDSRET  tds_func_get_info(TDSSOCKET* tds, TDSCOLUMN* col);
    //typedef TDSRET  tds_func_get_data(TDSSOCKET* tds, TDSCOLUMN* col);
    //typedef TDS_INT tds_func_row_len(TDSCOLUMN* col);
    //typedef unsigned tds_func_put_info_len(TDSSOCKET* tds, TDSCOLUMN* col);
    //typedef TDSRET  tds_func_put_info(TDSSOCKET* tds, TDSCOLUMN* col);
    //typedef TDSRET  tds_func_put_data(TDSSOCKET* tds, TDSCOLUMN* col, int bcp7);
    //typedef int tds_func_check(const TDSCOLUMN* col);

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSCOLUMNFUNCS
    {
        public IntPtr get_info;
        public IntPtr get_data;
        public IntPtr row_len;
        /*
         * Returns metadata column information size.
         * \tds
         * \param col  column to send
         */
        public IntPtr put_info_len;
        /*
         * Send metadata column information to server.
         * \tds
         * \param col  column to send
         */
        public IntPtr put_info;
        /*
         * Send column data to server.
         * Usually send parameters unless bcp7 is specified, in
         * this case send BCP for TDS7+ (Sybase use a completely
         * different format for BCP)
         * \tds
         * \param col  column to send
         * \param bcp7 1 to send BCP column on TDS7+
         */
        public IntPtr put_data;
#if ENABLE_EXTRA_CHECKS
        /*
         * Check column is valid.
         * Some things should be checked:
         * - column_type and on_server.column_type;
         * - column_size and on_server.column_size;
         * - column_cur_size;
         * - column_prec and column_scale;
         * - is_XXXX_type macros/functions (nullable/fixed/blob/variable);
         * - tds_get_size_by_type;
         * - tds_get_conversion_type.
         *
         * \tds
         * \param col  column to check
         */
        public IntPtr check;
#endif
    }

    /// <summary>
    /// Metadata about columns in regular and compute rows 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TDSCOLUMN
    {
        public TDSCOLUMNFUNCS funcs;
        public int column_usertype;
        public int column_flags;

        public int column_size;        /* < maximun size of data. For fixed is the size. */

        public TDS_SERVER_TYPE column_type;    /* < This type can be different from wire type because
	 				 * conversion (e.g. UCS-2->Ascii) can be applied.
					 * I'm beginning to wonder about the wisdom of this, however.
					 * April 2003 jkl
					 */
        public byte column_varint_size; /* < size of length when reading from wire (0, 1, 2 or 4) */

        public byte column_prec;    /* < precision for decimal/numeric */
        public byte column_scale;   /* < scale for decimal/numeric */

        [StructLayout(LayoutKind.Sequential)]
        public struct on_server_t
        {

            public TDS_SERVER_TYPE column_type;    /* < type of data, saved from wire */
            public int column_size;
        }
        public on_server_t on_server;

        public TDSICONV char_conv;    /* < refers to previously allocated iconv information */

        [MarshalAs(UnmanagedType.TBStr)] public string table_name;
        [MarshalAs(UnmanagedType.TBStr)] public string column_name;
        [MarshalAs(UnmanagedType.TBStr)] public string table_column_name;

        public byte[] column_data;
        public IntPtr column_data_free;
        public byte _data_;
        public byte column_nullable => 0; //:1;
        public byte column_writeable => 0; //:1;
        public byte column_identity => 0; //:1;
        public byte column_key => 0; //:1;
        public byte column_hidden => 0; //:1;
        public byte column_output => 0; //:1;
        public byte column_timestamp => 0; //:1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)] public string column_collation;

        /* additional fields flags for compute results */
        public short column_operand;
        public byte column_operator;

        /* FIXME this is data related, not column */
        /* size written in variable (ie: char, text, binary). -1 if NULL. */
        public int column_cur_size;

        /* related to binding or info stored by client libraries */
        /* FIXME find a best place to store these data, some are unused */
        public short column_bindtype;
        public short column_bindfmt;
        public uint column_bindlen;
        [MarshalAs(UnmanagedType.LPArray)] public short[] column_nullbind;
        [MarshalAs(UnmanagedType.LPArray)] public byte[] column_varaddr;
        [MarshalAs(UnmanagedType.LPArray)] public int[] column_lenbind;
        public int column_textpos;
        public int column_text_sqlgetdatapos;
        public byte column_text_sqlputdatainfo;
        public byte column_iconv_left;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)] public string column_iconv_buf;

        public BCPCOLDATA bcp_column_data;
        /*
         * The length, in bytes, of any length prefix this column may have.
         * For example, strings in some non-C programming languages are
         * made up of a one-byte length prefix, followed by the string
         * data itself.
         * If the data do not have a length prefix, set prefixlen to 0.
         * Currently not very used in code, however do not remove.
         */
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
        public TDSSOCKET attached_to;
        public byte[] current_row;
        public IntPtr row_free;
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
        TDS_IDLE,   /* < no data expected */
        TDS_WRITING,    /* < client is writing data */
        TDS_SENDING,    /* < client would send data */
        TDS_PENDING,    /* < cilent is waiting for data */
        TDS_READING,    /* < client is reading data */
        TDS_DEAD    /* < no connection */
    }

    public enum TDS_OPERATION
    {
        TDS_OP_NONE = 0,

        /* mssql operations */
        TDS_OP_CURSOR = TDS_SP_CURSOR,
        TDS_OP_CURSOROPEN = TDS_SP_CURSOROPEN,
        TDS_OP_CURSORPREPARE = TDS_SP_CURSORPREPARE,
        TDS_OP_CURSOREXECUTE = TDS_SP_CURSOREXECUTE,
        TDS_OP_CURSORPREPEXEC = TDS_SP_CURSORPREPEXEC,
        TDS_OP_CURSORUNPREPARE = TDS_SP_CURSORUNPREPARE,
        TDS_OP_CURSORFETCH = TDS_SP_CURSORFETCH,
        TDS_OP_CURSOROPTION = TDS_SP_CURSOROPTION,
        TDS_OP_CURSORCLOSE = TDS_SP_CURSORCLOSE,
        TDS_OP_EXECUTESQL = TDS_SP_EXECUTESQL,
        TDS_OP_PREPARE = TDS_SP_PREPARE,
        TDS_OP_EXECUTE = TDS_SP_EXECUTE,
        TDS_OP_PREPEXEC = TDS_SP_PREPEXEC,
        TDS_OP_PREPEXECRPC = TDS_SP_PREPEXECRPC,
        TDS_OP_UNPREPARE = TDS_SP_UNPREPARE,

        /* sybase operations */
        TDS_OP_DYN_DEALLOC = 100,
    }

    partial class G
    {
        //#define TDS_DBG_LOGIN   __FILE__, ((__LINE__ << 4) | 11)
        //#define TDS_DBG_HEADER  __FILE__, ((__LINE__ << 4) | 10)
        //#define TDS_DBG_FUNC    __FILE__, ((__LINE__ << 4) |  7)
        //#define TDS_DBG_INFO2   __FILE__, ((__LINE__ << 4) |  6)
        //#define TDS_DBG_INFO1   __FILE__, ((__LINE__ << 4) |  5)
        //#define TDS_DBG_NETWORK __FILE__, ((__LINE__ << 4) |  4)
        //#define TDS_DBG_WARN    __FILE__, ((__LINE__ << 4) |  3)
        //#define TDS_DBG_ERROR   __FILE__, ((__LINE__ << 4) |  2)
        //#define TDS_DBG_SEVERE  __FILE__, ((__LINE__ << 4) |  1)

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
        public const int TDS_DBGFLAG_PID = 0x100;0
public const int TDS_DBGFLAG_TIME = 0x2000;
        public const int TDS_DBGFLAG_SOURCE = 0x4000;
        public const int TDS_DBGFLAG_THREAD = 0x8000;
    }

    //typedef struct tds_result_info TDSCOMPUTEINFO;
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
        /* -1 .. 255 */
        public short state;
        public byte priv_msg_type;
        public byte severity;
        /* for library-generated errors */
        public int oserr;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSUPDCOL
    {
        public IntPtr next;
        public int colnamelength;
        [MarshalAs(UnmanagedType.LPTStr)] public string columnname;
    }

    public enum TDS_CURSOR_STATE
    {
        TDS_CURSOR_STATE_UNACTIONED = 0,    /* initial value */
        TDS_CURSOR_STATE_REQUESTED = 1,    /* called by ct_cursor */
        TDS_CURSOR_STATE_SENT = 2,     /* sent to server */
        TDS_CURSOR_STATE_ACTIONED = 3,     /* acknowledged by server */
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
        public IntPtr next; /* < next in linked list, keep first */
        public int ref_count;      /* < reference counter so client can retain safely a pointer */
        [MarshalAs(UnmanagedType.LPTStr)] public string cursor_name;      /* < name of the cursor */
        public int cursor_id;      /* < cursor id returned by the server after cursor declare */
        public byte options;        /* < read only|updatable TODO use it */
        /*
         * true if cursor was marker to be closed when connection is idle
         */
        public bool defer_close;
        [MarshalAs(UnmanagedType.LPTStr)] public string query;                    /* < SQL query */
        /* TODO for updatable columns */
        /* TDS_TINYINT number_upd_cols; */    /* < number of updatable columns */
        /* TDSUPDCOL *cur_col_list; */    /* < updatable column list */
        public int cursor_rows;        /* < number of cursor rows to fetch */
        /* TDSPARAMINFO *params; */    /* cursor parameter */
        public TDS_CURSOR_STATUS status;
        public ushort srv_status;
        public TDSRESULTINFO res_info;    /* row fetched from this cursor */
        public int type, concurrency;
    }

    /// <summary>
    /// Current environment as reported by the server
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TDSENV
    {
        /* packet size (512-65535) */
        public int block_size;
        [MarshalAs(UnmanagedType.LPTStr)] public string language;
        /* character set encoding */
        [MarshalAs(UnmanagedType.LPTStr)] public string charset;
        /* database name */
        [MarshalAs(UnmanagedType.LPTStr)] public string database;
    }

    /// <summary>
    /// Holds information for a dynamic (also called prepared) query.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TDSDYNAMIC
    {
        public IntPtr next; /*< next in linked list, keep first */
        public int ref_count;      /*< reference counter so client can retain safely a pointer */
        /* numeric id for mssql7+*/
        public int num_id;
        /* 
         * id of dynamic.
         * Usually this id correspond to server one but if not specified
         * is generated automatically by libTDS
         */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)] public string id;
        /*
         * this dynamic query cannot be prepared so libTDS have to construct a simple query.
         * This can happen for instance is tds protocol doesn't support dynamics or trying
         * to prepare query under Sybase that have BLOBs as parameters.
         */
        public byte emulated;
        /*
         * true if dynamic was marker to be closed when connection is idle
         */
        public bool defer_close;
        /* int dyn_state; */ /* TODO use it */
        public TDSPARAMINFO res_info; /*< query results */
        /*
         * query parameters.
         * Mostly used executing query however is a good idea to prepare query
         * again if parameter type change in an incompatible way (ie different
         * types or larger size). Is also better to prepare a query knowing
         * parameter types earlier.
         */
        public TDSPARAMINFO @params;
        /* saved query, we need to know original query if prepare is impossible */
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

    //typedef int (* err_handler_t) (const TDSCONTEXT*, TDSSOCKET *, TDSMESSAGE*);

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSCONTEXT
    {
        public TDSLOCALE locale;
        public IntPtr parent;
        /* handlers */
        public IntPtr msg_handler;
        public IntPtr err_handler;
        public IntPtr int_handler;
        public bool money_use_2_digits;
    }

    public enum TDS_ICONV_ENTRY
    {
        client2ucs2,
        client2server_chardata,
        initial_char_conv_count,   /* keep last */
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSAUTHENTICATION
    {
        public byte[] packet;
        public int packet_len;
        public IntPtr free;
        public IntPtr handle_next;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSPACKET
    {
        public IntPtr next;
        public short sid;
        public uint len, capacity;
        public byte[] buf;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDSPOLLWAKEUP
    {
        public TDS_SYS_SOCKET s_signal, s_signaled;
    }

    /* field related to connection */
    [StructLayout(LayoutKind.Sequential)]
    public struct TDSCONNECTION
    {
        public ushort tds_version;
        public uint product_version;   /*< version of product (Sybase/MS and full version) */
        [MarshalAs(UnmanagedType.LPTStr)] public string product_name;

        public TDS_SYS_SOCKET s;       /*< tcp socket, INVALID_SOCKET if not connected */
        public TDSPOLLWAKEUP wakeup;
        public TDSCONTEXT tds_ctx;

        /* environment is shared between all sessions */
        public TDSENV env;

        /*
         * linked list of cursors allocated for this connection
         * contains only cursors allocated on the server
         */
        public TDSCURSOR cursors;
        /*
         * list of dynamic allocated for this connection
         * contains only dynamic allocated on the server
         */
        public TDSDYNAMIC dyns;

        public int char_conv_count;
        public TDSICONV[] char_convs;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)] public string collation;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public string tds72_transaction;

        public TDS_CAPABILITIES capabilities;
        uint _data_;
        public uint emul_little_endian => 0; ///:1;
        public uint use_iconv => 0; ///:1;
        public uint tds71rev1 => 0; ///:1;
        public uint pending_close => 0; ///:1;   /*< true is connection has pending closing (cursors or dynamic) */
        public uint encrypt_single_packet => 0; ///:1;
#if ENABLE_ODBC_MARS
        public uint mars => 0; ///:1;

        public TDSSOCKET in_net_tds;
        public TDSPACKET packets;
        public TDSPACKET recv_packet;
        public TDSPACKET send_packets;
        public uint send_pos, recv_pos;

        public tds_mutex list_mtx;
        //#define BUSY_SOCKET ((TDSSOCKET*)(TDS_UINTPTR)1)
        //#define TDSSOCKET_VALID(tds) (((TDS_UINTPTR)(tds)) > 1)
        public tds_socket[] sessions;
        public uint num_sessions;
        public uint num_cached_packets;
        public TDSPACKET packet_cache;
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
        TDSAUTHENTICATION authentication;
        [MarshalAs(UnmanagedType.LPTStr)] public string server;
    }

    /// <summary>
    /// Information for a server connection
    /// </summary>
    public struct TDSSOCKET
    {
#if ENABLE_ODBC_MARS
        public TDSCONNECTION conn;
#else
        public TDSCONNECTION conn;
#endif

        /* Input buffer.
         * Points to receiving packet buffer.
         * As input buffer contains just the raw packet actually this pointer
         * is the address of recv_packet->buf.
         */
        public byte[] in_buf;

        /* Output buffer.
         * Points to sending packet buffer.
         * Output buffer can contain additional data before the raw TDS packet
         * so this buffer can point some bytes after send_packet->buf.
         */
        public byte[] out_buf;

        /* Maximum size of packet pointed by out_buf.
         * The buffer is actually a bit larger to make possible to do some
         * optimizations (at least TDS_ADDITIONAL_SPACE bytes).
         */
        public uint out_buf_max;
        public uint in_pos;        /*< current position in in_buf */
        public uint out_pos;       /*< current position in out_buf */
        public uint in_len;        /*< input buffer length */
        public byte in_flag;      /*< input buffer type */
        public byte out_flag;     /*< output buffer type */

        public IntPtr parent;

#if ENABLE_ODBC_MARS
        public short sid;
        public tds_condition packet_cond;
        public uint recv_seq;
        public uint send_seq;
        public uint recv_wnd;
        public uint send_wnd;
#endif
        /* packet we received */
        public TDSPACKET recv_packet;
        /* packet we are preparing to send */
        public TDSPACKET send_packet;

        /*
         * Current query information. 
         * Contains information in process, both normal and compute results.
         * This pointer shouldn't be freed; it's just an alias to another structure.
         */
        public TDSRESULTINFO current_results;
        public TDSRESULTINFO res_info;
        public uint num_comp_info;
        public TDSCOMPUTEINFO[] comp_info;
        public TDSPARAMINFO param_info;
        public TDSCURSOR cur_cursor;      /*< cursor in use */
        public bool bulk_query;        /*< true is query sent was a bulk query so we need to switch state to QUERYING */
        public bool has_status;        /*< true is ret_status is valid */
        public bool in_row;            /*< true if we are getting rows */
        public int ret_status;         /*< return status from store procedure */
        public TDS_STATE state;
        public volatile byte in_cancel;    /*< indicate we are waiting a cancel reply; discard tokens till acknowledge; 
	1 mean we have to send cancel packet, 2 already sent. */

        public byte rows_affected;     /*< rows updated/deleted/inserted/selected, TDS_NO_COUNT if not valid */
        public int query_timeout;

        public TDSDYNAMIC cur_dyn;        /*< dynamic structure in use */

        public TDSLOGIN login;    /*< config for login stuff. After login this field is NULL */

        public IntPtr env_chg_func;
        public TDS_OPERATION current_op;

        public int option_value;
        public tds_mutex wire_mtx;
    }

    partial class G
    {
        public static TDSCONTEXT tds_get_ctx(this TDSSOCKET tds) => tds.conn.tds_ctx;
        public static void tds_set_ctx(this TDSSOCKET tds, TDSCONTEXT val) => tds.conn.tds_ctx = val;
        public static TDSSOCKET tds_get_parent(this TDSSOCKET tds) => (TDSSOCKET)tds.parent;
        public static void tds_set_parent(this TDSSOCKET tds, TDSSOCKET val) => tds.parent = val;
        public static TDSCONNECTION tds_get_s(this TDSSOCKET tds) => tds.conn.s;
        public static void tds_set_s(this TDSSOCKET tds, TDSCONNECTION val) => tds.conn.s = val;
    }

    public static class NativeMethods
    {
        internal const string LibraryName = "tds.dll";

        /// <summary>
        /// Extracts the tag name from the original_text field of an element or token by
        /// stripping off &lt;/&gt; characters and attributes and adjusting the passed-in
        /// GumboStringPiece appropriately.  The tag name is in the original case and
        /// shares a buffer with the original text, to simplify memory management.
        /// Behavior is undefined if a string-piece that doesn't represent an HTML tag
        /// (&lt;tagname&gt; or &lt;/tagname&gt;) is passed in. If the string piece is completely
        /// empty (NULL data pointer), then this function will exit successfully as a
        /// no-op.
        /// </summary>
        /// <param name="text"></param>
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void gumbo_tag_from_original_text(ref GumboStringPiece text);

        /// <summary>
        /// Returns the normalized (usually all-lowercased, except for foreign content)
        /// tag name for an GumboTag enum.  Return value is static data owned by the
        /// library.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr gumbo_normalized_tagname(GumboTag tag);

        /// <summary>
        /// Parses a buffer of UTF8 text into an GumboNode parse tree.  The buffer must
        /// live at least as long as the parse tree, as some fields (eg. original_text)
        /// point directly into the original buffer.
        /// </summary>
        /// <remarks>
        /// This doesn't support buffers longer than 4 gigabytes.
        /// </remarks>
        /// <param name="buffer"></param>
        /// <returns></returns>
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr gumbo_parse(IntPtr buffer);

        /// <summary>
        /// Extended version of <see cref="gumbo_parse"/> that takes an explicit options structure,
        /// buffer, and length.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="buffer"></param>
        /// <param name="buffer_length"></param>
        /// <returns></returns>
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr gumbo_parse_with_options(ref GumboOptions options, IntPtr buffer, [MarshalAs(UnmanagedType.SysUInt)] uint buffer_length);

        /// <summary>
        /// Release the memory used for the parse tree and parse errors.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="output"></param>
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)] public static extern void gumbo_destroy_output(ref GumboOptions options, IntPtr output);
    }
}