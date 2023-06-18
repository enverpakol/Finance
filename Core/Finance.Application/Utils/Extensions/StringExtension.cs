using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Utils.Extensions
{
    public static class StringExtender
    {
        public static bool IsEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }
        public static string FirstUpper(this string text)
        {
            var a = text.First().ToString().ToUpper() + String.Join("", text.ToLower().Skip(1));
            return a;
        }
        public static string FirstUpperWord(this string text, string culture = "tr-TR")
        {
            if (text.IsEmpty())
                return text;
            TextInfo myTI = new CultureInfo(culture, false).TextInfo;
            return myTI.ToTitleCase(text);
        }

        public static string ToShort(this string text, int length, string prefix = "...")
        {
            return !string.IsNullOrEmpty(text)
                       ? (text.Length > length ? text.Substring(0, length - prefix.Length) + prefix : text)
                       : text;
        }

        public static bool IsNumeric(this string value)
        {
            return value.All(Char.IsNumber);
        }

        public static bool IsContainsNumber(this string s)
        {
            if (s == null || s == "")
                return false;
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c >= '0' && c <= '9')
                    return true;
            }
            return false;
        }
    }
}
