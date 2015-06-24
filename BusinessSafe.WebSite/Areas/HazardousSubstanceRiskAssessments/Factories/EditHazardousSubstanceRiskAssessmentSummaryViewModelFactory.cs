using System;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public class EditHazardousSubstanceRiskAssessmentSummaryViewModelFactory : IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory
    {
        private readonly IHazardousSubstanceRiskAssessmentService _riskAssessmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IHazardousSubstancesService _hazardousSubstancesService;
        private readonly ISiteService _siteService;
        private long _riskAssessmentId;
        private long _companyId;
        private IList<long> _allowedSiteIds;

        public EditHazardousSubstanceRiskAssessmentSummaryViewModelFactory(
            IHazardousSubstanceRiskAssessmentService riskAssessmentService, 
            IEmployeeService employeeService, 
            IHazardousSubstancesService hazardousSubstancesService, 
            ISiteService siteService)
        {
            _riskAssessmentService = riskAssessmentService;
            _employeeService = employeeService;
            _hazardousSubstancesService = hazardousSubstancesService;
            _siteService = siteService;
        }

        public IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory WithAllowableSiteIds(IList<long> allowableSites)
        {
            _allowedSiteIds = allowableSites;
            return this;
        }

        public EditSummaryViewModel GetViewModel()
        {
            var riskAssessment = _riskAssessmentService.GetRiskAssessment(_riskAssessmentId, _companyId);

            var model = new EditSummaryViewModel()
                   {
                       RiskAssessmentId = _riskAssessmentId,
                       CompanyId = _companyId,
                       Reference = riskAssessment.Reference,
                       Title = riskAssessment.Title,
                       DateOfAssessment = riskAssessment.AssessmentDate ?? DateTime.Today,
                       HazardousSubstanceId = riskAssessment.HazardousSubstance.Id,
                       HazardousSubstance = riskAssessment.HazardousSubstance.Name
                   };

            model = AttachDropDownData(model);
            if(riskAssessment.Site != null)
            {
                model.Site = riskAssessment.Site.Name;
                model.SiteId = riskAssessment.Site.Id;
            }
            if(riskAssessment.RiskAssessor != null)
            {
                model.RiskAssessorId = riskAssessment.RiskAssessor.Id;
                model.RiskAssessor = riskAssessment.RiskAssessor.FormattedName;
            }

            return model;
        }

        public EditSummaryViewModel AttachDropDownData(EditSummaryViewModel model)
        {
            var riskAssessmentAssessors = _employeeService.Search(new SearchEmployeesRequest()
            {
                CompanyId = _companyId,
                MaximumResults = 100
            });
            var assessors = riskAssessmentAssessors.OrderBy(x => x.FullName).Select(AutoCompleteViewModel.ForEmployeeNoJobTitle).AddDefaultOption();

            var hazardousSubstances = _hazardousSubstancesService.Search(new SearchHazardousSubstancesRequest()
            {
                CompanyId = _companyId
            });
            var hazardousSubstancesForDropdown = hazardousSubstances.OrderBy(x => x.Name).Select(AutoCompleteViewModel.ForHazardousSubstance).AddDefaultOption();

            var siteStructureElements = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = _companyId,
                AllowedSiteIds = _allowedSiteIds,
                PageLimit = 100
            });
            var sites = siteStructureElements.Select(x => new AutoCompleteViewModel(x.Name, x.Id.ToString())).AddDefaultOption();
            model.Sites = sites;

            model.HazardousSubstances = hazardousSubstancesForDropdown;
            model.RiskAssessmentAssessors = assessors;

            return model;
        }
    }
}