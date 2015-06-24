using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class RiskAssessmentReviewDto
    {
        public long Id { get; set; }
        public string Comments { get;  set; }
        public RiskAssessmentDto RiskAssessment { get; set; }
        public DateTime? CompletionDueDate { get; set; }
        public EmployeeDto ReviewAssignedTo { get; set; }
        public DateTime? CompletedDate { get; set; }
        public EmployeeDto CompletedBy { get; set; }
        public RiskAssessmentReviewTaskDto RiskAssessmentReviewTask { get; set; }
        public bool IsReviewOutstanding { get; set; }
    }
}