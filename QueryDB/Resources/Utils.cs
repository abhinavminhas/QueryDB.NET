using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace QueryDB.Resources
{
    internal static class Utils
    {
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

        internal static bool IsBFileColumn(OracleDataReader reader, int columnIndex)
        {
            const string BFILE_COLUMN = "BFILE";
            string columnType = reader.GetDataTypeName(columnIndex);
            return columnType.Equals(BFILE_COLUMN, StringComparison.OrdinalIgnoreCase);
        }

        internal static string GetBFileContent(OracleDataReader reader, int columnIndex)
        {
            string content = string.Empty;
            var bFile = reader.GetOracleBFile(columnIndex);
            if (bFile != null)
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
                Console.WriteLine(content);
                bFile.Close();
            }
            return content;
        }
    }
}
