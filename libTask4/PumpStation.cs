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

        public bool IsWorking { get; set; }
        private readonly double _brakeChance = 0.05;
        private readonly Random _rnd = new Random();
        private static object lockObj = new object();

        public void StartWork()
        {
            IsWorking = true;
            while (IsWorking)
            {
                if (_rnd.NextDouble() <= _brakeChance && !IsBroken)
                {
                    IsBroken = true;
                    Broken?.Invoke(this);
                    return;
                }
                if (!IsBroken && Tank.CurrentVolume < Tank.Capacity)
                   Tank.AddOil(1);
                Thread.Sleep(1000);
            }
        }

        public void StopWork()
        {
            IsWorking = false;
        }

        public override void Fix(TimeSpan time)
        {
            lock (lockObj)
            {
                Thread.Sleep(time);
                IsBroken = false;
            }
            Fixed?.Invoke(this);
        }
    }
}
