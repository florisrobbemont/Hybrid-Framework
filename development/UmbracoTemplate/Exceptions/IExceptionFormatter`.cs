using System;

namespace UmbracoTemplate.Exceptions
{
    /// <summary>
    /// Formats an exception for display or logging purposes
    /// </summary>
    public interface IExceptionFormatter<in TException> : IExceptionFormatter
        where TException : Exception
    {
        /// <summary>
        /// Formats an exception for display or logging purposes
        /// </summary>
        /// <param name="exception">The exception to format</param>
        /// <returns>THe formatted message</returns>
        string Format(TException exception);
    }
}
