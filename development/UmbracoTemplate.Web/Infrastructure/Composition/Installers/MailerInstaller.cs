using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace UmbracoTemplate.Web.Infrastructure.Composition.Installers
{
    public class MailerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IMailer>().ImplementedBy<Mailer>().LifeStyle.Is(LifestyleType.Transient));
        }
    }
}