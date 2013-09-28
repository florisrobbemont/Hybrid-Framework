using System.Collections.Generic;
using System.IO;
using System.Web.Optimization;

namespace UmbracoTemplate.Web.Bundles
{
    internal class UnmodifiedBundleOrderer : IBundleOrderer
    {
        public IEnumerable<FileInfo> OrderFiles(BundleContext context, IEnumerable<FileInfo> files)
        {
            return files;
        }
    }
}