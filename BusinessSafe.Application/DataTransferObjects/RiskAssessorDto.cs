namespace BusinessSafe.Application.DataTransferObjects
{
    public class RiskAssessorDto
    {
        public long Id { get; set; }
        public EmployeeDto Employee { get; set; }
        public bool HasAccessToAllSites { get; set; }
        public SiteStructureElementDto Site { get; set; }
        public bool DoNotSendTaskOverdueNotifications { get; set; }
        public bool DoNotSendTaskCompletedNotifications { get; set; }
        public bool DoNotSendReviewDueNotification { get; set; }
        public bool DoNotSendDueTomorrowNotification { get; set; }
        public string FormattedName { get; set; }

        public string SiteName
        {
            get { return HasAccessToAllSites ? "All Sites" : Site != null ? Site.Name : ""; }
        }

    }
}
