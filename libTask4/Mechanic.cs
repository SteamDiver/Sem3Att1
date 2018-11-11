using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using libTask4.Abstract;
using libTask4.Interfaces;

namespace libTask4
{
    public class Mechanic : Worker
    {
        public TimeSpan TimeToFix { get; private set; } = TimeSpan.FromSeconds(2);

        public delegate void MechanickEventHandler(Mechanic sender);
        public event MechanickEventHandler IsFree;

        public static Semaphore Semaphore = new Semaphore(2, 2);
        public static int SemCount { get; private set; } = 2;

        public override void DoWork(Item target)
        {
            FixObject(target);
        }

        private void FixObject(Item obj)
        {
            Semaphore.WaitOne();
            Thread.Sleep(TimeToFix);
            obj.Fix();
            SemCount = Semaphore.Release();
            IsFree?.Invoke(this);
        }
    }

}
