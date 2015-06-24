using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public class EmployeeChecklistGeneratorViewModelFactory : IEmployeeChecklistGeneratorViewModelFactory
    {
        private readonly IEmployeeService _employeeService;
        private readonly IChecklistService _checklistService;
        private readonly IPersonalRiskAssessmentService _personalRiskAssessmentService;
        private readonly ISiteService _siteService;
        private readonly ISelectedEmployeeViewModelFactory _selectedEmployeeViewModelFactory;
        private long _riskAssessmentId;
        private long _companyId;
        private string _riskAssessorEmail;
        private Guid _currentUserId;

        public EmployeeChecklistGeneratorViewModelFactory(
            IEmployeeService employeeService,
            IChecklistService checklistService,
            IPersonalRiskAssessmentService personalRiskAssessmentService,
            ISiteService siteService,
            ISelectedEmployeeViewModelFactory selectedEmployeeViewModelFactory
            )
        {
            _employeeService = employeeService;
            _checklistService = checklistService;
            _personalRiskAssessmentService = personalRiskAssessmentService;
            _siteService = siteService;
            _selectedEmployeeViewModelFactory = selectedEmployeeViewModelFactory;
        }

        public IEmployeeChecklistGeneratorViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IEmployeeChecklistGeneratorViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }
        
        public IEmployeeChecklistGeneratorViewModelFactory WithRiskAssessorEmail(string email)
        {
            _riskAssessorEmail = email;
            return this;
        }

        public IEmployeeChecklistGeneratorViewModelFactory WithCurrentUserId(Guid currentUserId)
        {
            _currentUserId = currentUserId;
            return this;
        }

        private List<ChecklistGeneratorChecklistViewModel> GetChecklists()
        {
            var checklists =
                _checklistService
                .GetByRiskAssessmentType(ChecklistRiskAssessmentType.Personal)
                .Select(checklist => new ChecklistGeneratorChecklistViewModel
                {
                    Id = checklist.Id,
                    Title = checklist.Title,
                    ControlId =
                        string.Format("IncludeChecklist_{0}", checklist.Id.ToString(CultureInfo.InvariantCulture))
                }).ToList();

            return checklists;
        }

        private List<AutoCompleteViewModel> GetEmployees()
        {
            var employees = _employeeService.GetAll(_companyId);
            return employees.Select(AutoCompleteViewModel.ForEmployeeWithEmail).AddDefaultOption().ToList();
        }

        private List<AutoCompleteViewModel> GetSites()
        {
            var sites = _siteService.GetAll(_companyId);
            return sites.Select(AutoCompleteViewModel.ForSite).AddDefaultOption().ToList();
        }

        public EmployeeChecklistGeneratorViewModel GetViewModel()
        {
            var personalRiskAssessment = _personalRiskAssessmentService.GetWithChecklistGeneratorEmployeesAndChecklists(_riskAssessmentId, _companyId, _currentUserId);
            var employees = _employeeService.GetAll(_companyId).ToList();

            var viewModel = new EmployeeChecklistGeneratorViewModel()
                   {
                       RiskAssessmentId = _riskAssessmentId,
                       CompanyId = _companyId,
                       Employees = GetEmployees(),
                       Sites = GetSites(),
                       MultiSelectEmployees = employees,
                       Checklists = GetChecklists(),
                       Message = personalRiskAssessment.ChecklistGeneratorMessage,
                       PersonalRiskAssessementEmployeeChecklistStatus = personalRiskAssessment.PersonalRiskAssessementEmployeeChecklistStatus,
                       SendCompletedChecklistNotificationEmail = personalRiskAssessment.SendCompletedChecklistNotificationEmail,
                       CompletionDueDateForChecklists = personalRiskAssessment.CompletionDueDateForChecklists,
                       CompletionNotificationEmailAddress = personalRiskAssessment.CompletionNotificationEmailAddress ?? _riskAssessorEmail,
                       MultiSelectedEmployees = new List<SelectedEmployeeViewModel>()
                   };

            SetChecklistGeneratorEmployees(personalRiskAssessment, viewModel);

            foreach (var checklist in viewModel.Checklists)
            {
                if(personalRiskAssessment.Checklists.Select(currentChecklist => currentChecklist.Id).Contains(checklist.Id))
                {
                    checklist.Checked = true;
                }
            }

            return viewModel;
        }

        public EmployeeChecklistGeneratorViewModel GetViewModel(EmployeeChecklistGeneratorViewModel viewModel)
        {
            var personalRiskAssessment = _personalRiskAssessmentService.GetWithChecklistGeneratorEmployeesAndChecklists(_riskAssessmentId, _companyId, _currentUserId);
            var employees = _employeeService.GetAll(_companyId).ToList();

            var returnViewModel = new EmployeeChecklistGeneratorViewModel
                                      {
                                          EmployeeId = viewModel.EmployeeId,
                                          RiskAssessmentId = viewModel.RiskAssessmentId,
                                          IsForMultipleEmployees = viewModel.IsForMultipleEmployees,
                                          SingleEmployeesSectionVisible = viewModel.SingleEmployeesSectionVisible,
                                          MultipleEmployeesSectionVisible = viewModel.MultipleEmployeesSectionVisible,
                                          Employees = GetEmployees(),
                                          MultiSelectEmployees = employees,
                                          NewEmployeeEmail = viewModel.NewEmployeeEmail,
                                          NewEmployeeEmailVisible = viewModel.NewEmployeeEmailVisible,
                                          ExistingEmployeeEmail = viewModel.ExistingEmployeeEmail,
                                          ExistingEmployeeEmailVisible = viewModel.ExistingEmployeeEmailVisible,
                                          Checklists = GetChecklists(),
                                          Message = viewModel.Message,
                                          ChecklistsToGenerate = viewModel.ChecklistsToGenerate,
                                          SendCompletedChecklistNotificationEmail = viewModel.SendCompletedChecklistNotificationEmail,
                                          CompletionNotificationEmailAddress = viewModel.CompletionNotificationEmailAddress,
                                          CompletionDueDateForChecklists = viewModel.CompletionDueDateForChecklists,
                                          CompanyId = _companyId
                                      };

            SetChecklistGeneratorEmployees(personalRiskAssessment, returnViewModel);

            return returnViewModel;
        }

        private void SetChecklistGeneratorEmployees(PersonalRiskAssessmentDto personalRiskAssessment, EmployeeChecklistGeneratorViewModel viewModel)
        {
            if (personalRiskAssessment.ChecklistGeneratorEmployees.Count() == 1)
            {
                viewModel.IsForMultipleEmployees = "single";
                viewModel.SingleEmployeesSectionVisible = true;
                var employee = personalRiskAssessment.ChecklistGeneratorEmployees.ToList()[0];

                if (employee.MainContactDetails == null || string.IsNullOrEmpty(employee.MainContactDetails.Email))
                {
                    viewModel.NewEmployeeEmailVisible = true;
                    viewModel.ExistingEmployeeEmailVisible = false;
                }
                else
                {
                    viewModel.NewEmployeeEmailVisible = false;
                    viewModel.ExistingEmployeeEmailVisible = true;
                    viewModel.ExistingEmployeeEmail = employee.MainContactDetails.Email;
                }

                viewModel.EmployeeId = employee.Id;
            }

            if (personalRiskAssessment.ChecklistGeneratorEmployees.Count() > 1)
            {
                viewModel.IsForMultipleEmployees = "multiple";
                viewModel.MultipleEmployeesSectionVisible = true;

                viewModel.MultiSelectedEmployees = _selectedEmployeeViewModelFactory
                    .WithCompanyId(_companyId)
                    .WithRiskAssessmentId(_riskAssessmentId)
                    .WithUserId(_currentUserId)
                    .GetViewModel();
            }
        }
    }
}