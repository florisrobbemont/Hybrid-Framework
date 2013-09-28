using System.Collections.Generic;
using System.Web.Optimization;
using UmbracoTemplate.Kernel;
using UmbracoTemplate.Web.Bundles;

namespace UmbracoTemplate.Web.Boot
{
    public class BundleStartup : KernelEvent
    {
        private readonly IEnumerable<IBundleConfiguration> configurations;

        public BundleStartup(IEnumerable<IBundleConfiguration> configurations)
        {
            this.configurations = configurations;
        }

        public override KernelEventCompletedArguments Execute()
        {
            // These transformers are the default transformers of the system. If you need optimized ones, change them here
            var cssTransformer = new CssMinify();
            var jsTransformer = new JsMinify();

            // This bundle orderer does nothing to the order of the files in a bundle. It will allow you to control the order.
            // The order you add files to the bundle will be the order in the minified file
            var unmodifiedBundleOrderer = new UnmodifiedBundleOrderer();

            foreach (var bundleConfig in configurations)
            {
                bundleConfig.RegisterBundle(BundleTable.Bundles, cssTransformer, jsTransformer, unmodifiedBundleOrderer);
            }

            return Succes();
        }

        public override string EventGroup
        {
            get { return "Startup"; }
        }

        public override string EventType
        {
            get { return "BundleStartup"; }
        }

        public override int Priority
        {
            get { return 1; }
        }

        public override string DisplayName
        {
            get { return "Bundle configuration startup"; }
        }
    }
}