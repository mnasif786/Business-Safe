using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class EmployeeChecklistDtoMapper
    {
        public EmployeeChecklistDto Map(EmployeeChecklist entity)
        {
            var dto = new EmployeeChecklistDto
                      {
                          Id = entity.Id,
                          Employee = new EmployeeDtoMapper().MapWithNationalityAndContactDetailsAndEmergencyContactDetails(entity.Employee),
                          Checklist = new ChecklistDtoMapper().MapWithSections(entity.Checklist),
                          Answers = new PersonalAnswerDtoMapper().Map(entity.Answers),
                          StartDate = entity.StartDate,
                          CompletedDate = entity.CompletedDate,
                          Password = entity.Password,
                          FriendlyReference = entity.FriendlyReference,
                          CompletionNotificationEmailAddress = entity.CompletionNotificationEmailAddress,
                          DueDateForCompletion = entity.DueDateForCompletion,
                          SendCompletedChecklistNotificationEmail = entity.SendCompletedChecklistNotificationEmail ?? false,
                          LastRecipientEmail = entity.LastRecipientEmail,
                          LastMessage = entity.LastMessage,
                          IsFurtherActionRequired = entity.IsFurtherActionRequired,
                          AssessedByEmployee = new EmployeeForAuditingDtoMapper().Map(entity.AssessedByEmployee),
                          AssessmentDate = entity.AssessmentDate
                      };
            return dto;
        }

        public IEnumerable<EmployeeChecklistDto> Map(IEnumerable<EmployeeChecklist> entities)
        {
            return entities.Select(Map);
        }

        public EmployeeChecklistDto MapWithCompletedOnEmployeesBehalfBy(EmployeeChecklist entity)
        {
            var dto = Map(entity);

            dto.CompletedOnEmployeesBehalfBy = entity.CompletedOnEmployeesBehalfBy != null
                                                   ? new UserDtoMapper().MapIncludingEmployeeAndSite(entity.CompletedOnEmployeesBehalfBy)
                                                   : null;

            return dto;
        } 
    }
}
