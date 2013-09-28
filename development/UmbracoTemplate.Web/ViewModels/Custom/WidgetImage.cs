using System.Web;

namespace UmbracoTemplate.Web.ViewModels.Custom
{
    public class WidgetImage : WidgetBase
    {
        public string Title { get; set; }
        public HtmlString Text { get; set; }
        public MediaItemCrop Image { get; set; }
    }
}