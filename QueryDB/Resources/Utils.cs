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
            var bfile = reader.GetOracleBFile(columnIndex);
            Console.WriteLine("START BFILE READ");
            Console.WriteLine("BFILE: " + bfile);
            Console.WriteLine("BFILE  EXISTS: " + bfile.FileExists);
            if (bfile != null && bfile.FileExists)
            {
                bfile.OpenFile();
                byte[] buffer = new byte[bfile.Length];
                bfile.Read(buffer, 0, buffer.Length);
                content = Convert.ToBase64String(buffer);
                Console.WriteLine(content);
                bfile.Close();
            }
            Console.WriteLine("END BFILE READ");
            return content;
        }
    }
}
