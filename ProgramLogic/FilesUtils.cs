using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProgramLogic
{
    public class FilesUtils
    {
        public static List<TV> ReadTVFromFile(string path)
        {
            List<TV> TVList = new List<TV>();
            
            string[] fileLines = File.ReadAllLines(path, Encoding.Default);

            foreach (string line in fileLines) {
                string[] parts = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                TV tv = null;
                string company;
                double diagonal, power;
                switch (parts[0])
                {
                    case "TV":
                        company = parts[1];
                        diagonal = double.Parse(parts[2]);
                        power = double.Parse(parts[3]);
                        tv = new TV(company, diagonal, power);
                        break;
                    case "TV_P":
                        company = parts[1];
                        diagonal = double.Parse(parts[2]);
                        power = double.Parse(parts[3]);
                        string country = parts[4];
                        tv = new TV_P(company, diagonal, power, country);
                        break;
                }  
                if(tv!=null)
                    TVList.Add(tv);
            }
            return TVList;
        }

        public static void SaveTVToFile(string path, List<TV_P> tvs)
        {
            List<string> lines = new List<string>();

            foreach (TV_P tv in tvs)
            {
                lines.Add(tv.GetType().Name + ":" + tv.Company + ":" + tv.Diagonal + ":"+ tv.Power + ":" + tv.Country);
            }

            File.WriteAllLines(path, lines);
        }
    }
}
