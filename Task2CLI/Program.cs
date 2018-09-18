using System;
using System.Collections.Generic;
using System.Linq;
using libTask2;

namespace Task2CLI
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter base programmer info (sername, progCount, langCount)");
                var str = Console.ReadLine();
                var chunks = GetChunks(str);
                string sername = chunks[0];
                var programCount = int.Parse(chunks[1]);
                var langCount = int.Parse(chunks[2]);
                var baseProgrammer = new Programmer(sername, programCount, langCount);

                Console.WriteLine("Enter base programmer info (sername, progCount, langCount, correctCount)");
                str = Console.ReadLine();
                chunks = GetChunks(str);
                sername = chunks[0];
                programCount = int.Parse(chunks[1]);
                langCount = int.Parse(chunks[2]);
                var p = int.Parse(chunks[3]);
                var customProgrammer = new CustomProgrammer(sername, programCount, langCount, p);

                Console.WriteLine(baseProgrammer.GetInfo());
                Console.WriteLine(customProgrammer.GetInfo());
            }

        }

        private static List<string> GetChunks(string input)
        {
            return input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}