using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libTask3
{
    class Comedy : DomesticMovie
    {
        public List<object> Team { get; set; }
        public int Rating { get; set; }

        public Comedy(string name, string director, double duration) : base(name, director, duration)
        {
        }

        public double GetRatingPerPerson() => Rating / Team.Count;

        public void GoToEnd()
        {
            CurrentPosition = Duration;
        }
    }
}
