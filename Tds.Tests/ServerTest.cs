using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreeTds
{
    public class ServerTest
    {
        [Test]
        public void TestServer()
        {
            Tds.DumpOpen(@"C:\T_\dump.log");
            void dump_login(TdsLogin login)
            {
                Console.WriteLine("host {0}", login.ClientHostName);
                Console.WriteLine("user {0}", login.UserName);
                Console.WriteLine("pass {0}", login.Password);
                Console.WriteLine("app  {0}", login.AppName);
                Console.WriteLine("srvr {0}", login.ServerName);
                Console.WriteLine("vers {0}.{0}", login.TdsMajor(), login.TdsMinor());
                Console.WriteLine("lib  {0}", login.Library);
                Console.WriteLine("lang {0}", login.Language);
                Console.WriteLine("char {0}", login.ServerCharset);
                Console.WriteLine("bsiz {0}", login.BlockSize);
            }

            var connection = Task.Run(() =>
            {
                //var login = new TdsLogin(false);
                //var ret = Common.try_tds_login(ref login, out var tds);
                //if (ret != G.TDS_SUCCESS)
                //    throw new Exception("try_tds_login() failed");
                //Common.run_query(tds, "Select * From Test;");
                //Common.try_tds_logout(login, tds);

                using (var conn = new SqlConnection("Data Source=tcp:localhost,1433;Initial Catalog=AdventureWorks;MultipleActiveResultSets=True;user=guest;pwd=sybase;Encrypt=false;trustservercertificate=false"))
                using (var com = new SqlCommand("Select * From Table", conn))
                {
                    conn.Open();
                    com.ExecuteNonQuery();
                }
            });

            //NativeMethods.tdsdump_log(@"C:\T_\dump.log", 1, $"A0: {tds.Value.state}\n");
            using (var ctx = new TdsContext())
            {
                //ctx.MsgHandler = (a, b, c) =>
                //{
                //    return 0;
                //};
                //ctx.ErrHandler = (a, b, c) =>
                //{
                //    return 0;
                //};
                //ctx.IntHandler = (a) =>
                //{
                //    return 0;
                //};
                var tds = ctx.Listen() ?? throw new Exception("Error Listening");
                using (var login = tds.AllocReadLogin() ?? throw new Exception("Error reading login"))
                {
                    tds.Conn.ProductName = "Microsoft SQL Server";
                    tds.Conn.ProductVersion = G.TDS_MS_VER(7, 0, 0);
                    dump_login(login);
                    if (login.UserName == "guest" && login.Password == "sybase")
                    {
                        tds.OutFlag = TDS_PACKET_TYPE.TDS_REPLY;
                        tds.EnvChange(P.TDS_ENV_DATABASE, "master", "pubs2");
                        tds.SendMsg(5701, 2, 10, "Changed database context to 'pubs2'.", "JDBC", "ZZZZZ", 1);
                        if (!login.Value.suppress_language)
                        {
                            tds.EnvChange(P.TDS_ENV_LANG, null, "us_english");
                            tds.SendMsg(5703, 1, 10, "Changed language setting to 'us_english'.", "JDBC", "ZZZZZ", 1);
                        }
                        tds.EnvChange(P.TDS_ENV_PACKSIZE, null, "512");
                        //* TODO set mssql if tds7+ */
                        tds.SendLoginAck("sql server");
                        if (G.IS_TDS50(tds.Conn.Value))
                            tds.SendCapabilitiesToken();
                        tds.SendDoneToken(0, 1);
                    }
                    else
                        return; // send nack before exiting
                    tds.FlushPacket();
                }
                var query = tds.GetGenericQuery();
                Console.WriteLine("query : {0}", query);
                tds.OutFlag = TDS_PACKET_TYPE.TDS_REPLY;
                using (var resinfo = new TdsResultInfo(1))
                {
                    resinfo.CurrentRow = Marshal.StringToHGlobalAnsi("pubs2");
                    resinfo.Columns[0] = new TdsColumn
                    {
                        ColumnType = TDS_SERVER_TYPE.SYBVARCHAR,
                        ColumnSize = 30,
                        ColumnName = "name",
                        ColumnData = resinfo.CurrentRow,
                    };
                    tds.SendResult(resinfo);
                    tds.SendControlToken(1);
                    tds.SendRow(resinfo);
                    tds.SendDoneToken(16, 1);
                    tds.FlushPacket();
                    Thread.Sleep(30 * 1000);
                }
            }
        }
    }
}
