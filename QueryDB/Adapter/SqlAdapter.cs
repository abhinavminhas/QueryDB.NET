using Microsoft.Data.SqlClient;
using System.Data;

namespace QueryDB.Adapter
{
    /// <summary>
    /// 'Sql Server' adapter.
    /// </summary>
    internal class SqlAdapter
    {
        /// <summary>
        /// Gets the 'Sql Server' data reader.
        /// </summary>
        /// <param name="selectSql">'Select' sql value.</param>
        /// <param name="connection">'Sql Server' connection.</param>
        /// <returns>'Sql Server' data reader for the select sql.</returns>
        internal SqlDataReader GetSqlReader(string selectSql, SqlConnection connection)
        {
            connection.Open();
            using (var sqlCommand = new SqlCommand(selectSql, connection) { CommandType = CommandType.Text })
            {
                return sqlCommand.ExecuteReader();
            }
        }
    }
}
