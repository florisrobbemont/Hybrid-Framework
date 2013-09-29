using System;
using System.Text;

namespace UmbracoTemplate.Common
{
    public static class SafeConvert
    {
        public static bool ToBoolean(object input, bool def)
        {
            if (Strings.IsEmpty(input)) return def;
            string strInput = Convert.ToString(input);
            if (strInput.ToUpper() == "TRUE" || strInput == "1") return true;
            if (strInput.ToUpper() == "FALSE" || strInput == "0") return false;
            return def;
        }

        public static bool ToBoolean(object input)
        {
            return ToBoolean(input, false);
        }

        public static int ToInt(object input, int def)
        {
            return Strings.IsNumeric(input) ? Convert.ToInt32(input) : def;
        }

        public static int ToInt(object input)
        {
            return ToInt(input, 0);
        }

        public static decimal ToDecimal(object input, decimal def)
        {
            return Strings.IsNumeric(input) ? Convert.ToDecimal(input) : def;
        }

        public static decimal ToDecimal(object input)
        {
            return ToDecimal(input, 0);
        }

        public static double ToDouble(object input, double def)
        {
            return Strings.IsNumeric(input) ? Convert.ToDouble(input) : def;
        }

        public static double ToDouble(object input)
        {
            return ToDouble(input, 0);
        }

        public static string ToHexString(byte[] data)
        {
            if (data.Length == 0) return string.Empty;
            var sb = new StringBuilder(data.Length * 2);
            foreach (var b in data)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }
        
        public static DateTime ToDate(string dateString)
        {
            return ToDate(dateString, DateTime.Now);
        }

        public static DateTime ToDate(string dateString, DateTime defaultDate)
        {
            DateTime outDateTime;
            try
            {
                outDateTime = DateTime.Parse(dateString);
            }
            catch
            {
                outDateTime = defaultDate;
            }

            return outDateTime;
        }

        public static DateTime FromUnixTime(long unixTime)
        {
            var epochTime = new DateTime(1970, 1, 1);
            epochTime = epochTime.AddMilliseconds(unixTime);
            return epochTime;
        }
    }
}