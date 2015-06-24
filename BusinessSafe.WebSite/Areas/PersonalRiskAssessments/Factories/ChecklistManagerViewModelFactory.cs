using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public class ChecklistManagerViewModelFactory: IChecklistManagerViewModelFactory
    {
        private readonly IPersonalRiskAssessmentService _personalRiskAssessmentService;
        private long _riskAssessmentId;
        private long _companyId;
        private Guid _currentUserId;
        private string _emailContains;
        private string _nameContains
;

        public ChecklistManagerViewModelFactory(IPersonalRiskAssessmentService personalRiskAssessmentService)
        {
            _personalRiskAssessmentService = personalRiskAssessmentService;
        }

        public IChecklistManagerViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IChecklistManagerViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IChecklistManagerViewModelFactory WithCurrentUserId(Guid currentUserId)
        {
            _currentUserId = currentUserId;
            return this;
        }

        public  IChecklistManagerViewModelFactory WithEmailContains(string emailContains)
        {
            _emailContains = emailContains;
            return this;
        }

        public IChecklistManagerViewModelFactory WithNameContains(string nameContains)
        {
            _nameContains = nameContains;
            return this;
        }

        public ChecklistManagerViewModel GetViewModel()
        {
            var personalRiskAssessmentDto = _personalRiskAssessmentService.GetWithEmployeeChecklists(_riskAssessmentId, _companyId, _currentUserId);
            var employeeChecklists = personalRiskAssessmentDto.EmployeeChecklists ?? new List<EmployeeChecklistDto>(); 

            employeeChecklists = ApplyEmailFilterIfRequired(employeeChecklists);

            employeeChecklists = ApplyEmployeeNameFilterIfRequired(employeeChecklists);
            
            return new ChecklistManagerViewModel()
                       {
                           RiskAssessmentId = _riskAssessmentId,
                           CompanyId = _companyId,
                           PersonalRiskAssessementEmployeeChecklistStatus = personalRiskAssessmentDto.PersonalRiskAssessementEmployeeChecklistStatus,
                           EmployeeChecklists = employeeChecklists.Select(x => new EmployeeChecklistViewModel()
                                                                                   {
                                                                                       Id = x.Id,
                                                                                       FriendlyReference = x.FriendlyReference,
                                                                                       EmployeeId = x.Employee.Id,
                                                                                       EmployeeName = x.Employee.FullName,
                                                                                       EmployeeEmail = x.Employee.MainContactDetails != null ? x.Employee.MainContactDetails.Email : null,
                                                                                       Site = x.Employee.SiteName,
                                                                                       ChecklistName = x.Checklist.Title,
                                                                                       ChecklistNameForDisplay = GetCheckListNameForDisplay(x.Checklist.Title),
                                                                                       IsFurtherActionRequired = GetIsFurtherActionRequiredDisplayText(x),
                                                                                       IsCompleted = x.CompletedDate.HasValue,
                                                                                       CompletedDate = x.CompletedDate.HasValue ? x.CompletedDate.Value.ToShortDateString(): string.Empty,

                                                                                   })
                       };
        }

        private IEnumerable<EmployeeChecklistDto> ApplyEmployeeNameFilterIfRequired(IEnumerable<EmployeeChecklistDto> employeeChecklists)
        {
            if (!string.IsNullOrEmpty(_nameContains))
            {
                employeeChecklists = employeeChecklists.Where(x => x.Employee.FullName != null
                                                                   && x.Employee.FullName.ToLower().Contains(_nameContains.ToLower() )).ToList();
            }
            return employeeChecklists;
        }

        private IEnumerable<EmployeeChecklistDto> ApplyEmailFilterIfRequired(IEnumerable<EmployeeChecklistDto> employeeChecklists)
        {
            if (!string.IsNullOrEmpty(_emailContains))
            {
                employeeChecklists = employeeChecklists.Where(x => x.Employee.MainContactDetails != null
                                                                   && x.Employee.MainContactDetails.Email != null
                                                                   && x.Employee.MainContactDetails.Email.ToLower().Contains(_emailContains.ToLower())).ToList();
            }
            return employeeChecklists;
        }
		
		private string GetIsFurtherActionRequiredDisplayText(EmployeeChecklistDto employeeChecklist)
        {
            if (!employeeChecklist.CompletedDate.HasValue)
                return string.Empty;

            if (!employeeChecklist.IsFurtherActionRequired.HasValue)
                return string.Empty;

            return employeeChecklist.IsFurtherActionRequired.Value ? "Yes" : "No";
        }

		private string GetCheckListNameForDisplay(string title)
        {
            var result = string.Empty;
            var words = title.Split(' ');

		    return words.Where(t => t.ToUpper() != "AND").Aggregate(result, (current, t) => current + t.Substring(0, 1));
        }
    }
}
