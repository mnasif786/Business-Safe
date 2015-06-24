using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class HazardousSubstanceSafetyPhraseDtoMapper
    {
        public HazardousSubstanceSafetyPhraseDto Map(HazardousSubstanceSafetyPhrase entity)
        {
            return new HazardousSubstanceSafetyPhraseDto
                       {
                           Id = entity.Id,
                           SafetyPhase =  new SafetyPhraseDtoMapper().Map(entity.SafetyPhrase),
                           AdditionalInformation = entity.AdditionalInformation
                       };
        }

        public IEnumerable<HazardousSubstanceSafetyPhraseDto> Map(IEnumerable<HazardousSubstanceSafetyPhrase> entities)
        {
            return entities.Select(Map);
        }
    }
}