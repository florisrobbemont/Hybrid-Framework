using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace UmbracoTemplate.Common.Extensions
{
    public static class LinqExtensions
    {
        public static string GetDelimitedList(this IEnumerable<int> currentList, char delimiter)
        {
            return string.Join(delimiter.ToString(CultureInfo.InvariantCulture), currentList.ToArray());
        }

        public static string GetDelimitedList(this IEnumerable<string> currentList, char delimiter)
        {
            return string.Join(delimiter.ToString(CultureInfo.InvariantCulture), currentList.ToArray());
        }

        public static string GetJavascriptArray(this IEnumerable<string> currentList)
        {
            return string.Join("','", currentList.ToArray());
        }
    }
}