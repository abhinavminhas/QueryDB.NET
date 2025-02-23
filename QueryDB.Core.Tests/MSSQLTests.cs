using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryDB.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public void Test_MSSQL_FetchData_Dictionary_SelectQuery()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql);
            Assert.AreEqual(12, data.Count);
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
        public void Test_MSSQL_FetchData_Dictionary_SelectQuery_UpperCaseKeys()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql, upperCaseKeys: true);
            Assert.AreEqual(12, data.Count);
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
        public void Test_MSSQL_FetchData_Dictionary_SelectQuery_Joins()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql);
            Assert.AreEqual(34, data.Count);
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
        public void Test_MSSQL_FetchData_Dictionary_SelectQuery_Joins_UpperCaseKeys()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql, upperCaseKeys: true);
            Assert.AreEqual(34, data.Count);
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
        public void Test_MSSQL_FetchData_Dictionary_SelectQuery_Aliases()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql);
            Assert.AreEqual(34, data.Count);
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
        public void Test_MSSQL_FetchData_Dictionary_SelectQuery_Aliases_UpperCaseKeys()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql, upperCaseKeys: true);
            Assert.AreEqual(34, data.Count);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_CODE"] == "A004" && X.ReferenceData["CUST_CODE"] == "C00006");
            Assert.AreEqual("A004", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["AGENT"]);
            Assert.AreEqual("Torento", agent.ReferenceData["AGENT_LOCATION"]);
            Assert.AreEqual("C00006", agent.ReferenceData["CUST_CODE"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["CUSTOMER"]);
            Assert.AreEqual("Torento", agent.ReferenceData["CUSTOMER_LOCATION"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_Dictionary_DataTypes_Check()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_DataTypes;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql);
            Assert.AreEqual(1, data.Count);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual("9223372036854775807", dataType.ReferenceData["BigInt_Column"]);
            Assert.AreEqual("EjRWeJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=", dataType.ReferenceData["Binary_Column"]);
            Assert.AreEqual("True", dataType.ReferenceData["Bit_Column"]);
            Assert.AreEqual("CharData", dataType.ReferenceData["Char_Column"]);
            Assert.AreEqual("09/21/2024 00:00:00", ConvertToUSFormat(dataType.ReferenceData["Date_Column"]));
            Assert.AreEqual("09/21/2024 08:34:51", ConvertToUSFormat(dataType.ReferenceData["DateTime_Column"]));
            Assert.AreEqual("09/21/2024 08:34:51", ConvertToUSFormat(dataType.ReferenceData["DateTime2_Column"]));
            Assert.AreEqual("09/20/2024 22:34:51", ConvertToUTCInUSFormat(dataType.ReferenceData["DateTimeOffset_Column"]));
            Assert.AreEqual("123456.78", dataType.ReferenceData["Decimal_Column"]);
            Assert.AreEqual("123456.78", dataType.ReferenceData["Float_Column"]);
            Assert.AreEqual("EjRWeJA=", dataType.ReferenceData["Image_Column"]);
            Assert.AreEqual("2147483647", dataType.ReferenceData["Int_Column"]);
            Assert.AreEqual("123456.7800", dataType.ReferenceData["Money_Column"]);
            Assert.AreEqual("NCharData", dataType.ReferenceData["NChar_Column"]);
            Assert.AreEqual("NTextData", dataType.ReferenceData["NText_Column"]);
            Assert.AreEqual("123456.78", dataType.ReferenceData["Numeric_Column"]);
            Assert.AreEqual("NVarCharData", dataType.ReferenceData["NVarChar_Column"]);
            Assert.AreEqual("123.45", dataType.ReferenceData["Real_Column"]);
            Assert.AreEqual("09/21/2024 08:35:00", ConvertToUSFormat(dataType.ReferenceData["SmallDateTime_Column"]));
            Assert.AreEqual("32767", dataType.ReferenceData["SmallInt_Column"]);
            Assert.AreEqual("123456.7800", dataType.ReferenceData["SmallMoney_Column"]);
            Assert.AreEqual("SampleVariant", dataType.ReferenceData["SqlVariant_Column"]);
            Assert.AreEqual("TextData", dataType.ReferenceData["Text_Column"]);
            Assert.AreEqual("08:34:51", dataType.ReferenceData["Time_Column"]);
            Assert.AreEqual("255", dataType.ReferenceData["TinyInt_Column"]);
            Assert.AreEqual("12345678-1234-1234-1234-123456789012", dataType.ReferenceData["UniqueIdentifier_Column"]);
            Assert.AreEqual("EjRWeJA=", dataType.ReferenceData["VarBinary_Column"]);
            Assert.AreEqual("VarCharData", dataType.ReferenceData["VarChar_Column"]);
            Assert.AreEqual("<root><element>XmlData</element></root>", dataType.ReferenceData["Xml_Column"]);
        }

        #endregion

        #region Fetch Data Async Tests - << Task<List<DataDictionary>> FetchDataAsync(string selectSql, bool upperCaseKeys = false) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public async Task Test_MSSQL_FetchDataAsync_Dictionary_SelectQuery()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql;
            var data = await new DBContext(DB.MSSQL, MSSQLConnectionString).FetchDataAsync(selectSql);
            Assert.AreEqual(12, data.Count);
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
        public async Task Test_MSSQL_FetchDataAsync_Dictionary_SelectQuery_UpperCaseKeys()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql;
            var data = await new DBContext(DB.MSSQL, MSSQLConnectionString).FetchDataAsync(selectSql, upperCaseKeys: true);
            Assert.AreEqual(12, data.Count);
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
        public async Task Test_MSSQL_FetchDataAsync_Dictionary_SelectQuery_Joins()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Join;
            var data = await new  DBContext(DB.MSSQL, MSSQLConnectionString).FetchDataAsync(selectSql);
            Assert.AreEqual(34, data.Count);
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
        public async Task Test_MSSQL_FetchDataAsync_Dictionary_SelectQuery_Joins_UpperCaseKeys()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Join;
            var data = await new DBContext(DB.MSSQL, MSSQLConnectionString).FetchDataAsync(selectSql, upperCaseKeys: true);
            Assert.AreEqual(34, data.Count);
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
        public async Task Test_MSSQL_FetchDataAsync_Dictionary_SelectQuery_Aliases()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Alias;
            var data = await new  DBContext(DB.MSSQL, MSSQLConnectionString).FetchDataAsync(selectSql);
            Assert.AreEqual(34, data.Count);
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
        public async Task Test_MSSQL_FetchDataAsync_Dictionary_SelectQuery_Aliases_UpperCaseKeys()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Alias;
            var data = await new DBContext(DB.MSSQL, MSSQLConnectionString).FetchDataAsync(selectSql, upperCaseKeys: true);
            Assert.AreEqual(34, data.Count);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_CODE"] == "A004" && X.ReferenceData["CUST_CODE"] == "C00006");
            Assert.AreEqual("A004", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["AGENT"]);
            Assert.AreEqual("Torento", agent.ReferenceData["AGENT_LOCATION"]);
            Assert.AreEqual("C00006", agent.ReferenceData["CUST_CODE"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["CUSTOMER"]);
            Assert.AreEqual("Torento", agent.ReferenceData["CUSTOMER_LOCATION"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public async Task Test_MSSQL_FetchDataAsync_Dictionary_DataTypes_Check()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_DataTypes;
            var data = await new DBContext(DB.MSSQL, MSSQLConnectionString).FetchDataAsync(selectSql);
            Assert.AreEqual(1, data.Count);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual("9223372036854775807", dataType.ReferenceData["BigInt_Column"]);
            Assert.AreEqual("EjRWeJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=", dataType.ReferenceData["Binary_Column"]);
            Assert.AreEqual("True", dataType.ReferenceData["Bit_Column"]);
            Assert.AreEqual("CharData", dataType.ReferenceData["Char_Column"]);
            Assert.AreEqual("09/21/2024 00:00:00", ConvertToUSFormat(dataType.ReferenceData["Date_Column"]));
            Assert.AreEqual("09/21/2024 08:34:51", ConvertToUSFormat(dataType.ReferenceData["DateTime_Column"]));
            Assert.AreEqual("09/21/2024 08:34:51", ConvertToUSFormat(dataType.ReferenceData["DateTime2_Column"]));
            Assert.AreEqual("09/20/2024 22:34:51", ConvertToUTCInUSFormat(dataType.ReferenceData["DateTimeOffset_Column"]));
            Assert.AreEqual("123456.78", dataType.ReferenceData["Decimal_Column"]);
            Assert.AreEqual("123456.78", dataType.ReferenceData["Float_Column"]);
            Assert.AreEqual("EjRWeJA=", dataType.ReferenceData["Image_Column"]);
            Assert.AreEqual("2147483647", dataType.ReferenceData["Int_Column"]);
            Assert.AreEqual("123456.7800", dataType.ReferenceData["Money_Column"]);
            Assert.AreEqual("NCharData", dataType.ReferenceData["NChar_Column"]);
            Assert.AreEqual("NTextData", dataType.ReferenceData["NText_Column"]);
            Assert.AreEqual("123456.78", dataType.ReferenceData["Numeric_Column"]);
            Assert.AreEqual("NVarCharData", dataType.ReferenceData["NVarChar_Column"]);
            Assert.AreEqual("123.45", dataType.ReferenceData["Real_Column"]);
            Assert.AreEqual("09/21/2024 08:35:00", ConvertToUSFormat(dataType.ReferenceData["SmallDateTime_Column"]));
            Assert.AreEqual("32767", dataType.ReferenceData["SmallInt_Column"]);
            Assert.AreEqual("123456.7800", dataType.ReferenceData["SmallMoney_Column"]);
            Assert.AreEqual("SampleVariant", dataType.ReferenceData["SqlVariant_Column"]);
            Assert.AreEqual("TextData", dataType.ReferenceData["Text_Column"]);
            Assert.AreEqual("08:34:51", dataType.ReferenceData["Time_Column"]);
            Assert.AreEqual("255", dataType.ReferenceData["TinyInt_Column"]);
            Assert.AreEqual("12345678-1234-1234-1234-123456789012", dataType.ReferenceData["UniqueIdentifier_Column"]);
            Assert.AreEqual("EjRWeJA=", dataType.ReferenceData["VarBinary_Column"]);
            Assert.AreEqual("VarCharData", dataType.ReferenceData["VarChar_Column"]);
            Assert.AreEqual("<root><element>XmlData</element></root>", dataType.ReferenceData["Xml_Column"]);
        }

        #endregion

        #region Fetch Data Tests - << List<T> FetchData<T>(string selectSql, bool strict = false) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_Entity_SelectQuery()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData<Entities.MSSQL.Agents>(selectSql);
            Assert.AreEqual(12, data.Count);
            var agent = data.FirstOrDefault(X => X.Agent_Name == "Benjamin");
            Assert.AreEqual("A009", agent.Agent_Code);
            Assert.AreEqual("Benjamin", agent.Agent_Name);
            Assert.AreEqual("Hampshair", agent.Working_Area);
            Assert.AreEqual((decimal)0.11, agent.Commission);
            Assert.AreEqual("008-22536178", agent.Phone_No);
            Assert.AreEqual("", agent.Country);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_Entity_SelectQuery_Joins()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData<Entities.MSSQL.Orders>(selectSql);
            Assert.AreEqual(34, data.Count);
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
            Assert.IsNull(agent.Agent);
            Assert.IsNull(agent.Agent_Location);
            Assert.IsNull(agent.Customer);
            Assert.IsNull(agent.Customer_Location);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_Entity_SelectQuery_Aliases()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData<Entities.MSSQL.Orders>(selectSql);
            Assert.AreEqual(34, data.Count);
            var agent = data.FirstOrDefault(X => X.Agent_Code == "A004" && X.Cust_Code == "C00006");
            Assert.AreEqual("A004", agent.Agent_Code);
            Assert.AreEqual("Ivan", agent.Agent);
            Assert.AreEqual("Torento", agent.Agent_Location);
            Assert.AreEqual("C00006", agent.Cust_Code);
            Assert.AreEqual("Shilton", agent.Customer);
            Assert.AreEqual("Torento", agent.Customer_Location);
            // Non Existent Query Data
            Assert.IsNull(agent.Agent_Name);
            Assert.IsNull(agent.Cust_Name);
            Assert.AreEqual(0, agent.Ord_Num);
            Assert.AreEqual(0, agent.Ord_Amount);
            Assert.AreEqual(0, agent.Advance_Amount);
            Assert.IsNull(agent.Ord_Description);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_Entity_DataTypes_Check()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_DataTypes;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData<Entities.MSSQL.DataTypes>(selectSql);
            Assert.AreEqual(1, data.Count);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual(9223372036854775807, dataType.BigInt_Column);
            Assert.AreEqual("EjRWeJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=", ConvertByteArrayToBase64(dataType.Binary_Column));
            Assert.IsTrue(dataType.Bit_Column);
            Assert.AreEqual("CharData", dataType.Char_Column);
            Assert.AreEqual("09/21/2024 00:00:00", ConvertToUSFormat(dataType.Date_Column.ToString()));
            Assert.AreEqual("09/21/2024 08:34:51", ConvertToUSFormat(dataType.DateTime_Column.ToString()));
            Assert.AreEqual("09/21/2024 08:34:51", ConvertToUSFormat(dataType.DateTime2_Column.ToString()));
            Assert.AreEqual("09/20/2024 22:34:51", ConvertToUTCInUSFormat(dataType.DateTimeOffset_Column.ToString()));
            Assert.AreEqual((decimal)123456.78, dataType.Decimal_Column);
            Assert.AreEqual((double)123456.78, dataType.Float_Column);
            Assert.AreEqual("EjRWeJA=", ConvertByteArrayToBase64(dataType.Image_Column));
            Assert.AreEqual(2147483647, dataType.Int_Column);
            Assert.AreEqual((decimal)123456.7800, dataType.Money_Column);
            Assert.AreEqual("NCharData", dataType.NChar_Column);
            Assert.AreEqual("NTextData", dataType.NText_Column);
            Assert.AreEqual((decimal)123456.78, dataType.Numeric_Column);
            Assert.AreEqual("NVarCharData", dataType.NVarChar_Column);
            Assert.AreEqual((float)123.45, dataType.Real_Column);
            Assert.AreEqual("09/21/2024 08:35:00", ConvertToUSFormat(dataType.SmallDateTime_Column.ToString()));
            Assert.AreEqual(32767, dataType.SmallInt_Column);
            Assert.AreEqual((decimal)123456.7800, dataType.SmallMoney_Column);
            Assert.AreEqual("SampleVariant", dataType.SqlVariant_Column.ToString());
            Assert.AreEqual("TextData", dataType.Text_Column);
            Assert.AreEqual("08:34:51", dataType.Time_Column.ToString());
            Assert.AreEqual(255, dataType.TinyInt_Column);
            Assert.AreEqual("12345678-1234-1234-1234-123456789012", dataType.UniqueIdentifier_Column.ToString());
            Assert.AreEqual("EjRWeJA=", ConvertByteArrayToBase64(dataType.VarBinary_Column));
            Assert.AreEqual("VarCharData", dataType.VarChar_Column);
            Assert.AreEqual("<root><element>XmlData</element></root>", dataType.Xml_Column);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_Entity_Strict_Check()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Strict;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData<Entities.MSSQL.Details>(selectSql, strict: true);
            Assert.AreEqual(34, data.Count);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual("A003", dataType.Agent_Code);
            Assert.AreEqual("Alex", dataType.Agent);
            Assert.AreEqual("C00013", dataType.Cust_Code);
            Assert.AreEqual("Holmes", dataType.Customer);
            Assert.AreEqual(200100, dataType.Ord_Num);
            Assert.AreEqual((decimal)1000.00, dataType.Ord_Amount);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_Entity_Strict_Error_Check()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Strict;
            try
            {
                var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData<Entities.MSSQL.Orders>(selectSql, strict: true);
                Assert.Fail("No Exception");
            }
            catch (IndexOutOfRangeException ex)
            {
                Assert.AreEqual("Agent_Name", ex.Message);
            }
        }

        #endregion

        #region Fetch Data Async Tests - << Task<List<T>> FetchDataAsync<T>(string selectSql, bool strict = false) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public async Task Test_MSSQL_FetchDataAsync_Entity_SelectQuery()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql;
            var data = await new DBContext(DB.MSSQL, MSSQLConnectionString).FetchDataAsync<Entities.MSSQL.Agents>(selectSql);
            Assert.AreEqual(12, data.Count);
            var agent = data.FirstOrDefault(X => X.Agent_Name == "Benjamin");
            Assert.AreEqual("A009", agent.Agent_Code);
            Assert.AreEqual("Benjamin", agent.Agent_Name);
            Assert.AreEqual("Hampshair", agent.Working_Area);
            Assert.AreEqual((decimal)0.11, agent.Commission);
            Assert.AreEqual("008-22536178", agent.Phone_No);
            Assert.AreEqual("", agent.Country);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public async Task Test_MSSQL_FetchDataAsync_Entity_SelectQuery_Joins()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Join;
            var data = await new DBContext(DB.MSSQL, MSSQLConnectionString).FetchDataAsync<Entities.MSSQL.Orders>(selectSql);
            Assert.AreEqual(34, data.Count);
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
            Assert.IsNull(agent.Agent);
            Assert.IsNull(agent.Agent_Location);
            Assert.IsNull(agent.Customer);
            Assert.IsNull(agent.Customer_Location);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public async Task Test_MSSQL_FetchDataAsync_Entity_SelectQuery_Aliases()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Alias;
            var data = await new DBContext(DB.MSSQL, MSSQLConnectionString).FetchDataAsync<Entities.MSSQL.Orders>(selectSql);
            Assert.AreEqual(34, data.Count);
            var agent = data.FirstOrDefault(X => X.Agent_Code == "A004" && X.Cust_Code == "C00006");
            Assert.AreEqual("A004", agent.Agent_Code);
            Assert.AreEqual("Ivan", agent.Agent);
            Assert.AreEqual("Torento", agent.Agent_Location);
            Assert.AreEqual("C00006", agent.Cust_Code);
            Assert.AreEqual("Shilton", agent.Customer);
            Assert.AreEqual("Torento", agent.Customer_Location);
            // Non Existent Query Data
            Assert.IsNull(agent.Agent_Name);
            Assert.IsNull(agent.Cust_Name);
            Assert.AreEqual(0, agent.Ord_Num);
            Assert.AreEqual(0, agent.Ord_Amount);
            Assert.AreEqual(0, agent.Advance_Amount);
            Assert.IsNull(agent.Ord_Description);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public async Task Test_MSSQL_FetchDataAsync_Entity_DataTypes_Check()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_DataTypes;
            var data = await new DBContext(DB.MSSQL, MSSQLConnectionString).FetchDataAsync<Entities.MSSQL.DataTypes>(selectSql);
            Assert.AreEqual(1, data.Count);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual(9223372036854775807, dataType.BigInt_Column);
            Assert.AreEqual("EjRWeJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=", ConvertByteArrayToBase64(dataType.Binary_Column));
            Assert.IsTrue(dataType.Bit_Column);
            Assert.AreEqual("CharData", dataType.Char_Column);
            Assert.AreEqual("09/21/2024 00:00:00", ConvertToUSFormat(dataType.Date_Column.ToString()));
            Assert.AreEqual("09/21/2024 08:34:51", ConvertToUSFormat(dataType.DateTime_Column.ToString()));
            Assert.AreEqual("09/21/2024 08:34:51", ConvertToUSFormat(dataType.DateTime2_Column.ToString()));
            Assert.AreEqual("09/20/2024 22:34:51", ConvertToUTCInUSFormat(dataType.DateTimeOffset_Column.ToString()));
            Assert.AreEqual((decimal)123456.78, dataType.Decimal_Column);
            Assert.AreEqual((double)123456.78, dataType.Float_Column);
            Assert.AreEqual("EjRWeJA=", ConvertByteArrayToBase64(dataType.Image_Column));
            Assert.AreEqual(2147483647, dataType.Int_Column);
            Assert.AreEqual((decimal)123456.7800, dataType.Money_Column);
            Assert.AreEqual("NCharData", dataType.NChar_Column);
            Assert.AreEqual("NTextData", dataType.NText_Column);
            Assert.AreEqual((decimal)123456.78, dataType.Numeric_Column);
            Assert.AreEqual("NVarCharData", dataType.NVarChar_Column);
            Assert.AreEqual((float)123.45, dataType.Real_Column);
            Assert.AreEqual("09/21/2024 08:35:00", ConvertToUSFormat(dataType.SmallDateTime_Column.ToString()));
            Assert.AreEqual(32767, dataType.SmallInt_Column);
            Assert.AreEqual((decimal)123456.7800, dataType.SmallMoney_Column);
            Assert.AreEqual("SampleVariant", dataType.SqlVariant_Column.ToString());
            Assert.AreEqual("TextData", dataType.Text_Column);
            Assert.AreEqual("08:34:51", dataType.Time_Column.ToString());
            Assert.AreEqual(255, dataType.TinyInt_Column);
            Assert.AreEqual("12345678-1234-1234-1234-123456789012", dataType.UniqueIdentifier_Column.ToString());
            Assert.AreEqual("EjRWeJA=", ConvertByteArrayToBase64(dataType.VarBinary_Column));
            Assert.AreEqual("VarCharData", dataType.VarChar_Column);
            Assert.AreEqual("<root><element>XmlData</element></root>", dataType.Xml_Column);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public async Task Test_MSSQL_FetchDataAsync_Entity_Strict_Check()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Strict;
            var data = await new DBContext(DB.MSSQL, MSSQLConnectionString).FetchDataAsync<Entities.MSSQL.Details>(selectSql, strict: true);
            Assert.AreEqual(34, data.Count);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual("A003", dataType.Agent_Code);
            Assert.AreEqual("Alex", dataType.Agent);
            Assert.AreEqual("C00013", dataType.Cust_Code);
            Assert.AreEqual("Holmes", dataType.Customer);
            Assert.AreEqual(200100, dataType.Ord_Num);
            Assert.AreEqual((decimal)1000.00, dataType.Ord_Amount);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public async Task Test_MSSQL_FetchDataAsync_Entity_Strict_Error_Check()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Strict;
            try
            {
                var data = await new DBContext(DB.MSSQL, MSSQLConnectionString).FetchDataAsync<Entities.MSSQL.Orders>(selectSql, strict: true);
                Assert.Fail("No Exception");
            }
            catch (IndexOutOfRangeException ex)
            {
                Assert.AreEqual("Agent_Name", ex.Message);
            }
        }

        #endregion

        #region Execute Scalar Tests - << string ExecuteScalar(string sqlStatement) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_ExecuteScalar_As_StringReturn_Scalar_Queries()
        {
            var countOfRecords = Queries.MSSQLQueries.TestDB.ScalarQueries.Count_Of_Records;
            var max = Queries.MSSQLQueries.TestDB.ScalarQueries.Max;
            var min = Queries.MSSQLQueries.TestDB.ScalarQueries.Min;
            var sum = Queries.MSSQLQueries.TestDB.ScalarQueries.Sum;
            var avg = Queries.MSSQLQueries.TestDB.ScalarQueries.Avg;
            var singleValueSelect = Queries.MSSQLQueries.TestDB.ScalarQueries.Single_Value_Select;

            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);

            var count = dbContext.ExecuteScalar(countOfRecords);
            Assert.AreEqual("12", count);
            var maxValue = dbContext.ExecuteScalar(max);
            Assert.AreEqual("10000.00", maxValue);
            var minValue = dbContext.ExecuteScalar(min);
            Assert.AreEqual("3000.00", minValue);
            var sumValue = dbContext.ExecuteScalar(sum);
            Assert.AreEqual("161000.00", sumValue);
            var avgValue = dbContext.ExecuteScalar(avg);
            Assert.AreEqual("6520.000000", avgValue);
            var singleValue = dbContext.ExecuteScalar(singleValueSelect);
            Assert.AreEqual("2", singleValue);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_ExecuteScalar_As_StringReturn_DefaultValue()
        {
            var noValueReturned = Queries.MSSQLQueries.TestDB.ScalarQueries.No_Value_Returned;
            var dBNullValue = Queries.MSSQLQueries.TestDB.ScalarQueries.DB_Null_Value;

            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);

            var result = dbContext.ExecuteScalar(noValueReturned);
            Assert.IsInstanceOfType<string>(result);
            Assert.AreEqual("", result);

            result = dbContext.ExecuteScalar(dBNullValue);
            Assert.IsInstanceOfType<string>(result);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_ExecuteScalar_As_StringReturn_UnsupportedCommands()
        {
            var sqlStatements = new List<string>
            {
                Queries.MSSQLQueries.TestDB.DDL.Create_Table,
                Queries.MSSQLQueries.TestDB.DDL.Alter_Table,
                Queries.MSSQLQueries.TestDB.DDL.Comment_Table,
                Queries.MSSQLQueries.TestDB.DDL.Truncate_Table,
                Queries.MSSQLQueries.TestDB.DDL.Drop_Table,

                Queries.MSSQLQueries.TestDB.DML.InsertSql,
                Queries.MSSQLQueries.TestDB.DML.UpdateSql,
                Queries.MSSQLQueries.TestDB.DML.DeleteSql,

                Queries.MSSQLQueries.TestDB.DCL.GrantSql_Command_Table_User,
                Queries.MSSQLQueries.TestDB.DCL.RevokeSql_Command_Table_User
            };

            foreach (var sqlStatement in sqlStatements)
            {
                try
                {
                    var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);
                    dbContext.ExecuteScalar(sqlStatement);
                    Assert.Fail("No Exception");
                }
                catch (QueryDBException ex)
                {
                    Assert.AreEqual("Only SELECT queries are supported here.", ex.Message);
                    Assert.AreEqual("UnsupportedCommand", ex.ErrorType);
                    Assert.AreEqual("'ExecuteScalar' only supports SELECT queries that have a scalar (single value) return.", ex.AdditionalInfo);
                }
            }
        }

        #endregion

        #region Execute Scalar Async Tests - << Task<string> ExecuteScalarAsync(string sqlStatement) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public async Task Test_MSSQL_ExecuteScalarAsync_As_StringReturn_Scalar_Queries()
        {
            var countOfRecords = Queries.MSSQLQueries.TestDB.ScalarQueries.Count_Of_Records;
            var max = Queries.MSSQLQueries.TestDB.ScalarQueries.Max;
            var min = Queries.MSSQLQueries.TestDB.ScalarQueries.Min;
            var sum = Queries.MSSQLQueries.TestDB.ScalarQueries.Sum;
            var avg = Queries.MSSQLQueries.TestDB.ScalarQueries.Avg;
            var singleValueSelect = Queries.MSSQLQueries.TestDB.ScalarQueries.Single_Value_Select;

            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);

            var count = await dbContext.ExecuteScalarAsync(countOfRecords);
            Assert.AreEqual("12", count);
            var maxValue = await dbContext.ExecuteScalarAsync(max);
            Assert.AreEqual("10000.00", maxValue);
            var minValue = await dbContext.ExecuteScalarAsync(min);
            Assert.AreEqual("3000.00", minValue);
            var sumValue = await dbContext.ExecuteScalarAsync(sum);
            Assert.AreEqual("161000.00", sumValue);
            var avgValue = await dbContext.ExecuteScalarAsync(avg);
            Assert.AreEqual("6520.000000", avgValue);
            var singleValue = await dbContext.ExecuteScalarAsync(singleValueSelect);
            Assert.AreEqual("2", singleValue);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public async Task Test_MSSQL_ExecuteScalarAsync_As_StringReturn_DefaultValue()
        {
            var noValueReturned = Queries.MSSQLQueries.TestDB.ScalarQueries.No_Value_Returned;
            var dBNullValue = Queries.MSSQLQueries.TestDB.ScalarQueries.DB_Null_Value;

            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);

            var result = await dbContext .ExecuteScalarAsync(noValueReturned);
            Assert.IsInstanceOfType<string>(result);
            Assert.AreEqual("", result);

            result = await dbContext.ExecuteScalarAsync(dBNullValue);
            Assert.IsInstanceOfType<string>(result);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public async Task Test_MSSQL_ExecuteScalarAsync_As_StringReturn_UnsupportedCommands()
        {
            var sqlStatements = new List<string>
            {
                Queries.MSSQLQueries.TestDB.DDL.Create_Table,
                Queries.MSSQLQueries.TestDB.DDL.Alter_Table,
                Queries.MSSQLQueries.TestDB.DDL.Comment_Table,
                Queries.MSSQLQueries.TestDB.DDL.Truncate_Table,
                Queries.MSSQLQueries.TestDB.DDL.Drop_Table,

                Queries.MSSQLQueries.TestDB.DML.InsertSql,
                Queries.MSSQLQueries.TestDB.DML.UpdateSql,
                Queries.MSSQLQueries.TestDB.DML.DeleteSql,

                Queries.MSSQLQueries.TestDB.DCL.GrantSql_Command_Table_User,
                Queries.MSSQLQueries.TestDB.DCL.RevokeSql_Command_Table_User
            };

            foreach (var sqlStatement in sqlStatements)
            {
                try
                {
                    var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);
                    await dbContext.ExecuteScalarAsync(sqlStatement);
                    Assert.Fail("No Exception");
                }
                catch (QueryDBException ex)
                {
                    Assert.AreEqual("Only SELECT queries are supported here.", ex.Message);
                    Assert.AreEqual("UnsupportedCommand", ex.ErrorType);
                    Assert.AreEqual("'ExecuteScalar' only supports SELECT queries that have a scalar (single value) return.", ex.AdditionalInfo);
                }
            }
        }

        #endregion

        #region Execute Scalar Tests - << T ExecuteScalar<T>(string sqlStatement) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_ExecuteScalar_As_TypedReturn_Scalar_Queries()
        {
            var countOfRecords = Queries.MSSQLQueries.TestDB.ScalarQueries.Count_Of_Records;
            var max = Queries.MSSQLQueries.TestDB.ScalarQueries.Max;
            var min = Queries.MSSQLQueries.TestDB.ScalarQueries.Min;
            var sum = Queries.MSSQLQueries.TestDB.ScalarQueries.Sum;
            var avg = Queries.MSSQLQueries.TestDB.ScalarQueries.Avg;
            var singleValueSelect = Queries.MSSQLQueries.TestDB.ScalarQueries.Single_Value_Select;

            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);

            var count = dbContext.ExecuteScalar<int>(countOfRecords);
            Assert.AreEqual(12, count);
            var maxValue = dbContext.ExecuteScalar<float>(max);
            Assert.AreEqual(10000.00, maxValue);
            var minValue = dbContext.ExecuteScalar<float>(min);
            Assert.AreEqual(3000.00, minValue);
            var sumValue = dbContext.ExecuteScalar<float>(sum);
            Assert.AreEqual(161000.00, sumValue);
            var avgValue = dbContext.ExecuteScalar<decimal>(avg);
            Assert.AreEqual((decimal)6520.000000, avgValue);
            var singleValue = dbContext.ExecuteScalar<string>(singleValueSelect);
            Assert.AreEqual("2", singleValue);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_ExecuteScalar_As_TypedReturn_DefaultValue()
        {
            var dBNullValue = Queries.MSSQLQueries.TestDB.ScalarQueries.DB_Null_Value;

            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);

            dynamic result = dbContext.ExecuteScalar<int>(dBNullValue);
            Assert.IsInstanceOfType<int>(result);
            Assert.AreEqual(default(int), result);

            result = dbContext.ExecuteScalar<long>(dBNullValue);
            Assert.IsInstanceOfType<long>(result);
            Assert.AreEqual(default(long), result);

            result = dbContext.ExecuteScalar<short>(dBNullValue);
            Assert.IsInstanceOfType<short>(result);
            Assert.AreEqual(default(short), result);

            result = dbContext.ExecuteScalar<uint>(dBNullValue);
            Assert.IsInstanceOfType<uint>(result);
            Assert.AreEqual(default(uint), result);

            result = dbContext.ExecuteScalar<ulong>(dBNullValue);
            Assert.IsInstanceOfType<ulong>(result);
            Assert.AreEqual(default(ulong), result);

            result = dbContext.ExecuteScalar<ushort>(dBNullValue);
            Assert.IsInstanceOfType<ushort>(result);
            Assert.AreEqual(default(ushort), result);

            result = dbContext.ExecuteScalar<decimal>(dBNullValue);
            Assert.IsInstanceOfType<decimal>(result);
            Assert.AreEqual(default(decimal), result);

            result = dbContext.ExecuteScalar<double>(dBNullValue);
            Assert.IsInstanceOfType<double>(result);
            Assert.AreEqual(default(double), result);

            result = dbContext.ExecuteScalar<float>(dBNullValue);
            Assert.IsInstanceOfType<float>(result);
            Assert.AreEqual(default(float), result);

            result = dbContext.ExecuteScalar<byte>(dBNullValue);
            Assert.IsInstanceOfType<byte>(result);
            Assert.AreEqual(default(byte), result);

            result = dbContext.ExecuteScalar<bool>(dBNullValue);
            Assert.IsInstanceOfType<bool>(result);
            Assert.AreEqual(default(bool), result);

            result = dbContext.ExecuteScalar<DateTime>(dBNullValue);
            Assert.IsInstanceOfType<DateTime>(result);
            Assert.AreEqual(default(DateTime), result);

            result = dbContext.ExecuteScalar<Guid>(dBNullValue);
            Assert.IsInstanceOfType<Guid>(result);
            Assert.AreEqual(default(Guid), result);

            result = dbContext.ExecuteScalar<string>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(string), result);

            result = dbContext.ExecuteScalar<int?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(int?), result);

            result = dbContext.ExecuteScalar<long?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(long?), result);

            result = dbContext.ExecuteScalar<short?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(short?), result);

            result = dbContext.ExecuteScalar<decimal?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(decimal?), result);

            result = dbContext.ExecuteScalar<double?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(double?), result);

            result = dbContext.ExecuteScalar<float?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(float?), result);

            result = dbContext.ExecuteScalar<bool?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(bool?), result);

            result = dbContext.ExecuteScalar<DateTime?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(DateTime?), result);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_ExecuteScalar_As_TypedReturn_UnsupportedCommands()
        {
            var sqlStatements = new List<string>
            {
                Queries.MSSQLQueries.TestDB.DDL.Create_Table,
                Queries.MSSQLQueries.TestDB.DDL.Alter_Table,
                Queries.MSSQLQueries.TestDB.DDL.Comment_Table,
                Queries.MSSQLQueries.TestDB.DDL.Truncate_Table,
                Queries.MSSQLQueries.TestDB.DDL.Drop_Table,

                Queries.MSSQLQueries.TestDB.DML.InsertSql,
                Queries.MSSQLQueries.TestDB.DML.UpdateSql,
                Queries.MSSQLQueries.TestDB.DML.DeleteSql,

                Queries.MSSQLQueries.TestDB.DCL.GrantSql_Command_Table_User,
                Queries.MSSQLQueries.TestDB.DCL.RevokeSql_Command_Table_User
            };

            foreach (var sqlStatement in sqlStatements)
            {
                try
                {
                    var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);
                    dbContext.ExecuteScalar<string>(sqlStatement);
                    Assert.Fail("No Exception");
                }
                catch (QueryDBException ex)
                {
                    Assert.AreEqual("Only SELECT queries are supported here.", ex.Message);
                    Assert.AreEqual("UnsupportedCommand", ex.ErrorType);
                    Assert.AreEqual("'ExecuteScalar' only supports SELECT queries that have a scalar (single value) return.", ex.AdditionalInfo);
                }
            }
        }

        #endregion

        #region Execute Scalar Async Tests - << Task<T> ExecuteScalarAsync<T>(string sqlStatement) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public async Task Test_MSSQL_ExecuteScalarAsync_As_TypedReturn_Scalar_Queries()
        {
            var countOfRecords = Queries.MSSQLQueries.TestDB.ScalarQueries.Count_Of_Records;
            var max = Queries.MSSQLQueries.TestDB.ScalarQueries.Max;
            var min = Queries.MSSQLQueries.TestDB.ScalarQueries.Min;
            var sum = Queries.MSSQLQueries.TestDB.ScalarQueries.Sum;
            var avg = Queries.MSSQLQueries.TestDB.ScalarQueries.Avg;
            var singleValueSelect = Queries.MSSQLQueries.TestDB.ScalarQueries.Single_Value_Select;

            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);

            var count = await dbContext.ExecuteScalarAsync<int>(countOfRecords);
            Assert.AreEqual(12, count);
            var maxValue = await dbContext.ExecuteScalarAsync<float>(max);
            Assert.AreEqual(10000.00, maxValue);
            var minValue = await dbContext.ExecuteScalarAsync<float>(min);
            Assert.AreEqual(3000.00, minValue);
            var sumValue = await dbContext.ExecuteScalarAsync<float>(sum);
            Assert.AreEqual(161000.00, sumValue);
            var avgValue = await dbContext.ExecuteScalarAsync<decimal>(avg);
            Assert.AreEqual((decimal)6520.000000, avgValue);
            var singleValue = await dbContext.ExecuteScalarAsync<string>(singleValueSelect);
            Assert.AreEqual("2", singleValue);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public async Task Test_MSSQL_ExecuteScalarAsync_As_TypedReturn_DefaultValue()
        {
            var dBNullValue = Queries.MSSQLQueries.TestDB.ScalarQueries.DB_Null_Value;

            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);

            dynamic result = await dbContext.ExecuteScalarAsync<int>(dBNullValue);
            Assert.IsInstanceOfType<int>(result);
            Assert.AreEqual(default(int), result);

            result = await dbContext.ExecuteScalarAsync<long>(dBNullValue);
            Assert.IsInstanceOfType<long>(result);
            Assert.AreEqual(default(long), result);

            result = await dbContext.ExecuteScalarAsync<short>(dBNullValue);
            Assert.IsInstanceOfType<short>(result);
            Assert.AreEqual(default(short), result);

            result = await dbContext.ExecuteScalarAsync<uint>(dBNullValue);
            Assert.IsInstanceOfType<uint>(result);
            Assert.AreEqual(default(uint), result);

            result = await dbContext.ExecuteScalarAsync<ulong>(dBNullValue);
            Assert.IsInstanceOfType<ulong>(result);
            Assert.AreEqual(default(ulong), result);

            result = await dbContext.ExecuteScalarAsync<ushort>(dBNullValue);
            Assert.IsInstanceOfType<ushort>(result);
            Assert.AreEqual(default(ushort), result);

            result = await dbContext.ExecuteScalarAsync<decimal>(dBNullValue);
            Assert.IsInstanceOfType<decimal>(result);
            Assert.AreEqual(default(decimal), result);

            result = await dbContext.ExecuteScalarAsync<double>(dBNullValue);
            Assert.IsInstanceOfType<double>(result);
            Assert.AreEqual(default(double), result);

            result = await dbContext.ExecuteScalarAsync<float>(dBNullValue);
            Assert.IsInstanceOfType<float>(result);
            Assert.AreEqual(default(float), result);

            result = await dbContext.ExecuteScalarAsync<byte>(dBNullValue);
            Assert.IsInstanceOfType<byte>(result);
            Assert.AreEqual(default(byte), result);

            result = await dbContext.ExecuteScalarAsync<bool>(dBNullValue);
            Assert.IsInstanceOfType<bool>(result);
            Assert.AreEqual(default(bool), result);

            result = await dbContext.ExecuteScalarAsync<DateTime>(dBNullValue);
            Assert.IsInstanceOfType<DateTime>(result);
            Assert.AreEqual(default(DateTime), result);

            result = await dbContext.ExecuteScalarAsync<Guid>(dBNullValue);
            Assert.IsInstanceOfType<Guid>(result);
            Assert.AreEqual(default(Guid), result);

            result = await dbContext.ExecuteScalarAsync<string>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(string), result);

            result = await dbContext.ExecuteScalarAsync<int?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(int?), result);

            result = await dbContext.ExecuteScalarAsync<long?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(long?), result);

            result = await dbContext.ExecuteScalarAsync<short?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(short?), result);

            result = await dbContext.ExecuteScalarAsync<decimal?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(decimal?), result);

            result = await dbContext.ExecuteScalarAsync<double?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(double?), result);

            result = await dbContext.ExecuteScalarAsync<float?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(float?), result);

            result = await dbContext.ExecuteScalarAsync<bool?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(bool?), result);

            result = await dbContext.ExecuteScalarAsync<DateTime?>(dBNullValue);
            Assert.IsNull(result);
            Assert.AreEqual(default(DateTime?), result);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public async Task Test_MSSQL_ExecuteScalarAsync_As_TypedReturn_UnsupportedCommands()
        {
            var sqlStatements = new List<string>
            {
                Queries.MSSQLQueries.TestDB.DDL.Create_Table,
                Queries.MSSQLQueries.TestDB.DDL.Alter_Table,
                Queries.MSSQLQueries.TestDB.DDL.Comment_Table,
                Queries.MSSQLQueries.TestDB.DDL.Truncate_Table,
                Queries.MSSQLQueries.TestDB.DDL.Drop_Table,

                Queries.MSSQLQueries.TestDB.DML.InsertSql,
                Queries.MSSQLQueries.TestDB.DML.UpdateSql,
                Queries.MSSQLQueries.TestDB.DML.DeleteSql,

                Queries.MSSQLQueries.TestDB.DCL.GrantSql_Command_Table_User,
                Queries.MSSQLQueries.TestDB.DCL.RevokeSql_Command_Table_User
            };

            foreach (var sqlStatement in sqlStatements)
            {
                try
                {
                    var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);
                    await dbContext.ExecuteScalarAsync<string>(sqlStatement);
                    Assert.Fail("No Exception");
                }
                catch (QueryDBException ex)
                {
                    Assert.AreEqual("Only SELECT queries are supported here.", ex.Message);
                    Assert.AreEqual("UnsupportedCommand", ex.ErrorType);
                    Assert.AreEqual("'ExecuteScalar' only supports SELECT queries that have a scalar (single value) return.", ex.AdditionalInfo);
                }
            }
        }

        #endregion

        #region Execute Command Tests - << int ExecuteCommand(string sqlStatement) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_ExecuteCommand_DDL_Queries()
        {
            var createTableSql = Queries.MSSQLQueries.TestDB.DDL.Create_Table;
            var alterTableSql = Queries.MSSQLQueries.TestDB.DDL.Alter_Table;
            var commentTableSql = Queries.MSSQLQueries.TestDB.DDL.Comment_Table;
            var commentTableColumnSql = Queries.MSSQLQueries.TestDB.DDL.Comment_Table_Column;
            var truncateTableSql = Queries.MSSQLQueries.TestDB.DDL.Truncate_Table;
            var renameTableSql = Queries.MSSQLQueries.TestDB.DDL.Rename_Table;
            var dropTableSql = Queries.MSSQLQueries.TestDB.DDL.Drop_Table;
            var dDLExecutionCheckSql = Queries.MSSQLQueries.TestDB.DDL.DDL_Execute_check;
            var dDLTableCommentCheckSql = Queries.MSSQLQueries.TestDB.DDL.DDL_Table_Comment_check;
            var dDLTableColumnCommentCheckSql = Queries.MSSQLQueries.TestDB.DDL.DDL_Table_Column_Comment_check;

            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);
            dbContext.ExecuteCommand(createTableSql);
            dbContext.ExecuteCommand(alterTableSql);
            dbContext.ExecuteCommand(commentTableSql);
            dbContext.ExecuteCommand(commentTableColumnSql);
            dbContext.ExecuteCommand(truncateTableSql);

            var tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "dbo", "Employee"));
            Assert.AreEqual("1", tableCount[0].ReferenceData["Table_Count"]);
            var tableComment = dbContext
                .FetchData(string.Format(dDLTableCommentCheckSql, "dbo", "Employee"));
            Assert.AreEqual("This table stores employee records", tableComment[0].ReferenceData["Table_Comment"]);
            var tableColumnComment = dbContext
                .FetchData(string.Format(dDLTableColumnCommentCheckSql, "dbo", "Employee"));
            Assert.AreEqual("This column stores employee middle name", tableColumnComment[0].ReferenceData["Table_Column_Comment"]);

            dbContext.ExecuteCommand(renameTableSql);

            tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "dbo", "Employee"));
            Assert.AreEqual("0", tableCount[0].ReferenceData["Table_Count"]);
            tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "dbo", "Employees"));
            Assert.AreEqual("1", tableCount[0].ReferenceData["Table_Count"]);

            dbContext.ExecuteCommand(dropTableSql);

            tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "dbo", "Employees"));
            Assert.AreEqual("0", tableCount[0].ReferenceData["Table_Count"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_ExecuteCommand_DML_Queries()
        {
            var insertSql = Queries.MSSQLQueries.TestDB.DML.InsertSql;
            var updateSql = Queries.MSSQLQueries.TestDB.DML.UpdateSql;
            var deleteSql = Queries.MSSQLQueries.TestDB.DML.DeleteSql;
            var verifyDMLExecution = Queries.MSSQLQueries.TestDB.DML.VerifyDMLExecution;

            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);

            // Insert
            var rows = dbContext.ExecuteCommand(insertSql);
            Assert.AreEqual(1, rows);
            var data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(1, data.Count);
            var agent = data.FirstOrDefault();
            Assert.AreEqual("A020", agent.ReferenceData["Agent_Code"]);
            Assert.AreEqual("John", agent.ReferenceData["Agent_Name"]);
            Assert.AreEqual("Wick", agent.ReferenceData["Working_Area"]);
            Assert.AreEqual("0.11", agent.ReferenceData["Commission"]);
            Assert.AreEqual("010-44536178", agent.ReferenceData["Phone_No"]);
            Assert.AreEqual("", agent.ReferenceData["Country"]);

            // Update
            rows = dbContext.ExecuteCommand(updateSql);
            Assert.AreEqual(1, rows);
            data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(1, data.Count);
            agent = data.FirstOrDefault();
            Assert.AreEqual("A020", agent.ReferenceData["Agent_Code"]);
            Assert.AreEqual("John", agent.ReferenceData["Agent_Name"]);
            Assert.AreEqual("Wick", agent.ReferenceData["Working_Area"]);
            Assert.AreEqual("0.15", agent.ReferenceData["Commission"]);
            Assert.AreEqual("010-44536178", agent.ReferenceData["Phone_No"]);
            Assert.AreEqual("", agent.ReferenceData["Country"]);

            // Delete
            rows = dbContext.ExecuteCommand(deleteSql);
            Assert.AreEqual(1, rows);
            data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(0, data.Count);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_ExecuteCommand_DML_Unsupported_SELECT_Queries()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.DML.SelectSql;

            // Select
            try
            {
                var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);
                var rows = dbContext.ExecuteCommand(selectSql);
                Assert.Fail("No Exception");
            }
            catch (QueryDBException ex)
            {
                Assert.AreEqual("SELECT queries are not supported here.", ex.Message);
                Assert.AreEqual("UnsupportedCommand", ex.ErrorType);
                Assert.AreEqual("'ExecuteCommand' doesn't support SELECT queries.", ex.AdditionalInfo);
            }
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_ExecuteCommand_DCL_Queries()
        {
            var login = "test_user";
            var user = "test_user";
            var password = "Test@1234";
            var table = "agents";
            var commands = "SELECT, UPDATE";
            var checkCommand = "SELECT";

            var createLogin = string.Format(Queries.MSSQLQueries.TestDB.DCL.CreateLoginSql_Login_Password, login, password);
            var createUser = string.Format(Queries.MSSQLQueries.TestDB.DCL.CreateUserSql_User_Login, user, login);
            var grantSql = string.Format(Queries.MSSQLQueries.TestDB.DCL.GrantSql_Command_Table_User, commands, table, user);
            var revokeSql = string.Format(Queries.MSSQLQueries.TestDB.DCL.RevokeSql_Command_Table_User, commands, table, user);
            var verifyPermissions = string.Format(Queries.MSSQLQueries.TestDB.DCL.VerifyPermission_User_Table_Command, user, table, checkCommand);
            var removeUser = string.Format(Queries.MSSQLQueries.TestDB.DCL.RemoveUserSql_User, user);
            var removeLogin = string.Format(Queries.MSSQLQueries.TestDB.DCL.RemoveLoginSql_Login, login);

            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);

            // Create Login
            var result = dbContext.ExecuteCommand(createLogin);
            Assert.AreEqual(-1, result);

            // Create User
            result = dbContext.ExecuteCommand(createUser);
            Assert.AreEqual(-1, result);

            // Existing Permissions
            var data = dbContext.FetchData(verifyPermissions).FirstOrDefault();
            Assert.AreEqual("0", data.ReferenceData["HasPermission"]);

            // Grant
            result = dbContext.ExecuteCommand(grantSql);
            Assert.AreEqual(-1, result);
            data = dbContext.FetchData(verifyPermissions).FirstOrDefault();
            Assert.AreEqual("1", data.ReferenceData["HasPermission"]);

            // Revoke
            result = dbContext.ExecuteCommand(revokeSql);
            Assert.AreEqual(-1, result);
            data = dbContext.FetchData(verifyPermissions).FirstOrDefault();
            Assert.AreEqual("0", data.ReferenceData["HasPermission"]);

            // Remove User
            result = dbContext.ExecuteCommand(removeUser);
            Assert.AreEqual(-1, result);

            // Remove Login
            result = dbContext.ExecuteCommand(removeLogin);
            Assert.AreEqual(-1, result);
        }

        #endregion

        #region Execute Transaction Tests - << bool ExecuteTransaction(List<string> sqlStatements) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_ExecuteTransaction_DDL_Multiple_Queries()
        {
            var createTableSql = Queries.MSSQLQueries.TestDB.DDL.Create_Table;
            var alterTableSql = Queries.MSSQLQueries.TestDB.DDL.Alter_Table;
            var truncateTableSql = Queries.MSSQLQueries.TestDB.DDL.Truncate_Table;
            var renameTableSql = Queries.MSSQLQueries.TestDB.DDL.Rename_Table;
            var dropTableSql = Queries.MSSQLQueries.TestDB.DDL.Drop_Table;
            var dDLExecutionCheckSql = Queries.MSSQLQueries.TestDB.DDL.DDL_Execute_check;

            // Create, Alter & Truncate
            var statements = new List<string>
            {
                createTableSql,
                alterTableSql,
                truncateTableSql
            };
            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);
            var result = dbContext.ExecuteTransaction(statements);
            Assert.IsTrue(result);

            var tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "dbo", "Employee"));
            Assert.AreEqual("1", tableCount[0].ReferenceData["Table_Count"]);

            // Rename & Drop
            statements = new List<string>
            {
                renameTableSql,
                dropTableSql
            };
            result = dbContext.ExecuteTransaction(statements);
            Assert.IsTrue(result);

            tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "dbo", "Employees"));
            Assert.AreEqual("0", tableCount[0].ReferenceData["Table_Count"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_ExecuteTransaction_DML_Multiple_Queries()
        {
            var insertSql = Queries.MSSQLQueries.TestDB.DML.InsertSql;
            var updateSql = Queries.MSSQLQueries.TestDB.DML.UpdateSql;
            var deleteSql = Queries.MSSQLQueries.TestDB.DML.DeleteSql;
            var verifyDMLExecution = Queries.MSSQLQueries.TestDB.DML.VerifyDMLExecution;

            var statements = new List<string>
            {
                insertSql,
                updateSql
            };
            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);

            // Insert & Update
            var result = dbContext.ExecuteTransaction(statements);
            Assert.IsTrue(result);
            var data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(1, data.Count);
            var agent = data.FirstOrDefault();
            Assert.AreEqual("A020", agent.ReferenceData["Agent_Code"]);
            Assert.AreEqual("John", agent.ReferenceData["Agent_Name"]);
            Assert.AreEqual("Wick", agent.ReferenceData["Working_Area"]);
            Assert.AreEqual("0.15", agent.ReferenceData["Commission"]);
            Assert.AreEqual("010-44536178", agent.ReferenceData["Phone_No"]);
            Assert.AreEqual("", agent.ReferenceData["Country"]);

            // Delete
            statements = new List<string>
            {
                deleteSql
            };
            result = dbContext.ExecuteTransaction(statements);
            Assert.IsTrue(result);
            data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(0, data.Count);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_ExecuteTransaction_Incomplete_Transaction_Rollback_On_Error()
        {
            var insertSql = Queries.MSSQLQueries.TestDB.DML.InsertSql;
            var updateSql = Queries.MSSQLQueries.TestDB.DML.UpdateSql;
            var updateErrorSql = "UPDATE";
            var verifyDMLExecution = Queries.MSSQLQueries.TestDB.DML.VerifyDMLExecution;

            var statements = new List<string>
            {
                insertSql,
                updateSql,
                updateErrorSql
            };
            var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);

            // Insert & Update
            var result = dbContext.ExecuteTransaction(statements);
            Assert.IsFalse(result);
            var data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(0, data.Count);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_ExecuteTransaction_DML_Unsupported_SELECT_Queries()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.DML.SelectSql;

            // Select
            try
            {
                var statements = new List<string>
                {
                    selectSql
                };
                var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);
                var result = dbContext.ExecuteTransaction(statements);
                Assert.Fail("No Exception");
            }
            catch (QueryDBException ex)
            {
                Assert.AreEqual("SELECT queries are not supported here.", ex.Message);
                Assert.AreEqual("UnsupportedCommand", ex.ErrorType);
                Assert.AreEqual("'ExecuteTransaction' doesn't support SELECT queries.", ex.AdditionalInfo);
            }
        }

        #endregion

        #endregion

    }
}
