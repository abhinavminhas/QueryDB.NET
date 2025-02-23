using Oracle.ManagedDataAccess.Client;
using QueryDB.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace QueryDB.Oracle
{
    /// <summary>
    /// 'Oracle' adapter.
    /// </summary>
    internal class Adapter
    {

        #region Synchronous

        /// <summary>
        /// Executes the specified SQL command and returns 'Oracle' data reader.
        /// </summary>
        /// <param name="cmdText">The text of the query.</param>
        /// <param name="connection">The <see cref="OracleConnection"/> object used to connect to the database.</param>
        /// <param name="commandType">Sql command type.</param>
        /// <returns>A <see cref="OracleConnection"/> object that can be used to read the query results.</returns>
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
        /// <param name="connection">The <see cref="OracleConnection"/> object used to connect to the database.</param>
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
        /// Executes and retrieves records for 'Select' queries from the database.
        /// Converts column names to keys holding values, with multiple database rows returned into a list.
        /// Note: Use aliases in query for similar column names.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="connection">The <see cref="OracleConnection"/> object used to connect to the database.</param>
        /// <param name="upperCaseKeys">Boolean parameter to return dictionary keys in uppercase.</param>
        /// <returns>List of <see cref="DataDictionary"/> with column names as keys holding values into a list for multiple rows of data.
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
        /// Executes and retrieves records for 'Select' queries from the database.
        /// </summary>
        /// <typeparam name="T">Object entity to return data mapped into.</typeparam>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="connection">The <see cref="OracleConnection"/> object used to connect to the database.</param>
        /// <param name="strict">Enables fetch data only for object type <typeparamref name="T"/> properties existing in database query result.</param>
        /// <returns>List of data rows mapped into object of type <typeparamref name="T"/>.</returns>
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
        /// Executes the provided SQL statement using the given Oracle connection and returns the first column of the first row in the result set.
        /// If the result is DBNull, an empty string is returned.
        /// </summary>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <param name="connection">The <see cref="OracleConnection"/> to use for executing the SQL statement.</param>
        /// <returns>
        /// A <see cref="string"/> representing the value of the first column of the first row in the result set,
        /// or an empty string if the result is DBNull.
        /// </returns>
        internal string ExecuteScalar(string sqlStatement, OracleConnection connection)
        {
            using (var sqlCommand = GetOracleCommand(sqlStatement, connection, CommandType.Text))
            {
                var result = sqlCommand.ExecuteScalar();
                return result == DBNull.Value ? string.Empty : result.ToString();
            }
        }

        /// <summary>
        /// Executes the provided SQL statement using the given Oracle connection and returns the first column of the first row in the result set,
        /// converted to the specified type <typeparamref name="T"/>. If the result is DBNull, the default value of <typeparamref name="T"/> is returned.
        /// </summary>
        /// <typeparam name="T">The type to which the result should be converted.</typeparam>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <param name="connection">The <see cref="OracleConnection"/> to use for executing the SQL statement.</param>
        /// <returns>
        /// The value of the first column of the first row in the result set, converted to type <typeparamref name="T"/>,
        /// or the default value of <typeparamref name="T"/> if the result is DBNull.
        /// </returns>
        internal T ExecuteScalar<T>(string sqlStatement, OracleConnection connection)
        {
            using (var sqlCommand = GetOracleCommand(sqlStatement, connection, CommandType.Text))
            {
                var result = sqlCommand.ExecuteScalar();
                return result == null || result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
            }
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
        internal static bool ExecuteTransaction(List<string> sqlStatements, OracleConnection connection)
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

        #endregion

        #region Asynchronous

        /// <summary>
        /// Asynchronously executes the specified SQL command and returns 'Oracle' data reader.
        /// </summary>
        /// <param name="cmdText">The text of the query.</param>
        /// <param name="connection">The <see cref="OracleConnection"/> object used to connect to the database.</param>
        /// <param name="commandType">Sql command type.</param>
        /// <returns>A <see cref="OracleConnection"/> object that can be used to read the query results.</returns>
        internal async Task<OracleDataReader> GetOracleReaderAsync(string cmdText, OracleConnection connection, CommandType commandType)
        {
            await connection.OpenAsync();
            using (var sqlCommand = new OracleCommand(cmdText, connection) { CommandType = commandType })
            {
                return (OracleDataReader) await sqlCommand.ExecuteReaderAsync();
            }
        }

        /// <summary>
        /// Asynchronously creates and returns a new <see cref="OracleCommand"/> with the specified command text, 
        /// connection, and command type. Opens the connection before creating the command.
        /// </summary>
        /// <param name="cmdText">The SQL command text to execute.</param>
        /// <param name="connection">The <see cref="OracleConnection"/> object used to connect to the database.</param>
        /// <param name="commandType">The type of the command (e.g., Text, StoredProcedure).</param>
        /// <returns>A configured <see cref="OracleCommand"/> instance.</returns>
        internal async Task<OracleCommand> GetOracleCommandAsync(string cmdText, OracleConnection connection, CommandType commandType)
        {
            await connection.OpenAsync();
            var sqlCommand = new OracleCommand(cmdText, connection) { CommandType = commandType };
            return sqlCommand;
        }

        /// <summary>
        /// Asynchronously executes and retrieves records for 'Select' queries from the database.
        /// Converts column names to keys holding values, with multiple database rows returned into a list.
        /// Note: Use aliases in query for similar column names.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="connection">The <see cref="OracleConnection"/> object used to connect to the database.</param>
        /// <param name="upperCaseKeys">Boolean parameter to return dictionary keys in uppercase.</param>
        /// <returns>List of <see cref="DataDictionary"/> with column names as keys holding values into a list for multiple rows of data.
        /// Note: Byte[]/BFile is returned as Base64 string.</returns>
        internal async Task<List<DataDictionary>> FetchDataAsync(string selectSql, OracleConnection connection, bool upperCaseKeys)
        {
            var dataList = new List<DataDictionary>();
            using (var reader = await GetOracleReaderAsync(selectSql, connection, CommandType.Text))
            {
                while (await reader.ReadAsync())
                {
                    var addRow = new DataDictionary();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string key = upperCaseKeys ? reader.GetName(i).ToUpper() : reader.GetName(i);
                        if (Utils.IsBFileColumn(reader, i))
                            addRow.ReferenceData.Add(key, await Utils.GetBFileBase64ContentAsync(reader, i));
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
        /// Asynchronously executes and retrieves records for 'Select' queries from the database.
        /// </summary>
        /// <typeparam name="T">Object entity to return data mapped into.</typeparam>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="connection">The <see cref="OracleConnection"/> object used to connect to the database.</param>
        /// <param name="strict">Enables fetch data only for object type <typeparamref name="T"/> properties existing in database query result.</param>
        /// <returns>List of data rows mapped into object of type <typeparamref name="T"/>.</returns>
        internal async Task<List<T>> FetchDataAsync<T>(string selectSql, OracleConnection connection, bool strict) where T : new()
        {
            var dataList = new List<T>();
            using (var reader = await GetOracleReaderAsync(selectSql, connection, CommandType.Text))
            {
                while (await reader.ReadAsync())
                {
                    var addObjectRow = new T();
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        if ((strict || Utils.ColumnExists(reader, prop.Name)) && !await reader.IsDBNullAsync(reader.GetOrdinal(prop.Name)))
                        {
                            if (Utils.IsBFileColumn(reader, prop.Name))
                                prop.SetValue(addObjectRow, await Utils.GetBFileByteContentAsync(reader, prop.Name));
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
        /// Asynchronously executes the provided SQL statement using the given Oracle connection and returns the first column of the first row in the result set.
        /// If the result is DBNull, an empty string is returned.
        /// </summary>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <param name="connection">The <see cref="OracleConnection"/> to use for executing the SQL statement.</param>
        /// <returns>
        /// A <see cref="string"/> representing the value of the first column of the first row in the result set,
        /// or an empty string if the result is DBNull.
        /// </returns>
        internal async Task<string> ExecuteScalarAsync(string sqlStatement, OracleConnection connection)
        {
            using (var sqlCommand = await GetOracleCommandAsync(sqlStatement, connection, CommandType.Text))
            {
                var result = await sqlCommand.ExecuteScalarAsync();
                return result == DBNull.Value ? string.Empty : result.ToString();
            }
        }

        /// <summary>
        /// Asynchronously executes the provided SQL statement using the given Oracle connection and returns the first column of the first row in the result set,
        /// converted to the specified type <typeparamref name="T"/>. If the result is DBNull, the default value of <typeparamref name="T"/> is returned.
        /// </summary>
        /// <typeparam name="T">The type to which the result should be converted.</typeparam>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <param name="connection">The <see cref="OracleConnection"/> to use for executing the SQL statement.</param>
        /// <returns>
        /// The value of the first column of the first row in the result set, converted to type <typeparamref name="T"/>,
        /// or the default value of <typeparamref name="T"/> if the result is DBNull.
        /// </returns>
        internal async Task<T> ExecuteScalarAsync<T>(string sqlStatement, OracleConnection connection)
        {
            using (var sqlCommand = await GetOracleCommandAsync(sqlStatement, connection, CommandType.Text))
            {
                var result = await sqlCommand.ExecuteScalarAsync();
                return result == null || result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
            }
        }

        #endregion
    }
}