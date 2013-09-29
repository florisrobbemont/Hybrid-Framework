using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Umbraco.Web;

namespace UmbracoTemplate.Web.Extensions
{
    public static class UmbracoHelperExtensions
    {
        /// <summary>
        /// Appends or updates a query string value to the current Url
        /// </summary>
        /// <param name="umbraco"></param>
        /// <param name="key">The query string key</param>
        /// <param name="value">The query string value</param>
        /// <returns>The updated Url</returns>
        public static string AppendOrUpdateQueryString(this UmbracoHelper umbraco, string key, string value)
        {
            return umbraco.AppendOrUpdateQueryString(HttpContext.Current.Request.RawUrl, key, value);
        }

        /// <summary>
        /// Appends or updates a query string value to supplied Url
        /// </summary>
        /// <param name="umbraco"></param>
        /// <param name="url">The Url to update</param>
        /// <param name="key">The query string key</param>
        /// <param name="value">The query string value</param>
        /// <returns>The updated Url</returns>
        public static string AppendOrUpdateQueryString(this UmbracoHelper umbraco, string url, string key, string value)
        {
            const char q = '?';

            if (url.IndexOf(q) == -1)
            {
                return string.Concat(url, q, key, '=', HttpUtility.UrlEncode(value));
            }

            var baseUrl = url.Substring(0, url.IndexOf(q));
            var queryString = url.Substring(url.IndexOf(q) + 1);
            var match = false;
            var kvps = HttpUtility.ParseQueryString(queryString);

            foreach (var queryStringKey in kvps.AllKeys)
            {
                if (queryStringKey == key)
                {
                    kvps[queryStringKey] = value;
                    match = true;
                    break;
                }
            }

            if (!match)
            {
                kvps.Add(key, value);
            }

            return string.Concat(baseUrl, q, ConstructQueryString(kvps, null, false));
        }

        /// <summary>
        /// Constructs a NameValueCollection into a query string.
        /// </summary>
        /// <remarks>Consider this method to be the opposite of "System.Web.HttpUtility.ParseQueryString"</remarks>
        /// <param name="parameters">The NameValueCollection</param>
        /// <param name="delimiter">The String to delimit the key/value pairs</param>
        /// <param name="omitEmpty">Boolean to chose whether to omit empty values</param>
        /// <returns>A key/value structured query string, delimited by the specified String</returns>
        /// <example>
        /// http://blog.leekelleher.com/2009/09/19/how-to-convert-namevaluecollection-to-a-query-string-revised/
        /// </example>
        private static string ConstructQueryString(NameValueCollection parameters, string delimiter, bool omitEmpty)
        {
            const char @equals = '=';

            if (string.IsNullOrEmpty(delimiter))
                delimiter = "&";

            var items = new List<string>();

            for (var i = 0; i < parameters.Count; i++)
            {
                var strings = parameters.GetValues(i);

                if (strings != null)
                {
                    foreach (var value in strings)
                    {
                        var addValue = !omitEmpty || !string.IsNullOrEmpty(value);
                        if (addValue)
                        {
                            items.Add(string.Concat(parameters.GetKey(i), @equals, HttpUtility.UrlEncode(value)));
                        }
                    }
                }
                    
            }

            return string.Join(delimiter, items.ToArray());
        }
    }
}