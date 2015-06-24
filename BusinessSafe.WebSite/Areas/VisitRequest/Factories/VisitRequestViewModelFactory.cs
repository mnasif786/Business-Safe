using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.VisitRequest.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.VisitRequest.Factories
{
    public interface IVisitRequestViewModelFactory
    {
        IVisitRequestViewModelFactory WithCompanyId(long companyId);
        IVisitRequestViewModelFactory WithSiteId(long? siteId);
        IVisitRequestViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds);
        IVisitRequestViewModelFactory WithPersonToVisit(string personName);
        IVisitRequestViewModelFactory WithEmailAddress(string emailAddress);
        IVisitRequestViewModelFactory WithVisitTimePreference(string visitTimePreference);
        VisitRequestViewModel GetViewModel();
        VisitRequestViewModel GetViewModel(VisitRequestViewModel postedViewModel);
    }

    public class VisitRequestViewModelFactory : IVisitRequestViewModelFactory
    {
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly ISiteService _siteService;

        private long _companyId;
        private long? _SiteId;
        private IList<long> _allowedSiteIds;
        private string  _personNameToVisit;
        private string  _emailAddress;
        private string _visitTimePreference;

        public VisitRequestViewModelFactory(ISiteService siteService, ICompanyDetailsService companyDetailsService)
        {
            _siteService = siteService;
            _companyDetailsService = companyDetailsService;
        }

        public IVisitRequestViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IVisitRequestViewModelFactory WithSiteId(long? siteId)
        {
            _SiteId = siteId;
            return this;
        }

        public IVisitRequestViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds)
        {
            _allowedSiteIds = allowedSiteIds;
            return this;
        }

        public IVisitRequestViewModelFactory WithPersonToVisit(string personName)
        {
            _personNameToVisit = personName;
            return this;
        }

        public IVisitRequestViewModelFactory WithEmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;
            return this;
        }

        public IVisitRequestViewModelFactory WithVisitTimePreference(string visitTimePreference)
        {
            _visitTimePreference = visitTimePreference;
            return this;
        }
        public VisitRequestViewModel GetViewModel()
        {
            var viewModel = new VisitRequestViewModel();

            var companyDetailsFromPeninsula = _companyDetailsService.GetCompanyDetails(_companyId);

            var sites = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = _companyId,
                AllowedSiteIds = _allowedSiteIds
            });

            var selectedSite = sites.First(s => s.Parent == null); //Defaults to Main site

            viewModel.Sites = sites.Select(AutoCompleteViewModel.ForSite).AddDefaultOption().ToList();
            viewModel.SiteId = selectedSite.Id;
            viewModel.SiteName = selectedSite.Name;
            viewModel.VisitTimePreference = _visitTimePreference;
            viewModel.PersonNameToVisit = _personNameToVisit;
            viewModel.EmailAddress = _emailAddress;
            viewModel.CAN = companyDetailsFromPeninsula.CAN;
            viewModel.CompanyName = companyDetailsFromPeninsula.CompanyName;
            viewModel.Postcode = companyDetailsFromPeninsula.PostCode;
            
            return viewModel;
        }

        public VisitRequestViewModel GetViewModel(VisitRequestViewModel postedViewModel)
        {
            var viewModel = new VisitRequestViewModel();

            var companyDetailsFromPeninsula = _companyDetailsService.GetCompanyDetails(_companyId);

            var sites = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = _companyId,
                AllowedSiteIds = _allowedSiteIds
            });

            viewModel.Sites = sites.Select(AutoCompleteViewModel.ForSite).AddDefaultOption().ToList();

            if (postedViewModel.SiteId.HasValue)
            {
                var selectedSite = sites.First(s => s.Id == postedViewModel.SiteId);
                viewModel.SiteId = selectedSite.Id;
                viewModel.SiteName = selectedSite.Name;
            }

            viewModel.VisitTimePreference = postedViewModel.VisitTimePreference;
            viewModel.PersonNameToVisit = postedViewModel.PersonNameToVisit;
            viewModel.EmailAddress = postedViewModel.EmailAddress;
            viewModel.VisitFrom = postedViewModel.VisitFrom;
            viewModel.VisitTo = postedViewModel.VisitTo;
            viewModel.Comments = postedViewModel.Comments;
            viewModel.ContactNumber = postedViewModel.ContactNumber;

            viewModel.CAN = companyDetailsFromPeninsula.CAN;
            viewModel.CompanyName = companyDetailsFromPeninsula.CompanyName;
            viewModel.Postcode = companyDetailsFromPeninsula.PostCode;

            return viewModel;
        }
    }
}