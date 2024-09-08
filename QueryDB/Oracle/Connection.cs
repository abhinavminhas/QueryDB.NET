using Oracle.ManagedDataAccess.Client;
using System;

namespace QueryDB.Oracle
{
    /// <summary>
    /// 'Oracle' database connection.
    /// </summary>
    internal sealed class Connection : IDisposable
    {
        /// <summary>
        /// Holds 'Oracle' connection.
        /// </summary>
        internal OracleConnection OracleConnection { get; private set; }

        /// <summary>
        /// Creates 'Oracle' database connection.
        /// </summary>
        /// <param name="connectionString">'Oracle' connection string value.</param>
        internal Connection(string connectionString)
        {
            OracleConnection = new OracleConnection(connectionString);
        }

        /// <summary>
        /// Disposes 'Oracle' connection.
        /// </summary>
        public void Dispose()
        {
            if (OracleConnection != null)
            {
                OracleConnection.Close();
                OracleConnection.Dispose();
            }
        }
    }
}
