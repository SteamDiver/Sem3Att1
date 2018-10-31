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
        public OilTankUI(OilTank obj, SynchronizationContext context) : base(obj, context)
        {
        }

        public void Init(object o)
        {
            Context.Post(s =>
            {
                VisualElement = (ProgressBar) o;
                ((ProgressBar) o).Maximum = LogicObj.Capacity;
                LogicObj.Added += LogicObj_Added;
            }, null);
        }

        private void LogicObj_Added()
        {
            Context.Post(s => { ((ProgressBar) VisualElement).Value++; }, null);
        }
    }
}
