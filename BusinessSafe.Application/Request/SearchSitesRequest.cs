using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Application.Request
{

    public class SearchSitesRequest
    {
        public long CompanyId { get; set; }
        public string NameStartsWith { get; set; }
        public IList<long> AllowedSiteIds { get; set; }
        public int? PageLimit { get; set; }
    }
}
