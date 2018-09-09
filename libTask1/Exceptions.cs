using System;

namespace libTask1
{
    public class FileExistException : Exception
    {
        public override string Message => "Файл уже существует";
    }

    public class DirectoryExistException : Exception
    {
        public override string Message => "Директория уже существует";
    }
}