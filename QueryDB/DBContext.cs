using QueryDB.Adapter;
using QueryDB.Connection;
using QueryDB.Connection.Database;
using QueryDB.Resources;
using System.Collections.Generic;

namespace QueryDB
{
    /// <summary>
    /// DBContext to connect to a database type and run commands.
    /// </summary>
    public class DBContext
    {
        /// <summary>
        /// Database type value to connect to.
        /// </summary>
        internal static DB Database;

        /// <summary>
        /// Holds 'Oracle' connection string value for the DBContext created.
        /// </summary>
        internal static string OracleConnectionString;

        /// <summary>
        /// Holds 'Sql Server' connection string value for the DBContext created.
        /// </summary>
        internal static string SqlServerConnectionString;

        /// <summary>
        /// Holds 'MySQL' connection string value for the DBContext created.
        /// </summary>
        internal static string MySqlConnectionString;

        /// <summary>
        /// Defines database type and connection string to connect to.
        /// </summary>
        /// <param name="database">'DB' enum value for database type.</param>
        /// <param name="connectionString">Connection string for the database selected.</param>
        public DBContext(DB database, string connectionString)
        {
            Database = database;
            if (Database.Equals(DB.Oracle))
                OracleConnectionString = connectionString;
            else if (Database.Equals(DB.SqlServer))
                SqlServerConnectionString = connectionString;
            else if (Database.Equals(DB.MySql))
                MySqlConnectionString = connectionString;
        }

        /// <summary>
        /// Retrives records for the 'Select' query from the database.
        /// Converts column names to keys holding values, with multiple database rows returned into a list.
        /// Note: Use aliases in query for similar column names.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="upperCaseKeys">Optional parameter to return dictionary keys in upper case. Default - 'false'.</param>
        /// <returns>List of data Dictionary with column names as keys holding values into a list for multiple rows of data.</returns>
        public List<DataDictionary> FetchData(string selectSql, bool upperCaseKeys = false)
        {
            var dataList = new List<DataDictionary>();
            if (Database.Equals(DB.Oracle))
            {
                using (var oracleDBConnection = GetOracleConnection())
                {
                    var _systemAdapter = new OracleAdapter();
                    var reader = _systemAdapter.GetOracleReader(selectSql, oracleDBConnection.OracleConnection);
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
                }
            }
            else if (Database.Equals(DB.SqlServer))
            {
                using (var sqlDBConnection = GetSqlServerConnection())
                {
                    var _systemAdapter = new SqlAdapter();
                    var reader = _systemAdapter.GetSqlReader(selectSql, sqlDBConnection.SqlConnection);
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
                }
            }
            else if (Database.Equals(DB.MySql))
            {
                using (var mySqlDBConnection = GetMySqlConnection())
                {
                    var _systemAdapter = new MySqlAdapter();
                    var reader = _systemAdapter.GetSqlReader(selectSql, mySqlDBConnection.MySqlConnection);
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
                }
            }
            return dataList;
        }

        /// <summary>
        /// Gets 'Oracle' connection.
        /// </summary>
        /// <returns>'Oracle' connection.</returns>
        private OracleDBConnection GetOracleConnection()
        {
            var _connectionBuilder = new ConnectionBuilder();
            return _connectionBuilder.GetOracleConnection;
        }

        /// <summary>
        /// Gets 'Sql Server' connection.
        /// </summary>
        /// <returns>'Sql Server' Connection.</returns>
        private SqlDBConnection GetSqlServerConnection()
        {
            var _connectionBuilder = new ConnectionBuilder();
            return _connectionBuilder.GetSqlServerConnection;
        }

        /// <summary>
        /// Gets 'MySQL' connection.
        /// </summary>
        /// <returns>'MySQL' Connection.</returns>
        private MySqlDBConnection GetMySqlConnection()
        {
            var _connectionBuilder = new ConnectionBuilder();
            return _connectionBuilder.GetMySqlConnection;
        }
    }

    /// <summary>
    /// Database Type.
    /// </summary>
    public enum DB
    {
        Oracle,
        SqlServer,
        MySql
    }
}
