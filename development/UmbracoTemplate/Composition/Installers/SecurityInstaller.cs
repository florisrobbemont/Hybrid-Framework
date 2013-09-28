using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using UmbracoTemplate.Security;

namespace UmbracoTemplate.Composition.Installers
{
    public class SecurityInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICryptographyService>().ImplementedBy<CryptographyService>().LifestyleTransient());
            container.Register(Component.For<IPasswordService>().ImplementedBy<PasswordService>().LifestyleTransient());
        }
    }
}