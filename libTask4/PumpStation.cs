using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using libTask4.Interfaces;

namespace libTask4
{
    public class PumpStation : Item
    {
        public double Productivity { get; set; }

        public delegate void PumpStationEventHandler(PumpStation sender);
        public event PumpStationEventHandler Broken;
        public event PumpStationEventHandler Fixed;

        private readonly double _brakeChance = 0.05;
        private readonly Timer _t = new Timer(1000);
        private readonly Random rnd = new Random();



        public void StartWork()
        {
            _t.Elapsed += _t_Elapsed;
            _t.Start();
        }

        private void _t_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (rnd.NextDouble() <= _brakeChance && !IsBroken)
            {
                IsBroken = true;
                Broken?.Invoke(this);
            }
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
