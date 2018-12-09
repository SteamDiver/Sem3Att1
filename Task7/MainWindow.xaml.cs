using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Data.Candles;
using Data.Providers;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Task7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();

            LastHourSeries = new SeriesCollection
            {
                new LineSeries
                {
                    AreaLimit = -10,
                    Values = new ChartValues<ObservableValue>
                    {
                        
                    }
                }
            };

            DataController.DataController controller = new DataController.DataController(new ExcelDataProvider(new FileInfo(@"Sources\prices.xlsx")));
            controller.StartReceive(1000);
            controller.DataReceived += Controller_DataReceived;
            DataContext = this;
        }

        private decimal lastValue;
        private void Controller_DataReceived(Data.Interfaces.ICandle data)
        {
            if (data != null)
            {
                var values = LastHourSeries[0].Values;
                OpenValue = ((Candle) data).Open;
                CloseValue = ((Candle) data).Close;
                HighValue = ((Candle) data).High;
                LowValue = ((Candle) data).Low;

                LastTime = ((Candle) data).Time.ToString();

                if (lastValue != 0m)
                {
                    var value = (OpenValue / lastValue - 1) * 100;
                    ChartColor = value < 0 ? Brushes.IndianRed : Brushes.LightGreen;
                    values.Add(new ObservableValue((double) value));
                }

                lastValue = OpenValue;
                if (values.Count == 20)
                    values.RemoveAt(0);
            }
        }

        public SeriesCollection LastHourSeries { get; set; }

        public string LastTime {
            get => Dispatcher.Invoke(() => (string)GetValue(LastTimeProperty));
            set => Dispatcher.Invoke(() => this.SetValue(LastTimeProperty, value));
        }

        public static readonly DependencyProperty ChartColorProperty =
            DependencyProperty.Register("ChartColor", typeof(Brush), typeof(MainWindow));
        public static readonly DependencyProperty LastTimeProperty =
            DependencyProperty.Register("LastTime", typeof(string), typeof(MainWindow));
        public static readonly DependencyProperty OpenProperty =
            DependencyProperty.Register("OpenValue", typeof(decimal), typeof(MainWindow));
        public static readonly DependencyProperty CloseProperty =
            DependencyProperty.Register("CloseValue", typeof(decimal), typeof(MainWindow));
        public static readonly DependencyProperty HighProperty =
            DependencyProperty.Register("HighValue", typeof(decimal), typeof(MainWindow));
        public static readonly DependencyProperty LowProperty =
            DependencyProperty.Register("LowValue", typeof(decimal), typeof(MainWindow));

        public Brush ChartColor
        {
            get => Dispatcher.Invoke(() => (Brush)GetValue(OpenProperty));
            set => Dispatcher.Invoke(() => this.SetValue(ChartColorProperty, value));
        }

        public decimal OpenValue
        {
            get => Dispatcher.Invoke(() => (decimal)GetValue(OpenProperty));
            set => Dispatcher.Invoke(() => this.SetValue(OpenProperty, value));
        }
        public decimal CloseValue
        {
            get => Dispatcher.Invoke(() => (decimal)GetValue(CloseProperty));
            set => Dispatcher.Invoke(() => this.SetValue(CloseProperty, value));
        }
        public decimal HighValue
        {
            get => Dispatcher.Invoke(() => (decimal)GetValue(HighProperty));
            set => Dispatcher.Invoke(() => this.SetValue(HighProperty, value));
        }
        public decimal LowValue
        {
            get => Dispatcher.Invoke(() => (decimal)GetValue(LowProperty));
            set => Dispatcher.Invoke(() => this.SetValue(LowProperty, value));
        }
    }
}
