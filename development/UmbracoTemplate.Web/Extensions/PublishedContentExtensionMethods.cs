using System;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;
using UmbracoTemplate.Web.Models;

namespace UmbracoTemplate.Web.Extensions
{
    public static class PublishedContentExtensionMethods
    {
        /// <summary>
        /// Return the strings that has been filled in.
        /// </summary>
        public static GoogleMaps GetGoogleMapsValues(this IPublishedContent content, string alias)
        {
            char[] splitChar = { ',', ';' };
            var values = content.GetPropertyValue<string>(alias).Split(splitChar, StringSplitOptions.RemoveEmptyEntries);

            if (!values.Any())
            {
                return null;
            }

            //Return the Google Maps values with the properties set.
            return
                (
                    new GoogleMaps()
                        {
                            Lat = values[0],
                            Lng = values[1],
                            Zoom = Convert.ToInt32(values[2])
                        }
                );
        }

        /// <summary>
        /// Return the node where default settings are stored.
        /// </summary>
        public static IPublishedContent TopPage(this IPublishedContent content)
        {
            var topPage = (IPublishedContent)System.Web.HttpContext.Current.Items["TopPage"];

            if (topPage == null)
            {
                topPage = content.AncestorOrSelf(1);
                System.Web.HttpContext.Current.Items["TopPage"] = topPage;
            }

            return topPage;
        }
    }
}