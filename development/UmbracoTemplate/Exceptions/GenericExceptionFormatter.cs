using System;

namespace UmbracoTemplate.Exceptions
{
    /// <summary>
    /// Provides generic exception formatting
    /// </summary>
    public class GenericExceptionFormatter : ExceptionFormatter<Exception>
    {
        protected override string FormatException(Exception exception)
        {
            return exception.Message;
        }
    }
}
