using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Mappers
{
    public class SafetyPhraseDtoMapper
    {
        public SafetyPhraseDto Map(SafetyPhrase entity)
        {
            return new SafetyPhraseDto
                   {
                       Id = entity.Id,
                       Title = entity.Title,
                       ReferenceNumber = entity.ReferenceNumber,
                       HazardousSubstanceStandard = entity.HazardousSubstanceStandard,
                       RequiresAdditionalInformation = entity.RequiresAdditionalInformation.GetValueOrDefault()
                   };
        }
        
        public IEnumerable<SafetyPhraseDto> Map(IEnumerable<SafetyPhrase> entities)
        {
            return entities.Select(Map);
        }
    }
}
