using NUnit.Framework;
using System;
using System.Data.SqlClient;
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
                Console.WriteLine("host {0}", login.client_host_name);
                Console.WriteLine("user {0}", login.user_name);
                Console.WriteLine("pass {0}", login.password);
                Console.WriteLine("app  {0}", login.app_name);
                Console.WriteLine("srvr {0}", login.server_name);
                Console.WriteLine("vers {0}.{0}", G.TDS_MAJOR(login), G.TDS_MINOR(login));
                Console.WriteLine("lib  {0}", login.library);
                Console.WriteLine("lang {0}", login.language);
                Console.WriteLine("char {0}", login.server_charset);
                Console.WriteLine("bsiz {0}", login.block_size);
            }

            var connection = Task.Run(() =>
            {
                using (var conn = new SqlConnection("Server=tcp:localhost,1433;user=guest;pwd=sysbase"))
                using (var com = new SqlCommand("Select * From Table", conn))
                {
                    conn.Open();
                    com.ExecuteNonQuery();
                }
            });
            //
            using (var ctx = new TdsContext())
            {
                var tds = ctx.Listen(1433);
                if (tds == null)
                    return;
                var tdsValue = tds.Value;
                //get_incoming(tdsValue.s);
                using (var login = tds.AllocReadLogin())
                {
                    if (login == null)
                        throw new Exception("Error reading login");
                    var loginValue = login.Value;
                    dump_login(ref loginValue);
                    if (loginValue.user_name == "guest" && loginValue.password == "sybase")
                    {
                        tds.out_flag = TDS_PACKET_TYPE.TDS_REPLY;
                        tds.EnvChange(P.TDS_ENV_DATABASE, "master", "pubs2");
                        tds.SendMsg(5701, 2, 10, "Changed database context to 'pubs2'.", "JDBC", "ZZZZZ", 1);
                        if (!loginValue.suppress_language)
                        {
                            tds.EnvChange(P.TDS_ENV_LANG, null, "us_english");
                            tds.SendMsg(5703, 1, 10, "Changed language setting to 'us_english'.", "JDBC", "ZZZZZ", 1);
                        }
                        tds.EnvChange(P.TDS_ENV_PACKSIZE, null, "512");
                        //* TODO set mssql if tds7+ */
                        tds.SendLoginAck("sql server");
                        //if (G.IS_TDS50(tds.Value.conn))
                        //    tds.SendCapabilitiesToken();
                        tds.SendDoneToken(0, 1);
                    }
                    else
                        return; // send nack before exiting
                    tds.FlushPacket();
                }
                /* printf("incoming packet %d\n", tds_read_packet(tds)); */
                Console.WriteLine("query : {0}", tds.GetGenericQuery());
                tds.out_flag = TDS_PACKET_TYPE.TDS_REPLY;
                //    resinfo = tds_alloc_results(1);
                //    resinfo->columns[0]->column_type = SYBVARCHAR;
                //    resinfo->columns[0]->column_size = 30;
                //    if (!tds_dstr_copy(&resinfo->columns[0]->column_name, "name"))
                //        exit(1);
                //    resinfo->current_row = (TDS_UCHAR*)"pubs2";
                //    resinfo->columns[0]->column_data = resinfo->current_row;
                //    tds_send_result(tds, resinfo);
                //    tds_send_control_token(tds, 1);
                //    tds_send_row(tds, resinfo);
                //    tds_send_done_token(tds, 16, 1);
                //    tds_flush_packet(tds);
                //    tds_sleep_s(30);
                //    tds_free_results(resinfo);
                //    tds_free_socket(tds);
                //    tds_free_context(ctx);

            }
        }
    }
}
