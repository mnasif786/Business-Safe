using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class FireAnswerDtoMapper
    {
        public FireAnswerDto Map(FireAnswer entity)
        {
            return new AnswerDtoMapper().Map(entity) as FireAnswerDto;
        }

        public IEnumerable<FireAnswerDto> Map(IEnumerable<FireAnswer> entities)
        {
            return entities.Select(Map);
        }
    }
}
