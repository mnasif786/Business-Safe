using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class PersonalRiskAssessmentDto : MultiHazardRiskAssessmentDto
    {
        public bool Sensitive { get; set; }
        public IEnumerable<EmployeeDto> ChecklistGeneratorEmployees { get; set; }
        public IEnumerable<ChecklistDto> Checklists { get; set; }
        /// <summary>
        /// We have assumed that emails have been sent to these employees. 
        /// However, to get a true sent value we need the email service to raise an event that this email has been sent, which a listener will then use to update the sent status.
        /// </summary>
        public IEnumerable<EmployeeChecklistDto> EmployeeChecklists { get; set; } 
        public string ChecklistGeneratorMessage { get; set; }
        public PersonalRiskAssessementEmployeeChecklistStatusEnum PersonalRiskAssessementEmployeeChecklistStatus { get; set; }
        public bool? SendCompletedChecklistNotificationEmail { get; set; }
        public DateTime? CompletionDueDateForChecklists { get; set; }
        public string CompletionNotificationEmailAddress { get; set; }


        public static PersonalRiskAssessmentDto CreateFrom(MultiHazardRiskAssessment riskAssessment)
        {
            if (riskAssessment == null) return null;

            DateTime? createdOn = null;
            if (riskAssessment.CreatedOn != null)
            {
                createdOn = riskAssessment.CreatedOn.Value;
            }

            return new PersonalRiskAssessmentDto
            {
                Id = riskAssessment.Id,
                CompanyId = riskAssessment.CompanyId,
                AssessmentDate = riskAssessment.AssessmentDate,
                CreatedOn = createdOn,
                CreatedBy = new AuditedUserDtoMapper().Map(riskAssessment.CreatedBy),
                RiskAssessor = riskAssessment.RiskAssessor != null ? new RiskAssessorDtoMapper().MapWithEmployeeAndSite(riskAssessment.RiskAssessor) : null,
                Location = riskAssessment.Location,
                Reference = riskAssessment.Reference,
                RiskAssessmentSite = (riskAssessment.RiskAssessmentSite != null) ? new SiteStructureElementDto() { Id = riskAssessment.RiskAssessmentSite.Id, Name = riskAssessment.RiskAssessmentSite.Name } : null,
                TaskProcessDescription = riskAssessment.TaskProcessDescription,
                Title = riskAssessment.Title,
                Employees = riskAssessment.Employees != null ? new RiskAssessmentEmployeeDtoMapper().MapWithEmployee(riskAssessment.Employees) : null,
                NonEmployees = riskAssessment.NonEmployees != null ? new RiskAssessmentNonEmployeeDtoMapper().MapWithNonEmployee(riskAssessment.NonEmployees) : null,
                Deleted = riskAssessment.Deleted,
                Status = riskAssessment.Status,
                NextReviewDate = riskAssessment.Reviews.Any() ? riskAssessment.Reviews.OrderByDescending(x => x.Id).First().CompletionDueDate : null
            };
        }

        public static PersonalRiskAssessmentDto CreateFromWithHazards(PersonalRiskAssessment riskAssessment)
        {
            var result = CreateFrom(riskAssessment);
            result.EmployeeChecklists = riskAssessment.EmployeeChecklists != null ? new EmployeeChecklistDtoMapper().Map(riskAssessment.EmployeeChecklists) : null;
            result.PersonalRiskAssessementEmployeeChecklistStatus = riskAssessment.PersonalRiskAssessementEmployeeChecklistStatus;

            foreach (var riskAssessmentHazard in riskAssessment.Hazards.OrderBy(x=> x.OrderNumber))
            {
                result.Hazards.Add(HazardDtoMapper.Map(riskAssessmentHazard));
            }

            return result;
        }
    }
}