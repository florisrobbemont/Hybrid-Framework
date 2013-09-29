using Castle.MicroKernel.Registration;
using UmbracoTemplate.Kernel;

namespace UmbracoTemplate.Composition.Installers
{
    public class KernelInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                    Classes.FromAssemblyInDirectory(new ApplicationAssemblyFilter())
                           .BasedOn<IKernelEvent>()
                           .WithService.AllInterfaces()
                           .LifestyleTransient());

            container.Register(Component.For<IKernelContext>().ImplementedBy<KernelContext>().LifestyleSingleton());
        }
    }
}