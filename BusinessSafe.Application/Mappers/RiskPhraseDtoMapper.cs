using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class RiskPhraseDtoMapper
    {
        public RiskPhraseDto Map(RiskPhrase entity)
        {
            return new RiskPhraseDto
                   {
                       Id = entity.Id,
                       Title = entity.Title,
                       ReferenceNumber = entity.ReferenceNumber,
                       HazardousSubstanceStandard = entity.HazardousSubstanceStandard
                   };
        }

        public IEnumerable<RiskPhraseDto> Map(IEnumerable<RiskPhrase> entities)
        {
            return entities.Select(Map);
        }
    }
}
