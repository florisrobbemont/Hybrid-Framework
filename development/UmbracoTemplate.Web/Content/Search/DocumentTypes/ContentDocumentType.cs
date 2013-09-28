using Umbraco.Web;

namespace UmbracoTemplate.Web.Content.Search.DocumentTypes
{
    public class ContentDocumentType : WebsiteSearchDocumentType
    {
        private readonly IUmbracoHelperFactory umbracoHelperFactory;

        public ContentDocumentType(IUmbracoHelperFactory umbracoHelperFactory)
            : base("Content")
        {
            this.umbracoHelperFactory = umbracoHelperFactory;
        }
        
        public override WebsiteSearchDocumentTypeContent FindSearchContent(int nodeId)
        {
            var contentPage = umbracoHelperFactory.GetCurrent().TypedContent(nodeId);

            return new WebsiteSearchDocumentTypeContent()
            {
                Name = contentPage.Name,
                Title = contentPage.GetPropertyValue<string>("contentTitleH1"),
                Content = contentPage.GetPropertyValue<string>("contentRichText")
            };
        }
    }
}