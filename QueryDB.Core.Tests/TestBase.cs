using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;

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

        protected string ConvertToUTCInUSFormat(string dateString)
        {
            DateTimeOffset date;
            string[] formats = {
            "d/M/yyyy h:m:s tt zzz", "d/M/yyyy H:m:s zzz", // Single-digit day, month, hour, minute, second with offset
            "dd/MM/yyyy h:m:s tt zzz", "dd/MM/yyyy H:m:s zzz", // Double-digit day and month, single-digit hour, minute, second with offset
            "M/d/yyyy h:m:s tt zzz", "M/d/yyyy H:m:s zzz", // Single-digit month and day, single-digit hour, minute, second with offset
            "MM/dd/yyyy h:m:s tt zzz", "MM/dd/yyyy H:m:s zzz", // Double-digit month and day, single-digit hour, minute, second with offset
            "d/M/yyyy hh:mm:ss tt zzz", "d/M/yyyy HH:mm:ss zzz", // Single-digit day and month, double-digit hour, minute, second with offset
            "dd/MM/yyyy hh:mm:ss tt zzz", "dd/MM/yyyy HH:mm:ss zzz", // Double-digit day and month, double-digit hour, minute, second with offset
            "M/d/yyyy hh:mm:ss tt zzz", "M/d/yyyy HH:mm:ss zzz", // Single-digit month and day, double-digit hour, minute, second with offset
            "MM/dd/yyyy hh:mm:ss tt zzz", "MM/dd/yyyy HH:mm:ss zzz" // Double-digit month and day, double-digit hour, minute, second with offset
        };
            CultureInfo provider = CultureInfo.InvariantCulture;

            try
            {
                date = DateTimeOffset.ParseExact(dateString, formats, provider, DateTimeStyles.None);
                DateTime utcDate = date.UtcDateTime; // Convert to UTC
                return utcDate.ToString("MM/dd/yyyy HH:mm:ss", provider); // 24-hour format
            }
            catch (FormatException)
            {
                throw new ArgumentException("The date string is not in a recognized format.");
            }
        }

        protected string ConvertToUSFormat(string dateString)
        {
            DateTime date;
            string[] formats = {
            "d/M/yyyy h:m:s tt", "d/M/yyyy H:m:s", // Single-digit day, month, hour, minute, second
            "dd/MM/yyyy h:m:s tt", "dd/MM/yyyy H:m:s", // Double-digit day and month, single-digit hour, minute, second
            "M/d/yyyy h:m:s tt", "M/d/yyyy H:m:s", // Single-digit month and day, single-digit hour, minute, second
            "MM/dd/yyyy h:m:s tt", "MM/dd/yyyy H:m:s", // Double-digit month and day, single-digit hour, minute, second
            "d/M/yyyy hh:mm:ss tt", "d/M/yyyy HH:mm:ss", // Single-digit day and month, double-digit hour, minute, second
            "dd/MM/yyyy hh:mm:ss tt", "dd/MM/yyyy HH:mm:ss", // Double-digit day and month, double-digit hour, minute, second
            "M/d/yyyy hh:mm:ss tt", "M/d/yyyy HH:mm:ss", // Single-digit month and day, double-digit hour, minute, second
            "MM/dd/yyyy hh:mm:ss tt", "MM/dd/yyyy HH:mm:ss", // Double-digit month and day, double-digit hour, minute, second
            "d/M/yyyy h:m:s tt zzz", "d/M/yyyy H:m:s zzz", // Single-digit day, month, hour, minute, second with offset
            "dd/MM/yyyy h:m:s tt zzz", "dd/MM/yyyy H:m:s zzz", // Double-digit day and month, single-digit hour, minute, second with offset
            "M/d/yyyy h:m:s tt zzz", "M/d/yyyy H:m:s zzz", // Single-digit month and day, single-digit hour, minute, second with offset
            "MM/dd/yyyy h:m:s tt zzz", "MM/dd/yyyy H:m:s zzz", // Double-digit month and day, single-digit hour, minute, second with offset
            "d/M/yyyy hh:mm:ss tt zzz", "d/M/yyyy HH:mm:ss zzz", // Single-digit day and month, double-digit hour, minute, second with offset
            "dd/MM/yyyy hh:mm:ss tt zzz", "dd/MM/yyyy HH:mm:ss zzz", // Double-digit day and month, double-digit hour, minute, second with offset
            "M/d/yyyy hh:mm:ss tt zzz", "M/d/yyyy HH:mm:ss zzz", // Single-digit month and day, double-digit hour, minute, second with offset
            "MM/dd/yyyy hh:mm:ss tt zzz", "MM/dd/yyyy HH:mm:ss zzz" // Double-digit month and day, double-digit hour, minute, second with offset
            };
            CultureInfo provider = CultureInfo.InvariantCulture;
            try
            {
                date = DateTime.ParseExact(dateString, formats, provider, DateTimeStyles.None);
                return date.ToString("MM/dd/yyyy HH:mm:ss", provider); // 24-hour format
            }
            catch (FormatException)
            {
                throw new ArgumentException("The date string is not in a recognized format.");
            }
        }

        protected string GetBase64Content(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found - '" + filePath + "'.");
            byte[] fileBytes = File.ReadAllBytes(filePath);
            string base64Content = Convert.ToBase64String(fileBytes);
            return base64Content;
        }
    }
}
