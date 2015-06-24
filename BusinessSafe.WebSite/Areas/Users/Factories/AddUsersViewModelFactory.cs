using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Employee;
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
    public class AddUsersViewModelFactory : IAddUsersViewModelFactory
    {
        private readonly IEmployeeService _employeeService;
        private readonly IRolesService _rolesService;
        private readonly ISiteService _siteService;
        private readonly ISiteGroupService _siteGroupService;
        private long _companyId;
        private Guid _employeeId;
        private bool _saveSuccessNotificationVisible;
        private bool _canChangeEmployee;
        private ICustomPrincipal _currentUser;

        public AddUsersViewModelFactory(IEmployeeService employeeService, IRolesService rolesService, ISiteService siteService, ISiteGroupService siteGroupService)
        {
            _employeeService = employeeService;
            _rolesService = rolesService;
            _siteService = siteService;
            _siteGroupService = siteGroupService;
        }

        public IAddUsersViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IAddUsersViewModelFactory WithEmployeeId(Guid userId)
        {
            _employeeId = userId;
            return this;
        }

        public IAddUsersViewModelFactory WithCurrentUser(ICustomPrincipal currentUser)
        {
            _currentUser = currentUser;
            return this;
        }

        public AddUsersViewModel GetViewModel(long companyId, Guid? employeeId, bool? saveSuccess, bool canChangeEmployee)
        {
            _companyId = companyId;
            _employeeId = employeeId.HasValue ? employeeId.Value : Guid.Empty;
            _saveSuccessNotificationVisible = saveSuccess.HasValue && saveSuccess.Value;
            _canChangeEmployee = canChangeEmployee;

            return GetViewModel();
        }

        public AddUsersViewModel GetViewModel()
        {
            var viewModel = new AddUsersViewModel();

            //var employeesNotRegisteredAsUsers = _employeeService.GetEmployeesNotRegisteredAsUsers(_companyId);
            //var employeesRegisteredAsUsers = _employeeService.GetEmployeesRegisteredAsUsers(_companyId);

            //var employees = employeesNotRegisteredAsUsers.Union(employeesRegisteredAsUsers);

            var employees = _employeeService.Search(new SearchEmployeesRequest
                                                    {
                                                        CompanyId = _companyId,
                                                        ShowDeleted = false,
                                                        ExcludeWithActiveUser = true
                                                    });

            var roles = _rolesService.GetAllRoles(_companyId);

            var sites = _siteService.GetByCompanyId(_companyId);
            var siteGroups = _siteGroupService.GetByCompanyId(_companyId);

            viewModel.MainSiteId = sites.Where(x => x.IsMainSite).Select(x => x.Id).Single();

            viewModel.CompanyId = _companyId;


            viewModel.Employees = employees
                                        .OrderBy(x => x.FullName)
                                        .Select(AutoCompleteViewModel.ForEmployee)
                                        .AddDefaultOption();

            viewModel.Roles = roles
                                    .Where(role => role.Name != "UserAdmin")
                                    .Select(AutoCompleteViewModel.ForRole)
                                    .AddDefaultOption();

            viewModel.SiteGroups = siteGroups
                                        .Select(AutoCompleteViewModel.ForSiteGroup)
                                        .AddDefaultOption();

            viewModel.Sites = sites
                                   .Select(AutoCompleteViewModel.ForSite)
                                   .AddDefaultOption();

            viewModel.SaveSuccessNotificationVisible = _saveSuccessNotificationVisible;
            viewModel.CanChangeEmployeeDdl = _canChangeEmployee;
            viewModel.CanChangeRoleDdl = true;

            if (_employeeId != default(Guid))
            {
                viewModel.SaveCancelButtonsVisible = true;
                viewModel.EmployeeId = _employeeId;

                //todo: change this call to call Peninsula Online - that way we can see if the employee is already registered
                //as a user for other systems. If it is, then we need that User Guid, rather than creating a new one as below
                //because we need it to have the same ID.
                var employee = _employeeService.GetEmployee(_employeeId, _companyId);

                viewModel.EmployeeName = employee.FullName;
                viewModel.EmployeeReference = employee.EmployeeReference;
                viewModel.JobTitle = employee.JobTitle;
                //Todo: store employee department?
                viewModel.Department = "NOT AVAILABLE";
                //Todo: store manager?
                viewModel.ManagerName = "NOT AVAILABLE";

                if (employee.User != null)
                {
                    viewModel.EmployeeAlreadyExistsAsUser = true;
                    viewModel.UserId = employee.User.Id;
                    viewModel.IsUserDeleted = employee.User.Deleted;
                    viewModel.IsUserRegistered = employee.User.IsRegistered;
                    viewModel.RoleId = employee.User.Role.Id;
                    viewModel.RoleDescription = employee.User.Role.Description;

                    if (employee.User.SiteStructureElement != null)
                    {
                        viewModel.PermissionsApplyToAllSites = employee.User.SiteStructureElement.IsMainSite;

                        if (viewModel.PermissionsApplyToAllSites == false)
                        {
                            if (employee.User.SiteStructureElement.GetType() == typeof(SiteGroupDto))
                                viewModel.SiteGroupId = employee.User.SiteStructureElement.Id;
                            else if (employee.User.SiteStructureElement.GetType() == typeof(SiteDto))
                                viewModel.SiteId = employee.User.SiteStructureElement.Id;
                        }
                    }

                    if (_currentUser != null
                        && employee.User.Role.Description == "User Admin"
                        && _currentUser.UserId == employee.User.Id)
                    {
                        viewModel.CanChangeRoleDdl = false;
                    }
                }
                else
                {
                    viewModel.EmployeeAlreadyExistsAsUser = false;
                    viewModel.UserId = Guid.NewGuid();
                }
            }
            else
            {
                viewModel.SaveCancelButtonsVisible = false;
            }

            return viewModel;
        }
    }
}