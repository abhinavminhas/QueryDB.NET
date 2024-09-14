using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QueryDB.Core.Tests
{
    [TestClass]
    public class PostgreSQLTests : TestBase
    {

        #region PostgreSQL DB Tests

        #region Smoke Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(POSTGRESQL_TESTS), TestCategory(SMOKE_TESTS)]
        public void Test_PostgreSQL_FetchData()
        {
            var selectSql = Queries.PostgreSQLQueries.Smoke.SelectSql;
            var data = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString).FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("postgres", data[0].ReferenceData["current_database"]);

            var dbContext = new DBContext(DB.PostgreSQL, PostgreSQLConnectionString);
            data = dbContext.FetchData(selectSql);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual("postgres", data[0].ReferenceData["current_database"]);
        }

        #endregion

        #endregion

    }
}
