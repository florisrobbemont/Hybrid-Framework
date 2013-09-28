using System;

namespace UmbracoTemplate.Exceptions
{
    public interface IExceptionFormatterFactory
    {
        IExceptionFormatter<TException> Create<TException>() where TException : Exception;
            
        IExceptionFormatter Create(Type type);
    }
}
