
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class ResponsibilityTaskDtoMapper : TaskDtoMapper
    {
        public ResponsibilityTaskDto MapWithAssignedTo(ResponsibilityTask entity)
        {
            var dto = new ResponsibilityTaskDto();
            dto = PopulateTaskDto(entity, dto) as ResponsibilityTaskDto;

            dto.StatutoryResponsibilityTaskTemplateCreatedFrom =
                new StatutoryResponsibilityTaskTemplateDtoMapper().Map(
                    entity.StatutoryResponsibilityTaskTemplateCreatedFrom);
            
            return dto;
        }

        public IEnumerable<ResponsibilityTaskDto> MapWithAssignedToAndStatutoryTemplates(IEnumerable<ResponsibilityTask> entities)
        {
            return entities.Select(MapWithAssignedTo).AsEnumerable();
        }
    }
}