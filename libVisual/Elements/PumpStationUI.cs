using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using libTask4;
using XamlAnimatedGif;

namespace libVisual.Elements
{
    public class PumpStationUI : ElementUI<PumpStation>
    {
        public bool IsLinked => LogicObj?.Tank != null;

        public PumpStationUI(int n, SynchronizationContext context) : base(context)
        {
            VisualElement = new Image()
            {
                Margin = new Thickness(n * 250, 0, 0, 0),
                Height = 180
            };
            LogicObj = new PumpStation();
            LogicObj.Broken += Station_Broken;
            LogicObj.Fixed += Station_Fixed;
            AnimationBehavior.SetSourceUri((Image)VisualElement, new Uri("pack://application:,,,/Resources/pump.gif"));
        }

        public void LinkToTank(OilTankUI tank)
        {
            LogicObj.Tank = tank.LogicObj;
           
            LogicObj.Tank.IsEmpty += (sender) =>
            {
                if (!LogicObj.IsBroken)
                    Start();
            };
        }

        public void Start()
        {
            if (IsLinked)
            {
                StartAnimation();
                Thread pumpThread = new Thread(LogicObj.StartWork);
                pumpThread.Start();
            }
        }

        public void Stop()
        {
            StopAnimation();
            LogicObj.StopWork();
        }

        private void Station_Fixed(PumpStation sender)
        {
            Start();
        }

        private void Station_Broken(PumpStation sender)
        {
           Stop();
        }

        private void StopAnimation()
        {
            Context.Send(s=> AnimationBehavior.GetAnimator(VisualElement)?.Pause(), null);
        }

        private void StartAnimation()
        {
            Context.Send(s =>
            {
                AnimationBehavior.GetAnimator(VisualElement)?.Play();
            }, null);
        }
    }
}
