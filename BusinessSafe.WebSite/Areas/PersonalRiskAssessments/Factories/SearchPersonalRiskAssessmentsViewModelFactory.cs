using System.Collections.Generic;
using System;
using System.Linq;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.WebSite.Extensions;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public class SearchPersonalRiskAssessmentsViewModelFactory : ISearchPersonalRiskAssessmentsViewModelFactory
    {
        private string _title;
        private long _companyId;
        private string _createdFrom;
        private string _createdTo;
        private long? _siteGroupId;
        private bool _showDeleted;
        private bool _showArchived;
        private bool _isRiskAssessmentTemplating;
        private IList<long> _allowedSiteIds;
        private Guid _currentUserId;
        private long? _siteId;
        private readonly ISiteGroupService _siteGroupService;
        private readonly IPersonalRiskAssessmentService _riskAssessmentService;
        private readonly ISiteService _siteService;
        private int _page;
        private int _pageSize;
        private const int DEFAULT_PAGE_SIZE = 10;
        private string _orderBy;

        public SearchPersonalRiskAssessmentsViewModelFactory(
            ISiteGroupService siteGroupService,
            IPersonalRiskAssessmentService riskAssessmentService,
            ISiteService siteService)
        {
            _siteGroupService = siteGroupService;
            _riskAssessmentService = riskAssessmentService;
            _siteService = siteService;
        }

        public ISearchPersonalRiskAssessmentsViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ISearchPersonalRiskAssessmentsViewModelFactory WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public ISearchPersonalRiskAssessmentsViewModelFactory WithCreatedFrom(string createdFrom)
        {
            _createdFrom = createdFrom;
            return this;
        }

        public ISearchPersonalRiskAssessmentsViewModelFactory WithCreatedTo(string createdTo)
        {
            _createdTo = createdTo;
            return this;
        }

        public ISearchPersonalRiskAssessmentsViewModelFactory WithSiteGroupId(long? siteGroupId)
        {
            _siteGroupId = siteGroupId;
            return this;
        }

        public ISearchPersonalRiskAssessmentsViewModelFactory WithShowDeleted(bool showDeleted)
        {
            _showDeleted = showDeleted;
            return this;
        }

        public ISearchPersonalRiskAssessmentsViewModelFactory WithShowArchived(bool showArchived)
        {
            _showArchived = showArchived;
            return this;
        }

        public ISearchPersonalRiskAssessmentsViewModelFactory WithRiskAssessmentTemplatingMode(bool isRiskAssessmentTemplating)
        {
            _isRiskAssessmentTemplating = isRiskAssessmentTemplating;
            return this;
        }

        public ISearchPersonalRiskAssessmentsViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds)
        {
            _allowedSiteIds = allowedSiteIds;
            return this;
        }

        public ISearchPersonalRiskAssessmentsViewModelFactory WithCurrentUserId(Guid currentUserId)
        {
            _currentUserId = currentUserId;
            return this;
        }

        public ISearchPersonalRiskAssessmentsViewModelFactory WithSiteId(long? siteId)
        {
            _siteId = siteId;
            return this;
        }

        public ISearchPersonalRiskAssessmentsViewModelFactory WithPageNumber(int page)
        {
            _page = page;
            return this;
        }

        public ISearchPersonalRiskAssessmentsViewModelFactory WithPageSize(int pageSize)
        {
            _pageSize = pageSize;
            return this;
        }

        /// <summary>
        /// Telerik uses the following format.  Reference-asc
        /// </summary>
        /// <param name="orderBy"></param>
        public ISearchPersonalRiskAssessmentsViewModelFactory WithOrderBy(string orderBy)
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

            return CreatePersonalRiskAssessmentsViewModel(riskAssessments, siteGroups, sites, count);
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
                Page = _page != default(int) ? _page : 1,
                PageSize = _pageSize != default(int) ? _pageSize : DEFAULT_PAGE_SIZE,
                OrderBy = RiskAssessmentTelerikOrderByHelper.GetOrderBy(_orderBy),
                OrderByDirection = RiskAssessmentTelerikOrderByHelper.GetOrderByDirection(_orderBy)
            };

            return riskAssessmentsSearchRequest;
        }

        private SearchRiskAssessmentsViewModel CreatePersonalRiskAssessmentsViewModel(
            IEnumerable<PersonalRiskAssessmentDto> riskAssessments,
            IEnumerable<SiteGroupDto> siteGroups,
            IEnumerable<SiteDto> sites, int count)
        {
            return new SearchRiskAssessmentsViewModel()
            {
                CompanyId = _companyId,
                CreatedFrom = _createdFrom,
                CreatedTo = _createdTo,
                IsRiskAssessmentTemplating = _isRiskAssessmentTemplating,
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
                    IsDeleted = ra.Deleted,
                }).ToList(),
                PageSize = _pageSize != default(int) ? _pageSize : DEFAULT_PAGE_SIZE,
                Total = count
            };
        }

    }
}