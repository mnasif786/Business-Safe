using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Infrastructure.Attributes;
using System.Runtime.Serialization;
using BusinessSafe.Application.Mappers;

namespace BusinessSafe.Application.DataTransferObjects
{
    [CoverageExclude]
    [DataContract]
    public class RoleDto
    {
        [DataMember]
        public bool IsSystemRole { get; set; }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Name { get; set; }

        public IEnumerable<RolePermissionDto> RolePermissions { get; set; }

        public RoleDto()
        {
            Id = Guid.Empty;
            RolePermissions = new RolePermissionDto[]{};
        }

        public static IEnumerable<RoleDto> CreateFrom(IEnumerable<Role> roles)
        {
            return roles.Select(CreateFrom);
        }

        public static RoleDto CreateFrom(Role role)
        {
            var result = new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                IsSystemRole = role.CompanyId == 0,
                RolePermissions = new RolePermissionDtoMapper().MapWithPermission(role.Permissions)
            };
            return result;
        }

    }
}