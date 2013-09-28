using System.Web.Optimization;

namespace UmbracoTemplate.Web.Bundles
{
    /// <summary>
    /// Registeres the javascript bundles
    /// </summary>
    public class JavascriptBundleConfiguration : IBundleConfiguration
    {
        public void RegisterBundle(BundleCollection bundleCollection, IBundleTransform cssTransformer, 
                                   IBundleTransform jsTransformer, IBundleOrderer bundleOrderer)
        {
            var javascriptBundle = new ScriptBundle("~/bundle/javascript.js")
                .Include(
                    "~/scripts/jquery-1.10.2.js",
                    "~/umbraco/plugins/TrackMedia/Tracking.js",
                    "~/scripts/jquery.validate.js",
                    "~/scripts/slimmage.js",
                    "~/scripts/functions.js"
                );

            javascriptBundle.Transforms.Add(jsTransformer);
            javascriptBundle.Orderer = bundleOrderer;

            bundleCollection.Add(javascriptBundle);
        }
    }
}