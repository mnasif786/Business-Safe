using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.Request
{
    public class DetachDocumentsFromRiskAssessmentRequest
    {
        public Guid UserId { get; set; }
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public List<long> DocumentsToDetach { get; set; }
    }
}