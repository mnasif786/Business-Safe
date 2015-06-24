using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class Keyword : Entity<long>
    {
        public virtual string Text { get; set; }

        public static List<string> ParseKeywordString(string keywordString)
        {
            if (keywordString == null) return new List<string>();
            var keywordStringParts = keywordString.Split(',');
            var trimmedKeywordStringParts = keywordStringParts.Select(x => x.Trim());
            var notNullOrEmptyTrimmedKeywordStringParts = trimmedKeywordStringParts.Where(x => !String.IsNullOrEmpty(x));
            return notNullOrEmptyTrimmedKeywordStringParts.ToList();
        }
    }
}
