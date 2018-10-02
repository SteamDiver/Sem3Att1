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

        private Timer T= new Timer(1000);

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
            var selectedIndex = DataGrid.SelectedIndex;
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = Movies;
            DataGrid.SelectedIndex = selectedIndex;
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            var row = DataGrid.SelectedIndex;
            if (row != -1)
            {
                Movies[row].Start();
            }
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            var row = DataGrid.SelectedIndex;
            if (row != -1)
            {
                Movies[row].Stop();
            }
        }

        private void ForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            var row = DataGrid.SelectedIndex;
            if (row != -1)
            {
                var seconds = int.Parse(ForwardTb.Text);
                ((DomesticMovie)Movies[row]).Forward(seconds);
            }
        }
    }
}