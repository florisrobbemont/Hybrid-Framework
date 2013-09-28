using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace UmbracoTemplate.Web.Infrastructure.Composition.Installers
{
    public class LoggingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ILoggingService>()
                         .ImplementedBy<LoggingService>()
                         .LifeStyle.HybridPerWebRequestPerThread());
        }
    }
}