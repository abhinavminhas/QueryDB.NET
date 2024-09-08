using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace QueryDB.Core.Tests
{
    [TestClass]
    public class DatabaseTests : TestBase
    {

        #region MSSQL DB Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS), TestCategory(SMOKE_TESTS)]
        public void Test_MSSQL_FetchData()
        {
            var selectSql = Queries.SQLServerQueries.Smoke.SelectSql;
            var data = new DBContext(DB.MSSQL, SqlServerDatabaseString).FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mssql", data[0].ReferenceData["current_database"]);

            var dbContext = new DBContext(DB.MSSQL, SqlServerDatabaseString);
            data = dbContext.FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mssql", data[0].ReferenceData["current_database"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_SelectQuery_LowerCaseKeys()
        {
            var selectSql = Queries.SQLServerQueries.SalesDB.SelectSql;
            var data = new DBContext(DB.MSSQL, SqlServerDatabaseString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 12);
            var agent = data.FirstOrDefault(X => X.ReferenceData["Agent_Name"] == "Benjamin");
            Assert.AreEqual("A009", agent.ReferenceData["Agent_Code"]);
            Assert.AreEqual("Benjamin", agent.ReferenceData["Agent_Name"]);
            Assert.AreEqual("Hampshair", agent.ReferenceData["Working_Area"]);
            Assert.AreEqual("0.11", agent.ReferenceData["Commission"]);
            Assert.AreEqual("008-22536178", agent.ReferenceData["Phone_No"]);
            Assert.AreEqual("", agent.ReferenceData["Country"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_SelectQuery_UpperCaseKeys()
        {
            var selectSql = Queries.SQLServerQueries.SalesDB.SelectSql;
            var data = new DBContext(DB.MSSQL, SqlServerDatabaseString).FetchData(selectSql, upperCaseKeys: true);
            Assert.IsTrue(data.Count == 12);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_NAME"] == "Benjamin");
            Assert.AreEqual("A009", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Benjamin", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("Hampshair", agent.ReferenceData["WORKING_AREA"]);
            Assert.AreEqual("0.11", agent.ReferenceData["COMMISSION"]);
            Assert.AreEqual("008-22536178", agent.ReferenceData["PHONE_NO"]);
            Assert.AreEqual("", agent.ReferenceData["COUNTRY"]);
        }

        #endregion

        #region MySQL DB Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS), TestCategory(SMOKE_TESTS)]
        public void Test_MySQL_FetchData()
        {
            var selectSql = Queries.MySQLQueries.Smoke.SelectSql;
            var data = new DBContext(DB.MySQL, MySqlDatabaseString).FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mysql", data[0].ReferenceData["current_database"]);

            var dbContext = new DBContext(DB.MySQL, MySqlDatabaseString);
            data = dbContext.FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mysql", data[0].ReferenceData["current_database"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_SelectQuery_LowerCaseKeys()
        {
            var selectSql = Queries.SQLServerQueries.SalesDB.SelectSql;
            var data = new DBContext(DB.MySQL, SqlServerDatabaseString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 12);
            var agent = data.FirstOrDefault(X => X.ReferenceData["Agent_Name"] == "Benjamin");
            Assert.AreEqual("A009", agent.ReferenceData["Agent_Code"]);
            Assert.AreEqual("Benjamin", agent.ReferenceData["Agent_Name"]);
            Assert.AreEqual("Hampshair", agent.ReferenceData["Working_Area"]);
            Assert.AreEqual("0.11", agent.ReferenceData["Commission"]);
            Assert.AreEqual("008-22536178", agent.ReferenceData["Phone_No"]);
            Assert.AreEqual("", agent.ReferenceData["Country"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_SelectQuery_UpperCaseKeys()
        {
            var selectSql = Queries.SQLServerQueries.SalesDB.SelectSql;
            var data = new DBContext(DB.MySQL, SqlServerDatabaseString).FetchData(selectSql, upperCaseKeys: true);
            Assert.IsTrue(data.Count == 12);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_NAME"] == "Benjamin");
            Assert.AreEqual("A009", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Benjamin", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("Hampshair", agent.ReferenceData["WORKING_AREA"]);
            Assert.AreEqual("0.11", agent.ReferenceData["COMMISSION"]);
            Assert.AreEqual("008-22536178", agent.ReferenceData["PHONE_NO"]);
            Assert.AreEqual("", agent.ReferenceData["COUNTRY"]);
        }

        #endregion

        #region Oracle DB Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS), TestCategory(SMOKE_TESTS)]
        public void Test_Oracle_FetchData()
        {
            var selectSql = Queries.OracleQueries.Smoke.SelectSql;
            var data = new DBContext(DB.Oracle, OracleDatabaseString).FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("oracle", data[0].ReferenceData["CURRENT_DATABASE"]);

            var dbContext = new DBContext(DB.Oracle, OracleDatabaseString);
            data = dbContext.FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("oracle", data[0].ReferenceData["CURRENT_DATABASE"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_SelectQuery_LowerCaseKeys()
        {
            var selectSql = Queries.SQLServerQueries.SalesDB.SelectSql;
            var data = new DBContext(DB.Oracle, SqlServerDatabaseString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 12);
            var agent = data.FirstOrDefault(X => X.ReferenceData["Agent_Name"] == "Benjamin");
            Assert.AreEqual("A009", agent.ReferenceData["Agent_Code"]);
            Assert.AreEqual("Benjamin", agent.ReferenceData["Agent_Name"]);
            Assert.AreEqual("Hampshair", agent.ReferenceData["Working_Area"]);
            Assert.AreEqual("0.11", agent.ReferenceData["Commission"]);
            Assert.AreEqual("008-22536178", agent.ReferenceData["Phone_No"]);
            Assert.AreEqual("", agent.ReferenceData["Country"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_SelectQuery_UpperCaseKeys()
        {
            var selectSql = Queries.SQLServerQueries.SalesDB.SelectSql;
            var data = new DBContext(DB.Oracle, SqlServerDatabaseString).FetchData(selectSql, upperCaseKeys: true);
            Assert.IsTrue(data.Count == 12);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_NAME"] == "Benjamin");
            Assert.AreEqual("A009", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Benjamin", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("Hampshair", agent.ReferenceData["WORKING_AREA"]);
            Assert.AreEqual("0.11", agent.ReferenceData["COMMISSION"]);
            Assert.AreEqual("008-22536178", agent.ReferenceData["PHONE_NO"]);
            Assert.AreEqual("", agent.ReferenceData["COUNTRY"]);
        }

        #endregion
    }
}
