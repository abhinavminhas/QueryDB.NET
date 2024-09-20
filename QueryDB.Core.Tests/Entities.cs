namespace QueryDB.Core.Tests
{
    public class Entities
    {
        public class MSSQL
        {
            public class Agents
            {
                public string Agent_Code { get; set; }
                public string Agent_Name { get; set; }
                public string Working_Area { get; set; }
                public decimal Commission { get; set; }
                public string Phone_No { get; set; }
                public string Country { get; set; }
            }
            public class Orders
            {
                public string Agent_Code { get; set; }
                public string Agent { get; set; }
                public string Agent_Name { get; set; }
                public string Agent_Location { get; set; }
                public string Cust_Code { get; set; }
                public string Customer { get; set; }
                public string Cust_Name { get; set; }
                public string Customer_Location { get; set; }
                public decimal Ord_Num { get; set; }
                public decimal Ord_Amount { get; set; }
                public decimal Advance_Amount { get; set; }
                public string Ord_Description { get; set; }
            }
        }
        public class MySQL
        {
            public class Agents
            {
                public string Agent_Code { get; set; }
                public string Agent_Name { get; set; }
                public string Working_Area { get; set; }
                public decimal Commission { get; set; }
                public string Phone_No { get; set; }
                public string Country { get; set; }
            }
            public class Orders
            {
                public string Agent_Code { get; set; }
                public string Agent { get; set; }
                public string Agent_Name { get; set; }
                public string Agent_Location { get; set; }
                public string Cust_Code { get; set; }
                public string Customer { get; set; }
                public string Cust_Name { get; set; }
                public string Customer_Location { get; set; }
                public decimal Ord_Num { get; set; }
                public decimal Ord_Amount { get; set; }
                public decimal Advance_Amount { get; set; }
                public string Ord_Description { get; set; }
            }
        }
        public class Oracle
        {
            public class Agents
            {
                public string Agent_Code { get; set; }
                public string Agent_Name { get; set; }
                public string Working_Area { get; set; }
                public double Commission { get; set; }
                public string Phone_No { get; set; }
                public string Country { get; set; }
            }
            public class Orders
            {
                public string Agent_Code { get; set; }
                public string Agent { get; set; }
                public string Agent_Name { get; set; }
                public string Agent_Location { get; set; }
                public string Cust_Code { get; set; }
                public string Customer { get; set; }
                public string Cust_Name { get; set; }
                public string Customer_Location { get; set; }
                public int Ord_Num { get; set; }
                public decimal Ord_Amount { get; set; }
                public decimal Advance_Amount { get; set; }
                public string Ord_Description { get; set; }
            }
        }
        public class PostgreSQL
        {
            public class Agents
            {
                public string Agent_Code { get; set; }
                public string Agent_Name { get; set; }
                public string Working_Area { get; set; }
                public decimal Commission { get; set; }
                public string Phone_No { get; set; }
                public string Country { get; set; }
            }
            public class Orders
            {
                public string Agent_Code { get; set; }
                public string Agent { get; set; }
                public string Agent_Name { get; set; }
                public string Agent_Location { get; set; }
                public string Cust_Code { get; set; }
                public string Customer { get; set; }
                public string Cust_Name { get; set; }
                public string Customer_Location { get; set; }
                public decimal Ord_Num { get; set; }
                public decimal Ord_Amount { get; set; }
                public decimal Advance_Amount { get; set; }
                public string Ord_Description { get; set; }
            }
        }
    }
}
