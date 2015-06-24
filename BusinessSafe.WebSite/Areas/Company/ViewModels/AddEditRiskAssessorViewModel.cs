using System;

namespace BusinessSafe.WebSite.Areas.Company.ViewModels
{
    public class AddEditRiskAssessorViewModel
    {
        public string FormName { get; set; }

        // validation is done in controller
        public Guid? EmployeeId { get; set; }
        public string Employee { get; set; }

        // validation is done in controller
        public long? SiteId { get; set; }
        public string Site { get; set; }

        public bool HasAccessToAllSites { get; set; }

        public bool DoNotSendTaskOverdueNotifications { get; set; }
        public bool DoNotSendTaskCompletedNotifications { get; set; }
        public bool DoNotSendReviewDueNotification { get; set; }
        public bool DoNotSendTaskDueTomorrowNotification { get; set; }

        public bool SaveButtonEnabled { get; set; }

        public long? RiskAssessorId { get; set; }
    }
}