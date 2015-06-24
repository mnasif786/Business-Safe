using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class PersonalRiskAssessmentDtoMapper
    {
        public PersonalRiskAssessmentDto MapWithChecklistGeneratorEmployeesAndChecklists(PersonalRiskAssessment entity)
        {
            var dto = new RiskAssessmentDtoMapper().MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor(entity) as PersonalRiskAssessmentDto;

            //TODO: PTD - map this properly
            dto.ChecklistGeneratorEmployees = new EmployeeDtoMapper().MapWithNationalityAndContactDetailsAndEmergencyContactDetails(entity.ChecklistGeneratorEmployees.Select(x => x.Employee));
            
            //TODO: PTD - map this properly
            dto.Checklists = new ChecklistDtoMapper().Map(entity.Checklists.Select(x => x.Checklist));
            return dto;
        }

        public PersonalRiskAssessmentDto MapWithEmployeeChecklists(PersonalRiskAssessment entity)
        {
            var dto = new RiskAssessmentDtoMapper().MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor(entity) as PersonalRiskAssessmentDto;
            dto.EmployeeChecklists = entity.EmployeeChecklists != null ? new EmployeeChecklistDtoMapper().Map(entity.EmployeeChecklists) : null;
            return dto;
        }
    }
}
