using System.Web.Http.Controllers;
using System.Web.Mvc;
using Castle.Core;
using Castle.MicroKernel.Registration;
using UmbracoTemplate.Composition;

namespace UmbracoTemplate.Web.Infrastructure.Composition.Installers
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(FindControllers().Configure(c => c.LifeStyle.Is(LifestyleType.Transient)));
            container.Register(FindApiControllers().Configure(c => c.LifeStyle.Is(LifestyleType.Transient)));
        }

        /// <summary>
        /// Find controllers within this assembly
        /// </summary>
        /// <returns></returns>
        private BasedOnDescriptor FindControllers()
        {
            return Classes.FromAssemblyInDirectory(new EntireBinAssemblyFilter())
                          .BasedOn<IController>()
                          .If(x => x.Name.EndsWith("Controller"));
        }

        /// <summary>
        /// Find API controllers within this assembly
        /// </summary>
        /// <returns></returns>
        private BasedOnDescriptor FindApiControllers()
        {
            return Classes.FromAssemblyInDirectory(new EntireBinAssemblyFilter())
                          .BasedOn<IHttpController>()
                          .If(x => x.Name.EndsWith("ApiController"));
        }
    }
}