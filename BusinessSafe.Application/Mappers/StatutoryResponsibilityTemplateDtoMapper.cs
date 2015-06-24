using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class StatutoryResponsibilityTemplateDtoMapper
    {
        public StatutoryResponsibilityTemplateDto MapWithCategoryAndReason(StatutoryResponsibilityTemplate entity)
        {
            var dto = new StatutoryResponsibilityTemplateDto()
                      {
                          Id = entity.Id,
                          Title = entity.Title,
                          Description = entity.Description,
                          GuidanceNote = entity.GuidanceNote,
                          TaskReoccurringType = entity.TaskReoccurringType,
                          ResponsibilityCategory = entity.ResponsibilityCategory !=null ? new ResponsibilityCategoryDtoMapper().Map(entity.ResponsibilityCategory) : null,
                          ResponsibilityReason = entity.ResponsibilityReason!=null ?  new ResponsibilityReasonDtoMapper().Map(entity.ResponsibilityReason) : null,
                          ResponsibilityTasks = new StatutoryResponsibilityTaskTemplateDtoMapper().Map(entity.ResponsibilityTasks),
                      };

            return dto;
        }


        public IEnumerable<StatutoryResponsibilityTemplateDto> MapWithCategoryAndReason(IEnumerable<StatutoryResponsibilityTemplate> entities)
        {
            return entities.Select(MapWithCategoryAndReason);
        }
    }
}