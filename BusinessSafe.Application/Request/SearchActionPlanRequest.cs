using System;
using System.Collections.Generic;
using System.ComponentModel;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Request
{
    public class SearchActionPlanRequest
    {
        public long CompanyId { get; set; }
        public long? SiteId { get; set; }
        public long? SiteGroupId { get; set; }
        public DateTime? SubmittedFrom { get; set; }
        public DateTime? SubmittedTo { get; set; }
        public long ActionPlanId { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public ActionPlanOrderByColumn OrderBy { get; set; }
        public bool Ascending { get; set; }
        public bool ShowArchived { get; set; }
        public IList<long> AllowedSiteIds { get; set; }
    }
}
