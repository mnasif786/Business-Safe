using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.Request
{
    public class CopyRiskAssessmentForMultipleSitesRequest
    {
        public CopyRiskAssessmentForMultipleSitesRequest()
        {
            SiteIds = new List<long>();
        }

        public long CompanyId { get; set; }
        public long RiskAssessmentToCopyId { get; set; }
        public string Reference { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public IList<long> SiteIds { get; set; } 
    }
}
