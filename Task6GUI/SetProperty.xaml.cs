using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Task6GUI
{
    /// <summary>
    /// Логика взаимодействия для SetProperty.xaml
    /// </summary>
    public partial class SetProperty : Window
    {
        public PropertyInfo Property { get; }
        public object Obj { get; }

        public SetProperty(PropertyInfo property, object obj)
        {
            Property = property;
            Obj = obj;
            InitializeComponent();

            CurValueLbl.Content = Property.GetValue(Obj).ToString();
        }

        private void SetValueBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var value = Convert.ChangeType(NewValeTb.Text, Property.PropertyType);
                Property.SetValue(Obj, value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Close();
            }
        }
    }
}
