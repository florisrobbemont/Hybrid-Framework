using System.Web;

namespace UmbracoTemplate.Web.Models
{
    public class WidgetImage : WidgetBase
    {
        public string Title { get; set; }
        public HtmlString Text { get; set; }
        public MediaItemCrop Image { get; set; }
    }
}