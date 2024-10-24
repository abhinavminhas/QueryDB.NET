USE master;

CREATE TABLE Agents 
(
    Agent_Code VARCHAR(6) NOT NULL PRIMARY KEY, 
    Agent_Name VARCHAR(40), 
    Working_Area VARCHAR(35), 
    Commission DECIMAL(10,2), 
    Phone_No VARCHAR(15), 
    Country VARCHAR(25)
);

INSERT INTO Agents VALUES ('A007', 'Ramasundar', 'Bangalore', '0.15', '077-25814763', '');
INSERT INTO Agents VALUES ('A003', 'Alex', 'London', '0.13', '075-12458969', '');
INSERT INTO Agents VALUES ('A008', 'Alford', 'New York', '0.12', '044-25874365', '');
INSERT INTO Agents VALUES ('A011', 'Ravi Kumar', 'Bangalore', '0.15', '077-45625874', '');
INSERT INTO Agents VALUES ('A010', 'Santakumar', 'Chennai', '0.14', '007-22388644', '');
INSERT INTO Agents VALUES ('A012', 'Lucida', 'San Jose', '0.12', '044-52981425', '');
INSERT INTO Agents VALUES ('A005', 'Anderson', 'Brisbane', '0.13', '045-21447739', '');
INSERT INTO Agents VALUES ('A001', 'Subbarao', 'Bangalore', '0.14', '077-12346674', '');
INSERT INTO Agents VALUES ('A002', 'Mukesh', 'Mumbai', '0.11', '029-12358964', '');
INSERT INTO Agents VALUES ('A006', 'McDen', 'London', '0.15', '078-22255588', '');
INSERT INTO Agents VALUES ('A004', 'Ivan', 'Torento', '0.15', '008-22544166', '');
INSERT INTO Agents VALUES ('A009', 'Benjamin', 'Hampshair', '0.11', '008-22536178', '');

CREATE TABLE Customer 
(  
    Cust_Code VARCHAR(6) NOT NULL PRIMARY KEY, 
	Cust_Name VARCHAR(40) NOT NULL, 
	Cust_City VARCHAR(35), 
	Working_Area VARCHAR(35) NOT NULL, 
	Cust_Country VARCHAR(20) NOT NULL, 
	Grade INTEGER, 
	Opening_Amt DECIMAL(12,2) NOT NULL, 
	Recieve_Amt DECIMAL(12,2) NOT NULL, 
	Payment_Amt DECIMAL(12,2) NOT NULL, 
	Outstanding_Amt DECIMAL(12,2) NOT NULL, 
	Phone_No VARCHAR(17) NOT NULL, 
	Agent_Code VARCHAR(6) NOT NULL REFERENCES Agents
);

