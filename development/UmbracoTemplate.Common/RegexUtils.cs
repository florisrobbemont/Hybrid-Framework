using System.Text.RegularExpressions;

namespace UmbracoTemplate.Common
{
    public static class RegexUtils
    {
        public const string RegexEmail = @"([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,8}|[0-9]{1,8})(\]?)";
        public const string RegexNumeric = @"^\-?\(?([0-9]{0,3}(\,?[0-9]{3})*(\.?[0-9]*))\)?$";
        public const string RegexUrl = @"(?<Protocol>\w+):\/\/(?<Domain>[\w@][\w.:@]+)\/?[\w\.?=%&=\-@/$,]*";
        public const string RegexIpaddress = @"(?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)";
        public const string RegexHtml = "<.*?>";

        /// <summary>
        /// Wraps the default regex isMatch to include regex options to pre compile and ignore case. also checks for nulls and empty strings
        /// </summary>
        public static bool IsExactMatch(string input, string pattern)
        {
            if (Strings.IsEmpty(input)) return false;
            var m = Regex.Match(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            if (!m.Success) return false;
            return m.Groups[0].Value == input;
        }

        /// <summary>
        /// Wraps the default regex Contains to include regex options to pre compile and ignore case. also checks for nulls and empty strings
        /// </summary>
        public static bool Contains(string input, string pattern)
        {
            if (Strings.IsEmpty(input)) return false;
            var m = Regex.Match(input, pattern);
            return m.Success;
        }

        /// <summary>
        /// Wraps the default regex Replace to include regex options to pre compile and ignore case. also checks for nulls and empty strings
        /// </summary>
        public static string Replace(string input, string pattern, string replace)
        {
            return Strings.IsEmpty(input) ? input : Regex.Replace(input, pattern, replace, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Pattern must be something like :
        /// @"Subject\s*\:\s*(?<![CDATA[<SubjectReturn>]]>.*)\r\n"
        /// from a string "Subject: Testing", with groupname "SubjectReturn" will return "Testing"
        /// the pattern must contain the groupname text ?<![CDATA[<AnyGroupName>]]>. in it to return anything
        /// </summary>
        public static string GetMatch(string input, string pattern, string groupname)
        {
            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                var grp = match.Groups[groupname];
                if (grp != null)
                    return grp.Value;
            }
            return string.Empty;
        }
    }
}