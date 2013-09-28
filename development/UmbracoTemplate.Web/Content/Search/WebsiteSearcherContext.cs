using Examine;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Highlight;
using Lucene.Net.Search;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UmbracoTemplate.Web.Content.Search
{
    /// <summary>
    /// Searches the predefined Examine index and returns results
    /// </summary>
    public class WebsiteSearcherContext : IWebsiteSearcherContext
    {
        private readonly IUmbracoHelperFactory umbracoHelperFactory;

        private const string ExamineIndex = "ExternalSearcher";
        private const int MaximumHighlights = 7;

        /// <summary>
        /// List of allowed search types
        /// </summary>
        private IEnumerable<WebsiteSearchDocumentType> WebsiteSearchDocumentTypes { get; set; }

        /// <summary>
        /// Constructs a new searcher class
        /// </summary>
        public WebsiteSearcherContext(IEnumerable<WebsiteSearchDocumentType> websiteSearchDocumentTypes, IUmbracoHelperFactory umbracoHelperFactory)
        {
            this.umbracoHelperFactory = umbracoHelperFactory;

            WebsiteSearchDocumentTypes = websiteSearchDocumentTypes;
        }

        /// <summary>
        /// Searches the examine index and returns the results
        /// </summary>
        /// <param name="searchTerm">The term to be searched.</param>
        /// <param name="maxResults">The maximum amount of results returned.</param>
        /// <param name="highlight">Wether to highligt the words.</param>
        public IEnumerable<WebsiteSearchResult> Search(string searchTerm, int maxResults, bool highlight)
        {
            return Search(new WebsiteSearchArgument(searchTerm, maxResults, highlight));
        }

        /// <summary>
        /// Searches the examine index and returns the results
        /// </summary>
        /// <param name="argument">The search arguments.</param>
        public IEnumerable<WebsiteSearchResult> Search(WebsiteSearchArgument argument)
        {
            var searcher = ExamineManager.Instance.SearchProviderCollection[ExamineIndex];
            var searchResults = searcher.Search(argument.SearchTerm.ToLower(), false);

            return ProcessSearchResults(searchResults, argument);
        }

        
        /// <summary>
        /// Rebuilds the website search index.
        /// </summary>
        public void Rebuild()
        {
            ExamineManager.Instance.IndexProviderCollection[ExamineIndex].RebuildIndex();
        }

        /// <summary>
        /// Transforms the search results into a collection of results
        /// </summary>
        private IEnumerable<WebsiteSearchResult> ProcessSearchResults(ISearchResults searchResults, WebsiteSearchArgument argument)
        {
            var umbracoHelper = umbracoHelperFactory.GetCurrent();
            var results = new List<WebsiteSearchResult>();

            foreach (var searchResultObj in searchResults.Where(x => x.Fields["__IndexType"] != "media")
                                                         .OrderByDescending(x => x.Score))
            {
                if (results.Count == argument.MaxResults)
                    break;

                var currentNode = umbracoHelper.TypedContent(searchResultObj.Id);
                var currentNodeAlias = searchResultObj.Fields["nodeTypeAlias"];
                var currentAllowedType = WebsiteSearchDocumentTypes.FirstOrDefault(x => x.DocumentType == currentNodeAlias);

                while (currentAllowedType == null)
                {
                    if (currentNode.Parent == null)
                        break;

                    currentNode = umbracoHelper.TypedContent(currentNode.Parent.Id);
                    currentNodeAlias = currentNode.DocumentTypeAlias;
                    currentAllowedType = WebsiteSearchDocumentTypes.FirstOrDefault(x => x.DocumentType == currentNodeAlias);
                }

                if (currentAllowedType == null)
                    continue;

                if (results.Any(x => x.PageId == currentNode.Id))
                    results.First(x => x.PageId == currentNode.Id).UpdateCount();
                else
                    results.Add(new WebsiteSearchResult(currentNode.Id, 1, currentAllowedType));
            }

            return ProcessSearchResultsContent(results, searchResults, argument);
        }

        /// <summary>
        /// Handles all the different content types and add the content
        /// </summary>
        private IEnumerable<WebsiteSearchResult> ProcessSearchResultsContent(List<WebsiteSearchResult> results, ISearchResults searchResults, WebsiteSearchArgument argument)
        {
            foreach (var result in results)
            {
                var content = result.DocumentType.FindSearchContent(result.PageId);

                result.UpdateContent(
                    content.Name,
                    content.Title,
                    HandleContent(searchResults, content.Content, argument)
                );
            }

            return results;
        }

        /// <summary>
        /// Parses the found content
        /// </summary>
        private string HandleContent(ISearchResults searchResults, string content, WebsiteSearchArgument argument)
        {
            content = umbraco.library.StripHtml(content);

            if (argument.Highlight)
                content = HighlightWords(searchResults, content, argument);

            return content;
        }

        /// <summary>
        /// Finds the higlighted words and returns the trimmed content (in HTML)
        /// </summary>
        private string HighlightWords(ISearchResults searchResults, string content, WebsiteSearchArgument argument)
        {
            var results = (Examine.LuceneEngine.SearchResults)searchResults;
            var scorer = new QueryScorer(results.LuceneQuery.Rewrite(((IndexSearcher)results.LuceneSearcher).GetIndexReader()));
            var highlighter = new Highlighter(new SimpleHTMLFormatter("<span class=\"SearchHighlight\">", "</span>"), new SimpleHTMLEncoder(), scorer);
            var tokenStream = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29).TokenStream(argument.SearchTerm, new StringReader(content));

            return highlighter.GetBestFragments(tokenStream, content, MaximumHighlights, "");
        }
    }
}