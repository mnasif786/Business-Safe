using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public static class HazardDtoMapper
    {
        public static HazardDto Map(MultiHazardRiskAssessmentHazard riskAssessmentHazard)
        {
            return new HazardDto()
                       {
                           Id = riskAssessmentHazard.Hazard.Id,
                           RiskAssessmentHazardId = riskAssessmentHazard.Id,
                           Name = riskAssessmentHazard.Hazard.Name,
                           IsSystemDefault = riskAssessmentHazard.Hazard.CompanyId == 0,
                           Description = riskAssessmentHazard.Description,
                           ControlMeasures = GetRiskAssessmentControlMeasures(riskAssessmentHazard).ToList(),
                           FurtherActionTasks = GetRiskAssessmentFurtherActionTasks(riskAssessmentHazard),
                           RiskAssessmentId = riskAssessmentHazard.Hazard.RiskAssessmentId
                       };
        }

        public static HazardDto MapWithoutFurtherActionTasks(MultiHazardRiskAssessmentHazard riskAssessmentHazard)
        {
            return new HazardDto()
            {
                Id = riskAssessmentHazard.Hazard.Id,
                RiskAssessmentHazardId = riskAssessmentHazard.Id,
                Name = riskAssessmentHazard.Hazard.Name,
                IsSystemDefault = riskAssessmentHazard.Hazard.CompanyId == 0,
                Description = riskAssessmentHazard.Description,
                ControlMeasures = GetRiskAssessmentControlMeasures(riskAssessmentHazard).ToList(),
                RiskAssessmentId = riskAssessmentHazard.Hazard.RiskAssessmentId
            };
        }

        private static IEnumerable<ControlMeasureDto> GetRiskAssessmentControlMeasures(MultiHazardRiskAssessmentHazard riskAssessmentHazard)
        {
            return new ControlMeasureDtoMapper().Map(riskAssessmentHazard.ControlMeasures);
        }

        private static List<TaskDto> GetRiskAssessmentFurtherActionTasks(MultiHazardRiskAssessmentHazard riskAssessmentHazard)
        {
            return new TaskDtoMapper().MapWithAssignedTo(riskAssessmentHazard.FurtherControlMeasureTasks).ToList();
        }
    }
}