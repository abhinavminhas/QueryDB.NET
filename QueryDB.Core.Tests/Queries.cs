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
            internal static class TestDB
            {
                public static string SelectSql = @"SELECT * FROM Agents";
                public static string SelectSql_Join = @"SELECT A.Agent_Code, A.Agent_Name, C.Cust_Code, C.Cust_Name, O.Ord_Num, O.Ord_Amount, O.Advance_Amount, O.Ord_Date, O.Ord_Description FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                public static string SelectSql_Alias = @"SELECT A.Agent_Name AS Agent, A.WORKING_AREA AS Agent_Location, C.Cust_Name AS Customer, C.WORKING_AREA AS Customer_Location, O.Agent_Code, O.Cust_Code FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                public static string SelectSql_DataTypes = @"SELECT * FROM DataTypes";
            }
        }

        internal static class MySQLQueries
        {
            internal static class Smoke
            {
                public static string SelectSql = @"SELECT DATABASE() AS current_database";
            }
            internal static class TestDB
            {
                public static string SelectSql = @"SELECT * FROM Agents";
                public static string SelectSql_Join = @"SELECT A.Agent_Code, A.Agent_Name, C.Cust_Code, C.Cust_Name, O.Ord_Num, O.Ord_Amount, O.Advance_Amount, O.Ord_Date, O.Ord_Description FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                public static string SelectSql_Alias = @"SELECT A.Agent_Name AS Agent, A.WORKING_AREA AS Agent_Location, C.Cust_Name AS Customer, C.WORKING_AREA AS Customer_Location, O.Agent_Code, O.Cust_Code FROM Agents A INNER JOIN 
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
            internal static class TestDB
            {
                public static string SelectSql = @"SELECT * FROM Agents";
                public static string SelectSql_Join = @"SELECT A.Agent_Code, A.Agent_Name, C.Cust_Code, C.Cust_Name, O.Ord_Num, O.Ord_Amount, O.Advance_Amount, O.Ord_Date, O.Ord_Description FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                public static string SelectSql_Alias = @"SELECT A.Agent_Name AS Agent, A.WORKING_AREA AS Agent_Location, C.Cust_Name AS Customer, C.WORKING_AREA AS Customer_Location, O.Agent_Code, O.Cust_Code FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
            }
        }

        internal static class PostgreSQLQueries
        {
            internal static class Smoke
            {
                public static string SelectSql = @"SELECT 'postgres' AS current_database";
            }
            internal static class TestDB
            {
                public static string SelectSql = @"SELECT * FROM Agents";
                public static string SelectSql_Join = @"SELECT A.Agent_Code, A.Agent_Name, C.Cust_Code, C.Cust_Name, O.Ord_Num, O.Ord_Amount, O.Advance_Amount, O.Ord_Date, O.Ord_Description FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                public static string SelectSql_Alias = @"SELECT A.Agent_Name AS Agent, A.WORKING_AREA AS Agent_Location, C.Cust_Name AS Customer, C.WORKING_AREA AS Customer_Location, O.Agent_Code, O.Cust_Code FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
            }
        }
    }
}
