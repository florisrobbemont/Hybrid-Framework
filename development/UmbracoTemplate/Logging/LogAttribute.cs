using System;

namespace UmbracoTemplate.Logging
{
    /// <summary>
    /// Provides DEBUG logging for method name parameter info and class instantiation
    /// </summary>
    public sealed class LogAttribute : Attribute
    {
        public bool LogExceptions { get; private set; }

        public LogAttribute(bool logExceptions = true)
        {
            LogExceptions = logExceptions;
        }
    }
}
