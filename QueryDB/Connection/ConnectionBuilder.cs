using QueryDB.Connection.Database;

namespace QueryDB.Connection
{
    /// <summary>
    /// Connection builder.
    /// </summary>
    internal class ConnectionBuilder
    {
        /// <summary>
        /// Creates 'Oracle' database connection.
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
        /// Creates 'Sql Server' database connection.
        /// </summary>
        internal SqlDBConnection GetSqlServerConnection
        {
            get
            {
                var connection = new SqlDBConnection(DBContext.SqlServerConnectionString);
                return connection;
            }
        }
    }
}
