using System;
using System.Text;

namespace UmbracoTemplate.Common
{
    /// <summary>
    /// A collection of common string utility methods
    /// </summary>
    public static class Strings
    {
        public static bool IsEmpty(object input)
        {
            return input == null || Convert.ToString(input).Length == 0;
        }

        public static bool IsEmpty(string input)
        {
            return IsEmpty((Object)input);
        }

        public static bool IsNumeric(object input)
        {
            return RegexUtils.IsExactMatch(CutWhitespace(Convert.ToString(input)), RegexUtils.RegexNumeric);
        }

        public static bool IsEmail(string input)
        {
            return RegexUtils.IsExactMatch(input, RegexUtils.RegexEmail);
        }

        public static bool IsUrl(string input)
        {
            return RegexUtils.IsExactMatch(input, RegexUtils.RegexUrl);
        }

        public static string StripHtml(string input)
        {
            return RegexUtils.Replace(input, RegexUtils.RegexHtml, string.Empty);
        }

        public static string Trim(string input)
        {
            if (IsEmpty(input)) return input;
            return input.Trim();
        }

        public static string CutWhitespace(string input)
        {
            if (IsEmpty(input)) return input;
            return Trim(RegexUtils.Replace(input, @"\s+", " "));
        }

        public static string CutEnd(string input, int length)
        {
            if (IsEmpty(input)) return input;
            if (input.Length <= length) return input;
            return input.Substring(0, length);
        }

        public static string CutEnd(string input, int length, string endString)
        {
            if (IsEmpty(input)) return input;
            if (input.Length <= length) return input;
            return input.Substring(0, length) + endString;
        }

        public static string CutStart(string input, int length)
        {
            if (IsEmpty(input)) return input;
            if (input.Length <= length) return input;
            return input.Substring(length);
        }

        public static string Start(string input, int length)
        {
            if (IsEmpty(input)) return input;
            if (input.Length <= length) return input;
            return input.Substring(0, length);
        }

        public static string End(string input, int length)
        {
            if (IsEmpty(input)) return input;
            if (input.Length <= length) return input;
            return input.Substring(input.Length - length);
        }

        public static string[] GetOccurences(string input, string pattern)
        {
            if (IsEmpty(input) || IsEmpty(pattern)) return new string[] { };
            var col = System.Text.RegularExpressions.Regex.Matches(input, pattern);
            var colText = new string[col.Count];
            for (var i = 0; i < col.Count; i++)
            {
                colText[i] = col[i].Value;
            }
            return colText;
        }

        public static int OccurenceCount(string input, string pattern)
        {
            return GetOccurences(input, pattern).Length;
        }

        public static string Replace(string input, string oldValue, string newValue, StringComparison comparison)
        {
            var sb = new StringBuilder();
            var previousIndex = 0;
            var index = input.IndexOf(oldValue, comparison);

            while (index != -1)
            {
                sb.Append(input.Substring(previousIndex, index - previousIndex));
                sb.Append(newValue);
                index += oldValue.Length;

                previousIndex = index;
                index = input.IndexOf(oldValue, index, comparison);
            }

            sb.Append(input.Substring(previousIndex));

            return sb.ToString();
        }

        public static string Xor(string input, string key)
        {
            if (IsEmpty(input)) return input;
            string strEncoded = string.Empty;
            int nKeyIndex = 0;
            for (int i = 0; i < input.Length; i++)
            {
                strEncoded += Convert.ToChar(input[i] ^ key[nKeyIndex]);
                nKeyIndex++;
                if (nKeyIndex == key.Length) nKeyIndex = 0;
            }
            return strEncoded;
        }

        public static string ToTitleCase(string input)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input);
        }

        /// <summary>
        /// returns a friendly name of a string
        /// eg. "SomeUtilsText" becomes "Some Utils Text"
        /// eg2. "BillInvoiceID" becomes "Bill Invoice" if trimIDText = true
        /// </summary>
        /// <param name="input">the input string</param>
        /// <param name="trimIdText">if "ID" text should be cut off the end of the string</param>
        /// <returns>a friendly name string</returns>
        public static string ToFriendlyName(string input, bool trimIdText)
        {
            if (string.IsNullOrEmpty(input)) return input;
            input = input.Trim(); //trim it
            if (input.ToUpper() == input) return input;    //if its all capitals we cant do anything with it
            var sb = new StringBuilder(input.Length);
            char? last = null;
            foreach (var c in input.ToCharArray())
            {
                if (last != null && Char.IsUpper(c)) // && Char.IsLower(last ?? char.MinValue))
                    sb.Append(" ").Append(c);
                else sb.Append(c);
                last = c;
            }
            if (trimIdText)
            {
                //if the string ends with ' id' cut it off
                var strOutput = sb.ToString();
                if (strOutput.ToLower().EndsWith(" id"))
                    return CutEnd(strOutput, 3);
            }
            return sb.ToString();
        }

        public static string ToFriendlyName(string input)
        {
            return ToFriendlyName(input, true);
        }
    }
}