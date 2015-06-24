using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class HazardousSubstanceRiskAssessmentDtoMapper
    {
        public HazardousSubstanceRiskAssessmentDto Map(HazardousSubstanceRiskAssessment riskAssessment)
        {
            var result = new HazardousSubstanceRiskAssessmentDto()
                             {
                                 CompanyId = riskAssessment.CompanyId,
                                 Id = riskAssessment.Id,
                                 Title = riskAssessment.Title,
                                 Reference = riskAssessment.Reference,
                                 CreatedOn = riskAssessment.CreatedOn,
                                 CreatedBy = riskAssessment.CreatedBy != null ? new AuditedUserDtoMapper().Map(riskAssessment.CreatedBy) : null,
                                 Status = riskAssessment.Status,
                                 AssessmentDate = riskAssessment.AssessmentDate,
                                 Deleted = riskAssessment.Deleted,
                                 IsInhalationRouteOfEntry = riskAssessment.IsInhalationRouteOfEntry.HasValue ? riskAssessment.IsInhalationRouteOfEntry.Value : false,
                                 IsIngestionRouteOfEntry = riskAssessment.IsIngestionRouteOfEntry.HasValue ? riskAssessment.IsIngestionRouteOfEntry.Value : false,
                                 IsAbsorptionRouteOfEntry = riskAssessment.IsAbsorptionRouteOfEntry.HasValue ? riskAssessment.IsAbsorptionRouteOfEntry.Value : false,
                                 WorkspaceExposureLimits = riskAssessment.WorkspaceExposureLimits,
                                 NonEmployees = GetNonEmployees(riskAssessment),
                                 Employees = GetEmployees(riskAssessment),
                                 Quantity = riskAssessment.Quantity,
                                 MatterState = riskAssessment.MatterState,
                                 DustinessOrVolatility = riskAssessment.DustinessOrVolatility,
                                 Group = riskAssessment.Group != null ? new HazardousSubstanceGroupDtoMapper().Map(riskAssessment.Group) : null,
                                 HealthSurveillanceRequired = riskAssessment.HealthSurveillanceRequired,
                                 NextReviewDate = riskAssessment.NextReviewDate,
                                 CompletionDueDate = riskAssessment.CompletionDueDate
                             };

            if (riskAssessment.HazardousSubstance != null)
            {
                result.HazardousSubstance = new HazardousSubstanceDtoMapper().Map(riskAssessment.HazardousSubstance);
            }

            if (riskAssessment.RiskAssessor != null)
            {
                result.RiskAssessor = new RiskAssessorDtoMapper().MapWithEmployee(riskAssessment.RiskAssessor);
            }

            if (riskAssessment.RiskAssessmentSite != null)
            {
                result.Site = new SiteStructureElementDtoMapper().Map(riskAssessment.RiskAssessmentSite);
            }

            if (riskAssessment.ControlMeasures != null && riskAssessment.ControlMeasures.Any())
            {
                result.ControlMeasures = new HazardousSubstanceRiskAssessmentControlMeasureDtoMapper().Map(riskAssessment.ControlMeasures);
            }

            if (riskAssessment.FurtherControlMeasureTasks != null && riskAssessment.FurtherControlMeasureTasks.Any())
            {
                result.FurtherControlMeasureTasks = new TaskDtoMapper().MapWithAssignedTo(riskAssessment.FurtherControlMeasureTasks);
            }

            result.Reviews = riskAssessment.Reviews != null ? riskAssessment.Reviews.Select(review => new RiskAssessmentReviewDto()
                                                                                                          {
                                                                                                              Comments = review.Comments
                                                                                                              ,
                                                                                                              CompletedBy = review.CompletedBy != null ? new EmployeeDtoMapper().Map(review.CompletedBy) : null
                                                                                                              ,
                                                                                                              CompletedDate = review.CompletedDate
                                                                                                              ,
                                                                                                              CompletionDueDate = review.CompletionDueDate,
                                                                                                              Id = review.Id,
                                                                                                              IsReviewOutstanding = review.IsReviewOutstanding
                                                                                                              ,
                                                                                                              ReviewAssignedTo = review.ReviewAssignedTo != null ? new EmployeeDtoMapper().Map(review.ReviewAssignedTo) : null
                                                                                                          }).ToList() : new List<RiskAssessmentReviewDto>();

            return result;
        }

        public IEnumerable<HazardousSubstanceRiskAssessmentDto> Map(IEnumerable<HazardousSubstanceRiskAssessment> entities)
        {
            return entities.Select(Map);
        }

        private IList<Tuple<long, string>> GetNonEmployees(HazardousSubstanceRiskAssessment riskAssessment)
        {
            if(riskAssessment.NonEmployees != null)
                return riskAssessment.NonEmployees.Select(nonEmployee => new Tuple<long, string>(nonEmployee.Id, nonEmployee.NonEmployee.GetFormattedName())).ToList();
            
            return new List<Tuple<long, string>>();
        }

        private IList<Tuple<Guid, string>> GetEmployees(HazardousSubstanceRiskAssessment riskAssessment)
        {
            if (riskAssessment.Employees != null)
                return riskAssessment.Employees.Select(employee => new Tuple<Guid, string>(employee.Employee.Id, employee.Employee.FullName)).ToList();

            return new List<Tuple<Guid, string>>();
        }
    }
}