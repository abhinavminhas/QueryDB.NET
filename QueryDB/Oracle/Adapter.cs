using Oracle.ManagedDataAccess.Client;
using QueryDB.Resources;
using System;
using System.Collections.Generic;
using System.Data;

namespace QueryDB.Oracle
{
    /// <summary>
    /// 'Oracle' adapter.
    /// </summary>
    internal class Adapter
    {
        /// <summary>
        /// Gets the 'Oracle' data reader.
        /// </summary>
        /// <param name="cmdText">The text of the query.</param>
        /// <param name="connection">'Oracle' connection.</param>
        /// <param name="commandType">Sql command type.</param>
        /// <returns>'Oracle' data reader.</returns>
        internal OracleDataReader GetOracleReader(string cmdText, OracleConnection connection, CommandType commandType)
        {
            connection.Open();
            using (var sqlCommand = new OracleCommand(cmdText, connection) { CommandType = commandType })
            {
                return sqlCommand.ExecuteReader();
            }
        }

        /// <summary>
        /// Creates and returns a new <see cref="OracleCommand"/> with the specified command text, 
        /// connection, and command type. Opens the connection before creating the command.
        /// </summary>
        /// <param name="cmdText">The SQL command text to execute.</param>
        /// <param name="connection">The <see cref="OracleConnection"/> to use.</param>
        /// <param name="commandType">The type of the command (e.g., Text, StoredProcedure).</param>
        /// <returns>A configured <see cref="OracleCommand"/> instance.</returns>
        internal OracleCommand GetOracleCommand(string cmdText, OracleConnection connection, CommandType commandType)
        {
            connection.Open();
            var sqlCommand = new OracleCommand(cmdText, connection) { CommandType = commandType };
            return sqlCommand;
        }

        /// <summary>
        /// Creates and returns a new Oracle command associated with the specified connection.
        /// </summary>
        /// <param name="cmdText">The SQL command text to execute.</param>
        /// <param name="connection">The Oracle database connection.</param>
        /// <returns>A new <see cref="OracleCommand"/> instance configured with the provided connection.</returns>
        internal static OracleCommand GetOracleCommand(string cmdText, OracleConnection connection)
        {
            var sqlCommand = new OracleCommand(cmdText, connection);
            return sqlCommand;
        }

        /// <summary>
        /// Initiates and returns a new Oracle transaction for the specified database connection.
        /// </summary>
        /// <param name="connection">The Oracle database connection.</param>
        /// <returns>A new <see cref="OracleTransaction"/> associated with the provided connection.</returns>
        internal static OracleTransaction GetOracleTransaction(OracleConnection connection)
        {
            connection.Open();
            var oracleTransaction = connection.BeginTransaction();
            return oracleTransaction;
        }

        /// <summary>
        /// Retrieves records for 'Select' queries from the database.
        /// Converts column names to keys holding values, with multiple database rows returned into a list.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="connection">'Oracle' Connection.</param>
        /// <param name="upperCaseKeys">Boolean parameter to return dictionary keys in uppercase.</param>
        /// <returns>List of data Dictionary with column names as keys holding values into a list for multiple rows of data.
        /// Note: Byte[]/BFile is returned as Base64 string.</returns>
        internal List<DataDictionary> FetchData(string selectSql, OracleConnection connection, bool upperCaseKeys)
        {
            var dataList = new List<DataDictionary>();
            using (var reader = GetOracleReader(selectSql, connection, CommandType.Text))
            {
                while (reader.Read())
                {
                    var addRow = new DataDictionary();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string key = upperCaseKeys ? reader.GetName(i).ToUpper() : reader.GetName(i);
                        if (Utils.IsBFileColumn(reader, i))
                            addRow.ReferenceData.Add(key, Utils.GetBFileBase64Content(reader, i));
                        else if (reader.GetValue(i) is byte[] value)
                            addRow.ReferenceData.Add(key, Convert.ToBase64String(value));
                        else
                            addRow.ReferenceData.Add(key, reader.GetValue(i).ToString());
                    }
                    dataList.Add(addRow);
                }
            }
            return dataList;
        }

        /// <summary>
        ///  Retrieves records for 'Select' queries from the database.
        /// </summary>
        /// <typeparam name="T">Object entity to return data mapped into.</typeparam>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="connection">'Oracle' Connection.</param>
        /// <param name="strict">Enables fetch data only for object <T> properties existing in database query result.</param>
        /// <returns>List of data rows mapped into object entity into a list for multiple rows of data.</returns>
        internal List<T> FetchData<T>(string selectSql, OracleConnection connection, bool strict) where T : new()
        {
            var dataList = new List<T>();
            using (var reader = GetOracleReader(selectSql, connection, CommandType.Text))
            {
                while (reader.Read())
                {
                    var addObjectRow = new T();
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        if ((strict || Utils.ColumnExists(reader, prop.Name)) && !reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                        {
                            if (Utils.IsBFileColumn(reader, prop.Name))
                                prop.SetValue(addObjectRow, Utils.GetBFileByteContent(reader, prop.Name));
                            else
                                prop.SetValue(addObjectRow, reader[prop.Name]);
                        }   
                    }
                    dataList.Add(addObjectRow);
                }
            }
            return dataList;
        }

        /// <summary>
        /// Executes SQL commands.
        /// </summary>
        /// <param name="sqlStatement">SQL statement as command.</param>
        /// <param name="connection">'Oracle' Connection.</param>
        /// <returns>The number of rows affected.</returns>
        internal int ExecuteCommand(string sqlStatement, OracleConnection connection)
        {
            using (var sqlCommand = GetOracleCommand(sqlStatement, connection, CommandType.Text))
            {
                return sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Executes multiple SQL statements within an Oracle transaction to ensure atomicity.
        /// </summary>
        /// <param name="sqlStatements">A list of SQL statements to execute.</param>
        /// <param name="connection">The Oracle database connection.</param>
        /// <returns>
        /// Returns <c>true</c> if the transaction is committed successfully; 
        /// otherwise, <c>false</c> if an error occurs and the transaction is rolled back.
        /// </returns>
        /// <exception cref="Exception">
        /// Logs and handles exceptions if any SQL command execution fails.
        /// </exception>
        internal bool ExecuteTransaction(List<string> sqlStatements, OracleConnection connection)
        {
            using (OracleTransaction transaction = GetOracleTransaction(connection))
            {
                try
                {
                    foreach (var sqlStatement in sqlStatements)
                    {
                        using (var sqlCommand = GetOracleCommand(sqlStatement, connection))
                        {
                            sqlCommand.Transaction = transaction;
                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Transaction rolled back due to error: {ex.Message}");
                    return false;
                }
            }
        }
    }
}