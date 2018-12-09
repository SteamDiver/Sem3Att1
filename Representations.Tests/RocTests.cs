using System;
using Data.Candles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Representations.Tests
{
    [TestClass]
    public class RocTests
    {
        [TestMethod]
        public void RocCreateTest()
        {
            var r = new RoCController(20);
            Assert.AreNotEqual(null, r.SeriesCollection[0]);
        }

        [TestMethod]
        public void RocAddValue()
        {
            var r = new RoCController(20);
            var candle1 = new Candle(1, 1, 1, 1, DateTime.Now);
            var candle2 = new Candle(2, 2, 2, 2, DateTime.Now);

            r.AddValueToLine(candle1, (x) => x.Open);
            r.AddValueToLine(candle2, (x) => x.Open);

            Assert.AreEqual(2, r.SeriesCollection[0].Values.Count);
        }
    }
}
