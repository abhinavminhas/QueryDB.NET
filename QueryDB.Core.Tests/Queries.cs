namespace QueryDB.Core.Tests
{
    internal static class Queries
    {
        internal static class OracleQuery
        {
            public static string SelectSql = @"SELECT name AS current_database FROM v$database";
        }

        internal static class SqlServerQuery
        {
            public static string SelectSql = @"SELECT DB_NAME() AS current_database";
        }

        internal static class MySqlQuery
        {
            public static string SelectSql = @"SELECT DATABASE() AS current_database";
        }
    }
}
