using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class ChecklistDtoMapper
    {
        public ChecklistDto Map(Checklist entity)
        {
            if (entity == null)
            {
                return null;
            }
            var dto = new ChecklistDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                ChecklistRiskAssessmentType = entity.ChecklistRiskAssessmentType
            };

            return dto;
        }

        public ChecklistDto MapWithSections(Checklist entity)
        {
            if (entity == null)
            {
                return null;
            }
            var dto = Map(entity);
            dto.Sections = new SectionDtoMapper().Map(entity.Sections);
            return dto;
        }

        public IEnumerable<ChecklistDto> Map(IEnumerable<Checklist> entities)
        {
            return entities.Select(Map);
        }

        public IEnumerable<ChecklistDto> MapWithSections(IEnumerable<Checklist> entities)
        {
            return entities.Select(MapWithSections);
        }
    }
}
