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
        public OilTank Tank { get; set; }
        public int HP { get; set; } = 5;
        public delegate void PumpStationEventHandler(PumpStation sender);
        public event PumpStationEventHandler Broken;
        public event PumpStationEventHandler Dead;
        public event PumpStationEventHandler Fixed;

        private bool _isWorking;
        private readonly double _brakeChance = 0.05;
        private readonly Random _rnd = new Random();
        private Timer _hpTimer;

        public void StartWork()
        {
            _isWorking = true;
            while (_isWorking)
            {
                if (_rnd.NextDouble() <= _brakeChance && !IsBroken)
                {
                    IsBroken = true;
                    Broken?.Invoke(this);
                    _hpTimer = new Timer(TimeSpan.FromSeconds(HP).TotalMilliseconds);
                    _hpTimer.Elapsed += (o, e) =>
                    {
                        Dead?.Invoke(this);
                        StopWork();
                        _hpTimer?.Stop();
                    };
                    _hpTimer.Start();
                    return;
                }
                if (!IsBroken && Tank.CurrentVolume < Tank.Capacity)
                   Tank.AddOil(1);
                Thread.Sleep(1000);
            }
        }

        public void StopWork()
        {
            _isWorking = false;
        }

        public override void Fix(TimeSpan time)
        {
            _hpTimer.Stop();
            Thread.Sleep(time);
            IsBroken = false;
            Fixed?.Invoke(this);
        }
    }
}
