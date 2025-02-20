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

## Getting Started
- Setup DBContext with the database of your choice

    ```
    var dbContext = new DBContext(DB.<Database Type>, <Connection String>);
    ```

- Execute DBContext commands

    ```
    var result = dbContext.FetchData(<Sql Statement>);
    var result = dbContext.FetchData<T>(<Sql Statement>);
    var result = dbContext.ExecuteScalar(<Sql Statement>);
    var result = dbContext.ExecuteScalar<T>(<Sql Statement>);
    var result = dbContext.ExecuteCommand(<Sql Statement>);
    var result = dbContext.ExecuteTransaction(<List of Sql Statements>);
    ```
