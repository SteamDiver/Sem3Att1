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
using libTask4.Interfaces;
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
        private readonly List<PumpStationUI> _pumpList = new List<PumpStationUI>();
        private readonly BufferBlock<MechanicUI> _mechanicks = new BufferBlock<MechanicUI>();
        private OilTankUI _tank;
        private CarTankerUI _car;
        private readonly VisualElementsFactory _uiFactory = new VisualElementsFactory(SynchronizationContext.Current);

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
            var pumpStation = _uiFactory.CreatePumpStationUI(_pumpList.Count);
            pumpStation.LinkToTank(_tank);
            pumpStation.LogicObj.Broken += St_Broken;
            pumpStation.LogicObj.Dead += St_Dead;
            pumpStation.Start();
            PumpsCanv.Children.Add(pumpStation.VisualElement);
            _pumpList.Add(pumpStation);

            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri("pack://application:,,,/Resources/melkie_puzyrki.mp3"));
            player.Play();
        }

        private void St_Dead(PumpStation sender)
        {
            var ui = _pumpList.Find(x => x.LogicObj.Equals(sender));
            if (ui != null)
            {
                _pumpList.Remove(ui);
                Dispatcher.Invoke(() =>
                {
                    PumpsCanv.Children.Remove(ui.VisualElement);
                    PumpsCanv.Children.Add(_uiFactory.GetExplosion(((Image)ui.VisualElement).Margin));
                });
            }

        }

        private void St_Broken(PumpStation sender)
        {
            ThreadPool.QueueUserWorkItem(FixFactory, sender);
        }

        private async void FixFactory(object o)
        {
            while (true)
            {
                if (_pumpList.Count == 0)
                    return;

                if (_mechanicks.Count > 0)
                {
                    var st = o as PumpStation;
                    var station = _pumpList.Find(x => x.LogicObj.Equals(st));

                    var m = await _mechanicks.ReceiveAsync();

                    if (m != null && station != null)
                    {
                        m.MoveTo(station, new Uri("pack://application:,,,/Resources/mechanic_run.gif"));
                        Thread.Sleep(1500);
                        
                        //если еще не рвануло
                        if (_pumpList.Contains(station)){
                            m.DoWork(station.LogicObj);
                        } 
                    }
                    _mechanicks.Post(m);
                    return;
                }
                Thread.Sleep(1000);
            }
        }

        private void AddMechanic_Click(object sender, RoutedEventArgs e)
        {
            MechanicUI mec = new MechanicUI(SynchronizationContext.Current);
            _mechanicks.Post(mec);
            PumpsCanv.Children.Add(mec.VisualElement);
        }

        private void InitOilTank()
        {
            _tank = _uiFactory.CreateOilTankUI(TankStatusPb);
            _tank.LogicObj.IsFull += TankObj_IsFull;
            _tank.LogicObj.IsEmpty += LogicObj_IsEmpty;
            _tank.LogicObj.Added += LogicObj_Added;
        }

        private void LogicObj_Added(OilTank sender)
        {
            var percentage = _tank.LogicObj.CurrentVolume / _tank.LogicObj.Capacity * 100;
            if (percentage == 50)
            {
                new Thread(() =>
                {
                    _car.MoveTo(new Thickness(600, 500, 0, 0),
                        new Uri("pack://application:,,,/Resources/carTanker.png"));
                    Thread.Sleep(3000);
                    _tank.LogicObj.Get();
                }).Start();
            }
        }

        private void LogicObj_IsEmpty(OilTank sender)
        {
            _car.MoveTo(new Thickness(-200, 500, 0, 0), new Uri("pack://application:,,,/Resources/carTanker.png"));
        }

        private void TankObj_IsFull(OilTank sender)
        {
            Dispatcher.Invoke(() =>
            {
                _pumpList.ForEach(p => p.Stop());
                var fire = _uiFactory.GetFire(MainCanvas.ActualWidth, MainCanvas.ActualHeight);
                Panel.SetZIndex(fire, 2);
                MainCanvas.Children.Add(fire);

            });
        }

        private void InitCarTank()
        {
            _car = _uiFactory.CreateCarTankUI();
            MainCanvas.Children.Add(_car.VisualElement);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //stop each pump
            _pumpList.ForEach(p => p.Stop());
        }
    }
}
