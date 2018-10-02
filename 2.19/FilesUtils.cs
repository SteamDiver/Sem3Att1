using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace libTask2
{
    public class FilesUtils
    {
        public static List<Programmer> ReadFromFile(string path)
        {
            List<Programmer> prList = new List<Programmer>();
            
            string[] fileLines = File.ReadAllLines(path, Encoding.Default);

            foreach (string line in fileLines) {
                string[] parts = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                Programmer pr = null;
                string surname;
                int progCount, langCount;
                switch (parts[0])
                {
                    case "Programmer":
                        surname = parts[1];
                        progCount = int.Parse(parts[2]);
                        langCount = int.Parse(parts[3]);
                        pr = new Programmer(surname, progCount, langCount);
                        break;
                    case "CustomProgrammer":
                        surname = parts[1];
                        progCount = int.Parse(parts[2]);
                        langCount = int.Parse(parts[3]);
                        int successProgs = int.Parse(parts[4]);
                        pr = new CustomProgrammer(surname, progCount, langCount, successProgs);
                        break;
                }  
                if(pr!=null)
                    prList.Add(pr);
            }
            return prList;
        }

        public static void SaveToFile(string path, List<Programmer> tvs)
        {
            List<string> lines = new List<string>();

            foreach (var tv in tvs)
            {
                var p = tv.GetType() == typeof(CustomProgrammer) ? ((CustomProgrammer) tv).P.ToString() : "";
                lines.Add(tv.GetType().Name + ":" + tv.Surname + ":" + tv.ProgramCount + ":"+ tv.LangCount + ":" + p);
            }

            File.WriteAllLines(path, lines);
        }
    }
}
