using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using libTask1;

namespace Task1GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateFileBtn_Click(object sender, RoutedEventArgs e)
        {
            var newFileWindow = new NewFile();
            if (newFileWindow.ShowDialog() == true)
            {
                RefillDataSource();
            }
        }

        private void RefillDataSource()
        {
            Directories.ItemsSource = null;
            Directories.ItemsSource = FileSystem.Directories;
        }

        private void Directories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Directories.SelectedValue != null)
            {
                var dirName = ((Directory) Directories.SelectedValue).Path;
                var files = FileSystem.FindDerectory(dirName).Files;
                Files.ItemsSource = files;
                FileContent.Text = "";
            }
        }

        private void Files_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Files.SelectedValue is TextFile file)
                FileContent.Text = file.GetContent();
        }

        private void AppendBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Files.SelectedValue is TextFile file)
                file.Append(FileContent.Text);
        }

        private void DeleteFile_Click(object sender, RoutedEventArgs e)
        {
            if (Files.SelectedValue is TextFile file)
                try
                {
                    file.Delete();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
    }
}
