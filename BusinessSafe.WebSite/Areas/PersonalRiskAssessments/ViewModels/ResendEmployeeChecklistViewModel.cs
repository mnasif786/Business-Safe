using System;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels
{
    public class ResendEmployeeChecklistViewModel
    {
        public Guid EmployeeChecklistId { get; set; }
        public long RiskAssessmentId { get; set; }
    }
}