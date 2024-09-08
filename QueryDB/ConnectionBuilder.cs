namespace QueryDB
{
    /// <summary>
    /// Connection builder.
    /// </summary>
    internal class ConnectionBuilder
    {
        /// <summary>
        /// Gets 'SQL Server' database connection.
        /// </summary>
        internal MSSQL.Connection GetSqlConnection
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
        internal MySQL.Connection GetMySqlConnection
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
        internal Oracle.Connection GetOracleConnection
        {
            get
            {
                var connection = new Oracle.Connection(DBContext.OracleConnectionString);
                return connection;
            }
        }
    }
}
