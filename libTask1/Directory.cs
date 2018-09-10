using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using libTask1.Annotations;

namespace libTask1
{
    public class Directory
    {
        public string Path { get; }
        public List<IFile> Files { get; } = new List<IFile>();

        public Directory(string path)
        {
            Path = path;
        }

        public bool FileExists(IFile file)
        {
            return Files.Contains(file);
        }

        public void AddFile(IFile file)
        {
            if(!Files.Contains(file))
                Files.Add(file);
            else
            {
                throw new FileExistException();
            }
        }

        public void DeleteFile(IFile file)
        {
            if (Files.Contains(file))
            {
                Files.Remove(file);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}
