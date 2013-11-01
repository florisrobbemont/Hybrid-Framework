using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Web.Mvc;
using UmbracoTemplate.Composition;
using UmbracoTemplate.Web.Controllers;
using UmbracoTemplate.Web.Infrastructure.Composition;

namespace UmbracoTemplate.Web.Boot
{
    /// <summary>
    /// Umbraco boot handler to start the application
    /// </summary>
    public class UmbracoBoot : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            umbracoApplication.Disposed += umbracoApplication_Disposed;
        }

        void umbracoApplication_Disposed(object sender, System.EventArgs e)
        {
            Application.Stop();
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            FilterConfig.Register();
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            Application.Debug = umbracoApplication.Context.IsDebuggingEnabled;
            Application.Create();

            FilteredControllerFactoriesResolver.Current.InsertType<UmbracoFilteredControllerFactory>(0);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(Application.ObjectFactory));

            //By registering this here we can make sure that if route hijacking doesn't find a controller it will use this controller.
            //That way each page will always be routed through one of our controllers.
            DefaultRenderMvcControllerResolver.Current.SetDefaultControllerType(typeof(DefaultController));

            Application.Start();
        }
    }
}