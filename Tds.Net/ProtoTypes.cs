using System;
using System.Runtime.InteropServices;

namespace FreeTds
{
    /// <summary>
    /// This file contains defines and structures strictly related to TDS protocol
    /// </summary>
    public static partial class P { }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct TDS_NUMERIC
    {
        public byte precision;
        public byte scale;
        public fixed byte array[33];
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDS_OLD_MONEY
    {
        public int mnyhigh;
        public uint mnylow;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct TDS_MONEY
    {
        [FieldOffset(0)]
        public TDS_OLD_MONEY tdsoldmoney;
        [FieldOffset(0)]
        public long mny;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDS_MONEY4
    {
        public int mny4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDS_DATETIME
    {
        public int dtdays;
        public int dttime;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TDS_DATETIME4
    {
        public ushort days;
        public ushort minutes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct TDS_UNIQUE
    {
        public uint Data1;
        public ushort Data2;
        public ushort Data3;
        public fixed byte Data4[8];
    }

    partial class P
    {
        //typedef int TDS_DATE;
        //typedef int TDS_TIME;

        //typedef long TDS_BIGTIME;
        //typedef long TDS_BIGDATETIME;

        public const int TDS5_PARAMFMT2_TOKEN = 32;     /* 0x20 */
        public const int TDS_LANGUAGE_TOKEN = 33;       /* 0x21    TDS 5.0 only              */
        public const int TDS_ORDERBY2_TOKEN = 34;       /* 0x22 */
        public const int TDS_ROWFMT2_TOKEN = 97;        /* 0x61    TDS 5.0 only              */
        public const int TDS_MSG_TOKEN = 101;           /* 0x65    TDS 5.0 only              */
        public const int TDS_LOGOUT_TOKEN = 113;        /* 0x71    TDS 5.0 only? ct_close()  */
        public const int TDS_RETURNSTATUS_TOKEN = 121;  /* 0x79                              */
        public const int TDS_PROCID_TOKEN = 124;        /* 0x7C    TDS 4.2 only - TDS_PROCID */
        public const int TDS7_RESULT_TOKEN = 129;       /* 0x81    TDS 7.0 only              */
        public const int TDS7_COMPUTE_RESULT_TOKEN = 136;   /* 0x88    TDS 7.0 only              */
        public const int TDS_COLNAME_TOKEN = 160;       /* 0xA0    TDS 4.2 only              */
        public const int TDS_COLFMT_TOKEN = 161;        /* 0xA1    TDS 4.2 only - TDS_COLFMT */
        public const int TDS_DYNAMIC2_TOKEN = 163;      /* 0xA3 */
        public const int TDS_TABNAME_TOKEN = 164;       /* 0xA4 */
        public const int TDS_COLINFO_TOKEN = 165;       /* 0xA5 */
        public const int TDS_OPTIONCMD_TOKEN = 166;     /* 0xA6 */
        public const int TDS_COMPUTE_NAMES_TOKEN = 167; /* 0xA7 */
        public const int TDS_COMPUTE_RESULT_TOKEN = 168; /* 0xA8 */
        public const int TDS_ORDERBY_TOKEN = 169;       /* 0xA9    TDS_ORDER                 */
        public const int TDS_ERROR_TOKEN = 170;         /* 0xAA                              */
        public const int TDS_INFO_TOKEN = 171;          /* 0xAB                              */
        public const int TDS_PARAM_TOKEN = 172;         /* 0xAC    RETURNVALUE?              */
        public const int TDS_LOGINACK_TOKEN = 173;      /* 0xAD                              */
        public const int TDS_CONTROL_FEATUREEXTACK_TOKEN = 174; /* 0xAE    TDS_CONTROL/TDS_FEATUREEXTACK */
        public const int TDS_ROW_TOKEN = 209;           /* 0xD1                              */
        public const int TDS_NBC_ROW_TOKEN = 210;       /* 0xD2    as of TDS 7.3.B           */
        public const int TDS_CMP_ROW_TOKEN = 211;       /* 0xD3                              */
        public const int TDS5_PARAMS_TOKEN = 215;       /* 0xD7    TDS 5.0 only              */
        public const int TDS_CAPABILITY_TOKEN = 226;    /* 0xE2                              */
        public const int TDS_ENVCHANGE_TOKEN = 227;     /* 0xE3                              */
        public const int TDS_SESSIONSTATE_TOKEN = 228;  /* 0xE4    TDS 7.4                   */
        public const int TDS_EED_TOKEN = 229;           /* 0xE5                              */
        public const int TDS_DBRPC_TOKEN = 230;         /* 0xE6    TDS 5.0 only              */
        public const int TDS5_DYNAMIC_TOKEN = 231;      /* 0xE7    TDS 5.0 only              */
        public const int TDS5_PARAMFMT_TOKEN = 236;     /* 0xEC    TDS 5.0 only              */
        public const int TDS_AUTH_TOKEN = 237;          /* 0xED    TDS 7.0 only              */
        public const int TDS_RESULT_TOKEN = 238;        /* 0xEE                              */
        public const int TDS_DONE_TOKEN = 253;          /* 0xFD    TDS_DONE                  */
        public const int TDS_DONEPROC_TOKEN = 254;      /* 0xFE    TDS_DONEPROC              */
        public const int TDS_DONEINPROC_TOKEN = 255;    /* 0xFF    TDS_DONEINPROC            */

        /* CURSOR support: TDS 5.0 only*/
        public const int TDS_CURCLOSE_TOKEN = 128;      /* 0x80    TDS 5.0 only              */
        public const int TDS_CURDELETE_TOKEN = 129;     /* 0x81    TDS 5.0 only              */
        public const int TDS_CURFETCH_TOKEN = 130;      /* 0x82    TDS 5.0 only              */
        public const int TDS_CURINFO_TOKEN = 131;       /* 0x83    TDS 5.0 only              */
        public const int TDS_CUROPEN_TOKEN = 132;       /* 0x84    TDS 5.0 only              */
        public const int TDS_CURDECLARE_TOKEN = 134;    /* 0x86    TDS 5.0 only              */

        /* environment type field */
        public const int TDS_ENV_DATABASE = 1;
        public const int TDS_ENV_LANG = 2;
        public const int TDS_ENV_CHARSET = 3;
        public const int TDS_ENV_PACKSIZE = 4;
        public const int TDS_ENV_LCID = 5;
        public const int TDS_ENV_SQLCOLLATION = 7;
        public const int TDS_ENV_BEGINTRANS = 8;
        public const int TDS_ENV_COMMITTRANS = 9;
        public const int TDS_ENV_ROLLBACKTRANS = 10;
        public const int TDS_ENV_ROUTING = 20;

        /* Microsoft internal stored procedure id's */
        public const int TDS_SP_CURSOR = 1;
        public const int TDS_SP_CURSOROPEN = 2;
        public const int TDS_SP_CURSORPREPARE = 3;
        public const int TDS_SP_CURSOREXECUTE = 4;
        public const int TDS_SP_CURSORPREPEXEC = 5;
        public const int TDS_SP_CURSORUNPREPARE = 6;
        public const int TDS_SP_CURSORFETCH = 7;
        public const int TDS_SP_CURSOROPTION = 8;
        public const int TDS_SP_CURSORCLOSE = 9;
        public const int TDS_SP_EXECUTESQL = 10;
        public const int TDS_SP_PREPARE = 11;
        public const int TDS_SP_EXECUTE = 12;
        public const int TDS_SP_PREPEXEC = 13;
        public const int TDS_SP_PREPEXECRPC = 14;
        public const int TDS_SP_UNPREPARE = 15;
    }

    /// <summary>
    /// TDS_SERVER_TYPE
    /// </summary>
    /// <rant>Sybase does an awful job of this stuff, non null ints of size 1 2  and 4 have there own codes but nullable ints are lumped into INTN sheesh!</rant>
    public enum TDS_SERVER_TYPE
    {
        SYBCHAR = 47,       /* 0x2F */
        SYBVARCHAR = 39,    /* 0x27 */
        SYBINTN = 38,       /* 0x26 */
        SYBINT1 = 48,       /* 0x30 */
        SYBINT2 = 52,       /* 0x34 */
        SYBINT4 = 56,       /* 0x38 */
        SYBFLT8 = 62,       /* 0x3E */
        SYBDATETIME = 61,   /* 0x3D */
        SYBBIT = 50,        /* 0x32 */
        SYBTEXT = 35,       /* 0x23 */
        SYBNTEXT = 99,      /* 0x63 */
        SYBIMAGE = 34,      /* 0x22 */
        SYBMONEY4 = 122,    /* 0x7A */
        SYBMONEY = 60,      /* 0x3C */
        SYBDATETIME4 = 58,  /* 0x3A */
        SYBREAL = 59,       /* 0x3B */
        SYBBINARY = 45,     /* 0x2D */
        SYBVOID = 31,       /* 0x1F */
        SYBVARBINARY = 37,  /* 0x25 */
        SYBBITN = 104,      /* 0x68 */
        SYBNUMERIC = 108,   /* 0x6C */
        SYBDECIMAL = 106,   /* 0x6A */
        SYBFLTN = 109,      /* 0x6D */
        SYBMONEYN = 110,    /* 0x6E */
        SYBDATETIMN = 111,  /* 0x6F */

        /* MS only types */
        SYBNVARCHAR = 103,  /* 0x67 */
        SYBINT8 = 127,      /* 0x7F */
        XSYBCHAR = 175,     /* 0xAF */
        XSYBVARCHAR = 167,  /* 0xA7 */
        XSYBNVARCHAR = 231, /* 0xE7 */
        XSYBNCHAR = 239,    /* 0xEF */
        XSYBVARBINARY = 165,    /* 0xA5 */
        XSYBBINARY = 173,   /* 0xAD */
        SYBUNIQUE = 36,     /* 0x24 */
        SYBVARIANT = 98,    /* 0x62 */
        SYBMSUDT = 240,     /* 0xF0 */
        SYBMSXML = 241,     /* 0xF1 */
        SYBMSDATE = 40,     /* 0x28 */
        SYBMSTIME = 41,     /* 0x29 */
        SYBMSDATETIME2 = 42,    /* 0x2a */
        SYBMSDATETIMEOFFSET = 43,/* 0x2b */

        /* Sybase only types */
        SYBLONGBINARY = 225,    /* 0xE1 */
        SYBUINT1 = 64,      /* 0x40 */
        SYBUINT2 = 65,      /* 0x41 */
        SYBUINT4 = 66,      /* 0x42 */
        SYBUINT8 = 67,      /* 0x43 */
        SYBBLOB = 36,       /* 0x24 */
        SYBBOUNDARY = 104,  /* 0x68 */
        SYBDATE = 49,       /* 0x31 */
        SYBDATEN = 123,     /* 0x7B */
        SYB5INT8 = 191,     /* 0xBF */
        SYBINTERVAL = 46,   /* 0x2E */
        SYBLONGCHAR = 175,  /* 0xAF */
        SYBSENSITIVITY = 103,   /* 0x67 */
        SYBSINT1 = 176,     /* 0xB0 */
        SYBTIME = 51,       /* 0x33 */
        SYBTIMEN = 147,     /* 0x93 */
        SYBUINTN = 68,      /* 0x44 */
        SYBUNITEXT = 174,   /* 0xAE */
        SYBXML = 163,       /* 0xA3 */
        SYB5BIGDATETIME = 187,  /* 0xBB */
        SYB5BIGTIME = 188,  /* 0xBC */
    }

    public enum TDS_USER_TYPE
    {
        USER_UNICHAR_TYPE = 34,     /* 0x22 */
        USER_UNIVARCHAR_TYPE = 35   /* 0x23 */
    }

    partial class P
    {
        /* compute operator */
        public const int SYBAOPCNT = 75;        /* 0x4B */
        public const int SYBAOPCNTU = 76;       /* 0x4C, obsolete */
        public const int SYBAOPSUM = 77;        /* 0x4D */
        public const int SYBAOPSUMU = 78;       /* 0x4E, obsolete */
        public const int SYBAOPAVG = 79;        /* 0x4F */
        public const int SYBAOPAVGU = 80;       /* 0x50, obsolete */
        public const int SYBAOPMIN = 81;        /* 0x51 */
        public const int SYBAOPMAX = 82;        /* 0x52 */

        /* mssql2k compute operator */
        public const int SYBAOPCNT_BIG = 9; /* 0x09 */
        public const int SYBAOPSTDEV = 48;  /* 0x30 */
        public const int SYBAOPSTDEVP = 49; /* 0x31 */
        public const int SYBAOPVAR = 50;    /* 0x32 */
        public const int SYBAOPVARP = 51;   /* 0x33 */
        public const int SYBAOPCHECKSUM_AGG = 114;  /* 0x72 */
    }

    /// <summary>
    /// options that can be sent with a TDS_OPTIONCMD token
    /// </summary>
    public enum TDS_OPTION_CMD
    {
        /// <summary>
        /// Set an option.
        /// </summary>
        TDS_OPT_SET = 1,
        /// <summary>
        /// Set option to its default value.
        /// </summary>
        TDS_OPT_DEFAULT = 2,
        /// <summary>
        /// Request current setting of a specific option.
        /// </summary>
        TDS_OPT_LIST = 3,
        /// <summary>
        /// Report current setting of a specific option.
        /// </summary>
        TDS_OPT_INFO = 4,
    }

    public enum TDS_OPTION
    {
        TDS_OPT_DATEFIRST = 1,       /* 0x01 */
        TDS_OPT_TEXTSIZE = 2,      /* 0x02 */
        TDS_OPT_STAT_TIME = 3,     /* 0x03 */
        TDS_OPT_STAT_IO = 4,       /* 0x04 */
        TDS_OPT_ROWCOUNT = 5,      /* 0x05 */
        TDS_OPT_NATLANG = 6,       /* 0x06 */
        TDS_OPT_DATEFORMAT = 7,    /* 0x07 */
        TDS_OPT_ISOLATION = 8,     /* 0x08 */
        TDS_OPT_AUTHON = 9,        /* 0x09 */
        TDS_OPT_CHARSET = 10,      /* 0x0a */
        TDS_OPT_SHOWPLAN = 13,     /* 0x0d */
        TDS_OPT_NOEXEC = 14,       /* 0x0e */
        TDS_OPT_ARITHIGNOREON = 15,    /* 0x0f */
        TDS_OPT_ARITHABORTON = 17, /* 0x11 */
        TDS_OPT_PARSEONLY = 18,    /* 0x12 */
        TDS_OPT_GETDATA = 20,      /* 0x14 */
        TDS_OPT_NOCOUNT = 21,      /* 0x15 */
        TDS_OPT_FORCEPLAN = 23,    /* 0x17 */
        TDS_OPT_FORMATONLY = 24,   /* 0x18 */
        TDS_OPT_CHAINXACTS = 25,   /* 0x19 */
        TDS_OPT_CURCLOSEONXACT = 26,   /* 0x1a */
        TDS_OPT_FIPSFLAG = 27,     /* 0x1b */
        TDS_OPT_RESTREES = 28,     /* 0x1c */
        TDS_OPT_IDENTITYON = 29,   /* 0x1d */
        TDS_OPT_CURREAD = 30,      /* 0x1e */
        TDS_OPT_CURWRITE = 31,     /* 0x1f */
        TDS_OPT_IDENTITYOFF = 32,  /* 0x20 */
        TDS_OPT_AUTHOFF = 33,      /* 0x21 */
        TDS_OPT_ANSINULL = 34,     /* 0x22 */
        TDS_OPT_QUOTED_IDENT = 35, /* 0x23 */
        TDS_OPT_ARITHIGNOREOFF = 36,   /* 0x24 */
        TDS_OPT_ARITHABORTOFF = 37,    /* 0x25 */
        TDS_OPT_TRUNCABORT = 38,   /* 0x26 */
    }

    public enum _OPT
    {
        TDS_OPT_ARITHOVERFLOW = 0x01,
        TDS_OPT_NUMERICTRUNC = 0x02
    }

    public enum TDS_OPT_DATEFIRST_CHOICE
    {
        TDS_OPT_MONDAY = 1, TDS_OPT_TUESDAY = 2, TDS_OPT_WEDNESDAY = 3, TDS_OPT_THURSDAY = 4, TDS_OPT_FRIDAY = 5, TDS_OPT_SATURDAY = 6, TDS_OPT_SUNDAY = 7
    }

    public enum TDS_OPT_DATEFORMAT_CHOICE
    {
        TDS_OPT_FMTMDY = 1, TDS_OPT_FMTDMY = 2, TDS_OPT_FMTYMD = 3, TDS_OPT_FMTYDM = 4, TDS_OPT_FMTMYD = 5, TDS_OPT_FMTDYM = 6
    }

    public enum TDS_OPT_ISOLATION_CHOICE
    {
        TDS_OPT_LEVEL0 = 0,
        TDS_OPT_LEVEL1 = 1,
        TDS_OPT_LEVEL2 = 2,
        TDS_OPT_LEVEL3 = 3
    }

    public enum TDS_PACKET_TYPE : byte
    {
        TDS_QUERY = 1,
        TDS_LOGIN = 2,
        TDS_RPC = 3,
        TDS_REPLY = 4,
        TDS_CANCEL = 6,
        TDS_BULK = 7,
        TDS7_TRANS = 14,    /* transaction management */
        TDS_NORMAL = 15,
        TDS7_LOGIN = 16,
        TDS7_AUTH = 17,
        TDS71_PRELOGIN = 18,
        TDS72_SMP = 0x53
    }

    /// <summary>
    /// TDS 7.1 collation informations.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TDS71_COLLATION
    {
        public ushort locale_id;    /* master..syslanguages.lcid */
        public ushort flags;
        public byte charset_id;     /* or zero */
    }

    /// <summary>
    /// TDS 7.2 SMP packet header
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TDS72_SMP_HEADER
    {
        public byte signature;    /* TDS72_SMP */
        public byte type;
        public ushort sid;
        public uint size;
        public uint seq;
        public uint wnd;
    }

    public enum _SMP
    {
        TDS_SMP_SYN = 1,
        TDS_SMP_ACK = 2,
        TDS_SMP_FIN = 4,
        TDS_SMP_DATA = 8,
    }

    partial class P
    {
        /* SF stands for "sort flag" */
        public const ushort TDS_SF_BIN = (ushort)0x100;
        public const ushort TDS_SF_WIDTH_INSENSITIVE = (ushort)0x080;
        public const ushort TDS_SF_KATATYPE_INSENSITIVE = (ushort)0x040;
        public const ushort TDS_SF_ACCENT_SENSITIVE = (ushort)0x020;
        public const ushort TDS_SF_CASE_INSENSITIVE = (ushort)0x010;

        /* UT stands for user type */
        public const int TDS_UT_TIMESTAMP = 80;
    }

    /// <summary>
    /// mssql login options flags
    /// </summary>
    [Flags]
    public enum option_flag1_values : byte
    {
        TDS_BYTE_ORDER_X86 = 0,
        TDS_CHARSET_ASCII = 0,
        TDS_DUMPLOAD_ON = 0,
        TDS_FLOAT_IEEE_754 = 0,
        TDS_INIT_DB_WARN = 0,
        TDS_SET_LANG_OFF = 0,
        TDS_USE_DB_SILENT = 0,
        TDS_BYTE_ORDER_68000 = 0x01,
        TDS_CHARSET_EBDDIC = 0x02,
        TDS_FLOAT_VAX = 0x04,
        TDS_FLOAT_ND5000 = 0x08,
        /// <summary>
        /// prevent BCP
        /// </summary>
        TDS_DUMPLOAD_OFF = 0x10,
        TDS_USE_DB_NOTIFY = 0x20,
        TDS_INIT_DB_FATAL = 0x40,
        TDS_SET_LANG_ON = 0x80
    }

    [Flags]
    public enum option_flag2_values : byte
    {
        TDS_INIT_LANG_WARN = 0,
        TDS_INTEGRATED_SECURTY_OFF = 0,
        TDS_ODBC_OFF = 0,
        /// <summary>
        /// SQL Server login
        /// </summary>
        TDS_USER_NORMAL = 0,
        TDS_INIT_LANG_REQUIRED = 0x01,
        TDS_ODBC_ON = 0x02,
        /// <summary>
        /// removed in TDS 7.2
        /// </summary>
        TDS_TRANSACTION_BOUNDARY71 = 0x04,
        /// <summary>
        /// removed in TDS 7.2
        /// </summary>
        TDS_CACHE_CONNECT71 = 0x08,
        /// <summary>
        /// reserved
        /// </summary>
        TDS_USER_SERVER = 0x10,
        /// <summary>
        /// DQ login
        /// </summary>
        TDS_USER_REMUSER = 0x20,
        /// <summary>
        /// replication login
        /// </summary>
        TDS_USER_SQLREPL = 0x40,
        TDS_INTEGRATED_SECURITY_ON = 0x80
    }

    public enum option_flag3_values
    {
        TDS_RESTRICTED_COLLATION = 0,
        TDS_CHANGE_PASSWORD = 0x01, /* TDS 7.2 */
        TDS_SEND_YUKON_BINARY_XML = 0x02, /* TDS 7.2 */
        TDS_REQUEST_USER_INSTANCE = 0x04, /* TDS 7.2 */
        TDS_UNKNOWN_COLLATION_HANDLING = 0x08, /* TDS 7.3 */
        TDS_EXTENSION = 0x10, /* TDS 7.4 */
    }

    public enum type_flags
    {
        TDS_OLEDB_ON = 0x10,
        TDS_READONLY_INTENT = 0x20,
    }

    /// <summary>
    /// Sybase dynamic types
    /// </summary>
    public enum dynamic_types
    {
        TDS_DYN_PREPARE = 0x01,
        TDS_DYN_EXEC = 0x02,
        TDS_DYN_DEALLOC = 0x04,
        TDS_DYN_EXEC_IMMED = 0x08,
        TDS_DYN_PROCNAME = 0x10,
        TDS_DYN_ACK = 0x20,
        TDS_DYN_DESCIN = 0x40,
        TDS_DYN_DESCOUT = 0x80,
    }

    /// <summary>
    /// http://jtds.sourceforge.net/apiCursors.html
    /// Cursor scroll option, must be one of 0x01 - 0x10, OR'd with other bits
    /// </summary>
    public enum _CUR_TYPE
    {
        TDS_CUR_TYPE_KEYSET = 0x0001, /* default */
        TDS_CUR_TYPE_DYNAMIC = 0x0002,
        TDS_CUR_TYPE_FORWARD = 0x0004,
        TDS_CUR_TYPE_STATIC = 0x0008,
        TDS_CUR_TYPE_FASTFORWARDONLY = 0x0010,
        TDS_CUR_TYPE_PARAMETERIZED = 0x1000,
        TDS_CUR_TYPE_AUTO_FETCH = 0x2000
    }

    public enum _CUR_CONCUR
    {
        TDS_CUR_CONCUR_READ_ONLY = 1,
        TDS_CUR_CONCUR_SCROLL_LOCKS = 2,
        TDS_CUR_CONCUR_OPTIMISTIC = 4, /* default */
        TDS_CUR_CONCUR_OPTIMISTIC_VALUES = 8
    }

    partial class P
    {
        /* TDS 4/5 login*/
        /// <summary>
        /// maximum login name lengths
        /// </summary>
        public const int TDS_MAXNAME = 30;
        /// <summary>
        /// maximum program length
        /// </summary>
        public const int TDS_PROGNLEN = 10;
        /// <summary>
        /// maximum packet length in login
        /// </summary>
        public const int TDS_PKTLEN = 6;
    }

    /* TDS 5 login security flags */
    public enum _SEC_LOG
    {
        TDS5_SEC_LOG_ENCRYPT = 1,
        TDS5_SEC_LOG_CHALLENGE = 2,
        TDS5_SEC_LOG_LABELS = 4,
        TDS5_SEC_LOG_APPDEFINED = 8,
        TDS5_SEC_LOG_SECSESS = 16,
        TDS5_SEC_LOG_ENCRYPT2 = 32,
        TDS5_SEC_LOG_NONCE = 128
    }

    /// <summary>
    /// MS encryption byte (pre login)
    /// </summary>
    public enum _ENCRYPT : byte
    {
        TDS7_ENCRYPT_OFF,
        TDS7_ENCRYPT_ON,
        TDS7_ENCRYPT_NOT_SUP,
        TDS7_ENCRYPT_REQ,
    }
}