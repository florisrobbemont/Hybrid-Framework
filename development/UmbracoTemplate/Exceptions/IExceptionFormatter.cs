using System;

namespace UmbracoTemplate.Exceptions
{
    /// <summary>
    /// Formats an exception for display or logging purposes
    /// </summary>
    public interface IExceptionFormatter
    {
        /// <summary>
        /// Formats an exception for display or logging purposes
        /// </summary>
        /// <param name="exception">The exception to format</param>
        /// <returns>THe formatted message</returns>
        string Format(Exception exception);
    }
}
