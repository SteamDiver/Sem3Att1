using System;
using Data.Interfaces;

namespace Data.Candles
{
    public class Candle : ICandle
    {
        public Candle(decimal high, decimal low, decimal open, decimal close, DateTime time)
        {
            High = high;
            Low = low;
            Open = open;
            Close = close;
            Time = time;
            TimeStamp = (long) Time.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public Candle(long timeStamp, decimal high, decimal low, decimal open, decimal close)
        {
            TimeStamp = timeStamp;
            High = high;
            Low = low;
            Open = open;
            Close = close;
            Time = new DateTime(1970, 1, 1).AddSeconds(timeStamp);
        }

        public long TimeStamp { get; private set; }
        public decimal High { get; private set; }
        public decimal Low { get; private set; }
        public decimal Open { get; private set; }
        public decimal Close { get; private set; }
        public DateTime Time { get; private set; }
    }
}