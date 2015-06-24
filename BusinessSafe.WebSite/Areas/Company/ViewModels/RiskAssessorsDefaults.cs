using System;

namespace BusinessSafe.WebSite.Areas.Company.ViewModels
{
    public class RiskAssessorsDefaults: Defaults
    {
        public Guid EmployeeId { get; set; }
        public string Forename { get; set; }
        public string Surname{ get; set; }
        public string Site { get; set; }
        public bool DoNotSendOverDueNotifications { get; set; }
        public bool DoNotSendCompletedNotifications { get; set; }
        public bool DoNotSendDueNotifications { get; set; }
    }
}