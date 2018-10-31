using Microsoft.VisualStudio.TestTools.UnitTesting;
using _1530Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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