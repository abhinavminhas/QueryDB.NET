using Oracle.ManagedDataAccess.Client;
using QueryDB.Resources;
using System;
using System.Collections.Generic;
using System.Data;

namespace QueryDB.Oracle
{
    /// <summary>
    /// 'Oracle' adapter.
    /// </summary>
    internal class Adapter
    {
        /// <summary>
        /// Gets the 'Oracle' data reader.
        /// </summary>
        /// <param name="cmdText">The text of the query.</param>
        /// <param name="connection">'Oracle' connection.</param>
        /// <param name="commandType">Sql command type.</param>
        /// <returns>'Oracle' data reader.</returns>
        internal OracleDataReader GetOracleReader(string cmdText, OracleConnection connection, CommandType commandType)
        {
            connection.Open();
            using (var sqlCommand = new OracleCommand(cmdText, connection) { CommandType = commandType })
            {
                return sqlCommand.ExecuteReader();
            }
        }

        internal OracleCommand GetOracleCommand(string cmdText, OracleConnection connection, CommandType commandType)
        {
            connection.Open();
            var sqlCommand = new OracleCommand(cmdText, connection) { CommandType = commandType };
            return sqlCommand;
        }

        /// <summary>
        /// Retrieves records for 'Select' queries from the database.
        /// Converts column names to keys holding values, with multiple database rows returned into a list.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="connection">'Oracle' Connection.</param>
        /// <param name="upperCaseKeys">Boolean parameter to return dictionary keys in uppercase.</param>
        /// <returns>List of data Dictionary with column names as keys holding values into a list for multiple rows of data.
        /// Note: Byte[]/BFile is returned as Base64 string.</returns>
        internal List<DataDictionary> FetchData(string selectSql, OracleConnection connection, bool upperCaseKeys)
        {
            var dataList = new List<DataDictionary>();
            using (var reader = GetOracleReader(selectSql, connection, CommandType.Text))
            {
                while (reader.Read())
                {
                    var addRow = new DataDictionary();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string key = upperCaseKeys ? reader.GetName(i).ToUpper() : reader.GetName(i);
                        if (Utils.IsBFileColumn(reader, i))
                            addRow.ReferenceData.Add(key, Utils.GetBFileBase64Content(reader, i));
                        else if (reader.GetValue(i) is byte[] value)
                            addRow.ReferenceData.Add(key, Convert.ToBase64String(value));
                        else
                            addRow.ReferenceData.Add(key, reader.GetValue(i).ToString());
                    }
                    dataList.Add(addRow);
                }
            }
            return dataList;
        }

        /// <summary>
        ///  Retrieves records for 'Select' queries from the database.
        /// </summary>
        /// <typeparam name="T">Object entity to return data mapped into.</typeparam>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="connection">'Oracle' Connection.</param>
        /// <param name="strict">Enables fetch data only for object <T> properties existing in database query result.</param>
        /// <returns>List of data rows mapped into object entity into a list for multiple rows of data.</returns>
        internal List<T> FetchData<T>(string selectSql, OracleConnection connection, bool strict) where T : new()
        {
            var dataList = new List<T>();
            using (var reader = GetOracleReader(selectSql, connection, CommandType.Text))
            {
                while (reader.Read())
                {
                    var addObjectRow = new T();
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        if ((strict || Utils.ColumnExists(reader, prop.Name)) && !reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                        {
                            if (Utils.IsBFileColumn(reader, prop.Name))
                                prop.SetValue(addObjectRow, Utils.GetBFileByteContent(reader, prop.Name));
                            else
                                prop.SetValue(addObjectRow, reader[prop.Name]);
                        }   
                    }
                    dataList.Add(addObjectRow);
                }
            }
            return dataList;
        }

        internal void ExecuteDDL(string ddlStatement, OracleConnection connection)
        {
            using (var sqlCommand = GetOracleCommand(ddlStatement, connection, CommandType.Text))
            {
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}