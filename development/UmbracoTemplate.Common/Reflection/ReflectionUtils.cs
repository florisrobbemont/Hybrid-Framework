using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UmbracoTemplate.Common.Reflection
{
    /// <summary>
    /// Class holding utils for reflection
    /// </summary>
    public static class ReflectionUtils
    {
        #region "Old - To be added to Reflection namespace later"

        ///// <summary>
        ///// Returns wether the given type implements the interface
        ///// </summary>
        ///// <typeparam name="T">The interface type</typeparam>
        ///// <param name="type">The type to check with</param>
        ///// <returns>true if the given type implements the interface, otherwise false</returns>
        //public static bool IsTypeWithInterface<T>(Type type) where T : class
        //{
        //    return typeof(T).IsAssignableFrom(type);
        //}

        ///// <summary>
        ///// Returns wether the given type implements the interface
        ///// </summary>
        ///// <param name="interfaceType">The interface type</param>
        ///// <param name="type">The type to check with</param>
        ///// <returns>true if the given type implements the interface, otherwise false</returns>
        //public static bool IsTypeWithInterface(Type interfaceType, Type type)
        //{
        //    return interfaceType.IsAssignableFrom(type);
        //}

        ///// <summary>
        ///// Returns wether the given type derives from a specific base class
        ///// </summary>
        ///// <param name="baseClassType">The base class type</param>
        ///// <param name="type">The type to check with</param>
        ///// <returns>true if the given type derives from the specified base class, otherwise false</returns>
        //public static bool IsTypeWithBaseClass(Type baseClassType, Type type)
        //{
        //    return baseClassType.IsAssignableFrom(type);
        //}

        ///// <summary>
        ///// Returns the attribute instance of the given class
        ///// </summary>
        ///// <typeparam name="TClass">The type of the class</typeparam>
        ///// <typeparam name="TAttribute">The type of the attribute</typeparam>
        ///// <returns>the instance of the attribute class if it exists, otherwise null</returns>
        //public static TAttribute GetClassAttribute<TClass, TAttribute>()
        //{
        //    return (TAttribute)typeof(TClass).GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault();
        //}

        ///// <summary>
        ///// Returns wether a class has a attribute
        ///// </summary>
        ///// <typeparam name="TType">The type of the class</typeparam>
        ///// <typeparam name="TAttribute">The type of the attribute</typeparam>
        ///// <returns>true if the class is marked with the attribute, otherwise false</returns>
        //public static bool IsTypeMarkedWithAttribute<TType, TAttribute>()
        //{
        //    return typeof(TType).GetCustomAttributes(typeof(TAttribute), false).Length > 0;
        //}

        ///// <summary>
        ///// Returns wether a class has a attribute
        ///// </summary>
        ///// <param name="type">The type of the class</param>
        ///// <typeparam name="TAttribute">The type of the attribute</typeparam>
        ///// <returns>true if the class is marked with the attribute, otherwise false</returns>
        //public static bool IsTypeMarkedWithAttribute<TAttribute>(Type type)
        //{
        //    return type.GetCustomAttributes(typeof(TAttribute), false).Length > 0;
        //}

        ///// <summary>
        ///// Returns wether a method has a attribute
        ///// </summary>
        ///// <param name="methodInfo">The type of the method</param>
        ///// <typeparam name="TAttribute">The type of the attribute</typeparam>
        ///// <returns>true if the method is marked with the attribute, otherwise false</returns>
        //public static bool IsMethodMarkedWithAttribute<TAttribute>(MethodInfo methodInfo)
        //{
        //    return methodInfo.GetCustomAttributes(typeof(TAttribute), false).Length > 0;
        //}

        ///// <summary>
        ///// Gets all attributes of <typeparamref name="TAttribute"/> from a property
        ///// </summary>
        ///// <typeparam name="TAttribute">The type of the attribute</typeparam>
        ///// <param name="methodInfo">The method to search</param>
        ///// <returns>A list of attributes</returns>
        //public static IEnumerable<TAttribute> GetMethodAttributes<TAttribute>(MethodInfo methodInfo)
        //{
        //    if (methodInfo == null)
        //        throw new ArgumentNullException("methodInfo");

        //    return methodInfo.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>();
        //}

        ///// <summary>
        ///// Returns wether a class has a attribute
        ///// </summary>
        ///// <param name="type">The type of the class</param>
        ///// <param name="attributeType">The type of the attribute</param>
        ///// <returns>true if the class is marked with the attribute, otherwise false</returns>
        //public static bool IsTypeMarkedWithAttribute(Type type, Type attributeType)
        //{
        //    if (type == null)
        //        return false;

        //    if (attributeType == null)
        //        throw new ArgumentNullException("attributeType");

        //    return type.GetCustomAttributes(attributeType, false).Length > 0;
        //}

        ///// <summary>
        ///// Returns wether a class has a attribute
        ///// </summary>
        ///// <param name="type">The type of the class as a string</param>
        ///// <param name="attributeType">The type of the attribute</param>
        ///// <returns>true if the class is marked with the attribute, otherwise false</returns>
        //public static bool IsTypeMarkedWithAttribute(string type, Type attributeType)
        //{
        //    return IsTypeMarkedWithAttribute(Type.GetType(type, false), attributeType);
        //}

        ///// <summary>
        ///// Searches all loaded assemblies for classes marked with the attribute passed in.
        ///// </summary>
        ///// <param name="searchGac">Wether to search the GAC</param>
        ///// <typeparam name="TAttribute">The type of the attribute</typeparam>
        ///// <returns>A list of appropriate types</returns>
        //public static IEnumerable<Type> FindClassesMarkedWithAttribute<TAttribute>(bool searchGac)
        //{
        //    var typeList = new List<Type>();

        //    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        //    {
        //        // skip if the assembly is part of the GAC
        //        if (!searchGac && assembly.GlobalAssemblyCache)
        //            continue;

        //        typeList.AddRange(FindClassesMarkedWithAttribute(assembly, typeof(TAttribute)));
        //    }

        //    return typeList.Distinct();
        //}

        ///// <summary>
        ///// Searches all loaded assemblies for classes marked with the attribute passed in.
        ///// </summary>
        ///// <param name="attributeType">The type of the attribute</param>
        ///// <param name="searchGac">Wether to search the GAC</param>
        ///// <returns>A list of appropriate types</returns>
        //public static IEnumerable<Type> FindClassesMarkedWithAttribute(Type attributeType, bool searchGac)
        //{
        //    var typeList = new List<Type>();

        //    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        //    {
        //        // skip if the assembly is part of the GAC
        //        if (!searchGac && assembly.GlobalAssemblyCache)
        //            continue;

        //        typeList.AddRange(FindClassesMarkedWithAttribute(assembly, attributeType));
        //    }
        //    return typeList.Distinct();
        //}

        ///// <summary>
        ///// Retusn all type in the assembly that are marked with the attribute
        ///// </summary>
        ///// <param name="searchableAssembly">The assembly being searched</param>
        ///// <param name="attributeType">The type of the attribute</param>
        ///// <returns>A list of appropriate types</returns>
        //public static IEnumerable<Type> FindClassesMarkedWithAttribute(Assembly searchableAssembly, Type attributeType)
        //{
        //    try
        //    {
        //        // Get all types where the attribute is present
        //        return searchableAssembly.GetTypes().Where(type => type.GetCustomAttributes(attributeType, false).Length > 0);
        //    }
        //    catch (ReflectionTypeLoadException ex)
        //    {
        //        // return the types that were loaded, ignore those that could not be loaded
        //        return ex.Types;
        //    }
        //}

        ///// <summary>
        ///// Gets all properties of a class that are marked with a specific attribute
        ///// </summary>
        ///// <typeparam name="TAttribute">The attribute type</typeparam>
        ///// <param name="classType">The class type</param>
        ///// <returns>A dictiory of Properties and attributes</returns>
        //public static Dictionary<PropertyInfo, TAttribute> GetClassPropertiesAndAttributes<TAttribute>(Type classType)
        //{
        //    if (classType == null)
        //        throw new ArgumentNullException("classType");

        //    var propertyDictionary = new Dictionary<PropertyInfo, TAttribute>();

        //    foreach (var propertyInfo in classType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        //    {
        //        var customAttributes = GetPropertyAttributes<TAttribute>(propertyInfo).ToList();
        //        if (customAttributes.Any())
        //        {
        //            propertyDictionary.Add(propertyInfo, customAttributes.FirstOrDefault());
        //        }
        //    }

        //    return propertyDictionary;
        //}

        ///// <summary>
        ///// Gets all method of a class that are marked with a specific attribute
        ///// </summary>
        ///// <typeparam name="TAttribute">The attribute type</typeparam>
        ///// <param name="classType">The class type</param>
        ///// <returns>A dictiory of methods and attributes</returns>
        //public static Dictionary<MethodInfo, TAttribute> GetClassMethodsAndAttributes<TAttribute>(Type classType)
        //{
        //    if (classType == null)
        //        throw new ArgumentNullException("classType");

        //    var methodsDictionary = new Dictionary<MethodInfo, TAttribute>();

        //    foreach (var methodInfo in classType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic))
        //    {
        //        var customAttributes = GetMethodAttributes<TAttribute>(methodInfo).ToList();
        //        if (customAttributes.Any())
        //        {
        //            methodsDictionary.Add(methodInfo, customAttributes.FirstOrDefault());
        //        }
        //    }

        //    return methodsDictionary;
        //}

        ///// <summary>
        ///// Gets all attributes of <typeparamref name="TAttribute"/> from a property
        ///// </summary>
        ///// <typeparam name="TAttribute">The type of the attribute</typeparam>
        ///// <param name="propertyInfo">The property to search</param>
        ///// <returns>A list of attributes</returns>
        //public static IEnumerable<TAttribute> GetPropertyAttributes<TAttribute>(PropertyInfo propertyInfo)
        //{
        //    if (propertyInfo == null)
        //        throw new ArgumentNullException("propertyInfo");

        //    return propertyInfo.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>();
        //}

        ///// <summary>
        ///// Gets all attributes of <paramref name="attributeType"/> from a property
        ///// </summary>
        ///// <param name="propertyInfo">The property to search</param>
        ///// <param name="attributeType">The type of the attribute</param>
        ///// <returns>A list of attributes</returns>
        //public static IEnumerable<object> GetPropertyAttributes(PropertyInfo propertyInfo, Type attributeType)
        //{
        //    if (propertyInfo == null)
        //        throw new ArgumentNullException("propertyInfo");

        //    return propertyInfo.GetCustomAttributes(attributeType, false);
        //}

        ///// <summary>
        ///// Creates a new instance of a typename
        ///// </summary>
        ///// <typeparam name="TType">The type of the return instance</typeparam>
        ///// <param name="typeName">The full name (including assembly and namespaces) of the type to create</param>
        ///// <returns>
        ///// A new instance of the type if it is (or boxable to) <typeparamref name="TType"/>,
        ///// otherwise the default of <typeparamref name="TType"/>
        ///// </returns>
        //public static TType CreateTypeInstance<TType>(string typeName) where TType : class
        //{
        //    Type classType = Type.GetType(typeName, false);

        //    if (classType == null)
        //        return default(TType);

        //    object newType = Activator.CreateInstance(classType);

        //    return newType as TType;
        //}

        ///// <summary>
        ///// Returns the <c>System.Type</c> corresponding to the type string
        ///// </summary>
        ///// <param name="type">The input type string</param>
        ///// <returns>A <c>System.Type</c> object if the type was found, otherwise null</returns>
        //public static Type GetTypeFromString(string type)
        //{
        //    return Type.GetType(type, false, true);
        //}

        ///// <summary>
        ///// Determines wheter the object is actualy a proxy, or the real POCO class
        ///// </summary>
        ///// <param name="type">The object you want to check</param>
        ///// <returns></returns>
        //public static bool IsProxy(object type)
        //{
        //    return type != null && ObjectContext.GetObjectType(type.GetType()) != type.GetType();
        //}

        ///// <summary>
        ///// Returns the underlying type from a Proxy class
        ///// </summary>
        ///// <param name="type">The object to get the underlying type for</param>
        ///// <returns>The underlying type of the proxy class</returns>
        //public static Type GetProxyUnderlyingType(object type)
        //{
        //    if (type == null)
        //        throw new ArgumentNullException("type");

        //    return IsProxy(type) ? ObjectContext.GetObjectType(type.GetType()) : type.GetType();
        //}

        ///// <summary>
        ///// Gets all generic parameters from the interfaces implemented by the object
        ///// </summary>
        ///// <param name="type">The object to get the interface's generic parameters from</param>
        ///// <returns>A collection of types containing information about the generic parameters</returns>
        //public static IEnumerable<Type> GetGenericParametersFromInterface(object type)
        //{
        //    return type.GetType()
        //                    .GetInterfaces()
        //                    .SelectMany(i => i.GetGenericArguments())
        //                    .ToArray();
        //}

        ///// <summary>
        ///// Determines wheter the given type is a generic and equals the given type definition
        ///// </summary>
        ///// <param name="type">The type to check</param>
        ///// <param name="genericTypeDef">The generic type definition the type must be equal to</param>
        ///// <returns>true if the type is generic and equals the type definition, otherwise false</returns>
        //public static bool IsGenericType(Type type, Type genericTypeDef)
        //{
        //    return type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDef;
        //}

        #endregion

        /// <summary>
        /// Find all classes in the given assembly that are of a specific interface
        /// </summary>
        /// <typeparam name="TType">The type of the interface to get</typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypesWithInterface<TType>(Assembly assembly)
            where TType : class
        {
            return assembly.GetTypes()
                           .Where(p => typeof(TType).IsAssignableFrom(p))
                           .Where(p => p.IsClass && !p.IsAbstract);
        } 

        /// <summary>
        /// Find all classes in the given assembly that are of a specific interface
        /// </summary>
        /// <typeparam name="TType">The type of the interface to get</typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<TType> GetClassesWithInterface<TType>(Assembly assembly)
            where TType : class
        {
            return assembly.GetTypes()
                           .Where(p => typeof(TType).IsAssignableFrom(p))
                           .Where(p => p.IsClass)
                           .Select(p => Activator.CreateInstance(p) as TType);
        } 

        /// <summary>
        /// Returns the <c>System.Type</c> corresponding to the type string. This method will create an instance of the type string to resolve its type.
        /// </summary>
        /// <param name="type">The input type string</param>
        /// <returns>A <c>System.Type</c> object if the type was found, otherwise null</returns>
        public static Type GetTypeFromStringUsingLoop(string type)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.FullName == type);
        }

        /// <summary>
        /// Returns all the generic types from the underlying interface
        /// </summary>
        public static IEnumerable<Type> GetGenericParameters(object type)
        {
            return type.GetType()
                       .GetInterfaces()
                       .SelectMany(i => i.GetGenericArguments())
                       .ToArray();
        }
    }
}