using System.Collections.Generic;

namespace BusinessSafe.Application.Request
{
    public class SearchUsersRequest
    {
        public long CompanyId { get; set; }
        public string ForenameLike { get; set; }
        public string SurnameLike { get; set; }
        public long SiteId { get; set; }
        public bool ShowDeleted { get; set; }
        public int MaximumResults { get; set; }
        public IList<long> AllowedSiteIds { get; set; }
    }
}