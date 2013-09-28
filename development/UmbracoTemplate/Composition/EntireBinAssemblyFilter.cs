using System;
using Castle.MicroKernel.Registration;

namespace UmbracoTemplate.Composition
{
    /// <summary>
    /// Installer filter to get all the assemblies in the current bin folder
    /// </summary>
    public class EntireBinAssemblyFilter : AssemblyFilter
    {
        public EntireBinAssemblyFilter()
            : base(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll") { }
    }
  
}
