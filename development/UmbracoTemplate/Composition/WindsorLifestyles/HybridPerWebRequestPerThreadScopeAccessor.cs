using Castle.MicroKernel.Lifestyle;

namespace UmbracoTemplate.Composition.WindsorLifestyles {
    public class HybridPerWebRequestPerThreadScopeAccessor: HybridPerWebRequestScopeAccessor {
        public HybridPerWebRequestPerThreadScopeAccessor() :
            base(new ThreadScopeAccessor()) { }
    }
}
