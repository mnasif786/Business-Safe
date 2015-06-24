using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class EmployeeForAuditingDtoMapper
    {
        public EmployeeForAuditingDto Map(EmployeeForAuditing employeeForAuditing)
        {
            if (employeeForAuditing == null)
                return null;

            var dto = new EmployeeForAuditingDto
                      {
                          Id = employeeForAuditing.Id,
                          Forename = employeeForAuditing.Forename,
                          Surname = employeeForAuditing.Surname,
                          FullName = employeeForAuditing.FullName
                      };

            return dto;
        }
    }
}