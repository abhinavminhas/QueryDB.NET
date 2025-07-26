using QueryDB.Exceptions;
using QueryDB.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
        /// Executes and retrieves records for 'Select' queries from the database.
        /// Converts column names to keys holding values, with multiple database rows returned into a list.
        /// Note: Use aliases in query for similar column names.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="upperCaseKeys">Boolean parameter to return dictionary keys in uppercase. Default - <c>false</c>.</param>
        /// <returns>List of <see cref="DataDictionary"/> with column names as keys holding values into a list for multiple rows of data.</returns>
        public List<DataDictionary> FetchData(string selectSql, bool upperCaseKeys = false)
        {
            if (!Regex.IsMatch(selectSql, Utils.SelectQueryPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)))
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedFetchDataCommand,
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedFetchDataCommand);
            var dataList = new List<DataDictionary>();
            switch (Database)
            {
                case DB.MSSQL:
                    using (var connection = GetSqlServerConnection())
                    {
                        var adapter = new MSSQL.Adapter();
                        dataList = adapter.FetchData(selectSql, connection.SqlConnection, upperCaseKeys);
                    }
                    break;
                case DB.MySQL:
                    using (var connection = GetMySqlConnection())
                    {
                        var adapter = new MySQL.Adapter();
                        dataList = adapter.FetchData(selectSql, connection.MySqlConnection, upperCaseKeys);
                    }
                    break;
                case DB.Oracle:
                    using (var connection = GetOracleConnection())
                    {
                        var adapter = new Oracle.Adapter();
                        dataList = adapter.FetchData(selectSql, connection.OracleConnection, upperCaseKeys);
                    }
                    break;
                case DB.PostgreSQL:
                    using (var connection = GetPostgreSqlConnection())
                    {
                        var adapter = new PostgreSQL.Adapter();
                        dataList = adapter.FetchData(selectSql, connection.PostgreSQLConnection, upperCaseKeys);
                    }
                    break;
            }
            return dataList;
        }

        /// <summary>
        /// Executes and retrieves records for 'Select' queries from the database.
        /// </summary>
        /// <typeparam name="T">Object entity to return data mapped into.</typeparam>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="strict">Enables fetch data only for object type <typeparamref name="T"/> properties existing in database query result. Default - <c>false</c>.</param>
        /// <returns>List of data rows mapped into object of type <typeparamref name="T"/>.</returns>
        public List<T> FetchData<T>(string selectSql, bool strict = false) where T : new()
        {
            if (!Regex.IsMatch(selectSql, Utils.SelectQueryPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)))
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedFetchDataCommand,
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedFetchDataCommand);
            var dataList = new List<T>();
            switch (Database)
            {
                case DB.MSSQL:
                    using (var connection = GetSqlServerConnection())
                    {
                        var adapter = new MSSQL.Adapter();
                        dataList = adapter.FetchData<T>(selectSql, connection.SqlConnection, strict);
                    }
                    break;
                case DB.MySQL:
                    using (var connection = GetMySqlConnection())
                    {
                        var adapter = new MySQL.Adapter();
                        dataList = adapter.FetchData<T>(selectSql, connection.MySqlConnection, strict);
                    }
                    break;
                case DB.Oracle:
                    using (var connection = GetOracleConnection())
                    {
                        var adapter = new Oracle.Adapter();
                        dataList = adapter.FetchData<T>(selectSql, connection.OracleConnection, strict);
                    }
                    break;
                case DB.PostgreSQL:
                    using (var connection = GetPostgreSqlConnection())
                    {
                        var adapter = new PostgreSQL.Adapter();
                        dataList = adapter.FetchData<T>(selectSql, connection.PostgreSQLConnection, strict);
                    }
                    break;
            }
            return dataList;
        }

        /// <summary>
        /// Asynchronously executes and retrieves records for 'Select' queries from the database.
        /// Converts column names to keys holding values, with multiple database rows returned into a list.
        /// Note: Use aliases in query for similar column names.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="upperCaseKeys">Boolean parameter to return dictionary keys in uppercase. Default - <c>false</c>.</param>
        /// <returns>List of <see cref="DataDictionary"/> with column names as keys holding values into a list for multiple rows of data.</returns>
        public async Task<List<DataDictionary>> FetchDataAsync(string selectSql, bool upperCaseKeys = false)
        {
            if (!Regex.IsMatch(selectSql, Utils.SelectQueryPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)))
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedFetchDataCommand, 
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedFetchDataCommand);
            var dataList = new List<DataDictionary>();
            switch (Database)
            {
                case DB.MSSQL:
                    using (var connection = GetSqlServerConnection())
                    {
                        var adapter = new MSSQL.Adapter();
                        dataList = await adapter.FetchDataAsync(selectSql, connection.SqlConnection, upperCaseKeys);
                    }
                    break;
                case DB.MySQL:
                    using (var connection = GetMySqlConnection())
                    {
                        var adapter = new MySQL.Adapter();
                        dataList = await adapter.FetchDataAsync(selectSql, connection.MySqlConnection, upperCaseKeys);
                    }
                    break;
                case DB.Oracle:
                    using (var connection = GetOracleConnection())
                    {
                        var adapter = new Oracle.Adapter();
                        dataList = await adapter.FetchDataAsync(selectSql, connection.OracleConnection, upperCaseKeys);
                    }
                    break;
                case DB.PostgreSQL:
                    using (var connection = GetPostgreSqlConnection())
                    {
                        var adapter = new PostgreSQL.Adapter();
                        dataList = await adapter.FetchDataAsync(selectSql, connection.PostgreSQLConnection, upperCaseKeys);
                    }
                    break;
            }
            return dataList;
        }

        /// <summary>
        /// Asynchronously executes and retrieves records for 'Select' queries from the database.
        /// </summary>
        /// <typeparam name="T">Object entity to return data mapped into.</typeparam>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="strict">Enables fetch data only for object type <typeparamref name="T"/> properties existing in database query result. Default - <c>false</c>.</param>
        /// <returns>List of data rows mapped into object of type <typeparamref name="T"/>.</returns>
        public async Task<List<T>> FetchDataAsync<T>(string selectSql, bool strict = false) where T : new()
        {
            if (!Regex.IsMatch(selectSql, Utils.SelectQueryPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)))
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedFetchDataCommand, 
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedFetchDataCommand);
            var dataList = new List<T>();
            switch (Database)
            {
                case DB.MSSQL:
                    using (var connection = GetSqlServerConnection())
                    {
                        var adapter = new MSSQL.Adapter();
                        dataList = await adapter.FetchDataAsync<T>(selectSql, connection.SqlConnection, strict);
                    }
                    break;
                case DB.MySQL:
                    using (var connection = GetMySqlConnection())
                    {
                        var adapter = new MySQL.Adapter();
                        dataList = await adapter.FetchDataAsync<T>(selectSql, connection.MySqlConnection, strict);
                    }
                    break;
                case DB.Oracle:
                    using (var connection = GetOracleConnection())
                    {
                        var adapter = new Oracle.Adapter();
                        dataList = await adapter.FetchDataAsync<T>(selectSql, connection.OracleConnection, strict);
                    }
                    break;
                case DB.PostgreSQL:
                    using (var connection = GetPostgreSqlConnection())
                    {
                        var adapter = new PostgreSQL.Adapter();
                        dataList = await adapter.FetchDataAsync<T>(selectSql, connection.PostgreSQLConnection, strict);
                    }
                    break;
            }
            return dataList;
        }

        /// <summary>
        /// Executes the provided SQL statement and returns the first column of the first row in the result set.
        /// If the result is DBNull, an empty string is returned.
        /// </summary>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <returns>
        /// A <see cref="string"/> representing the value of the first column of the first row in the result set,
        /// or an empty string if the result is DBNull.
        /// </returns>
        public string ExecuteScalar(string sqlStatement)
        {
            if (!Regex.IsMatch(sqlStatement, Utils.SelectQueryPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)))
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedExecuteScalarCommand, 
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedExecuteScalarCommand);
            var value = string.Empty;
            switch (Database)
            {
                case DB.MSSQL:
                    using (var connection = GetSqlServerConnection())
                    {
                        var adapter = new MSSQL.Adapter();
                        value = adapter.ExecuteScalar(sqlStatement, connection.SqlConnection);
                    }
                    break;
                case DB.MySQL:
                    using (var connection = GetMySqlConnection())
                    {
                        var adapter = new MySQL.Adapter();
                        value = adapter.ExecuteScalar(sqlStatement, connection.MySqlConnection);
                    }
                    break;
                case DB.Oracle:
                    using (var connection = GetOracleConnection())
                    {
                        var adapter = new Oracle.Adapter();
                        value = adapter.ExecuteScalar(sqlStatement, connection.OracleConnection);
                    }
                    break;
                case DB.PostgreSQL:
                    using (var connection = GetPostgreSqlConnection())
                    {
                        var adapter = new PostgreSQL.Adapter();
                        value = adapter.ExecuteScalar(sqlStatement, connection.PostgreSQLConnection);
                    }
                    break;
            }
            return value;
        }

        /// <summary>
        /// Executes the provided SQL statement and returns the first column of the first row in the result set,
        /// converted to the specified type <typeparamref name="T"/>. If the result is DBNull, the default value of <typeparamref name="T"/> is returned.
        /// </summary>
        /// <typeparam name="T">The type to which the result should be converted.</typeparam>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <returns>
        /// The value of the first column of the first row in the result set, converted to type <typeparamref name="T"/>,
        /// or the default value of <typeparamref name="T"/> if the result is DBNull.
        /// </returns>
        public T ExecuteScalar<T>(string sqlStatement)
        {
            if (!Regex.IsMatch(sqlStatement, Utils.SelectQueryPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)))
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedExecuteScalarCommand, 
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedExecuteScalarCommand);
            var value = default(T);
            switch (Database)
            {
                case DB.MSSQL:
                    using (var connection = GetSqlServerConnection())
                    {
                        var adapter = new MSSQL.Adapter();
                        value = adapter.ExecuteScalar<T>(sqlStatement, connection.SqlConnection);
                    }
                    break;
                case DB.MySQL:
                    using (var connection = GetMySqlConnection())
                    {
                        var adapter = new MySQL.Adapter();
                        value = adapter.ExecuteScalar<T>(sqlStatement, connection.MySqlConnection);
                    }
                    break;
                case DB.Oracle:
                    using (var connection = GetOracleConnection())
                    {
                        var adapter = new Oracle.Adapter();
                        value = adapter.ExecuteScalar<T>(sqlStatement, connection.OracleConnection);
                    }
                    break;
                case DB.PostgreSQL:
                    using (var connection = GetPostgreSqlConnection())
                    {
                        var adapter = new PostgreSQL.Adapter();
                        value = adapter.ExecuteScalar<T>(sqlStatement, connection.PostgreSQLConnection);
                    }
                    break;
            }
            return value;
        }

        /// <summary>
        /// Asynchronously executes the provided SQL statement and returns the first column of the first row in the result set.
        /// If the result is DBNull, an empty string is returned.
        /// </summary>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <returns>
        /// A <see cref="string"/> representing the value of the first column of the first row in the result set,
        /// or an empty string if the result is DBNull.
        /// </returns>
        public async Task<string> ExecuteScalarAsync(string sqlStatement)
        {
            if (!Regex.IsMatch(sqlStatement, Utils.SelectQueryPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)))
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedExecuteScalarCommand, 
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedExecuteScalarCommand);
            var value = string.Empty;
            switch (Database)
            {
                case DB.MSSQL:
                    using (var connection = GetSqlServerConnection())
                    {
                        var adapter = new MSSQL.Adapter();
                        value = await adapter.ExecuteScalarAsync(sqlStatement, connection.SqlConnection);
                    }
                    break;
                case DB.MySQL:
                    using (var connection = GetMySqlConnection())
                    {
                        var adapter = new MySQL.Adapter();
                        value = await adapter.ExecuteScalarAsync(sqlStatement, connection.MySqlConnection);
                    }
                    break;
                case DB.Oracle:
                    using (var connection = GetOracleConnection())
                    {
                        var adapter = new Oracle.Adapter();
                        value = await adapter.ExecuteScalarAsync(sqlStatement, connection.OracleConnection);
                    }
                    break;
                case DB.PostgreSQL:
                    using (var connection = GetPostgreSqlConnection())
                    {
                        var adapter = new PostgreSQL.Adapter();
                        value = await adapter.ExecuteScalarAsync(sqlStatement, connection.PostgreSQLConnection);
                    }
                    break;
            }
            return value;
        }

        /// <summary>
        /// Asynchronously executes the provided SQL statement and returns the first column of the first row in the result set,
        /// converted to the specified type <typeparamref name="T"/>. If the result is DBNull, the default value of <typeparamref name="T"/> is returned.
        /// </summary>
        /// <typeparam name="T">The type to which the result should be converted.</typeparam>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <returns>
        /// The value of the first column of the first row in the result set, converted to type <typeparamref name="T"/>,
        /// or the default value of <typeparamref name="T"/> if the result is DBNull.
        /// </returns>
        public async Task<T> ExecuteScalarAsync<T>(string sqlStatement)
        {
            if (!Regex.IsMatch(sqlStatement, Utils.SelectQueryPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)))
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedExecuteScalarCommand, 
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedExecuteScalarCommand);
            var value = default(T);
            switch (Database)
            {
                case DB.MSSQL:
                    using (var connection = GetSqlServerConnection())
                    {
                        var adapter = new MSSQL.Adapter();
                        value = await adapter.ExecuteScalarAsync<T>(sqlStatement, connection.SqlConnection);
                    }
                    break;
                case DB.MySQL:
                    using (var connection = GetMySqlConnection())
                    {
                        var adapter = new MySQL.Adapter();
                        value = await adapter.ExecuteScalarAsync<T>(sqlStatement, connection.MySqlConnection);
                    }
                    break;
                case DB.Oracle:
                    using (var connection = GetOracleConnection())
                    {
                        var adapter = new Oracle.Adapter();
                        value = await adapter.ExecuteScalarAsync<T>(sqlStatement, connection.OracleConnection);
                    }
                    break;
                case DB.PostgreSQL:
                    using (var connection = GetPostgreSqlConnection())
                    {
                        var adapter = new PostgreSQL.Adapter();
                        value = await adapter.ExecuteScalarAsync<T>(sqlStatement, connection.PostgreSQLConnection);
                    }
                    break;
            }
            return value;
        }

        /// <summary>
        /// Executes a SQL statement that does not return a result set.
        /// </summary>
        /// <param name="sqlStatement">SQL statement to execute.</param>
        /// <returns>The number of rows affected by the execution of the SQL statement.</returns>
        public int ExecuteCommand(string sqlStatement)
        {
            if (Regex.IsMatch(sqlStatement, Utils.SelectQueryPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)))
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedSelectExecuteCommand, 
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedSelectExecuteCommand);
            switch (Database)
            {
                case DB.MSSQL:
                    using (var connection = GetSqlServerConnection())
                    {
                        var adapter = new MSSQL.Adapter();
                        return adapter.ExecuteCommand(sqlStatement, connection.SqlConnection);
                    }
                case DB.MySQL:
                    using (var connection = GetMySqlConnection())
                    {
                        var adapter = new MySQL.Adapter();
                        return adapter.ExecuteCommand(sqlStatement, connection.MySqlConnection);
                    }
                case DB.Oracle:
                    using (var connection = GetOracleConnection())
                    {
                        var adapter = new Oracle.Adapter();
                        return adapter.ExecuteCommand(sqlStatement, connection.OracleConnection);
                    }
                case DB.PostgreSQL:
                    using (var connection = GetPostgreSqlConnection())
                    {
                        var adapter = new PostgreSQL.Adapter();
                        return adapter.ExecuteCommand(sqlStatement, connection.PostgreSQLConnection);
                    }
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Asynchronously executes a SQL statement that does not return a result set.
        /// </summary>
        /// <param name="sqlStatement">SQL statement to execute.</param>
        /// <returns>The number of rows affected by the execution of the SQL statement.</returns>
        public async Task<int> ExecuteCommandAsync(string sqlStatement)
        {
            if (Regex.IsMatch(sqlStatement, Utils.SelectQueryPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)))
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedSelectExecuteCommand, 
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedSelectExecuteCommand);
            switch (Database)
            {
                case DB.MSSQL:
                    using (var connection = GetSqlServerConnection())
                    {
                        var adapter = new MSSQL.Adapter();
                        return await adapter.ExecuteCommandAsync(sqlStatement, connection.SqlConnection);
                    }
                case DB.MySQL:
                    using (var connection = GetMySqlConnection())
                    {
                        var adapter = new MySQL.Adapter();
                        return await adapter.ExecuteCommandAsync(sqlStatement, connection.MySqlConnection);
                    }
                case DB.Oracle:
                    using (var connection = GetOracleConnection())
                    {
                        var adapter = new Oracle.Adapter();
                        return await adapter.ExecuteCommandAsync(sqlStatement, connection.OracleConnection);
                    }
                case DB.PostgreSQL:
                    using (var connection = GetPostgreSqlConnection())
                    {
                        var adapter = new PostgreSQL.Adapter();
                        return await adapter.ExecuteCommandAsync(sqlStatement, connection.PostgreSQLConnection);
                    }
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Executes multiple SQL statements within a transaction, ensuring that all statements are executed together.
        /// </summary>
        /// <param name="sqlStatements">A list of SQL statements to execute.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the outcome of the transaction.
        /// The <see cref="Result.Success"/> property is <c>true</c> if the transaction is committed successfully; 
        /// otherwise, <c>false</c> if an error occurs and the transaction is rolled back.
        /// If an error occurs, the <see cref="Result.Exception"/> property contains the exception details.
        /// </returns>
        public Result ExecuteTransaction(List<string> sqlStatements)
        {
            var result = new Result();
            var selectExists = sqlStatements.Any(sqlStatement => Regex.IsMatch(sqlStatement, Utils.SelectQueryPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)));
            if (selectExists)
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedSelectExecuteTransaction, 
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedSelectExecuteTransaction);
            switch (Database)
            {
                case DB.MSSQL:
                    using (var connection = GetSqlServerConnection())
                    {
                        result = MSSQL.Adapter.ExecuteTransaction(sqlStatements, connection.SqlConnection);
                    }
                    break;
                case DB.MySQL:
                    using (var connection = GetMySqlConnection())
                    {
                        result = MySQL.Adapter.ExecuteTransaction(sqlStatements, connection.MySqlConnection);
                    }
                    break;
                case DB.Oracle:
                    using (var connection = GetOracleConnection())
                    {
                        result = Oracle.Adapter.ExecuteTransaction(sqlStatements, connection.OracleConnection);
                    }
                    break;
                case DB.PostgreSQL:
                    using (var connection = GetPostgreSqlConnection())
                    {
                        result = PostgreSQL.Adapter.ExecuteTransaction(sqlStatements, connection.PostgreSQLConnection);
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Asynchronously executes multiple SQL statements within a transaction, ensuring that all statements are executed together.
        /// </summary>
        /// <param name="sqlStatements">A list of SQL statements to execute.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the outcome of the transaction.
        /// The <see cref="Result.Success"/> property is <c>true</c> if the transaction is committed successfully; 
        /// otherwise, <c>false</c> if an error occurs and the transaction is rolled back.
        /// If an error occurs, the <see cref="Result.Exception"/> property contains the exception details.
        /// </returns>
        public async Task<Result> ExecuteTransactionAsync(List<string> sqlStatements)
        {
            var result = new Result();
            var selectExists = sqlStatements.Any(sqlStatement => Regex.IsMatch(sqlStatement, Utils.SelectQueryPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(5)));
            if (selectExists)
                throw new QueryDBException(QueryDBExceptions.ErrorMessage.UnsupportedSelectExecuteTransaction, 
                    QueryDBExceptions.ErrorType.UnsupportedCommand, QueryDBExceptions.AdditionalInfo.UnsupportedSelectExecuteTransaction);
            switch (Database)
            {
                case DB.MSSQL:
                    using (var connection = GetSqlServerConnection())
                    {
                        result = await MSSQL.Adapter.ExecuteTransactionAsync(sqlStatements, connection.SqlConnection);
                    }
                    break;
                case DB.MySQL:
                    using (var connection = GetMySqlConnection())
                    {
                        result = await MySQL.Adapter.ExecuteTransactionAsync(sqlStatements, connection.MySqlConnection);
                    }
                    break;
                case DB.Oracle:
                    using (var connection = GetOracleConnection())
                    {
                        result = await Oracle.Adapter.ExecuteTransactionAsync(sqlStatements, connection.OracleConnection);
                    }
                    break;
                case DB.PostgreSQL:
                    using (var connection = GetPostgreSqlConnection())
                    {
                        result = await PostgreSQL.Adapter.ExecuteTransactionAsync(sqlStatements, connection.PostgreSQLConnection);
                    }
                    break;
            }
            return result;
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
