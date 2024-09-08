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
        internal static DB Database;

        /// <summary>
        /// Holds 'SQL Server' connection string value for the DBContext created.
        /// </summary>
        internal static string SqlConnectionString;

        /// <summary>
        /// Holds 'MySQL' connection string value for the DBContext created.
        /// </summary>
        internal static string MySqlConnectionString;

        /// <summary>
        /// Holds 'Oracle' connection string value for the DBContext created.
        /// </summary>
        internal static string OracleConnectionString;

        /// <summary>
        /// Defines database type and connection string to connect to.
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
        }

        /// <summary>
        /// Retrieves records for 'Select' queries from the database.
        /// Converts column names to keys holding values, with multiple database rows returned into a list.
        /// Note: Use aliases in query for similar column names.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="upperCaseKeys">Boolean parameter to return dictionary keys in uppercase / lowercase.. Default - 'false'.</param>
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
            return dataList;
        }

        /// <summary>
        /// Gets 'SQL Server' connection.
        /// </summary>
        /// <returns>'SQL Server' Connection.</returns>
        internal MSSQL.Connection GetSqlServerConnection()
        {
            var _connectionBuilder = new ConnectionBuilder();
            return _connectionBuilder.GetSqlConnection;
        }

        /// <summary>
        /// Gets 'MySQL' connection.
        /// </summary>
        /// <returns>'MySQL' Connection.</returns>
        internal MySQL.Connection GetMySqlConnection()
        {
            var _connectionBuilder = new ConnectionBuilder();
            return _connectionBuilder.GetMySqlConnection;
        }

        /// <summary>
        /// Gets 'Oracle' connection.
        /// </summary>
        /// <returns>'Oracle' connection.</returns>
        internal Oracle.Connection GetOracleConnection()
        {
            var _connectionBuilder = new ConnectionBuilder();
            return _connectionBuilder.GetOracleConnection;
        }
    }

    /// <summary>
    /// Database Type.
    /// </summary>
    public enum DB
    {
        MSSQL = 1,
        MySQL = 2,
        Oracle = 3
    }
}
