using System;
using System.Threading.Tasks;
using UmbracoTemplate.Logging;

namespace UmbracoTemplate.Kernel
{
    /// <summary>
    /// Holds functionality for running Kernel events procedures
    /// </summary>
    public interface IKernelContext : IDisposable
    {
        /// <summary>
        /// Runs all Kernel events groups based on the given group name in the correct sequence
        /// </summary>
        /// <param name="groups">The Kernel groups to run</param>
        [Log]
        void RunKernelEventGroups(params string[] groups);

        /// <summary>
        /// Runs all Kernel events based on the given types in the correct sequence
        /// </summary>
        /// <param name="types">The Kernel types to run</param>
        [Log]
        void RunKernelEvents(params string[] types);

        /// <summary>
        /// Runs the given events in the order they are supplied
        /// </summary>
        /// <param name="events">Events to run</param>
        [Log]
        void RunKernelEvents(params IKernelEvent[] events);
    }
}
