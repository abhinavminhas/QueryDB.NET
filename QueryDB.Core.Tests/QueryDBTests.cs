using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueryDB.Core.Tests
{
    [TestClass]
    public class QueryDBTests : TestBase
    {

        #region Unknow DB Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(UNKNOW_DB_TESTS)]
        public void ExecuteCommand_UnknownDB_ReturnsNegativeOne()
        {
            string sqlStatement = "DELETE FROM users";

            var dbContext = new DBContext((DB)999, "some_invalid_connection_string");
            var result = dbContext.ExecuteCommand(sqlStatement);

            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(UNKNOW_DB_TESTS)]
        public async Task ExecuteCommandAsync_UnknownDB_ReturnsNegativeOne()
        {
            string sqlStatement = "DELETE FROM users";

            var dbContext = new DBContext((DB)999, "some_invalid_connection_string");
            var result = await dbContext.ExecuteCommandAsync(sqlStatement);

            Assert.AreEqual(-1, result);
        }

        #endregion

    }
}
