using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
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

        BufferBlock<MechanicUI> mechanicks = new BufferBlock<MechanicUI>();

        private OilTankUI tank;
        private CarTankerUI car;
        private VisualElementsFactory _uiFactory = new VisualElementsFactory(SynchronizationContext.Current);

        public MainWindow()
        {
            InitializeComponent();
            InitOilTank();
            InitCarTank();
            Thread fixFactoryThread = new Thread(FixFactory);
            fixFactoryThread.Start();
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
            ThreadPool.QueueUserWorkItem(FixFactory, sender);
        }

        private async void FixFactory(object o)
        {
            while (true)
            {
                if (pumpList.Count == 0)
                    return;

                if (mechanicks.Count > 0)
                {
                    var st = o as PumpStation;
                    var station = pumpList.Find(x => x.LogicObj.Equals(st));

                    var m = await mechanicks.ReceiveAsync();

                    if (m != null && station != null)
                    {
                        m.MoveTo(station, new Uri("pack://application:,,,/Resources/mechanic_run.gif"));
                        Thread.Sleep(1500);
                        m.DoWork(station.LogicObj);
                        mechanicks.Post(m);
                    }

                    return;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }

        private void AddMechanic_Click(object sender, RoutedEventArgs e)
        {
            MechanicUI mec = new MechanicUI(SynchronizationContext.Current);
            mechanicks.Post(mec);
            PumpsCanv.Children.Add(mec.VisualElement);
        }



        private void InitOilTank()
        {
            tank = _uiFactory.CreateOilTankUI(TankStatusPb);
            tank.LogicObj.IsFull += TankObj_IsFull;
            tank.LogicObj.IsEmpty += LogicObj_IsEmpty;
            tank.LogicObj.Added += LogicObj_Added;
        }

        private void LogicObj_Added(OilTank sender)
        {
            var percentage = tank.LogicObj.CurrentVolume / tank.LogicObj.Capacity;
            if (percentage >= 50)
            {
                //tank.LogicObj.Get();
                //car.MoveTo(new Thickness(600, 500, 0, 0), new Uri("pack://application:,,,/Resources/carTanker.png"));
            }
        }

        private void LogicObj_IsEmpty(OilTank sender)
        {
            car.MoveTo(new Thickness(-200, 500, 0, 0), new Uri("pack://application:,,,/Resources/carTanker.png"));
        }

        private void TankObj_IsFull(OilTank sender)
        {
            Dispatcher.Invoke(() =>
            {
                pumpList.ForEach(p => p.Stop());
                var fire = _uiFactory.GetFire(MainCanvas.ActualWidth, MainCanvas.ActualHeight);
                Canvas.SetZIndex(fire, 2);
                MainCanvas.Children.Add(fire);

            });
        }

        private void InitCarTank()
        {
            car = _uiFactory.CreateCarTankUI();
            MainCanvas.Children.Add(car.VisualElement);
        }
    }
}
