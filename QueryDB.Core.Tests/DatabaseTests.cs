using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QueryDB.Core.Tests
{
    [TestClass]
    public class DatabaseTests : TestBase
    {

        #region Oracle DB Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS), TestCategory(SMOKE_TESTS)]
        public void Test_Oracle_FetchData()
        {
            var selectSql = Queries.OracleQuery.SelectSql;
            var data = new DBContext(DB.Oracle, OracleDatabaseString).FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("oracle", data[0].ReferenceData["CURRENT_DATABASE"]);

            var dbContext = new DBContext(DB.Oracle, OracleDatabaseString);
            data = dbContext.FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("oracle", data[0].ReferenceData["CURRENT_DATABASE"]);
        }

        #endregion

        #region MSSQL DB Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS), TestCategory(SMOKE_TESTS)]
        public void Test_MSSQL_FetchData()
        {
            var selectSql = Queries.SqlServerQuery.SelectSql;
            var data = new DBContext(DB.MSSQL, SqlServerDatabaseString).FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mssql", data[0].ReferenceData["current_database"]);

            var dbContext = new DBContext(DB.MSSQL, SqlServerDatabaseString);
            data = dbContext.FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mssql", data[0].ReferenceData["current_database"]);
        }

        #endregion

        #region MySQL DB Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS), TestCategory(SMOKE_TESTS)]
        public void Test_MySQL_FetchData()
        {
            var selectSql = Queries.MySqlQuery.SelectSql;
            var data = new DBContext(DB.MySQL, MySqlDatabaseString).FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mysql", data[0].ReferenceData["current_database"]);

            var dbContext = new DBContext(DB.MySQL, MySqlDatabaseString);
            data = dbContext.FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mysql", data[0].ReferenceData["current_database"]);
        }

        #endregion
    }
}
