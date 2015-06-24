using System;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories
{
    public class EditGeneralRiskAssessmentSummaryViewModelFactory : IEditGeneralRiskAssessmentSummaryViewModelFactory
    {
        private readonly IGeneralRiskAssessmentService _riskAssessmentService;
        private readonly IRiskAssessorService _riskAssessorService;
        private readonly ISiteService _siteService;
        private long _riskAssessmentId;
        private long _companyId;
        private IList<long> _allowedSiteIds;

        public EditGeneralRiskAssessmentSummaryViewModelFactory(
            IGeneralRiskAssessmentService riskAssessmentService, 
            IRiskAssessorService riskAssessorService, 
            ISiteService siteService)
        {
            _riskAssessmentService = riskAssessmentService;
            _riskAssessorService = riskAssessorService;
            _siteService = siteService;
        }

        public IEditGeneralRiskAssessmentSummaryViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IEditGeneralRiskAssessmentSummaryViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IEditGeneralRiskAssessmentSummaryViewModelFactory WithAllowableSiteIds(IList<long> allowableSites)
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
                       RiskAssessorId = riskAssessment.RiskAssessor != null ? riskAssessment.RiskAssessor.Id: (long?) null,
                       RiskAssessor = riskAssessment.RiskAssessor != null ? riskAssessment.RiskAssessor.FormattedName : string.Empty,
                       SiteId = riskAssessment.RiskAssessmentSite != null ? riskAssessment.RiskAssessmentSite.Id : 0,
                       Site = riskAssessment.RiskAssessmentSite != null ? riskAssessment.RiskAssessmentSite.Name: string.Empty,
                   };
            model = AttachDropdownData(model);
            return model;
        }

        public EditSummaryViewModel GetViewModel(EditSummaryViewModel viewModel)
        {
            viewModel = AttachDropdownData(viewModel);
            return viewModel;
        }
        
        private EditSummaryViewModel AttachDropdownData(EditSummaryViewModel model)
        {
            var riskAssessmentAssessors = _riskAssessorService.Search(new SearchRiskAssessorRequest()
            {
                SiteId = (long)model.SiteId,
                CompanyId = _companyId,
                MaximumResults = 100
            });
            var assessors = riskAssessmentAssessors.ToList().Select(x => AutoCompleteViewModel.ForEmployeeNoJobTitle(x.Employee)).AddDefaultOption();
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