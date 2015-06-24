using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;
using RestSharp.Extensions;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public sealed class SearchResponsibilityViewModelFactory : ISearchResponsibilityViewModelFactory
    {
        private readonly ISiteGroupService _siteGroupService;
        private readonly ISiteService _siteService;
        private readonly IResponsibilitiesService _responsibilitiesService;
        private long _companyId;
        private long _categoryId;
        private long _siteId;
        private long _siteGroupId;
        private string _title;
        private string _createdFrom;
        private string _createdTo;
        private bool _showDeleted;        
        private int _page;
        private int _pageSize;
        private string _orderBy;
        private IList<long> _allowedSiteIds;
     
        private const int DEFAULT_PAGE_SIZE = 10;


        public SearchResponsibilityViewModelFactory(IResponsibilitiesService responsibilitiesService, ISiteGroupService siteGroupService, ISiteService siteService)
        {
            _responsibilitiesService = responsibilitiesService;
            _siteGroupService = siteGroupService;
            _siteService = siteService;
        }

        public ISearchResponsibilityViewModelFactory WithCompanyId(long Id)
        {
            _companyId = Id;
            return this;
        }

        public ISearchResponsibilityViewModelFactory WithCategoryId(long categoryId)
        {
            _categoryId = categoryId;
            return this;
        }

        public ISearchResponsibilityViewModelFactory WithSiteId(long siteId)
        {
            _siteId = siteId;
            return this;
        }

        public ISearchResponsibilityViewModelFactory WithSiteGroupId(long siteGroupId)
        {
            _siteGroupId = siteGroupId;
            return this;
        }

        public ISearchResponsibilityViewModelFactory WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public ISearchResponsibilityViewModelFactory WithCreatedFrom(DateTime createdFrom)
        {
            _createdFrom = createdFrom.ToShortDateString();
            return this;
        }

        public ISearchResponsibilityViewModelFactory WithCreatedTo(DateTime createdTo)
        {
            _createdTo = createdTo.ToShortDateString();
            return this;
        }

        public ISearchResponsibilityViewModelFactory WithShowDeleted(bool showDeleted)
        {
            _showDeleted = showDeleted;
            return this;
        }

        public ISearchResponsibilityViewModelFactory WithPageSize(int pageSize)
        {
            _pageSize = pageSize;
            return this;
        }

        public ISearchResponsibilityViewModelFactory WithPageNumber(int page)
        {
            _page = page;
            return this;
        }

        public ISearchResponsibilityViewModelFactory WithOrderBy(string orderBy)
        {
            _orderBy = orderBy;
            return this;
        }

        public ISearchResponsibilityViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds)
        {
            _allowedSiteIds = allowedSiteIds;
            return this;
        }           

        public ResponsibilitiesIndexViewModel GetViewModel()
        {
            var sites = GetSites();
            var siteGroups = GetSiteGroups();
            var categories = GetResponsibilityCategories();
            var responsibilities = GetResponsibilities();

            var request = CreateSearchResponsibilitiesRequest();
            var total = _responsibilitiesService.Count(request);
            var viewModel = new ResponsibilitiesIndexViewModel
            {
                Sites = sites,
                SiteGroups = siteGroups,
                Categories = categories,
                Responsibilities = responsibilities,
                CategoryId = _categoryId,
                CreatedFrom = _createdFrom,
                CreatedTo = _createdTo,
                Title = _title,
                SiteId = _siteId,
                SiteGroupId = _siteGroupId,
                IsShowDeleted = _showDeleted,
                Page = _page,
                PageSize = _pageSize,
                Total = total
            };

            return viewModel;
        }

        private IEnumerable<SearchResponsibilitiesResultViewModel> GetResponsibilities()
        {
            var request = CreateSearchResponsibilitiesRequest();
            var dtos = _responsibilitiesService.Search( request );

            return dtos.Select(x => new SearchResponsibilitiesResultViewModel()
                                          {
                                              AssignedTo = x.Owner != null ? x.Owner.FullName : null,
                                              Category = x.ResponsibilityCategory.Category,
                                              CreatedDate = x.CreatedOn,
                                              Description = x.Description.TruncateWithEllipsis(50),
                                              DueDate = x.NextDueDate,
                                              Frequency = x.HasMultipleFrequencies ? "Multiple" : EnumHelper.GetEnumDescription(x.InitialTaskReoccurringType), // todo
                                              Id = x.Id,
                                              IsDeleted = x.Deleted,
                                              Reason = x.ResponsibilityReason !=null ? x.ResponsibilityReason.Reason : string.Empty,
                                              Site = x.Site != null ? x.Site.Name : null,
                                              Status = EnumHelper.GetEnumDescription(x.StatusDerivedFromTasks),
                                              Title = x.Title,
                                                                                           
                                          }).ToList();
        }

        private SearchResponsibilitiesRequest CreateSearchResponsibilitiesRequest()
        {
            SearchResponsibilitiesRequest request = new SearchResponsibilitiesRequest()
                                                        {
                                                            CompanyId = _companyId,
                                                            ResponsibilityCategoryId = _categoryId,
                                                            ShowDeleted = _showDeleted,
                                                            SiteId = _siteId,
                                                            SiteGroupId = _siteGroupId,
                                                            Title = _title,
                                                            CreatedFrom =
                                                                string.IsNullOrEmpty(_createdFrom)
                                                                    ? (DateTime?) null
                                                                    : DateTime.Parse(_createdFrom),
                                                            CreatedTo =
                                                                string.IsNullOrEmpty(_createdTo)
                                                                    ? (DateTime?) null
                                                                    : DateTime.Parse(_createdTo),
                                                            Page = _page != default(int) ? _page : 1,
                                                            PageSize = _pageSize != default(int) ? _pageSize : DEFAULT_PAGE_SIZE,
                                                            OrderBy = GetOrderBy(),
                                                            Ascending =  Ascending(),
                                                            AllowedSiteIds = _allowedSiteIds
                                                        };
            return request;
        }

        private ResponsibilitiesRequestOrderByColumn GetOrderBy()
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

            var orderyByColumn = ResponsibilitiesRequestOrderByColumn.None;
            Enum.TryParse(orderBy, out orderyByColumn);

            return orderyByColumn;
        }

        private bool Ascending()
        {
            var isAscending = true;

            if (!string.IsNullOrEmpty(_orderBy))
            {
                string[] parts = _orderBy.Split('-');
                if (parts.Length == 2)
                {
                    isAscending = parts[1] == "asc";
                }
            }
            return isAscending;
        }

        //private string GetResponsibilityStatus(ResponsibilityDto responsibilityDto)
        //{
        //    var status = string.Empty;
        //    if(responsibilityDto.ResponsibilityTasks.Any(x => x.TaskStatus == TaskStatus.Outstanding.ToString()
        //        && DateTime.Parse(x.TaskCompletionDueDate) < DateTime.Now.Date))
        //    {
        //        status = TaskStatus.Overdue.ToString();
        //    }
        //    else if (responsibilityDto.ResponsibilityTasks.Any(x => x.TaskStatus == TaskStatus.Outstanding.ToString()
        //        && DateTime.Parse(x.TaskCompletionDueDate) >=  DateTime.Now.Date))
        //    {
        //        status = TaskStatus.Outstanding.ToString();
        //    }
        //    else if (responsibilityDto.ResponsibilityTasks.Any(x => x.TaskStatus == TaskStatus.Completed.ToString()))
        //    {
        //        status = TaskStatus.Completed.ToString();
        //    }
        //    else
        //    {
        //        status = string.Empty;
        //    }
        //    return status;
        //}

        private IEnumerable<AutoCompleteViewModel> GetSiteGroups()
        {
            var linkedGroupsDtos = _siteGroupService.GetByCompanyId(_companyId);
            return linkedGroupsDtos.Where(x => _allowedSiteIds.Contains(x.Id)).Select(AutoCompleteViewModel.ForSiteGroup).AddDefaultOption();
        }

        private IEnumerable<AutoCompleteViewModel> GetSites()
        {
            var siteDtos = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = _companyId,
                AllowedSiteIds = _allowedSiteIds
            });
            return siteDtos.Select(AutoCompleteViewModel.ForSite).AddDefaultOption();
        }

        private IEnumerable<AutoCompleteViewModel> GetResponsibilityCategories()
        {
            var responsibilityCategoryDtos = _responsibilitiesService.GetResponsibilityCategories();
            return
                responsibilityCategoryDtos.Select(AutoCompleteViewModel.ForResponsibilityCategory).AddDefaultOption();
        }

    }
}