using Castle.MicroKernel.Registration;

namespace UmbracoTemplate.Composition.WindsorLifestyles
{
    public static class ComponentRegistrationExtensions
    {
        public static ComponentRegistration<TService> LifestylePerSession<TService>(this ComponentRegistration<TService> reg)
            where TService : class
        {
            return reg.LifestyleScoped<WebSessionScopeAccessor>();
        }
    }
}