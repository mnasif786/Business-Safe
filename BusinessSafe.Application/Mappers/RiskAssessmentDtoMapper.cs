using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;


namespace BusinessSafe.Application.Mappers
{
    public class RiskAssessmentDtoMapper
    {
        public RiskAssessmentDto MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessorAndReviews(RiskAssessment entity)
        {
            var dto = MapWithSiteAndRiskAssessor(entity);
            dto.Employees = entity.Employees != null ? new RiskAssessmentEmployeeDtoMapper().MapWithEmployee(entity.Employees) : null;
            dto.NonEmployees = entity.NonEmployees != null ? new RiskAssessmentNonEmployeeDtoMapper().MapWithNonEmployee(entity.NonEmployees) : null;
            dto.Reviews = new RiskAssessmentReviewDtoMapper().Map(entity.Reviews).ToList();
            return dto;
        }

        public RiskAssessmentDto MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor(RiskAssessment entity)
        {
            var dto = MapWithSiteAndRiskAssessor(entity);
            dto.Employees = entity.Employees != null ? new RiskAssessmentEmployeeDtoMapper().MapWithEmployee(entity.Employees) : null;
            dto.NonEmployees = entity.NonEmployees != null ? new RiskAssessmentNonEmployeeDtoMapper().MapWithNonEmployee(entity.NonEmployees) : null;
            return dto;
        }

