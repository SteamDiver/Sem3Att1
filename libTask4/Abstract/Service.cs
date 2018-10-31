using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libTask4.Interfaces;

namespace libTask4.Abstract
{
    public abstract class Service : ISupport
    {
        public abstract void DoWork(Item target);
    }
}
