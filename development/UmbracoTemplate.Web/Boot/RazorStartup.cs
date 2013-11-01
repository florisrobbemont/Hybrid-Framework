using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using RazorGenerator.Mvc;
using UmbracoTemplate.Composition;
using UmbracoTemplate.Kernel;

namespace UmbracoTemplate.Web.Boot
{
    public class RazorStartup : KernelEvent
    {
        public override KernelEventCompletedArguments Execute()
        {
            var engine = new PrecompiledMvcEngine(typeof(RazorStartup).Assembly)
            {
                UsePhysicalViewsIfNewer = Application.Debug
            };

            ViewEngines.Engines.Insert(0, engine);

            // StartPage lookups are done by WebPages. 
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
           
            return Succes();
        }

        public override string EventGroup
        {
            get { return "Startup"; }
        }

        public override string EventType
        {
            get { return "RazorStartup"; }
        }

        public override int Priority
        {
            get { return 0; }
        }

        public override string DisplayName
        {
            get { return "Razor compilation startup"; }
        }
    }
}