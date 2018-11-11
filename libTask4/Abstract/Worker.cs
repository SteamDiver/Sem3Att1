using System;
using libTask4.Interfaces;

namespace libTask4.Abstract
{
    public abstract class Worker : ISupport
    {
        public abstract void DoWork(Item target);
    }
}
