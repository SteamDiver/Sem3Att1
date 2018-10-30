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
        public double Capacity { get; set; }
        public double CurrentVolume { get; set; }
        public delegate void OilTankEventHandler();

        public event OilTankEventHandler IsFull;
        public event OilTankEventHandler IsEmpty;

        public OilTank(double capacity)
        {
            Capacity = capacity;
        }

        public void AddOil(double amount, out double extra)
        {
            extra = 0;
            if (CurrentVolume + amount > Capacity)
            {
                CurrentVolume += amount;
            }
            else
            {
                extra = CurrentVolume + amount - Capacity;
                IsFull?.Invoke();
            }
        }

        public double GetAll()
        {
            IsEmpty?.Invoke();
            CurrentVolume = 0;
            return CurrentVolume;
        }

        public override void Fix()
        {
            throw new NotImplementedException();
        }
    }
}
