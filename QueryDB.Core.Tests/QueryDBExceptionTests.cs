using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryDB.Exceptions;

namespace QueryDB.Core.Tests
{
    [TestClass]
    public class QueryDBExceptionTests : TestBase
    {

        #region QueryDBException Tests

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(QUERY_DB_EXCEPTION_TESTS)]
        public void ToString_ShouldReturnFormattedString_WhenAdditionalInfoIsPresent()
        {
            var exception = new QueryDBException("An error occurred", "Critical", "More details");

            var result = exception.ToString();

            Assert.AreEqual("Type: Critical, Message: An error occurred, Info: More details", result);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(QUERY_DB_EXCEPTION_TESTS)]
        public void ToString_ShouldReturnFormattedString_WithoutAdditionalInfo_WhenAdditionalInfoIsNull()
        {
            var exception = new QueryDBException("Something went wrong", "Warning", null);

            var result = exception.ToString();

            Assert.AreEqual("Type: Warning, Message: Something went wrong", result);
        }

        [TestMethod]
        [TestCategory(DB_TESTS), TestCategory(QUERY_DB_EXCEPTION_TESTS)]
        public void ToString_ShouldReturnFormattedString_WithoutAdditionalInfo_WhenAdditionalInfoIsEmpty()
        {

            var exception = new QueryDBException("Just an update", "Info", "");

            var result = exception.ToString();

            Assert.AreEqual("Type: Info, Message: Just an update", result);
        }

        #endregion

    }
}
