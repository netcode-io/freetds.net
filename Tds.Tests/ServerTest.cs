using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace FreeTds
{
    public class ServerTest
    {
        [Test]
        public void TestServer()
        {
            void dump_login(ref TDSLOGIN login)
            {
                Console.WriteLine("host {0}", login.client_host_name__);
                Console.WriteLine("user {0}", login.user_name__);
                Console.WriteLine("pass {0}", login.password__);
                Console.WriteLine("app  {0}", login.app_name__);
                Console.WriteLine("srvr {0}", login.server_name__);
                Console.WriteLine("vers {0}.{0}", G.TDS_MAJOR(login), G.TDS_MINOR(login));
                Console.WriteLine("lib  {0}", login.library__);
                Console.WriteLine("lang {0}", login.language__);
                Console.WriteLine("char {0}", login.server_charset__);
                Console.WriteLine("bsiz {0}", login.block_size);
            }

            var connection = Task.Run(() =>
            {
                using (var conn = new SqlConnection("Data Source=tcp:localhost,1433;Initial Catalog=AdventureWorks;MultipleActiveResultSets=True;user=guest;pwd=sybase"))
                using (var com = new SqlCommand("Select * From Table", conn))
                {
                    conn.Open();
                    com.ExecuteNonQuery();
                }
            });
            //
            Tds.DumpOpen(@"C:\T_\dump.log");
            //NativeMethods.tdsdump_log(@"C:\T_\dump.log", 1, $"A0: {tds.Value.state}\n");
            using (var ctx = new TdsContext())
            {
                //var a = ctx.Value.err_handler;
                var tds = ctx.Listen() ?? throw new Exception("Error Listening");
                //get_incoming(tds.Value.s);
                using (var login = tds.AllocReadLogin() ?? throw new Exception("Error reading login"))
                {
                    var loginValue = login.Value;
                    dump_login(ref loginValue);
                    if (login.Value.user_name__ == "guest" && login.Value.password__ == "sybase")
                    {
                        tds.out_flag = TDS_PACKET_TYPE.TDS_REPLY;
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
                        if (G.IS_TDS50(tds.Value.conn__))
                            tds.SendCapabilitiesToken();
                        tds.SendDoneToken(0, 1);
                    }
                    else
                        return; // send nack before exiting
                    tds.FlushPacket();
                }
                /* printf("incoming packet %d\n", tds_read_packet(tds)); */
                var query = tds.GetGenericQuery();
                Console.WriteLine("query : {0}", query);
                tds.out_flag = TDS_PACKET_TYPE.TDS_REPLY;
                using (var resinfo = new TdsResultInfo(1))
                {
                    //resinfo.Columns[0].column_type = SYBVARCHAR;
                    //resinfo.Columns[0].column_size = 30;
                    //resinfo.Columns[0].column_name = "name";
                    //resinfo.CurrentRow = "pubs2";
                    //resinfo.Columns[0].column_data = resinfo.current_row;
                    //tds.SendResult(resinfo);
                    //tds.SendControlToken(1);
                    //tds.SendRow(resinfo);
                    //tds.SendDoneToken(16, 1);
                    //tds.FlushPacket();
                    //Thread.Sleep(30 * 1000);
                }
            }
        }
    }
}
