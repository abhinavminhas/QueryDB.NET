using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryDB.Exceptions;
using System;
using System.Collections.Generic;
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
            Assert.AreEqual(12, data.Count);
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
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Dictionary_SelectQuery_Joins()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData(selectSql);
            Assert.AreEqual(34, data.Count);
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
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Dictionary_SelectQuery_Aliases()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData(selectSql);
            Assert.AreEqual(34, data.Count);
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
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Dictionary_DataTypes_Check()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql_DataTypes;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData(selectSql);
            Assert.AreEqual(1, data.Count);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual("9223372036854775807", dataType.ReferenceData["bigint_column"]);
            Assert.AreEqual("True", dataType.ReferenceData["boolean_column"]);
            Assert.AreEqual("3q2+7w==", dataType.ReferenceData["bytea_column"]);
            Assert.AreEqual("char10", dataType.ReferenceData["char_column"]);
            Assert.AreEqual("varchar50", dataType.ReferenceData["varchar_column"]);
            Assert.AreEqual("09/21/2024 00:00:00", ConvertToUSFormat(dataType.ReferenceData["date_column"]));
            Assert.AreEqual("123456.789", dataType.ReferenceData["double_column"]);
            Assert.AreEqual("2147483647", dataType.ReferenceData["int_column"]);
            Assert.AreEqual("12345.67", dataType.ReferenceData["money_column"]);
            Assert.AreEqual("123456.789", dataType.ReferenceData["numeric_column"]);
            Assert.AreEqual("12345.67", dataType.ReferenceData["real_column"]);
            Assert.AreEqual("32767", dataType.ReferenceData["smallint_column"]);
            Assert.AreEqual("some text", dataType.ReferenceData["text_column"]);
            Assert.AreEqual("12:34:56", dataType.ReferenceData["time_column"]);
            Assert.AreEqual("09/21/2024 14:34:56", ConvertToUSFormat(dataType.ReferenceData["timestamp_column"]));
            Assert.AreEqual("123e4567-e89b-12d3-a456-426614174000", dataType.ReferenceData["uuid_column"]);
        }

        #endregion

        #region Fetch Data Tests - << List<T> FetchData<T>(string selectSql) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Entity_SelectQuery()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData<Entities.PostgreSQL.Agents>(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Entity_SelectQuery_Joins()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql_Join;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData<Entities.PostgreSQL.Orders>(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Entity_SelectQuery_Aliases()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql_Alias;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData<Entities.PostgreSQL.Orders>(selectSql);
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
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Entity_DataTypes_Check()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql_DataTypes;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData<Entities.PostgreSQL.DataTypes>(selectSql);
            Assert.AreEqual(1, data.Count);
            var dataType = data.FirstOrDefault();
            Assert.AreEqual(9223372036854775807, dataType.BigInt_Column);
            Assert.IsTrue(dataType.Boolean_Column);
            Assert.AreEqual("3q2+7w==", ConvertByteArrayToBase64(dataType.Bytea_Column));
            Assert.AreEqual("char10", dataType.Char_Column);
            Assert.AreEqual("varchar50", dataType.Varchar_Column);
            Assert.AreEqual("09/21/2024 00:00:00", ConvertToUSFormat(dataType.Date_Column.ToString()));
            Assert.AreEqual(123456.789, dataType.Double_Column);
            Assert.AreEqual(2147483647, dataType.Int_Column);
            Assert.AreEqual((decimal)12345.67, dataType.Money_Column);
            Assert.AreEqual((decimal)123456.789, dataType.Numeric_Column);
            Assert.AreEqual((float)12345.67, dataType.Real_Column);
            Assert.AreEqual(32767, dataType.SmallInt_Column);
            Assert.AreEqual("some text", dataType.Text_Column);
            Assert.AreEqual("12:34:56", dataType.Time_Column.ToString());
            Assert.AreEqual("09/21/2024 14:34:56", ConvertToUSFormat(dataType.Timestamp_Column.ToString()));
            Assert.AreEqual("123e4567-e89b-12d3-a456-426614174000", dataType.Uuid_Column.ToString());
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Entity_Strict_Check()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql_Strict;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData<Entities.PostgreSQL.Details>(selectSql, strict: true);
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
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_FetchData_Entity_Strict_Error_Check()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.SelectSql_Strict;
            try
            {
                var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData<Entities.PostgreSQL.Orders>(selectSql, strict: true);
            }
            catch (IndexOutOfRangeException ex)
            {
                Assert.AreEqual("Field not found in row: Agent_Name", ex.Message);
            }
        }

        #endregion

        #region Execute Scalar Tests - << string ExecuteScalar(string sqlStatement); >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_ExecuteScalar_As_StringReturn_Scalar_Queries()
        {
            var countOfRecords = Queries.PostgreSQLQueries.TestDB.ScalarQueries.Count_Of_Records;
            var max = Queries.PostgreSQLQueries.TestDB.ScalarQueries.Max;
            var min = Queries.PostgreSQLQueries.TestDB.ScalarQueries.Min;
            var sum = Queries.PostgreSQLQueries.TestDB.ScalarQueries.Sum;
            var avg = Queries.PostgreSQLQueries.TestDB.ScalarQueries.Avg;
            var singleValueSelect = Queries.PostgreSQLQueries.TestDB.ScalarQueries.Single_Value_Select;

            var dbContext = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString);

            var count = dbContext.ExecuteScalar(countOfRecords);
            Assert.AreEqual("12", count);
            var maxValue = dbContext.ExecuteScalar(max);
            Assert.AreEqual("10000.00", maxValue);
            var minValue = dbContext.ExecuteScalar(min);
            Assert.AreEqual("3000.00", minValue);
            var sumValue = dbContext.ExecuteScalar(sum);
            Assert.AreEqual("161000.00", sumValue);
            var avgValue = dbContext.ExecuteScalar(avg);
            Assert.AreEqual("6520.0000000000000000", avgValue);
            var singleValue = dbContext.ExecuteScalar(singleValueSelect);
            Assert.AreEqual("2", singleValue);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_ExecuteScalar_As_StringReturn_DefaultValue()
        {
            var noValueReturned = Queries.PostgreSQLQueries.TestDB.ScalarQueries.No_Value_Returned;
            var dBNullValue = Queries.PostgreSQLQueries.TestDB.ScalarQueries.DB_Null_Value;

            var dbContext = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString);

            var result = dbContext.ExecuteScalar(noValueReturned);
            Assert.IsInstanceOfType<string>(result);
            Assert.AreEqual("", result);

            result = dbContext.ExecuteScalar(dBNullValue);
            Assert.IsInstanceOfType<string>(result);
            Assert.AreEqual(string.Empty, result);
        }

        #endregion

        #region Execute Scalar Tests - << T ExecuteScalar<T>(string sqlStatement); >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_ExecuteScalar_As_TypedReturn_Scalar_Queries()
        {
            var countOfRecords = Queries.PostgreSQLQueries.TestDB.ScalarQueries.Count_Of_Records;
            var max = Queries.PostgreSQLQueries.TestDB.ScalarQueries.Max;
            var min = Queries.PostgreSQLQueries.TestDB.ScalarQueries.Min;
            var sum = Queries.PostgreSQLQueries.TestDB.ScalarQueries.Sum;
            var avg = Queries.PostgreSQLQueries.TestDB.ScalarQueries.Avg;
            var singleValueSelect = Queries.PostgreSQLQueries.TestDB.ScalarQueries.Single_Value_Select;

            var dbContext = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString);

            var count = dbContext.ExecuteScalar<int>(countOfRecords);
            Assert.AreEqual(12, count);
            var maxValue = dbContext.ExecuteScalar<float>(max);
            Assert.AreEqual(10000.00, maxValue);
            var minValue = dbContext.ExecuteScalar<float>(min);
            Assert.AreEqual(3000.00, minValue);
            var sumValue = dbContext.ExecuteScalar<float>(sum);
            Assert.AreEqual(161000.00, sumValue);
            var avgValue = dbContext.ExecuteScalar<decimal>(avg);
            Assert.AreEqual((decimal)6520.0000000000000000, avgValue);
            var singleValue = dbContext.ExecuteScalar<string>(singleValueSelect);
            Assert.AreEqual("2", singleValue);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_ExecuteScalar_As_TypedReturn_DefaultValue()
        {
            var dBNullValue = Queries.PostgreSQLQueries.TestDB.ScalarQueries.DB_Null_Value;

            var dbContext = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString);

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

        #endregion

        #region Execute Command Tests - << int ExecuteCommand(string sqlStatement) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_ExecuteCommand_DDL_Queries()
        {
            var createTableSql = Queries.PostgreSQLQueries.TestDB.DDL.Create_Table;
            var alterTableSql = Queries.PostgreSQLQueries.TestDB.DDL.Alter_Table;
            var commentTableSql = Queries.PostgreSQLQueries.TestDB.DDL.Comment_Table;
            var commentTableColumnSql = Queries.PostgreSQLQueries.TestDB.DDL.Comment_Table_Column;
            var truncateTableSql = Queries.PostgreSQLQueries.TestDB.DDL.Truncate_Table;
            var renameTableSql = Queries.PostgreSQLQueries.TestDB.DDL.Rename_Table;
            var dropTableSql = Queries.PostgreSQLQueries.TestDB.DDL.Drop_Table;
            var dDLExecutionCheckSql = Queries.PostgreSQLQueries.TestDB.DDL.DDL_Execute_check;
            var dDLTableCommentCheckSql = Queries.PostgreSQLQueries.TestDB.DDL.DDL_Table_Comment_check;
            var dDLTableColumnCommentCheckSql = Queries.PostgreSQLQueries.TestDB.DDL.DDL_Table_Column_Comment_check;

            var dbContext = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString);
            dbContext.ExecuteCommand(createTableSql);
            dbContext.ExecuteCommand(alterTableSql);
            dbContext.ExecuteCommand(commentTableSql);
            dbContext.ExecuteCommand(commentTableColumnSql);
            dbContext.ExecuteCommand(truncateTableSql);

            var tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "public", "Employee"));
            Assert.AreEqual("1", tableCount[0].ReferenceData["table_count"]);
            var tableComment = dbContext
                .FetchData(string.Format(dDLTableCommentCheckSql, "public", "Employee"));
            Assert.AreEqual("This table stores employee records", tableComment[0].ReferenceData["table_comment"]);
            var tableColumnComment = dbContext
                .FetchData(string.Format(dDLTableColumnCommentCheckSql, "public", "Employee"));
            Assert.AreEqual("This column stores employee middle name", tableColumnComment[3].ReferenceData["table_column_comment"]);

            dbContext.ExecuteCommand(renameTableSql);

            tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "public", "Employee"));
            Assert.AreEqual("0", tableCount[0].ReferenceData["table_count"]);
            tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "public", "Employees"));
            Assert.AreEqual("1", tableCount[0].ReferenceData["table_count"]);

            dbContext.ExecuteCommand(dropTableSql);

            tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "public", "Employees"));
            Assert.AreEqual("0", tableCount[0].ReferenceData["table_count"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_ExecuteCommand_DML_Queries()
        {
            var insertSql = Queries.PostgreSQLQueries.TestDB.DML.InsertSql;
            var updateSql = Queries.PostgreSQLQueries.TestDB.DML.UpdateSql;
            var deleteSql = Queries.PostgreSQLQueries.TestDB.DML.DeleteSql;
            var verifyDMLExecution = Queries.PostgreSQLQueries.TestDB.DML.VerifyDMLExecution;

            var dbContext = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString);

            // Insert
            var rows = dbContext.ExecuteCommand(insertSql);
            Assert.AreEqual(1, rows);
            var data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(1, data.Count);
            var agent = data.FirstOrDefault();
            Assert.AreEqual("A020", agent.ReferenceData["agent_code"]);
            Assert.AreEqual("John", agent.ReferenceData["agent_name"]);
            Assert.AreEqual("Wick", agent.ReferenceData["working_area"]);
            Assert.AreEqual("0.11", agent.ReferenceData["commission"]);
            Assert.AreEqual("010-44536178", agent.ReferenceData["phone_no"]);
            Assert.AreEqual("", agent.ReferenceData["country"]);

            // Update
            rows = dbContext.ExecuteCommand(updateSql);
            Assert.AreEqual(1, rows);
            data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(1, data.Count);
            agent = data.FirstOrDefault();
            Assert.AreEqual("A020", agent.ReferenceData["agent_code"]);
            Assert.AreEqual("John", agent.ReferenceData["agent_name"]);
            Assert.AreEqual("Wick", agent.ReferenceData["working_area"]);
            Assert.AreEqual("0.15", agent.ReferenceData["commission"]);
            Assert.AreEqual("010-44536178", agent.ReferenceData["phone_no"]);
            Assert.AreEqual("", agent.ReferenceData["country"]);

            // Delete
            rows = dbContext.ExecuteCommand(deleteSql);
            Assert.AreEqual(1, rows);
            data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(0, data.Count);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_ExecuteCommand_DML_Unsupported_SELECT_Queries()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.DML.SelectSql;

            // Select
            try
            {
                var dbContext = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString);
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
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_ExecuteCommand_DCL_Queries()
        {
            var user = "test_user";
            var password = "Test@1234";
            var table = "Agents";
            var commands = "SELECT, UPDATE";
            var checkCommand = "SELECT";

            var createUser = string.Format(Queries.PostgreSQLQueries.TestDB.DCL.CreateUserSql_User_Password, user, password);
            var grantSql = string.Format(Queries.PostgreSQLQueries.TestDB.DCL.GrantSql_Command_Table_User, commands, table, user);
            var revokeSql = string.Format(Queries.PostgreSQLQueries.TestDB.DCL.RevokeSql_Command_Table_User, commands, table, user);
            var verifyPermissions = string.Format(Queries.PostgreSQLQueries.TestDB.DCL.VerifyPermission_User, user);
            var removeUser = string.Format(Queries.PostgreSQLQueries.TestDB.DCL.RemoveUserSql_User, user);

            var dbContext = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString);

            // Create User
            var result = dbContext.ExecuteCommand(createUser);
            Assert.AreEqual(-1, result);

            // Existing Permissions
            var data = dbContext.FetchData(verifyPermissions);
            Assert.AreEqual(0, data.Count);
            Assert.IsFalse(data.Any(data => data.ReferenceData.Values.Any(value => value.Contains(checkCommand))));

            // Grant
            result = dbContext.ExecuteCommand(grantSql);
            Assert.AreEqual(-1, result);
            data = dbContext.FetchData(verifyPermissions);
            Assert.AreEqual(2, data.Count);
            Assert.IsTrue(data.Any(data => data.ReferenceData.Values.Any(value => value.Contains(checkCommand))));

            // Revoke
            result = dbContext.ExecuteCommand(revokeSql);
            Assert.AreEqual(-1, result);
            data = dbContext.FetchData(verifyPermissions);
            Assert.AreEqual(0, data.Count);
            Assert.IsFalse(data.Any(data => data.ReferenceData.Values.Any(value => value.Contains(checkCommand))));

            // Remove User
            result = dbContext.ExecuteCommand(removeUser);
            Assert.AreEqual(-1, result);
        }

        #endregion

        #region Execute Transaction Tests - << bool ExecuteTransaction(List<string> sqlStatements) >>

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_ExecuteTransaction_DDL_Multiple_Queries()
        {
            var createTableSql = Queries.PostgreSQLQueries.TestDB.DDL.Create_Table;
            var alterTableSql = Queries.PostgreSQLQueries.TestDB.DDL.Alter_Table;
            var truncateTableSql = Queries.PostgreSQLQueries.TestDB.DDL.Truncate_Table;
            var renameTableSql = Queries.PostgreSQLQueries.TestDB.DDL.Rename_Table;
            var dropTableSql = Queries.PostgreSQLQueries.TestDB.DDL.Drop_Table;
            var dDLExecutionCheckSql = Queries.PostgreSQLQueries.TestDB.DDL.DDL_Execute_check;

            // Create, Alter & Truncate
            var statements = new List<string>
            {
                createTableSql,
                alterTableSql,
                truncateTableSql
            };
            var dbContext = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString);
            var result = dbContext.ExecuteTransaction(statements);
            Assert.IsTrue(result);

            var tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "public", "Employee"));
            Assert.AreEqual("1", tableCount[0].ReferenceData["table_count"]);

            // Rename & Drop
            statements = new List<string>
            {
                renameTableSql,
                dropTableSql
            };
            result = dbContext.ExecuteTransaction(statements);
            Assert.IsTrue(result);

            tableCount = dbContext
                .FetchData(string.Format(dDLExecutionCheckSql, "public", "Employees"));
            Assert.AreEqual("0", tableCount[0].ReferenceData["table_count"]);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_ExecuteTransaction_DML_Multiple_Queries()
        {
            var insertSql = Queries.PostgreSQLQueries.TestDB.DML.InsertSql;
            var updateSql = Queries.PostgreSQLQueries.TestDB.DML.UpdateSql;
            var deleteSql = Queries.PostgreSQLQueries.TestDB.DML.DeleteSql;
            var verifyDMLExecution = Queries.PostgreSQLQueries.TestDB.DML.VerifyDMLExecution;

            var statements = new List<string>
            {
                insertSql,
                updateSql
            };
            var dbContext = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString);

            // Insert & Update
            var result = dbContext.ExecuteTransaction(statements);
            Assert.IsTrue(result);
            var data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(1, data.Count);
            var agent = data.FirstOrDefault();
            Assert.AreEqual("A020", agent.ReferenceData["agent_code"]);
            Assert.AreEqual("John", agent.ReferenceData["agent_name"]);
            Assert.AreEqual("Wick", agent.ReferenceData["working_area"]);
            Assert.AreEqual("0.15", agent.ReferenceData["commission"]);
            Assert.AreEqual("010-44536178", agent.ReferenceData["phone_no"]);
            Assert.AreEqual("", agent.ReferenceData["country"]);

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
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_ExecuteTransaction_Incomplete_Transaction_Rollback_On_Error()
        {
            var insertSql = Queries.PostgreSQLQueries.TestDB.DML.InsertSql;
            var updateSql = Queries.PostgreSQLQueries.TestDB.DML.UpdateSql;
            var updateErrorSql = "UPDATE";
            var verifyDMLExecution = Queries.PostgreSQLQueries.TestDB.DML.VerifyDMLExecution;

            var statements = new List<string>
            {
                insertSql,
                updateSql,
                updateErrorSql
            };
            var dbContext = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString);

            // Insert & Update
            var result = dbContext.ExecuteTransaction(statements);
            Assert.IsFalse(result);
            var data = dbContext.FetchData(verifyDMLExecution);
            Assert.AreEqual(0, data.Count);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS)]
        public void Test_PostgreSQL_ExecuteTransaction_DML_Unsupported_SELECT_Queries()
        {
            var selectSql = Queries.PostgreSQLQueries.TestDB.DML.SelectSql;

            // Select
            try
            {
                var statements = new List<string>
                {
                    selectSql
                };
                var dbContext = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString);
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
