using NUnit.Framework;

namespace FreeTds
{
    public class TdsTest
    {
        [Test]
        public void TestCompiletimeSettings()
        {
            var settings = Tds.GetCompiletimeSettings();
            Assert.AreEqual("auto", settings.tdsver);
        }
    }
}
