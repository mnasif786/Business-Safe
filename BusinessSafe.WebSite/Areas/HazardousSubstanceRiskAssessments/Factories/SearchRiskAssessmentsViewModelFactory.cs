using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public class SearchRiskAssessmentsViewModelFactory : ISearchRiskAssessmentsViewModelFactory
    {
        private string _title;
        private string _createFrom;
        private string _createTo;
        private long _companyId;
        private bool _showDeleted;
        private bool _showArchived;
        private long _hazardousSubstanceId;
        private Guid _currentUserId;
        private IList<long> _allowedSiteIds;
        private long? _siteId;
        private long? _siteGroupId;
        private readonly ISiteService _siteService;
        private readonly ISiteGroupService _siteGroupService;
        private int _page;
        private int _pageSize;
        private string _orderBy;

        public SearchRiskAssessmentsViewModelFactory(ISiteService siteService, ISiteGroupService siteGroupService)
        {
            _siteService = siteService;
            _siteGroupService = siteGroupService;
        }
        
        public ISearchRiskAssessmentsViewModelFactory WithTitle(string title)
        {
            _title = title;
            return this;
        }
        
        public ISearchRiskAssessmentsViewModelFactory WithCurrentUserId(Guid currentUserId)
        {
            _currentUserId = currentUserId;
            return this;
        }

        public ISearchRiskAssessmentsViewModelFactory WithSiteId(long? siteId)
        {
            _siteId = siteId;
            return this;
        }

        public ISearchRiskAssessmentsViewModelFactory WithSiteGroupId(long? siteGroupId)
        {
            _siteGroupId = siteGroupId;
            return this;
        }

        public ISearchRiskAssessmentsViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ISearchRiskAssessmentsViewModelFactory WithCreatedFrom(string createFrom)
        {
            _createFrom = createFrom;
            return this;
        }

        public ISearchRiskAssessmentsViewModelFactory WithCreatedTo(string createTo)
        {
            _createTo = createTo;
            return this;
        }

        public ISearchRiskAssessmentsViewModelFactory WithShowDeleted(bool showDeleted)
        {
            _showDeleted = showDeleted;
            return this;
        }

        public ISearchRiskAssessmentsViewModelFactory WithShowArchived(bool showArchived)
        {
            _showArchived = showArchived;
            return this;
        }

        public ISearchRiskAssessmentsViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds)
        {
            _allowedSiteIds = allowedSiteIds;
            return this;
        }

        public virtual ISearchRiskAssessmentsViewModelFactory WithPageNumber(int page)
        {
            _page = page;
            return this;
        }

        public ISearchRiskAssessmentsViewModelFactory WithPageSize(int pageSize)
        {
            _pageSize = pageSize;
            return this;
        }

        public ISearchRiskAssessmentsViewModelFactory WithOrderBy(string order)
        {
            _orderBy = order;
            return this;
        }
        
        public SearchRiskAssessmentsViewModel GetViewModel(IHazardousSubstanceRiskAssessmentService riskAssessmentService)
        {
            var searchRequest = CreateSearchRequest();
            var count = riskAssessmentService.Count(searchRequest);
            var riskAssessments = riskAssessmentService.Search(searchRequest);
            var siteGroups = _siteGroupService.GetByCompanyId(_companyId);
            var sites = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = _companyId,
                AllowedSiteIds = _allowedSiteIds
            });
            return CreateViewModel(riskAssessments, sites, siteGroups,count);
        }

        public ISearchRiskAssessmentsViewModelFactory WithHazardousSubstanceId(long hazardousSubstanceId)
        {
            _hazardousSubstanceId = hazardousSubstanceId;
            return this;
        }

        private SearchHazardousSubstanceRiskAssessmentsRequest CreateSearchRequest()
        {
            var searchRequest = new SearchHazardousSubstanceRiskAssessmentsRequest
                                     {
                                         Title = _title,
                                         CompanyId = _companyId,
                                         ShowDeleted = _showDeleted,
                                         ShowArchived = _showArchived,
                                         HazardousSubstanceId = _hazardousSubstanceId,
                                         AllowedSiteIds = _allowedSiteIds,
                                         CurrentUserId = _currentUserId,
                                         SiteGroupId = _siteGroupId,
                                         SiteId = _siteId,
                                         Page = _page,
                                         PageSize = _pageSize,
                                         OrderBy = GetOrderBy(_orderBy),
                                         OrderByDirection = GetOrderByDirection(_orderBy)
                                     };


            if (!string.IsNullOrEmpty(_createFrom))
            {
                DateTime createdFromDate;
                DateTime.TryParse(_createFrom, out createdFromDate);
                searchRequest.CreatedFrom = createdFromDate;
            }

            if (!string.IsNullOrEmpty(_createTo))
            {
                DateTime createdToDate;
                DateTime.TryParse(_createTo, out createdToDate);
                searchRequest.CreatedTo = createdToDate;
            }

            return searchRequest;
        }

        private SearchRiskAssessmentsViewModel CreateViewModel(
            IEnumerable<HazardousSubstanceRiskAssessmentDto> riskAssessments, IEnumerable<SiteDto> sites,
            IEnumerable<SiteGroupDto> siteGroups, int count)
        {
            return new SearchRiskAssessmentsViewModel
            {
                CompanyId = _companyId,
                CreatedFrom = _createFrom,
                CreatedTo = _createTo,
                SiteId = _siteId.HasValue ? _siteId.Value : default(long),
                SiteGroupId = _siteGroupId,
                Sites = sites.Select(AutoCompleteViewModel.ForSite).AddDefaultOption(),
                SiteGroups = siteGroups.Select(AutoCompleteViewModel.ForSiteGroup).AddDefaultOption(),
                IsShowDeleted = _showDeleted,
                IsShowArchived = _showArchived,
                MultiSelectSites = sites != null
                    ? sites.OrderBy(x => x.Name).Select(x => new SiteMultiSelectViewModel() {Id = x.Id, Name = x.Name})
                    : new List<SiteMultiSelectViewModel>(),
                RiskAssessments =
                    riskAssessments.Select(ra => new SearchRiskAssessmentResultViewModel()
                    {
                        Id = ra.Id,
                        Reference = ra.Reference,
                        Title = ra.Title,
                        AssignedTo = ra.RiskAssessor == null ? "" : ra.RiskAssessor.Employee.FullName,
                        Status = ra.Status.ToString(),
                        AssessmentDate = ra.AssessmentDate,
                        NextReviewDate = ra.NextReviewDate,
                        IsDeleted = ra.Deleted,
                        Site = ra.Site == null ? "" : ra.Site.Name
                    }).ToList(),
                PageSize = _pageSize,
                Total = count,
            };
        }

        private RiskAssessmentOrderByColumn GetOrderBy(string orderBy)
        {
            var columnNameMapping = new Dictionary<string, RiskAssessmentOrderByColumn>();
            columnNameMapping.Add("Ref", RiskAssessmentOrderByColumn.Reference);
            columnNameMapping.Add("Reference", RiskAssessmentOrderByColumn.Reference);
            columnNameMapping.Add("Title", RiskAssessmentOrderByColumn.Title);
            columnNameMapping.Add("Site", RiskAssessmentOrderByColumn.Site);
            columnNameMapping.Add("AssignedTo", RiskAssessmentOrderByColumn.AssignedTo);
            columnNameMapping.Add("Status", RiskAssessmentOrderByColumn.Status);
            columnNameMapping.Add("AssessmentDateFormatted", RiskAssessmentOrderByColumn.AssessmentDate);
            columnNameMapping.Add("NextReviewDateFormatted", RiskAssessmentOrderByColumn.NextReview);
            columnNameMapping.Add("CreatedOn", RiskAssessmentOrderByColumn.CreatedOn);

            var columnName = GetColumnNameFromOrderBy(orderBy);

            if (string.IsNullOrEmpty(columnName))
            {
                return RiskAssessmentOrderByColumn.AssessmentDate;
            }

            if (columnNameMapping.ContainsKey(columnName))
            {
                return columnNameMapping[columnName];
            }
            else
            {
                return RiskAssessmentOrderByColumn.AssessmentDate;
            }

        }

        private string GetColumnNameFromOrderBy(string orderBy)
        {
            var columnName = String.Empty;
            if (!string.IsNullOrEmpty(orderBy))
            {
                string[] parts = _orderBy.Split('-');
                if (parts.Length == 2)
                {
                    columnName = parts[0];
                }
            }

            return columnName;
        }


        private string GetOrderBy()
        {
            var orderBy = string.Empty;
            if (!string.IsNullOrEmpty(_orderBy))
            {
                string[] parts = _orderBy.Split('-');
                if (parts.Length == 2)
                {
                    orderBy = parts[0];
                }
            }

            var columnName = string.Empty;
            switch (orderBy)
            {
                case "Reference":
                    columnName = "Reference";
                    break;
                case "Title":
                    columnName = "Title";
                    break;
                case "Site":
                    columnName = "RiskAssessmentSite";
                    break;
                default:
                    columnName = "CreatedOn";
                    break;
            }

            return columnName;
        }

        private string GetOrderByDirectionFromOrderBy(string orderBy)
        {
            var orderByDirection = String.Empty;
            if (!string.IsNullOrEmpty(orderBy))
            {
                string[] parts = _orderBy.Split('-');
                if (parts.Length == 2)
                {
                    orderByDirection = parts[1];
                }
            }

            return orderByDirection;
        }

        private OrderByDirection GetOrderByDirection(string orderBy)
        {
            var orderByDirection = GetOrderByDirectionFromOrderBy(orderBy);
            if (string.IsNullOrEmpty(orderByDirection))
            {
                return OrderByDirection.Ascending;
            }

            switch (orderByDirection)
            {
                case "desc":
                    return OrderByDirection.Descending;
                default:
                    return OrderByDirection.Ascending;
            }

        }
    }
}