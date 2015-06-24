using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class AccidentRecordInjuryDtoMapper
    {
        public AccidentRecordInjuryDto MapWithInjury(AccidentRecordInjury entity)
        {
            return new AccidentRecordInjuryDto
                       {
                           Id = entity.Id,
                           Injury = entity.Injury.Map(),
                           AdditionalInformation = entity.AdditionalInformation
                       };
        }

        public IEnumerable<AccidentRecordInjuryDto> MapWithInjury(IEnumerable<AccidentRecordInjury> entities)
        {
            return entities.Select(MapWithInjury);
        }
    }
}
