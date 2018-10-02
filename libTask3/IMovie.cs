using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libTask3
{
    public interface IMovie
    {
        string Name { get; set; }

        void Start();

        void Stop();
    }
}