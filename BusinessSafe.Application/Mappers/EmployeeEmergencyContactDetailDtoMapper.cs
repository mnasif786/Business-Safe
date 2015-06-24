using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class EmployeeEmergencyContactDetailDtoMapper
    {
        public EmployeeEmergencyContactDetailDto Map(EmployeeEmergencyContactDetail entity)
        {
            return new EmployeeEmergencyContactDetailDto
                       {
                           EmergencyContactId = entity.Id,
                           Title = entity.Title,
                           Forename = entity.Forename,
                           Surname = entity.Surname,
                           Relationship = entity.Relationship,
                           SameAddressAsEmployee = entity.SameAddressAsEmployee,
                           Address1 = entity.Address1,
                           Address2 = entity.Address2,
                           Address3 = entity.Address3,
                           Town = entity.Town,
                           County = entity.County,
                           Country = entity.Country != null ? new CountryDtoMapper().Map(entity.Country) : null,
                           PostCode = entity.PostCode,
                           WorkTelephone = entity.Telephone1,
                           HomeTelephone = entity.Telephone2,
                           MobileTelephone = entity.Telephone3,
                           PreferredContactNumber = entity.PreferedTelephone
                       };
        }

        public IEnumerable<EmployeeEmergencyContactDetailDto> Map(IEnumerable<EmployeeEmergencyContactDetail> entities)
        {
            return entities.Select(Map);
        }
    }
}