INSERT INTO Customer VALUES ('C00013', 'Holmes', 'London', 'London', 'UK', '2', '6000.00', '5000.00', '7000.00', '4000.00', 'BBBBBBB', 'A003');
INSERT INTO Customer VALUES ('C00001', 'Micheal', 'New York', 'New York', 'USA', '2', '3000.00', '5000.00', '2000.00', '6000.00', 'CCCCCCC', 'A008');
INSERT INTO Customer VALUES ('C00020', 'Albert', 'New York', 'New York', 'USA', '3', '5000.00', '7000.00', '6000.00', '6000.00', 'BBBBSBB', 'A008');
INSERT INTO Customer VALUES ('C00025', 'Ravindran', 'Bangalore', 'Bangalore', 'India', '2', '5000.00', '7000.00', '4000.00', '8000.00', 'AVAVAVA', 'A011');
INSERT INTO Customer VALUES ('C00024', 'Cook', 'London', 'London', 'UK', '2', '4000.00', '9000.00', '7000.00', '6000.00', 'FSDDSDF', 'A006');
INSERT INTO Customer VALUES ('C00015', 'Stuart', 'London', 'London', 'UK', '1', '6000.00', '8000.00', '3000.00', '11000.00', 'GFSGERS', 'A003');
INSERT INTO Customer VALUES ('C00002', 'Bolt', 'New York', 'New York', 'USA', '3', '5000.00', '7000.00', '9000.00', '3000.00', 'DDNRDRH', 'A008');
INSERT INTO Customer VALUES ('C00018', 'Fleming', 'Brisbane', 'Brisbane', 'Australia', '2', '7000.00', '7000.00', '9000.00', '5000.00', 'NHBGVFC', 'A005');
INSERT INTO Customer VALUES ('C00021', 'Jacks', 'Brisbane', 'Brisbane', 'Australia', '1', '7000.00', '7000.00', '7000.00', '7000.00', 'WERTGDF', 'A005');
INSERT INTO Customer VALUES ('C00019', 'Yearannaidu', 'Chennai', 'Chennai', 'India', '1', '8000.00', '7000.00', '7000.00', '8000.00', 'ZZZZBFV', 'A010');
INSERT INTO Customer VALUES ('C00005', 'Sasikant', 'Mumbai', 'Mumbai', 'India', '1', '7000.00', '11000.00', '7000.00', '11000.00', '147-25896312', 'A002');
INSERT INTO Customer VALUES ('C00007', 'Ramanathan', 'Chennai', 'Chennai', 'India', '1', '7000.00', '11000.00', '9000.00', '9000.00', 'GHRDWSD', 'A010');
INSERT INTO Customer VALUES ('C00022', 'Avinash', 'Mumbai', 'Mumbai', 'India', '2', '7000.00', '11000.00', '9000.00', '9000.00', '113-12345678','A002');
INSERT INTO Customer VALUES ('C00004', 'Winston', 'Brisbane', 'Brisbane', 'Australia', '1', '5000.00', '8000.00', '7000.00', '6000.00', 'AAAAAAA', 'A005');
INSERT INTO Customer VALUES ('C00023', 'Karl', 'London', 'London', 'UK', '0', '4000.00', '6000.00', '7000.00', '3000.00', 'AAAABAA', 'A006');
INSERT INTO Customer VALUES ('C00006', 'Shilton', 'Torento', 'Torento', 'Canada', '1', '10000.00', '7000.00', '6000.00', '11000.00', 'DDDDDDD', 'A004');
INSERT INTO Customer VALUES ('C00010', 'Charles', 'Hampshair', 'Hampshair', 'UK', '3', '6000.00', '4000.00', '5000.00', '5000.00', 'MMMMMMM', 'A009');
INSERT INTO Customer VALUES ('C00017', 'Srinivas', 'Bangalore', 'Bangalore', 'India', '2', '8000.00', '4000.00', '3000.00', '9000.00', 'AAAAAAB', 'A007');
INSERT INTO Customer VALUES ('C00012', 'Steven', 'San Jose', 'San Jose', 'USA', '1', '5000.00', '7000.00', '9000.00', '3000.00', 'KRFYGJK', 'A012');
INSERT INTO Customer VALUES ('C00008', 'Karolina', 'Torento', 'Torento', 'Canada', '1', '7000.00', '7000.00', '9000.00', '5000.00', 'HJKORED', 'A004');
INSERT INTO Customer VALUES ('C00003', 'Martin', 'Torento', 'Torento', 'Canada', '2', '8000.00', '7000.00', '7000.00', '8000.00', 'MJYURFD', 'A004');
INSERT INTO Customer VALUES ('C00009', 'Ramesh', 'Mumbai', 'Mumbai', 'India', '3', '8000.00', '7000.00', '3000.00', '12000.00', 'Phone No', 'A002');
INSERT INTO Customer VALUES ('C00014', 'Rangarappa', 'Bangalore', 'Bangalore', 'India', '2', '8000.00', '11000.00', '7000.00', '12000.00', 'AAAATGF', 'A001');
INSERT INTO Customer VALUES ('C00016', 'Venkatpati', 'Bangalore', 'Bangalore', 'India', '2', '8000.00', '11000.00', '7000.00', '12000.00', 'JRTVFDD', 'A007');
INSERT INTO Customer VALUES ('C00011', 'Sundariya', 'Chennai', 'Chennai', 'India', '3', '7000.00', '11000.00', '7000.00', '11000.00', 'PPHGRTS', 'A010');

CREATE TABLE Orders 
(
    Ord_Num DECIMAL(6,0) NOT NULL PRIMARY KEY, 
	Ord_Amount DECIMAL(12,2) NOT NULL, 
	Advance_Amount DECIMAL(12,2) NOT NULL, 
	Ord_Date DATE DEFAULT GETUTCDATE()NOT NULL , 
	Cust_Code VARCHAR(6) NOT NULL REFERENCES Customer, 
	Agent_Code VARCHAR(6) NOT NULL REFERENCES Agents, 
	Ord_Description VARCHAR(60) NOT NULL
);

