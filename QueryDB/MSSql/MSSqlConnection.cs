using Microsoft.Data.SqlClient;
using System;

namespace QueryDB.MSSql
{
    /// <summary>
    /// 'Sql Server' database connection.
    /// </summary>
    internal sealed class MSSqlConnection : IDisposable
    {
        /// <summary>
        /// Holds 'Sql Server' connection.
        /// </summary>
        internal SqlConnection SqlConnection { get; private set; }

        /// <summary>
        /// Creates 'Sql Server' database connection.
        /// </summary>
        /// <param name="connectionString">'Sql Server' connection string value.</param>
        internal MSSqlConnection(string connectionString)
        {
            SqlConnection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Disposes 'Sql Server' connection.
        /// </summary>
        public void Dispose()
        {
            if (SqlConnection != null)
                SqlConnection.Dispose();
        }
    }
}
