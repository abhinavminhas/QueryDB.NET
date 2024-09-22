USE mysql;

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
INSERT INTO Agents VALUES ('A003', 'Alex ', 'London', '0.13', '075-12458969', '');
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
	Ord_Date DATE NOT NULL, 
	Cust_Code VARCHAR(6) NOT NULL REFERENCES Customer, 
	Agent_Code VARCHAR(6) NOT NULL REFERENCES Agents, 
	Ord_Description VARCHAR(60) NOT NULL
);

INSERT INTO Orders VALUES('200100', '1000.00', '600.00', '2008-01-08', 'C00013', 'A003', 'SOD');
INSERT INTO Orders VALUES('200110', '3000.00', '500.00', '2008-04-15', 'C00019', 'A010', 'SOD');
INSERT INTO Orders VALUES('200107', '4500.00', '900.00', '2008-08-30', 'C00007', 'A010', 'SOD');
INSERT INTO Orders VALUES('200112', '2000.00', '400.00', '2008-05-30', 'C00016', 'A007', 'SOD'); 
INSERT INTO Orders VALUES('200113', '4000.00', '600.00', '2008-06-10', 'C00022', 'A002', 'SOD');
INSERT INTO Orders VALUES('200102', '2000.00', '300.00', '2008-05-25', 'C00012', 'A012', 'SOD');
INSERT INTO Orders VALUES('200114', '3500.00', '2000.00', '2008-08-15', 'C00002', 'A008', 'SOD');
INSERT INTO Orders VALUES('200122', '2500.00', '400.00', '2008-09-16', 'C00003', 'A004', 'SOD');
INSERT INTO Orders VALUES('200118', '500.00', '100.00', '2008-07-20', 'C00023', 'A006', 'SOD');
INSERT INTO Orders VALUES('200119', '4000.00', '700.00', '2008-09-16', 'C00007', 'A010', 'SOD');
INSERT INTO Orders VALUES('200121', '1500.00', '600.00', '2008-09-23', 'C00008', 'A004', 'SOD');
INSERT INTO Orders VALUES('200130', '2500.00', '400.00', '2008-07-30', 'C00025', 'A011', 'SOD');
INSERT INTO Orders VALUES('200134', '4200.00', '1800.00', '2008-09-25', 'C00004', 'A005', 'SOD');
INSERT INTO Orders VALUES('200108', '4000.00', '600.00', '2008-02-15', 'C00008', 'A004', 'SOD');
INSERT INTO Orders VALUES('200103', '1500.00', '700.00', '2008-05-15', 'C00021', 'A005', 'SOD');
INSERT INTO Orders VALUES('200105', '2500.00', '500.00', '2008-07-18', 'C00025', 'A011', 'SOD');
INSERT INTO Orders VALUES('200109', '3500.00', '800.00', '2008-07-30', 'C00011', 'A010', 'SOD');
INSERT INTO Orders VALUES('200101', '3000.00', '1000.00', '2008-07-15', 'C00001', 'A008', 'SOD');
INSERT INTO Orders VALUES('200111', '1000.00', '300.00', '2008-07-10', 'C00020', 'A008', 'SOD');
INSERT INTO Orders VALUES('200104', '1500.00', '500.00', '2008-03-13', 'C00006', 'A004', 'SOD');
INSERT INTO Orders VALUES('200106', '2500.00', '700.00', '2008-04-20', 'C00005', 'A002', 'SOD');
INSERT INTO Orders VALUES('200125', '2000.00', '600.00', '2008-10-10', 'C00018', 'A005', 'SOD');
INSERT INTO Orders VALUES('200117', '800.00', '200.00', '2008-10-20', 'C00014', 'A001', 'SOD');
INSERT INTO Orders VALUES('200123', '500.00', '100.00', '2008-09-16', 'C00022', 'A002', 'SOD');
INSERT INTO Orders VALUES('200120', '500.00', '100.00', '2008-07-20', 'C00009', 'A002', 'SOD');
INSERT INTO Orders VALUES('200116', '500.00', '100.00', '2008-07-13', 'C00010', 'A009', 'SOD');
INSERT INTO Orders VALUES('200124', '500.00', '100.00', '2008-06-20', 'C00017', 'A007', 'SOD'); 
INSERT INTO Orders VALUES('200126', '500.00', '100.00', '2008-06-24', 'C00022', 'A002', 'SOD');
INSERT INTO Orders VALUES('200129', '2500.00', '500.00', '2008-07-20', 'C00024', 'A006', 'SOD');
INSERT INTO Orders VALUES('200127', '2500.00', '400.00', '2008-07-20', 'C00015', 'A003', 'SOD');
INSERT INTO Orders VALUES('200128', '3500.00', '1500.00', '2008-07-20', 'C00009', 'A002', 'SOD');
INSERT INTO Orders VALUES('200135', '2000.00', '800.00', '2008-09-16', 'C00007', 'A010', 'SOD');
INSERT INTO Orders VALUES('200131', '900.00', '150.00', '2008-08-26', 'C00012', 'A012', 'SOD');
INSERT INTO Orders VALUES('200133', '1200.00', '400.00', '2008-06-29', 'C00009', 'A002', 'SOD');

CREATE TABLE DataTypes 
(
    BigInt_Column BIGINT,
    Bit_Column BIT,
    Char_Column CHAR(1),
    Date_Column DATE,
    DateTime_Column DATETIME DEFAULT CURRENT_TIMESTAMP,
    Decimal_Column DECIMAL(10, 2),
    Float_Column FLOAT,
    Int_Column INT,
    LongText_Column LONGTEXT,
    MediumInt_Column MEDIUMINT,
    MediumText_Column MEDIUMTEXT,
    SmallInt_Column SMALLINT,
    Text_Column TEXT,
    Time_Column TIME,
    Timestamp_Column TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    TinyInt_Column TINYINT,
    TinyText_Column TINYTEXT,
    VarBinary_Column VARBINARY(255),
    VarChar_Column VARCHAR(255)
);

INSERT INTO DataTypes (
    BigInt_Column, 
    Bit_Column, 
    Char_Column, 
    Date_Column, 
    DateTime_Column, 
    Decimal_Column, 
    Float_Column, 
    Int_Column, 
    LongText_Column, 
    MediumInt_Column, 
    MediumText_Column, 
    SmallInt_Column, 
    Text_Column, 
    Time_Column, 
    Timestamp_Column, 
    TinyInt_Column, 
    TinyText_Column, 
    VarBinary_Column, 
    VarChar_Column
) VALUES 
(
    9223372036854775807, -- BigInt Column
    1, -- Bit Column
    'A', -- Char Column
    '2024-09-21', -- Date Column
    '2024-09-21 13:24:10', -- DateTime Column
    12345.67, -- Decimal Column
    123.45, -- Float Column
    2147483647, -- Int Column
    'This is a long text', -- LongText Column
    8388607, -- MediumInt Column
    'This is a medium text', -- MediumText Column
    32767, -- SmallInt Column
    'This is a text', -- Text Column
    '13:24:10', -- Time Column
    '2024-09-21 13:24:10', -- Timestamp Column
    127, -- TinyInt Column
    'This is a tiny text', -- TinyText Column
    0xDEADBEEF, -- VarBinary Column
    'This is a varchar' -- VarChar Column
);
