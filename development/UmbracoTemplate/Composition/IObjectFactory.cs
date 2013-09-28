using System;
using System.Collections.Generic;
using Castle.Windsor;

namespace UmbracoTemplate.Composition
{
    /// <summary>
    /// Defines the set of functionality used to inject dependencies
    /// </summary>
    public interface IObjectFactory : IDisposable
    {
        /// <summary>
        /// Resolves a dependency
        /// </summary>
        /// <param name="type">The type to resolve</param>
        /// <returns>An instance of the Type, or null if not found</returns>
        object Resolve(Type type);

        /// <summary>
        /// Resolves a dependency
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <returns>An instance of the Type, or null if not found</returns>
        T Resolve<T>();

        /// <summary>
        /// Resolves a dependency by its ID
        /// </summary>
        /// <typeparam name="T">The type to retrieve</typeparam>
        /// <param name="contractName">The identifier the part was exported with</param>
        /// <returns>An instance of the Type, or null if not found</returns>
        T Resolve<T>(string contractName);

        /// <summary>
        /// Returns a exported value from the internal container
        /// </summary>
        /// <typeparam name="T">The type to retrieve</typeparam>
        /// <returns>An instance of the Type, or null if not found</returns>
        IEnumerable<T> ResolveAll<T>();

        /// <summary>
        /// Returns a exported value from the internal container
        /// </summary>
        /// <param name="type">The type to resolve</param>
        /// <returns>An instance of the Type, or null if not found</returns>
        IEnumerable<object> ResolveAll(Type type);

        /// <summary>
        /// Gets if the container has a component
        /// </summary>
        bool HasComponent(Type type);

        /// <summary>
        /// Releases a component from the container
        /// </summary>
        /// <param name="instance">The instance to release</param>
        void Release(object instance);

        /// <summary>
        /// Gets access to the underlying container
        /// </summary>
        IWindsorContainer Container { get; }
    }
}
