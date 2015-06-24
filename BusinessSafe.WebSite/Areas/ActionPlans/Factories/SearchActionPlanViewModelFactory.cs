using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;
using System.Linq;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.ActionPlans.Factories
{
    public class SearchActionPlanViewModelFactory : ISearchActionPlanViewModelFactory
    {
        private readonly IActionPlanService _actionPlanService;
        private readonly ISiteService _siteService;
        private readonly ISiteGroupService _siteGroupService;
        private long _companyId;
        private IList<long> _allowedSiteIds;
        private int _page;
        private int _size;
        private string _orderBy;
        private long? _siteGroupId;
        private long? _siteId;
        private string _submittedFrom;
        private string _submittedTo;
        private bool _showArchived;


        public SearchActionPlanViewModelFactory(IActionPlanService actionPlanService, ISiteGroupService siteGroupService,ISiteService siteService)
        {
            _actionPlanService = actionPlanService;
            _siteGroupService = siteGroupService;
            _siteService = siteService;
        }

        public ISearchActionPlanViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ISearchActionPlanViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds)
        {
            _allowedSiteIds = allowedSiteIds;
            return this;
        }

        public ISearchActionPlanViewModelFactory WithPageNumber(int page)
        {
            _page = page;
            return this;
        }

        public ISearchActionPlanViewModelFactory WithPageSize(int size)
        {
            _size = size;
            return this;
        }

        public ISearchActionPlanViewModelFactory WithOrderBy(string orderBy)
        {
            _orderBy = orderBy;
            return this;
        }

        public ISearchActionPlanViewModelFactory WithSiteGroupId(long? siteGroupId)
        {
            _siteGroupId = siteGroupId;
            return this;
        }

        public ISearchActionPlanViewModelFactory WithSiteId(long? siteId)
        {
            _siteId = siteId;
            return this;
        }

        public ISearchActionPlanViewModelFactory WithSubmittedFrom(string submittedFrom)
        {
            _submittedFrom = submittedFrom;
            return this;
        }

        public ISearchActionPlanViewModelFactory WithSubmittedTo(string submittedTo)
        {
            _submittedTo = submittedTo;
            return this;
        }

        public ISearchActionPlanViewModelFactory WithShowArchived(bool showArchived)
        {
            _showArchived = showArchived;
            return this;
        }

        public ActionPlanIndexViewModel GetViewModel()
        {

            var request = new SearchActionPlanRequest
            {
                CompanyId = _companyId,
                SiteId = _siteId,
                SiteGroupId = _siteGroupId,
                SubmittedFrom = string.IsNullOrEmpty(_submittedFrom) ? (DateTime?)null : DateTime.Parse(_submittedFrom),
                SubmittedTo = string.IsNullOrEmpty(_submittedTo) ? (DateTime?)null : DateTime.Parse(_submittedTo),
                Page = _page != default(int) ? _page : 1,
                PageSize = _size != default(int) ? _size : 10,
                OrderBy = string.IsNullOrEmpty(_orderBy) ? ActionPlanOrderByColumn.SubmittedOn : GetOrderBy(),
                Ascending = !string.IsNullOrEmpty(_orderBy) && Ascending(),
                ShowArchived = _showArchived,
                AllowedSiteIds = _allowedSiteIds
            };
            
            var count = _actionPlanService.Count(request);
            var actionPlans = _actionPlanService.Search(request);

            var viewModel = new ActionPlanIndexViewModel()
                                {
                                    Size = _size != default(int) ? _size : 10,
                                    Total = count,
                                    IsShowArchived = _showArchived
                                };

            var siteGroups = _siteGroupService.GetByCompanyId(_companyId);

            var sites = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = _companyId,
                AllowedSiteIds = _allowedSiteIds
            });

            viewModel.Sites = sites.Select(AutoCompleteViewModel.ForSite).AddDefaultOption().ToList();
            viewModel.SiteGroups = siteGroups.Select(AutoCompleteViewModel.ForSiteGroup).AddDefaultOption().ToList();
            
            viewModel.ActionPlans = actionPlans.Select(x=> new SearchActionPlanResultViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                Site = (x.Site != null) ? x.Site.Name : null,
                VisitDate = x.DateOfVisit,
                VisitBy = x.VisitBy,
                SubmittedDate = x.SubmittedOn,
                Status = x.Status == DerivedTaskStatusForDisplay.NoLongerRequired ? "Archived" : EnumHelper.GetEnumDescription(x.Status),
                EvaluationReportId = x.ExecutiveSummaryDocumentLibraryId
            });
            return viewModel;
        }


        private ActionPlanOrderByColumn GetOrderBy()
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

            ActionPlanOrderByColumn orderyByColumn;

            switch (orderBy)
            {
                case "Title":
                    orderyByColumn = ActionPlanOrderByColumn.Title;
                    break;
                case "Site":
                    orderyByColumn = ActionPlanOrderByColumn.Site;
                    break;
                case "VisitDateFormatted":
                    orderyByColumn = ActionPlanOrderByColumn.DateOfVisit;
                    break;
                case "VisitBy":
                    orderyByColumn = ActionPlanOrderByColumn.VisitBy;
                    break;
                case "SubmittedDateFormatted":
                    orderyByColumn = ActionPlanOrderByColumn.SubmittedOn;
                    break;
                case "Status":
                    orderyByColumn = ActionPlanOrderByColumn.SubmittedOn; //todo
                    break;
                default:
                    orderyByColumn = ActionPlanOrderByColumn.None;
                    break;
            }

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

    }
}