using System;
using System.Collections.Generic;
using HomeBudget.Extensions;
using NUnit.Framework;

namespace HomeBudgetTests.Helpers
{
    [TestFixture]
    class ListExtensionsTests
    {
        [Test]
        public void IfInputNull_ShallThrowException()
        {
            List<List<int>> input = null;
            Assert.Throws<ArgumentNullException>(() => input.To2DArray());
        }

        [Test]
        public void ShallProperlyConvertListOfLists_To2DArray()
        {
            var input = new List<List<int>>
            {
                new List<int> {11, 12, 13},
                new List<int> {21, 22, 23},
            };

            var result = input.To2DArray();

            Assert.AreEqual(11, result[0,0]);
            Assert.AreEqual(12, result[0,1]);
            Assert.AreEqual(13, result[0,2]);

            Assert.AreEqual(21, result[1,0]);
            Assert.AreEqual(22, result[1,1]);
            Assert.AreEqual(23, result[1,2]);
        }
    }
}
