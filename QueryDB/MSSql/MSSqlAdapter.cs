using Microsoft.Data.SqlClient;
using QueryDB.Resources;
using System.Collections.Generic;
using System.Data;

namespace QueryDB.MSSql
{
    /// <summary>
    /// 'Sql Server' adapter.
    /// </summary>
    internal class MSSqlAdapter
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

        internal List<DataDictionary> GetData(string selectSql, SqlConnection connection, bool upperCaseKeys)
        {
            var dataList = new List<DataDictionary>();
            var _systemAdapter = new MSSqlAdapter();
            var reader = _systemAdapter.GetSqlReader(selectSql, connection);
            while (reader.Read())
            {
                var addedRow = new DataDictionary();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (upperCaseKeys)
                        addedRow.ReferenceData.Add(reader.GetName(i).ToUpper(), reader.GetValue(i).ToString());
                    else
                        addedRow.ReferenceData.Add(reader.GetName(i), reader.GetValue(i).ToString());
                }
                dataList.Add(addedRow);
            }
            return dataList;
        }
    }
}
