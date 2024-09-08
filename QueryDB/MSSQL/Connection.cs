using Microsoft.Data.SqlClient;
using System;

namespace QueryDB.MSSQL
{
    /// <summary>
    /// 'SQL Server' database connection.
    /// </summary>
    internal sealed class Connection : IDisposable
    {
        /// <summary>
        /// Holds 'SQL Server' connection.
        /// </summary>
        internal SqlConnection SqlConnection { get; private set; }

        /// <summary>
        /// Creates 'SQL Server' database connection.
        /// </summary>
        /// <param name="connectionString">'SQL Server' connection string value.</param>
        internal Connection(string connectionString)
        {
            SqlConnection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Disposes 'SQL Server' connection.
        /// </summary>
        public void Dispose()
        {
            if (SqlConnection != null)
                SqlConnection.Close();
                SqlConnection.Dispose();
        }
    }
}
