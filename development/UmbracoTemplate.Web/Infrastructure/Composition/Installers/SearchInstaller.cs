using Castle.MicroKernel.Registration;

namespace UmbracoTemplate.Web.Infrastructure.Composition.Installers
{
    public class SearchInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Component.For<IWebsiteSearcherContext>().ImplementedBy<WebsiteSearcherContext>().LifestyleSingleton());

            container.Register(
                Classes.FromThisAssembly()
                       .BasedOn<WebsiteSearchDocumentType>()
                       .WithServiceAllInterfaces()
                       .LifestyleTransient());
        }
    }
}