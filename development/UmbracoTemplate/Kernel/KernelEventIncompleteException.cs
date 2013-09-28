using System;

namespace UmbracoTemplate.Kernel
{
    /// <summary>
    /// Thrown when one or more kernel events procedures has failed to execute
    /// </summary>
    [Serializable]
    public class KernelEventIncompleteException : Exception
    {
        private const string DefaultExceptionMessage = "Some of the kernel events have failed! Check the log file for any specifics.";

        /// <summary>
        /// Throws the KernelEventIncompleteException using the default messsage
        /// </summary>
        public KernelEventIncompleteException()
            : base(DefaultExceptionMessage) { }

        /// <summary>
        /// Throws the KernelEventIncompleteException using the default messsage and an inner exception
        /// </summary>
        /// <param name="innerException">the inner exception to be included</param>
        public KernelEventIncompleteException(Exception innerException)
            : base(DefaultExceptionMessage, innerException) { }
    }
}