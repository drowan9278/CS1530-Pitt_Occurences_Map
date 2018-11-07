using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _1530Application.Tests
{
    [TestClass()]
    public class DbConnectionTests
    {
        [TestMethod()]
        public void DbConnectionTest()
        {
            DbConnection connection = new DbConnection();
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void mainTest()
        {
            Assert.Fail();
        }
    }
}