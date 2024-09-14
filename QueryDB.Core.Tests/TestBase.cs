using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace QueryDB.Core.Tests
{
    public class TestBase
    {
        private readonly string _useDocker = ConfigurationManager.AppSettings["UseDocker"];
        protected readonly string MSSQLConnectionString = ConfigurationManager.AppSettings["MSSQLConnection"];
        protected readonly string MySQLConnectionString = ConfigurationManager.AppSettings["MySQLConnection"];
        protected readonly string OracleConnectionString = ConfigurationManager.AppSettings["OracleConnection"];
        protected const string DB_TESTS = "DB-TESTS";
        protected const string SMOKE_TESTS = "SMOKE-TESTS";
        protected const string ORACLE_TESTS = "ORACLE-TESTS";
        protected const string MSSQL_TESTS = "MSSQL-TESTS";
        protected const string MYSQL_TESTS = "MYSQL-TESTS";

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
