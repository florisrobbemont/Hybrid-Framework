﻿using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using DevTrends.MvcDonutCaching;
using Umbraco.Core.Logging;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using UmbracoTemplate.Web.ViewModels.Custom;

namespace UmbracoTemplate.Web.Infrastructure
{
    public abstract class BaseSurfaceController : SurfaceController, IRenderMvcController
    {
        /// <summary>
        /// Checks to make sure the physical view file exists on disk.
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        protected bool EnsurePhsyicalViewExists(string template)
        {
            var result = ViewEngines.Engines.FindView(ControllerContext, template, null);
            if (result.View == null)
            {
                LogHelper.Warn<RenderMvcController>("No physical template file was found for template " + template);
                return false;
            }
            return true;
        }

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
            if (!EnsurePhsyicalViewExists(template))
            {
                return Content("");
            }
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
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        protected T GetModel<T>()
            where T : BaseModel, new()
        {
            var model = new T();
            model.MenuItems = GetMenuItems();
            model.Widgets = GetWidgets();

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
                            Url = widget.GetUrlPicker("widgetUrl"),
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
            Umbraco.LogException(filterContext.Exception);

            //Clear the cache if an error occurs.
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems();

            //Show the view error.
            filterContext.Result = View("Error");
            filterContext.ExceptionHandled = true;
        }
    }
}