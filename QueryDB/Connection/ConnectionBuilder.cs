using QueryDB.Connection.Database;

namespace QueryDB.Connection
{
    /// <summary>
    /// Connection builder.
    /// </summary>
    internal class ConnectionBuilder
    {
        /// <summary>
        /// Gets 'Oracle' database connection.
        /// </summary>
        internal OracleDBConnection GetOracleConnection
        {
            get
            {
                var connection = new OracleDBConnection(DBContext.OracleConnectionString);
                return connection;
            }
        }

        /// <summary>
        /// Gets 'Sql Server' database connection.
        /// </summary>
        internal SqlDBConnection GetSqlServerConnection
        {
            get
            {
                var connection = new SqlDBConnection(DBContext.SqlServerConnectionString);
                return connection;
            }
        }

        /// <summary>
        /// Gets 'MySQL' database connection.
        /// </summary>
        internal MySqlDBConnection GetMySqlConnection
        {
            get
            {
                var connection = new MySqlDBConnection(DBContext.MySqlConnectionString);
                return connection;
            }
        }
    }
}
