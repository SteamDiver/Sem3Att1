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
        private OilTankUI tank;
        //private CarTankerUI car;
        private VisualElementsFactory _uiFactory = new VisualElementsFactory(SynchronizationContext.Current);
        public MainWindow()
        {
           InitializeComponent();
           InitOilTank();
            InitCarTank();
        }

        private void AddPumpBtn_Click(object sender, RoutedEventArgs e)
        {
            var pumpStation = _uiFactory.CreatePumpStationUI(pumpList.Count);
            pumpStation.LinkToTank(tank);
            pumpStation.LogicObj.Broken += St_Broken;
            pumpStation.Start();
            PumpsCanv.Children.Add(pumpStation.VisualElement);
            pumpList.Add(pumpStation);
        }

        private void St_Broken(PumpStation sender)
        {
            var m = mechanicks.Find(x => !x.LogicObj.IsBusy);
            if (m != null)
            {
                new Task(() => m.LogicObj.DoWork(sender)).Start();
                var uiObject = pumpList.Find(x => x.LogicObj.Equals(sender));
                m.MoveTo(uiObject);
            }

        }

        private void AddMechanic_Click(object sender, RoutedEventArgs e)
        {
            MechanicUI mec = new MechanicUI(SynchronizationContext.Current);
            mec.LogicObj.IsFree += M_IsFree;
            mechanicks.Add(mec);
            PumpsCanv.Children.Add(mec.VisualElement);
            M_IsFree(mec.LogicObj);
        }

        private void M_IsFree(Mechanic sender)
        {
            var needToRepair = pumpList.Find(x => x.LogicObj.IsBroken);
            if (needToRepair != null && !needToRepair.LogicObj.IsFixing)
            {
                var m = mechanicks.Find(x => x.LogicObj.Equals(sender));
                m.MoveTo(needToRepair);
                new Task(() => sender.DoWork(needToRepair.LogicObj)).Start();
            }

        }

        private void InitOilTank()
        {
            tank = _uiFactory.CreateOilTankUI(TankStatusPb);
            //tank.LogicObj.IsFull += TankObj_IsFull;
        }

        private void InitCarTank()
        {
            //car = _uiFactory.CreateCarTankUI();
            //car.LinkToTanks(new List<OilTankUI>(){tank});
        }

        //private void TankObj_IsFull(OilTank sender)
        //{
        //    car?.DoWork(sender);
        //}
    }
}
