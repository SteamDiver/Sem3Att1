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
        public PumpStation Station { get; set; }
        public PumpStationUI(int n, PumpStation st, SynchronizationContext context) : base(st, context)
        {
            VisualElement = new Image()
            {
                Margin = new Thickness(n * 250, 0, 0, 0),
                Height = 180
            };
            AnimationBehavior.SetSourceUri((Image)VisualElement, new Uri("pack://application:,,,/Resources/pump.gif"));
            Station = st;
            Station.Broken += Station_Broken;
            Station.Fixed += Station_Fixed;
        }

        private void Station_Fixed(PumpStation sender)
        {
            Context.Post(s => StartAnimation(), null);
        }

        private void Station_Broken(PumpStation sender)
        {
           Context.Post(s => StopAnimation(), null);
        }

        private void StopAnimation()
        {
            AnimationBehavior.GetAnimator(VisualElement).Pause();
        }

        private void StartAnimation()
        {
            AnimationBehavior.GetAnimator(VisualElement).Play();
        }
    }
}
