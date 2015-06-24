using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class RiskAssessmentEmployeeDtoMapper
    {
        public RiskAssessmentEmployeeDto MapWithEmployee(RiskAssessmentEmployee entity)
        {
            return new RiskAssessmentEmployeeDto
                       {
                           Employee = new EmployeeDtoMapper().MapWithNationalityAndContactDetailsAndEmergencyContactDetails(entity.Employee)
                       };
        }

        public IEnumerable<RiskAssessmentEmployeeDto> MapWithEmployee(IEnumerable<RiskAssessmentEmployee> entites)
        {
            return entites.Select(MapWithEmployee);
        }
    }
}
