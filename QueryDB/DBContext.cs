using QueryDB.Exceptions;
using QueryDB.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace QueryDB
{
    /// <summary>
    /// Allows to connect to a database type and run commands.
    /// </summary>
    public sealed class DBContext : IDBContext
    {
        /// <summary>
        /// Database type value to connect to.
        /// </summary>
        internal static DB Database { get; private set; }

        /// <summary>
        /// Holds 'SQL Server' connection string value for the DBContext created.
        /// </summary>
        internal static string SqlConnectionString { get; private set; }

        /// <summary>
        /// Holds 'MySQL' connection string value for the DBContext created.
        /// </summary>
        internal static string MySqlConnectionString { get; private set; }

        /// <summary>
        /// Holds 'Oracle' connection string value for the DBContext created.
        /// </summary>
        internal static string OracleConnectionString { get; private set; }

        /// <summary>
        /// Holds 'PostgreSQL' connection string value for the DBContext created.
        /// </summary>
        internal static string PostgreSqlConnectionString { get; private set; }

        /// <summary>
        /// Defines database type and connection string for connection.
        /// </summary>
        /// <param name="database">'DB' enum value for database type.</param>
        /// <param name="connectionString">Connection string for the database selected.</param>
        public DBContext(DB database, string connectionString)
        {
            Database = database;
            if (Database.Equals(DB.MSSQL))
                SqlConnectionString = connectionString;
            else if (Database.Equals(DB.MySQL))
                MySqlConnectionString = connectionString;
            else if(Database.Equals(DB.Oracle))
                OracleConnectionString = connectionString;
            else if (Database.Equals(DB.PostgreSQL))
                PostgreSqlConnectionString = connectionString;
        }

        /// <summary>
        /// Retrieves records for 'Select' queries from the database.
        /// Converts column names to keys holding values, with multiple database rows returned into a list.
        /// Note: Use aliases in query for similar column names.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="upperCaseKeys">Boolean parameter to return dictionary keys in uppercase. Default - 'false'.</param>
        /// <returns>List of data Dictionary with column names as keys holding values into a list for multiple rows of data.</returns>
        public List<DataDictionary> FetchData(string selectSql, bool upperCaseKeys = false)
        {
            var dataList = new List<DataDictionary>();
            if (Database.Equals(DB.MSSQL))
            {
                using (var msSqlDBConnection = GetSqlServerConnection())
                {
                    var _systemAdapter = new MSSQL.Adapter();
                    dataList = _systemAdapter.FetchData(selectSql, msSqlDBConnection.SqlConnection, upperCaseKeys);
                }
            }
            else if (Database.Equals(DB.MySQL))
            {
                using (var mySqlDBConnection = GetMySqlConnection())
                {
                    var _systemAdapter = new MySQL.Adapter();
                    dataList = _systemAdapter.FetchData(selectSql, mySqlDBConnection.MySqlConnection, upperCaseKeys);
                }
            }
            else if (Database.Equals(DB.Oracle))
            {
                using (var oracleDBConnection = GetOracleConnection())
                {
                    var _systemAdapter = new Oracle.Adapter();
                    dataList = _systemAdapter.FetchData(selectSql, oracleDBConnection.OracleConnection, upperCaseKeys);
                }
            }
            else if (Database.Equals(DB.PostgreSQL))
            {
                using (var postgreSqlDBConnection = GetPostgreSqlConnection())
                {
                    var _systemAdapter = new PostgreSQL.Adapter();
                    dataList = _systemAdapter.FetchData(selectSql, postgreSqlDBConnection.PostgreSQLConnection, upperCaseKeys);
                }
            }
            return dataList;
        }

        /// <summary>
        ///  Retrieves records for 'Select' queries from the database.
        /// </summary>
        /// <typeparam name="T">Object entity to return data mapped into.</typeparam>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="strict">Enables fetch data only for object <T> properties existing in database query result. Default - 'false'.</param>
        /// <returns>List of data rows mapped into object entity into a list for multiple rows of data.</returns>
        public List<T> FetchData<T>(string selectSql, bool strict = false) where T : new()
        {
            var dataList = new List<T>();
            if (Database.Equals(DB.MSSQL))
            {
                using (var msSqlDBConnection = GetSqlServerConnection())
                {
                    var _systemAdapter = new MSSQL.Adapter();
                    dataList = _systemAdapter.FetchData<T>(selectSql, msSqlDBConnection.SqlConnection, strict);
                }
            }
            else if (Database.Equals(DB.MySQL))
            {
                using (var mySqlDBConnection = GetMySqlConnection())
                {
                    var _systemAdapter = new MySQL.Adapter();
                    dataList = _systemAdapter.FetchData<T>(selectSql, mySqlDBConnection.MySqlConnection, strict);
                }
            }
            else if (Database.Equals(DB.Oracle))
            {
                using (var oracleDBConnection = GetOracleConnection())
                {
                    var _systemAdapter = new Oracle.Adapter();
                    dataList = _systemAdapter.FetchData<T>(selectSql, oracleDBConnection.OracleConnection, strict);
                }
            }
            else if (Database.Equals(DB.PostgreSQL))
            {
                using (var postgreSqlDBConnection = GetPostgreSqlConnection())
                {
                    var _systemAdapter = new PostgreSQL.Adapter();
                    dataList = _systemAdapter.FetchData<T>(selectSql, postgreSqlDBConnection.PostgreSQLConnection, strict);
                }
            }
            return dataList;
        }

        /// <summary>
        /// Executes a SQL query and returns the result as a string.
        /// </summary>
        /// <param name="sqlStatement">The SQL query to execute.</param>
        /// <returns>A string representing the result of the query. If the result is DBNull, an empty string is returned.</returns>
        public string ExecuteScalar(string sqlStatement)
        {
            if (!Regex.IsMatch(sqlStatement, @"^\s*SELECT\s+.*", RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)))
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedExecuteScalarCommand,
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedExecuteScalarCommand);
            var value = string.Empty;
            if (Database.Equals(DB.MSSQL))
            {
                using (var msSqlDBConnection = GetSqlServerConnection())
                {
                    var _systemAdapter = new MSSQL.Adapter();
                    value = _systemAdapter.ExecuteScalar(sqlStatement, msSqlDBConnection.SqlConnection);
                }
            }
            else if (Database.Equals(DB.MySQL))
            {
                using (var mySqlDBConnection = GetMySqlConnection())
                {
                    var _systemAdapter = new MySQL.Adapter();
                    value = _systemAdapter.ExecuteScalar(sqlStatement, mySqlDBConnection.MySqlConnection);
                }
            }
            else if (Database.Equals(DB.Oracle))
            {
                using (var oracleDBConnection = GetOracleConnection())
                {
                    var _systemAdapter = new Oracle.Adapter();
                    value = _systemAdapter.ExecuteScalar(sqlStatement, oracleDBConnection.OracleConnection);
                }
            }
            else if (Database.Equals(DB.PostgreSQL))
            {
                using (var postgreSqlDBConnection = GetPostgreSqlConnection())
                {
                    var _systemAdapter = new PostgreSQL.Adapter();
                    value = _systemAdapter.ExecuteScalar(sqlStatement, postgreSqlDBConnection.PostgreSQLConnection);
                }
            }
            return value;
        }

        /// <summary>
        /// Executes a SQL query and returns the result as the specified type.
        /// </summary>
        /// <typeparam name="T">The type to which the result should be converted.</typeparam>
        /// <param name="sqlStatement">The SQL query to execute.</param>
        /// <returns>The result of the query, converted to the specified type. If the result is DBNull, the default value for the type is returned.</returns>
        public T ExecuteScalar<T>(string sqlStatement)
        {
            if (!Regex.IsMatch(sqlStatement, @"^\s*SELECT\s+.*", RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)))
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedExecuteScalarCommand,
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedExecuteScalarCommand);
            var value = default(T);
            if (Database.Equals(DB.MSSQL))
            {
                using (var msSqlDBConnection = GetSqlServerConnection())
                {
                    var _systemAdapter = new MSSQL.Adapter();
                    value = _systemAdapter.ExecuteScalar<T>(sqlStatement, msSqlDBConnection.SqlConnection);
                }
            }
            else if (Database.Equals(DB.MySQL))
            {
                using (var mySqlDBConnection = GetMySqlConnection())
                {
                    var _systemAdapter = new MySQL.Adapter();
                    value = _systemAdapter.ExecuteScalar<T>(sqlStatement, mySqlDBConnection.MySqlConnection);
                }
            }
            else if (Database.Equals(DB.Oracle))
            {
                using (var oracleDBConnection = GetOracleConnection())
                {
                    var _systemAdapter = new Oracle.Adapter();
                    value = _systemAdapter.ExecuteScalar<T>(sqlStatement, oracleDBConnection.OracleConnection);
                }
            }
            else if (Database.Equals(DB.PostgreSQL))
            {
                using (var postgreSqlDBConnection = GetPostgreSqlConnection())
                {
                    var _systemAdapter = new PostgreSQL.Adapter();
                    value = _systemAdapter.ExecuteScalar<T>(sqlStatement, postgreSqlDBConnection.PostgreSQLConnection);
                }
            }
            return value;
        }

        /// <summary>
        /// Executes SQL commands.
        /// </summary>
        /// <param name="sqlStatement">SQL statement as command.</param>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteCommand(string sqlStatement)
        {
            if (Regex.IsMatch(sqlStatement, "^\\s*SELECT\\s+.*", RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)))
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedSelectExecuteCommand,
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedSelectExecuteCommand);
            if (Database.Equals(DB.MSSQL))
            {
                using (var msSqlDBConnection = GetSqlServerConnection())
                {
                    var _systemAdapter = new MSSQL.Adapter();
                    return _systemAdapter.ExecuteCommand(sqlStatement, msSqlDBConnection.SqlConnection);
                }
            }
            else if (Database.Equals(DB.MySQL))
            {
                using (var mySqlDBConnection = GetMySqlConnection())
                {
                    var _systemAdapter = new MySQL.Adapter();
                    return _systemAdapter.ExecuteCommand(sqlStatement, mySqlDBConnection.MySqlConnection);
                }
            }
            else if (Database.Equals(DB.Oracle))
            {
                using (var oracleDBConnection = GetOracleConnection())
                {
                    var _systemAdapter = new Oracle.Adapter();
                    return _systemAdapter.ExecuteCommand(sqlStatement, oracleDBConnection.OracleConnection);
                }
            }
            else if (Database.Equals(DB.PostgreSQL))
            {
                using (var postgreSqlDBConnection = GetPostgreSqlConnection())
                {
                    var _systemAdapter = new PostgreSQL.Adapter();
                    return _systemAdapter.ExecuteCommand(sqlStatement, postgreSqlDBConnection.PostgreSQLConnection);
                }
            }
            return -1;
        }

        /// <summary>
        /// Executes multiple SQL statements within a transaction, ensuring that all statements are executed together.
        /// </summary>
        /// <param name="sqlStatements">A list of SQL statements to execute.</param>
        /// <returns>
        /// Returns <c>true</c> if all statements are executed successfully and the transaction is committed;
        /// <c>false</c> if any statement fails and the transaction is rolled back.
        /// </returns>
        public bool ExecuteTransaction(List<string> sqlStatements)
        {
            var selectExists = sqlStatements.Any(sqlStatement => Regex.IsMatch(sqlStatement, "^\\s*SELECT\\s+.*", RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)));
            if (selectExists)
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedSelectExecuteTransaction,
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedSelectExecuteTransaction);
            if (Database.Equals(DB.MSSQL))
            {
                using (var msSqlDBConnection = GetSqlServerConnection())
                {
                    return MSSQL.Adapter.ExecuteTransaction(sqlStatements, msSqlDBConnection.SqlConnection);
                }
            }
            else if (Database.Equals(DB.MySQL))
            {
                using (var mySqlDBConnection = GetMySqlConnection())
                {
                    return MySQL.Adapter.ExecuteTransaction(sqlStatements, mySqlDBConnection.MySqlConnection);
                }
            }
            else if (Database.Equals(DB.Oracle))
            {
                using (var oracleDBConnection = GetOracleConnection())
                {
                    return Oracle.Adapter.ExecuteTransaction(sqlStatements, oracleDBConnection.OracleConnection);
                }
            }
            else if (Database.Equals(DB.PostgreSQL))
            {
                using (var postgreSqlDBConnection = GetPostgreSqlConnection())
                {
                    return PostgreSQL.Adapter.ExecuteTransaction(sqlStatements, postgreSqlDBConnection.PostgreSQLConnection);
                }
            }
            return false;
        }

        /// <summary>
        /// Gets 'SQL Server' connection.
        /// </summary>
        /// <returns>'SQL Server' Connection.</returns>
        internal static MSSQL.Connection GetSqlServerConnection()
        {
            return ConnectionBuilder.GetSqlConnection;
        }

        /// <summary>
        /// Gets 'MySQL' connection.
        /// </summary>
        /// <returns>'MySQL' Connection.</returns>
        internal static MySQL.Connection GetMySqlConnection()
        {
            return ConnectionBuilder.GetMySqlConnection;
        }

        /// <summary>
        /// Gets 'Oracle' connection.
        /// </summary>
        /// <returns>'Oracle' connection.</returns>
        internal static Oracle.Connection GetOracleConnection()
        {
            return ConnectionBuilder.GetOracleConnection;
        }

        /// <summary>
        /// Gets 'PostgreSQL' connection.
        /// </summary>
        /// <returns>'PostgreSQL' connection.</returns>
        internal static PostgreSQL.Connection GetPostgreSqlConnection()
        {
            return ConnectionBuilder.GetPostgreSqlConnection;
        }
    }

    /// <summary>
    /// Database Type.
    /// </summary>
    public enum DB
    {
        MSSQL = 1,
        MySQL = 2,
        Oracle = 3,
        PostgreSQL = 4
    }
}
