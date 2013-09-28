using System;
using System.Collections.Generic;
using System.Linq;

namespace UmbracoTemplate.Common.Reflection
{
    public class ReflectionAttributeList : List<Attribute>
    {
        internal ReflectionAttributeList(IEnumerable<Attribute> attributes)
        {
            this.AddRange(attributes);
        }

        public List<Attribute> this[Type type]
        {
            get
            {
                return this.Where(a => a.GetType() == type).ToList();
            }
        }
    }
}