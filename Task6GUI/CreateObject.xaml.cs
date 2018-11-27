using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using libReflection;

namespace Task6GUI
{
    /// <summary>
    /// Логика взаимодействия для CreateObject.xaml
    /// </summary>
    public partial class CreateObject : Window
    {
        private readonly List<UIElement> _inputs = new List<UIElement>();

        public CreateObject(TypeHelper typeHelper)
        {
            InitializeComponent();
            ConstructorsCB.ItemsSource = typeHelper.Constructors;
        }

        private void ConstructorsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InitInputs((ConstructorInfo) ConstructorsCB.SelectedValue);

        }

        private void InitInputs(ConstructorInfo cInfo)
        {
            _inputs.Clear();
            StackPanel.Children.Clear();

            foreach (var parameter in cInfo.GetParameters())
            {
                _inputs.Add(new Label() { Content = $"{parameter.Name} ({parameter.ParameterType})" });
                if (!parameter.ParameterType.IsEnum)
                {
                    _inputs.Add(new TextBox() { Name = parameter.Name });
                }
                else if(parameter.ParameterType.IsEnum)
                {
                    var type = parameter.ParameterType;
                    _inputs.Add(new ComboBox() { Name = parameter.Name, ItemsSource = type.GetEnumValues() });
                }
                else
                {
                    MessageBox.Show("I will not create this object. Too hard((");
                    return;
                }
            }

            _inputs.ForEach(i => StackPanel.Children.Add(i));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ctor = ConstructorsCB.SelectedValue as ConstructorInfo;
            List<object> args = new List<object>();
            if (ctor != null)
            {
                var prms = ctor.GetParameters();

                int i = 0;
                foreach (var input in _inputs)
                {
                    if (input.GetType() != typeof(Label))
                    {
                        if (input.GetType() == typeof(TextBox))
                        {
                            var t = prms[i].ParameterType;
                            args.Add(Convert.ChangeType(((TextBox)input).Text, t));
                        }

                        if (input.GetType() == typeof(ComboBox))
                        {
                            args.Add(((ComboBox)input).SelectedValue);
                        }

                        i++;
                    }
                }
            }

            var obj = Convert.ChangeType(ctor.Invoke(args.ToArray()), ctor.DeclaringType ?? throw new InvalidOperationException());
            MainWindow.Objects.Add(obj);
        }
    }
}
