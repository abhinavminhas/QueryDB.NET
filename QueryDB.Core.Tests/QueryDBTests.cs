using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
        public void ExecuteTransaction_UnknownDB_ReturnsFalse()
        {
            var sqlStatements = new List<string>
            {
                "DELETE FROM users"
            };

            var dbContext = new DBContext((DB)999, "some_invalid_connection_string");
            var result = dbContext.ExecuteTransaction(sqlStatements);

            Assert.IsFalse(result);
        }

        #endregion

    }
}
