using System.Collections.Generic;
using UmbracoTemplate.Web.Models;

namespace UmbracoTemplate.Web.ViewModels
{
    public class NewsOverviewModel : BaseModel
    {
        public IEnumerable<NewsItem> NewsItems { get; set; }
        public Pager Pager { get; set; }
    }
}