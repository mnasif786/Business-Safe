using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories
{
    public class SearchRiskAssessmentViewModelFactory : ISearchRiskAssessmentViewModelFactory
    {
        private long _companyId;

        private string _createdFrom;
        private string _createdTo;
        private string _title;
        private long? _siteGroupId;
        private bool _showDeleted;
        private bool _showArchived;
        private bool _isRiskAssessmentTemplating;
        private IList<long> _allowedSiteIds;
        private Guid _currentUserId;
        private long? _siteId;
        private int _page;
        private int _pageSize;
        private readonly ISiteGroupService _siteGroupService;
        private readonly IFireRiskAssessmentService _riskAssessmentService;
        private readonly ISiteService _siteService;
        private string _orderBy;

        public SearchRiskAssessmentViewModelFactory(
            ISiteGroupService siteGroupService,
            IFireRiskAssessmentService riskAssessmentService,
            ISiteService siteService)
        {
            _siteGroupService = siteGroupService;
            _riskAssessmentService = riskAssessmentService;
            _siteService = siteService;
        }

        public ISearchRiskAssessmentViewModelFactory WithCurrentUserId(Guid currentUserId)
        {
            _currentUserId = currentUserId;

            return this;
        }

        public ISearchRiskAssessmentViewModelFactory WithSiteId(long? siteId)
        {
            _siteId = siteId;
            return this;
        }

        public ISearchRiskAssessmentViewModelFactory WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public ISearchRiskAssessmentViewModelFactory WithPageNumber(int page)
        {
            _page = page;
            return this;
        }

        public ISearchRiskAssessmentViewModelFactory WithPageSize(int pageSize)
        {
            _pageSize = pageSize;
            return this;
        }

        public ISearchRiskAssessmentViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ISearchRiskAssessmentViewModelFactory WithCreatedFrom(string createdFrom)
        {
            _createdFrom = createdFrom;
            return this;
        }

        public ISearchRiskAssessmentViewModelFactory WithCreatedTo(string createdTo)
        {
            _createdTo = createdTo;
            return this;
        }

        public ISearchRiskAssessmentViewModelFactory WithSiteGroupId(long? siteGroupId)
        {
            _siteGroupId = siteGroupId;
            return this;
        }

        public ISearchRiskAssessmentViewModelFactory WithShowDeleted(bool showDeleted)
        {
            _showDeleted = showDeleted;
            return this;
        }

        public ISearchRiskAssessmentViewModelFactory WithShowArchived(bool showArchived)
        {
            _showArchived = showArchived;
            return this;
        }

        public ISearchRiskAssessmentViewModelFactory WithRiskAssessmentTemplatingMode(bool isRiskAssessmentTemplating)
        {
            _isRiskAssessmentTemplating = isRiskAssessmentTemplating;
            return this;
        }

        public ISearchRiskAssessmentViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds)
        {
            _allowedSiteIds = allowedSiteIds;
            return this;
        }

        /// <summary>
        /// Telerik uses the following format.  Reference-asc
        /// </summary>
        /// <param name="orderBy"></param>
        public ISearchRiskAssessmentViewModelFactory WithOrderBy(string orderBy)
        {
            _orderBy = orderBy;
            return this;
        }

        public SearchRiskAssessmentsViewModel GetViewModel()
        {
            var riskAssessmentsSearchRequest = CreateRiskAssessmentsSearchRequest();
            var count = _riskAssessmentService.Count(riskAssessmentsSearchRequest);
            var riskAssessments = _riskAssessmentService.Search(riskAssessmentsSearchRequest);

            var siteGroups = _siteGroupService.GetByCompanyId(_companyId);

            var sites = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = _companyId,
                AllowedSiteIds = _allowedSiteIds
            });

            return CreateFireRiskAssessmentsViewModel(riskAssessments, siteGroups, sites, count);
        }

        private SearchRiskAssessmentsRequest CreateRiskAssessmentsSearchRequest()
        {
            var riskAssessmentsSearchRequest = new SearchRiskAssessmentsRequest
            {
                Title = _title,
                CompanyId = _companyId,
                CreatedFrom = string.IsNullOrEmpty(_createdFrom) ? (DateTime?)null : DateTime.Parse(_createdFrom),
                CreatedTo = string.IsNullOrEmpty(_createdTo) ? (DateTime?)null : DateTime.Parse(_createdTo),
                SiteGroupId = _siteGroupId,
                ShowDeleted = _showDeleted,
                ShowArchived = _showArchived,
                AllowedSiteIds = _allowedSiteIds,
                CurrentUserId = _currentUserId,
                SiteId = _siteId,
                Page = _page,
                PageSize = _pageSize,
                OrderBy = RiskAssessmentTelerikOrderByHelper.GetOrderBy(_orderBy),
                OrderByDirection = RiskAssessmentTelerikOrderByHelper.GetOrderByDirection(_orderBy)
            };

            return riskAssessmentsSearchRequest;
        }

        private SearchRiskAssessmentsViewModel CreateFireRiskAssessmentsViewModel(
            IEnumerable<FireRiskAssessmentDto> riskAssessments,
            IEnumerable<SiteGroupDto> siteGroups,
            IEnumerable<SiteDto> sites,
            int count)
        {
            var viewModel = new SearchRiskAssessmentsViewModel
                            {
                                CompanyId = _companyId,
                                CreatedFrom = _createdFrom,
                                CreatedTo = _createdTo,
                                SiteGroups = siteGroups.Select(AutoCompleteViewModel.ForSiteGroup).AddDefaultOption(),
                                Sites = sites != null
                                    ? sites.OrderBy(x => x.Name).Select(AutoCompleteViewModel.ForSite).AddDefaultOption()
                                    : new List<AutoCompleteViewModel>(),
                                IsShowDeleted = _showDeleted,
                                IsShowArchived = _showArchived,
                                RiskAssessments = riskAssessments.Select(ra => new SearchRiskAssessmentResultViewModel()
                                                                               {
                                                                                   Id = ra.Id,
                                                                                   Reference = ra.Reference,
                                                                                   Title = ra.Title,
                                                                                   Site =
                                                                                       ra.RiskAssessmentSite != null
                                                                                           ? ra.RiskAssessmentSite.Name
                                                                                           : string.Empty,
                                                                                   AssignedTo =
                                                                                       ra.RiskAssessor != null
                                                                                           ? ra.RiskAssessor.FormattedName
                                                                                           : string.Empty,
                                                                                   Status = ra.Status.ToString(),
                                                                                   AssessmentDate = ra.AssessmentDate,
                                                                                   NextReviewDate = ra.NextReviewDate,
                                                                                   IsDeleted = ra.Deleted
                                                                               }).ToList(),
                                PageSize = _pageSize,
                                Total = count,
                                MultiSelectSites = sites != null
                                    ? sites.OrderBy(x => x.Name).Select(x => new SiteMultiSelectViewModel() { Id = x.Id, Name = x.Name })
                                    : new List<SiteMultiSelectViewModel>()
                            };
            
            return viewModel;
        }

       

        private SearchRiskAssessmentsViewModel CreateGeneralRiskAssessmentsViewModel(
            IEnumerable<GeneralRiskAssessmentDto> riskAssessments, IEnumerable<SiteGroupDto> siteGroups,
            IEnumerable<SiteDto> sites, int count)
        {
            return new SearchRiskAssessmentsViewModel()
            {
                CompanyId = _companyId,
                CreatedFrom = _createdFrom,
                CreatedTo = _createdTo,
                SiteGroups = siteGroups.Select(AutoCompleteViewModel.ForSiteGroup).AddDefaultOption(),
                Sites = sites != null ? sites.OrderBy(x => x.Name).Select(AutoCompleteViewModel.ForSite).AddDefaultOption() : new List<AutoCompleteViewModel>(),
                IsShowDeleted = _showDeleted,
                IsShowArchived = _showArchived,
                MultiSelectSites = sites != null
                    ? sites.OrderBy(x => x.Name).Select(x => new SiteMultiSelectViewModel() { Id = x.Id, Name = x.Name })
                    : new List<SiteMultiSelectViewModel>(),
                RiskAssessments = riskAssessments.Select(ra => new SearchRiskAssessmentResultViewModel()
                {
                    Id = ra.Id,
                    Reference = ra.Reference,
                    Title = ra.Title,
                    Site = ra.RiskAssessmentSite != null ? ra.RiskAssessmentSite.Name : string.Empty,
                    AssignedTo = ra.RiskAssessor != null ? ra.RiskAssessor.FormattedName : string.Empty,
                    Status = ra.Status.ToString(),
                    AssessmentDate = ra.AssessmentDate,
                    NextReviewDate = ra.NextReviewDate,
                    IsDeleted = ra.Deleted
                }).ToList(),
                PageSize = _pageSize,
                Total = count
            };
        }

       
    }
}