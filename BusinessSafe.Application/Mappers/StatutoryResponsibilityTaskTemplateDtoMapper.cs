using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class StatutoryResponsibilityTaskTemplateDtoMapper
    {

        public StatutoryResponsibilityTaskTemplateDto Map(StatutoryResponsibilityTaskTemplate entity)
        {
            StatutoryResponsibilityTaskTemplateDto dto = null;

            if (entity != null)
            {
                dto = new StatutoryResponsibilityTaskTemplateDto()
                          {
                              Id = entity.Id,
                              Title = entity.Title,
                              Description = entity.Description,
                              TaskReoccurringType = entity.TaskReoccurringType
                          };
            }
            return dto;
        }

        public IEnumerable<StatutoryResponsibilityTaskTemplateDto> Map(IEnumerable<StatutoryResponsibilityTaskTemplate> entities)
        {
            return entities.Select(Map);
        }
    }
}