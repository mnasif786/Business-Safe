using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.AutoMappers
{
    public class UserRolePermissionsViewModelMapper
    {
        private IEnumerable<PermissionDto> _permissions;
        private RoleDto _role;
        private bool _customRoleEditingEnabled;

        public UserRolePermissionsViewModel Map(IEnumerable<PermissionDto> permissions, RoleDto role, bool enableCustomRoleEditing)
        {
            _permissions = permissions;
            _role = role;
            _customRoleEditingEnabled = enableCustomRoleEditing;

            var roleName = role.Description;
            var roleId = role.Id == Guid.Empty ? "" : role.Id.ToString();
            var permissionGroups = GeneratePermissionsGroupViewModelList();

            return new UserRolePermissionsViewModel()
                       {
                           RoleId = roleId,
                           RoleName = roleName,
                           IsSystemRole = role.IsSystemRole,
                           PermissionGroups = permissionGroups,
                           CustomRoleEditingEnabled = enableCustomRoleEditing
                       };
        }

        private List<PermissionsGroupViewModel> GeneratePermissionsGroupViewModelList()
        {
            var viewModels = CreatePermissionsGroupHeaders();

            AddPermissionTargetsToPermissionGroupHeaders(viewModels);

            AddPermissionsToPermissionTargets(viewModels);

            return viewModels;
        }

        private List<PermissionsGroupViewModel> CreatePermissionsGroupHeaders()
        {
            var result = new List<PermissionsGroupViewModel>();

            foreach (var permission in _permissions.OrderBy(p => p.PermissionGroupName))
            {
                if (PermissionGroupHeaderDoesNotExist(permission, result))
                {
                    result.Add(new PermissionsGroupViewModel()
                    {
                        PermissionGroupId = permission.PermissionGroupId,
                        PermissionGroupName = permission.PermissionGroupName,
                        IsSystemRole = _role.IsSystemRole
                    });
                }
            }

            return result;
        }

        private bool PermissionGroupHeaderDoesNotExist(PermissionDto permission, IEnumerable<PermissionsGroupViewModel> permissionGroups)
        {
            return permissionGroups.Count(x => x.PermissionGroupId == permission.PermissionGroupId) == 0;
        }

        private void AddPermissionTargetsToPermissionGroupHeaders(IEnumerable<PermissionsGroupViewModel> viewModels)
        {
            foreach (var permissionGroup in viewModels)
            {
                var localPermissionGroup = permissionGroup;
                foreach (var permission in _permissions
                                                .Where(p => p.PermissionGroupId == localPermissionGroup.PermissionGroupId)
                                                .OrderBy(p => p.PermissionTargetDto.Name))
                {
                    var permissionTargetName = permission.PermissionTargetDto.Name;

                    if (PermissionTargetDoesNotExist(permissionTargetName, permissionGroup))
                    {
                        permissionGroup.PermissionTargets.Add(new PermissionTarget()
                        {
                            PermissionTargetName = permissionTargetName,
                            IsSystemRole = _role.IsSystemRole,
                            CustomRoleEditingEnabled = _customRoleEditingEnabled
                        });
                    }
                }
            }
        }

        private static bool PermissionTargetDoesNotExist(string generalPermissionName, PermissionsGroupViewModel permissionGroup)
        {
            return permissionGroup.PermissionTargets.Count(x => x.PermissionTargetName == generalPermissionName) == 0;
        }

        private void AddPermissionsToPermissionTargets(List<PermissionsGroupViewModel> viewModels)
        {
            foreach (var permission in _permissions)
            {
                var permissionGroup = viewModels.First(x => x.PermissionGroupId == permission.PermissionGroupId);
                var permissionTarget = permissionGroup.PermissionTargets.First(x => x.PermissionTargetName == permission.PermissionTargetDto.Name);

                permissionTarget.Permissions.Add(new PermissionViewModel()
                {
                    PermissionId = permission.Id,
                    PermissionName = permission.Name,
                    PermissionActivity = permission.PermissionActivity,
                    Selected = _role.RolePermissions.Any(x => x.Permission.Id == permission.Id)
                });
            }
        }
    }
}