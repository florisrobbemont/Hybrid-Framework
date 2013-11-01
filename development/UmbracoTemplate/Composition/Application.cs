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
        /// Gets if the application has booted
        /// </summary>
        internal static bool IsApplicationBooted { get; private set; }

        /// <summary>
        /// Gets or sets the debug mode.
        /// </summary>
        /// <exception cref="InvalidOperationException">Can only set the debug mode before the application has booted!</exception>
        public static bool Debug
        {
            get { return debug; }
            set
            {
                if (!IsApplicationBooted)
                    debug = value;
                else
                    throw new InvalidOperationException("Can only set the debug mode before the application has booted!");
            }
        }
        private static bool debug;

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

            IsApplicationBooted = true;
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