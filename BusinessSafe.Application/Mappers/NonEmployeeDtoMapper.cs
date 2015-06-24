using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class NonEmployeeDtoMapper
    {
        public NonEmployeeDto Map(NonEmployee entity)
        {
            return new NonEmployeeDto
            {
                Id = entity.Id,
                Company = entity.Company,
                Position = entity.Position,
                Name = entity.Name,
                FormattedName = entity.GetFormattedName()
            };
        }

        public IEnumerable<NonEmployeeDto> Map(IEnumerable<NonEmployee> entities)
        {
            return entities.Select(Map);
        }
    }
}
