using QueryDB.Resources;
using System.Collections.Generic;

namespace QueryDB
{
    /// <summary>
    /// Represents Database Context to connect to a database type and run commands.
    /// </summary>
    interface IDBContext
    {
        /// <summary>
        /// Retrieves records for 'Select' queries from the database.
        /// </summary>
        /// <param name="selectSql">'Select' query.</param>
        /// <param name="upperCaseKeys">Boolean parameter to return dictionary keys in uppercase / lowercase. Default - 'false'.</param>
        /// <returns>List of data Dictionary with column names as keys holding values into a list for multiple rows of data.</returns>
        List<DataDictionary> FetchData(string selectSql, bool upperCaseKeys = false);
    }
}
