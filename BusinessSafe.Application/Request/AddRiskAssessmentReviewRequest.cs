using System;

namespace BusinessSafe.Application.Request
{
    public class AddRiskAssessmentReviewRequest
    {
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public DateTime ReviewDate { get; set; }
        public Guid ReviewingEmployeeId { get; set; }
        public Guid AssigningUserId { get; set; }
        public bool SendTaskNotification { get; set; }
        public bool SendTaskCompletedNotification { get; set; }
        public bool SendTaskOverdueNotification { get; set; }
        public bool SendTaskDueTomorrowNotification { get; set; }
        public Guid TaskGuid { get; set; }
    }
}
