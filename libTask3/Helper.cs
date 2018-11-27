using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libTask3
{
    public static class Helper
    {
        public static IMovie GetRandom()
        {
            Random rnd = new Random();
            var names = Enum.GetNames(typeof(Names));
            var countries = Enum.GetValues(typeof(Country));
            var directors = Enum.GetNames(typeof(Directors));

            var name = (string)names.GetValue(rnd.Next(names.Length));
            var country = (Country)countries.GetValue(rnd.Next(countries.Length));
            var director = (string)directors.GetValue(rnd.Next(directors.Length));
            var duration = rnd.Next(60 * 60, 180 * 60);

            var comedy = new Comedy(name, director, duration, country) { Rating = rnd.Next(10) };
            return comedy;
        }
    }
}
