using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class SignificantFindingDtoMapper
    {
        public SignificantFindingDto Map(SignificantFinding significantFinding)
        {
            return new SignificantFindingDto()
            {
                Id = significantFinding.Id,
                FireAnswer = new FireAnswerDtoMapper().Map(significantFinding.FireAnswer)
            };
        }

        public IEnumerable<SignificantFindingDto> Map(IEnumerable<SignificantFinding> entities)
        {
            return entities.Select(Map);
        }
    }
}
