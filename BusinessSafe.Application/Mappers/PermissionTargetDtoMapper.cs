using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class PermissionTargetDtoMapper
    {
        public static IEnumerable<PermissionTargetDto> Map(IEnumerable<PermissionTarget> permissionsTargets)
        {
            return permissionsTargets.Select(Map).ToList();
        }

        public static PermissionTargetDto Map(PermissionTarget permissionTarget)
        {
            return new PermissionTargetDto
                       {
                           DisplayOrder = permissionTarget.DisplayOrder,
                           Name = permissionTarget.Name
                       };
        }
    }
}