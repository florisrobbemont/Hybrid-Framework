using Castle.Core;
using Castle.MicroKernel;
using UmbracoTemplate.Composition;

namespace UmbracoTemplate.Logging
{
    internal static class ComponentLogger
    {
        private static IKernel kernel;
        private static ILoggingService loggingService;

        internal static void StartLogging(IKernel kernelArgument)
        {
            kernel = kernelArgument;

            kernel.ComponentCreated += kernel_ComponentCreated;
        }

        private static void kernel_ComponentCreated(ComponentModel model, object instance)
        {
            EnsureLoggingService();

            if (loggingService == null) return;

            if(!model.SearchRegistrationForAttribute<IgnoreInstanceLogAttribute>())
                loggingService.Info("Creating {0} instance of '{1}'", model.LifestyleType.ToString(), model.Implementation.FullName);
        }

        private static void EnsureLoggingService()
        {
            if (loggingService != null)
                return;

            kernel.ComponentCreated -= kernel_ComponentCreated;

            try
            {
                if (loggingService == null)
                {
                    loggingService = kernel.Resolve<ILoggingService>();
                }
            }
            catch
            {
            }
            finally
            {
                kernel.ComponentCreated += kernel_ComponentCreated;
            }
        }
    }
}
