using System;
using EulerSharp.Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EulerSharpTest
{
    [TestClass]
    public class Problem1to3Test
    {
        [TestMethod]
        public void Problem1_ReturnsAnswer()
        {
            var expected = 233168;

            var actual = new Problem1().Solve();

            Assert.AreEqual(actual, expected, "Answer did not equal expected value");
        }

        [TestMethod]
        public void Problem2_ReturnsAnswer()
        {
            var expected = 4613732;

            var actual = new Problem2().Solve();

            Assert.AreEqual(actual, expected, "Answer did not equal expected value");
        }

        [TestMethod]
        public void Problem3_ReturnsAnswer()
        {
            long expected = 6857;

            var actual = new Problem3().Solve();

            Assert.AreEqual(actual, expected, "Answer did not equal expected value");
        }
    }
}