INSERT INTO Orders VALUES('200100', '1000.00', '600.00', '08/01/2008', 'C00013', 'A003', 'SOD');
INSERT INTO Orders VALUES('200110', '3000.00', '500.00', '04/15/2008', 'C00019', 'A010', 'SOD');
INSERT INTO Orders VALUES('200107', '4500.00', '900.00', '08/30/2008', 'C00007', 'A010', 'SOD');
INSERT INTO Orders VALUES('200112', '2000.00', '400.00', '05/30/2008', 'C00016', 'A007', 'SOD'); 
INSERT INTO Orders VALUES('200113', '4000.00', '600.00', '06/10/2008', 'C00022', 'A002', 'SOD');
INSERT INTO Orders VALUES('200102', '2000.00', '300.00', '05/25/2008', 'C00012', 'A012', 'SOD');
INSERT INTO Orders VALUES('200114', '3500.00', '2000.00', '08/15/2008', 'C00002', 'A008', 'SOD');
INSERT INTO Orders VALUES('200122', '2500.00', '400.00', '09/16/2008', 'C00003', 'A004', 'SOD');
INSERT INTO Orders VALUES('200118', '500.00', '100.00', '07/20/2008', 'C00023', 'A006', 'SOD');
INSERT INTO Orders VALUES('200119', '4000.00', '700.00', '09/16/2008', 'C00007', 'A010', 'SOD');
INSERT INTO Orders VALUES('200121', '1500.00', '600.00', '09/23/2008', 'C00008', 'A004', 'SOD');
INSERT INTO Orders VALUES('200130', '2500.00', '400.00', '07/30/2008', 'C00025', 'A011', 'SOD');
INSERT INTO Orders VALUES('200134', '4200.00', '1800.00', '09/25/2008', 'C00004', 'A005', 'SOD');
INSERT INTO Orders VALUES('200108', '4000.00', '600.00', '02/15/2008', 'C00008', 'A004', 'SOD');
INSERT INTO Orders VALUES('200103', '1500.00', '700.00', '05/15/2008', 'C00021', 'A005', 'SOD');
INSERT INTO Orders VALUES('200105', '2500.00', '500.00', '07/18/2008', 'C00025', 'A011', 'SOD');
INSERT INTO Orders VALUES('200109', '3500.00', '800.00', '07/30/2008', 'C00011', 'A010', 'SOD');
INSERT INTO Orders VALUES('200101', '3000.00', '1000.00', '07/15/2008', 'C00001', 'A008', 'SOD');
INSERT INTO Orders VALUES('200111', '1000.00', '300.00', '07/10/2008', 'C00020', 'A008', 'SOD');
INSERT INTO Orders VALUES('200104', '1500.00', '500.00', '03/13/2008', 'C00006', 'A004', 'SOD');
INSERT INTO Orders VALUES('200106', '2500.00', '700.00', '04/20/2008', 'C00005', 'A002', 'SOD');
INSERT INTO Orders VALUES('200125', '2000.00', '600.00', '10/10/2008', 'C00018', 'A005', 'SOD');
INSERT INTO Orders VALUES('200117', '800.00', '200.00', '10/20/2008', 'C00014', 'A001', 'SOD');
INSERT INTO Orders VALUES('200123', '500.00', '100.00', '09/16/2008', 'C00022', 'A002', 'SOD');
INSERT INTO Orders VALUES('200120', '500.00', '100.00', '07/20/2008', 'C00009', 'A002', 'SOD');
INSERT INTO Orders VALUES('200116', '500.00', '100.00', '07/13/2008', 'C00010', 'A009', 'SOD');
INSERT INTO Orders VALUES('200124', '500.00', '100.00', '06/20/2008', 'C00017', 'A007', 'SOD'); 
INSERT INTO Orders VALUES('200126', '500.00', '100.00', '06/24/2008', 'C00022', 'A002', 'SOD');
INSERT INTO Orders VALUES('200129', '2500.00', '500.00', '07/20/2008', 'C00024', 'A006', 'SOD');
INSERT INTO Orders VALUES('200127', '2500.00', '400.00', '07/20/2008', 'C00015', 'A003', 'SOD');
INSERT INTO Orders VALUES('200128', '3500.00', '1500.00', '07/20/2008', 'C00009', 'A002', 'SOD');
INSERT INTO Orders VALUES('200135', '2000.00', '800.00', '09/16/2008', 'C00007', 'A010', 'SOD');
INSERT INTO Orders VALUES('200131', '900.00', '150.00', '08/26/2008', 'C00012', 'A012', 'SOD');
INSERT INTO Orders VALUES('200133', '1200.00', '400.00', '06/29/2008', 'C00009', 'A002', 'SOD');

