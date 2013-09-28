using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UmbracoTemplate.Composition;

namespace UmbracoTemplate.Web.Infrastructure.Composition
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        protected readonly IObjectFactory ObjectFactory;

        public WindsorControllerFactory()
        {
            ObjectFactory = Application.ObjectFactory; ;
        }

        public override void ReleaseController(IController controller)
        {
            ObjectFactory.Release(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                if (!requestContext.HttpContext.IsDebuggingEnabled)
                {
                    throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.",
                                                               requestContext.HttpContext.Request.Path));
                }

                return null;
            }

            return (IController)ObjectFactory.Resolve(controllerType);
        }
    }
}