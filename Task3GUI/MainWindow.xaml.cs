using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using libTask3;

namespace Task3GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<IMovie> Movies { get; set; } = new List<IMovie>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            Movies.Add(Helper.GetRandom());
            UpdateItemsSource();
        }

        private void UpdateItemsSource()
        {
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = Movies;
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            var row = DataGrid.SelectedIndex;
            if (row != -1)
            {
                Timer t = new Timer(1000);
                t.Elapsed += Timer_Tick;
                t.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ((Comedy)Movies[0]).CurrentPosition += 1;
            UpdateItemsSource();
        }
    }
}