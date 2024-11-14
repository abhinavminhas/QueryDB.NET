namespace QueryDB.Core.Tests
{
    internal static class Queries
    {
        internal static class MSSQLQueries
        {
            internal static class Smoke
            {
                internal static string SelectSql = @"SELECT 'mssql' AS current_database";
            }
            internal static class TestDB
            {
                internal static string SelectSql = @"SELECT * FROM Agents";
                internal static string SelectSql_Join = @"SELECT A.Agent_Code, A.Agent_Name, C.Cust_Code, C.Cust_Name, O.Ord_Num, O.Ord_Amount, O.Advance_Amount, O.Ord_Date, O.Ord_Description FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                internal static string SelectSql_Alias = @"SELECT A.Agent_Name AS Agent, A.WORKING_AREA AS Agent_Location, C.Cust_Name AS Customer, C.WORKING_AREA AS Customer_Location, O.Agent_Code, O.Cust_Code FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                internal static string SelectSql_DataTypes = @"SELECT * FROM DataTypes";
                internal static string SelectSql_Strict = @"SELECT A.Agent_Code, A.Agent_Name AS Agent, C.Cust_Code, C.Cust_Name AS Customer, O.Ord_Num, O.Ord_Amount FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                internal static class DDL
                {
                    internal static string Create_Table = @"CREATE TABLE Employee (EmployeeID INT PRIMARY KEY, FirstName NVARCHAR(50), LastName NVARCHAR(50))";
                    internal static string Alter_Table = @"ALTER TABLE Employee ADD MiddleName VARCHAR(50)";
                    internal static string Truncate_Table = @"TRUNCATE TABLE Employee";
                    internal static string Rename_Table = @"EXEC SP_RENAME Employee, Employees";
                    internal static string Drop_Table = @"DROP TABLE Employees";
                    internal static string DDL_Execute_check = @"SELECT COUNT(*) AS Table_Count FROM Information_Schema.Tables WHERE LOWER(Table_Schema) = LOWER('{0}') AND LOWER(Table_Name) = LOWER('{1}')";
                }
            }
        }

        internal static class MySQLQueries
        {
            internal static class Smoke
            {
                internal static string SelectSql = @"SELECT DATABASE() AS current_database";
            }
            internal static class TestDB
            {
                internal static string SelectSql = @"SELECT * FROM Agents";
                internal static string SelectSql_Join = @"SELECT A.Agent_Code, A.Agent_Name, C.Cust_Code, C.Cust_Name, O.Ord_Num, O.Ord_Amount, O.Advance_Amount, O.Ord_Date, O.Ord_Description FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                internal static string SelectSql_Alias = @"SELECT A.Agent_Name AS Agent, A.WORKING_AREA AS Agent_Location, C.Cust_Name AS Customer, C.WORKING_AREA AS Customer_Location, O.Agent_Code, O.Cust_Code FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                internal static string SelectSql_DataTypes = @"SELECT * FROM DataTypes";
                internal static string SelectSql_Strict = @"SELECT A.Agent_Code, A.Agent_Name AS Agent, C.Cust_Code, C.Cust_Name AS Customer, O.Ord_Num, O.Ord_Amount FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                internal static class DDL
                {
                    internal static string Create_Table = @"CREATE TABLE Employee (EmployeeID INT PRIMARY KEY, FirstName NVARCHAR(50), LastName NVARCHAR(50))";
                    internal static string Alter_Table = @"ALTER TABLE Employee ADD MiddleName VARCHAR(50)";
                    internal static string Comment_Table = @"ALTER TABLE Employee COMMENT = 'This table stores employee records'";
                    internal static string Comment_Table_Column = @"ALTER TABLE Employee MODIFY COLUMN MiddleName VARCHAR(50) COMMENT 'This column stores employee middle name'";
                    internal static string Truncate_Table = @"TRUNCATE TABLE Employee";
                    internal static string Rename_Table = @"ALTER TABLE Employee RENAME TO Employees";
                    internal static string Drop_Table = @"DROP TABLE Employees";
                    internal static string DDL_Execute_check = @"SELECT COUNT(*) AS Table_Count FROM Information_Schema.Tables WHERE LOWER(Table_Schema) = LOWER('{0}') AND LOWER(Table_Name) = LOWER('{1}')";
                    internal static string DDL_Table_Comment_check = @"SELECT Table_Name, Table_Comment AS Table_Comment FROM Information_Schema.Tables WHERE LOWER(Table_Schema) = LOWER('{0}') AND LOWER(Table_Name) = LOWER('{1}')";
                    internal static string DDL_Table_Column_Comment_check = @"SELECT Column_Name, Column_Comment AS Table_Column_Comment FROM Information_Schema.Columns WHERE LOWER(Table_Schema) = LOWER('{0}') AND LOWER(Table_Name) = LOWER('{1}')";
                }
            }
        }

        internal static class OracleQueries
        {
            internal static class Smoke
            {
                internal static string SelectSql = @"SELECT 'oracle' AS current_database FROM dual";
            }
            internal static class TestDB
            {
                internal static string SelectSql = @"SELECT * FROM Agents";
                internal static string SelectSql_Join = @"SELECT A.Agent_Code, A.Agent_Name, C.Cust_Code, C.Cust_Name, O.Ord_Num, O.Ord_Amount, O.Advance_Amount, O.Ord_Date, O.Ord_Description FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                internal static string SelectSql_Alias = @"SELECT A.Agent_Name AS Agent, A.WORKING_AREA AS Agent_Location, C.Cust_Name AS Customer, C.WORKING_AREA AS Customer_Location, O.Agent_Code, O.Cust_Code FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                internal static string SelectSql_DataTypes = @"SELECT * FROM DataTypes";
                internal static string SelectSql_Strict = @"SELECT A.Agent_Code, A.Agent_Name AS Agent, C.Cust_Code, C.Cust_Name AS Customer, O.Ord_Num, O.Ord_Amount FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                internal static class DDL
                {
                    internal static string Create_Table = @"CREATE TABLE Employee (EmployeeID NUMBER PRIMARY KEY, FirstName NVARCHAR2(50), LastName NVARCHAR2(50))";
                    internal static string Alter_Table = @"ALTER TABLE Employee ADD MiddleName VARCHAR(50)";
                    internal static string Comment_Table = @"COMMENT ON TABLE Employee IS 'This table stores employee records'";
                    internal static string Comment_Table_Column = @"COMMENT ON COLUMN Employee.MiddleName IS 'This column stores employee middle name'";
                    internal static string Truncate_Table = @"TRUNCATE TABLE Employee";
                    internal static string Rename_Table = @"RENAME Employee TO Employees";
                    internal static string Drop_Table = @"DROP TABLE Employees";
                    internal static string DDL_Execute_check = @"SELECT COUNT(*) AS Table_Count FROM User_Tables WHERE LOWER(Table_Name) = LOWER('{0}')";
                    internal static string DDL_Table_Comment_check = @"SELECT Table_Name, Comments AS Table_Comment FROM All_Tab_Comments WHERE LOWER(Table_Name) = LOWER('{0}')";
                    internal static string DDL_Table_Column_Comment_check = @"SELECT Column_Name, Comments AS Table_Column_Comment FROM All_Col_Comments WHERE LOWER(Table_Name = LOWER('{0}')";
                }
            }
        }

        internal static class PostgreSQLQueries
        {
            internal static class Smoke
            {
                internal static string SelectSql = @"SELECT 'postgres' AS current_database";
            }
            internal static class TestDB
            {
                internal static string SelectSql = @"SELECT * FROM Agents";
                internal static string SelectSql_Join = @"SELECT A.Agent_Code, A.Agent_Name, C.Cust_Code, C.Cust_Name, O.Ord_Num, O.Ord_Amount, O.Advance_Amount, O.Ord_Date, O.Ord_Description FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                internal static string SelectSql_Alias = @"SELECT A.Agent_Name AS Agent, A.WORKING_AREA AS Agent_Location, C.Cust_Name AS Customer, C.WORKING_AREA AS Customer_Location, O.Agent_Code, O.Cust_Code FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                internal static string SelectSql_DataTypes = @"SELECT * FROM DataTypes";
                internal static string SelectSql_Strict = @"SELECT A.Agent_Code, A.Agent_Name AS Agent, C.Cust_Code, C.Cust_Name AS Customer, O.Ord_Num, O.Ord_Amount FROM Agents A INNER JOIN 
                                                        Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
                                                        Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";
                internal static class DDL
                {
                    internal static string Create_Table = @"CREATE TABLE Employee (EmployeeID INT PRIMARY KEY, FirstName VARCHAR(50), LastName VARCHAR(50))";
                    internal static string Alter_Table = @"ALTER TABLE Employee ADD MiddleName VARCHAR(50)";
                    internal static string Comment_Table = @"COMMENT ON TABLE Employee IS 'This table stores employee records'";
                    internal static string Comment_Table_Column = @"COMMENT ON COLUMN Employee.MiddleName IS 'This column stores employee middle name'";
                    internal static string Truncate_Table = @"TRUNCATE TABLE Employee";
                    internal static string Rename_Table = @"ALTER TABLE Employee RENAME TO Employees";
                    internal static string Drop_Table = @"DROP TABLE Employees";
                    internal static string DDL_Execute_check = @"SELECT COUNT(*) AS Table_Count FROM Information_Schema.Tables WHERE LOWER(Table_Schema) = LOWER('{0}') AND LOWER(Table_Name) = LOWER('{1}')";
                    internal static string DDL_Table_Comment_check = @"SELECT Table_Name, Obj_Description(Table_Name::Regclass) AS Table_Comment FROM Information_Schema.Tables WHERE LOWER(Table_Schema) = LOWER('{0}') AND LOWER(Table_Name) = LOWER('{1}')";
                    internal static string DDL_Table_Column_Comment_check = @"SELECT Column_Name, Col_Description(Table_Name::Regclass, Ordinal_Position) AS Table_Column_Comment FROM Information_Schema.Columns WHERE LOWER(Table_Schema) = LOWER('{0}') AND LOWER(Table_Name) = LOWER('{1}')";
                }
            }
        }
    }
}
