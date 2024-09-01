using Microsoft.Data.SqlClient;
using System;

namespace QueryDB.Connection.Database
{
    /// <summary>
    /// 'Sql Server' database connection.
    /// </summary>
    internal sealed class SqlDBConnection : IDisposable
    {
        /// <summary>
        /// Holds 'Sql Server' connection.
        /// </summary>
        internal SqlConnection SqlConnection { get; private set; }

        /// <summary>
        /// Creates 'Sql Server' database connection.
        /// </summary>
        /// <param name="connectionString">'Sql Server' connection string value.</param>
        internal SqlDBConnection(string connectionString)
        {
            SqlConnection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Disposes 'Sql Server' connection.
        /// </summary>
        public void Dispose()
        {
            if (SqlConnection != null)
            {
                SqlConnection.Close();
                SqlConnection.Dispose();
            }
        }
    }
}
