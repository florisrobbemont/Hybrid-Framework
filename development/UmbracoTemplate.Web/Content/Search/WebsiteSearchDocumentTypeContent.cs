namespace UmbracoTemplate.Web.Content.Search
{
    /// <summary>
    /// Represents the content found in a searchable node
    /// </summary>
    public class WebsiteSearchDocumentTypeContent
    {
        /// <summary>
        /// The name of the found content
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The title of the found content
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A portion of the content found
        /// </summary>
        public string Content { get; set; }
    }
}