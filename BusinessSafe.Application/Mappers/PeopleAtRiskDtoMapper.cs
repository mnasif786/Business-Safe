using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public static class PeopleAtRiskDtoMapper
    {
        public static PeopleAtRiskDto Map(PeopleAtRisk peopleAtRisk)
        {
            return new PeopleAtRiskDto()
                       {
                           Id = peopleAtRisk.Id,
                           Name = peopleAtRisk.Name,
                           IsSystemDefault = peopleAtRisk.CompanyId == 0,
                       };
        }

        public static IEnumerable<PeopleAtRiskDto> Map(IList<PeopleAtRisk> peopleAtRisk)
        {
            return peopleAtRisk.Select(Map);
        }
    }
}