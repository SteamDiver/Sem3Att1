using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Providers.Tests.Interfaces
{
    public interface IFileProviderTest
    {
        [TestMethod]
        void CreateObjectTest();

        [TestMethod]
        void GetDataNotNullTest();

        [TestMethod]
        void GetDataNotSameTest();

        [TestMethod]
        void TimerTest();
    }
}
