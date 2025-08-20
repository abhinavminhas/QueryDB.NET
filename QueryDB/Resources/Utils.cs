using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QueryDB.Resources
{
    /// <summary>
    /// Provides a set of utility functions for various operations.
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// Regex pattern to validate that a SQL query starts with 'SELECT'.
        /// </summary>
        internal static readonly string SelectQueryPattern = @"^\s*SELECT\s+.*";

        /// <summary>
        /// Checks if a specified column exists in the given data reader.
        /// </summary>
        /// <param name="reader">The data reader to check.</param>
        /// <param name="columnName">The name of the column to find.</param>
        /// <returns>Returns <c>true</c> if the column exists; otherwise, <c>false</c>.</returns>
        internal static bool ColumnExists(IDataReader reader, string columnName)
        {
            try
            {
                reader.GetOrdinal(columnName);
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines if a specified column in an Oracle data reader is of type "BFILE".
        /// </summary>
        /// <param name="reader">The Oracle data reader containing the column.</param>
        /// <param name="columnIndex">The index of the column to check.</param>
        /// <returns>Returns <c>true</c> if the column is of type "BFILE"; otherwise, <c>false</c>.</returns>
        internal static bool IsBFileColumn(OracleDataReader reader, int columnIndex)
        {
            const string BFILE_COLUMN = "BFILE";
            string columnType = reader.GetDataTypeName(columnIndex);
            return columnType.Equals(BFILE_COLUMN, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines if a specified column in an Oracle data reader, identified by its column name, is of type "BFILE".
        /// </summary>
        /// <param name="reader">The Oracle data reader containing the column.</param>
        /// <param name="columnName">The name of the column to check.</param>
        /// <returns>Returns <c>true</c> if the column is of type "BFILE"; otherwise, <c>false</c>.</returns>
        internal static bool IsBFileColumn(OracleDataReader reader, string columnName)
        {
            const string BFILE_COLUMN = "BFILE";
            int columnIndex = reader.GetOrdinal(columnName);
            string columnType = reader.GetDataTypeName(columnIndex);
            return columnType.Equals(BFILE_COLUMN, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Retrieves the content of a BFILE column from an Oracle data reader as a Base64-encoded string.
        /// </summary>
        /// <param name="reader">The Oracle data reader containing the BFILE column.</param>
        /// <param name="columnIndex">The index of the BFILE column to read.</param>
        /// <returns>Returns the BFILE content as a Base64-encoded string, or an empty string if the BFILE is null.</returns>
        internal static string GetBFileBase64Content(OracleDataReader reader, int columnIndex)
        {
            string content = string.Empty;
            var bFile = reader.GetOracleBFile(columnIndex);
            if (bFile != null && !reader.IsDBNull(columnIndex))
            {
                bFile.OpenFile();
                byte[] buffer = new byte[bFile.Length];
                int bytesReadTotal = 0;
                while (bytesReadTotal < buffer.Length)
                {
                    int bytesRead = bFile.Read(buffer, bytesReadTotal, buffer.Length - bytesReadTotal);
                    if (bytesRead == 0)
                        break;
                    bytesReadTotal += bytesRead;
                }
                content = Convert.ToBase64String(buffer);
                bFile.Close();
            }
            return content;
        }

        /// <summary>
        /// Asynchronously retrieves the content of a BFILE column from an Oracle data reader as a Base64-encoded string.
        /// </summary>
        /// <param name="reader">The Oracle data reader containing the BFILE column.</param>
        /// <param name="columnIndex">The index of the BFILE column to read.</param>
        /// <returns>Returns the BFILE content as a Base64-encoded string, or an empty string if the BFILE is null.</returns>
        internal static async Task<string> GetBFileBase64ContentAsync(OracleDataReader reader, int columnIndex)
        {
            string content = string.Empty;
            var bFile = reader.GetOracleBFile(columnIndex);
            if (bFile != null && !await reader.IsDBNullAsync(columnIndex))
            {
                bFile.OpenFile();
                byte[] buffer = new byte[bFile.Length];
                int bytesReadTotal = 0;
                while (bytesReadTotal < buffer.Length)
                {
                    int bytesRead = await bFile.ReadAsync(buffer, bytesReadTotal, buffer.Length - bytesReadTotal);
                    if (bytesRead == 0)
                        break;
                    bytesReadTotal += bytesRead;
                }
                content = Convert.ToBase64String(buffer);
                bFile.Close();
            }
            return content;
        }

        /// <summary>
        /// Retrieves the content of a BFILE column from an Oracle data reader as a byte array, using the column name.
        /// </summary>
        /// <param name="reader">The Oracle data reader containing the BFILE column.</param>
        /// <param name="columnName">The name of the BFILE column to read.</param>
        /// <returns>Returns the BFILE content as a byte array, or <c>null</c> if the BFILE is null or the column value is null.</returns>
        internal static byte[] GetBFileByteContent(OracleDataReader reader, string columnName)
        {
            byte[] buffer = null;
            int columnIndex = reader.GetOrdinal(columnName);
            var bFile = reader.GetOracleBFile(columnIndex);
            if (bFile != null && !reader.IsDBNull(columnIndex))
            {
                bFile.OpenFile();
                buffer = new byte[bFile.Length];
                int bytesReadTotal = 0;
                while (bytesReadTotal < buffer.Length)
                {
                    int bytesRead = bFile.Read(buffer, bytesReadTotal, buffer.Length - bytesReadTotal);
                    if (bytesRead == 0)
                        break;
                    bytesReadTotal += bytesRead;
                }
                bFile.Close();
            }
            return buffer;
        }

        /// <summary>
        /// Asynchronously retrieves the content of a BFILE column from an Oracle data reader as a byte array, using the column name.
        /// </summary>
        /// <param name="reader">The Oracle data reader containing the BFILE column.</param>
        /// <param name="columnName">The name of the BFILE column to read.</param>
        /// <returns>Returns the BFILE content as a byte array, or <c>null</c> if the BFILE is null or the column value is null.</returns>
        internal static async Task<byte[]> GetBFileByteContentAsync(OracleDataReader reader, string columnName)
        {
            byte[] buffer = null;
            int columnIndex = reader.GetOrdinal(columnName);
            var bFile = reader.GetOracleBFile(columnIndex);
            if (bFile != null && !await reader.IsDBNullAsync(columnIndex))
            {
                bFile.OpenFile();
                buffer = new byte[bFile.Length];
                int bytesReadTotal = 0;
                while (bytesReadTotal < buffer.Length)
                {
                    int bytesRead = await bFile.ReadAsync(buffer, bytesReadTotal, buffer.Length - bytesReadTotal);
                    if (bytesRead == 0)
                        break;
                    bytesReadTotal += bytesRead;
                }
                bFile.Close();
            }
            return buffer;
        }

        #region Sql Parameters

        /// <summary>
        /// Converts an object or dictionary into a sequence of SQL parameters.
        /// </summary>
        /// <param name="parameters">
        /// An object with properties or a dictionary of key-value pairs to convert into <see cref="SqlParameter"/>s.
        /// The input to convert:
        /// - Anonymous or POCO object.
        /// - Dictionary of key-value pairs of string keys and object values.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="SqlParameter"/>s with names prefixed by '@' and null values replaced with <see cref="DBNull.Value"/>.
        /// </returns>
        internal static IEnumerable<SqlParameter> ToSqlParameters(object parameters)
        {
            if (parameters is IDictionary<string, object> dict)
            {
                foreach (var kv in dict)
                    yield return new SqlParameter("@" + kv.Key, kv.Value ?? DBNull.Value);
            }
            else
            {
                foreach (PropertyInfo prop in parameters.GetType().GetProperties())
                {
                    var value = prop.GetValue(parameters, null) ?? DBNull.Value;
                    yield return new SqlParameter("@" + prop.Name, value);
                }
            }
        }

        /// <summary>
        /// Converts an object or dictionary into a sequence of MySQL parameters.
        /// </summary>
        /// <param name="parameters">
        /// An object with properties or a dictionary of key-value pairs to convert into <see cref="MySqlParameter"/>s.
        /// The input to convert:
        /// - Anonymous or POCO object.
        /// - Dictionary of key-value pairs of string keys and object values.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="MySqlParameter"/>s with names prefixed by '@' and null values replaced with <see cref="DBNull.Value"/>.
        /// </returns>
        internal static IEnumerable<MySqlParameter> ToMySqlParameters(object parameters)
        {
            if (parameters is IDictionary<string, object> dict)
            {
                foreach (var kv in dict)
                    yield return new MySqlParameter("@" + kv.Key, kv.Value ?? DBNull.Value);
            }
            else
            {
                foreach (PropertyInfo prop in parameters.GetType().GetProperties())
                {
                    var value = prop.GetValue(parameters, null) ?? DBNull.Value;
                    yield return new MySqlParameter("@" + prop.Name, value);
                }
            }
        }

        /// <summary>
        /// Converts an object or dictionary into a sequence of Oracle parameters.
        /// </summary>
        /// <param name="sql">The SQL command text.</param>
        /// <param name="parameters">
        /// An object with properties or a dictionary of key-value pairs to convert into <see cref="OracleParameter"/>s.
        /// The input to convert:
        /// - Anonymous or POCO object.
        /// - Dictionary of key-value pairs of string keys and object values.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="OracleParameter"/>s with names prefixed by ':' and null values replaced with <see cref="DBNull.Value"/>.
        /// </returns>
        internal static IEnumerable<OracleParameter> ToOracleParameters(string sql, object parameters)
        {
            var matches = Regex.Matches(sql, @"(?<!:):(\w+)", RegexOptions.None, TimeSpan.FromSeconds(5));
            var bindNames = matches.Cast<Match>().Select(m => m.Groups[1].Value).ToList();
            IDictionary<string, object> paramDict;
            if (parameters is IDictionary<string, object> dict)
                paramDict = dict;
            else
            {
                paramDict = parameters.GetType()
                    .GetProperties()
                    .ToDictionary(p => p.Name, p => p.GetValue(parameters, null) ?? DBNull.Value, StringComparer.OrdinalIgnoreCase);
            }
            foreach (var name in bindNames)
            {
                if (paramDict.TryGetValue(name, out var value))
                    yield return new OracleParameter(":" + name, value);
            }
        }

        /// <summary>
        /// Converts an object or dictionary into a sequence of PostgreSQL parameters.
        /// </summary>
        /// <param name="parameters">
        /// An object with properties or a dictionary of key-value pairs to convert into <see cref="NpgsqlParameter"/>s.
        /// The input to convert:
        /// - Anonymous or POCO object.
        /// - Dictionary of key-value pairs of string keys and object values.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="NpgsqlParameter"/>s with names prefixed by '@' and null values replaced with <see cref="DBNull.Value"/>.
        /// </returns>
        internal static IEnumerable<NpgsqlParameter> ToNpgsqlParameters(object parameters)
        {
            if (parameters is IDictionary<string, object> dict)
            {
                foreach (var kv in dict)
                    yield return new NpgsqlParameter("@" + kv.Key, kv.Value ?? DBNull.Value);
            }
            else
            {
                foreach (PropertyInfo prop in parameters.GetType().GetProperties())
                {
                    var value = prop.GetValue(parameters, null) ?? DBNull.Value;
                    yield return new NpgsqlParameter("@" + prop.Name, value);
                }
            }
        }

        #endregion

    }
}
