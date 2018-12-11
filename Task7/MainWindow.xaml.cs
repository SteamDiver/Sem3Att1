using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
using DataController;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Representations;

namespace Task7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RepresentController RoCRepresentController { get; set; }
        public RepresentController CandleRepresentController { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            IncomingDataController controller =
                new IncomingDataController(new ExcelDataProvider(new FileInfo(@"Sources\prices.xlsx")));
            controller.StartReceive(1000);
            controller.DataReceived += Controller_DataReceived;

            RoCRepresentController = new RoCController(20);
            CandleRepresentController = new CandleRepresentController(20);
            DataContext = this;
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
            get => Dispatcher.Invoke(() => (Brush) GetValue(OpenProperty));
            set => Dispatcher.Invoke(() => SetValue(ChartColorProperty, value));
        }

        public decimal OpenValue
        {
            get => Dispatcher.Invoke(() => (decimal) GetValue(OpenProperty));
            set => Dispatcher.Invoke(() => SetValue(OpenProperty, value));
        }

        public decimal CloseValue
        {
            get => Dispatcher.Invoke(() => (decimal) GetValue(CloseProperty));
            set => Dispatcher.Invoke(() => SetValue(CloseProperty, value));
        }

        public decimal HighValue
        {
            get => Dispatcher.Invoke(() => (decimal) GetValue(HighProperty));
            set => Dispatcher.Invoke(() => SetValue(HighProperty, value));
        }

        public decimal LowValue
        {
            get => Dispatcher.Invoke(() => (decimal) GetValue(LowProperty));
            set => Dispatcher.Invoke(() => SetValue(LowProperty, value));
        }

        private void Controller_DataReceived(Data.Interfaces.ICandle data)
        {
            if (data != null)
            {
                OpenValue = data.Open;
                CloseValue = data.Close;
                HighValue = data.High;
                LowValue = data.Low;
                LastTime = data.Time.ToString();
                RoCRepresentController.AddValueToLine(data, (x) => x.Close);
                CandleRepresentController.AddValueToLine(data, (x)=>x.Close);
            }
        }

        public string LastTime
        {
            get => Dispatcher.Invoke(() => (string) GetValue(LastTimeProperty));
            set => Dispatcher.Invoke(() => this.SetValue(LastTimeProperty, value));
        }
    }
}