using System.Collections.Generic;
using Umbraco.Web;
using Umbraco.Web.Models;
using UmbracoTemplate.Web.ViewModels.Custom;

namespace UmbracoTemplate.Web.ViewModels
{
    public class BaseModel : RenderModel
    {
        public BaseModel() : base(UmbracoContext.Current.PublishedContentRequest.PublishedContent) { }

        public IEnumerable<MenuItem> MenuItems { get; set; }
        public IEnumerable<WidgetBase> Widgets { get; set; }
    }
}