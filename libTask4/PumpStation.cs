using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using libTask4.Interfaces;
using Timer = System.Timers.Timer;

namespace libTask4
{
    public class PumpStation : Item
    {
        public double Productivity { get; set; }
        public OilTank Tank { get; set; }

        public delegate void PumpStationEventHandler(PumpStation sender);
        public event PumpStationEventHandler Broken;
        public event PumpStationEventHandler Fixed;

        private readonly double _brakeChance = 0.05;
        private readonly Timer _t = new Timer(1000);
        private readonly Random _rnd = new Random();

        

        public void StartWork()
        {
            _t.Elapsed += _t_Elapsed;
            _t.Start();
        }

        private void _t_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_rnd.NextDouble() <= _brakeChance && !IsBroken)
            {
                IsBroken = true;
                Broken?.Invoke(this);
            }
            if(!IsBroken)
                new Task(() => Tank.AddOil(1)).Start();
        }

        public void StopWork()
        {
            _t.Stop();
        }

        public override void Fix()
        {
            IsBroken = false;
            Fixed?.Invoke(this);
        }
    }
}
