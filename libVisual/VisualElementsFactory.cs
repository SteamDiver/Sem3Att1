using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using libTask4;
using libVisual.Elements;
using XamlAnimatedGif;

namespace libVisual
{
    public class VisualElementsFactory
    {
        public SynchronizationContext Context { get; set; }
        public VisualElementsFactory(SynchronizationContext context)
        {
            Context = context;
        }

        public PumpStationUI CreatePumpStationUI(int n)
        {
            return new PumpStationUI(n, Context);
        }

        public MechanicUI CreateMechanicUI(int n)
        {
            return new MechanicUI(Context);
        }

        public OilTankUI CreateOilTankUI(ProgressBar bar)
        {
            return new OilTankUI(bar, Context);
        }

        public CarTankerUI CreateCarTankUI()
        {
            return new CarTankerUI(Context);
        }
    }
}
