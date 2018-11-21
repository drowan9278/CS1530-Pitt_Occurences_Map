using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _1530Application.Tests
{
    [TestClass()]
    public class DbConnectionTests
    {
        [TestMethod()]
        public void DbConnectionTest()
        {
            DbConnection1530 connection = new DbConnection1530();
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void mainTest()
        {
            Assert.Fail();
        }
    }
}