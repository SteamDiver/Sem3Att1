using System;

namespace Data.Interfaces
{
    public interface ICandle
    {
        long TimeStamp { get; }
        decimal High { get; }
        decimal Low { get; }
        decimal Open { get; }
        decimal Close { get; }
        DateTime Time { get; }
    }
}
