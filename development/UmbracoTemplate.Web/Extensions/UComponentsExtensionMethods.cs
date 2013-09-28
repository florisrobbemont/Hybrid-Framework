using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Umbraco.Core.Dynamics;
using Umbraco.Core.Models;
using Umbraco.Web;
using UmbracoTemplate.Web.Models;
using uComponents.DataTypes.UrlPicker;
using uComponents.DataTypes.UrlPicker.Dto;

namespace UmbracoTemplate.Web.Extensions
{
    public static class UComponentsExtensionMethods
    {
        /// <summary>
        /// Return the nodes selected with MNTP (xml only) as IPublishedContent.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IEnumerable<IPublishedContent> GetMntpNodes(this IPublishedContent node, string propertyName)
        {
            var xml = node.GetPropertyValue<RawXmlString>(propertyName);

            if (xml != null)
            {
                var xmlData = xml.Value;

                if (!string.IsNullOrEmpty(xmlData))
                {
                    var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
                    return umbracoHelper.TypedContent(XElement.Parse(xmlData).Descendants("nodeId").Select(x => (x.Value))).Where(y => y != null);
                }
            }

            return Enumerable.Empty<IPublishedContent>();
        }

        /// <summary>
        /// Return the UrlPicker that has been selected.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static UrlPicker GetUrlPicker(this IPublishedContent content, string alias)
        {
            var urlPickerState = UrlPickerState.Deserialize(content.GetPropertyValue<string>(alias));

            if ((urlPickerState.Mode == UrlPickerMode.Content && !urlPickerState.NodeId.HasValue) || string.IsNullOrEmpty(urlPickerState.Url))
            {
                return new UrlPicker();
            }

            return new UrlPicker()
            {
                Url = urlPickerState.Mode == UrlPickerMode.Content ? Umbraco.NiceUrl(urlPickerState.NodeId.Value) : urlPickerState.Url,
                Title = urlPickerState.Title,
                NewWindow = urlPickerState.NewWindow
            };
        }

        /// <summary>
        /// Return the Multi UrlPicker that has been selected.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static IEnumerable<UrlPicker> GetMultiUrlPicker(this IPublishedContent content, string alias)
        {
            var xml = content.GetPropertyValue<RawXElement>(alias);

            if (xml != null)
            {
                var xmlData = xml.Value;

                if (xmlData != null)
                {
                    return
                    (
                        from pickerXml in xmlData.Elements("url-picker")
                        let urlPickerState = UrlPickerState.Deserialize(pickerXml.ToString())
                        where (urlPickerState.Mode == UrlPickerMode.Content && urlPickerState.NodeId.HasValue)
                        || !string.IsNullOrEmpty(urlPickerState.Url)
                        select new UrlPicker()
                        {
                            Url = urlPickerState.Mode == UrlPickerMode.Content ? Umbraco.TypedContent(urlPickerState.NodeId.Value).Url : urlPickerState.Url,
                            Title = urlPickerState.Title,
                            NewWindow = urlPickerState.NewWindow
                        }
                    );
                }
            }

            return Enumerable.Empty<UrlPicker>();
        }

        /// <summary>
        /// Return the strings that has been filled in.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetMultipleTextStrings(this IPublishedContent content, string alias)
        {
            var xml = content.GetPropertyValue<RawXElement>(alias);

            if (xml != null)
            {
                var xmlData = xml.Value;

                if (xmlData != null)
                {
                    return
                    (
                        from x in xmlData.Descendants("value")
                        select x.Value
                    );
                }
            }

            return Enumerable.Empty<string>();
        }
    }
}