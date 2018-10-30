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

        public PumpStationUI CreatePumpStationUI(int n, PumpStation stationObj)
        {
            return new PumpStationUI(n, stationObj, Context);
        }

        public MechanicUI CreateMechanicUI(int n, Mechanic mechanicObj)
        {
            return new MechanicUI(mechanicObj, Context);
        }
    }
}
