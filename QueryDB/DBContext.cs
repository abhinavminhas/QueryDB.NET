using QueryDB.Resources;
using System.Collections.Generic;

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
