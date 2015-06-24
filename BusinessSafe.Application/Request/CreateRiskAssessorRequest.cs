using System;

namespace BusinessSafe.Application.Request
{
    public class CreateEditRiskAssessorRequest
    {
        public long CompanyId { get; set; }
        
        public Guid EmployeeId { get; set; }

        public long? SiteId { get; set; }
        public bool HasAccessToAllSites { get; set; }

        public bool DoNotSendTaskOverdueNotifications { get; set; }
        public bool DoNotSendTaskCompletedNotifications { get; set; }
        public bool DoNotSendReviewDueNotification { get; set; }
        public bool DoNotSendDueTomorrowNotification { get; set; }
        
        public Guid CreatingUserId { get; set; }

        public long RiskAssessorId { get; set; }
    }
}
