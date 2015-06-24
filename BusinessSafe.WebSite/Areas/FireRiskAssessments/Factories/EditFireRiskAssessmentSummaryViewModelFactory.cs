using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories
{
    public class EditFireRiskAssessmentSummaryViewModelFactory : IEditFireRiskAssessmentSummaryViewModelFactory
    {
        private long _riskAssessmentId;
        private long _companyId;
        private readonly IEmployeeService _employeeService;
        private readonly IFireRiskAssessmentService _riskAssessmentService;
        private readonly ISiteService _siteService;
        private IList<long> _allowedSiteIds;

        public EditFireRiskAssessmentSummaryViewModelFactory(IEmployeeService employeeService, IFireRiskAssessmentService riskAssessmentService, ISiteService siteService)
        {
            _employeeService = employeeService;
            _riskAssessmentService = riskAssessmentService;
            _siteService = siteService;
        }

        public IEditFireRiskAssessmentSummaryViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IEditFireRiskAssessmentSummaryViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IEditFireRiskAssessmentSummaryViewModelFactory WithAllowableSiteIds(IList<long> allowableSites)
        {
            _allowedSiteIds = allowableSites;
            return this;
        }

        public EditSummaryViewModel GetViewModel()
        {
            var riskAssessment = _riskAssessmentService.GetRiskAssessment(_riskAssessmentId, _companyId);
            var assessors = GetRiskAssessors();
            var sites = GetSites();

            return new EditSummaryViewModel()
            {
                CompanyId = _companyId,
                RiskAssessmentId = _riskAssessmentId,
                RiskAssessmentAssessors = assessors,
                Reference = riskAssessment.Reference,
                Title = riskAssessment.Title,
                DateOfAssessment = riskAssessment.AssessmentDate ?? DateTime.Today,
                RiskAssessorId = riskAssessment.RiskAssessor != null ? riskAssessment.RiskAssessor.Id : (long?) null,
                RiskAssessor = riskAssessment.RiskAssessor != null ? riskAssessment.RiskAssessor.FormattedName : string.Empty,
                SiteId = riskAssessment.RiskAssessmentSite != null ? riskAssessment.RiskAssessmentSite.Id: 0,
                Site = riskAssessment.RiskAssessmentSite!= null ? riskAssessment.RiskAssessmentSite.Name:string.Empty,
                Sites = sites,
                PersonAppointed = riskAssessment.PersonAppointed
            };
        }

        private IEnumerable<AutoCompleteViewModel> GetSites()
        {
            var sites = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = _companyId,
                AllowedSiteIds = _allowedSiteIds
            });
            return sites.Select(x => new AutoCompleteViewModel(x.Name, x.Id.ToString())).AddDefaultOption();
            
        }

        public EditSummaryViewModel GetViewModel(EditSummaryViewModel viewModel)
        {
            var assessors = GetRiskAssessors();
            var sites = GetSites();
            viewModel.RiskAssessmentAssessors = assessors;
            viewModel.Sites = sites;
            return viewModel;
        }

        private IEnumerable<AutoCompleteViewModel> GetRiskAssessors()
        {
            var riskAssessmentAssessors = _employeeService.Search(new SearchEmployeesRequest()
            {
                CompanyId = _companyId,
                MaximumResults = 100
            });

            var assessors =
                riskAssessmentAssessors.OrderBy(x => x.FullName).Select(AutoCompleteViewModel.ForEmployeeNoJobTitle).
                    AddDefaultOption();
            return assessors;
        }
    }
}
