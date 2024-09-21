using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace QueryDB.Core.Tests
{
    [TestClass]
    public class PostgreSQLTests : TestBase
    {

        #region PostgreSQL DB Tests

        #region Smoke Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS), TestCategory(SMOKE_TESTS)]
        public void Test_PostgreSQL_FetchData()
        {
            var selectSql = Queries.PostgreSQLQueries.Smoke.SelectSql;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("postgres", data[0].ReferenceData["current_database"]);

            var dbContext = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString);
            data = dbContext.FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("postgres", data[0].ReferenceData["current_database"]);
        }

        #endregion

        #region Fetch Data Tests - << List<DataDictionary> FetchData(string selectSql, bool upperCaseKeys = false) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Dictionary_SelectQuery()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 12);
            var agent = data.FirstOrDefault(X => X.ReferenceData["agent_name"] == "Benjamin");
            Assert.AreEqual("A009", agent.ReferenceData["agent_code"]);
            Assert.AreEqual("Benjamin", agent.ReferenceData["agent_name"]);
            Assert.AreEqual("Hampshair", agent.ReferenceData["working_area"]);
            Assert.AreEqual("0.11", agent.ReferenceData["commission"]);
            Assert.AreEqual("008-22536178", agent.ReferenceData["phone_no"]);
            Assert.AreEqual("", agent.ReferenceData["country"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Dictionary_SelectQuery_UpperCaseKeys()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData(selectSql, upperCaseKeys: true);
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
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Dictionary_SelectQuery_Joins()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 34);
            var agent = data.FirstOrDefault(X => X.ReferenceData["agent_code"] == "A004" && X.ReferenceData["cust_code"] == "C00006");
            Assert.AreEqual("A004", agent.ReferenceData["agent_code"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["agent_name"]);
            Assert.AreEqual("C00006", agent.ReferenceData["cust_code"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["cust_name"]);
            Assert.AreEqual("200104", agent.ReferenceData["ord_num"]);
            Assert.AreEqual("1500.00", agent.ReferenceData["ord_amount"]);
            Assert.AreEqual("500.00", agent.ReferenceData["advance_amount"]);
            Assert.AreEqual("SOD", agent.ReferenceData["ord_description"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Dictionary_SelectQuery_Joins_UpperCaseKeys()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData(selectSql, upperCaseKeys: true);
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
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Dictionary_SelectQuery_Aliases()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 34);
            var agent = data.FirstOrDefault(X => X.ReferenceData["agent_code"] == "A004" && X.ReferenceData["cust_code"] == "C00006");
            Assert.AreEqual("A004", agent.ReferenceData["agent_code"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["agent"]);
            Assert.AreEqual("Torento", agent.ReferenceData["agent_location"]);
            Assert.AreEqual("C00006", agent.ReferenceData["cust_code"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["customer"]);
            Assert.AreEqual("Torento", agent.ReferenceData["customer_location"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Dictionary_SelectQuery_Aliases_UpperCaseKeys()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData(selectSql, upperCaseKeys: true);
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
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Entity_SelectQuery()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData<Entities.PostgreSQL.Agents>(selectSql);
            Assert.IsTrue(data.Count == 12);
            var agent = data.FirstOrDefault(X => X.Agent_Name == "Benjamin");
            Assert.AreEqual("A009", agent.Agent_Code);
            Assert.AreEqual("Benjamin", agent.Agent_Name);
            Assert.AreEqual("Hampshair", agent.Working_Area);
            Assert.AreEqual((decimal)0.11, agent.Commission);
            Assert.AreEqual("008-22536178", agent.Phone_No);
            Assert.AreEqual("", agent.Country);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Entity_SelectQuery_Joins()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData<Entities.PostgreSQL.Orders>(selectSql);
            Assert.IsTrue(data.Count == 34);
            var agent = data.FirstOrDefault(X => X.Agent_Code == "A004" && X.Cust_Code == "C00006");
            Assert.AreEqual("A004", agent.Agent_Code);
            Assert.AreEqual("Ivan", agent.Agent_Name);
            Assert.AreEqual("C00006", agent.Cust_Code);
            Assert.AreEqual("Shilton", agent.Cust_Name);
            Assert.AreEqual(200104, agent.Ord_Num);
            Assert.AreEqual((decimal)1500.00, agent.Ord_Amount);
            Assert.AreEqual((decimal)500.00, agent.Advance_Amount);
            Assert.AreEqual("SOD", agent.Ord_Description);
            // Non Existent Query Data
            Assert.AreEqual(null, agent.Agent);
            Assert.AreEqual(null, agent.Agent_Location);
            Assert.AreEqual(null, agent.Customer);
            Assert.AreEqual(null, agent.Customer_Location);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Entity_SelectQuery_Aliases()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData<Entities.PostgreSQL.Orders>(selectSql);
            Assert.IsTrue(data.Count == 34);
            var agent = data.FirstOrDefault(X => X.Agent_Code == "A004" && X.Cust_Code == "C00006");
            Assert.AreEqual("A004", agent.Agent_Code);
            Assert.AreEqual("Ivan", agent.Agent);
            Assert.AreEqual("Torento", agent.Agent_Location);
            Assert.AreEqual("C00006", agent.Cust_Code);
            Assert.AreEqual("Shilton", agent.Customer);
            Assert.AreEqual("Torento", agent.Customer_Location);
            // Non Existent Query Data
            Assert.AreEqual(null, agent.Agent_Name);
            Assert.AreEqual(null, agent.Cust_Name);
            Assert.AreEqual(0, agent.Ord_Num);
            Assert.AreEqual(0, agent.Ord_Amount);
            Assert.AreEqual(0, agent.Advance_Amount);
            Assert.AreEqual(null, agent.Ord_Description);
        }

        #endregion

        #endregion

    }
}
