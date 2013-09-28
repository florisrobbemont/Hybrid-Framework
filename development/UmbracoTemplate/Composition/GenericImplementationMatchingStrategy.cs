using System;
using Castle.Core;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Handlers;

namespace UmbracoTemplate.Composition
{
    /// <summary>
    /// Strategy class for selecting components based on a type given in the arguments of the factory method.
    /// </summary>
    public class GenericImplementationMatchingStrategy : IGenericImplementationMatchingStrategy
    {
        public Type[] GetGenericArguments(ComponentModel model, CreationContext context)
        {
            if (context.AdditionalArguments.Count > 0)
            {
                var typeArgument = context.AdditionalArguments["type"];

                if (typeArgument is Type)
                {
                    return new [] {typeArgument as Type};
                }
            }

            return null;
        }
    }
}
