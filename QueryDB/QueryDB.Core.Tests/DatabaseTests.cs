using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QueryDB.Core.Tests
{
    [TestClass]
    public class DatabaseTests : TestBase
    {

        [TestMethod]
        [TestCategory(DB_TESTS)]
        public void Test_Oracle_RetrieveData()
        {
            var selectSql = Queries.OracleQuery.SelectSql;
            var data = new DBContext(DB.Oracle, OracleDatabaseString).RetrieveData(selectSql);
            Assert.IsTrue(data.Count > 0);

            var dbContext = new DBContext(DB.Oracle, OracleDatabaseString);
            data = dbContext.RetrieveData(selectSql);
            Assert.IsTrue(data.Count > 0);
        }

        [TestMethod]
        [TestCategory(DB_TESTS)]
        public void Test_SqlServer_RetrieveData()
        {
            var selectSql = Queries.SqlServerQuery.SelectSql;
            var data = new DBContext(DB.SqlServer, SqlServerDatabaseString).RetrieveData(selectSql);
            Assert.IsTrue(data.Count > 0);

            var dbContext = new DBContext(DB.SqlServer, SqlServerDatabaseString);
            data = dbContext.RetrieveData(selectSql);
            Assert.IsTrue(data.Count > 0);
        }
    }
}
