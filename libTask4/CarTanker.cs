using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using libTask4.Abstract;
using libTask4.Interfaces;

namespace libTask4
{
    public class CarTanker : Service
    {
        public override void DoWork(Item target)
        {
            if (target is OilTank)
            {
                Thread.Sleep(5000);
                (target as OilTank).Get();
            }
        }
    }
}
