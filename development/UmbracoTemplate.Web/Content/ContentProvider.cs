using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Umbraco.Core.Models;
using umbraco.NodeFactory;
using Umbraco.Web;

namespace UmbracoTemplate.Web.Content
{
    public class ContentProvider : IContentProvider
    {
       public IPublishedContent GetHomeNode(IPublishedContent currentNode)
        {
            var rootNode = currentNode.AncestorOrSelf("Language");
            return rootNode.Children.FirstOrDefault(x => x.IsDocumentType("Home"));
        }
    }
}
