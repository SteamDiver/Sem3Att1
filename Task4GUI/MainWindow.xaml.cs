using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using libTask4;
using libVisual;
using libVisual.Elements;
using XamlAnimatedGif;

namespace Task4GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<PumpStationUI> pumpList = new List<PumpStationUI>();
        List<MechanicUI> mechanicks = new List<MechanicUI>();
        private VisualElementsFactory _uiFactory = new VisualElementsFactory(SynchronizationContext.Current);
        public MainWindow()
        {
            InitializeComponent();
        }

        private int _counter;
        private void AddPumpBtn_Click(object sender, RoutedEventArgs e)
        {
            PumpStation st = new PumpStation();
            Thread th = new Thread(st.StartWork);

            var pumpStation = _uiFactory.CreatePumpStationUI(_counter++, st);
            PumpsCanv.Children.Add(pumpStation.Image);
            pumpList.Add(pumpStation);

            st.Broken += St_Broken;
            th.Start();
        }

        private void St_Broken(PumpStation sender)
        {
            var m = mechanicks.Find(x => !x.LogicObj.IsBusy);
            if (m != null)
            {
                new Task(() => m.LogicObj.FixObject(sender)).Start();
                var uiObject = pumpList.Find(x => x.LogicObj.Equals(sender));
                m.MoveTo(uiObject);
            }

        }

        private void AddMechanic_Click(object sender, RoutedEventArgs e)
        {
            var m = new Mechanic();
            MechanicUI mec = new MechanicUI(m, SynchronizationContext.Current);
            m.IsFree += M_IsFree;
            mechanicks.Add(mec);
            PumpsCanv.Children.Add(mec.Image);
        }

        private void M_IsFree(Mechanic sender)
        {
            var needToRepair = pumpList.Find(x => x.Station.IsBroken);
            if (needToRepair != null && !needToRepair.Station.IsFixing)
            {
                new Task(() => sender.FixObject(needToRepair.Station)).Start();
                var m = mechanicks.Find(x => x.LogicObj.Equals(sender));
                m.MoveTo(needToRepair);
            }

        }
    }
}
