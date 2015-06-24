using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class EmployeeChecklistEmailDtoMapper
    {
        public EmployeeChecklistEmailDto Map(EmployeeChecklistEmail entity)
        {
            return new EmployeeChecklistEmailDto
                       {
                           Id = entity.Id,
                           EmailPusherId = entity.EmailPusherId,
                           Message = entity.Message,
                           EmployeeChecklists = entity.EmployeeChecklists != null ? new EmployeeChecklistDtoMapper().Map(entity.EmployeeChecklists) : null,
                           RecipientEmail = entity.RecipientEmail,
                           CreatedOn = entity.CreatedOn
                       };
        }

        public IEnumerable<EmployeeChecklistEmailDto> Map(IEnumerable<EmployeeChecklistEmail> entities)
        {
            return entities.Select(Map);
        }
    }
}