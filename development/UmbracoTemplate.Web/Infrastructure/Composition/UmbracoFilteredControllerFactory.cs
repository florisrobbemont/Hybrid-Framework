using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Web.Mvc;
using UmbracoTemplate.Composition;

namespace UmbracoTemplate.Web.Infrastructure.Composition
{
    public class UmbracoFilteredControllerFactory : UmbracoControllerFactory
    {
        protected readonly IObjectFactory ObjectFactory;

        public UmbracoFilteredControllerFactory()
        {
            ObjectFactory = Application.ObjectFactory; 
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            var controllerType = GetControllerType(requestContext, controllerName);

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
        
        public override bool CanHandle(RequestContext request)
        {
            var controllerType = GetControllerType(request, request.RouteData.Values["controller"].ToString());
            
            return ObjectFactory.HasComponent(controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            ObjectFactory.Release(controller);
        }
    }
}