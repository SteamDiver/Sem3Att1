using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using libTask4;
using libTask4.Interfaces;

namespace libVisual.Elements
{
    public class OilTankUI : ElementUI<OilTank>
    {
        public OilTankUI(ProgressBar bar, SynchronizationContext context) : base(context)
        {
            LogicObj = new OilTank(100);
            bar.Maximum = LogicObj.Capacity;
            VisualElement = bar;
            LogicObj.Added += LogicObj_Added;
            LogicObj.IsEmpty += LogicObj_IsEmpty;
        }

        private void LogicObj_IsEmpty(OilTank sender)
        { 
            Context.Post(s => { ((ProgressBar)VisualElement).Value = sender.CurrentVolume; }, null);
        }

        private void LogicObj_Added(OilTank tank)
        {
            Context.Post(s => { ((ProgressBar) VisualElement).Value = tank.CurrentVolume; }, null);
        }

        
    }
}
