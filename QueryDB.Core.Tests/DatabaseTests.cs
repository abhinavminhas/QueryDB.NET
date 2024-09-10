using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace QueryDB.Core.Tests
{
    [TestClass]
    public class DatabaseTests : TestBase
    {

        #region MSSQL DB Tests

        #region Smoke Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS), TestCategory(SMOKE_TESTS)]
        public void Test_MSSQL_FetchData()
        {
            var selectSql = Queries.SQLServerQueries.Smoke.SelectSql;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mssql", data[0].ReferenceData["current_database"]);

            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);
            data = dbContext.FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mssql", data[0].ReferenceData["current_database"]);
        }

        #endregion

        #region Fetch Data Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_SelectQuery()
        {
            var selectSql = Queries.SQLServerQueries.SalesDB.SelectSql;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql);
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
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql, upperCaseKeys: true);
            Assert.IsTrue(data.Count == 12);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_NAME"] == "Benjamin");
            Assert.AreEqual("A009", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Benjamin", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("Hampshair", agent.ReferenceData["WORKING_AREA"]);
            Assert.AreEqual("0.11", agent.ReferenceData["COMMISSION"]);
            Assert.AreEqual("008-22536178", agent.ReferenceData["PHONE_NO"]);
            Assert.AreEqual("", agent.ReferenceData["COUNTRY"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_SelectQuery_Joins()
        {
            var selectSql = Queries.SQLServerQueries.SalesDB.SelectSql_Join;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 34);
            var agent = data.FirstOrDefault(X => X.ReferenceData["Agent_Code"] == "A004");
            Assert.AreEqual("A004", agent.ReferenceData["Agent_Code"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["Agent_Name"]);
            Assert.AreEqual("C00006", agent.ReferenceData["Cust_Code"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["Cust_Name"]);
            Assert.AreEqual("200104", agent.ReferenceData["Ord_Num"]);
            Assert.AreEqual("1500.00", agent.ReferenceData["Ord_Amount"]);
            Assert.AreEqual("500.00", agent.ReferenceData["Advance_Amount"]);
            Assert.AreEqual("13/03/2008 12:00:00 AM", agent.ReferenceData["Ord_Date"]);
            Assert.AreEqual("SOD", agent.ReferenceData["Ord_Description"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_SelectQuery_Joins_UpperCaseKeys()
        {
            var selectSql = Queries.SQLServerQueries.SalesDB.SelectSql_Join;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql, upperCaseKeys: true);
            Assert.IsTrue(data.Count == 34);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_CODE"] == "A004");
            Assert.AreEqual("A004", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("C00006", agent.ReferenceData["CUST_CODE"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["CUST_NAME"]);
            Assert.AreEqual("200104", agent.ReferenceData["ORD_NUM"]);
            Assert.AreEqual("1500.00", agent.ReferenceData["ORD_AMOUNT"]);
            Assert.AreEqual("500.00", agent.ReferenceData["ADVANCE_AMOUNT"]);
            Assert.AreEqual("13/03/2008 12:00:00 AM", agent.ReferenceData["ORD_DATE"]);
            Assert.AreEqual("SOD", agent.ReferenceData["ORD_DESCRIPTION"]);
        }

        #endregion

        #endregion

        #region MySQL DB Tests

        #region Smoke Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS), TestCategory(SMOKE_TESTS)]
        public void Test_MySQL_FetchData()
        {
            var selectSql = Queries.MySQLQueries.Smoke.SelectSql;
            var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mysql", data[0].ReferenceData["current_database"]);

            var dbContext = new DBContext(DB.MySQL, MySQLConnectionString);
            data = dbContext.FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mysql", data[0].ReferenceData["current_database"]);
        }

        #endregion

        #region Fetch Data Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_SelectQuery()
        {
            var selectSql = Queries.SQLServerQueries.SalesDB.SelectSql;
            var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData(selectSql);
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
            var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData(selectSql, upperCaseKeys: true);
            Assert.IsTrue(data.Count == 12);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_NAME"] == "Benjamin");
            Assert.AreEqual("A009", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Benjamin", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("Hampshair", agent.ReferenceData["WORKING_AREA"]);
            Assert.AreEqual("0.11", agent.ReferenceData["COMMISSION"]);
            Assert.AreEqual("008-22536178", agent.ReferenceData["PHONE_NO"]);
            Assert.AreEqual("", agent.ReferenceData["COUNTRY"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_SelectQuery_Joins()
        {
            var selectSql = Queries.MySQLQueries.SalesDB.SelectSql_Join;
            var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 34);
            var agent = data.FirstOrDefault(X => X.ReferenceData["Agent_Code"] == "A004");
            Assert.AreEqual("A004", agent.ReferenceData["Agent_Code"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["Agent_Name"]);
            Assert.AreEqual("C00006", agent.ReferenceData["Cust_Code"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["Cust_Name"]);
            Assert.AreEqual("200104", agent.ReferenceData["Ord_Num"]);
            Assert.AreEqual("1500.00", agent.ReferenceData["Ord_Amount"]);
            Assert.AreEqual("500.00", agent.ReferenceData["Advance_Amount"]);
            Assert.AreEqual("13/03/2008 00:00:00", agent.ReferenceData["Ord_Date"]);
            Assert.AreEqual("SOD", agent.ReferenceData["Ord_Description"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_SelectQuery_Joins_UpperCaseKeys()
        {
            var selectSql = Queries.MySQLQueries.SalesDB.SelectSql_Join;
            var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData(selectSql, upperCaseKeys: true);
            Assert.IsTrue(data.Count == 34);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_CODE"] == "A004");
            Assert.AreEqual("A004", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("C00006", agent.ReferenceData["CUST_CODE"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["CUST_NAME"]);
            Assert.AreEqual("200104", agent.ReferenceData["ORD_NUM"]);
            Assert.AreEqual("1500.00", agent.ReferenceData["ORD_AMOUNT"]);
            Assert.AreEqual("500.00", agent.ReferenceData["ADVANCE_AMOUNT"]);
            Assert.AreEqual("13/03/2008 00:00:00", agent.ReferenceData["ORD_DATE"]);
            Assert.AreEqual("SOD", agent.ReferenceData["ORD_DESCRIPTION"]);
        }

        #endregion

        #endregion

        #region Oracle DB Tests

        #region Smoke Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS), TestCategory(SMOKE_TESTS)]
        public void Test_Oracle_FetchData()
        {
            var selectSql = Queries.OracleQueries.Smoke.SelectSql;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("oracle", data[0].ReferenceData["CURRENT_DATABASE"]);

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);
            data = dbContext.FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("oracle", data[0].ReferenceData["CURRENT_DATABASE"]);
        }

        #endregion

        #region Fetch Data Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_SelectQuery()
        {
            var selectSql = Queries.SQLServerQueries.SalesDB.SelectSql;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 12);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_NAME"] == "Benjamin");
            Assert.AreEqual("A009", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Benjamin", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("Hampshair", agent.ReferenceData["WORKING_AREA"]);
            Assert.AreEqual("0.11", agent.ReferenceData["COMMISSION"]);
            Assert.AreEqual("008-22536178", agent.ReferenceData["PHONE_NO"]);
            Assert.AreEqual("", agent.ReferenceData["COUNTRY"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_SelectQuery_UpperCaseKeys()
        {
            var selectSql = Queries.SQLServerQueries.SalesDB.SelectSql;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData(selectSql, upperCaseKeys: true);
            Assert.IsTrue(data.Count == 12);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_NAME"] == "Benjamin");
            Assert.AreEqual("A009", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Benjamin", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("Hampshair", agent.ReferenceData["WORKING_AREA"]);
            Assert.AreEqual("0.11", agent.ReferenceData["COMMISSION"]);
            Assert.AreEqual("008-22536178", agent.ReferenceData["PHONE_NO"]);
            Assert.AreEqual("", agent.ReferenceData["COUNTRY"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_SelectQuery_Joins()
        {
            var selectSql = Queries.OracleQueries.SalesDB.SelectSql_Join;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 34);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_CODE"] == "A004");
            Assert.AreEqual("A004", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("C00006", agent.ReferenceData["CUST_CODE"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["CUST_NAME"]);
            Assert.AreEqual("200104", agent.ReferenceData["ORD_NUM"]);
            Assert.AreEqual("1500.00", agent.ReferenceData["ORD_AMOUNT"]);
            Assert.AreEqual("500.00", agent.ReferenceData["ADVANCE_AMOUNT"]);
            Assert.AreEqual("13/03/2008 12:00:00 AM", agent.ReferenceData["ORD_DATE"]);
            Assert.AreEqual("SOD", agent.ReferenceData["ORD_DESCRIPTION"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_SelectQuery_Joins_UpperCaseKeys()
        {
            var selectSql = Queries.OracleQueries.SalesDB.SelectSql_Join;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData(selectSql, upperCaseKeys: true);
            Assert.IsTrue(data.Count == 34);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_CODE"] == "A004");
            Assert.AreEqual("A004", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("C00006", agent.ReferenceData["CUST_CODE"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["CUST_NAME"]);
            Assert.AreEqual("200104", agent.ReferenceData["ORD_NUM"]);
            Assert.AreEqual("1500.00", agent.ReferenceData["ORD_AMOUNT"]);
            Assert.AreEqual("500.00", agent.ReferenceData["ADVANCE_AMOUNT"]);
            Assert.AreEqual("13/03/2008 12:00:00 AM", agent.ReferenceData["ORD_DATE"]);
            Assert.AreEqual("SOD", agent.ReferenceData["ORD_DESCRIPTION"]);
        }

        #endregion

        #endregion

    }
}
