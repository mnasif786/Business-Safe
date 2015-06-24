using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class PermissionDtoMapper
    {
        public IEnumerable<PermissionDto> Map(IEnumerable<Permission> permissions)
        {
            return permissions.Select(Map).ToList();
        }

        public PermissionDto Map(Permission permission)
        {
            return new PermissionDto
            {
                Id = permission.Id,
                Name = permission.Name,
                PermissionTargetDto = PermissionTargetDtoMapper.Map(permission.PermissionTarget),
                PermissionActivity = permission.PermissionActivity,
                PermissionGroupId = (int)permission.PermissionTarget.PermissionGroup,
                PermissionGroupName = EnumHelper.GetEnumDescription(permission.PermissionTarget.PermissionGroup)
            };
        }
    }

    

}