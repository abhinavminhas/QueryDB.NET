using QueryDB.Resources;
using System.Collections.Generic;

namespace QueryDB
{
    /// <summary>
    /// Represents database context commands.
    /// </summary>
    interface IDBContext
    {
        /// <summary>
        /// Retrieves records for 'Select' queries from the database.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="upperCaseKeys">Boolean parameter to return dictionary keys in uppercase. Default - 'false'.</param>
        /// <returns>List of data Dictionary with column names as keys holding values into a list for multiple rows of data.</returns>
        List<DataDictionary> FetchData(string selectSql, bool upperCaseKeys = false);

        /// <summary>
        ///  Retrieves records for 'Select' queries from the database.
        /// </summary>
        /// <typeparam name="T">Object entity to return data mapped into.</typeparam>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="strict">Enables fetch data only for object <T> properties existing in database query result. Default - 'false'.</param>
        /// <returns>List of data rows mapped into object entity into a list for multiple rows of data.</returns>
        List<T> FetchData<T>(string selectSql, bool strict = false) where T : new();

        /// <summary>
        /// Executes 'Data Definition Language' commands.
        /// </summary>
        /// <param name="ddlStatement">DDL statement as command.</param>
        void ExecuteDDL(string ddlStatement);
    }
}
