using System;
using System.Collections.Generic;
using System.Text;

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
        }
        public class Oracle
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
        }
    }
}
