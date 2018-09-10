using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace libTask1
{
    public class File : IFile
    {
        private readonly List<byte> _contentBytes = new List<byte>();
        public string Name { get; set; }
        public string DirPath { get; set; }

        public File(string path)
        {
            Name = Path.GetFileName(path);
            DirPath = Path.GetDirectoryName(path);
            var dir = FileSystem.FindDerectory(DirPath);

            if (dir == null)
            {
                dir = new Directory(DirPath);
                FileSystem.AddDirectory(dir);
            }
            dir.AddFile(this);
        }

        public void Rename(string newName)
        {
            Name = newName;
        }

        public byte[] GetBytes()
        {
            return _contentBytes.ToArray();
        }

        public void AppendBytes(byte[] content)
        {
            _contentBytes.AddRange(content);
        }

        public void Delete()
        {
            var dir = FileSystem.FindDerectory(DirPath);
            if (dir != null)
                dir.DeleteFile(this);
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}
