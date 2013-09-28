using System;
using System.Collections.Generic;
using UmbracoTemplate.Common.Reflection.Exceptions;

namespace UmbracoTemplate.Common.Reflection
{
    public static class ReflectionCache
    {
        private static readonly Dictionary<Type, ReflectionClass> Cache = new Dictionary<Type, ReflectionClass>();

        public static ReflectionClass GetReflection(Type t)
        {
            if (t == null)
            {
                throw new NullReferenceReflectionException();
            }

            if (!Cache.ContainsKey(t))
            {
                Cache[t] = new ReflectionClass(t);
            }

            return Cache[t];
        }
    }
}