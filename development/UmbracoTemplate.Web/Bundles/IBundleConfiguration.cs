using System.Web.Optimization;

namespace UmbracoTemplate.Web.Bundles
{
    /// <summary>
    /// Provides support for registering bundle configurations
    /// </summary>
    public interface IBundleConfiguration
    {
        /// <summary>
        /// Adds bundle configurations to the given <c>BundleCollection</c>
        /// </summary>
        /// <param name="bundleCollection">The <c>BundleCollection</c> on which to add new bundles</param>
        /// <param name="cssTransformer">The CSS <c>IBundleTransform</c> to be used</param>
        /// <param name="jsTransformer">The JS <c>IBundleTransform</c> to be used</param>
        /// <param name="bundleOrderer">The <c>IBundleOrderer</c> to be used</param>
        void RegisterBundle(BundleCollection bundleCollection, 
                            IBundleTransform cssTransformer, 
                            IBundleTransform jsTransformer, 
                            IBundleOrderer bundleOrderer);
    }
}
