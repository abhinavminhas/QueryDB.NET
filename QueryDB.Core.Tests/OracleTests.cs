using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryDB.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryDB.Core.Tests
{
    [TestClass]
    public class OracleTests : TestBase
    {

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

        #region Fetch Data Tests - << List<DataDictionary> FetchData(string selectSql, bool upperCaseKeys = false) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Dictionary_SelectQuery()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Dictionary_SelectQuery_UpperCaseKeys()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData(selectSql, upperCaseKeys: true);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Dictionary_SelectQuery_Joins()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData(selectSql);
            Assert.AreEqual(34, data.Count);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_CODE"] == "A004" && X.ReferenceData["CUST_CODE"] == "C00006");
            Assert.AreEqual("A004", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("C00006", agent.ReferenceData["CUST_CODE"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["CUST_NAME"]);
            Assert.AreEqual("200104", agent.ReferenceData["ORD_NUM"]);
            Assert.AreEqual("1500", agent.ReferenceData["ORD_AMOUNT"]);
            Assert.AreEqual("500", agent.ReferenceData["ADVANCE_AMOUNT"]);
            Assert.AreEqual("SOD", agent.ReferenceData["ORD_DESCRIPTION"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Dictionary_SelectQuery_Joins_UpperCaseKeys()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData(selectSql, upperCaseKeys: true);
            Assert.AreEqual(34, data.Count);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_CODE"] == "A004" && X.ReferenceData["CUST_CODE"] == "C00006");
            Assert.AreEqual("A004", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("C00006", agent.ReferenceData["CUST_CODE"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["CUST_NAME"]);
            Assert.AreEqual("200104", agent.ReferenceData["ORD_NUM"]);
            Assert.AreEqual("1500", agent.ReferenceData["ORD_AMOUNT"]);
            Assert.AreEqual("500", agent.ReferenceData["ADVANCE_AMOUNT"]);
            Assert.AreEqual("SOD", agent.ReferenceData["ORD_DESCRIPTION"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Dictionary_SelectQuery_Aliases()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Dictionary_SelectQuery_Aliases_UpperCaseKeys()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData(selectSql, upperCaseKeys: true);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Dictionary_DataTypes_Check()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_DataTypes;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData(selectSql);
            Assert.AreEqual(2, data.Count);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual(GetBase64Content(Environment.CurrentDirectory + "/SeedData/oracle.sql"), dataType.ReferenceData["BFILE_COLUMN"]);
            Assert.AreEqual("3q2+7w==", dataType.ReferenceData["BLOB_COLUMN"]);
            Assert.AreEqual("A", dataType.ReferenceData["CHAR_COLUMN"]);
            Assert.AreEqual("Sample CLOB data", dataType.ReferenceData["CLOB_COLUMN"]);
            Assert.AreEqual("09/21/2024 00:00:00", ConvertToUSFormat(dataType.ReferenceData["DATE_COLUMN"]));
            Assert.AreEqual("123.45", dataType.ReferenceData["FLOAT_COLUMN"]);
            Assert.AreEqual("123", dataType.ReferenceData["INTEGER_COLUMN"]);
            Assert.AreEqual("14", dataType.ReferenceData["INTERVALYEARTOMONTH_COLUMN"]);
            Assert.AreEqual("1.02:03:04.5000000", dataType.ReferenceData["INTERNALDAYTOSECOND_COLUMN"]);
            Assert.AreEqual("Sample LONG data", dataType.ReferenceData["LONG_COLUMN"]);
            Assert.AreEqual("A", dataType.ReferenceData["NCHAR_COLUMN"]);
            Assert.AreEqual("Sample NCLOB data", dataType.ReferenceData["NCLOB_COLUMN"]);
            Assert.AreEqual("123.45", dataType.ReferenceData["NUMBER_COLUMN"]);
            Assert.AreEqual("Sample NVARCHAR2 data", dataType.ReferenceData["NVARCHAR2_COLUMN"]);
            Assert.AreEqual("3q2+7w==", dataType.ReferenceData["RAW_COLUMN"]);
            Assert.AreEqual("09/21/2024 12:34:56", ConvertToUSFormat(dataType.ReferenceData["TIMESTAMP_COLUMN"]));
            Assert.AreEqual("09/21/2024 12:34:56", ConvertToUSFormat(dataType.ReferenceData["TIMESTAMPWITHTIMEZONE_COLUMN"]));
            Assert.AreEqual("09/21/2024 12:34:56", ConvertToUSFormat(dataType.ReferenceData["TIMESTAMPWITHLOCALTIMEZONE_COLUMN"]));
            Assert.AreEqual("Sample VARCHAR data", dataType.ReferenceData["VARCHAR_COLUMN"]);
            Assert.AreEqual("Sample VARCHAR2 data", dataType.ReferenceData["VARCHAR2_COLUMN"]);
        }

        #endregion

        #region Fetch Data Async Tests - << List<DataDictionary> FetchData(string selectSql, bool upperCaseKeys = false) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_FetchDataAsync_Dictionary_SelectQuery()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql;
            var data = await new DBContext(DB.Oracle, OracleConnectionString).FetchDataAsync(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_FetchDataAsync_Dictionary_SelectQuery_UpperCaseKeys()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql;
            var data = await new DBContext(DB.Oracle, OracleConnectionString).FetchDataAsync(selectSql, upperCaseKeys: true);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_FetchDataAsync_Dictionary_SelectQuery_Joins()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Join;
            var data = await new DBContext(DB.Oracle, OracleConnectionString).FetchDataAsync(selectSql);
            Assert.AreEqual(34, data.Count);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_CODE"] == "A004" && X.ReferenceData["CUST_CODE"] == "C00006");
            Assert.AreEqual("A004", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("C00006", agent.ReferenceData["CUST_CODE"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["CUST_NAME"]);
            Assert.AreEqual("200104", agent.ReferenceData["ORD_NUM"]);
            Assert.AreEqual("1500", agent.ReferenceData["ORD_AMOUNT"]);
            Assert.AreEqual("500", agent.ReferenceData["ADVANCE_AMOUNT"]);
            Assert.AreEqual("SOD", agent.ReferenceData["ORD_DESCRIPTION"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_FetchDataAsync_Dictionary_SelectQuery_Joins_UpperCaseKeys()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Join;
            var data = await new DBContext(DB.Oracle, OracleConnectionString).FetchDataAsync(selectSql, upperCaseKeys: true);
            Assert.AreEqual(34, data.Count);
            var agent = data.FirstOrDefault(X => X.ReferenceData["AGENT_CODE"] == "A004" && X.ReferenceData["CUST_CODE"] == "C00006");
            Assert.AreEqual("A004", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("Ivan", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("C00006", agent.ReferenceData["CUST_CODE"]);
            Assert.AreEqual("Shilton", agent.ReferenceData["CUST_NAME"]);
            Assert.AreEqual("200104", agent.ReferenceData["ORD_NUM"]);
            Assert.AreEqual("1500", agent.ReferenceData["ORD_AMOUNT"]);
            Assert.AreEqual("500", agent.ReferenceData["ADVANCE_AMOUNT"]);
            Assert.AreEqual("SOD", agent.ReferenceData["ORD_DESCRIPTION"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_FetchDataAsync_Dictionary_SelectQuery_Aliases()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Alias;
            var data = await new DBContext(DB.Oracle, OracleConnectionString).FetchDataAsync(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_FetchDataAsync_Dictionary_SelectQuery_Aliases_UpperCaseKeys()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Alias;
            var data = await new DBContext(DB.Oracle, OracleConnectionString).FetchDataAsync(selectSql, upperCaseKeys: true);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_FetchDataAsync_Dictionary_DataTypes_Check()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_DataTypes;
            var data = await new DBContext(DB.Oracle, OracleConnectionString).FetchDataAsync(selectSql);
            Assert.AreEqual(2, data.Count);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual(GetBase64Content(Environment.CurrentDirectory + "/SeedData/oracle.sql"), dataType.ReferenceData["BFILE_COLUMN"]);
            Assert.AreEqual("3q2+7w==", dataType.ReferenceData["BLOB_COLUMN"]);
            Assert.AreEqual("A", dataType.ReferenceData["CHAR_COLUMN"]);
            Assert.AreEqual("Sample CLOB data", dataType.ReferenceData["CLOB_COLUMN"]);
            Assert.AreEqual("09/21/2024 00:00:00", ConvertToUSFormat(dataType.ReferenceData["DATE_COLUMN"]));
            Assert.AreEqual("123.45", dataType.ReferenceData["FLOAT_COLUMN"]);
            Assert.AreEqual("123", dataType.ReferenceData["INTEGER_COLUMN"]);
            Assert.AreEqual("14", dataType.ReferenceData["INTERVALYEARTOMONTH_COLUMN"]);
            Assert.AreEqual("1.02:03:04.5000000", dataType.ReferenceData["INTERNALDAYTOSECOND_COLUMN"]);
            Assert.AreEqual("Sample LONG data", dataType.ReferenceData["LONG_COLUMN"]);
            Assert.AreEqual("A", dataType.ReferenceData["NCHAR_COLUMN"]);
            Assert.AreEqual("Sample NCLOB data", dataType.ReferenceData["NCLOB_COLUMN"]);
            Assert.AreEqual("123.45", dataType.ReferenceData["NUMBER_COLUMN"]);
            Assert.AreEqual("Sample NVARCHAR2 data", dataType.ReferenceData["NVARCHAR2_COLUMN"]);
            Assert.AreEqual("3q2+7w==", dataType.ReferenceData["RAW_COLUMN"]);
            Assert.AreEqual("09/21/2024 12:34:56", ConvertToUSFormat(dataType.ReferenceData["TIMESTAMP_COLUMN"]));
            Assert.AreEqual("09/21/2024 12:34:56", ConvertToUSFormat(dataType.ReferenceData["TIMESTAMPWITHTIMEZONE_COLUMN"]));
            Assert.AreEqual("09/21/2024 12:34:56", ConvertToUSFormat(dataType.ReferenceData["TIMESTAMPWITHLOCALTIMEZONE_COLUMN"]));
            Assert.AreEqual("Sample VARCHAR data", dataType.ReferenceData["VARCHAR_COLUMN"]);
            Assert.AreEqual("Sample VARCHAR2 data", dataType.ReferenceData["VARCHAR2_COLUMN"]);
        }

        #endregion

        #region Fetch Data Tests - << List<T> FetchData<T>(string selectSql, bool strict = false) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Entity_SelectQuery()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData<Entities.Oracle.Agents>(selectSql);
            Assert.AreEqual(12, data.Count);
            var agent = data.FirstOrDefault(X => X.Agent_Name == "Benjamin");
            Assert.AreEqual("A009", agent.Agent_Code);
            Assert.AreEqual("Benjamin", agent.Agent_Name);
            Assert.AreEqual("Hampshair", agent.Working_Area);
            Assert.AreEqual(0.11, agent.Commission);
            Assert.AreEqual("008-22536178", agent.Phone_No);
            Assert.IsNull(agent.Country);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Entity_SelectQuery_Joins()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData<Entities.Oracle.Orders>(selectSql);
            Assert.AreEqual(34, data.Count);
            var agent = data.FirstOrDefault(X => X.Agent_Code == "A004" && X.Cust_Code == "C00006");
            Assert.AreEqual("A004", agent.Agent_Code);
            Assert.AreEqual("Ivan", agent.Agent_Name);
            Assert.AreEqual("C00006", agent.Cust_Code);
            Assert.AreEqual("Shilton", agent.Cust_Name);
            Assert.AreEqual(200104, agent.Ord_Num);
            Assert.AreEqual(1500.00, agent.Ord_Amount);
            Assert.AreEqual(500.00, agent.Advance_Amount);
            Assert.AreEqual("SOD", agent.Ord_Description);
            // Non Existent Query Data
            Assert.IsNull(agent.Agent);
            Assert.IsNull(agent.Agent_Location);
            Assert.IsNull(agent.Customer);
            Assert.IsNull(agent.Customer_Location);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Entity_SelectQuery_Aliases()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData<Entities.Oracle.Orders>(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Entity_DataTypes_Check()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_DataTypes;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData<Entities.Oracle.DataTypes>(selectSql);
            Assert.AreEqual(2, data.Count);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual(GetBase64Content(Environment.CurrentDirectory + "/SeedData/oracle.sql"), ConvertByteArrayToBase64(dataType.BFile_Column));
            Assert.AreEqual("3q2+7w==", ConvertByteArrayToBase64(dataType.Blob_Column));
            Assert.AreEqual("A", dataType.Char_Column);
            Assert.AreEqual("Sample CLOB data", dataType.Clob_Column);
            Assert.AreEqual("09/21/2024 00:00:00", ConvertToUSFormat(dataType.Date_Column.ToString()));
            Assert.AreEqual((decimal)123.45, dataType.Float_Column);
            Assert.AreEqual(123, (int)dataType.Integer_Column);
            Assert.AreEqual(14, dataType.IntervalYearToMonth_Column);
            Assert.AreEqual("1.02:03:04.5000000", dataType.InternalDayToSecond_Column.ToString());
            Assert.AreEqual("Sample LONG data", dataType.Long_Column);
            Assert.AreEqual("A", dataType.NChar_Column);
            Assert.AreEqual("Sample NCLOB data", dataType.NClob_Column);
            Assert.AreEqual((decimal)123.45, dataType.Number_Column);
            Assert.AreEqual("Sample NVARCHAR2 data", dataType.NVarchar2_Column);
            Assert.AreEqual("3q2+7w==", ConvertByteArrayToBase64(dataType.Raw_Column));
            Assert.AreEqual("09/21/2024 12:34:56", ConvertToUSFormat(dataType.Timestamp_Column.ToString()));
            Assert.AreEqual("09/21/2024 12:34:56", ConvertToUSFormat(dataType.TimestampWithTimeZone_Column.ToString()));
            Assert.AreEqual("09/21/2024 12:34:56", ConvertToUSFormat(dataType.TimestampWithLocalTimeZone_Column.ToString()));
            Assert.AreEqual("Sample VARCHAR data", dataType.Varchar_Column);
            Assert.AreEqual("Sample VARCHAR2 data", dataType.Varchar2_Column);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Entity_Strict_Check()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Strict;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData<Entities.Oracle.Details>(selectSql, strict: true);
            Assert.AreEqual(34, data.Count);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual("A003", dataType.Agent_Code);
            Assert.AreEqual("Alex", dataType.Agent);
            Assert.AreEqual("C00013", dataType.Cust_Code);
            Assert.AreEqual("Holmes", dataType.Customer);
            Assert.AreEqual(200100, dataType.Ord_Num);
            Assert.AreEqual(1000.00, dataType.Ord_Amount);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Entity_Strict_Error_Check()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Strict;
            try
            {
                var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData<Entities.Oracle.Orders>(selectSql, strict: true);
                Assert.Fail("No Exception");
            }
            catch (IndexOutOfRangeException ex)
            {
                Assert.AreEqual("Unable to find specified column in result set", ex.Message);
            }
        }

        #endregion

        #region Fetch Data Async Tests - << List<T> FetchData<T>(string selectSql, bool strict = false) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_FetchDataAsync_Entity_SelectQuery()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql;
            var data = await new DBContext(DB.Oracle, OracleConnectionString).FetchDataAsync<Entities.Oracle.Agents>(selectSql);
            Assert.AreEqual(12, data.Count);
            var agent = data.FirstOrDefault(X => X.Agent_Name == "Benjamin");
            Assert.AreEqual("A009", agent.Agent_Code);
            Assert.AreEqual("Benjamin", agent.Agent_Name);
            Assert.AreEqual("Hampshair", agent.Working_Area);
            Assert.AreEqual(0.11, agent.Commission);
            Assert.AreEqual("008-22536178", agent.Phone_No);
            Assert.IsNull(agent.Country);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_FetchDataAsync_Entity_SelectQuery_Joins()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Join;
            var data = await new DBContext(DB.Oracle, OracleConnectionString).FetchDataAsync<Entities.Oracle.Orders>(selectSql);
            Assert.AreEqual(34, data.Count);
            var agent = data.FirstOrDefault(X => X.Agent_Code == "A004" && X.Cust_Code == "C00006");
            Assert.AreEqual("A004", agent.Agent_Code);
            Assert.AreEqual("Ivan", agent.Agent_Name);
            Assert.AreEqual("C00006", agent.Cust_Code);
            Assert.AreEqual("Shilton", agent.Cust_Name);
            Assert.AreEqual(200104, agent.Ord_Num);
            Assert.AreEqual(1500.00, agent.Ord_Amount);
            Assert.AreEqual(500.00, agent.Advance_Amount);
            Assert.AreEqual("SOD", agent.Ord_Description);
            // Non Existent Query Data
            Assert.IsNull(agent.Agent);
            Assert.IsNull(agent.Agent_Location);
            Assert.IsNull(agent.Customer);
            Assert.IsNull(agent.Customer_Location);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_FetchDataAsync_Entity_SelectQuery_Aliases()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Alias;
            var data = await new DBContext(DB.Oracle, OracleConnectionString).FetchDataAsync<Entities.Oracle.Orders>(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_FetchDataAsync_Entity_DataTypes_Check()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_DataTypes;
            var data = await new DBContext(DB.Oracle, OracleConnectionString).FetchDataAsync<Entities.Oracle.DataTypes>(selectSql);
            Assert.AreEqual(2, data.Count);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual(GetBase64Content(Environment.CurrentDirectory + "/SeedData/oracle.sql"), ConvertByteArrayToBase64(dataType.BFile_Column));
            Assert.AreEqual("3q2+7w==", ConvertByteArrayToBase64(dataType.Blob_Column));
            Assert.AreEqual("A", dataType.Char_Column);
            Assert.AreEqual("Sample CLOB data", dataType.Clob_Column);
            Assert.AreEqual("09/21/2024 00:00:00", ConvertToUSFormat(dataType.Date_Column.ToString()));
            Assert.AreEqual((decimal)123.45, dataType.Float_Column);
            Assert.AreEqual(123, (int)dataType.Integer_Column);
            Assert.AreEqual(14, dataType.IntervalYearToMonth_Column);
            Assert.AreEqual("1.02:03:04.5000000", dataType.InternalDayToSecond_Column.ToString());
            Assert.AreEqual("Sample LONG data", dataType.Long_Column);
            Assert.AreEqual("A", dataType.NChar_Column);
            Assert.AreEqual("Sample NCLOB data", dataType.NClob_Column);
            Assert.AreEqual((decimal)123.45, dataType.Number_Column);
            Assert.AreEqual("Sample NVARCHAR2 data", dataType.NVarchar2_Column);
            Assert.AreEqual("3q2+7w==", ConvertByteArrayToBase64(dataType.Raw_Column));
            Assert.AreEqual("09/21/2024 12:34:56", ConvertToUSFormat(dataType.Timestamp_Column.ToString()));
            Assert.AreEqual("09/21/2024 12:34:56", ConvertToUSFormat(dataType.TimestampWithTimeZone_Column.ToString()));
            Assert.AreEqual("09/21/2024 12:34:56", ConvertToUSFormat(dataType.TimestampWithLocalTimeZone_Column.ToString()));
            Assert.AreEqual("Sample VARCHAR data", dataType.Varchar_Column);
            Assert.AreEqual("Sample VARCHAR2 data", dataType.Varchar2_Column);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_FetchDataAsync_Entity_Strict_Check()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Strict;
            var data = await new DBContext(DB.Oracle, OracleConnectionString).FetchDataAsync<Entities.Oracle.Details>(selectSql, strict: true);
            Assert.AreEqual(34, data.Count);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual("A003", dataType.Agent_Code);
            Assert.AreEqual("Alex", dataType.Agent);
            Assert.AreEqual("C00013", dataType.Cust_Code);
            Assert.AreEqual("Holmes", dataType.Customer);
            Assert.AreEqual(200100, dataType.Ord_Num);
            Assert.AreEqual(1000.00, dataType.Ord_Amount);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_FetchDataAsync_Entity_Strict_Error_Check()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Strict;
            try
            {
                var data = await new DBContext(DB.Oracle, OracleConnectionString).FetchDataAsync<Entities.Oracle.Orders>(selectSql, strict: true);
                Assert.Fail("No Exception");
            }
            catch (IndexOutOfRangeException ex)
            {
                Assert.AreEqual("Unable to find specified column in result set", ex.Message);
            }
        }

        #endregion

        #region Execute Scalar Tests - << string ExecuteScalar(string sqlStatement) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_ExecuteScalar_As_StringReturn_Scalar_Queries()
        {
            var countOfRecords = Queries.OracleQueries.TestDB.ScalarQueries.Count_Of_Records;
            var max = Queries.OracleQueries.TestDB.ScalarQueries.Max;
            var min = Queries.OracleQueries.TestDB.ScalarQueries.Min;
            var sum = Queries.OracleQueries.TestDB.ScalarQueries.Sum;
            var avg = Queries.OracleQueries.TestDB.ScalarQueries.Avg;
            var singleValueSelect = Queries.OracleQueries.TestDB.ScalarQueries.Single_Value_Select;

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);

            var count = dbContext.ExecuteScalar(countOfRecords);
            Assert.AreEqual("12", count);
            var maxValue = dbContext.ExecuteScalar(max);
            Assert.AreEqual("10000", maxValue);
            var minValue = dbContext.ExecuteScalar(min);
            Assert.AreEqual("3000", minValue);
            var sumValue = dbContext.ExecuteScalar(sum);
            Assert.AreEqual("161000", sumValue);
            var avgValue = dbContext.ExecuteScalar(avg);
            Assert.AreEqual("6520", avgValue);
            var singleValue = dbContext.ExecuteScalar(singleValueSelect);
            Assert.AreEqual("2", singleValue);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_ExecuteScalar_As_StringReturn_DefaultValue()
        {
            var noValueReturned = Queries.OracleQueries.TestDB.ScalarQueries.No_Value_Returned;
            var dBNullValue = Queries.OracleQueries.TestDB.ScalarQueries.DB_Null_Value;

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);

            var result = dbContext.ExecuteScalar(noValueReturned);
            Assert.IsInstanceOfType<string>(result);
            Assert.AreEqual("", result);

            result = dbContext.ExecuteScalar(dBNullValue);
            Assert.IsInstanceOfType<string>(result);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_ExecuteScalar_As_StringReturn_UnsupportedCommands()
        {
            var sqlStatements = new List<string>
            {
                Queries.OracleQueries.TestDB.DDL.Create_Table,
                Queries.OracleQueries.TestDB.DDL.Alter_Table,
                Queries.OracleQueries.TestDB.DDL.Comment_Table,
                Queries.OracleQueries.TestDB.DDL.Truncate_Table,
                Queries.OracleQueries.TestDB.DDL.Drop_Table,

                Queries.OracleQueries.TestDB.DML.InsertSql,
                Queries.OracleQueries.TestDB.DML.UpdateSql,
                Queries.OracleQueries.TestDB.DML.DeleteSql,

                Queries.OracleQueries.TestDB.DCL.GrantSql_Command_Table_User,
                Queries.OracleQueries.TestDB.DCL.RevokeSql_Command_Table_User
            };

            foreach (var sqlStatement in sqlStatements)
            {
                try
                {
                    var dbContext = new DBContext(DB.Oracle, OracleConnectionString);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_ExecuteScalarAsync_As_StringReturn_Scalar_Queries()
        {
            var countOfRecords = Queries.OracleQueries.TestDB.ScalarQueries.Count_Of_Records;
            var max = Queries.OracleQueries.TestDB.ScalarQueries.Max;
            var min = Queries.OracleQueries.TestDB.ScalarQueries.Min;
            var sum = Queries.OracleQueries.TestDB.ScalarQueries.Sum;
            var avg = Queries.OracleQueries.TestDB.ScalarQueries.Avg;
            var singleValueSelect = Queries.OracleQueries.TestDB.ScalarQueries.Single_Value_Select;

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);

            var count = await dbContext.ExecuteScalarAsync(countOfRecords);
            Assert.AreEqual("12", count);
            var maxValue = await dbContext.ExecuteScalarAsync(max);
            Assert.AreEqual("10000", maxValue);
            var minValue = await dbContext.ExecuteScalarAsync(min);
            Assert.AreEqual("3000", minValue);
            var sumValue = await dbContext.ExecuteScalarAsync(sum);
            Assert.AreEqual("161000", sumValue);
            var avgValue = await dbContext.ExecuteScalarAsync(avg);
            Assert.AreEqual("6520", avgValue);
            var singleValue = await dbContext.ExecuteScalarAsync(singleValueSelect);
            Assert.AreEqual("2", singleValue);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_ExecuteScalarAsync_As_StringReturn_DefaultValue()
        {
            var noValueReturned = Queries.OracleQueries.TestDB.ScalarQueries.No_Value_Returned;
            var dBNullValue = Queries.OracleQueries.TestDB.ScalarQueries.DB_Null_Value;

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);

            var result = await dbContext.ExecuteScalarAsync(noValueReturned);
            Assert.IsInstanceOfType<string>(result);
            Assert.AreEqual("", result);

            result = await dbContext.ExecuteScalarAsync(dBNullValue);
            Assert.IsInstanceOfType<string>(result);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_ExecuteScalarAsync_As_StringReturn_UnsupportedCommands()
        {
            var sqlStatements = new List<string>
            {
                Queries.OracleQueries.TestDB.DDL.Create_Table,
                Queries.OracleQueries.TestDB.DDL.Alter_Table,
                Queries.OracleQueries.TestDB.DDL.Comment_Table,
                Queries.OracleQueries.TestDB.DDL.Truncate_Table,
                Queries.OracleQueries.TestDB.DDL.Drop_Table,

                Queries.OracleQueries.TestDB.DML.InsertSql,
                Queries.OracleQueries.TestDB.DML.UpdateSql,
                Queries.OracleQueries.TestDB.DML.DeleteSql,

                Queries.OracleQueries.TestDB.DCL.GrantSql_Command_Table_User,
                Queries.OracleQueries.TestDB.DCL.RevokeSql_Command_Table_User
            };

            foreach (var sqlStatement in sqlStatements)
            {
                try
                {
                    var dbContext = new DBContext(DB.Oracle, OracleConnectionString);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_ExecuteScalar_As_TypedReturn_Scalar_Queries()
        {
            var countOfRecords = Queries.OracleQueries.TestDB.ScalarQueries.Count_Of_Records;
            var max = Queries.OracleQueries.TestDB.ScalarQueries.Max;
            var min = Queries.OracleQueries.TestDB.ScalarQueries.Min;
            var sum = Queries.OracleQueries.TestDB.ScalarQueries.Sum;
            var avg = Queries.OracleQueries.TestDB.ScalarQueries.Avg;
            var singleValueSelect = Queries.OracleQueries.TestDB.ScalarQueries.Single_Value_Select;

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);

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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_ExecuteScalar_As_TypedReturn_DefaultValue()
        {
            var dBNullValue = Queries.OracleQueries.TestDB.ScalarQueries.DB_Null_Value;

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);

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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_ExecuteScalar_As_TypedReturn_UnsupportedCommands()
        {
            var sqlStatements = new List<string>
            {
                Queries.OracleQueries.TestDB.DDL.Create_Table,
                Queries.OracleQueries.TestDB.DDL.Alter_Table,
                Queries.OracleQueries.TestDB.DDL.Comment_Table,
                Queries.OracleQueries.TestDB.DDL.Truncate_Table,
                Queries.OracleQueries.TestDB.DDL.Drop_Table,

                Queries.OracleQueries.TestDB.DML.InsertSql,
                Queries.OracleQueries.TestDB.DML.UpdateSql,
                Queries.OracleQueries.TestDB.DML.DeleteSql,

                Queries.OracleQueries.TestDB.DCL.GrantSql_Command_Table_User,
                Queries.OracleQueries.TestDB.DCL.RevokeSql_Command_Table_User
            };

            foreach (var sqlStatement in sqlStatements)
            {
                try
                {
                    var dbContext = new DBContext(DB.Oracle, OracleConnectionString);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_ExecuteScalarAsync_As_TypedReturn_Scalar_Queries()
        {
            var countOfRecords = Queries.OracleQueries.TestDB.ScalarQueries.Count_Of_Records;
            var max = Queries.OracleQueries.TestDB.ScalarQueries.Max;
            var min = Queries.OracleQueries.TestDB.ScalarQueries.Min;
            var sum = Queries.OracleQueries.TestDB.ScalarQueries.Sum;
            var avg = Queries.OracleQueries.TestDB.ScalarQueries.Avg;
            var singleValueSelect = Queries.OracleQueries.TestDB.ScalarQueries.Single_Value_Select;

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);

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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_ExecuteScalarAsync_As_TypedReturn_DefaultValue()
        {
            var dBNullValue = Queries.OracleQueries.TestDB.ScalarQueries.DB_Null_Value;

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);

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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_ExecuteScalarAsync_As_TypedReturn_UnsupportedCommands()
        {
            var sqlStatements = new List<string>
            {
                Queries.OracleQueries.TestDB.DDL.Create_Table,
                Queries.OracleQueries.TestDB.DDL.Alter_Table,
                Queries.OracleQueries.TestDB.DDL.Comment_Table,
                Queries.OracleQueries.TestDB.DDL.Truncate_Table,
                Queries.OracleQueries.TestDB.DDL.Drop_Table,

                Queries.OracleQueries.TestDB.DML.InsertSql,
                Queries.OracleQueries.TestDB.DML.UpdateSql,
                Queries.OracleQueries.TestDB.DML.DeleteSql,

                Queries.OracleQueries.TestDB.DCL.GrantSql_Command_Table_User,
                Queries.OracleQueries.TestDB.DCL.RevokeSql_Command_Table_User
            };

            foreach (var sqlStatement in sqlStatements)
            {
                try
                {
                    var dbContext = new DBContext(DB.Oracle, OracleConnectionString);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_ExecuteCommand_DDL_Queries()
        {
            var createTableSql = Queries.OracleQueries.TestDB.DDL.Create_Table;
            var alterTableSql = Queries.OracleQueries.TestDB.DDL.Alter_Table;
            var commentTableSql = Queries.OracleQueries.TestDB.DDL.Comment_Table;
            var commentTableColumnSql = Queries.OracleQueries.TestDB.DDL.Comment_Table_Column;
            var truncateTableSql = Queries.OracleQueries.TestDB.DDL.Truncate_Table;
            var renameTableSql = Queries.OracleQueries.TestDB.DDL.Rename_Table;
            var dropTableSql = Queries.OracleQueries.TestDB.DDL.Drop_Table;
            var dDLExecutionCheckSql = Queries.OracleQueries.TestDB.DDL.DDL_Execute_check;
            var dDLTableCommentCheckSql = Queries.OracleQueries.TestDB.DDL.DDL_Table_Comment_check;
            var dDLTableColumnCommentCheckSql = Queries.OracleQueries.TestDB.DDL.DDL_Table_Column_Comment_check;

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);
            dbContext.ExecuteCommand(createTableSql);
            dbContext.ExecuteCommand(alterTableSql);
            dbContext.ExecuteCommand(commentTableSql);
            dbContext.ExecuteCommand(commentTableColumnSql);
            dbContext.ExecuteCommand(truncateTableSql);

            var tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "Employee"));
            Assert.AreEqual("1", tableCount[0].ReferenceData["TABLE_COUNT"]);
            var tableComment = dbContext
                .FetchData(string.Format(dDLTableCommentCheckSql, "Employee"));
            Assert.AreEqual("This table stores employee records", tableComment[0].ReferenceData["TABLE_COMMENT"]);
            var tableColumnComment = dbContext
                .FetchData(string.Format(dDLTableColumnCommentCheckSql, "Employee"));
            Assert.AreEqual("This column stores employee middle name", tableColumnComment[3].ReferenceData["TABLE_COLUMN_COMMENT"]);

            dbContext.ExecuteCommand(renameTableSql);

            tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "Employee"));
            Assert.AreEqual("0", tableCount[0].ReferenceData["TABLE_COUNT"]);
            tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "Employees"));
            Assert.AreEqual("1", tableCount[0].ReferenceData["TABLE_COUNT"]);

            dbContext.ExecuteCommand(dropTableSql);

            tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "Employees"));
            Assert.AreEqual("0", tableCount[0].ReferenceData["TABLE_COUNT"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_ExecuteCommand_DML_Queries()
        {
            var insertSql = Queries.OracleQueries.TestDB.DML.InsertSql;
            var updateSql = Queries.OracleQueries.TestDB.DML.UpdateSql;
            var deleteSql = Queries.OracleQueries.TestDB.DML.DeleteSql;
            var verifyDMLExecution = Queries.OracleQueries.TestDB.DML.VerifyDMLExecution;

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);

            // Insert
            var rows = dbContext.ExecuteCommand(insertSql);
            Assert.AreEqual(1, rows);
            var data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(1, data.Count);
            var agent = data.FirstOrDefault();
            Assert.AreEqual("A020", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("John", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("Wick", agent.ReferenceData["WORKING_AREA"]);
            Assert.AreEqual("0.11", agent.ReferenceData["COMMISSION"]);
            Assert.AreEqual("010-44536178", agent.ReferenceData["PHONE_NO"]);
            Assert.AreEqual("", agent.ReferenceData["COUNTRY"]);

            // Update
            rows = dbContext.ExecuteCommand(updateSql);
            Assert.AreEqual(1, rows);
            data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(1, data.Count);
            agent = data.FirstOrDefault();
            Assert.AreEqual("A020", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("John", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("Wick", agent.ReferenceData["WORKING_AREA"]);
            Assert.AreEqual("0.15", agent.ReferenceData["COMMISSION"]);
            Assert.AreEqual("010-44536178", agent.ReferenceData["PHONE_NO"]);
            Assert.AreEqual("", agent.ReferenceData["COUNTRY"]);

            // Delete
            rows = dbContext.ExecuteCommand(deleteSql);
            Assert.AreEqual(1, rows);
            data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(0, data.Count);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_ExecuteCommand_DML_Unsupported_SELECT_Queries()
        {
            var selectSql = Queries.OracleQueries.TestDB.DML.SelectSql;

            // Select
            try
            {
                var dbContext = new DBContext(DB.Oracle, OracleConnectionString);
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
        [TestCategory(ORACLE_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_ExecuteCommand_DCL_Queries()
        {
            var user = "C##TEST_USER";
            var password = "Test123456";
            var table = "AGENTS";
            var commands = "SELECT, UPDATE";
            var checkCommand = "SELECT";

            var createUser = string.Format(Queries.OracleQueries.TestDB.DCL.CreateUserSql_User_Password, user, password);
            var grantConnect = string.Format(Queries.OracleQueries.TestDB.DCL.GrantConnectSql_User, user);
            var grantSql = string.Format(Queries.OracleQueries.TestDB.DCL.GrantSql_Command_Table_User, commands, table, user);
            var revokeSql = string.Format(Queries.OracleQueries.TestDB.DCL.RevokeSql_Command_Table_User, commands, table, user);
            var verifyPermissions = string.Format(Queries.OracleQueries.TestDB.DCL.VerifyPermission_User, user);
            var removeUser = string.Format(Queries.OracleQueries.TestDB.DCL.RemoveUserSql_User, user);

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);

            // Create User
            var result = dbContext.ExecuteCommand(createUser);
            Assert.AreEqual(0, result);

            // Grant CONNECT to User
            result = dbContext.ExecuteCommand("CREATE ROLE CONNECT");
            result = dbContext.ExecuteCommand($"GRANT CONNECT, RESOURCE TO {user}");
            //result = dbContext.ExecuteCommand($"GRANT CREATE SEQUENCE TO {user}");
            //result = dbContext.ExecuteCommand($"GRANT CREATE SYNONYM TO {user}");
            //result = dbContext.ExecuteCommand($"GRANT UNLIMITED TABLESPACE TO {user}");
            //Assert.AreEqual(0, result);

            // Existing Permissions
            var data = dbContext.FetchData(verifyPermissions);
            Assert.AreEqual(1, data.Count);
            Assert.IsFalse(data.Any(data => data.ReferenceData.Values.Any(value => value.Contains(checkCommand))));

            // Grant
            result = dbContext.ExecuteCommand(grantSql);
            Assert.AreEqual(0, result);
            data = dbContext.FetchData(verifyPermissions);
            Assert.AreEqual(2, data.Count);
            Assert.IsTrue(data.Any(data => data.ReferenceData.Values.Any(value => value.Contains(checkCommand))));

            // Revoke
            result = dbContext.ExecuteCommand(revokeSql);
            Assert.AreEqual(0, result);
            data = dbContext.FetchData(verifyPermissions);
            Assert.AreEqual(1, data.Count);
            Assert.IsFalse(data.Any(data => data.ReferenceData.Values.Any(value => value.Contains(checkCommand))));

            // Remove User
            result = dbContext.ExecuteCommand(removeUser);
            Assert.AreEqual(0, result);
        }

        #endregion

        #region Execute Command Async Tests - << Task<int> ExecuteCommandAsync(string sqlStatement) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_ExecuteCommandAsync_DDL_Queries()
        {
            var createTableSql = Queries.OracleQueries.TestDB.DDL.Create_Table;
            var alterTableSql = Queries.OracleQueries.TestDB.DDL.Alter_Table;
            var commentTableSql = Queries.OracleQueries.TestDB.DDL.Comment_Table;
            var commentTableColumnSql = Queries.OracleQueries.TestDB.DDL.Comment_Table_Column;
            var truncateTableSql = Queries.OracleQueries.TestDB.DDL.Truncate_Table;
            var renameTableSql = Queries.OracleQueries.TestDB.DDL.Rename_Table;
            var dropTableSql = Queries.OracleQueries.TestDB.DDL.Drop_Table;
            var dDLExecutionCheckSql = Queries.OracleQueries.TestDB.DDL.DDL_Execute_check;
            var dDLTableCommentCheckSql = Queries.OracleQueries.TestDB.DDL.DDL_Table_Comment_check;
            var dDLTableColumnCommentCheckSql = Queries.OracleQueries.TestDB.DDL.DDL_Table_Column_Comment_check;

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);
            await dbContext.ExecuteCommandAsync(createTableSql);
            await dbContext.ExecuteCommandAsync(alterTableSql);
            await dbContext.ExecuteCommandAsync(commentTableSql);
            await dbContext.ExecuteCommandAsync(commentTableColumnSql);
            await dbContext.ExecuteCommandAsync(truncateTableSql);

            var tableCount = await dbContext
                .FetchDataAsync(string.Format(dDLExecutionCheckSql, "Employee"));
            Assert.AreEqual("1", tableCount[0].ReferenceData["TABLE_COUNT"]);
            var tableComment = await dbContext
                .FetchDataAsync(string.Format(dDLTableCommentCheckSql, "Employee"));
            Assert.AreEqual("This table stores employee records", tableComment[0].ReferenceData["TABLE_COMMENT"]);
            var tableColumnComment = await dbContext
                .FetchDataAsync(string.Format(dDLTableColumnCommentCheckSql, "Employee"));
            Assert.AreEqual("This column stores employee middle name", tableColumnComment[3].ReferenceData["TABLE_COLUMN_COMMENT"]);

            await dbContext.ExecuteCommandAsync(renameTableSql);

            tableCount = await dbContext
                .FetchDataAsync(string.Format(dDLExecutionCheckSql, "Employee"));
            Assert.AreEqual("0", tableCount[0].ReferenceData["TABLE_COUNT"]);
            tableCount = await dbContext
                .FetchDataAsync(string.Format(dDLExecutionCheckSql, "Employees"));
            Assert.AreEqual("1", tableCount[0].ReferenceData["TABLE_COUNT"]);

            await dbContext.ExecuteCommandAsync(dropTableSql);

            tableCount = await dbContext
                .FetchDataAsync(string.Format(dDLExecutionCheckSql, "Employees"));
            Assert.AreEqual("0", tableCount[0].ReferenceData["TABLE_COUNT"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_ExecuteCommandAsync_DML_Queries()
        {
            var insertSql = Queries.OracleQueries.TestDB.DML.InsertSql;
            var updateSql = Queries.OracleQueries.TestDB.DML.UpdateSql;
            var deleteSql = Queries.OracleQueries.TestDB.DML.DeleteSql;
            var verifyDMLExecution = Queries.OracleQueries.TestDB.DML.VerifyDMLExecution;

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);

            // Insert
            var rows = await dbContext.ExecuteCommandAsync(insertSql);
            Assert.AreEqual(1, rows);
            var data = await dbContext.FetchDataAsync(verifyDMLExecution);
            Assert.AreEqual(1, data.Count);
            var agent = data.FirstOrDefault();
            Assert.AreEqual("A020", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("John", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("Wick", agent.ReferenceData["WORKING_AREA"]);
            Assert.AreEqual("0.11", agent.ReferenceData["COMMISSION"]);
            Assert.AreEqual("010-44536178", agent.ReferenceData["PHONE_NO"]);
            Assert.AreEqual("", agent.ReferenceData["COUNTRY"]);

            // Update
            rows = await dbContext.ExecuteCommandAsync(updateSql);
            Assert.AreEqual(1, rows);
            data = await dbContext.FetchDataAsync(verifyDMLExecution);
            Assert.AreEqual(1, data.Count);
            agent = data.FirstOrDefault();
            Assert.AreEqual("A020", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("John", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("Wick", agent.ReferenceData["WORKING_AREA"]);
            Assert.AreEqual("0.15", agent.ReferenceData["COMMISSION"]);
            Assert.AreEqual("010-44536178", agent.ReferenceData["PHONE_NO"]);
            Assert.AreEqual("", agent.ReferenceData["COUNTRY"]);

            // Delete
            rows = await dbContext.ExecuteCommandAsync(deleteSql);
            Assert.AreEqual(1, rows);
            data = await dbContext.FetchDataAsync(verifyDMLExecution);
            Assert.AreEqual(0, data.Count);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_ExecuteCommandAsync_DML_Unsupported_SELECT_Queries()
        {
            var selectSql = Queries.OracleQueries.TestDB.DML.SelectSql;

            // Select
            try
            {
                var dbContext = new DBContext(DB.Oracle, OracleConnectionString);
                var rows = await dbContext.ExecuteCommandAsync(selectSql);
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
        [TestCategory(ORACLE_TESTS), TestCategory(ORACLE_TESTS)]
        public async Task Test_Oracle_ExecuteCommandAsync_DCL_Queries()
        {
            var user = "C##TEST_USER";
            var password = "Test123456";
            var table = "AGENTS";
            var commands = "SELECT, UPDATE";
            var checkCommand = "SELECT";

            var createUser = string.Format(Queries.OracleQueries.TestDB.DCL.CreateUserSql_User_Password, user, password);
            var grantConnect = string.Format(Queries.OracleQueries.TestDB.DCL.GrantConnectSql_User, user);
            var grantSql = string.Format(Queries.OracleQueries.TestDB.DCL.GrantSql_Command_Table_User, commands, table, user);
            var revokeSql = string.Format(Queries.OracleQueries.TestDB.DCL.RevokeSql_Command_Table_User, commands, table, user);
            var verifyPermissions = string.Format(Queries.OracleQueries.TestDB.DCL.VerifyPermission_User, user);
            var removeUser = string.Format(Queries.OracleQueries.TestDB.DCL.RemoveUserSql_User, user);

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);

            // Create User
            var result = await dbContext.ExecuteCommandAsync(createUser);
            Assert.AreEqual(0, result);

            // Grant CONNECT to User
            result = await dbContext.ExecuteCommandAsync("CREATE ROLE CONNECT");
            result = await dbContext.ExecuteCommandAsync($"GRANT CONNECT, RESOURCE TO {user}");
            //result = await dbContext.ExecuteCommandAsync($"GRANT CREATE SEQUENCE TO {user}");
            //result = await dbContext.ExecuteCommandAsync($"GRANT CREATE SYNONYM TO {user}");
            //result = await dbContext.ExecuteCommandAsync($"GRANT UNLIMITED TABLESPACE TO {user}");
            //Assert.AreEqual(0, result);

            // Existing Permissions
            var data = await dbContext.FetchDataAsync(verifyPermissions);
            Assert.AreEqual(1, data.Count);
            Assert.IsFalse(data.Any(data => data.ReferenceData.Values.Any(value => value.Contains(checkCommand))));

            // Grant
            result = await dbContext.ExecuteCommandAsync(grantSql);
            Assert.AreEqual(0, result);
            data = await dbContext.FetchDataAsync(verifyPermissions);
            Assert.AreEqual(2, data.Count);
            Assert.IsTrue(data.Any(data => data.ReferenceData.Values.Any(value => value.Contains(checkCommand))));

            // Revoke
            result = await dbContext.ExecuteCommandAsync(revokeSql);
            Assert.AreEqual(0, result);
            data = await dbContext.FetchDataAsync(verifyPermissions);
            Assert.AreEqual(1, data.Count);
            Assert.IsFalse(data.Any(data => data.ReferenceData.Values.Any(value => value.Contains(checkCommand))));

            // Remove User
            result = await dbContext.ExecuteCommandAsync(removeUser);
            Assert.AreEqual(0, result);
        }

        #endregion

        #region Execute Transaction Tests - << bool ExecuteTransaction(List<string> sqlStatements) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_ExecuteTransaction_DDL_Multiple_Queries()
        {
            var createTableSql = Queries.OracleQueries.TestDB.DDL.Create_Table;
            var alterTableSql = Queries.OracleQueries.TestDB.DDL.Alter_Table;
            var truncateTableSql = Queries.OracleQueries.TestDB.DDL.Truncate_Table;
            var renameTableSql = Queries.OracleQueries.TestDB.DDL.Rename_Table;
            var dropTableSql = Queries.OracleQueries.TestDB.DDL.Drop_Table;
            var dDLExecutionCheckSql = Queries.OracleQueries.TestDB.DDL.DDL_Execute_check;

            // Create, Alter & Truncate
            var statements = new List<string>
            {
                createTableSql,
                alterTableSql,
                truncateTableSql
            };
            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);
            var result = dbContext.ExecuteTransaction(statements);
            Assert.IsTrue(result);

            var tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "Employee"));
            Assert.AreEqual("1", tableCount[0].ReferenceData["TABLE_COUNT"]);

            // Rename & Drop
            statements = new List<string>
            {
                renameTableSql,
                dropTableSql
            };
            result = dbContext.ExecuteTransaction(statements);
            Assert.IsTrue(result);

            tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "Employees"));
            Assert.AreEqual("0", tableCount[0].ReferenceData["TABLE_COUNT"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_ExecuteTransaction_DML_Multiple_Queries()
        {
            var insertSql = Queries.OracleQueries.TestDB.DML.InsertSql;
            var updateSql = Queries.OracleQueries.TestDB.DML.UpdateSql;
            var deleteSql = Queries.OracleQueries.TestDB.DML.DeleteSql;
            var verifyDMLExecution = Queries.OracleQueries.TestDB.DML.VerifyDMLExecution;

            var statements = new List<string>
            {
                insertSql,
                updateSql
            };
            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);

            // Insert & Update
            var result = dbContext.ExecuteTransaction(statements);
            Assert.IsTrue(result);
            var data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(1, data.Count);
            var agent = data.FirstOrDefault();
            Assert.AreEqual("A020", agent.ReferenceData["AGENT_CODE"]);
            Assert.AreEqual("John", agent.ReferenceData["AGENT_NAME"]);
            Assert.AreEqual("Wick", agent.ReferenceData["WORKING_AREA"]);
            Assert.AreEqual("0.15", agent.ReferenceData["COMMISSION"]);
            Assert.AreEqual("010-44536178", agent.ReferenceData["PHONE_NO"]);
            Assert.AreEqual("", agent.ReferenceData["COUNTRY"]);

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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_ExecuteTransaction_Incomplete_Transaction_Rollback_On_Error()
        {
            var insertSql = Queries.OracleQueries.TestDB.DML.InsertSql;
            var updateSql = Queries.OracleQueries.TestDB.DML.UpdateSql;
            var updateErrorSql = "UPDATE";
            var verifyDMLExecution = Queries.OracleQueries.TestDB.DML.VerifyDMLExecution;

            var statements = new List<string>
            {
                insertSql,
                updateSql,
                updateErrorSql
            };
            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);

            // Insert & Update
            var result = dbContext.ExecuteTransaction(statements);
            Assert.IsFalse(result);
            var data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(0, data.Count);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_ExecuteTransaction_DML_Unsupported_SELECT_Queries()
        {
            var selectSql = Queries.OracleQueries.TestDB.DML.SelectSql;

            // Select
            try
            {
                var statements = new List<string>
                {
                    selectSql
                };
                var dbContext = new DBContext(DB.Oracle, OracleConnectionString);
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
