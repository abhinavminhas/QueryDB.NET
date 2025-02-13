﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryDB.Exceptions;
using System;
using System.Collections.Generic;
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
            Assert.AreEqual("3q2+7w==", ConvertByteArrayToBase64(dataType.VarBinary_Column));
            Assert.AreEqual("This is a varchar", dataType.VarChar_Column);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_Entity_Strict_Check()
        {
            var selectSql = Queries.MySQLQueries.TestDB.SelectSql_Strict;
            var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData<Entities.MySQL.Details>(selectSql, strict: true);
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
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_FetchData_Entity_Strict_Error_Check()
        {
            var selectSql = Queries.MySQLQueries.TestDB.SelectSql_Strict;
            try
            {
                var data = new DBContext(DB.MySQL, MySQLConnectionString).FetchData<Entities.MySQL.Orders>(selectSql, strict: true);
            }
            catch (IndexOutOfRangeException ex)
            {
                Assert.AreEqual("Could not find specified column in results: Agent_Name", ex.Message);
            }
        }

        #endregion

        #region Execute Command Tests - << int ExecuteCommand(string sqlStatement) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_ExecuteCommand_DDL_Queries()
        {
            var createTableSql = Queries.MySQLQueries.TestDB.DDL.Create_Table;
            var alterTableSql = Queries.MySQLQueries.TestDB.DDL.Alter_Table;
            var commentTableSql = Queries.MySQLQueries.TestDB.DDL.Comment_Table;
            var commentTableColumnSql = Queries.MySQLQueries.TestDB.DDL.Comment_Table_Column;
            var truncateTableSql = Queries.MySQLQueries.TestDB.DDL.Truncate_Table;
            var renameTableSql = Queries.MySQLQueries.TestDB.DDL.Rename_Table;
            var dropTableSql = Queries.MySQLQueries.TestDB.DDL.Drop_Table;
            var dDLExecutionCheckSql = Queries.MySQLQueries.TestDB.DDL.DDL_Execute_check;
            var dDLTableCommentCheckSql = Queries.MySQLQueries.TestDB.DDL.DDL_Table_Comment_check;
            var dDLTableColumnCommentCheckSql = Queries.MySQLQueries.TestDB.DDL.DDL_Table_Column_Comment_check;

            var dbContext = new DBContext(DB.MySQL, MySQLConnectionString);
            dbContext.ExecuteCommand(createTableSql);
            dbContext.ExecuteCommand(alterTableSql);
            dbContext.ExecuteCommand(commentTableSql);
            dbContext.ExecuteCommand(commentTableColumnSql);
            dbContext.ExecuteCommand(truncateTableSql);
            
            var tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "mysql", "Employee"));
            Assert.AreEqual("1", tableCount[0].ReferenceData["Table_Count"]);
            var tableComment = dbContext
                .FetchData(string.Format(dDLTableCommentCheckSql, "mysql", "Employee"));
            Assert.AreEqual("This table stores employee records", tableComment[0].ReferenceData["Table_Comment"]);
            var tableColumnComment = dbContext
                .FetchData(string.Format(dDLTableColumnCommentCheckSql, "mysql", "Employee"));
            Assert.AreEqual("This column stores employee middle name", tableColumnComment[3].ReferenceData["Table_Column_Comment"]);

            dbContext.ExecuteCommand(renameTableSql);

            tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "mysql", "Employee"));
            Assert.AreEqual("0", tableCount[0].ReferenceData["Table_Count"]);
            tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "mysql", "Employees"));
            Assert.AreEqual("1", tableCount[0].ReferenceData["Table_Count"]);

            dbContext.ExecuteCommand(dropTableSql);

            tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "mysql", "Employees"));
            Assert.AreEqual("0", tableCount[0].ReferenceData["Table_Count"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_ExecuteCommand_DML_Queries()
        {
            var insertSql = Queries.MySQLQueries.TestDB.DML.InsertSql;
            var updateSql = Queries.MySQLQueries.TestDB.DML.UpdateSql;
            var deleteSql = Queries.MySQLQueries.TestDB.DML.DeleteSql;
            var verifyDMLExecution = Queries.MSSQLQueries.TestDB.DML.VerifyDMLExecution;

            var dbContext = new DBContext(DB.MySQL, MySQLConnectionString);

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
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_ExecuteCommand_DML_Unsupported_SELECT_Queries()
        {
            var selectSql = Queries.MySQLQueries.TestDB.DML.SelectSql;

            // Select
            try
            {
                var dbContext = new DBContext(DB.MySQL, MySQLConnectionString);
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
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_ExecuteCommand_DCL_Queries()
        {
            var user = "test_user";
            var password = "Test@1234";
            var table = "Agents";
            var commands = "SELECT, UPDATE";
            var checkCommand = "SELECT";

            var createUser = string.Format(Queries.MySQLQueries.TestDB.DCL.CreateUserSql_User_Password, user, password);
            var grantSql = string.Format(Queries.MySQLQueries.TestDB.DCL.GrantSql_Command_Table_User, commands, table, user);
            var revokeSql = string.Format(Queries.MySQLQueries.TestDB.DCL.RevokeSql_Command_Table_User, commands, table, user);
            var verifyPermissions = string.Format(Queries.MySQLQueries.TestDB.DCL.VerifyPermission_User, user);
            var removeUser = string.Format(Queries.MySQLQueries.TestDB.DCL.RemoveUserSql_User, user);

            var dbContext = new DBContext(DB.MySQL, MySQLConnectionString);

            // Create User
            var result = dbContext.ExecuteCommand(createUser);
            Assert.AreEqual(0, result);

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

            //Remove User
            result = dbContext.ExecuteCommand(removeUser);
            Assert.AreEqual(0, result);
        }

        #endregion

        #region Execute Transaction Tests - << bool ExecuteTransaction(List<string> sqlStatements) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_ExecuteTransaction_DDL_Multiple_Queries()
        {
            var createTableSql = Queries.MySQLQueries.TestDB.DDL.Create_Table;
            var alterTableSql = Queries.MySQLQueries.TestDB.DDL.Alter_Table;
            var truncateTableSql = Queries.MySQLQueries.TestDB.DDL.Truncate_Table;
            var renameTableSql = Queries.MySQLQueries.TestDB.DDL.Rename_Table;
            var dropTableSql = Queries.MySQLQueries.TestDB.DDL.Drop_Table;
            var dDLExecutionCheckSql = Queries.MySQLQueries.TestDB.DDL.DDL_Execute_check;

            // Create, Alter & Truncate
            var statements = new List<string>
            {
                createTableSql,
                alterTableSql,
                truncateTableSql
            };
            var dbContext = new DBContext(DB.MySQL, MySQLConnectionString);
            var result = dbContext.ExecuteTransaction(statements);
            Assert.IsTrue(result);

            var tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "mysql", "Employee"));
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
                .FetchData(string.Format(dDLExecutionCheckSql, "mysql", "Employees"));
            Assert.AreEqual("0", tableCount[0].ReferenceData["Table_Count"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_ExecuteTransaction_DML_Multiple_Queries()
        {
            var insertSql = Queries.MySQLQueries.TestDB.DML.InsertSql;
            var updateSql = Queries.MySQLQueries.TestDB.DML.UpdateSql;
            var deleteSql = Queries.MySQLQueries.TestDB.DML.DeleteSql;
            var verifyDMLExecution = Queries.MySQLQueries.TestDB.DML.VerifyDMLExecution;

            var statements = new List<string>
            {
                insertSql,
                updateSql
            };
            var dbContext = new DBContext(DB.MySQL, MySQLConnectionString);

            // Insert & Update
            var result = dbContext.ExecuteTransaction(statements);
            Assert.IsTrue(result);
            var data = dbContext.FetchData(verifyDMLExecution);
            Assert.IsTrue(data.Count == 1);
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
            Assert.IsTrue(data.Count == 0);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_ExecuteTransaction_Incomplete_Transaction_Rollback_On_Error()
        {
            var insertSql = Queries.MySQLQueries.TestDB.DML.InsertSql;
            var updateSql = Queries.MySQLQueries.TestDB.DML.UpdateSql;
            var updateErrorSql = "UPDATE";
            var verifyDMLExecution = Queries.MySQLQueries.TestDB.DML.VerifyDMLExecution;

            var statements = new List<string>
            {
                insertSql,
                updateSql,
                updateErrorSql
            };
            var dbContext = new DBContext(DB.MySQL, MySQLConnectionString);

            // Insert & Update
            var result = dbContext.ExecuteTransaction(statements);
            Assert.IsFalse(result);
            var data = dbContext.FetchData(verifyDMLExecution);
            Assert.IsTrue(data.Count == 0);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(MYSQL_TESTS)]
        public void Test_MySQL_ExecuteTransaction_DML_Unsupported_SELECT_Queries()
        {
            var selectSql = Queries.MySQLQueries.TestDB.DML.SelectSql;

            // Select
            try
            {
                var statements = new List<string>
                {
                    selectSql
                };
                var dbContext = new DBContext(DB.MySQL, MySQLConnectionString);
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
