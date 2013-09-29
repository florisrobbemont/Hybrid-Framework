using System;

namespace UmbracoTemplate.Common
{
    /// <summary>
    /// Provides utilities for argument validation
    /// </summary>
    public static class ArgumentValidation
    {
        /// <summary>
        /// Determines if an object hold a numerical value
        /// </summary>
        /// <param name="input">The object to chek</param>
        /// <returns>true if the object is numeric, otherwise false</returns>
        public static bool IsNumeric(object input)
        {
            if (input == null) return false;

            double outputValue;
            return double.TryParse(input.ToString(), out outputValue);
        }

        /// <summary>
        /// Determines if an object is DbNull
        /// </summary>
        /// <param name="input">the object to check</param>
        /// <returns>true if the object is DbNull, otherwise false</returns>
        public static bool IsDbNull(object input)
        {
            return (input == DBNull.Value);
        }
    }
}