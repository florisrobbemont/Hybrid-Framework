using System;
using System.Collections.Generic;
using System.Linq;
using UmbracoTemplate.Logging;

namespace UmbracoTemplate.Kernel
{
    public class KernelContext : IKernelContext
    {
        private readonly ILoggingService loggingService;
        private readonly IEnumerable<IKernelEvent> kernelEvents;

        public KernelContext(ILoggingService loggingService, IEnumerable<IKernelEvent> kernelEvents)
        {
            this.loggingService = loggingService;
            this.kernelEvents = kernelEvents.ToList();
        }

        public void Dispose()
        {
        }

        #region "Kernel events"

        public void RunKernelEventGroups(params string[] groups)
        {
            groups.ToList().ForEach(group => RunKernelEvents(kernelEvents.Where(x => x.EventGroup == group).OrderBy(x => x.Priority)));
        }

        public void RunKernelEvents(params string[] types)
        {
            foreach (var type in types)
            {
                var localType = type;
                RunKernelEvents(kernelEvents.Where(x => x.EventType == localType)
                                            .OrderBy(x => x.Priority).ToList());
            }
        }

        public void RunKernelEvents(params IKernelEvent[] events)
        {
            RunKernelEvents(events.AsEnumerable());
        }

        /// <summary>
        /// Runs all kernel events in the list
        /// </summary>
        /// <param name="kernelList">A list of kernel events</param>
        private void RunKernelEvents(IEnumerable<IKernelEvent> kernelList)
        {
            foreach (var kernelEvent in kernelList)
            {
                KernelEventCompletedArguments kernelEventArguments = null;
                Exception kernelEventException = null;

                try
                {
                    kernelEventArguments = kernelEvent.Execute();
                }
                catch (Exception ex)
                {
                    kernelEventException = ex;
                }

                HandleKernelEventArguments(kernelEventArguments, kernelEventException);
            }
        }

        /// <summary>
        /// Handles the result of a tasks and throws an exception when necessary
        /// </summary>
        /// <param name="kernelEventArguments">The result param from an executed task</param>
        /// <param name="kernelEventException">The optional exception thrown when executing the task</param>
        private void HandleKernelEventArguments(KernelEventCompletedArguments kernelEventArguments, Exception kernelEventException)
        {
            if (kernelEventException != null)
            {
                // We got an exception during kernel event time, log this issue with Critical
                loggingService.Fatal("", kernelEventException);

                throw new KernelEventIncompleteException(kernelEventException);
            }

            // Check if the kernel event returned a value
            if (kernelEventArguments == null)
                kernelEventArguments = new KernelEventCompletedArguments()
                {
                    KernelEventSucceeded = false,
                    AllowContinue = false,
                    Exceptions = new List<Exception>()
                };

            if (!kernelEventArguments.KernelEventSucceeded) // Kernel event didn't succeed, log error
            {
                foreach (var exception in kernelEventArguments.Exceptions)
                {
                    loggingService.Fatal("Error executing kernel event", exception);    
                }
            }

            if (!kernelEventArguments.AllowContinue) // If we're not allowed to continue, throw exception to stop execution
                throw new KernelEventIncompleteException();
        }

        #endregion "Kernel events"
    }
}