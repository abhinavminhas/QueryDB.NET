using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QueryDB.Core.Tests
{
    [TestClass]
    public class DatabaseTests : TestBase
    {

        #region Oracle DB Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_LowerCaseKeys()
        {
            var selectSql = Queries.OracleQuery.SelectSql;
            var data = new DBContext(DB.Oracle, OracleDatabaseString).FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            System.Console.WriteLine(data[0].ReferenceData["current_database"]);

            var dbContext = new DBContext(DB.Oracle, OracleDatabaseString);
            data = dbContext.FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
        }

        #endregion

        #region MS SQL Server DB Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MS_SQL_SERVER_TESTS)]
        public void Test_SqlServer_FetchData_LowerCaseKeys()
        {
            var selectSql = Queries.SqlServerQuery.SelectSql;
            var data = new DBContext(DB.SqlServer, SqlServerDatabaseString).FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("master", data[0].ReferenceData["current_database"]);

            var dbContext = new DBContext(DB.SqlServer, SqlServerDatabaseString);
            data = dbContext.FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("master", data[0].ReferenceData["current_database"]);
        }

        #endregion

        #region MySQL DB Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MY_SQL_TESTS)]
        public void Test_MySql_FetchData_LowerCaseKeys()
        {
            var selectSql = Queries.MySqlQuery.SelectSql;
            var data = new DBContext(DB.MySql, MySqlDatabaseString).FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mysql", data[0].ReferenceData["current_database"]);

            var dbContext = new DBContext(DB.MySql, MySqlDatabaseString);
            data = dbContext.FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mysql", data[0].ReferenceData["current_database"]);
        }

        #endregion
    }
}
