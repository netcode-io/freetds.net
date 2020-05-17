using NUnit.Framework;
using System;

namespace FreeTds
{
    public class TdsTest
    {
        [SetUp]
        public void Setup()
        {
            Tds.Touch();
            //Tds.DumpOpen(@"C:\T_\dump.log");
        }

        [Test]
        public void TestCompiletimeSettings()
        {
            var settings = Tds.GetCompiletimeSettings();
            Assert.AreEqual("auto", settings.tdsver);
            Assert.AreEqual("freetds v1.1.36", settings.freetds_version);
        }

        /// <summary>
        /// Just connect to server and disconnect
        /// </summary>
        [Test]
        public void ConnectTest()
        {
            var verbose = true;
            var login = new TdsLogin(false);
            var ret = Common.try_tds_login(ref login, out var tds, verbose);
            if (ret != G.TDS_SUCCESS)
                throw new Exception("try_tds_login() failed");
            Common.try_tds_logout(login, tds, verbose);
        }

        [Test]
        public void QueryTest()
        {
            var verbose = true;
            var login = new TdsLogin(false);
            var ret = Common.try_tds_login(ref login, out var tds, verbose);
            if (ret != G.TDS_SUCCESS)
                throw new Exception("try_tds_login() failed");
            Common.run_query(tds, "Select * From Test;");
            Common.try_tds_logout(login, tds, verbose);
        }
    }
}
