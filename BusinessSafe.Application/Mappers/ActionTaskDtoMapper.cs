
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class ActionTaskDtoMapper : TaskDtoMapper
    {
        public ActionTaskDto MapWithAssignedTo(ActionTask entity)
        {
            var dto = new ActionTaskDto();

            dto = PopulateTaskDto(entity, dto) as ActionTaskDto;

            dto.Action = entity.Action != null ? new ActionDtoMapper().Map(entity.Action) : null;
            
            return dto;
        }
    }
}

