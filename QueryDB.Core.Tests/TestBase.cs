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
