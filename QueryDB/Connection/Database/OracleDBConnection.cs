using Oracle.ManagedDataAccess.Client;
using System;

namespace QueryDB.Connection.Database
{
    /// <summary>
    /// 'Oracle' database connection.
    /// </summary>
    internal sealed class OracleDBConnection : IDisposable
    {
        /// <summary>
        /// Holds 'Oracle' connection.
        /// </summary>
        internal OracleConnection OracleConnection { get; private set; }

        /// <summary>
        /// Creates 'Oracle' database connection.
        /// </summary>
        /// <param name="connectionString">'Oracle' connection string value.</param>
        internal OracleDBConnection(string connectionString)
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
                OracleConnection.Clone();
                OracleConnection.Dispose();
            }
        }
    }
}
