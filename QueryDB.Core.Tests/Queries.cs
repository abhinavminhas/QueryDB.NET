namespace QueryDB.Core.Tests
{
    internal static class Queries
    {
        internal static class OracleQuery
        {
            public static string SelectSql = @"select SYS_CONTEXT('USERENV', 'DB_NAME') AS current_database FROM dual";
        }

        internal static class SqlServerQuery
        {
            public static string SelectSql = @"select DB_NAME() AS current_database";
        }

        internal static class MySqlQuery
        {
            public static string SelectSql = @"select DATABASE() AS current_database";
        }
    }
}
