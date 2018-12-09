using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Data.Interfaces;

namespace DataController
{
    public class IncomingDataController
    {
        public delegate void DataEventHandler(ICandle data);
        public event DataEventHandler DataReceived;
        private readonly IDataProvider _dataProvider;
        private Timer _t;

        public IncomingDataController(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void StartReceive(int timeout)
        {
            _t = new Timer(timeout);
            _t.Elapsed += _t_Elapsed;
            _t.Start();
        }

        public void StopReceive()
        {
            _t.Stop();
        }

        private void _t_Elapsed(object sender, ElapsedEventArgs e)
        {
            var data = _dataProvider.GetData();
            OnDataReceived(data);
        }

        protected virtual void OnDataReceived(ICandle data)
        {
            DataReceived?.Invoke(data);
        }
    }
}
