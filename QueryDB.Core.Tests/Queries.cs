namespace QueryDB.Core.Tests
{
    internal static class Queries
    {
        internal static class OracleQuery
        {
            public static string SelectSql = @"SELECT SYSDATE FROM DUAL";
        }

        internal static class SqlServerQuery
        {
            public static string SelectSql = @"SELECT NAME FROM SYS.DATABASES";
        }

        internal static class MySqlQuery
        {
            public static string SelectSql = @"SELECT SYSDATE()";
        }
    }
}
