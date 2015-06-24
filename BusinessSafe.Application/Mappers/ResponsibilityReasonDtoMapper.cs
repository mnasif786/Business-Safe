using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class ResponsibilityReasonDtoMapper
    {
        public ResponsibilityReasonDto Map(ResponsibilityReason entity)
        {
            return new ResponsibilityReasonDto()
            {
                Id = entity.Id,
                Reason = entity.Reason
            };
        }

        public IEnumerable<ResponsibilityReasonDto> Map(IEnumerable<ResponsibilityReason> entities)
        {
            return entities.Select(Map);
        }
    }
}
