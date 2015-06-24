using System;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.AutoMappers;

namespace BusinessSafe.WebSite.Areas.Users.Factories
{
    public class UserRolePermissionsViewModelFactory: IUserRolePermissionsViewModelFactory
    {
        private readonly IRolesService _rolesService;
        private long _companyId;
        private Guid _roleId;
        private bool _enableCustomRoleEditing;

        public UserRolePermissionsViewModelFactory(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        public UserRolePermissionsViewModel GetViewModel()
        {
            var allPermissions = _rolesService.GetAllPermissions();

            var role = new RoleDto();
            if(HasRoleToLoad())
            {
                role = _rolesService.GetRole(_roleId, _companyId);    
            }

            return new UserRolePermissionsViewModelMapper().Map(allPermissions, role, _enableCustomRoleEditing);
        }

        public IUserRolePermissionsViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IUserRolePermissionsViewModelFactory WithRoleId(string roleId)
        {
            _roleId = string.IsNullOrEmpty(roleId) ? Guid.Empty: Guid.Parse(roleId);
            return this;
        }

        public IUserRolePermissionsViewModelFactory WithEnableCustomRoleEditing(bool enableCustomRoleEditing)
        {
            _enableCustomRoleEditing = enableCustomRoleEditing;
            return this;
        }

        private bool HasRoleToLoad()
        {
            return _roleId != Guid.Empty;
        }
    }
}