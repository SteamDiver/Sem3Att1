using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libTask3
{
    public class Comedy : DomesticMovie
    {
        public Country Country { get; set; }
        public int Rating { get; set; }

        public Comedy(string name, string director, double duration, Country country) : base(name, director, duration)
        {
            Country = country;
        }

        public void GoToStart()
        {
            CurrentPosition = 0;
        }
        public void GoToEnd()
        {
            CurrentPosition = Duration;
        }
    }
}
