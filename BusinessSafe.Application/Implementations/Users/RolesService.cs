using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Users
{
    public class RolesService : IRolesService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserForAuditingRepository _auditedUserRepository;
        private readonly IUserRepository _userForAuditingRepository;
        private readonly IPeninsulaLog _log;
        private readonly IPermissionRepository _permissionRepository;

        public RolesService(IRoleRepository roleRepository, IPermissionRepository permissionRepository, IUserForAuditingRepository auditedUserRepository, IPeninsulaLog log, IUserRepository userForAuditingRepository)
        {
            _roleRepository = roleRepository;
            _auditedUserRepository = auditedUserRepository;
            _log = log;
            _userForAuditingRepository = userForAuditingRepository;
            _permissionRepository = permissionRepository;
        }

        public IEnumerable<PermissionDto> GetAllPermissions()
        {
            _log.Add();

            try
            {
                var permissions = _permissionRepository.GetAll();
                
                return new PermissionDtoMapper().Map(permissions);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public IEnumerable<RoleDto> GetAllRoles(long companyId)
        {
            var roles = _roleRepository.GetAllByCompanyId(companyId);
            return RoleDto.CreateFrom(roles);
        }

        public RoleDto GetRole(Guid roleId, long companyId)
        {
            _log.Add(new object[] { roleId, companyId });

            try
            {
                var role = _roleRepository.GetByIdAndCompanyId(roleId, companyId);
                return RoleDto.CreateFrom(role);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public AddUserRoleResponse Add(AddUserRoleRequest request)
        {
            _log.Add(request);

            try
            {
                var creatingUser = _auditedUserRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var allPermissions = _permissionRepository.GetAll();
                var userRolePermissions = GetPermissionsForRole(request.Permissions, allPermissions);

                var role = Role.Create(request.RoleName, request.CompanyId, userRolePermissions, creatingUser);

                _roleRepository.SaveOrUpdate(role);


                return new AddUserRoleResponse
                {
                    Success = true,
                    RoleId = role.Id
                };

            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void Update(UpdateUserRoleRequest request)
        {
            _log.Add(request);

            try
            {

                var updatingUser = _auditedUserRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var allPermissions = _permissionRepository.GetAll();
                var userRolePermissions = GetPermissionsForRole(request.Permissions, allPermissions);
                var role = _roleRepository.GetByIdAndCompanyId(request.RoleId, request.CompanyId);

                role.Amend(request.RoleName, userRolePermissions, updatingUser);

                _roleRepository.SaveOrUpdate(role);


            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void MarkUserRoleAsDeleted(MarkUserRoleAsDeletedRequest request)
        {
            _log.Add(request);

            try
            {

                var role = _roleRepository.GetByIdAndCompanyId(request.UserRoleId, request.CompanyId);
                var user = _auditedUserRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                role.MarkForDelete(user);
                _roleRepository.SaveOrUpdate(role);

            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public IEnumerable<UserDto> GetUsersWithRole(Guid roleId, long companyId)
        {
            _log.Add(new object[] { roleId, companyId });

            try
            {
                var users = _userForAuditingRepository.GetAllByCompanyIdAndRole(companyId, roleId);
                return users.Select(new UserDtoMapper().MapIncludingEmployeeAndSite);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        private static List<Permission> GetPermissionsForRole(IEnumerable<int> permissions, IEnumerable<Permission> allPermissions)
        {
            var userRolePermissions = allPermissions.Where(x => permissions.Contains(x.Id)).ToList();
            return userRolePermissions;
        }
    }
}