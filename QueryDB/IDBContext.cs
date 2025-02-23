using QueryDB.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueryDB
{
    /// <summary>
    /// Represents database context commands.
    /// </summary>
    interface IDBContext
    {
        /// <summary>
        /// Executes and retrieves records for 'Select' queries from the database.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="upperCaseKeys">Boolean parameter to return dictionary keys in uppercase. Default - <c>false</c>.</param>
        /// <returns>List of <see cref="DataDictionary"/> with column names as keys holding values into a list for multiple rows of data.</returns>
        List<DataDictionary> FetchData(string selectSql, bool upperCaseKeys = false);

        /// <summary>
        /// Executes and retrieves records for 'Select' queries from the database.
        /// </summary>
        /// <typeparam name="T">Object entity to return data mapped into.</typeparam>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="strict">Enables fetch data only for object type <typeparamref name="T"/> properties existing in database query result. Default - <c>false</c>.</param>
        /// <returns>List of data rows mapped into object of type <typeparamref name="T"/>.</returns>
        List<T> FetchData<T>(string selectSql, bool strict = false) where T : new();

        /// <summary>
        /// Asynchronously executes and retrieves records for 'Select' queries from the database.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="upperCaseKeys">Boolean parameter to return dictionary keys in uppercase. Default - <c>false</c>.</param>
        /// <returns>List of <see cref="DataDictionary"/> with column names as keys holding values into a list for multiple rows of data.</returns>
        Task<List<DataDictionary>> FetchDataAsync(string selectSql, bool upperCaseKeys = false);

        /// <summary>
        /// Asynchronously executes and retrieves records for 'Select' queries from the database.
        /// </summary>
        /// <typeparam name="T">Object entity to return data mapped into.</typeparam>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="strict">Enables fetch data only for object type <typeparamref name="T"/> properties existing in database query result. Default - <c>false</c>.</param>
        Task<List<T>> FetchDataAsync<T>(string selectSql, bool strict = false) where T : new();

        /// <summary>
        /// Executes the provided SQL statement and returns the first column of the first row in the result set.
        /// If the result is DBNull, an empty string is returned.
        /// </summary>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <returns>
        /// A <see cref="string"/> representing the value of the first column of the first row in the result set,
        /// or an empty string if the result is DBNull.
        /// </returns>
        string ExecuteScalar(string sqlStatement);

        /// <summary>
        /// Executes the provided SQL statement and returns the first column of the first row in the result set,
        /// converted to the specified type <typeparamref name="T"/>. If the result is DBNull, the default value of <typeparamref name="T"/> is returned.
        /// </summary>
        /// <typeparam name="T">The type to which the result should be converted.</typeparam>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <returns>
        /// The value of the first column of the first row in the result set, converted to type <typeparamref name="T"/>,
        /// or the default value of <typeparamref name="T"/> if the result is DBNull.
        /// </returns>
        T ExecuteScalar<T>(string sqlStatement);

        /// <summary>
        /// Asynchronously executes the provided SQL statement and returns the first column of the first row in the result set.
        /// If the result is DBNull, an empty string is returned.
        /// </summary>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <returns>
        /// A <see cref="string"/> representing the value of the first column of the first row in the result set,
        /// or an empty string if the result is DBNull.
        /// </returns>
        Task<string> ExecuteScalarAsync(string sqlStatement);

        /// <summary>
        /// Asynchronously executes the provided SQL statement and returns the first column of the first row in the result set,
        /// converted to the specified type <typeparamref name="T"/>. If the result is DBNull, the default value of <typeparamref name="T"/> is returned.
        /// </summary>
        /// <typeparam name="T">The type to which the result should be converted.</typeparam>
        /// <param name="sqlStatement">The SQL statement to execute. It should be a query that returns a single value.</param>
        /// <returns>
        /// The value of the first column of the first row in the result set, converted to type <typeparamref name="T"/>,
        /// or the default value of <typeparamref name="T"/> if the result is DBNull.
        /// </returns>
        Task<T> ExecuteScalarAsync<T>(string sqlStatement);

        /// <summary>
        /// Executes SQL commands.
        /// </summary>
        /// <param name="sqlStatement">SQL statement as command.</param>
        /// <returns>The number of rows affected.</returns>
        int ExecuteCommand(string sqlStatement);

        /// <summary>
        /// Executes multiple SQL statements within a transaction, ensuring that all statements are executed together.
        /// </summary>
        /// <param name="sqlStatements">A list of SQL statements to execute.</param>
        /// <returns>
        /// Returns <c>true</c> if all statements are executed successfully and the transaction is committed;
        /// <c>false</c> if any statement fails and the transaction is rolled back.
        /// </returns>
        bool ExecuteTransaction(List<string> sqlStatements);
    }
}
