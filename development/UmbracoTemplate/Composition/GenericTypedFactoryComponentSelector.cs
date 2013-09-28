using System;
using System.Linq;
using System.Reflection;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;

namespace UmbracoTemplate.Composition
{
    /// <summary>
    /// Provides an abstract generic layer for selecting generic types from a factory
    /// </summary>
    public sealed class GenericTypedFactoryComponentSelector : ITypedFactoryComponentSelector
    {
        private readonly DefaultTypedFactoryComponentSelector defaultTypedFactoryComponentSelector;

        private readonly Type genericType;
        private readonly Type fallbackType;
        private readonly bool silentResolve;

        public GenericTypedFactoryComponentSelector(Type genericType, Type fallbackType = null, bool silentResolve = false)
        {
            this.genericType = genericType;
            this.fallbackType = fallbackType;
            this.silentResolve = silentResolve;

            defaultTypedFactoryComponentSelector = new DefaultTypedFactoryComponentSelector();
        }

        public Func<IKernelInternal, IReleasePolicy, object> SelectComponent(MethodInfo method, Type type, object[] arguments)
        {
            if (!arguments.Any())
                return defaultTypedFactoryComponentSelector.SelectComponent(method, type, arguments);

            if (!arguments.All(x => x is Type))
                throw new InvalidOperationException("You can only use the TypedFactory selector if all parameters are Types.");

            return (kernel, policy) =>
                {
                    var argumentsAsAnonymousType = genericType.MakeGenericType(arguments.Cast<Type>().ToArray());

                    if (silentResolve)
                        return TryResolve(kernel, argumentsAsAnonymousType, policy) ??
                               ResolveFallback(kernel, policy, fallbackType);

                    return Resolve(kernel, argumentsAsAnonymousType, policy);
                };
        }

        private static object ResolveFallback(IKernelInternal kernel, IReleasePolicy policy, Type fallbackType)
        {
            return ((fallbackType != null) ? Resolve(kernel, fallbackType, policy) : null);
        }

        private static object TryResolve(IKernelInternal kernel, Type argumentsAsAnonymousType, IReleasePolicy policy)
        {
            return kernel.HasComponent(argumentsAsAnonymousType)
                       ? Resolve(kernel, argumentsAsAnonymousType, policy)
                       : null;
        }

        private static object Resolve(IKernelInternal kernel, Type argumentsAsAnonymousType, IReleasePolicy policy)
        {
            return kernel.Resolve(argumentsAsAnonymousType, null, policy);
        }
    }
}