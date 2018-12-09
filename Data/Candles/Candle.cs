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
        }

        public Candle(long timeStamp, decimal high, decimal low, decimal open, decimal close)
        {
            TimeStamp = timeStamp;
            High = high;
            Low = low;
            Open = open;
            Close = close;
        }

        public long TimeStamp { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public DateTime Time { get; set; }
    }
}