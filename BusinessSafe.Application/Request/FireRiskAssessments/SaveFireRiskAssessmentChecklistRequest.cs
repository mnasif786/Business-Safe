using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.Request.FireRiskAssessments
{
    public class SaveFireRiskAssessmentChecklistRequest
    {
        public long FireRiskAssessmentId { get; set; }
        public long FireRiskAssessmentChecklistId { get; set; }
        public long CompanyId { get; set; }
        public IList<SubmitFireAnswerRequest> Answers { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}
