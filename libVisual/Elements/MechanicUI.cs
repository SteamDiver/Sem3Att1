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
        public MechanicUI(Mechanic obj, SynchronizationContext context) : base(obj, context)
        {
            obj.IsFree += Mec_IsFree;
            VisualElement =
                new Image()
                {
                    Height = 80
                };
            AnimationBehavior.SetSourceUri((Image)VisualElement, new Uri("pack://application:,,,/Resources/mechanic_idle.gif"));
        }

        private void Mec_IsFree(Mechanic sender)
        {
            Context.Post(s =>
            {
                AnimationBehavior.SetSourceUri((Image)VisualElement,
                    new Uri("pack://application:,,,/Resources/mechanic_idle.gif"));
            }, null);
        }

        public void MoveTo<T>(ElementUI<T> obj)
        {
            Context.Post(s =>
            {
                var o = (Image) VisualElement;
                AnimationBehavior.SetSourceUri(o, new Uri("pack://application:,,,/Resources/mechanic_run.gif"));
                var anim = new ThicknessAnimation();
                var transform = new ScaleTransform {ScaleX = o.Margin.Left > ((Image)obj.VisualElement).Margin.Left ? -1 : 1};
                VisualElement.RenderTransform = transform;
                var to = ((Image)obj.VisualElement).Margin;
                to.Left += 80;
                anim.From = ((Image)VisualElement).Margin;
                anim.To = to;
                anim.Duration = TimeSpan.FromSeconds(1.5);
                VisualElement.BeginAnimation(FrameworkElement.MarginProperty, anim);
            }, null);
        }

    }
}
