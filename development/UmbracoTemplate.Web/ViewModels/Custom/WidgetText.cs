using System.Web;

namespace UmbracoTemplate.Web.ViewModels.Custom
{
    public class WidgetText : WidgetBase
    {
        public string Title { get; set; }
        public HtmlString Text { get; set; }
        public UrlPicker Url { get; set; }
    }
}