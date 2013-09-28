using System.Collections.Generic;

namespace UmbracoTemplate.Web.Content.Search
{
    public interface IWebsiteSearcherContext
    {
        /// <summary>
        /// Searches the examine index and returns the results
        /// </summary>
        /// <param name="searchTerm">The term to be searched.</param>
        /// <param name="maxResults">The maximum amount of results returned.</param>
        /// <param name="highlight">Wether to highligt the words.</param>
        IEnumerable<WebsiteSearchResult> Search(string searchTerm, int maxResults, bool highlight);

        /// <summary>
        /// Searches the examine index and returns the results
        /// </summary>
        /// <param name="argument">The search arguments.</param>
        IEnumerable<WebsiteSearchResult> Search(WebsiteSearchArgument argument);

        /// <summary>
        /// Rebuilds the website search index.
        /// </summary>
        void Rebuild();
    }
}