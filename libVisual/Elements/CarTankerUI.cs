using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using libTask4;
using libTask4.Interfaces;

namespace libVisual.Elements
{
    public class CarTankerUI : ElementUI<CarTanker>
    {
        public CarTankerUI(SynchronizationContext context) : base(context)
        {
            LogicObj = new CarTanker();
            VisualElement = 
                new Image()
                {
                    Height = 160,
                    Margin = new Thickness(-160, 500, 0, 0),
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/carTanker.png"))
                };
        }
    }
}
