using System;
using System.IO;
using Data.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Providers.Tests.Interfaces;

namespace Providers.Tests
{
    [TestClass]
    public class JsonProviderTests : IFileProviderTest
    {
        private FileInfo file = new FileInfo("Sources/prices.json");
        [TestMethod]
        public void CreateObjectTest()
        {
            var provider = new JsonDataProvider(file);
        }

        [TestMethod]
        public void GetDataNotNullTest()
        {
            var provider = new JsonDataProvider(file);
            var data = provider.GetData();
            Console.WriteLine(JsonConvert.SerializeObject(data));

            Assert.AreNotEqual(null, data);
        }

        [TestMethod]
        public void GetDataNotSameTest()
        {
            var provider = new JsonDataProvider(file);
            var data1 = provider.GetData();
            var data2 = provider.GetData();
            Console.WriteLine(JsonConvert.SerializeObject(data1));
            Console.WriteLine(JsonConvert.SerializeObject(data2));

            Assert.AreNotSame(data1, data2);
        }
    }
}
