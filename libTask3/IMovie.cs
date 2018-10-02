using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace libTask3
{
    public interface IMovie : INotifyPropertyChanged
    {
        string Name { get; set; }
        Timer T { get; }

        void Start();

        void Stop();
    }
}