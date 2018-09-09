using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace libTask1
{
    public class File : IFile
    {
        public List<byte> ContentBytes = new List<byte>();
        public string Name { get; set; }

        public File(string path)
        {
            Name = Path.GetFileName(path);
            var dirPath = Path.GetDirectoryName(path);
            var dir = FileSystem.Directories.Find(x => x.Path == dirPath);

            if (dir == null)
            {
                dir = new Directory(dirPath);
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
            return ContentBytes.ToArray();
        }

        public void AppendBytes(byte[] content)
        {
            ContentBytes.AddRange(content);
        }

        public void Delete()
        {
            var dir = FileSystem.FindDerectory(Path.GetDirectoryName(Name));
            if (dir != null)
                dir.DeleteFile(this);
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}
