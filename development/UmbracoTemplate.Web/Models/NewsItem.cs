using System;

namespace UmbracoTemplate.Web.Models
{
    public class NewsItem
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public MediaItemCrop Image { get; set; }
        public DateTime Date { get; set; }
    }
}