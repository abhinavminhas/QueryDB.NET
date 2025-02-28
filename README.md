# QueryDB
*QueryDB solution in .NET*. </br></br>
[![Build / Test](https://github.com/abhinavminhas/QueryDB.NET/actions/workflows/build.yml/badge.svg)](https://github.com/abhinavminhas/QueryDB.NET/actions/workflows/build.yml)
[![codecov](https://codecov.io/gh/abhinavminhas/QueryDB.NET/graph/badge.svg?token=L21DM7HZ46)](https://codecov.io/gh/abhinavminhas/QueryDB.NET)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=abhinavminhas_QueryDB&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=abhinavminhas_QueryDB)
![maintainer](https://img.shields.io/badge/Creator/Maintainer-abhinavminhas-e65c00)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![NuGet](https://img.shields.io/nuget/v/QueryDB?color=%23004880&label=Nuget)](https://www.nuget.org/packages/QueryDB/)  

[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=abhinavminhas_QueryDB&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=abhinavminhas_QueryDB)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=abhinavminhas_QueryDB&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=abhinavminhas_QueryDB)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=abhinavminhas_QueryDB&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=abhinavminhas_QueryDB)

QueryDB is a flexible database query framework designed to simplify querying and executing transactions across multiple database systems.

## Supported Databases
- [MSSQL](https://www.microsoft.com/en-us/sql-server)
- [MySQL](https://www.mysql.com/)
- [Oracle](https://www.oracle.com/)
- [PostgreSQL](https://www.postgresql.org/)

## Download
The package is available and can be downloaded using [nuget.org](https://www.nuget.org/) package manager.  
- Package Name - [QueryDB](https://www.nuget.org/packages/QueryDB).

## .NET Supported Versions

Built on **.NET Standard 2.0** - ( [_Supported Versions_](https://learn.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-0#tabpanel_1_net-standard-2-0:~:text=Select%20.NET%20Standard%20version) )

## Features
- Retrieve data from the database.
- Execute scalar queries (returning a single value).
- Execute non-query database commands (e.g. `INSERT`, `UPDATE`, `DELETE`).
- Execute transactions while maintaining atomicity.
- Support for Synchronous and Asynchronous operations.

## Getting Started
    
- _**Setup `DBContext` with the database of your choice :**_

    ``` csharp
    var dbContext = new DBContext(DB.<Database Type>, <Connection String>);
    ```

- _**Execute `DBContext` commands :**_

    <details>

    <summary><b><tt>Execute DBContext Commands Synchronously</tt></b></summary></br>

    ``` csharp
    var result = dbContext.FetchData(<Sql Statement>);
    ```
    ``` csharp
    var result = dbContext.FetchData<T>(<Sql Statement>);
    ```
    ``` csharp
    var result = dbContext.ExecuteScalar(<Sql Statement>);
    ```
    ``` csharp
    var result = dbContext.ExecuteScalar<T>(<Sql Statement>);
    ```
    ``` csharp
    var result = dbContext.ExecuteCommand(<Sql Statement>);
    ```
    ``` csharp
    var result = dbContext.ExecuteTransaction(<List of Sql Statements>);
    ```

    </details>

    <details>

    <summary><b><tt>Execute DBContext Commands Asynchronously</tt></b></summary></br>
    
    ``` csharp
    var result = dbContext.FetchDataAsync(<Sql Statement>);
    ```
    ``` csharp
    var result = dbContext.FetchDataAsync<T>(<Sql Statement>);
    ```
    ``` csharp
    var result = dbContext.ExecuteScalarAsync(<Sql Statement>);
    ```
    ``` csharp
    var result = dbContext.ExecuteScalarAsync<T>(<Sql Statement>);
    ```
    ``` csharp
    var result = dbContext.ExecuteCommandAsync(<Sql Statement>);
    ```
    ``` csharp
    var result = dbContext.ExecuteTransactionAsync(<List of Sql Statements>);
    ```

    </details>

## Examples

> <b>Data Retrieval</b>
``` csharp
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

var sql = @"SELECT A.Agent_Code, A.Agent_Name, C.Cust_Code, C.Cust_Name, O.Ord_Num, O.Ord_Amount, O.Advance_Amount, O.Ord_Date, O.Ord_Description FROM Agents A INNER JOIN 
           Customer C ON C.Agent_Code = A.Agent_Code INNER JOIN 
           Orders O ON O.Cust_Code = C.Cust_Code AND O.Agent_Code = A.Agent_Code";

var data = new DBContext(DB.<Database Type>, <Connection String>).FetchData<Orders>(selectSql);
var agent = data.FirstOrDefault(X => X.Agent_Name == "Foo");
```

> <b>Transaction</b>

``` csharp
// Create, Insert & Update
var statements = new List<string>
{
    "CREATE TABLE Employee (EmployeeID INT PRIMARY KEY, FirstName NVARCHAR(50), LastName NVARCHAR(50))",
    "INSERT INTO Employee VALUES ('E01', 'John', 'Wick')",
    "UPDATE Employee SET FirstName = 'Jack' LastName = 'Reacher' WHERE EmployeeID = 'E01'"
};
var dbContext = new DBContext(DB.MSSQL, MSSQLConnectionString);
var result = dbContext.ExecuteTransaction(statements);
---
