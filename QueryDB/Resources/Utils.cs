using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace QueryDB.Resources
{
    /// <summary>
    /// Provides a set of utility functions for various operations.
    /// </summary>
    internal static class Utils
    {
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
    }
}
