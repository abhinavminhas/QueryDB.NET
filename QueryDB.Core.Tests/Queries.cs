namespace QueryDB.Core.Tests
{
    internal static class Queries
    {
        internal static class MSSQLQueries
        {
            internal static class Smoke
            {
                public static string SelectSql = @"SELECT 'mssql' AS current_database";
            }
            internal static class SalesDB
            {
                public static string SelectSql = @"SELECT * FROM Agents";
                public static string SelectSql_Join = @"SELECT A.Agent_Code, A.Agent_Name, C.Cust_Code, C.Cust_Name, O.Ord_Num, O.Ord_Amount, O.Advance_Amount, O.Ord_Date, O.Ord_Description FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
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
                public static string SelectSql = @"SELECT * FROM Agents";
                public static string SelectSql_Join = @"SELECT A.Agent_Code, A.Agent_Name, C.Cust_Code, C.Cust_Name, O.Ord_Num, O.Ord_Amount, O.Advance_Amount, O.Ord_Date, O.Ord_Description FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
            }
        }

        internal static class OracleQueries
        {
            internal static class Smoke
            {
                public static string SelectSql = @"SELECT 'oracle' AS current_database FROM dual";
            }
            internal static class SalesDB
            {
                public static string SelectSql = @"SELECT * FROM Agents";
                public static string SelectSql_Join = @"SELECT A.Agent_Code, A.Agent_Name, C.Cust_Code, C.Cust_Name, O.Ord_Num, O.Ord_Amount, O.Advance_Amount, O.Ord_Date, O.Ord_Description FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
            }
        }
    }
}
