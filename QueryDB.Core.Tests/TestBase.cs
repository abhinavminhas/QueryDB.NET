using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace QueryDB.Core.Tests
{
    public class TestBase
    {
        private readonly string _useDocker = ConfigurationManager.AppSettings["UseDocker"];
        protected readonly string OracleDatabaseString = ConfigurationManager.AppSettings["OracleConnection"];
        protected readonly string SqlServerDatabaseString = ConfigurationManager.AppSettings["SQLServerConnection"];
        protected readonly string MySqlDatabaseString = ConfigurationManager.AppSettings["MySQLConnection"];
        protected const string DB_TESTS = "DB-TESTS";
        protected const string ORACLE_TESTS = "ORACLE-TESTS";
        protected const string MS_SQL_SERVER_TESTS = "MS-SQL-SERVER-TESTS";
        protected const string MY_SQL_TESTS = "MY-SQL-TESTS";

        [AssemblyInitialize]
        internal void CheckDockerImages()
        {
            if (_useDocker.Equals("true"))
            {
                //TBD
            }
        }
    }
}
