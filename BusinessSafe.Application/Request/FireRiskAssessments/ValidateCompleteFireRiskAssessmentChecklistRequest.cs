using System.Collections.Generic;

namespace BusinessSafe.Application.Request.FireRiskAssessments
{
    public class ValidateCompleteFireRiskAssessmentChecklistRequest
    {
        public long ChecklistId { get; set; }
        public IEnumerable<long> QuestionIds { get; set; }
    }
}