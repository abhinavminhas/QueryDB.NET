using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryDB.Exceptions;
using System;
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
        public void Test_MSSQL_FetchData_Dictionary_SelectQuery()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql;
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
        public void Test_MSSQL_FetchData_Dictionary_SelectQuery_UpperCaseKeys()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql;
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
        public void Test_MSSQL_FetchData_Dictionary_SelectQuery_Joins()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Join;
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
        public void Test_MSSQL_FetchData_Dictionary_SelectQuery_Joins_UpperCaseKeys()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Join;
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
        public void Test_MSSQL_FetchData_Dictionary_SelectQuery_Aliases()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Alias;
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
        public void Test_MSSQL_FetchData_Dictionary_SelectQuery_Aliases_UpperCaseKeys()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Alias;
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

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_Dictionary_DataTypes_Check()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_DataTypes;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count == 1);
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

        #region Fetch Data Tests - << List<T> FetchData<T>(string selectSql) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_Entity_SelectQuery()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql;
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

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_Entity_SelectQuery_Joins()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData<Entities.MSSQL.Orders>(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_Entity_SelectQuery_Aliases()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData<Entities.MSSQL.Orders>(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(MSSQL_TESTS)]
        public void Test_MSSQL_FetchData_Entity_DataTypes_Check()
        {
            var selectSql = Queries.MSSQLQueries.TestDB.SelectSql_DataTypes;
            var data = new DBContext(DB.MSSQL, MSSQLConnectionString).FetchData<Entities.MSSQL.DataTypes>(selectSql);
            Assert.IsTrue(data.Count == 1);
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
            Assert.IsTrue(data.Count == 34);
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
            }
            catch (IndexOutOfRangeException ex)
            {
                Assert.AreEqual("Agent_Name", ex.Message);
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
            Assert.IsTrue(data.Count == 1);
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
            Assert.IsTrue(data.Count == 1);
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
            Assert.IsTrue(data.Count == 0);
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

        #endregion

    }
}
