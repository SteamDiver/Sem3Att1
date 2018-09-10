using System.Collections.Generic;

namespace libTask1
{
    public interface IFile
    {
        string Name { get; set; }
        string DirPath { get; set; }

        void Rename(string newName);
        byte[] GetBytes();
        void AppendBytes(byte[] content);
        void Delete();
    }
}
