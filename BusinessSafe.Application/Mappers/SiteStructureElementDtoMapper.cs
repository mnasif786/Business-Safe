using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using System;

namespace BusinessSafe.Application.Mappers
{
    public class SiteStructureElementDtoMapper
    {
        public SiteStructureElementDto Map(SiteStructureElement entity)
        {
            if (entity.Self as Site == null && entity.Self as SiteGroup == null)
            {
                throw new Exception(
                    "Entity could not be resolved to sub type. Is it a proxy? if so, it is likely another entity has lazy loaded it. All many to ones should be lazy=none or lazy=no-proxy");
            }

            SiteStructureElementDto dto = null;

            if (entity.Self as Site != null)
            {
                var site = entity.Self as Site;
                dto = new SiteDto();
                (dto as SiteDto).SiteId = site.SiteId;
            }

            if (entity.Self as SiteGroup != null)
            {
                dto = new SiteGroupDto();
            }

            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.ClientId = entity.ClientId;
            dto.Deleted = entity.Deleted;
            dto.SiteClosed = entity.SiteClosedDate != null;
            dto.IsMainSite = entity.IsMainSite;
            return dto;
        }

        public SiteStructureElementDto MapWithChildren(SiteStructureElement entity)
        {
            var dto = Map(entity);
            dto.Children = entity.Children != null ? MapWithChildren(entity.Children).ToList() : null;
            return dto;
        }

        public IEnumerable<SiteStructureElementDto> Map(IEnumerable<SiteStructureElement> entities)
        {
            return entities.Select(Map);
        }

        public IEnumerable<SiteStructureElementDto> MapWithChildren(IEnumerable<SiteStructureElement> entities)
        {
            return entities.Select(MapWithChildren);
        }
    }
}
