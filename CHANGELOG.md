# Changelog
All notable changes to this project documented here.

## [Released]

## [1.3.0](https://www.nuget.org/packages/QueryDB/1.3.0) - 2025-07-27
### Changed
- Seal adapter classes for better encapsulation.
- Restrict fetch data to SELECT commands.
- Refactoring DBContext to use switch statement for improved clarity and structure.

## [1.2.0](https://www.nuget.org/packages/QueryDB/1.2.0) - 2025-03-04
### Added
- Asynchronous operations
    - `FetchDataAsync()`
    - `ExecuteScalarAsync()`
    - `ExecuteCommandAsync()`
    - `ExecuteTransactionAsync()`
### Changed
- Execute transaction to return transaction outcome and exception details in case of failure instead of logging into console.

## [1.1.0](https://www.nuget.org/packages/QueryDB/1.1.0) - 2025-02-20
### Added
- Execute scalar queries (returning a single value).

## [1.0.0](https://www.nuget.org/packages/QueryDB/1.0.0) - 2025-02-18
### Added
- QueryDB framework for simplified querying and executing transactions across multiple database systems.
    - Retrieve data from database.
    - Execute database commands.
    - Execute transactions while maintaining atomicity.