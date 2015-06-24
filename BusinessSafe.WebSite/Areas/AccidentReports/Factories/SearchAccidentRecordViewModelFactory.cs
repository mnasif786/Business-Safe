using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.Application.Helpers;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Factories
{
    public sealed class SearchAccidentRecordViewModelFactory : ISearchAccidentRecordViewModelFactory
    {
        private readonly ISiteGroupService _siteGroupService;
        private readonly ISiteService _siteService;
        private readonly IAccidentRecordService _accidentRecordsService;

        private Guid _currentUserId; 
        private long _companyId;        
        private long? _siteId;
        private long _siteGroupId;
        private string _title;
        private string _createdFrom;
        private string _createdTo;
        private bool _showDeleted;
        private string _injuredPersonForename;
        private string _injuredPersonSurname;
        private IList<long> _sites;
        private int _page;
        private int _pageSize;
        private string _orderBy;
        private const int DEFAULT_PAGE_SIZE = 10;

        public SearchAccidentRecordViewModelFactory(IAccidentRecordService accidentRecordsService, ISiteGroupService siteGroupService, ISiteService siteService)
        {
            _accidentRecordsService = accidentRecordsService;
            _siteGroupService = siteGroupService;
            _siteService = siteService;
        }


        public ISearchAccidentRecordViewModelFactory WithCompanyId(long Id)
        {
            _companyId = Id;
            return this;
        }

        public ISearchAccidentRecordViewModelFactory WithSiteId(long? siteId)
        {
            _siteId = siteId;
            return this;
        }

        public ISearchAccidentRecordViewModelFactory WithAllowedSites(IList<long> siteIds)
        {
            _sites = siteIds;
            return this;
        }

        public ISearchAccidentRecordViewModelFactory WithCurrentUserId(Guid currentUserId)
        {
            _currentUserId = currentUserId;
            return this;
        }

        public ISearchAccidentRecordViewModelFactory WithSiteGroupId(long siteGroupId)
        {
            _siteGroupId = siteGroupId;
            return this;
        }

        public ISearchAccidentRecordViewModelFactory WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public ISearchAccidentRecordViewModelFactory WithCreatedFrom(DateTime createdFrom)
        {
            _createdFrom = createdFrom.ToShortDateString();
            return this;
        }

        public ISearchAccidentRecordViewModelFactory WithCreatedTo(DateTime createdTo)
        {
            _createdTo = createdTo.ToShortDateString();
            return this;
        }

        public ISearchAccidentRecordViewModelFactory WithShowDeleted(bool showDeleted)
        {
            _showDeleted = showDeleted;
            return this;
        }

        public ISearchAccidentRecordViewModelFactory WithInjuredPersonForename(string injuredPersonForename)
        {
            _injuredPersonForename = injuredPersonForename;
            return this;
        }

        public ISearchAccidentRecordViewModelFactory WithInjuredPersonSurname(string injuredPersonSurname)
        {
            _injuredPersonSurname = injuredPersonSurname;
            return this;
        }

        public ISearchAccidentRecordViewModelFactory WithPageNumber(int page)
        {
            _page = page;
            return this;
        }

        public ISearchAccidentRecordViewModelFactory WithPageSize(int pageSize)
        {
            _pageSize = pageSize;
            return this;
        }

        public ISearchAccidentRecordViewModelFactory WithOrderBy(string orderBy)
        {
            _orderBy = orderBy;
            return this;
        }

        private AccidentRecordstOrderByColumn GetOrderBy()
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

            var orderyByColumn = AccidentRecordstOrderByColumn.None;
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



        public ViewModels.AccidentRecordsIndexViewModel GetViewModel()
        {
            var request = new SearchAccidentRecordsRequest()
                              {
                                  CompanyId = _companyId,
                                  ShowDeleted = _showDeleted,
                                  SiteId = _siteId,
                                  Title = _title,
                                  CreatedFrom =
                                      string.IsNullOrEmpty(_createdFrom)
                                          ? (DateTime?) null
                                          : DateTime.Parse(_createdFrom),
                                  CreatedTo = string.IsNullOrEmpty(_createdTo) ? (DateTime?) null : DateTime.Parse(_createdTo),
                                  InjuredPersonForename = _injuredPersonForename,
                                  InjuredPersonSurname = _injuredPersonSurname,
                                  Page = _page != default(int) ? _page : 1,
                                  PageSize = _pageSize != default(int) ? _pageSize : DEFAULT_PAGE_SIZE,
                                  OrderBy = GetOrderBy(),
                                  Ascending = Ascending(),
                                  AllowedSiteIds = _sites
                              };

            var count = _accidentRecordsService.Count(request);
            var DTOs = _accidentRecordsService.Search(request);

            var viewModel = new AccidentRecordsIndexViewModel()
            {
                CreatedFrom = _createdFrom,
                CreatedTo = _createdTo,
                IsShowDeleted = _showDeleted,
                SiteId = _siteId,
                Title = _title,
                
                InjuredPersonForename = _injuredPersonForename,
                InjuredPersonSurname = _injuredPersonSurname,
                Size = _pageSize != default(int) ? _pageSize : DEFAULT_PAGE_SIZE,
                Total = count
            };

            
            viewModel.AccidentRecords  = DTOs.Select( x => new SearchAccidentRecordResultViewModel()
                                                        {
                                                            Id              = x.Id,
                                                            Reference       = x.Reference,
                                                            Title           = x.Title,                                                                                                                
                                                            Description     = x.DescriptionHowAccidentHappened != null ? x.DescriptionHowAccidentHappened.TruncateWithEllipsis(50) : null,
                                                            InjuredPerson   = x. InjuredPersonFullName,
                                                            Severity        = x.SeverityOfInjury == null    ? String.Empty : EnumHelper.GetEnumDescription( x.SeverityOfInjury ),
                                                            Site            = GetSiteDisplay(x),
                                                            ReportedBy      = x.CreatedBy == null           ? String.Empty : x.CreatedBy.Name,
                                                            DateCreated     = x.CreatedOn == null           ? String.Empty : x.CreatedOn.Value.ToShortDateString(),
                                                            DateOfAccident  = x.DateAndTimeOfAccident == null ? String.Empty : x.DateAndTimeOfAccident.Value.ToShortDateString(),
                                                            IsDeleted       = x.IsDeleted,
                                                            Status          = x.Status ? AccidentRecordStatusEnum.Open : AccidentRecordStatusEnum.Closed
                                                        }

                                                    ).ToList();

            var sites = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = _companyId,
                AllowedSiteIds = _sites
            });

            viewModel.Sites = sites.Select(AutoCompleteViewModel.ForSite)
                                    .AddDefaultOption(String.Empty)
                                    .WithOtherOption( new AutoCompleteViewModel("Off-site", "-1") )
                                    .ToList();
        
            return viewModel;
        }

        private string GetSiteDisplay(AccidentRecordDto accidentRecord)
        {
            var result = string.Empty;
            if(accidentRecord.SiteWhereHappened != null)
            {
                result = accidentRecord.SiteWhereHappened.Name;
            }  
            else if(!string.IsNullOrEmpty(accidentRecord.OffSiteSpecifics))
            {
                result = "Off-site";
            }
            
            return result; 
        }

        private IEnumerable<AutoCompleteViewModel> GetSiteGroups()
        {
            var linkedGroupsDtos = _siteGroupService.GetByCompanyId(_companyId);
            return linkedGroupsDtos.Select(AutoCompleteViewModel.ForSiteGroup);
        }

        private IEnumerable<AutoCompleteViewModel> GetSites()
        {
            var siteDtos = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = _companyId,
            });
            return siteDtos.Select(AutoCompleteViewModel.ForSite);
        }  
    }
}

