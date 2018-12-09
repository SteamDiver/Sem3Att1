using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Candles;
using Data.Interfaces;

namespace Data.Providers
{
    public abstract class DataProvider : IDataProvider
    {
        public delegate void DataProviderEventHandler(ICandle data);
        public event DataProviderEventHandler DataReceived;

        public abstract ICandle GetData();
        internal void OnDataReceived(ICandle data) => DataReceived?.Invoke(data);
    }
}
