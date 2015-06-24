using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class AccidentRecordNextStepSectionDtoMapper
    {
        public AccidentRecordNextStepSectionDto Map(AccidentRecordNextStepSection entity)
        {
            return new AccidentRecordNextStepSectionDto
                       {
                           Id = entity.Id,
                           NextStepsSection = entity.NextStepsSection
                       };
        }

        public IEnumerable<AccidentRecordNextStepSectionDto> Map(IEnumerable<AccidentRecordNextStepSection> entities)
        {
            return entities.Select(Map);
        }
    }
}
