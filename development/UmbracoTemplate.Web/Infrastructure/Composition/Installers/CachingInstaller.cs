using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DevTrends.MvcDonutCaching;

namespace UmbracoTemplate.Web.Infrastructure.Composition.Installers
{
    public class CachingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IOutputCacheManager>().ImplementedBy<OutputCacheManager>().LifestylePerWebRequest());
        }
    }
}