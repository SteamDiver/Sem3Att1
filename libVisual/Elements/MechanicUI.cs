using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using libTask4;
using libTask4.Interfaces;
using XamlAnimatedGif;

namespace libVisual.Elements
{
    public class MechanicUI : ElementUI<Mechanic>
    {
        public MechanicUI( SynchronizationContext context) : base(context)
        {
            LogicObj = new Mechanic();
            LogicObj.IsFree += Mec_IsFree;
            VisualElement =
                new Image()
                {
                    Height = 80,
                    Margin = new Thickness(10, 80, 0, 0)
                };
            AnimationBehavior.SetSourceUri((Image)VisualElement, new Uri("pack://application:,,,/Resources/mechanic_idle.gif"));
        }

        private void Mec_IsFree(Mechanic sender)
        {
            Context.Send(s =>
            {
                AnimationBehavior.SetSourceUri((Image)VisualElement,
                    new Uri("pack://application:,,,/Resources/mechanic_idle.gif"));
            }, null);
        }

        public void DoWork(Item target)
        {
            Context.Send(s =>
            {
                AnimationBehavior.SetSourceUri((Image)VisualElement,
                    new Uri("pack://application:,,,/Resources/work.gif"));
            }, null);

            LogicObj.DoWork(target);
        }
    }
}
