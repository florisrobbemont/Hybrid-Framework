﻿using System.Collections.Generic;
using UmbracoTemplate.Web.ViewModels.Custom;

namespace UmbracoTemplate.Web.ViewModels
{
    public class SitemapModel : BaseModel
    {
        public IEnumerable<SitemapItem> SitemapItems { get; set; }
    }
}