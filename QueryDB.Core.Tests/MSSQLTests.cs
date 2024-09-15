using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace QueryDB.Core.Tests
{
    [TestClass]
    public class MSSQLTests : TestBase
    {

        #region MSSQL DB Tests

        #region Smoke Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS), TestCategory(SMOKE_TESTS)]
        public void Test_MSSQL_FetchData()
        {
            var selectSql = Queries.MSSQLQueries.Smoke.SelectSql;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mssql", data[0].ReferenceData["current_database"]);

            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);
            data = dbContext.FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("mssql", data[0].ReferenceData["current_database"]);
        }

        #endregion

        #region Fetch Data Tests - << List<DataDictionary> FetchData(string selectSql, bool upperCaseKeys = false) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_SelectQuery()
        {
            var selectSql = Queries.MSSQLQueries.SalesDB.SelectSql;
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
            var selectSql = Queries.MSSQLQueries.SalesDB.SelectSql;
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
            var selectSql = Queries.MSSQLQueries.SalesDB.SelectSql_Join;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 34);
            var agent = data.FirstOrDefault(X => X.ReferenceData["Agent_Code"] == "A004" && X.ReferenceData["Cust_Code"] == "C00006");
            Assert.AreEqual("A004", agent.ReferenceData["Agent_Code"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["Agent_Name"]);
            Assert.AreEqual("C00006", agent.ReferenceData["Cust_Code"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["Cust_Name"]);
            Assert.AreEqual("200104", agent.ReferenceData["Ord_Num"]);
            Assert.AreEqual("1500.00", agent.ReferenceData["Ord_Amount"]);
            Assert.AreEqual("500.00", agent.ReferenceData["Advance_Amount"]);
            Assert.AreEqual("SOD", agent.ReferenceData["Ord_Description"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_SelectQuery_Joins_UpperCaseKeys()
        {
            var selectSql = Queries.MSSQLQueries.SalesDB.SelectSql_Join;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql, upperCaseKeys: true);
            Assert.IsTrue(data.Count == 34);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_CODE"] == "A004" && X.ReferenceData["CUST_CODE"] == "C00006");
            Assert.AreEqual("A004", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("C00006", agent.ReferenceData["CUST_CODE"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["CUST_NAME"]);
            Assert.AreEqual("200104", agent.ReferenceData["ORD_NUM"]);
            Assert.AreEqual("1500.00", agent.ReferenceData["ORD_AMOUNT"]);
            Assert.AreEqual("500.00", agent.ReferenceData["ADVANCE_AMOUNT"]);
            Assert.AreEqual("SOD", agent.ReferenceData["ORD_DESCRIPTION"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_SelectQuery_Aliases()
        {
            var selectSql = Queries.MSSQLQueries.SalesDB.SelectSql_Alias;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 34);
            var agent = data.FirstOrDefault(X => X.ReferenceData["Agent_Code"] == "A004" && X.ReferenceData["Cust_Code"] == "C00006");
            Assert.AreEqual("A004", agent.ReferenceData["Agent_Code"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["Agent"]);
            Assert.AreEqual("Torento", agent.ReferenceData["Agent_Location"]);
            Assert.AreEqual("C00006", agent.ReferenceData["Cust_Code"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["Customer"]);
            Assert.AreEqual("Torento", agent.ReferenceData["Customer_Location"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_SelectQuery_Aliases_UpperCaseKeys()
        {
            var selectSql = Queries.MSSQLQueries.SalesDB.SelectSql_Alias;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql, upperCaseKeys: true);
            Assert.IsTrue(data.Count == 34);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_CODE"] == "A004" && X.ReferenceData["CUST_CODE"] == "C00006");
            Assert.AreEqual("A004", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["AGENT"]);
            Assert.AreEqual("Torento", agent.ReferenceData["AGENT_LOCATION"]);
            Assert.AreEqual("C00006", agent.ReferenceData["CUST_CODE"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["CUSTOMER"]);
            Assert.AreEqual("Torento", agent.ReferenceData["CUSTOMER_LOCATION"]);
        }

        #endregion

        #region Fetch Data Tests - << List<T> FetchData<T>(string selectSql) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_Entity_SelectQuery()
        {
            var selectSql = Queries.MSSQLQueries.SalesDB.SelectSql;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData<Entities.MSSQL.Agents>(selectSql);
            Assert.IsTrue(data.Count == 12);
            var agent = data.FirstOrDefault(X => X.Agent_Name == "Benjamin");
            Assert.AreEqual("A009", agent.Agent_Code);
            Assert.AreEqual("Benjamin", agent.Agent_Name);
            Assert.AreEqual("Hampshair", agent.Working_Area);
            Assert.AreEqual((decimal)0.11, agent.Commission);
            Assert.AreEqual("008-22536178", agent.Phone_No);
            Assert.AreEqual("", agent.Country);
        }

        #endregion

        #endregion

    }
}
