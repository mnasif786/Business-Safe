using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.Request
{
    public class AttachHazardsToRiskAssessmentRequest
    {
        public AttachHazardsToRiskAssessmentRequest()
        {
            Hazards = new List<AttachHazardsToRiskAssessmentHazardDetail>();
        }
        //public IList<long> HazardIds { get; set; }
        public long RiskAssessmentId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
        public IList<AttachHazardsToRiskAssessmentHazardDetail> Hazards { get; set; }
    }

    public class AttachHazardsToRiskAssessmentHazardDetail
    {
        public long Id { get; set; }
        public int OrderNumber { get; set; }
    }
}