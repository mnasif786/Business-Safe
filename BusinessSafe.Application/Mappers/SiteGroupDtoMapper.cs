using System.Linq;
using AutoMapper;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Infrastructure.Attributes;
using System.Collections.Generic;

namespace BusinessSafe.Application.Mappers
{
    [CoverageExclude]
    public class SiteGroupDtoMapper
    {
        public SiteGroupDto Map(SiteGroup entity)
        {
            return new SiteGroupDto
                       {
                           Id = entity.Id,
                           ClientId = entity.ClientId,
                           Name = entity.Name
                       };
        }

        public SiteGroupDto MapWithHasChildrenAndParent(SiteGroup entity, IEnumerable<long> childIdsThatCannotBecomeParent)
        {
            return new SiteGroupDto
                        {
                            Id = entity.Id,
                            ClientId = entity.ClientId,
                            Name = entity.Name,
                            HasChildren = entity.HasChildren,
                            Parent = entity.Parent != null ? new SiteStructureElementDtoMapper().Map(entity.Parent) : null,
                            ChildIdsThatCannotBecomeParent = childIdsThatCannotBecomeParent
                        };
        }

        public SiteGroupDto MapWithParent(SiteGroup entity)
        {
            return new SiteGroupDto
            {
                Id = entity.Id,
                ClientId = entity.ClientId,
                Name = entity.Name,
                Parent = new SiteStructureElementDtoMapper().Map(entity.Parent)
            };
        }

        public IEnumerable<SiteGroupDto> Map(IEnumerable<SiteGroup> entities)
        {
            return entities.Select(Map);
        }
    }
}