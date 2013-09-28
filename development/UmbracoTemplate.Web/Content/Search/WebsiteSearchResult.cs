namespace UmbracoTemplate.Web.Content.Search
{
    /// <summary>
    /// Represents a single search result
    /// </summary>
    public class WebsiteSearchResult
    {
        /// <summary>
        /// Gets the PageId for this result
        /// </summary>
        public int PageId { get; private set; }

        /// <summary>
        /// Gets the ammount of times the search term was found in this result
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the name found in this search result
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the title found in this search result
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the content found in this search result
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Gets the document type for this search result
        /// </summary>
        public WebsiteSearchDocumentType DocumentType { get; private set; }

        /// <summary>
        /// Updates the count by one
        /// </summary>
        public void UpdateCount()
        {
            Count++;
        }

        /// <summary>
        /// Updates the content of this reulst
        /// </summary>
        public void UpdateContent(string name, string title, string content)
        {
            Name = name;
            Title = title;
            Content = content;
        }

        public WebsiteSearchResult(int pageId, int count, WebsiteSearchDocumentType documentType)
        {
            PageId = pageId;
            Count = count;
            Content = "";
            Name = "";
            Title = "";
            DocumentType = documentType;
        }
    }
}