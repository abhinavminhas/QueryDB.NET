using Npgsql;
using QueryDB.Resources;
using System;
using System.Collections.Generic;
using System.Data;

namespace QueryDB.PostgreSQL
{
    /// <summary>
    /// 'PostgreSQL' adapter.
    /// </summary>
    internal class Adapter
    {
        /// <summary>
        /// Gets the 'PostgreSQL' data reader.
        /// </summary>
        /// <param name="cmdText">The text of the query.</param>
        /// <param name="connection">'PostgreSQL' connection.</param>
        /// <param name="commandType">Sql command type.</param>
        /// <returns>'PostgreSQL' data reader.</returns>
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
        /// <param name="connection">The <see cref="NpgsqlConnection"/> to use.</param>
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
        /// <param name="connection">The PostgreSQL database connection.</param>
        /// <returns>A new <see cref="NpgsqlTransaction"/> associated with the provided connection.</returns>
        internal static NpgsqlTransaction GetPostgreSqlTransaction(NpgsqlConnection connection)
        {
            connection.Open();
            var npgsqlTransaction = connection.BeginTransaction();
            return npgsqlTransaction;
        }

        /// <summary>
        /// Retrieves records for 'Select' queries from the database.
        /// Converts column names to keys holding values, with multiple database rows returned into a list.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="connection">'PostgreSQL' Connection.</param>
        /// <param name="upperCaseKeys">Boolean parameter to return dictionary keys in uppercase.</param>
        /// <returns>List of data Dictionary with column names as keys holding values into a list for multiple rows of data.
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
        ///  Retrieves records for 'Select' queries from the database.
        /// </summary>
        /// <typeparam name="T">Object entity to return data mapped into.</typeparam>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="connection">'PostgreSQL' Connection.</param>
        /// <param name="strict">Enables fetch data only for object <T> properties existing in database query result.</param>
        /// <returns>List of data rows mapped into object entity into a list for multiple rows of data.</returns>
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
        /// Executes SQL commands.
        /// </summary>
        /// <param name="sqlStatement">SQL statement as command.</param>
        /// <param name="connection">'PostgreSQL' Connection.</param>
        /// <returns>The number of rows affected</returns>
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
        /// <param name="connection">The PostgreSQL database connection.</param>
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
    }
}
