using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public class SelectedEmployeeViewModelFactory : ISelectedEmployeeViewModelFactory
    {
        private readonly IPersonalRiskAssessmentService _personalRiskAssessmentService;
        private long _personalRiskAssessmentId;
        private long _companyId;
        private Guid _userId;

        public SelectedEmployeeViewModelFactory(
            IPersonalRiskAssessmentService personalRiskAssessmentService)
        {
            _personalRiskAssessmentService = personalRiskAssessmentService;
        }

        public ISelectedEmployeeViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ISelectedEmployeeViewModelFactory WithRiskAssessmentId(long personalRiskAssessmentId)
        {
            _personalRiskAssessmentId = personalRiskAssessmentId;
            return this;
        }

        public ISelectedEmployeeViewModelFactory WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }
        
        public List<SelectedEmployeeViewModel> GetViewModel()
        {
            var riskAssessment = _personalRiskAssessmentService.GetWithChecklistGeneratorEmployeesAndChecklists(_personalRiskAssessmentId,
                                                                                           _companyId, _userId);

            var viewModel = riskAssessment.ChecklistGeneratorEmployees.OrderBy(x => x.Surname != null ? x.Surname.ToLower() : x.Surname).Select(x =>
                                                                              new SelectedEmployeeViewModel
                                                                                  {
                                                                                      EmployeeId = x.Id,
                                                                                      Name = x.FullName,
                                                                                      Email =
                                                                                          x.MainContactDetails != null
                                                                                              ? x.MainContactDetails.
                                                                                                    Email
                                                                                              : null
                                                                                  }).OrderBy(x => (!String.IsNullOrEmpty(x.Email))).ToList();
            return viewModel;
        }
    }
}