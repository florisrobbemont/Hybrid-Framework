using System;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using UmbracoTemplate.Common.Reflection;

namespace UmbracoTemplate.Logging
{
    /// <summary>
    /// Provides logging for all methods and classes from the container using the [Log] attribute
    /// </summary>
    public class LoggingInterceptor : IInterceptor
    {
        private readonly ILoggingService loggingService;

        public LoggingInterceptor(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        public void Intercept(IInvocation invocation)
        {
            var logAttribute = GetLogAttribute(invocation);

            if (logAttribute == null)
            {
                invocation.Proceed();
                return;
            }

            var declaringTypeName = GetDeclaringTypeName(invocation);

            loggingService.Debug("Entering method '{0}' of class '{1}' with params: {2}", invocation.Method.Name,
                                 declaringTypeName, FormatMethodParameters(invocation));

            InterceptMethod(invocation, logAttribute);
            
            loggingService.Debug("Leaving method '{0}' of class '{1}'", invocation.Method.Name,
                                 declaringTypeName);
        }

        private void InterceptMethod(IInvocation invocation, LogAttribute logAttribute)
        {
            if (logAttribute.LogExceptions)
            {
                try
                {
                    invocation.Proceed();
                }
                catch (Exception ex)
                {
                    loggingService.Error("Exception in method '{0}'", ex, invocation.Method.Name);

                    throw; // Always throw for exception bubbling
                }
            }
            else
            {
                invocation.Proceed();
            }
        }

        private static LogAttribute GetLogAttribute(IInvocation invocation)
        {
            var implementation = ReflectionCache.GetReflection(invocation.MethodInvocationTarget.DeclaringType);
            var service = ReflectionCache.GetReflection(invocation.Method.DeclaringType);

            var implementationMethod = implementation.Methods[invocation.MethodInvocationTarget.Name].FirstOrDefault();
            var serviceMethod = service.Methods[invocation.Method.Name].FirstOrDefault();

            return FindLogAttribute(implementationMethod, serviceMethod);
        }

        private static LogAttribute FindLogAttribute(ReflectionMethod implementationMethod, ReflectionMethod serviceMethod)
        {
            var implementationAttribute = implementationMethod != null ? implementationMethod.Attributes.FirstOrDefault(x => x is LogAttribute) : null;

            if (implementationAttribute == null && serviceMethod != null)
            {
                return serviceMethod.Attributes.FirstOrDefault(x => x is LogAttribute) as LogAttribute;
            }

            return implementationAttribute as LogAttribute;
        }

        private static string GetDeclaringTypeName(IInvocation invocation)
        {
            return invocation.MethodInvocationTarget.DeclaringType == null ? "" : invocation.MethodInvocationTarget.DeclaringType.Name;
        }

        private static string FormatMethodParameters(IInvocation invocation)
        {
            var builder = new StringBuilder();
            var service = ReflectionCache.GetReflection(invocation.Method.DeclaringType);
            var parameters = service.Methods.First(x => x.Name == invocation.Method.Name).Parameters;

            for (var i = 0; i < parameters.Length; i++)
            {
                var argument = invocation.Arguments.Length > i ? invocation.Arguments[i] : null;

                if (argument != null)
                    builder.AppendFormat("{0}: '{1}'", parameters[i].Name, argument);
            }

            return builder.ToString();
        }
    }
}
