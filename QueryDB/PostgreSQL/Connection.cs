using Npgsql;
using System;

namespace QueryDB.PostgreSQL
{
    /// <summary>
    /// 'PostgreSQL' database connection.
    /// </summary>
    internal sealed class Connection : IDisposable
    {
        /// <summary>
        /// Holds 'PostgreSQL' connection.
        /// </summary>
        internal NpgsqlConnection PostgreSQLConnection { get; private set; }

        /// <summary>
        /// Creates 'PostgreSQL' database connection.
        /// </summary>
        /// <param name="connectionString">'PostgreSQL' connection string value.</param>
        internal Connection(string connectionString)
        {
            PostgreSQLConnection = new NpgsqlConnection(connectionString);
        }

        /// <summary>
        /// Disposes 'PostgreSQL' connection.
        /// </summary>
        public void Dispose()
        {
            if (PostgreSQLConnection != null)
            {
                PostgreSQLConnection.Dispose();
            }
        }
    }
}
