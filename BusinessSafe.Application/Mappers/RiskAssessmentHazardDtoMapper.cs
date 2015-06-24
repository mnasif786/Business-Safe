using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class RiskAssessmentHazardDtoMapper
    {
        public RiskAssessmentHazardDto Map(MultiHazardRiskAssessmentHazard riskAssessmentHazard)
        {
            var riskAssessmentHazardDto = new RiskAssessmentHazardDto
                                              {
                                                  Id = riskAssessmentHazard.Id,
                                                  Description = riskAssessmentHazard.Description,
                                                  Hazard = riskAssessmentHazard.Hazard != null ? 
                                                        new HazardDto //todo: ptd - replace this with its own mapper.
                                                               {
                                                                   Id = riskAssessmentHazard.Hazard.Id,
                                                                   Name = riskAssessmentHazard.Hazard.Name
                                                               } 
                                                        : null,
                                                        // Temporary until we start using multihazardriskassessment
                                                  RiskAssessment = GeneralRiskAssessmentDto.CreateFrom(riskAssessmentHazard.MultiHazardRiskAssessment)
                                              };

            return riskAssessmentHazardDto;
        }
    }
}