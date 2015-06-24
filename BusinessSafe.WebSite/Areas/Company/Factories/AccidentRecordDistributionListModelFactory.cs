using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.Factories
{
    public class AccidentRecordDistributionListModelFactory : IAccidentRecordDistributionListModelFactory
    {
        private readonly IEmployeeService _employeeService;
        private readonly ISiteService _siteService;

        private long _companyId;
        private long _siteId;
        private IList<long> _allowedSiteIDs;

        public AccidentRecordDistributionListModelFactory(IEmployeeService employeeService,
                                                            ISiteService siteService)
        {
            _employeeService = employeeService;
            _siteService = siteService;
        }

        public IAccidentRecordDistributionListModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IAccidentRecordDistributionListModelFactory WithSiteId(long siteId)
        {
            _siteId = siteId;
            return this;
        }

        public IAccidentRecordDistributionListModelFactory WithAllowedSites(IList<long> sites)
        {
            _allowedSiteIDs = sites;
            return this;
        }

        public AccidentRecordDistributionListViewModel GetViewModel()
        {      
            AccidentRecordDistributionListViewModel model = new AccidentRecordDistributionListViewModel();
            model.SiteId = _siteId;
            model.Sites = GetSites();
            model.SelectedEmployees =  new List<SelectedEmployeeViewModel>();
          
            if (_siteId > 0)
            {
                var site = _siteService.GetByIdAndCompanyId(_siteId, _companyId);
                if(site != null)
                {
                    model.SelectedEmployees = _siteService.GetAccidentRecordNotificationMembers(_siteId)
                        .Select(x => new SelectedEmployeeViewModel() {Email = x.Email(), EmployeeId = x.EmployeeId(), Name = x.FullName()})
                        .ToList();
                }
            }

            model.EmployeesToSelectFrom = _employeeService
                                          .GetAll(_companyId)
                                          .Where(emp => emp.SiteId == 0 || _allowedSiteIDs.Contains(emp.SiteId))
                                          .Where(emp => !model.SelectedEmployees.Select(x => x.EmployeeId).Contains(emp.Id))
                                          .ToList();

            return model;
        }

        private IEnumerable<AutoCompleteViewModel> GetSites()
        {
            var siteDtos = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = _companyId,
                AllowedSiteIds = _allowedSiteIDs
            });
            return siteDtos.Select(AutoCompleteViewModel.ForSite).AddDefaultOption();
        }
    }

}