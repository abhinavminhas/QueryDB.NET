using MySql.Data.MySqlClient;
using System;

namespace QueryDB.MySQL
{
    /// <summary>
    /// 'MySQL' database connection.
    /// </summary>
    internal sealed class Connection : IDisposable
    {
        /// <summary>
        /// Holds 'MySQL' connection.
        /// </summary>
        internal MySqlConnection MySqlConnection { get; private set; }

        /// <summary>
        /// Creates 'MySQL' database connection.
        /// </summary>
        /// <param name="connectionString">'MySQL' connection string value.</param>
        internal Connection(string connectionString)
        {
            MySqlConnection = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Disposes 'MySQL' connection.
        /// </summary>
        public void Dispose()
        {
            if (MySqlConnection != null)
            {
                MySqlConnection.Dispose();
            }
        }
    }
}
