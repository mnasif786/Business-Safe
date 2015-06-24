using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class RiskAssessmentNonEmployeeDtoMapper
    {
        public RiskAssessmentNonEmployeeDto MapWithNonEmployee(RiskAssessmentNonEmployee entity)
        {
            return new RiskAssessmentNonEmployeeDto
                       {
                           NonEmployee = new NonEmployeeDtoMapper().Map(entity.NonEmployee)
                       };
        }

        public IEnumerable<RiskAssessmentNonEmployeeDto> MapWithNonEmployee(IEnumerable<RiskAssessmentNonEmployee> entites)
        {
            return entites.Select(MapWithNonEmployee);
        }
    }
}
