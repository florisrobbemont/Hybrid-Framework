using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UmbracoTemplate.Kernel
{
    public abstract class KernelEvent : IKernelEvent
    {
        /// <summary>
        /// Returns a succesfull argument
        /// </summary>
        protected virtual KernelEventCompletedArguments Succes()
        {
            return new KernelEventCompletedArguments
                {
                    AllowContinue = true,
                    Exceptions = new List<Exception>(),
                    KernelEventSucceeded = true
                };
        }

        /// <summary>
        /// Executes the method for this class
        /// </summary>
        /// <returns>A <c>KernelEventCompletedArguments</c> object holding information about the event</returns>
        public abstract KernelEventCompletedArguments Execute();

        /// <summary>
        /// Gets the group for this event
        /// </summary>
        public abstract string EventGroup { get; }

        /// <summary>
        /// Gets the type for this event
        /// </summary>
        public abstract string EventType { get; }

        /// <summary>
        /// Gets the priority for this kernel event
        /// </summary>
        public abstract int Priority { get; }

        /// <summary>
        /// Gets the display name of this event
        /// </summary>
        public abstract string DisplayName { get; }
    }
}
