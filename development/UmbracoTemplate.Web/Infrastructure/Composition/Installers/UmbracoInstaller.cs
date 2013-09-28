using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Umbraco.Web;
using UmbracoTemplate.Web.Content;

namespace UmbracoTemplate.Web.Infrastructure.Composition.Installers
{
    public class UmbracoInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<UmbracoHelper>()
                         .LifestylePerWebRequest()
                         .UsingFactoryMethod(() => new UmbracoHelper(UmbracoContext.Current)));

            container.Register(Component.For<IUmbracoHelperFactory>().AsFactory());
        }
    }
}