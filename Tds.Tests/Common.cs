using System;
using TDSRET = System.Int32;

namespace FreeTds
{
    public class Common
    {
        static TdsContext test_context = null;
        static string USER = Environment.GetEnvironmentVariable("_TDSLOGIN")?.Split(":")[1];
        static string SERVER = Environment.GetEnvironmentVariable("_TDSLOGIN")?.Split(":")[0];
        static string PASSWORD = Environment.GetEnvironmentVariable("_TDSLOGIN")?.Split(":")[2];
        static string DATABASE = "";
        static string CHARSET = "ISO-8859-1";

        /// <summary>
        /// Run query for which there should be no return results
        /// </summary>
        /// <param name="tds"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static TDSRET run_query(TdsSocket tds, string query)
        {
            int result_type;
            var rc = tds.SubmitQuery(query);
            if (rc != G.TDS_SUCCESS)
            {
                Console.WriteLine("tds_submit_query() failed for query '{0}'", query);
                return G.TDS_FAIL;
            }

            while ((rc = tds.ProcessTokens(out result_type, out var dummy1, tds_token_flags.TDS_TOKEN_RESULTS)) == G.TDS_SUCCESS)
            {
                switch (result_type)
                {
                    case G.TDS_DONE_RESULT:
                    case G.TDS_DONEPROC_RESULT:
                    case G.TDS_DONEINPROC_RESULT:
                    // ignore possible spurious result (TDS7+ send it)
                    case G.TDS_STATUS_RESULT:
                        break;
                    default:
                        Console.WriteLine("Error:  query should not return results");
                        return G.TDS_FAIL;
                }
            }
            if (rc == G.TDS_FAIL)
            {
                Console.WriteLine("tds_process_tokens() returned TDS_FAIL for '{0}'", query);
                return G.TDS_FAIL;
            }
            else if (rc != G.TDS_NO_MORE_RESULTS)
            {
                Console.WriteLine("tds_process_tokens() unexpected return\n");
                return G.TDS_FAIL;
            }

            return G.TDS_SUCCESS;
        }

        public static TDSRET try_tds_login(ref TdsLogin login, out TdsSocket tds, bool verbose)
        {
            tds = null;
            if (!login.SetPasswd(PASSWORD)
                || !login.SetUser(USER)
                || !login.SetApp("app")
                || !login.SetHost("myhost")
                || !login.SetLibrary("TDS-Library")
                || !login.SetServer(SERVER)
                || !login.SetClientCharset("ISO-8859-1")
                || !login.SetLanguage("us_english"))
                return G.TDS_FAIL;
            if (verbose)
                Console.WriteLine("Connecting to database");
            test_context = new TdsContext();
            tds = test_context.AllocSocket(512);
            var connection = tds.ReadConfigInfo(ref login, test_context.Locale);
            if (connection == null || tds.ConnectAndLogin(connection) != G.TDS_SUCCESS)
            {
                if (connection != null)
                {
                    TdsContext.FreeSocket(tds);
                    tds = null;
                    Tds.FreeLogin(connection);
                }
                return G.TDS_FAIL;
            }
            Tds.FreeLogin(connection);
            return G.TDS_SUCCESS;
        }

        public static TDSRET try_tds_logout(TdsLogin login, TdsSocket tds, bool verbose)
        {
            if (verbose)
                Console.WriteLine("Entered tds_try_logout()");
            tds.CloseSocket();
            TdsContext.FreeSocket(tds);
            Tds.FreeLogin(login);
            Tds.FreeContext(test_context);
            test_context = null;
            return G.TDS_SUCCESS;
        }
    }
}
