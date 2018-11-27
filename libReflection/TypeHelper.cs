using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace libReflection
{
    public class TypeHelper
    {
        public Type Type { get; }
        public Assembly Assembly => Type.Assembly;
        public List<MethodInfo> Methods => Type.GetMethods().ToList();
        public List<PropertyInfo> Properties => Type.GetProperties().ToList();
        public List<ConstructorInfo> Constructors => Type.GetConstructors().ToList();

        public TypeHelper(Type t)
        {
            Type = t;
        }

        public static string ConstructorInfoToString(ConstructorInfo constr)
        {
            return string.Join(", ", constr.GetParameters().Select(p => p.ToString()));
        }
    }
}
