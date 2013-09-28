using System;

namespace UmbracoTemplate.Exceptions
{
    public abstract class ExceptionFormatter<TException> : IExceptionFormatter<TException>
        where TException : Exception
    {
        /// <summary>
        /// Formats an exception for display or logging purposes
        /// </summary>
        /// <param name="exception">The exception to format</param>
        /// <returns>THe formatted message</returns>
        protected abstract string FormatException(TException exception);
        
        public string Format(TException exception)
        {
            return FormatException(exception);
        }

        public string Format(Exception exception)
        {
            return FormatException((TException) exception);
        }
    }
}
