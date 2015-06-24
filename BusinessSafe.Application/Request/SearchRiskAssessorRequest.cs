namespace BusinessSafe.Application.Request
{
    public class SearchRiskAssessorRequest
    {
        public long SiteId { get; set; }
        public long CompanyId { get; set; }
        public int MaximumResults { get; set; }
        public string SearchTerm { get; set; }
        public bool IncludeDeleted { get; set; }
        public bool ExcludeActive { get; set; }
    }
}