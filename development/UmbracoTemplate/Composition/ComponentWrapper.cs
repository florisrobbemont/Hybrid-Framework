using System;
using Castle.MicroKernel;

namespace UmbracoTemplate.Composition
{
    /// <summary>
    /// Wraps the lifetime of a component in a single class so it can be used with a Using block
    /// </summary>
    /// <typeparam name="TComponent">The type of the component to resolve</typeparam>
    public sealed class ComponentWrapper<TComponent> : IDisposable
    {
        /// <summary>
        /// Gets the component
        /// </summary>
        public TComponent Component
        {
            get
            {
                return component;
            }
        }

        private readonly TComponent component;
            
        public ComponentWrapper()
        {
            component = Application.ObjectFactory.Resolve<TComponent>();
        }

        public ComponentWrapper(IKernel kernel)
        {
            component = kernel.Resolve<TComponent>();
        }

        public void Dispose()
        {
            Application.ObjectFactory.Release(component);
        }
    }
}