using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;
using System.Security.Principal;

namespace BusinessSafe.WebSite.Areas.Users.Factories
{
    public class UserSearchViewModelFactory : IUserSearchViewModelFactory
    {
        private long _companyId;
        private string _forename = string.Empty;
        private string _surname = string.Empty;
        private long _siteGroupId;
        private long _siteId;
        private bool _showDeleted;
        private IList<long> _allowedSiteIds;
        private readonly IUserService _userService;
        private readonly ISiteService _siteService;
        private IPrincipal _currentUser;

        public UserSearchViewModelFactory(IUserService userService, ISiteService siteService)
        {
            _userService = userService;
            _siteService = siteService;
        }

        public IUserSearchViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IUserSearchViewModelFactory WithForeName(string foreName)
        {
            _forename = foreName;
            return this;
        }

        public IUserSearchViewModelFactory WithSurname(string surname)
        {
            _surname = surname;
            return this;
        }

        public IUserSearchViewModelFactory WithSiteGroupId(long siteGroupId)
        {
            _siteGroupId = siteGroupId;
            return this;
        }

        public IUserSearchViewModelFactory WithSiteId(long siteId)
        {
            _siteId = siteId;
            return this;
        }

        public IUserSearchViewModelFactory WithShowDeleted(bool showDeleted)
        {
            _showDeleted = showDeleted;

            return this;
        }

        public IUserSearchViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds)
        {
            _allowedSiteIds = allowedSiteIds;
            return this;
        }

        public IUserSearchViewModelFactory WithCurrentUser(IPrincipal currentUser)
        {
            _currentUser = currentUser;
            return this;
        }

        public UserSearchViewModel GetViewModel()
        {
            var users = GetUsers();
            var sites = GetAllSitesForCompany();

            return CreateUserSearchViewModel(users, sites);
        }

        private IEnumerable<UserDto> GetUsers()
        {
            var userSearchRequest = CreateUserSearchRequest();

            return _userService.Search(userSearchRequest).ToList();
        }

        private SearchUsersRequest CreateUserSearchRequest()
        {
            var userSearchRequest = new SearchUsersRequest()
            {
                CompanyId = _companyId,
                ForenameLike = string.IsNullOrEmpty(_forename) ? null : _forename + "%",
                SurnameLike = string.IsNullOrEmpty(_surname) ? null : _surname + "%",
                SiteId = _siteId,
                ShowDeleted = _showDeleted,
                AllowedSiteIds = _allowedSiteIds
            };

            return userSearchRequest;
        }

        private IEnumerable<SiteDto> GetAllSitesForCompany()
        {
            return _siteService.GetAll(_companyId);
        }

        private IEnumerable<AutoCompleteViewModel> GetSites(IEnumerable<SiteDto> sites)
        {
            return sites.OrderBy(x => x.Name).Select(AutoCompleteViewModel.ForSite).AddDefaultOption();
        }

        private UserSearchViewModel CreateUserSearchViewModel(IEnumerable<UserDto> users, IEnumerable<SiteDto> sites)
        {
            // SGG: _CurrentUser.Identity.Name is usually Email but can be UserID if email is not defined. CustomPrincipal UserId is ALWAYS UserId 
            ICustomPrincipal customPrincipal = (ICustomPrincipal)_currentUser;

            return new UserSearchViewModel()
            {
                CompanyId = _companyId,

                Users = users.Select(u => new UsersSearchUserViewModel()
                {
                    Id = u.Id.ToString(),
                    EmployeeId = u.Employee.Id.ToString(),
                    EmployeeReference = u.Employee.EmployeeReference,
                    Forename = u.Employee.Forename,
                    Surname = u.Employee.Surname,
                    JobTitle = u.Employee.JobTitle,
                    SiteName = u.SiteStructureElement != null && u.SiteStructureElement.IsMainSite ? "ALL" : u.SiteStructureElement.Name,
                    Role = u.Role.Description,
                    IsDeleted = u.Deleted,
                    IsRegistered = u.IsRegistered ? 1 : 0,
                    ShowDeleteButton = (u.Id == Guid.Empty || customPrincipal.UserId == Guid.Empty ) || (u.Id != customPrincipal.UserId),
                    ShowResetUserRegistrationButton = !u.IsRegistered
                }).ToList(),

                GroupSiteId = _siteGroupId,
                Sites = GetSites(sites),
            };
        }    
    }
}