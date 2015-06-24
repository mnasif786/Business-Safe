using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessSafe.WebSite.Extensions
{
    public static class StringExtensions
    {
        public static DateTime? ParseAsDateTime(this string str)
        {
            DateTime date;
            
            bool isParsed = DateTime.TryParse(str, out date);
            
            if (isParsed)
                return date;
            
            return new DateTime?();
        }

        public static string AddSpacesToName(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            var newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ' && !char.IsUpper(text[i + 1]))
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
        
        public static string GetAcronym(this string text)
        {
            var words = text.Split(' ');
            var output = new StringBuilder();
            foreach(var word in words)
            {
                output.Append(word.Substring(0, 1));
            }
            return output.ToString();
        }

        public static string ParseAsFileName(this string str)
        {
            var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));

            return r.Replace(str, "") + ".pdf";
        }

        public static string TruncateWithEllipsis(this string str, int maxLength)
        {
            var truncated = str;
            if(!string.IsNullOrEmpty(str) && str.Length > maxLength)
            {
                truncated = string.Format("{0}{1}", str.Substring(0, maxLength).Trim(), "\u2026");
            }
            return truncated;
        }

        public static bool IsEqualWithNullOrWhiteSpace(this string str, string stringToCompanre)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.IsNullOrEmpty(stringToCompanre);
            }
            
            return string.Equals(str, stringToCompanre);
        }
    }
}