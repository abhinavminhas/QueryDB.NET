using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryDB.Exceptions;
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
            Assert.IsTrue(data.Count == 2);
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
            Assert.IsTrue(data.Count == 2);
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
        public void Test_Oracle_FetchData_Entity_Strict_Error_Check()
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
            var verifyDMLExecution = Queries.MSSQLQueries.TestDB.DML.VerifyDMLExecution;

            var dbContext = new DBContext(DB.Oracle, OracleConnectionString);

            // Insert
            var rows = dbContext.ExecuteCommand(insertSql);
            Assert.AreEqual(1, rows);
            var data = dbContext.FetchData(verifyDMLExecution);
            Assert.IsTrue(data.Count == 1);
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
            Assert.IsTrue(data.Count == 1);
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
            Assert.IsTrue(data.Count == 0);
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

        #endregion

        #endregion

    }
}
