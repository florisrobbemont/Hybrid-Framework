using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using UmbracoTemplate.Web.Bundles;

namespace UmbracoTemplate.Web.Infrastructure.Composition.Installers
{
    public class BundleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                                      .BasedOn<IBundleConfiguration>()
                                      .WithServiceFirstInterface()
                                      .LifestyleTransient());
        }
    }
}