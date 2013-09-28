using System.Web.Optimization;

namespace UmbracoTemplate.Web.Bundles
{
    /// <summary>
    /// Registeres the style bundles
    /// </summary>
    public class StyleBundleConfiguration : IBundleConfiguration
    {
        public void RegisterBundle(BundleCollection bundleCollection, IBundleTransform cssTransformer, IBundleTransform jsTransformer,
                                   IBundleOrderer bundleOrderer)
        {
            var styleBundle = new StyleBundle("~/bundle/styles.css")
                .Include(
                    "~/css/main.css"
                );

            styleBundle.Transforms.Add(cssTransformer);
            styleBundle.Orderer = bundleOrderer;

            bundleCollection.Add(styleBundle);
        }
    }
}