using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UmbracoTemplate.Common.Reflection
{
    public class ReflectionPropertyList : List<ReflectionProperty>
    {
        private readonly Dictionary<string, ReflectionProperty> cache = new Dictionary<string, ReflectionProperty>();

        internal ReflectionPropertyList(IEnumerable<PropertyInfo> properties, ReflectionClass parent)
        {
            this.AddRange(properties.Select(a => new ReflectionProperty(a, parent)));
            this.ForEach(a => this.cache.Add(a.Name, a));
        }

        public ReflectionProperty this[string name]
        {
            get
            {
                ReflectionProperty result;
                this.cache.TryGetValue(name, out result);
                return result;
            }
        }
    }
}