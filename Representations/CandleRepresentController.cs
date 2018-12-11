using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Representations
{
    public class CandleRepresentController : RepresentController
    {
        public CandleRepresentController(int maxCount) : base(maxCount)
        {
            SeriesCollection = new SeriesCollection{
                new CandleSeries()
                {
                    Values = new ChartValues<OhlcPoint>(),
                }
            };
        }

        public override void AddValueToLine(ICandle candle, Func<ICandle, decimal> func = null)
        {
            var lineValues = SeriesCollection[0].Values;
            
            lineValues.Add(new OhlcPoint((double)candle.Open, (double)candle.High, (double)candle.Low, (double)candle.Close));

            while (lineValues.Count > MaxPointsCount)
            {
                lineValues.RemoveAt(0);
            }
        }
    }
}
