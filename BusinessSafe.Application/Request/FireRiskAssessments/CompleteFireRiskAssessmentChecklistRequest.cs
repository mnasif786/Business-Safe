using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.Request.FireRiskAssessments
{
    public class CompleteFireRiskAssessmentChecklistRequest
    {
        public long FireRiskAssessmentId { get; set; }
        public long CompanyId { get; set; }
        public IList<SubmitFireAnswerRequest> Answers { get; set; }
        public Guid CurrentUserId { get; set; }
        
    }
}
