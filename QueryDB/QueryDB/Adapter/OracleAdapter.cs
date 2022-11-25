using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace QueryDB.Adapter
{
    /// <summary>
    /// 'Oracle' adapter.
    /// </summary>
    internal class OracleAdapter
    {
        /// <summary>
        /// Gets the 'Oracle' data reader.
        /// </summary>
        /// <param name="selectSql">'Select' sql value.</param>
        /// <param name="connection">'Oracle' connection.</param>
        /// <returns>'Oracle' data reader for the select sql.</returns>
        internal OracleDataReader GetOracleReader(string selectSql, OracleConnection connection)
        {
            connection.Open();
            using (var sqlCommand = new OracleCommand(selectSql, connection) { CommandType = CommandType.Text })
            {
                return sqlCommand.ExecuteReader();
            }
        }
    }
}