CREATE TABLE DataTypes 
(
    BigInt_Column BIGINT,
    Binary_Column BINARY(50),
    Bit_Column BIT,
    Char_Column CHAR(8),
    Date_Column DATE DEFAULT GETUTCDATE(),
    DateTime_Column DATETIME DEFAULT GETUTCDATE(),
    DateTime2_Column DATETIME2 DEFAULT GETUTCDATE(),
    DateTimeOffset_Column DATETIMEOFFSET,
    Decimal_Column DECIMAL(18, 2),
    Float_Column FLOAT,
    Image_Column IMAGE,
    Int_Column INT,
    Money_Column MONEY,
    NChar_Column NCHAR(9),
    NText_Column NTEXT,
    Numeric_Column NUMERIC(18, 2),
    NVarChar_Column NVARCHAR(50),
    Real_Column REAL,
    SmallDateTime_Column SMALLDATETIME DEFAULT GETUTCDATE(),
    SmallInt_Column SMALLINT,
    SmallMoney_Column SMALLMONEY,
    SqlVariant_Column SQL_VARIANT,
    Text_Column TEXT,
    Time_Column TIME,
    TinyInt_Column TINYINT,
    UniqueIdentifier_Column UNIQUEIDENTIFIER,
    VarBinary_Column VARBINARY(50),
    VarChar_Column VARCHAR(50),
    Xml_Column XML
);

INSERT INTO DataTypes 
(
    BigInt_Column,
    Binary_Column,
    Bit_Column,
    Char_Column,
    Date_Column,
    DateTime_Column,
    DateTime2_Column,
    DateTimeOffset_Column,
    Decimal_Column,
    Float_Column,
    Image_Column,
    Int_Column,
    Money_Column,
    NChar_Column,
    NText_Column,
    Numeric_Column,
    NVarChar_Column,
    Real_Column,
    SmallDateTime_Column,
    SmallInt_Column,
    SmallMoney_Column,
    SqlVariant_Column,
    Text_Column,
    Time_Column,
    TinyInt_Column,
    UniqueIdentifier_Column,
    VarBinary_Column,
    VarChar_Column,
    Xml_Column
) VALUES 
(
    9223372036854775807, -- BigInt_Column
    0x1234567890, -- Binary_Column
    1, -- Bit_Column
    'CharData', -- Char_Column
    '2024-09-21', -- Date_Column
    '2024-09-21 08:34:51', -- DateTime_Column
    '2024-09-21 08:34:51.1234567', -- DateTime2_Column
    '2024-09-21 08:34:51.1234567 +10:00', -- DateTimeOffset_Column
    123456.78, -- Decimal_Column
    123456.78, -- Float_Column
    0x1234567890, -- Image_Column
    2147483647, -- Int_Column
    123456.78, -- Money_Column
    N'NCharData', -- NChar_Column
    N'NTextData', -- NText_Column
    123456.78, -- Numeric_Column
    N'NVarCharData', -- NVarChar_Column
    123.45, -- Real_Column
    '2024-09-21 08:34:51', -- SmallDateTime_Column
    32767, -- SmallInt_Column
    123456.78, -- SmallMoney_Column
    CAST('SampleVariant' AS SQL_VARIANT), -- SqlVariant_Column
    'TextData', -- Text_Column
    '08:34:51', -- Time_Column
    255, -- TinyInt_Column
    '12345678-1234-1234-1234-123456789012', -- UniqueIdentifier_Column
    0x1234567890, -- VarBinary_Column
    'VarCharData', -- VarChar_Column
    '<root><element>XmlData</element></root>' -- Xml_Column
);
