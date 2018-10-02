using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libTask3
{
    public abstract class DomesticMovie : IMovie
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public double Duration { get; set; }
        public double CurrentPosition { get; set; }
        public bool IsStarted { get; set; }

        public DomesticMovie(string name, string director, double duration)
        {
            Name = name;
            Duration = duration;
            Director = director;
        }

        public void Start()
        {
            IsStarted = true;
        }

        public void Stop()
        {
            IsStarted = false;
        }

        public void Forward(double seconds)
        {
            if (seconds + CurrentPosition < Duration)
                CurrentPosition += seconds;
            else
            {
                throw new Exception("Невозможно перемотать");
            }
        }
    }
}