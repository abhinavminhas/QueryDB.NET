using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace QueryDB.Core.Tests
{
    [TestClass]
    public class MySQLTests : TestBase
    {

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

        #region Fetch Data Tests - << List<DataDictionary> FetchData(string selectSql, bool upperCaseKeys = false) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_Dictionary_SelectQuery()
        {
            var selectSql = Queries.MySQLQueries.TestDB.SelectSql;
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
        public void Test_MySQL_FetchData_Dictionary_SelectQuery_UpperCaseKeys()
        {
            var selectSql = Queries.MySQLQueries.TestDB.SelectSql;
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
        public void Test_MySQL_FetchData_Dictionary_SelectQuery_Joins()
        {
            var selectSql = Queries.MySQLQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_Dictionary_SelectQuery_Joins_UpperCaseKeys()
        {
            var selectSql = Queries.MySQLQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData(selectSql, upperCaseKeys: true);
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
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_Dictionary_SelectQuery_Aliases()
        {
            var selectSql = Queries.MySQLQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_Dictionary_SelectQuery_Aliases_UpperCaseKeys()
        {
            var selectSql = Queries.MySQLQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData(selectSql, upperCaseKeys: true);
            Assert.IsTrue(data.Count == 34);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_CODE"] == "A004" && X.ReferenceData["CUST_CODE"] == "C00006");
            Assert.AreEqual("A004", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["AGENT"]);
            Assert.AreEqual("Torento", agent.ReferenceData["AGENT_LOCATION"]);
            Assert.AreEqual("C00006", agent.ReferenceData["CUST_CODE"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["CUSTOMER"]);
            Assert.AreEqual("Torento", agent.ReferenceData["CUSTOMER_LOCATION"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_Dictionary_DataTypes_Check()
        {
            var selectSql = Queries.MySQLQueries.TestDB.SelectSql_DataTypes;
            var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 1);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual("9223372036854775807", dataType.ReferenceData["BigInt_Column"]);
            Assert.AreEqual("1", dataType.ReferenceData["Bit_Column"]);
            Assert.AreEqual("A", dataType.ReferenceData["Char_Column"]);
            Assert.AreEqual("09/21/2024 00:00:00", ConvertToUSFormat(dataType.ReferenceData["Date_Column"]));
            Assert.AreEqual("09/21/2024 13:24:10", ConvertToUSFormat(dataType.ReferenceData["DateTime_Column"]));
            Assert.AreEqual("12345.67", dataType.ReferenceData["Decimal_Column"]);
            Assert.AreEqual("123.45", dataType.ReferenceData["Float_Column"]);
            Assert.AreEqual("2147483647", dataType.ReferenceData["Int_Column"]);
            Assert.AreEqual("This is a long text", dataType.ReferenceData["LongText_Column"]);
            Assert.AreEqual("8388607", dataType.ReferenceData["MediumInt_Column"]);
            Assert.AreEqual("This is a medium text", dataType.ReferenceData["MediumText_Column"]);
            Assert.AreEqual("32767", dataType.ReferenceData["SmallInt_Column"]);
            Assert.AreEqual("This is a text", dataType.ReferenceData["Text_Column"]);
            Assert.AreEqual("13:24:10", dataType.ReferenceData["Time_Column"]);
            Assert.AreEqual("09/21/2024 13:24:10", ConvertToUSFormat(dataType.ReferenceData["Timestamp_Column"]));
            Assert.AreEqual("127", dataType.ReferenceData["TinyInt_Column"]);
            Assert.AreEqual("This is a tiny text", dataType.ReferenceData["TinyText_Column"]);
            Assert.AreEqual("3q2+7w==", dataType.ReferenceData["VarBinary_Column"]);
            Assert.AreEqual("This is a varchar", dataType.ReferenceData["VarChar_Column"]);
        }

        #endregion

        #region Fetch Data Tests - << List<T> FetchData<T>(string selectSql) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_Entity_SelectQuery()
        {
            var selectSql = Queries.MySQLQueries.TestDB.SelectSql;
            var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData<Entities.MySQL.Agents>(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_Entity_SelectQuery_Joins()
        {
            var selectSql = Queries.MySQLQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData<Entities.MySQL.Orders>(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_Entity_SelectQuery_Aliases()
        {
            var selectSql = Queries.MySQLQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData<Entities.MySQL.Orders>(selectSql);
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

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_Entity_DataTypes_Check()
        {
            var selectSql = Queries.MySQLQueries.TestDB.SelectSql_DataTypes;
            var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData<Entities.MySQL.DataTypes>(selectSql);
            Assert.IsTrue(data.Count == 1);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual(9223372036854775807, dataType.BigInt_Column);
            Assert.AreEqual((ulong?)1, dataType.Bit_Column);
            Assert.AreEqual("A", dataType.Char_Column);
            Assert.AreEqual("09/21/2024 00:00:00", ConvertToUSFormat(dataType.Date_Column.ToString()));
            Assert.AreEqual("09/21/2024 13:24:10", ConvertToUSFormat(dataType.DateTime_Column.ToString()));
            Assert.AreEqual((decimal)12345.67, dataType.Decimal_Column);
            Assert.AreEqual((float)123.45, dataType.Float_Column);
            Assert.AreEqual(2147483647, dataType.Int_Column);
            Assert.AreEqual("This is a long text", dataType.LongText_Column);
            Assert.AreEqual(8388607, dataType.MediumInt_Column);
            Assert.AreEqual("This is a medium text", dataType.MediumText_Column);
            Assert.AreEqual((short)32767, dataType.SmallInt_Column);
            Assert.AreEqual("This is a text", dataType.Text_Column);
            Assert.AreEqual("13:24:10", dataType.Time_Column.ToString());
            Assert.AreEqual("09/21/2024 13:24:10", ConvertToUSFormat(dataType.Timestamp_Column.ToString()));
            Assert.AreEqual((sbyte?)127, dataType.TinyInt_Column);
            Assert.AreEqual("This is a tiny text", dataType.TinyText_Column);
            Assert.IsTrue(dataType.VarBinary_Column is byte[] && dataType.VarBinary_Column != null);
            Assert.AreEqual("This is a varchar", dataType.VarChar_Column);
        }

        #endregion

        #endregion

    }
}
