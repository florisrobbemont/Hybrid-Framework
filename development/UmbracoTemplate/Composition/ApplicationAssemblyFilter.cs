using System;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;

namespace UmbracoTemplate.Composition
{
    /// <summary>
    /// Installer filter to get only the assemblies of the current application
    /// </summary>
    public class ApplicationAssemblyFilter : AssemblyFilter
    {
        public ApplicationAssemblyFilter()
            : base(AppDomain.CurrentDomain.RelativeSearchPath, GetAssemblyPrefixName() + "*") { }

        private static string GetAssemblyPrefixName()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            return !assemblyName.Contains(".") ? assemblyName : assemblyName.Split('.').FirstOrDefault();
        }
    }
  
}
