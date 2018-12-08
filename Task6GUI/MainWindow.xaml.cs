using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using libReflection;
using Microsoft.Win32;

namespace Task6GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileInfo _file;
        private ReflectionHelper _helper;
        private TypeHelper _typeInfo;
        public static List<object> Objects { get; set; } = new List<object>();

        public MainWindow()
        {
            InitializeComponent();
            DataGrid.ItemsSource = Objects;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == true)
                {
                    _file = new FileInfo(ofd.FileName);
                    FileTb.Text = _file.FullName;
                    _helper = new ReflectionHelper(_file.FullName);
                    FillInfo(_helper);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillInfo(ReflectionHelper helper)
        {
            if (helper != null)
            {
               ClassesLstB.ItemsSource = _helper.Types.Where(c => c.IsPublic && !c.IsSealed).Select(x => x.Name);
            }
        }

        private void ClassesLstB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClassesLstB.SelectedValue != null)
            {
                ClearListBoxes();
                var type = _helper.Types.Find(t => t.Name == ClassesLstB.SelectedValue.ToString());
                _typeInfo = new TypeHelper(type);
                PropertiesLstB.ItemsSource = _typeInfo.Properties;
                MethodsLstB.ItemsSource = _typeInfo.Methods.Where(m => m.IsSpecialName == false);
                ConstructorsLstB.ItemsSource = _typeInfo.Constructors.Select(TypeHelper.ConstructorInfoToString);
            }
        }

        private void ClearListBoxes()
        {
            PropertiesLstB.ItemsSource = null;
            MethodsLstB.ItemsSource = null;
            ConstructorsLstB.ItemsSource = null;
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_typeInfo != null && !_typeInfo.Type.IsAbstract)
            {
                    CreateObject createWindow = new CreateObject(_typeInfo);
                    createWindow.ShowDialog();
                    DataGrid.ItemsSource = Objects;
                DataGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show("You can not create object of this type");
            }
        }

        private void MethodsLstB_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var obj = DataGrid.SelectedItem;
            if (obj != null)
            {
                var method = (MethodInfo)MethodsLstB.SelectedItem;
                object[] param = null;

                //if (method.GetParameters().Length > 0)
                //{
                //    var invokerWindow = new Invoker(method);
                //}

                if (method.ReturnType == typeof(void))
                {
                    method.Invoke(obj, param);
                }
                else
                {
                    MessageBox.Show(method.Invoke(obj, param).ToString());
                }
            }
        }

        private void PropertiesLstB_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var obj = DataGrid.SelectedItem;
            if (obj != null)
            {
                var property = (PropertyInfo)PropertiesLstB.SelectedItem;
                var propWindow = new SetProperty(property, obj);
                propWindow.ShowDialog();
                DataGrid.Items.Refresh();
            }
        }
    }
}
