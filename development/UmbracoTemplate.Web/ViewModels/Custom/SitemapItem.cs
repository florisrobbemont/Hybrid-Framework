using System.Collections.Generic;

namespace UmbracoTemplate.Web.ViewModels.Custom
{
    public class SitemapItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public IEnumerable<SitemapItem> Children { get; set; }
    }
}