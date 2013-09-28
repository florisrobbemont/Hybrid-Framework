namespace UmbracoTemplate.Web.Content.Search
{
    /// <summary>
    /// Represents a document type for the search engine
    /// </summary>
    public abstract class WebsiteSearchDocumentType
    {
        /// <summary>
        /// Gets the document type of this search class
        /// </summary>
        public string DocumentType { get; private set; }

        /// <summary>
        /// Find the node and fetches the appropriate content for the search entry
        /// </summary>
        public abstract WebsiteSearchDocumentTypeContent FindSearchContent(int nodeId);

        /// <summary>
        /// Constructs a new document search type
        /// </summary>
        /// <param name="documentType">The document type this search class is associated with</param>
        protected WebsiteSearchDocumentType(string documentType)
        {
            DocumentType = documentType;
        }
    }
}