using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public class EditPersonalRiskAssessmentSummaryViewModelFactory : IEditPersonalRiskAssessmentSummaryViewModelFactory
    {
        private readonly IPersonalRiskAssessmentService _riskAssessmentService;
        private readonly IEmployeeService _employeeService;
        private readonly ISiteService _siteService;
        private long _riskAssessmentId;
        private long _companyId;
        private Guid _currentUserId;
        private IList<long> _allowedSiteIds;

        public EditPersonalRiskAssessmentSummaryViewModelFactory(IPersonalRiskAssessmentService riskAssessmentService, IEmployeeService employeeService, ISiteService siteService)
        {
            _riskAssessmentService = riskAssessmentService;
            _employeeService = employeeService;
            _siteService = siteService;
        }

        public IEditPersonalRiskAssessmentSummaryViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IEditPersonalRiskAssessmentSummaryViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IEditPersonalRiskAssessmentSummaryViewModelFactory WithCurrentUserId(Guid currentUserId)
        {
            _currentUserId = currentUserId;
            return this;
        }

        public IEditPersonalRiskAssessmentSummaryViewModelFactory WithAllowableSiteIds(IList<long> allowableSites)
        {
            _allowedSiteIds = allowableSites;
            return this;
        }

        public EditSummaryViewModel GetViewModel()
        {
            var riskAssessment = _riskAssessmentService.GetWithReviews(_riskAssessmentId, _companyId, _currentUserId);

            var model = new EditSummaryViewModel()
                   {
                       RiskAssessmentId = _riskAssessmentId,
                       CompanyId = _companyId,
                       Reference = riskAssessment.Reference,
                       Title = riskAssessment.Title,
                       DateOfAssessment = riskAssessment.AssessmentDate ?? DateTime.Today,
                       RiskAssessorId = riskAssessment.RiskAssessor != null ? riskAssessment.RiskAssessor.Id : (long?) null,
                       RiskAssessor = riskAssessment.RiskAssessor != null ? riskAssessment.RiskAssessor.FormattedName : null,
                       SiteId = riskAssessment.RiskAssessmentSite != null ? riskAssessment.RiskAssessmentSite.Id : 0,
                       Site = riskAssessment.RiskAssessmentSite != null ? riskAssessment.RiskAssessmentSite.Name: string.Empty,
                       Sensitive = riskAssessment.Sensitive,
                       IsReadOnly = !IsLoggedInUserCreator(riskAssessment) && riskAssessment.Sensitive,
                       IsSensitiveReadonly = !IsLoggedInUserCreator(riskAssessment) && !IsLoggedInUserAssessor(riskAssessment) && !IsLoggedInUserReviewer(riskAssessment)
                   };
             model = AttachDropdownData(model);
            return model;
        }

        private bool IsLoggedInUserCreator(PersonalRiskAssessmentDto riskAssessment)
        {
            return riskAssessment.CreatedBy.Id == _currentUserId;
        }


        private bool IsLoggedInUserAssessor(PersonalRiskAssessmentDto riskAssessment)
        {
            bool isLoggedInUserAssessor = false;
            if (riskAssessment.RiskAssessor != null && riskAssessment.RiskAssessor.Employee != null)
            {
                var employee = _employeeService.GetEmployee(riskAssessment.RiskAssessor.Employee.Id, _companyId);
                if (employee != null && employee.User != null && employee.User.Id == _currentUserId)
                {
                    isLoggedInUserAssessor = true;
                }
            }
            return isLoggedInUserAssessor;
        }

        private bool IsLoggedInUserReviewer(PersonalRiskAssessmentDto riskAssessment)
        {
            bool isLoggedInUserReviewer = false;
            
            if (riskAssessment.Reviews != null)
            {
                isLoggedInUserReviewer = riskAssessment.Reviews.Any(x => x.ReviewAssignedTo.User != null && x.ReviewAssignedTo.User.Id == _currentUserId && x.CompletedDate == null);
            }
            return isLoggedInUserReviewer;
        }

        public EditSummaryViewModel GetViewModel(EditSummaryViewModel model)
        {
            model = AttachDropdownData(model);
            return model;
        }

        private EditSummaryViewModel AttachDropdownData(EditSummaryViewModel model)
        {
            var riskAssessmentAssessors = _employeeService.Search(new SearchEmployeesRequest()
            {
                CompanyId = _companyId,
                MaximumResults = 100
            });
            var assessors = riskAssessmentAssessors.OrderBy(x => x.FullName).Select(AutoCompleteViewModel.ForEmployeeNoJobTitle).AddDefaultOption();
            model.RiskAssessmentAssessors = assessors;

            var siteStructureElements = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = _companyId,
                AllowedSiteIds = _allowedSiteIds
            });
            var sites = siteStructureElements.Select(x => new AutoCompleteViewModel(x.Name, x.Id.ToString())).AddDefaultOption();
            model.Sites = sites;

            return model;
        }
    }
}