using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libTask3
{
    public class Drama : DomesticMovie
    {
        public Drama(string name, string director, int duration) : base(name, director, duration)
        {
        }

        public override void Forward(int seconds)
        {
            CurrentPosition += seconds;
        }
    }
}
