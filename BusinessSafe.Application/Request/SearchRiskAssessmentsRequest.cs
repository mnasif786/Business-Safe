using System;
using System.Collections.Generic;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Request
{
    
    public class SearchRiskAssessmentsRequest
    {
        public SearchRiskAssessmentsRequest()
        {
            Page = 0;
            PageSize = 10;
            OrderBy = RiskAssessmentOrderByColumn.CreatedOn;
            OrderByDirection = OrderByDirection.Descending;
        }

        public string Title { get; set; }
        public long CompanyId { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public long? SiteGroupId { get; set; }
        public long? SiteId { get; set; }
        public bool ShowDeleted { get; set; }
        public bool ShowArchived { get; set; }
        public IList<long> AllowedSiteIds { get; set; }
        public Guid CurrentUserId { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }

        public RiskAssessmentOrderByColumn OrderBy { get; set; }
        public OrderByDirection OrderByDirection { get; set; }
    }
}