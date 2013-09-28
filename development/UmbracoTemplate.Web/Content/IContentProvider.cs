using System.Collections.Generic;
using Umbraco.Core.Models;

namespace UmbracoTemplate.Web.Content
{
    public interface IContentProvider
    {
        /// <summary>
        /// Returns the first found home node
        /// </summary>
        IPublishedContent GetHomeNode(IPublishedContent currentNode);
    }
}
