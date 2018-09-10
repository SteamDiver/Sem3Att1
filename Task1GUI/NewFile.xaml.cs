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
using System.Xml.Xsl;
using libTask1;

namespace Task1GUI
{
    /// <summary>
    /// Логика взаимодействия для NewFile.xaml
    /// </summary>
    public partial class NewFile : Window
    {
        public NewFile()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, FileName);
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new TextFile(FileName.Text);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void FileName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CreateBtn_Click(sender, e);
            }
        }
    }
}
