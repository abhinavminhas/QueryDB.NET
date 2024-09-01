namespace QueryDB.Core.Tests
{
    internal static class Queries
    {
        internal static class OracleQuery
        {
            public static string SelectSql = @"SELECT 'oracle' AS current_database FROM dual";
        }

        internal static class SqlServerQuery
        {
            public static string SelectSql = @"SELECT 'mssql' AS current_database";
        }

        internal static class MySqlQuery
        {
            public static string SelectSql = @"SELECT DATABASE() AS current_database";
        }
    }
}
