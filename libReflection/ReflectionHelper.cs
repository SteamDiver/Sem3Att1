using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace libReflection
{
    public class ReflectionHelper
    {
        public Assembly Ass { get; private set; }
        public List<Type> Types { get; private set; }

        public ReflectionHelper(string path)
        {
            Ass = Assembly.LoadFile(path);
            Types = GetClasses();
        }

        public List<Type> GetClasses()
        {
            return Ass.GetTypes().Where(t => t.IsClass).ToList();
        }
    }
}
