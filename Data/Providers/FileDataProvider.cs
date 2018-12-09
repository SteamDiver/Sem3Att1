using System.Collections.Generic;
using System.IO;
using Data.Candles;
using Data.Interfaces;

namespace Data.Providers
{
    public abstract class FileDataProvider : DataProvider
    {
        public FileInfo Source { get; set; }
        protected Queue<ICandle> Candles { get; set; } = new Queue<ICandle>();

        public FileDataProvider(FileInfo source)
        {
            Source = source;
            ReadDataFromFile();
        }

        public override ICandle GetData()
        {
            if (Candles.Count > 0)
            {
                return Candles.Dequeue();
            }
            return null;
        }

        protected abstract void ReadDataFromFile();

    }
}
