using System;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels
{
    public class EmployeeChecklistViewModel
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string FriendlyReference { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string ChecklistName { get; set; }
        public string ChecklistNameForDisplay { get; set; }
        public bool IsCompleted { get; set; }
        public string CompletedDate { get; set; }
        public string Site { get; set; }
        public string IsFurtherActionRequired { get; set; }
    }
}