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
        public TimeSpan TimeToFix { get; private set; } = TimeSpan.FromSeconds(5);

        public delegate void MechanickEventHandler(Mechanic sender);
        public event MechanickEventHandler IsFree;

        public override void DoWork(Item target)
        {
            FixObject(target);
        }

        private void FixObject(Item obj)
        {
            lock (obj)
            {
                obj.IsFixing = true;
                IsBusy = true;
                Thread.Sleep(TimeToFix);
                obj.Fix();
                obj.IsFixing = false;
                IsBusy = false;
                OnFree();
            }
        }

        protected virtual void OnFree()
        {
            IsFree?.Invoke(this);
        }
    }

}
