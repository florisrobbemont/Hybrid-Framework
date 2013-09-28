using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using UmbracoTemplate.Composition;

namespace UmbracoTemplate.Logging
{
    internal class LoggingModelConstruction : IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            if(model.SearchRegistrationForAttribute<LogAttribute>())
                EnableLoggingInterceptor(model);
        }

        private static void EnableLoggingInterceptor(ComponentModel model)
        {
            model.Interceptors.Add(new InterceptorReference(typeof (LoggingInterceptor)));
        }
    }
}
