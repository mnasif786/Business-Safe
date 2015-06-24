using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.Request
{
    public class AttachSourceOfIgnitionToRiskAssessmentRequest
    {
        public IList<long> SourceOfIgnitionIds { get; set; }
        public long RiskAssessmentId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}