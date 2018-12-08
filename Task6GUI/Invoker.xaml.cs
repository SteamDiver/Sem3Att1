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
    /// Логика взаимодействия для Invoker.xaml
    /// </summary>
    public partial class Invoker : Window
    {
        public MethodInfo Method { get; }
        public object[] Params = null;

        public Invoker(MethodInfo method)
        {
            Method = method;
            InitializeComponent();
            Params = new object[method.GetParameters().Length];
        }
    }
}