        public RiskAssessmentDto MapWithSiteAndRiskAssessor(RiskAssessment entity)
        {
            RiskAssessmentDto dto = null;

            if (entity.Self as MultiHazardRiskAssessment != null) 
            {
                if(entity.Self as PersonalRiskAssessment != null)
                {
                    dto = new PersonalRiskAssessmentDto();
                    var personalRiskAssessment = entity.Self as PersonalRiskAssessment;
                    var personalRiskAssessmentDto = dto as PersonalRiskAssessmentDto;
                    personalRiskAssessmentDto.Sensitive = personalRiskAssessment.Sensitive;
                    personalRiskAssessmentDto.ChecklistGeneratorMessage = personalRiskAssessment.ChecklistGeneratorMessage;
                    personalRiskAssessmentDto.PersonalRiskAssessementEmployeeChecklistStatus = personalRiskAssessment.PersonalRiskAssessementEmployeeChecklistStatus;
                    personalRiskAssessmentDto.SendCompletedChecklistNotificationEmail = personalRiskAssessment.SendCompletedChecklistNotificationEmail;
                    personalRiskAssessmentDto.CompletionDueDateForChecklists = personalRiskAssessment.CompletionDueDateForChecklists;
                    personalRiskAssessmentDto.CompletionNotificationEmailAddress = personalRiskAssessment.CompletionNotificationEmailAddress;
                    personalRiskAssessmentDto.Hazards = personalRiskAssessment.Hazards.ToList().Select(h => HazardDtoMapper.Map(h)).ToList();
                }

                if(entity.Self as GeneralRiskAssessment != null)
                {
                    dto = new GeneralRiskAssessmentDto();
                }

                var multiHazardRiskAssessmentDto = dto as MultiHazardRiskAssessmentDto;
                var multiHazardRiskAssessment = entity.Self as MultiHazardRiskAssessment;
                if (multiHazardRiskAssessment != null)
                {
                    multiHazardRiskAssessmentDto.Location = multiHazardRiskAssessment.Location;
                    multiHazardRiskAssessmentDto.TaskProcessDescription = multiHazardRiskAssessment.TaskProcessDescription;
                    multiHazardRiskAssessmentDto.CompletionDueDate = multiHazardRiskAssessment.CompletionDueDate;
                }
            }
            else if(entity.Self as FireRiskAssessment != null)
            {
                dto = new FireRiskAssessmentDto();
                var fireRiskAssessment = entity.Self as FireRiskAssessment;
                var fireRiskAssessmentDto = dto as FireRiskAssessmentDto;
                fireRiskAssessmentDto.PersonAppointed = fireRiskAssessment.PersonAppointed;
                fireRiskAssessmentDto.PremisesProvidesSleepingAccommodation = fireRiskAssessment.PremisesProvidesSleepingAccommodation;
                fireRiskAssessmentDto.PremisesProvidesSleepingAccommodationConfirmed = fireRiskAssessment.PremisesProvidesSleepingAccommodationConfirmed;
                fireRiskAssessmentDto.Location = fireRiskAssessment.Location;
                fireRiskAssessmentDto.BuildingUse = fireRiskAssessment.BuildingUse;
                fireRiskAssessmentDto.NumberOfFloors = fireRiskAssessment.NumberOfFloors;
                fireRiskAssessmentDto.NumberOfPeople = fireRiskAssessment.NumberOfPeople;
                fireRiskAssessmentDto.ElectricityEmergencyShutOff = fireRiskAssessment.ElectricityEmergencyShutOff;
                fireRiskAssessmentDto.WaterEmergencyShutOff = fireRiskAssessment.WaterEmergencyShutOff;
                fireRiskAssessmentDto.GasEmergencyShutOff = fireRiskAssessment.GasEmergencyShutOff;
                fireRiskAssessmentDto.OtherEmergencyShutOff = fireRiskAssessment.OtherEmergencyShutOff;
                fireRiskAssessmentDto.CompletionDueDate = fireRiskAssessment.CompletionDueDate;
                
            }
            else
            {
                dto = new RiskAssessmentDto();
            }

            dto.Id = entity.Id;
            dto.AssessmentDate = entity.AssessmentDate;
            dto.CreatedOn = entity.CreatedOn;
            dto.CompanyId = entity.CompanyId;
            dto.Reference = entity.Reference;
            dto.Status = entity.Status;
            dto.Title = entity.Title;
            dto.Deleted = entity.Deleted;
            dto.CreatedBy = entity.CreatedBy != null ?  new AuditedUserDtoMapper().Map(entity.CreatedBy) : null;

            dto.RiskAssessmentSite = entity.RiskAssessmentSite != null
                ? new SiteStructureElementDtoMapper().Map(entity.RiskAssessmentSite)
                : null;

            dto.RiskAssessor = entity.RiskAssessor != null
                                   ? new RiskAssessorDtoMapper().MapWithEmployee(entity.RiskAssessor)
                                   : null;

            dto.NextReviewDate = entity.NextReviewDate;
            //Required for the API
            dto.Reviews = entity.Reviews != null ? entity.Reviews.Select(review => new RiskAssessmentReviewDto()
                                                                                       {
                                                                                           Comments = review.Comments
                                                                                           ,CompletedBy = review.CompletedBy != null ? new EmployeeDtoMapper().Map(review.CompletedBy) : null
                                                                                           ,CompletedDate = review.CompletedDate
                                                                                           ,CompletionDueDate = review.CompletionDueDate,
                                                                                           Id = review.Id,
                                                                                           IsReviewOutstanding = review.IsReviewOutstanding
                                                                                           ,ReviewAssignedTo = review.ReviewAssignedTo != null ? new EmployeeDtoMapper().Map(review.ReviewAssignedTo) : null
                                                                                       }).ToList() : new List<RiskAssessmentReviewDto>();
            return dto;
        }

        public IEnumerable<RiskAssessmentDto> MapWithSiteAndRiskAssessor(IEnumerable<RiskAssessment> entities)
        {
            return entities.Select(MapWithSiteAndRiskAssessor);
        }

        public IEnumerable<RiskAssessmentDto> MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor(IEnumerable<RiskAssessment> entities)
        {
            return entities.Select(MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor);
        }

        public RiskAssessmentDto MapWithTitleReference(RiskAssessment entity)
        {
            var dto = new RiskAssessmentDto();

            dto.Id = entity.Id;
            dto.Reference = entity.Reference;
            dto.Title = entity.Title;
            
            return dto;
        }
    }
}
