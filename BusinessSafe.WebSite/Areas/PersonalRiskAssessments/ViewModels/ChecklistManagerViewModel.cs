using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels
{
    public class ChecklistManagerViewModel
    {
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public IEnumerable<EmployeeChecklistViewModel> EmployeeChecklists { get; set; }
        public PersonalRiskAssessementEmployeeChecklistStatusEnum PersonalRiskAssessementEmployeeChecklistStatus { get; set; }

        public ChecklistManagerViewModel()
        {
            EmployeeChecklists = new List<EmployeeChecklistViewModel>();
        }
    }
}