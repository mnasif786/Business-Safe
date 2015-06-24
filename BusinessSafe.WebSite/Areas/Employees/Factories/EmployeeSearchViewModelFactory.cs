using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Custom_Exceptions;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;
using NHibernate;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

using BusinessSafe.Domain.InfrastructureContracts.Logging;

namespace BusinessSafe.WebSite.Areas.Employees.Factories
{
    public class EmployeeSearchViewModelFactory : IEmployeeSearchViewModelFactory
    {
        private long _companyId;
        private long _siteId;
        private IList<long> _allowedSites; 
        private string _employeeReference = string.Empty;
        private string _forename = string.Empty;
        private string _surname = string.Empty;
        private bool _showDeleted;
        private IPrincipal _currentUser;       

        private readonly IEmployeeService _employeeService;
        private readonly ISiteService _siteService;
        private readonly ILookupService _lookupService;
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;
        

        public EmployeeSearchViewModelFactory(IEmployeeService employeeService, ISiteService siteService, ILookupService lookupService, IBusinessSafeSessionManager businessSafeSession)
        {
            _employeeService = employeeService;
            _siteService = siteService;
            _lookupService = lookupService;
            _businessSafeSessionManager = businessSafeSession;
        }

        public EmployeeSearchViewModel GetViewModel()
        {
            using (var session = _businessSafeSessionManager.Session)
            {
                session.FlushMode = FlushMode.Never;

                var employees = GetEmployees();
                var sites = GetAllowedSitesForUser();
                var employmentStatuses = GetEmploymentStatuses();
                return CreateEmployeeSearchViewModel(employees, sites, employmentStatuses);
            }
        }

        private IEnumerable<EmployeeDto> GetEmployees()
        {
            var employeeSearchRequest = CreateEmployeeSearchRequest();
            return _employeeService.Search(employeeSearchRequest).ToList();
        }

        private IEnumerable<LookupDto> GetEmploymentStatuses()
        {
            return _lookupService.GetEmploymentStatuses();
        }

        private IEnumerable<SiteDto> GetAllowedSitesForUser()
        {
            return _siteService.Search(new SearchSitesRequest()
                                       {
                                           CompanyId = _companyId,
                                           PageLimit = 100,
                                           AllowedSiteIds = _allowedSites
                                       });
        }

        private EmployeeSearchViewModel CreateEmployeeSearchViewModel(IEnumerable<EmployeeDto> employees, IEnumerable<SiteDto> sites, IEnumerable<LookupDto> employmentStatuses)
        {
            // SGG: _CurrentUser.Identity.Name is usually Email but can be UserID if email is not defined. CustomPrincipal UserId is ALWAYS UserId 
            ICustomPrincipal customPrincipal = (ICustomPrincipal) _currentUser;

            return new EmployeeSearchViewModel()
                       {
                           CompanyId = _companyId,
                           Employees = employees.Select(x => new EmployeesSearchEmployeeViewModel()
                                                                                     {
                                                                                         Id = x.Id.ToString(),
                                                                                         Forename = x.Forename,
                                                                                         Surname = x.Surname,
                                                                                         JobTitle = x.JobTitle,
                                                                                         Site = x.SiteName, // GetSiteName(x, sites),
                                                                                         Status = GetEmploymentStatus(x, employmentStatuses),
                                                                                         IsDeleted = x.Deleted,
                                                                                         ShowDeleteButton =     (x.User == null || x.User.Id == Guid.Empty || 
                                                                                                                 customPrincipal.UserId == Guid.Empty) 
                                                                                                                || 
                                                                                                                (x.User.Id != customPrincipal.UserId)
                                                                                     }).ToList(),
                           Sites = GetSites(sites),
                           SiteId = _siteId,                         
                       };
        }

        private string GetEmploymentStatus(EmployeeDto employeeDto, IEnumerable<LookupDto> employmentStatuses)
        {
            if (!employmentStatuses.Any() || employeeDto.EmploymentStatusId == 0)
                return string.Empty;

            var employmentStatus = employmentStatuses.FirstOrDefault(x => x.Id == employeeDto.EmploymentStatusId);
            if (employmentStatus == null)
            {
                throw new EmploymentStatusNotFoundForEmployeeException(employeeDto.Id, employeeDto.SiteId);
            }
            return employmentStatus.Name;
        }

        private string GetSiteName(EmployeeDto employeeDto, IEnumerable<SiteDto> sites)
        {
            if (!sites.Any() || employeeDto.SiteId == 0)
                return string.Empty;

            var site = sites.FirstOrDefault(x => x.Id == employeeDto.SiteId);
            if (site == null)
            {
                throw new SiteNotFoundForEmployeeException(employeeDto.Id, employeeDto.SiteId);
            }
            return site.Name;
        }

        private IEnumerable<AutoCompleteViewModel> GetSites(IEnumerable<SiteDto> sites)
        {
            return sites.OrderBy(x=> x.Name).Select(AutoCompleteViewModel.ForSite).AddDefaultOption();
        }

        private SearchEmployeesRequest CreateEmployeeSearchRequest()
        {
            var employeeSearchRequest = new SearchEmployeesRequest()
                                            {
                                                CompanyId = _companyId,
                                                EmployeeReferenceLike = string.IsNullOrEmpty(_employeeReference) ? null : _employeeReference + "%",
                                                ForenameLike = string.IsNullOrEmpty(_forename) ? null : _forename + "%",
                                                SurnameLike = string.IsNullOrEmpty(_surname) ? null : _surname + "%",
                                                SiteIds = _siteId != default(long) ? new long[] { _siteId } : _allowedSites.ToArray(),
                                                IncludeSiteless = _siteId != default(long) ? false : true,
                                                ShowDeleted = _showDeleted
                                            };
            return employeeSearchRequest;
        }

        public IEmployeeSearchViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IEmployeeSearchViewModelFactory WithAllowedSites(IList<long> allowedSites)
        {
            _allowedSites = allowedSites;
            return this;
        }

        public IEmployeeSearchViewModelFactory WithEmployeeReference(string employeeReference)
        {
            _employeeReference = employeeReference;
            return this;
        }

        public IEmployeeSearchViewModelFactory WithForeName(string foreName)
        {
            _forename = foreName;
            return this;
        }

        public IEmployeeSearchViewModelFactory WithSurname(string surname)
        {
            _surname = surname;
            return this;
        }

        public IEmployeeSearchViewModelFactory WithSiteId(long siteId)
        {
            _siteId = siteId;
            return this;
        }

        public IEmployeeSearchViewModelFactory WithShowDeleted(bool showDeleted)
        {
            _showDeleted = showDeleted;
            return this;
        }

        public IEmployeeSearchViewModelFactory WithCurrentUser(IPrincipal currentUser)
        {
            _currentUser = currentUser;       
            return this;
        }

    }
}