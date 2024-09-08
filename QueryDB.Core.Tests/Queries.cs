namespace QueryDB.Core.Tests
{
    internal static class Queries
    {
        internal static class OracleQueries
        {
            internal static class Smoke
            {
                public static string SelectSql = @"SELECT 'oracle' AS current_database FROM dual";
            }
            internal static class SalesDB
            {
                public static string SelectSql = @"SELECT * FROM AGENTS";
            }
        }

        internal static class SQLServerQueries
        {
            internal static class Smoke
            {
                public static string SelectSql = @"SELECT 'mssql' AS current_database";
            }
            internal static class SalesDB
            {
                public static string SelectSql = @"SELECT * FROM AGENTS";
            }
        }

        internal static class MySQLQueries
        {
            internal static class Smoke
            {
                public static string SelectSql = @"SELECT DATABASE() AS current_database";
            }
            internal static class SalesDB
            {
                public static string SelectSql = @"SELECT * FROM AGENTS";
            }
        }
    }
}
