using MySql.Data.MySqlClient;
using System.Data;

namespace QueryDB.MySql
{
    /// <summary>
    /// 'MySQL' adapter.
    /// </summary>
    internal class Adapter
    {
        /// <summary>
        /// Gets the 'MySQL' data reader.
        /// </summary>
        /// <param name="selectSql">'Select' sql value.</param>
        /// <param name="connection">'MySQL' connection.</param>
        /// <returns>'MySQL' data reader for the select sql.</returns>
        internal MySqlDataReader GetSqlReader(string selectSql, MySqlConnection connection)
        {
            connection.Open();
            using (var sqlCommand = new MySqlCommand(selectSql, connection) { CommandType = CommandType.Text })
            {
                return sqlCommand.ExecuteReader();
            }
        }
    }
}
