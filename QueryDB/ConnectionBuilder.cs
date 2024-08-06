
using QueryDB.MSSql;

namespace QueryDB
{
    /// <summary>
    /// Connection builder.
    /// </summary>
    internal class ConnectionBuilder
    {
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

        /// <summary>
        /// Gets 'Sql Server' database connection.
        /// </summary>
        internal MSSql.MSSqlConnection GetSqlServerConnection
        {
            get
            {
                var connection = new MSSqlConnection(DBContext.SqlServerConnectionString);
                return connection;
            }
        }

        /// <summary>
        /// Gets 'MySQL' database connection.
        /// </summary>
        internal MySql.Connection GetMySqlConnection
        {
            get
            {
                var connection = new MySql.Connection(DBContext.MySqlConnectionString);
                return connection;
            }
        }
    }
}
