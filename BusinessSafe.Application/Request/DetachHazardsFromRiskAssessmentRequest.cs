using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.Request
{
    public class DetachHazardsFromRiskAssessmentRequest
    {
        public IList<long> HazardIds { get; set; }
        public long RiskAssessmentId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}