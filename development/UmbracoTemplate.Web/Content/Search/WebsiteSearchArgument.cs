namespace UmbracoTemplate.Web.Content.Search
{
    /// <summary>
    /// Search arfument class for passing search results around.
    /// </summary>
    public class WebsiteSearchArgument
    {
        /// <summary>
        /// Constructs a new SearchArgument class
        /// </summary>
        /// <param name="searchTerm">The term to be searched.</param>
        /// <param name="maxResults">The maximum amount of results returned.</param>
        /// <param name="highlight">Wether to highligt the words.</param>
        public WebsiteSearchArgument(string searchTerm, int maxResults, bool highlight)
        {
            SearchTerm = searchTerm;
            MaxResults = maxResults;
            Highlight = highlight;
        }

        /// <summary>
        /// The term to be searched.
        /// </summary>
        public string SearchTerm { get; set; }

        /// <summary>
        /// The maximum amount of results returned.
        /// </summary>
        public int MaxResults { get; set; }

        /// <summary>
        /// Wether to highligt the words.
        /// </summary>
        public bool Highlight { get; set; }
    }
}