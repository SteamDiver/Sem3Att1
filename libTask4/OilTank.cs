using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libTask4.Interfaces;

namespace libTask4
{
    public class OilTank : Item
    {
        public double Capacity { get; private set; }
        public double CurrentVolume { get; private set; }
        public delegate void OilTankEventHandler(OilTank sender);

        public event OilTankEventHandler IsFull;
        public event OilTankEventHandler IsEmpty;
        public event OilTankEventHandler Added;

        public OilTank(double capacity)
        {
            Capacity = capacity;
        }

        public void AddOil(double amount)
        {
            if (CurrentVolume + amount <= Capacity)
            {
                if (CurrentVolume + amount == Capacity)
                    IsFull?.Invoke(this);

                CurrentVolume += amount;
                Added?.Invoke(this);

            }
        }

        public void Get(double val)
        {
            if (val < CurrentVolume && CurrentVolume != 0)
                CurrentVolume -= val;
            else
            {
                CurrentVolume = 0;
                IsEmpty?.Invoke(this);
            }
        }

        public void Get()
        {
            CurrentVolume = 0;
            IsEmpty?.Invoke(this);
        }

        public override void Fix()
        {
            throw new NotImplementedException();
        }
    }
}
