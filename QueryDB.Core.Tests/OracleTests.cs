using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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
        public void Test_Oracle_FetchData_Dictionary_SelectQuery_UpperCaseKeys()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql;
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
        public void Test_Oracle_FetchData_Dictionary_SelectQuery_Joins()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 34);
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
            Assert.IsTrue(data.Count == 34);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Dictionary_SelectQuery_Aliases_UpperCaseKeys()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData(selectSql, upperCaseKeys: true);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Dictionary_DataTypes_Check()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_DataTypes;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 1);
            var dataType = data.FirstOrDefault();
            Console.WriteLine(dataType.ReferenceData["BFILE_COLUMN"]);
            //Assert.AreEqual("", dataType.ReferenceData["BFILE_COLUMN"]);
            Assert.AreEqual("3q2+7w==", dataType.ReferenceData["BLOB_COLUMN"]);
            Assert.AreEqual("A", dataType.ReferenceData["CHAR_COLUMN"]);
            Assert.AreEqual("Sample CLOB data", dataType.ReferenceData["CLOB_COLUMN"]);
            Assert.AreEqual("09/21/2024 00:00:00", ConvertToUSFormat(dataType.ReferenceData["DATE_COLUMN"]));
            Assert.AreEqual("123.45", dataType.ReferenceData["FLOAT_COLUMN"]);
            Assert.AreEqual("123", dataType.ReferenceData["INTEGER_COLUMN"]);
            Assert.AreEqual("14", dataType.ReferenceData["INTERVALYEARTOMONTH_COLUMN"]);
            Assert.AreEqual("1.02:03:04.5000000", dataType.ReferenceData["INTERNALDAYTOSECOND_COLUMN"]);
            Assert.AreEqual("", dataType.ReferenceData["LONG_COLUMN"]);
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

        #region Fetch Data Tests - << List<T> FetchData<T>(string selectSql) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Entity_SelectQuery()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData<Entities.Oracle.Agents>(selectSql);
            Assert.IsTrue(data.Count == 12);
            var agent = data.FirstOrDefault(X => X.Agent_Name == "Benjamin");
            Assert.AreEqual("A009", agent.Agent_Code);
            Assert.AreEqual("Benjamin", agent.Agent_Name);
            Assert.AreEqual("Hampshair", agent.Working_Area);
            Assert.AreEqual(0.11, agent.Commission);
            Assert.AreEqual("008-22536178", agent.Phone_No);
            Assert.AreEqual(null, agent.Country);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Entity_SelectQuery_Joins()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData<Entities.Oracle.Orders>(selectSql);
            Assert.IsTrue(data.Count == 34);
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
            Assert.AreEqual(null, agent.Agent);
            Assert.AreEqual(null, agent.Agent_Location);
            Assert.AreEqual(null, agent.Customer);
            Assert.AreEqual(null, agent.Customer_Location);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Entity_SelectQuery_Aliases()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData<Entities.Oracle.Orders>(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Entity_DataTypes_Check()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_DataTypes;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData<Entities.Oracle.DataTypes>(selectSql);
            Assert.IsTrue(data.Count == 1);
            var dataType = data.FirstOrDefault();
            Console.WriteLine(dataType.BFile_Column);
            //Assert.IsTrue(dataType.BFile_Column is byte[] && dataType.BFile_Column == null);
            Assert.IsTrue(dataType.Blob_Column is byte[] && dataType.Blob_Column != null);
            Assert.AreEqual("A", dataType.Char_Column);
            Assert.AreEqual("Sample CLOB data", dataType.Clob_Column);
            Assert.AreEqual("09/21/2024 00:00:00", ConvertToUSFormat(dataType.Date_Column.ToString()));
            Assert.AreEqual((decimal)123.45, dataType.Float_Column);
            Assert.AreEqual(123, (int)dataType.Integer_Column);
            Assert.AreEqual(14, dataType.IntervalYearToMonth_Column);
            Assert.AreEqual("1.02:03:04.5000000", dataType.InternalDayToSecond_Column.ToString());
            Assert.AreEqual("", dataType.Long_Column);
            Assert.AreEqual("A", dataType.NChar_Column);
            Assert.AreEqual("Sample NCLOB data", dataType.NClob_Column);
            Assert.AreEqual((decimal)123.45, dataType.Number_Column);
            Assert.AreEqual("Sample NVARCHAR2 data", dataType.NVarchar2_Column);
            Assert.IsTrue(dataType.Raw_Column is byte[] && dataType.Raw_Column != null);
            Assert.AreEqual("09/21/2024 12:34:56", ConvertToUSFormat(dataType.Timestamp_Column.ToString()));
            Assert.AreEqual("09/21/2024 12:34:56", ConvertToUSFormat(dataType.TimestampWithTimeZone_Column.ToString()));
            Assert.AreEqual("09/21/2024 12:34:56", ConvertToUSFormat(dataType.TimestampWithLocalTimeZone_Column.ToString()));
            Assert.AreEqual("Sample VARCHAR data", dataType.Varchar_Column);
            Assert.AreEqual("Sample VARCHAR2 data", dataType.Varchar2_Column);
        }
         [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(ORACLE_TESTS)]
        public void Test_Oracle_FetchData_Entity_DataTypes_Strict_Check()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Strict;
            var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData<Entities.Oracle.Details>(selectSql, strict: true);
            Assert.IsTrue(data.Count == 34);
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
        public void Test_Oracle_FetchData_Entity_DataTypes_Strict_Error_Check()
        {
            var selectSql = Queries.OracleQueries.TestDB.SelectSql_Strict;
            try
            {
                var data = new DBContext(DB.Oracle, OracleConnectionString).FetchData<Entities.Oracle.Orders>(selectSql, strict: true);
            }
            catch (IndexOutOfRangeException ex)
            {
                Assert.AreEqual("Unable to find specified column in result set", ex.Message);
            }
        }

        #endregion

        #endregion

    }
}
