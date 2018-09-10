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
using System.Windows.Shapes;
using libTask1;

namespace Task1GUI
{
    /// <summary>
    /// Логика взаимодействия для RenameFile.xaml
    /// </summary>
    public partial class RenameFile : Window
    {
        private IFile _file;
        public RenameFile(IFile file)
        {
            InitializeComponent();
            _file = file;
            NewNameTb.Text = _file.Name;
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NewNameTb.Text != "")
            {
                _file.Rename(NewNameTb.Text);
                Close();
            }
            else
            {
                MessageBox.Show("Empty name", "Error", MessageBoxButton.OK);
            }
        }
    }
}
