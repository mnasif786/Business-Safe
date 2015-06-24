using System;
using System.Collections.Generic;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;

namespace BusinessSafe.Application.Contracts.Users
{
    public interface IRolesService
    {
        IEnumerable<PermissionDto> GetAllPermissions();
        IEnumerable<RoleDto> GetAllRoles(long companyId);
        RoleDto GetRole(Guid roleId, long companyId);
        AddUserRoleResponse Add(AddUserRoleRequest request);
        void Update(UpdateUserRoleRequest request);
        void MarkUserRoleAsDeleted(MarkUserRoleAsDeletedRequest request);
        IEnumerable<UserDto> GetUsersWithRole(Guid parse, long companyId);
    }
}