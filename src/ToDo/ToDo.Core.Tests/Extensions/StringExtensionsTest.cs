using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo.Core.Extensions;

namespace ToDo.Core.Tests.Extensions
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        [DataRow("\t")]
        public void IsNullOrEmpty_EmptyString_True(string target)
        {
            var result = target.IsNullOrEmpty();

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("a")]
        [DataRow("1")]
        [DataRow("╥")]
        public void IsNullOrEmpty_EmptyString_False(string target)
        {
            var result = target.IsNullOrEmpty();

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        [DataRow("\t")]
        public void IsNotNullOrEmpty_EmptyString_False(string target)
        {
            var result = target.IsNotNullOrEmpty();

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("a")]
        [DataRow("1")]
        [DataRow("╥")]
        public void IsNotNullOrEmpty_EmptyString_True(string target)
        {
            var result = target.IsNotNullOrEmpty();

            Assert.IsTrue(result);
        }
    }
}