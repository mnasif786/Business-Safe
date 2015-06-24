using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class RolePermissionDtoMapper
    {
        public RolePermissionDto MapWithPermission(RolePermission entity)
        {
            return new RolePermissionDto
                       {
                           Permission = new PermissionDtoMapper().Map(entity.Permission)
                       };
        }

        public IEnumerable<RolePermissionDto> MapWithPermission(IEnumerable<RolePermission> entities)
        {
            return entities.Select(MapWithPermission);
        }
    }
}
