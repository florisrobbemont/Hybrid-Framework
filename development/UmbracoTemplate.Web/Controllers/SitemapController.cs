using DevTrends.MvcDonutCaching;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
using Umbraco.Core.Models;
using Umbraco.Web;
using UmbracoTemplate.Web.Infrastructure;

namespace UmbracoTemplate.Web.Controllers
{
    public class SitemapController : BaseSurfaceController
    {
        //[DonutOutputCache(Duration = 86400, Location = OutputCacheLocation.Server, VaryByCustom = "url;device")]
        //public ActionResult Sitemap()
        //{
        //    var model = GetModel<SitemapModel>();
        //    model.SitemapItems = GetSitemapItems(CurrentPage.AncestorOrSelf(1));
        //    return CurrentTemplate(model);
        //}

        ///// <summary>
        ///// Get all sitemap items recursively.
        ///// </summary>
        ///// <param name="content"></param>
        ///// <returns></returns>
        //private List<SitemapItem> GetSitemapItems(IPublishedContent content)
        //{
        //    var items = new List<SitemapItem>();

        //    //Loop through the children and add them to the sitemap.
        //    foreach (var c in content.Children)
        //    {
        //        //Only add nodes which have a template and where umbracoNaviHide is not checked.
        //        if (c.TemplateId > 0 && !c.GetPropertyValue<bool>("umbracoNaviHide"))
        //        {
        //            //Add the current item and the children of that item recursively.
        //            items.Add(
        //                new SitemapItem()
        //                {
        //                    Name = c.Name,
        //                    Url = c.Url,
        //                    Children = GetSitemapItems(c)
        //                }
        //            );
        //        }
        //    }

        //    return items;
        //}
    }
}
