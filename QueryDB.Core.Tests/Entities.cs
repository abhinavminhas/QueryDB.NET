using System;

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
            public class DataTypes
            {
                public long BigInt_Column { get; set; }
                public byte[] Binary_Column { get; set; }
                public bool Bit_Column { get; set; }
                public string Char_Column { get; set; }
                public DateTime Date_Column { get; set; }
                public DateTime DateTime_Column { get; set; }
                public DateTime DateTime2_Column { get; set; }
                public DateTimeOffset DateTimeOffset_Column { get; set; }
                public decimal Decimal_Column { get; set; }
                public double Float_Column { get; set; }
                public byte[] Image_Column { get; set; }
                public int Int_Column { get; set; }
                public decimal Money_Column { get; set; }
                public string NChar_Column { get; set; }
                public string NText_Column { get; set; }
                public decimal Numeric_Column { get; set; }
                public string NVarChar_Column { get; set; }
                public float Real_Column { get; set; }
                public DateTime SmallDateTime_Column { get; set; }
                public short SmallInt_Column { get; set; }
                public decimal SmallMoney_Column { get; set; }
                public object SqlVariant_Column { get; set; }
                public string Text_Column { get; set; }
                public TimeSpan Time_Column { get; set; }
                public byte[] Timestamp_Column { get; set; }
                public byte TinyInt_Column { get; set; }
                public Guid UniqueIdentifier_Column { get; set; }
                public byte[] VarBinary_Column { get; set; }
                public string VarChar_Column { get; set; }
                public string Xml_Column { get; set; }
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
            public class DataTypes
            {
                public long BigInt_Column { get; set; }
                public ulong Bit_Column { get; set; }
                public string Char_Column { get; set; }
                public DateTime Date_Column { get; set; }
                public DateTime DateTime_Column { get; set; }
                public decimal Decimal_Column { get; set; }
                public float Float_Column { get; set; }
                public int Int_Column { get; set; }
                public string LongText_Column { get; set; }
                public int MediumInt_Column { get; set; }
                public string MediumText_Column { get; set; }
                public short SmallInt_Column { get; set; }
                public string Text_Column { get; set; }
                public TimeSpan Time_Column { get; set; }
                public DateTime Timestamp_Column { get; set; }
                public sbyte TinyInt_Column { get; set; }
                public string TinyText_Column { get; set; }
                public byte[] VarBinary_Column { get; set; }
                public string VarChar_Column { get; set; }
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
                public double Ord_Amount { get; set; }
                public double Advance_Amount { get; set; }
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
            public class DataTypes
            {
                public long BigInt_Column { get; set; }
                public bool Boolean_Column { get; set; }
                public byte[] Bytea_Column { get; set; }
                public string Char_Column { get; set; }
                public string Varchar_Column { get; set; }
                public DateTime Date_Column { get; set; }
                public double Double_Column { get; set; }
                public int Int_Column { get; set; }
                public decimal Money_Column { get; set; }
                public decimal Numeric_Column { get; set; }
                public float Real_Column { get; set; }
                public short SmallInt_Column { get; set; }
                public string Text_Column { get; set; }
                public TimeSpan Time_Column { get; set; }
                public DateTime Timestamp_Column { get; set; }
                public Guid Uuid_Column { get; set; }
            }
        }
    }
}
