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
        protected readonly string PostgreSQLConnectionString = ConfigurationManager.AppSettings["PostgreSQLConnection"];
        protected const string DB_TESTS = "DB-TESTS";
        protected const string SMOKE_TESTS = "SMOKE-TESTS";
        protected const string MSSQL_TESTS = "MSSQL-TESTS";
        protected const string MYSQL_TESTS = "MYSQL-TESTS";
        protected const string ORACLE_TESTS = "ORACLE-TESTS";
        protected const string POSTGRESQL_TESTS = "POSTGRESQL-TESTS";

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
