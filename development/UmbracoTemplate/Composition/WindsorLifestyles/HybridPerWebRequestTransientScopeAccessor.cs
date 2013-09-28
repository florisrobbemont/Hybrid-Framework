namespace UmbracoTemplate.Composition.WindsorLifestyles {
    public class HybridPerWebRequestTransientScopeAccessor : HybridPerWebRequestScopeAccessor {
        public HybridPerWebRequestTransientScopeAccessor() : 
            base(new TransientScopeAccessor()) {}
    }
}
