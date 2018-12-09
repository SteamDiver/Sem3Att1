using Data.Candles;
using Data.Interfaces;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Representations
{
    public abstract class RepresentController : INotifyPropertyChanged
    {
        public SeriesCollection SeriesCollection { get; set; } = new SeriesCollection
            {
                new LineSeries
                {
                    AreaLimit = -10,
                    Values = new ChartValues<ObservableValue>(),
                }
            };

        public ICandle LastCandle { get; protected set; }
        public int MaxPointsCount { get; set; }

        public RepresentController(int maxCount) { MaxPointsCount = maxCount; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public abstract void AddValueToLine(ICandle candle, Func<ICandle, decimal> func);
    }
}
