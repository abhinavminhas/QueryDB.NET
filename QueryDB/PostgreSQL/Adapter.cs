using Npgsql;
using QueryDB.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace QueryDB.PostgreSQL
{
    /// <summary>
    /// 'PostgreSQL' adapter.
    /// </summary>
    internal class Adapter
    {

        #region Synchronous

        /// <summary>
        /// Executes the specified SQL command and returns 'PostgreSQL' data reader.
        /// </summary>
        /// <param name="cmdText">The text of the query.</param>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> object used to connect to the database.</param>
        /// <param name="commandType">Sql command type.</param>
        /// <returns>A <see cref="NpgsqlDataReader"/> object that can be used to read the query results.</returns>
        internal NpgsqlDataReader GetPostgreSqlReader(string cmdText, NpgsqlConnection connection, CommandType commandType)
        {
            connection.Open();
            using (var sqlCommand = new NpgsqlCommand(cmdText, connection) { CommandType = commandType })
            {
                return sqlCommand.ExecuteReader();
            }
        }

        /// <summary>
        /// Creates and returns a new <see cref="NpgsqlCommand"/> with the specified command text, 
        /// connection, and command type. Opens the connection before creating the command.
        /// </summary>
        /// <param name="cmdText">The SQL command text to execute.</param>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> object used to connect to the database.</param>
        /// <param name="commandType">The type of the command (e.g., Text, StoredProcedure).</param>
        /// <returns>A configured <see cref="NpgsqlCommand"/> instance.</returns>
        internal NpgsqlCommand GetPostgreSqlCommand(string cmdText, NpgsqlConnection connection, CommandType commandType)
        {
            connection.Open();
            var sqlCommand = new NpgsqlCommand(cmdText, connection) { CommandType = commandType };
            return sqlCommand;
        }

        /// <summary>
        /// Creates and returns a new PostgreSQL command associated with the specified connection and transaction.
        /// </summary>
        /// <param name="cmdText">The SQL command text to execute.</param>
        /// <param name="connection">The PostgreSQL database connection.</param>
        /// <param name="transaction">The PostgreSQL transaction within which the command should be executed.</param>
        /// <returns>A new <see cref="NpgsqlCommand"/> instance configured with the provided connection and transaction.</returns>
        internal static NpgsqlCommand GetPostgreSqlCommand(string cmdText, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            var sqlCommand = new NpgsqlCommand(cmdText, connection, transaction);
            return sqlCommand;
        }

        /// <summary>
        /// Initiates and returns a new PostgreSQL transaction for the specified database connection.
        /// </summary>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> object used to connect to the database.</param>
        /// <returns>A new <see cref="NpgsqlTransaction"/> associated with the provided connection.</returns>
        internal static NpgsqlTransaction GetPostgreSqlTransaction(NpgsqlConnection connection)
        {
            connection.Open();
            var npgsqlTransaction = connection.BeginTransaction();
            return npgsqlTransaction;
        }

        /// <summary>
        /// Executes and retrieves records for 'Select' queries from the database.
        /// Converts column names to keys holding values, with multiple database rows returned into a list.
        /// Note: Use aliases in query for similar column names.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> object used to connect to the database.</param>
        /// <param name="upperCaseKeys">Boolean parameter to return dictionary keys in uppercase.</param>
        /// <returns>List of <see cref="DataDictionary"/> with column names as keys holding values into a list for multiple rows of data.
        /// Note: Byte[] is returned as Base64 string.</returns>
        internal List<DataDictionary> FetchData(string selectSql, NpgsqlConnection connection, bool upperCaseKeys)
        {
            var dataList = new List<DataDictionary>();
            using (var reader = GetPostgreSqlReader(selectSql, connection, CommandType.Text))
            {
                while (reader.Read())
                {
                    var addRow = new DataDictionary();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string key = upperCaseKeys ? reader.GetName(i).ToUpper() : reader.GetName(i);
                        if (reader.GetValue(i) is byte[] value)
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
        /// <param name="connection">The <see cref="NpgsqlConnection"/> object used to connect to the database.</param>
        /// <param name="strict">Enables fetch data only for object type <typeparamref name="T"/> properties existing in database query result.</param>
        /// <returns>List of data rows mapped into object of type <typeparamref name="T"/>.</returns>
        internal List<T> FetchData<T>(string selectSql, NpgsqlConnection connection, bool strict) where T : new()
        {
            var dataList = new List<T>();
            using (var reader = GetPostgreSqlReader(selectSql, connection, CommandType.Text))
            {
                while (reader.Read())
                {
                    var addObjectRow = new T();
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        if ((strict || Utils.ColumnExists(reader, prop.Name)) && !reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                            prop.SetValue(addObjectRow, reader[prop.Name]);
                    }
                    dataList.Add(addObjectRow);
                }
            }
            return dataList;
        }

        /// <summary>
        /// Executes the provided SQL statement using the given PostgreSQL connection and returns the first column of the first row in the result set.
        /// If the result is DBNull, an empty string is returned.
        /// </summary>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> to use for executing the SQL statement.</param>
        /// <returns>
        /// A <see cref="string"/> representing the value of the first column of the first row in the result set,
        /// or an empty string if the result is DBNull.
        /// </returns>
        internal string ExecuteScalar(string sqlStatement, NpgsqlConnection connection)
        {
            using (var sqlCommand = GetPostgreSqlCommand(sqlStatement, connection, CommandType.Text))
            {
                var result = sqlCommand.ExecuteScalar();
                return result == null || result == DBNull.Value ? string.Empty : result.ToString();
            }
        }

        /// <summary>
        /// Executes the provided SQL statement using the given PostgreSQL connection and returns the first column of the first row in the result set,
        /// converted to the specified type <typeparamref name="T"/>. If the result is DBNull, the default value of <typeparamref name="T"/> is returned.
        /// </summary>
        /// <typeparam name="T">The type to which the result should be converted.</typeparam>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> to use for executing the SQL statement.</param>
        /// <returns>
        /// The value of the first column of the first row in the result set, converted to type <typeparamref name="T"/>,
        /// or the default value of <typeparamref name="T"/> if the result is DBNull.
        /// </returns>
        internal T ExecuteScalar<T>(string sqlStatement, NpgsqlConnection connection)
        {
            using (var sqlCommand = GetPostgreSqlCommand(sqlStatement, connection, CommandType.Text))
            {
                var result = sqlCommand.ExecuteScalar();
                return result == null || result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
            }
        }

        /// <summary>
        /// Executes a SQL statement that does not return a result set.
        /// </summary>
        /// <param name="sqlStatement">SQL statement to execute.</param>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> object used to connect to the database.</param>
        /// <returns>The number of rows affected by the execution of the SQL statement.</returns>
        internal int ExecuteCommand(string sqlStatement, NpgsqlConnection connection)
        {
            using (var sqlCommand = GetPostgreSqlCommand(sqlStatement, connection, CommandType.Text))
            {
                return sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Executes multiple SQL statements within a PostgreSQL transaction to ensure atomicity.
        /// </summary>
        /// <param name="sqlStatements">A list of SQL statements to execute.</param>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> object used to connect to the database.</param>
        /// <returns>
        /// Returns <c>true</c> if the transaction is committed successfully; 
        /// otherwise, <c>false</c> if an error occurs and the transaction is rolled back.
        /// </returns>
        /// <exception cref="Exception">
        /// Logs and handles exceptions if any SQL command execution fails.
        /// </exception>
        internal static bool ExecuteTransaction(List<string> sqlStatements, NpgsqlConnection connection)
        {
            using (NpgsqlTransaction transaction = GetPostgreSqlTransaction(connection))
            {
                try
                {
                    foreach (var sqlStatement in sqlStatements)
                    {
                        using (var sqlCommand = GetPostgreSqlCommand(sqlStatement, connection, transaction))
                        {
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
        /// Asynchronously executes the specified SQL command and returns 'PostgreSQL' data reader.
        /// </summary>
        /// <param name="cmdText">The text of the query.</param>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> object used to connect to the database.</param>
        /// <param name="commandType">Sql command type.</param>
        /// <returns>A <see cref="NpgsqlDataReader"/> object that can be used to read the query results.</returns>
        internal async Task<NpgsqlDataReader> GetPostgreSqlReaderAsync(string cmdText, NpgsqlConnection connection, CommandType commandType)
        {
            await connection.OpenAsync();
            using (var sqlCommand = new NpgsqlCommand(cmdText, connection) { CommandType = commandType })
            {
                return await sqlCommand.ExecuteReaderAsync();
            }
        }

        /// <summary>
        /// Asynchronously creates and returns a new <see cref="NpgsqlCommand"/> with the specified command text, 
        /// connection, and command type. Opens the connection before creating the command.
        /// </summary>
        /// <param name="cmdText">The SQL command text to execute.</param>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> object used to connect to the database.</param>
        /// <param name="commandType">The type of the command (e.g., Text, StoredProcedure).</param>
        /// <returns>A configured <see cref="NpgsqlCommand"/> instance.</returns>
        internal async Task<NpgsqlCommand> GetPostgreSqlCommandAsync(string cmdText, NpgsqlConnection connection, CommandType commandType)
        {
            await connection.OpenAsync();
            var sqlCommand = new NpgsqlCommand(cmdText, connection) { CommandType = commandType };
            return sqlCommand;
        }

        /// <summary>
        /// Asynchronously initiates and returns a new PostgreSQL transaction for the specified database connection.
        /// </summary>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> object used to connect to the database.</param>
        /// <returns>A new <see cref="NpgsqlTransaction"/> associated with the provided connection.</returns>
        internal static async Task<NpgsqlTransaction> GetPostgreSqlTransactionAsync(NpgsqlConnection connection)
        {
            await connection.OpenAsync();
            var npgsqlTransaction = connection.BeginTransaction();
            return npgsqlTransaction;

        }
        /// <summary>
        /// Asynchronously executes and retrieves records for 'Select' queries from the database.
        /// Converts column names to keys holding values, with multiple database rows returned into a list.
        /// Note: Use aliases in query for similar column names.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> object used to connect to the database.</param>
        /// <param name="upperCaseKeys">Boolean parameter to return dictionary keys in uppercase.</param>
        /// <returns>List of <see cref="DataDictionary"/> with column names as keys holding values into a list for multiple rows of data.
        /// Note: Byte[] is returned as Base64 string.</returns>
        internal async Task<List<DataDictionary>> FetchDataAsync(string selectSql, NpgsqlConnection connection, bool upperCaseKeys)
        {
            var dataList = new List<DataDictionary>();
            using (var reader = await GetPostgreSqlReaderAsync(selectSql, connection, CommandType.Text))
            {
                while (await reader.ReadAsync())
                {
                    var addRow = new DataDictionary();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string key = upperCaseKeys ? reader.GetName(i).ToUpper() : reader.GetName(i);
                        if (reader.GetValue(i) is byte[] value)
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
        /// <param name="connection">The <see cref="NpgsqlConnection"/> object used to connect to the database.</param>
        /// <param name="strict">Enables fetch data only for object type <typeparamref name="T"/> properties existing in database query result.</param>
        /// <returns>List of data rows mapped into object of type <typeparamref name="T"/>.</returns>
        internal async Task<List<T>> FetchDataAsync<T>(string selectSql, NpgsqlConnection connection, bool strict) where T : new()
        {
            var dataList = new List<T>();
            using (var reader = await GetPostgreSqlReaderAsync(selectSql, connection, CommandType.Text))
            {
                while (await reader.ReadAsync())
                {
                    var addObjectRow = new T();
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        if ((strict || Utils.ColumnExists(reader, prop.Name)) && !await reader.IsDBNullAsync(reader.GetOrdinal(prop.Name)))
                            prop.SetValue(addObjectRow, reader[prop.Name]);
                    }
                    dataList.Add(addObjectRow);
                }
            }
            return dataList;
        }

        /// <summary>
        /// Asynchronously executes the provided SQL statement using the given PostgreSQL connection and returns the first column of the first row in the result set.
        /// If the result is DBNull, an empty string is returned.
        /// </summary>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> to use for executing the SQL statement.</param>
        /// <returns>
        /// A <see cref="string"/> representing the value of the first column of the first row in the result set,
        /// or an empty string if the result is DBNull.
        /// </returns>
        internal async Task<string> ExecuteScalarAsync(string sqlStatement, NpgsqlConnection connection)
        {
            using (var sqlCommand = await GetPostgreSqlCommandAsync(sqlStatement, connection, CommandType.Text))
            {
                var result = await sqlCommand.ExecuteScalarAsync();
                return result == null || result == DBNull.Value ? string.Empty : result.ToString();
            }
        }

        /// <summary>
        /// Asynchronously executes the provided SQL statement using the given PostgreSQL connection and returns the first column of the first row in the result set,
        /// converted to the specified type <typeparamref name="T"/>. If the result is DBNull, the default value of <typeparamref name="T"/> is returned.
        /// </summary>
        /// <typeparam name="T">The type to which the result should be converted.</typeparam>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> to use for executing the SQL statement.</param>
        /// <returns>
        /// The value of the first column of the first row in the result set, converted to type <typeparamref name="T"/>,
        /// or the default value of <typeparamref name="T"/> if the result is DBNull.
        /// </returns>
        internal async Task<T> ExecuteScalarAsync<T>(string sqlStatement, NpgsqlConnection connection)
        {
            using (var sqlCommand = await GetPostgreSqlCommandAsync(sqlStatement, connection, CommandType.Text))
            {
                var result = await sqlCommand.ExecuteScalarAsync();
                return result == null || result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
            }
        }

        /// <summary>
        /// Asynchronously executes a SQL statement that does not return a result set.
        /// </summary>
        /// <param name="sqlStatement">SQL statement to execute.</param>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> object used to connect to the database.</param>
        /// <returns>The number of rows affected by the execution of the SQL statement.</returns>
        internal async Task<int> ExecuteCommandAsync(string sqlStatement, NpgsqlConnection connection)
        {
            using (var sqlCommand = await GetPostgreSqlCommandAsync(sqlStatement, connection, CommandType.Text))
            {
                return await sqlCommand.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Asynchronously executes multiple SQL statements within a PostgreSQL transaction to ensure atomicity.
        /// </summary>
        /// <param name="sqlStatements">A list of SQL statements to execute.</param>
        /// <param name="connection">The <see cref="NpgsqlConnection"/> object used to connect to the database.</param>
        /// <returns>
        /// Returns <c>true</c> if the transaction is committed successfully; 
        /// otherwise, <c>false</c> if an error occurs and the transaction is rolled back.
        /// </returns>
        /// <exception cref="Exception">
        /// Logs and handles exceptions if any SQL command execution fails.
        /// </exception>
        internal static async Task<bool> ExecuteTransactionAsync(List<string> sqlStatements, NpgsqlConnection connection)
        {
            using (NpgsqlTransaction transaction = await GetPostgreSqlTransactionAsync(connection))
            {
                try
                {
                    foreach (var sqlStatement in sqlStatements)
                    {
                        using (var sqlCommand = GetPostgreSqlCommand(sqlStatement, connection, transaction))
                        {
                            await sqlCommand.ExecuteNonQueryAsync();
                        }
                    }
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Transaction rolled back due to error: {ex.Message}");
                    return false;
                }
            }
        }

        #endregion

    }
}
