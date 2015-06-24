using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using Telerik.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Controllers
{
    public class SearchController : BaseController
    {
        private readonly ISearchAccidentRecordViewModelFactory _searchAccidentRecordViewModelFactory ;

        public SearchController(ISearchAccidentRecordViewModelFactory searchAccidentRecordViewModelFactory)
        {
            _searchAccidentRecordViewModelFactory = searchAccidentRecordViewModelFactory;
        }

        // todo: create specific permissions for view accident reports
        [GridAction(EnableCustomBinding = true, GridName = "AccidentRecordsGrid")]
        [PermissionFilter(Permissions.ViewAccidentRecords)]
        public ViewResult Index(AccidentRecordsIndexViewModel model)
        {
            
            _searchAccidentRecordViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithTitle(model.Title)
                .WithShowDeleted(model.IsShowDeleted)
                .WithPageNumber(model.Page)
                .WithAllowedSites(CurrentUser.GetSitesFilter())
                .WithPageSize(model.Size)
                .WithOrderBy(model.OrderBy);
            
            if (model.SiteId.HasValue)
                _searchAccidentRecordViewModelFactory.WithSiteId(model.SiteId.Value);
           
            if (!string.IsNullOrEmpty(model.CreatedFrom))
                _searchAccidentRecordViewModelFactory.WithCreatedFrom(DateTime.Parse(model.CreatedFrom));

            if (!string.IsNullOrEmpty(model.CreatedTo))
                _searchAccidentRecordViewModelFactory.WithCreatedTo(DateTime.Parse(model.CreatedTo));

            if (!string.IsNullOrEmpty(model.InjuredPersonForename))
                _searchAccidentRecordViewModelFactory.WithInjuredPersonForename(model.InjuredPersonForename);

            if (!string.IsNullOrEmpty(model.InjuredPersonSurname))
                _searchAccidentRecordViewModelFactory.WithInjuredPersonSurname(model.InjuredPersonSurname);

            model = _searchAccidentRecordViewModelFactory               
                                    .GetViewModel();

            return View("Index", model);
        }  
    }
}
