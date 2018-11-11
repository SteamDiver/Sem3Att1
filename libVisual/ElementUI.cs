using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using libTask4;
using libTask4.Interfaces;
using XamlAnimatedGif;

namespace libVisual
{
    public class ElementUI<T>
    {
        public T LogicObj { get; set; }
        public SynchronizationContext Context { get; set; }
        public UIElement VisualElement { get; set; }

        public ElementUI(SynchronizationContext context)
        {
            Context = context;
        }

        public void MoveTo<TG>(ElementUI<TG> obj, Uri uri)
        {
            Context.Post(s =>
            {
                var o = (Image)VisualElement;
                AnimationBehavior.SetSourceUri(o, uri);
                var anim = new ThicknessAnimation();
                var transform = new ScaleTransform { ScaleX = o.Margin.Left > ((Image)obj.VisualElement).Margin.Left ? -1 : 1 };
                VisualElement.RenderTransform = transform;
                var to = ((Image)obj.VisualElement).Margin;
                to.Left += 80;
                to.Top += 80;
                anim.From = ((Image)VisualElement).Margin;
                anim.To = to;
                anim.Duration = TimeSpan.FromSeconds(1.5);
                VisualElement.BeginAnimation(FrameworkElement.MarginProperty, anim);
            }, null);
        }

        public void MoveTo(Thickness th, Uri uri)
        {
            Context.Post(s =>
            {
                var o = (Image)VisualElement;
                AnimationBehavior.SetSourceUri(o, uri);
                var anim = new ThicknessAnimation();
                var transform = new ScaleTransform { ScaleX = o.Margin.Left > th.Left ? -1 : 1 };
                VisualElement.RenderTransform = transform;
                var to = th;
                to.Left += 80;
                anim.From = ((Image)VisualElement).Margin;
                anim.To = to;
                anim.Duration = TimeSpan.FromSeconds(1.5);
                VisualElement.BeginAnimation(FrameworkElement.MarginProperty, anim);
            }, null);
        }
    }
}