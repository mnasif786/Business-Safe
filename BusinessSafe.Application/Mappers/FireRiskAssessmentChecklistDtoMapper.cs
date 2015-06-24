using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class FireRiskAssessmentChecklistDtoMapper
    {
        public FireRiskAssessmentChecklistDto MapWithChecklist(FireRiskAssessmentChecklist entity)
        {
            if (entity == null) return null;
            var dto = new FireRiskAssessmentChecklistDto
                          {
                              Id = entity.Id,
                              Checklist = new ChecklistDtoMapper().MapWithSections(entity.Checklist),
                              CompletedDate = entity.CompletedDate,
                              Answers = new FireAnswerDtoMapper().Map(entity.Answers),
                              HasCompleteFailureAttempt = entity.HasCompleteFailureAttempt
                          };

            return dto;
        }

        public IEnumerable<FireRiskAssessmentChecklistDto> MapWithChecklist(IEnumerable<FireRiskAssessmentChecklist> entities)
        {
            return entities.Select(MapWithChecklist);
        }
    }
}
