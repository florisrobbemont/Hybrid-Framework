using DevTrends.MvcDonutCaching;
using System.Web.Mvc;
using System.Web.UI;
using UmbracoTemplate.Web.Infrastructure;
using UmbracoTemplate.Web.ViewModels;

namespace UmbracoTemplate.Web.Controllers
{
    public class HomeController : BaseSurfaceController
    {
        /// <summary>
        /// Currently there is no logic in the home controller, but it usually needs a lot of logic so the controller and model are already available.
        /// </summary>
        /// <returns></returns>
        [DonutOutputCache(Duration = 86400, Location = OutputCacheLocation.Server, VaryByCustom = "url;device")]
        public ActionResult Home()
        {
            var model = GetModel<HomeModel>();
            return CurrentTemplate(model);
        }
    }
}
