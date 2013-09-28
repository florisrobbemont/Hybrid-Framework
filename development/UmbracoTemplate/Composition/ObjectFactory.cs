using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;

namespace UmbracoTemplate.Composition
{
    /// <summary>
    /// ObjectFactoryBase for default injection of dependencies
    /// </summary>
    internal class ObjectFactory : IObjectFactory
    {
        private readonly IWindsorContainer windsorContainer;

        public ObjectFactory(IWindsorContainer windsorContainer)
        {
            this.windsorContainer = windsorContainer;
        }

        public IWindsorContainer Container { get; set; }

        public object Resolve(Type type)
        {
            return windsorContainer.Resolve(type);
        }

        public T Resolve<T>()
        {
            return windsorContainer.Resolve<T>();
        }

        public T Resolve<T>(string contractName)
        {
            return windsorContainer.Resolve<T>(contractName);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return windsorContainer.ResolveAll<T>();
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            return windsorContainer.ResolveAll(type).Cast<object>();
        }

        public bool HasComponent(Type type)
        {
            return windsorContainer.Kernel.HasComponent(type);
        }

        public void Release(object instance)
        {
            windsorContainer.Release(instance);
        }
        
        public void Dispose()
        {
            if (windsorContainer != null)
                windsorContainer.Dispose();
        }
    }
}