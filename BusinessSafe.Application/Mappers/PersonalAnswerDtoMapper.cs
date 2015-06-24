using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class PersonalAnswerDtoMapper
    {
        public PersonalAnswerDto Map(PersonalAnswer entity)
        {
            return new AnswerDtoMapper().Map(entity) as PersonalAnswerDto;
        }

        public IEnumerable<PersonalAnswerDto> Map(IEnumerable<PersonalAnswer> entities)
        {
            return entities.Select(Map);
        }
    }
}
