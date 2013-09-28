using Umbraco.Web;

namespace UmbracoTemplate.Web.Content
{
    /// <summary>
    /// Provides access to the Umbraco Helper
    /// </summary>
    public interface IUmbracoHelperFactory
    {
        /// <summary>
        /// Gets the current Umbraco Helpers
        /// </summary>
        UmbracoHelper GetCurrent();
    }
}
