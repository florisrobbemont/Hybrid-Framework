using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UmbracoTemplate.Common.Reflection
{
    public class ReflectionMethodList : List<ReflectionMethod>
    {
        internal ReflectionMethodList(IEnumerable<MethodInfo> methods, ReflectionClass parent)
        {
            this.AddRange(from method in methods select new ReflectionMethod(method, parent));
        }

        public IEnumerable<ReflectionMethod> this[string name]
        {
            get
            {
                return this.Where(a => a.Name == name).ToList();
            }
        }
    }
}