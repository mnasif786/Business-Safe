using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class FireRiskAssessmentChecklistDto
    {
        public long Id { get; set; }
        public FireRiskAssessmentDto FireRiskAssessment { get; set; }
        public ChecklistDto Checklist { get; set; }
        public DateTime? CompletedDate { get; set; }
        public IEnumerable<FireAnswerDto> Answers { get; set; }
        public RiskAssessmentReviewDto ReviewGeneratedFrom { get; set; }
        public bool? HasCompleteFailureAttempt { get; set; }
    }
}
