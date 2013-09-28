using System;
using UmbracoTemplate.Kernel;

namespace UmbracoTemplate.Composition
{
    /// <summary>
    /// Provides access to all commenly used objects
    /// </summary>
    public static class Application
    {
        /// <summary>
        /// The composition container
        /// </summary>
        public static IObjectFactory ObjectFactory { get; private set; }

        /// <summary>
        /// Creates the application object by initializating the container
        /// </summary>
        public static void Create()
        {
            if(ObjectFactory != null)
                throw new InvalidOperationException("Application has already started!");

            ObjectFactory = new ObjectFactory(CompositionContainerFactory.Create());
        }

        /// <summary>
        /// Executes all startup code for the application
        /// </summary>
        public static void Start()
        {
            using (var kernelContext = new ComponentWrapper<IKernelContext>())
            {
                kernelContext.Component.RunKernelEventGroups("Startup");    
            }
        }

        /// <summary>
        /// Executes all cleanup code for the applicaion
        /// </summary>
        public static void Stop()
        {
            using (var kernelContext = new ComponentWrapper<IKernelContext>())
            {
                kernelContext.Component.RunKernelEventGroups("Shutdown");
            }

            ObjectFactory.Dispose();
        }
    }
}