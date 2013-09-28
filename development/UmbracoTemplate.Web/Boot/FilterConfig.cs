using System.Web.Mvc;

namespace UmbracoTemplate.Web.Boot
{
    internal static class FilterConfig
    {
        internal static void Register()
        {
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
        }
    }
}