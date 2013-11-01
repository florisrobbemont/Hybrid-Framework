using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevTrends.MvcDonutCaching;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using UmbracoTemplate.Logging;
using UmbracoTemplate.Web.Content;
using UmbracoTemplate.Web.Extensions;
using UmbracoTemplate.Web.Models;
using UmbracoTemplate.Web.ViewModels;

namespace UmbracoTemplate.Web.Infrastructure
{
    public abstract class BaseSurfaceController : SurfaceController, IRenderMvcController
    {
        /// <summary>
        /// Gets or sets the LoggingService
        /// </summary>
        public ILoggingService LoggingService { get; set; }

        /// <summary>
        /// Gets or sets the OutputCacheManager
        /// </summary>
        public IOutputCacheManager OutputCacheManager { get; set; }

        /// <summary>
        /// Gets or sets the Umbraco Helper Factory
        /// </summary>
        public IUmbracoHelperFactory UmbracoHelperFactory { get; set; }
        
        /// <summary>
        /// Returns an ActionResult based on the template name found in the route values and the given model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// If the template found in the route values doesn't physically exist, then an empty ContentResult will be returned.
        /// </remarks>
        protected ActionResult CurrentTemplate<T>(T model)
        {
            var template = ControllerContext.RouteData.Values["action"].ToString();
           
            return View(template, model);
        }

        /// <summary>
        /// The default action to render the front-end view.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual ActionResult Index(RenderModel model)
        {
            return CurrentTemplate(model);
        }

        /// <summary>
        /// Return the base model which needs to be used everywhere.
        /// </summary>
        protected T GetModel<T>()
            where T : BaseModel, new()
        {
            var model = new T
                {
                    MenuItems = GetMenuItems(), 
                    Widgets = GetWidgets()
                };

            return model;
        }

        private IEnumerable<MenuItem> GetMenuItems()
        {
            return
            (
                from n in CurrentPage.TopPage().Children
                where n.HasProperty("menuTitle")
                && !n.GetPropertyValue<bool>("hideInMenu")
                select new MenuItem()
                {
                    Id = n.Id,
                    Title = n.GetPropertyValue<string>("menuTitle"),
                    Url = n.Url(),
                    ActiveClass = CurrentPage.Path.Contains(n.Id.ToString()) ? "active" : null
                }
            );
        }

        private IEnumerable<WidgetBase> GetWidgets()
        {
            var nodes = CurrentPage.GetMntpNodes("widgets");
            var list = new List<WidgetBase>();

            foreach (var widget in nodes)
            {
                switch (widget.DocumentTypeAlias)
                {
                    case "WidgetText":
                        list.Add(new WidgetText()
                        {
                            Title = widget.GetPropertyValue<string>("widgetTitle"),
                            Text = widget.GetPropertyValue<HtmlString>("widgetText"),
                            Url = widget.GetUrlPicker(UmbracoHelperFactory.GetCurrent(), "widgetUrl"),
                            View = "WidgetText"
                        });
                        break;

                    case "WidgetImage":
                        list.Add(new WidgetImage()
                        {
                            Title = widget.GetPropertyValue<string>("widgetTitle"),
                            Text = widget.GetPropertyValue<HtmlString>("widgetText"),
                            Image = widget.GetCroppedImage("widgetImage", 200, 200),
                            View = "WidgetImage"
                        });
                        break;
                }
            }

            return list;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            //Log the exception.
            LoggingService.Error("Exception unhandled in BaseSurfaceController", filterContext.Exception);

            //Clear the cache if an error occurs.
            OutputCacheManager.RemoveItems();

            //Show the view error.
            filterContext.Result = View("Error");
            filterContext.ExceptionHandled = true;
        }
    }
}