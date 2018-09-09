using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libTask1;

namespace Task1
{
    class Program
    {
        private static string _help = "\nТекстовый файл\n" +
                              "create [filename]\n" +
                              "rename [oldname] [newname]\n" +
                              "append [filename] [text]\n" +
                              "get [filename]\n" +
                              "delete [filename]\n";

        static void Main(string[] args)
        {
            Console.WriteLine(_help);
            while (true)
            {
                try
                {
                    var text = Console.ReadLine();
                    var splited = text?.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (splited != null && splited.Count > 1)
                    {
                        string name;
                        TextFile file;
                        switch (splited[0])
                        {
                            case "create":
                                new TextFile(splited[1]);
                                break;
                            case "rename":
                                var oldName = splited[1];
                                var newName = splited[2];
                                file = (TextFile)FileSystem.FindFile(oldName);
                                file.Rename(newName);
                                break;
                            case "append":
                                name = splited[1];
                                splited.RemoveRange(0, 2);
                                var str = String.Join(" ", splited);
                                file = (TextFile)FileSystem.FindFile(name);
                                file.Append(str);
                                break;
                            case "get":
                                name = splited[1];
                                file = (TextFile)FileSystem.FindFile(name);
                                Console.WriteLine(file.GetContent());
                                break;
                            case "delete":
                                name = splited[1];
                                file = (TextFile)FileSystem.FindFile(name);
                                file.Delete();
                                break;
                            default:
                                Console.WriteLine(_help);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine(_help);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
