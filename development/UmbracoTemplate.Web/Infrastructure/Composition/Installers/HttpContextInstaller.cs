using System.Web;
using Castle.MicroKernel.Registration;

namespace UmbracoTemplate.Web.Infrastructure.Composition.Installers
{
    public class HttpContextInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<HttpRequestBase>()
                .LifeStyle.PerWebRequest
                .UsingFactoryMethod(() => new HttpRequestWrapper(HttpContext.Current.Request)));

            container.Register(Component.For<HttpContextBase>()
                .LifeStyle.PerWebRequest
                .UsingFactoryMethod(() => new HttpContextWrapper(HttpContext.Current))); 
        }
    }
}