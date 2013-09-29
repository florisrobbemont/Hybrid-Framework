using System;
using System.Text.RegularExpressions;

namespace UmbracoTemplate.Common.Extensions
{
    public static class StringExtensions
    {
        public static string Start(this string input, int length)
        {
            return Strings.Start(input, length);
        }

        public static string End(this string input, int length)
        {
            return Strings.End(input, length);
        }

        public static string CutStart(this string input, int length)
        {
            return Strings.CutStart(input, length);
        }

        public static string CutEnd(this string input, int length)
        {
            return Strings.CutEnd(input, length);
        }

        public static string CutEnd(this string input, int length, string endString)
        {
            return Strings.CutEnd(input, length, endString);
        }

        public static int OccurenceCount(this string input, string pattern)
        {
            return Strings.OccurenceCount(input, pattern);
        }

        public static string[] GetOccurences(this string input, string pattern)
        {
            return Strings.GetOccurences(input, pattern);
        }

        public static string ReplaceAll(this string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement);
        }

        public static string[] Split(this string input, string pattern)
        {
            return Regex.Split(input, pattern);
        }

        public static string[] Split(this string input, string pattern, RegexOptions options)
        {
            return Regex.Split(input, pattern, options);
        }

        public static string Join(this string[] input)
        {
            return string.Join("", input);
        }

        public static string Join(this string[] input, string seperator)
        {
            return string.Join(seperator, input);
        }

        public static string Replace(this string str, string oldValue, string newValue, StringComparison comparison)
        {
            return Strings.Replace(str, oldValue, newValue, comparison);
        }

        public static string FormatDateTimeString(this string dateString, string format)
        {
            if (string.IsNullOrEmpty(dateString))
                return string.Empty;
            
            var dateTime = DateTime.Parse(dateString);
            return dateTime.ToString(format);
        }
    }
}