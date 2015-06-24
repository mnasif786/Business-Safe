using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Users.Factories
{
    public class UserRolesViewModelFactory: IUserRolesViewModelFactory
    {
        private readonly IRolesService _rolesService;
        private long _companyId;
        private string _roleId;

        public UserRolesViewModelFactory(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        public UserRolesViewModel GetViewModel()
        {
            var permissions = _rolesService.GetAllRoles(_companyId);
            return CreateUserRolesViewModel(permissions);
        }

        private UserRolesViewModel CreateUserRolesViewModel(IEnumerable<RoleDto> rolesDtos)
        {
            return new UserRolesViewModel()
                       {
                           CompanyId = _companyId,
                           CompanyRoles = rolesDtos.OrderBy(x => x.Name).Select(AutoCompleteViewModel.ForRole).AddDefaultOption(),
                           RoleId = _roleId
                       };
        }

        public UserRolesViewModel GetViewModelForNewUseRole()
        {
            return new UserRolesViewModel()
                       {
                           CompanyId = _companyId,
                           IsNewUserRole = true
                       };
        }

        public IUserRolesViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IUserRolesViewModelFactory WithRoleId(string roleId)
        {
            _roleId = roleId;
            return this;
        }
    }
}