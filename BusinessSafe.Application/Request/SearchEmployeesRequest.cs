using System;

namespace BusinessSafe.Application.Request
{
    public class SearchEmployeesRequest
    {
        public long CompanyId { get; set; }
        public string EmployeeReferenceLike { get; set; }
        public string ForenameLike { get; set; }
        public string SurnameLike { get; set; }
        public long[] SiteIds { get; set; }
        public bool ShowDeleted { get; set; }
        public int MaximumResults { get; set; }
        public bool IncludeSiteless { get; set; }
        public bool ExcludeWithActiveUser { get; set; }
    }
}