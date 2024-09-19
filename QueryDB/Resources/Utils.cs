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
    }
}
