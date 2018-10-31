﻿using System;
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
        public delegate void OilTankEventHandler();

        public event OilTankEventHandler IsFull;
        public event OilTankEventHandler IsEmpty;
        public event OilTankEventHandler Added;

        public OilTank(double capacity)
        {
            Capacity = capacity;
        }

        public void AddOil(double amount, out double extra)
        {
            extra = 0;
            Added?.Invoke();
            if (CurrentVolume + amount > Capacity)
            {
                CurrentVolume += amount;
            }
            else
            {
                CurrentVolume = Capacity;
                extra = CurrentVolume + amount - Capacity;
                IsFull?.Invoke();
            }
        }

        public double GetAll()
        {
            CurrentVolume = 0;
            IsEmpty?.Invoke();

            return CurrentVolume;
        }

        public override void Fix()
        {
            throw new NotImplementedException();
        }
    }
}
