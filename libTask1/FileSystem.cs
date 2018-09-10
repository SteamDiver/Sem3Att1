using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace libTask1
{
    public class FileSystem
    {
        public static List<Directory> Directories { get; } = new List<Directory>();

        public static Directory FindDerectory(string path)
        {
            return Directories.Find(x => x.Path == path);
        }

        public static void AddDirectory(Directory dir)
        {
            if(!Directories.Contains(dir))
                Directories.Add(dir);
            else
            {
                throw new DirectoryExistException();
            }
        }

        public static IFile FindFile(string fileName)
        {
            var directory = Path.GetDirectoryName(fileName);
            var file = Path.GetFileName(fileName);
            var dir = Directories.Find(x => x.Path == directory);
            var f = dir?.Files.Find(x => x.Name == file);
            if (f != null)
                return f;
            throw new FileNotFoundException();
        }
    }
}
