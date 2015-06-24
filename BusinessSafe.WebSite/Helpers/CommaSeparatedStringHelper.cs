using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.WebSite.Helpers
{
    public static class CommaSeparatedStringHelper
    {
        public static string Construct(IList<string> elements)
        {
            if(elements == null || !elements.Any())
            {
                return "";
            }

            var returnString = new StringBuilder();

            foreach (var element in elements)
            {
                returnString.Append(element);

                if(element != elements.Last())
                {
                    returnString.Append(", ");
                }
            }

            return returnString.ToString();
        }

        public static long[] ConvertToLongArray(string csv)
        {
            if (string.IsNullOrEmpty(csv))
            {
                return null;
            }
            var stringArray = csv.Split(',');
            var longArray = new long[stringArray.Length];
            var index = 0;
            foreach (var s in stringArray)
            {
                longArray[index] = long.Parse(s);
                index++;
            }
            return longArray;
        }
    }
}