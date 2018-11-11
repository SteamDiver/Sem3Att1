using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libTask4.Interfaces
{
    public abstract class Item
    {
        public bool IsBroken { get; set; }

        public abstract void Fix(TimeSpan time);
    }
}
