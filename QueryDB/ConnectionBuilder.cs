namespace QueryDB
{
    /// <summary>
    /// Connection builder.
    /// </summary>
    internal static class ConnectionBuilder
    {
        /// <summary>
        /// Gets 'SQL Server' database connection.
        /// </summary>
        internal static MSSQL.Connection GetSqlConnection
        {
            get
            {
                var connection = new MSSQL.Connection(DBContext.SqlConnectionString);
                return connection;
            }
        }

        /// <summary>
        /// Gets 'MySQL' database connection.
        /// </summary>
        internal static MySQL.Connection GetMySqlConnection
        {
            get
            {
                var connection = new MySQL.Connection(DBContext.MySqlConnectionString);
                return connection;
            }
        }

        /// <summary>
        /// Gets 'Oracle' database connection.
        /// </summary>
        internal static Oracle.Connection GetOracleConnection
        {
            get
            {
                var connection = new Oracle.Connection(DBContext.OracleConnectionString);
                return connection;
            }
        }
    }
}
