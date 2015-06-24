using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class AccidentRecordBodyPartDtoMapper
    {
        public AccidentRecordBodyPartDto MapWithInjury(AccidentRecordBodyPart entity)
        {
            return new AccidentRecordBodyPartDto
                       {
                           Id = entity.Id,
                           BodyPart = entity.BodyPart.Map(),
                           AdditionalInformation = entity.AdditionalInformation
                       };
        }

        public IEnumerable<AccidentRecordBodyPartDto> MapWithInjury(IEnumerable<AccidentRecordBodyPart> entities)
        {
            return entities.Select(MapWithInjury);
        }
    }
}
