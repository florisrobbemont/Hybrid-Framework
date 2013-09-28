using System;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using UmbracoTemplate.Exceptions;

namespace UmbracoTemplate.Composition.Installers
{
    public class ExceptionInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                    Classes.FromAssemblyInDirectory(new ApplicationAssemblyFilter())
                           .BasedOn(typeof(IExceptionFormatter<>))
                           .WithService.AllInterfaces()
                           .WithServiceSelf()
                           .LifestyleTransient());

            container.Register(
                Component.For<IExceptionFormatterFactory>()
                         .AsFactory(c => 
                             c.SelectedWith(new GenericTypedFactoryComponentSelector(typeof(IExceptionFormatter<>), 
                                                                                     typeof(IExceptionFormatter<Exception>), 
                                                                                     silentResolve: true)))
                         .LifestyleTransient());

        }
    }
}
