using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class GeneralRiskAssessmentDto : MultiHazardRiskAssessmentDto
    {   
        public IList<PeopleAtRiskDto> PeopleAtRisk { get; set; }

        public static GeneralRiskAssessmentDto CreateFrom(MultiHazardRiskAssessment riskAssessment)
        {
            if (riskAssessment == null) return null;

            DateTime? createdOn = null;
            if (riskAssessment.CreatedOn != null)
                createdOn = riskAssessment.CreatedOn.Value;

            return new GeneralRiskAssessmentDto
                       {
                           Id = riskAssessment.Id,
                           CompanyId = riskAssessment.CompanyId,
                           AssessmentDate = riskAssessment.AssessmentDate,
                           CreatedOn = createdOn,
                           CreatedBy = new AuditedUserDtoMapper().Map(riskAssessment.CreatedBy),
                           Location = riskAssessment.Location,
                           Reference = riskAssessment.Reference,
                           RiskAssessmentSite = (riskAssessment.RiskAssessmentSite != null) ? new SiteStructureElementDto() {Id = riskAssessment.RiskAssessmentSite.Id, Name = riskAssessment.RiskAssessmentSite.Name} : null,
                           TaskProcessDescription = riskAssessment.TaskProcessDescription,
                           Title = riskAssessment.Title,
                           RiskAssessor = riskAssessment.RiskAssessor != null ? new RiskAssessorDtoMapper().Map(riskAssessment.RiskAssessor) : null,
                           Employees = riskAssessment.Employees != null ? new RiskAssessmentEmployeeDtoMapper().MapWithEmployee(riskAssessment.Employees) : null,
                           NonEmployees = riskAssessment.NonEmployees != null ? new RiskAssessmentNonEmployeeDtoMapper().MapWithNonEmployee(riskAssessment.NonEmployees) : null,
                           Deleted = riskAssessment.Deleted,
                           Status = riskAssessment.Status,
                           PeopleAtRisk = new List<PeopleAtRiskDto>(),
                           NextReviewDate = riskAssessment.Reviews.Any() ? riskAssessment.Reviews.OrderByDescending(x => x.Id).First().CompletionDueDate : null,
                           Reviews = riskAssessment.Reviews.Select(entity => new RiskAssessmentReviewDto()
                                                                                 {
                                                                                     Comments = entity.Comments,
                                                                                     CompletedBy = entity.CompletedBy != null ? new EmployeeDtoMapper().MapWithNationalityAndContactDetailsAndEmergencyContactDetails(entity.CompletedBy) : null,
                                                                                     CompletedDate = entity.CompletedDate,
                                                                                     CompletionDueDate = entity.CompletionDueDate,
                                                                                     Id = entity.Id,
                                                                                     IsReviewOutstanding = entity.IsReviewOutstanding,
                                                                                     ReviewAssignedTo = entity.ReviewAssignedTo != null ? new EmployeeDtoMapper().MapWithUser(entity.ReviewAssignedTo) : null,
                                                                                     RiskAssessmentReviewTask = entity.RiskAssessmentReviewTask != null ? new TaskDtoMapper().MapWithAssignedTo(entity.RiskAssessmentReviewTask) as RiskAssessmentReviewTaskDto : null
                                                                                 }).ToList()
                       };
        }

        public static GeneralRiskAssessmentDto CreateFromWithHazards(GeneralRiskAssessment riskAssessment)
        {
            var result = CreateFrom(riskAssessment);
            foreach (var riskAssessmentHazard in riskAssessment.Hazards.OrderBy(x=> x.OrderNumber))
            {
                result.Hazards.Add(HazardDtoMapper.Map(riskAssessmentHazard));
            }

            return result;
        }

        public static GeneralRiskAssessmentDto CreateFromWithHazardsAndPeopleAtRisk(GeneralRiskAssessment riskAssessment)
        {
            var result = CreateFromWithHazards(riskAssessment);
            foreach (var peopleAtRisk in riskAssessment.PeopleAtRisk)
            {
                //TODO: PTD - map this properly
                result.PeopleAtRisk.Add(PeopleAtRiskDtoMapper.Map(peopleAtRisk.PeopleAtRisk));
            }
            return result;
        }


    }
}