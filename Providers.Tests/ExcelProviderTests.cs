using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Data.Candles;
using Data.Interfaces;
using Data.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Providers.Tests.Interfaces;

namespace Providers.Tests
{
    [TestClass]
    public class ExcelProviderTests : IFileProviderTest
    {
        private FileInfo file = new FileInfo(@"Sources\prices.xlsx");

        [TestMethod]
        public void CreateObjectTest()
        {
            var provider = new ExcelDataProvider(file);
        }

        [TestMethod]
        public void GetDataNotNullTest()
        {
            var provider = new ExcelDataProvider(file);
            var data = provider.GetData();
            Assert.AreNotEqual(null, data);
        }

        [TestMethod]
        public void GetDataNotSameTest()
        {
            var provider = new ExcelDataProvider(file);
            var data1 = provider.GetData();
            var data2 = provider.GetData();
            Assert.AreNotEqual(data1, data2);
        }

        [TestMethod]
        public void TimerTest()
        {
            var provider = new ExcelDataProvider(file);
            provider.Timer.Start();
            List<ICandle> candles = new List<ICandle>();
            provider.DataReceived += (c) => candles.Add(c);
            Thread.Sleep(15000);
            Assert.IsTrue(candles.Count >= 2 && !candles[0].Equals(candles[1]));
        }
    }
}
