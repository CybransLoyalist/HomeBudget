using System;
using HomeBudget.Helpers;
using NUnit.Framework;

namespace HomeBudgetTests.Helpers
{
    [TestFixture]
    public class FlatToMultidimensionalArrayTests
    {
        [Test]
        public void ShallThrowException_IfSizeIsNotMatchingDimensions()
        {
            string[] input = new[] {"aaa", "bbb", "bbb"};

            Assert.Throws<Exception>(() => FlatToMultidimensionalArray.Fold(input, 2, 3));
        }

        [Test]
        public void DimensionsShallBeSetCorrectly_AfterFolding()
        {
            string[] input = new[] { "aaa1", "bbb1", "ccc1", "aaa2", "bbb2", "ccc2" };

            var result = FlatToMultidimensionalArray.Fold(input, 2, 3);

            Assert.AreEqual(2, result.GetLength(0));
            Assert.AreEqual(3, result.GetLength(1));
        }

        [Test]
        public void DataShallBeSetCorrectly_DimensionsCombination1()
        {
            string[] input = new[] {"aaa1", "bbb1", "ccc1", "aaa2", "bbb2", "ccc2"};

            var result = FlatToMultidimensionalArray.Fold(input, 2, 3);
            
            Assert.AreEqual("aaa1", result[0,0]);
            Assert.AreEqual("bbb1", result[0,1]);
            Assert.AreEqual("ccc1", result[0,2]);
            
            Assert.AreEqual("aaa2", result[1,0]);
            Assert.AreEqual("bbb2", result[1,1]);
            Assert.AreEqual("ccc2", result[1,2]);
        }

        [Test]
        public void DataShallBeSetCorrectly_DimensionsCombination2()
        {
            string[] input = new[] {"aaa1", "bbb1", "ccc1", "aaa2", "bbb2", "ccc2"};

            var result = FlatToMultidimensionalArray.Fold(input, 3, 2);
            
            Assert.AreEqual("aaa1", result[0,0]);
            Assert.AreEqual("bbb1", result[0,1]);

            Assert.AreEqual("ccc1", result[1,0]);
            Assert.AreEqual("aaa2", result[1,1]);

            Assert.AreEqual("bbb2", result[2,0]);
            Assert.AreEqual("ccc2", result[2,1]);
        }
    }
}