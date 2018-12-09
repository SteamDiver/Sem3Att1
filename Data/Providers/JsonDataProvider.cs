using System;
using System.IO;

namespace Data.Providers
{
    class JsonDataProvider : FileDataProvider
    {
        public JsonDataProvider(FileInfo source) : base(source)
        {
        }

        protected override void ReadDataFromFile()
        {
            throw new NotImplementedException();
        }
    }
}
