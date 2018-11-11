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
        Queue<MechanicUI> mechanicks = new Queue<MechanicUI>();
        Queue<PumpStationUI> brokenStations = new Queue<PumpStationUI>();

        private OilTankUI tank;
        private CarTankerUI car;
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

            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri("pack://application:,,,/Resources/melkie_puzyrki.mp3"));
            player.Play();
        }

        private void St_Broken(PumpStation sender)
        {
            new Task(() =>
            {
                brokenStations.Enqueue(pumpList.Find(x => x.LogicObj.Equals(sender)));
                Fix();
            }).Start();
           
        }

        private void Fix()
        {
                while (brokenStations.Count > 0)
                {
                    var station = brokenStations.Dequeue();

                    if (mechanicks.Count > 0 && Mechanic.SemCount > 0)
                    {
                        try
                        {
                            var m = mechanicks.Dequeue();
                            if (m != null)
                            {
                                m.MoveTo(station, new Uri("pack://application:,,,/Resources/mechanic_run.gif"));
                                Thread.Sleep(2000);
                                m.DoWork(station.LogicObj);
                                mechanicks.Enqueue(m);
                            }
                        }
                        catch
                        {
                            //ignore
                        }
                    }
            }

        }

        private void AddMechanic_Click(object sender, RoutedEventArgs e)
        {
            MechanicUI mec = new MechanicUI(SynchronizationContext.Current);
            mechanicks.Enqueue(mec);
            PumpsCanv.Children.Add(mec.VisualElement);
        }


        private void InitOilTank()
        {
            tank = _uiFactory.CreateOilTankUI(TankStatusPb);
            tank.LogicObj.IsFull += TankObj_IsFull;
            tank.LogicObj.IsEmpty += LogicObj_IsEmpty;
        }

        private void LogicObj_IsEmpty(OilTank sender)
        {
            car.MoveTo(new Thickness(-200, 500, 0, 0), new Uri("pack://application:,,,/Resources/carTanker.png"));
        }

        private void TankObj_IsFull(OilTank sender)
        {
            car.MoveTo(new Thickness(600, 500, 0, 0), new Uri("pack://application:,,,/Resources/carTanker.png"));
        }

        private void InitCarTank()
        {
            car = _uiFactory.CreateCarTankUI();
            car.LinkToTanks(new List<OilTankUI>() { tank });
            MainCanvas.Children.Add(car.VisualElement);
        }
    }
}
