using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class SiteDtoMapper
    {
        public SiteDto Map(Site entity)
        {
            return new SiteDto
                       {
                           Id = entity.Id,
                           Name = entity.Name,
                           ClientId = entity.ClientId,
                           SiteId = entity.SiteId,
                           IsMainSite = entity.IsMainSite,
                           SiteClosed = entity.SiteClosedDate != null,
                       };
        }

        public SiteDto MapWithParent(Site entity, IEnumerable<long> childIdsThatCannotBecomeParent)
        {
            return new SiteDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ClientId = entity.ClientId,
                SiteId = entity.SiteId,
                SiteContact = entity.SiteContact,
                SiteClosed = entity.SiteClosedDate != null,
                Parent = entity.Parent != null ? new SiteStructureElementDtoMapper().Map(entity.Parent) : null,
                ChildIdsThatCannotBecomeParent = childIdsThatCannotBecomeParent
            };
        }

        public SiteDto MapWithChildren(Site entity)
        {
            return new SiteDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ClientId = entity.ClientId,
                SiteId = entity.SiteId,
                SiteClosed = entity.SiteClosedDate != null,
                Children = entity.Children != null ? new SiteStructureElementDtoMapper().MapWithChildren(entity.Children).ToList() : null
            };
        }

        public IEnumerable<SiteDto> Map(IEnumerable<Site> entities)
        {
            return entities.Select(Map).ToList();
        }
    }
}
