using System;
using System.Linq;
using Castle.Core;
using UmbracoTemplate.Common.Reflection;

namespace UmbracoTemplate.Composition
{
    internal static class ComponentModelExtensions
    {
        /// <summary>
        /// Searches a registration and all it's implementations and services for a specific attribute 
        /// </summary>
        internal static bool SearchRegistrationForAttribute<TAttribute>(this ComponentModel componentModel)
        {
            return SearchImplementationForAttribute<TAttribute>(componentModel) || SearchServicesForAttribute<TAttribute>(componentModel);
        }

        private static bool SearchServicesForAttribute<TAttribute>(ComponentModel componentModel)
        {
            try
            {
                var services = componentModel.Services.Select(ReflectionCache.GetReflection).ToList();

                if (services.Any(x => x.Attributes.Any(a => a is TAttribute)))
                    return true;

                if (services.SelectMany(x => x.Methods).Any(x => x.Attributes.Any(a => a is TAttribute)))
                    return true;
            }
            catch (ArgumentNullException)
            {
            }

            return false;
        }

        private static bool SearchImplementationForAttribute<TAttribute>(ComponentModel componentModel)
        {
            try
            {
                var implementation = ReflectionCache.GetReflection(componentModel.Implementation);

                if (implementation.Attributes.Any(x => x is TAttribute))
                    return true;

                if (implementation.Methods.Any(x => x.Attributes.Any(a => a is TAttribute)))
                    return true;
            }
            catch (ArgumentNullException)
            {
            }

            return false;
        }
    }
}
