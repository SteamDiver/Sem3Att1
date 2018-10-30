using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using libTask4;
using libTask4.Interfaces;

namespace libVisual
{
    public class ElementUI<T>
    {
        public T LogicObj { get; set; }
        public SynchronizationContext Context { get; set; }
        public Image Image { get; set; }

        public ElementUI(T obj, SynchronizationContext context)
        {
            LogicObj = obj;
            Context = context;
        }
    }
}