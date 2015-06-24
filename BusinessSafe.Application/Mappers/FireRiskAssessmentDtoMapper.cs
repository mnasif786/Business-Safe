using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class FireRiskAssessmentDtoMapper
    {
        public FireRiskAssessmentDto Map(FireRiskAssessment entity)
        {
            var dto = new RiskAssessmentDtoMapper().MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor(entity) as FireRiskAssessmentDto;
            return dto;
        }

        public IEnumerable<FireRiskAssessmentDto> Map(IEnumerable<FireRiskAssessment> riskAssessments)
        {
            return riskAssessments.Select(Map);
        }

        public FireRiskAssessmentDto MapWithFireSafetyControlMeasuresAndPeopleAtRiskAndSourcesOfFuelAndSourcesOfIgnition(FireRiskAssessment entity)
        {
            var dto = new RiskAssessmentDtoMapper().MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor(entity) as FireRiskAssessmentDto;

            //TODO: PTD - map this properly
            dto.PeopleAtRisk = PeopleAtRiskDtoMapper.Map(entity.PeopleAtRisk.Select(x => x.PeopleAtRisk).ToList());

            dto.FireSafetyControlMeasures = new FireRiskAssessmentControlMeasureDtoMapper().MapWithControlMeasure(entity.FireSafetyControlMeasures);
            dto.FireRiskAssessmentSourcesOfFuel = new FireRiskAssessmentSourceOfFuelDtoMapper().MapWithSourceOfFuel(entity.FireRiskAssessmentSourcesOfFuel);
            dto.FireRiskAssessmentSourcesOfIgnition = new FireRiskAssessmentSourceOfIgnitionDtoMapper().MapWithSourceOfIgnition(entity.FireRiskAssessmentSourcesOfIgnition);
            return dto;
        }

        public FireRiskAssessmentDto MapWithLatestFireRiskAssessmentChecklist(FireRiskAssessment entity)
        {
            var dto = new RiskAssessmentDtoMapper().MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor(entity) as FireRiskAssessmentDto;
            dto.LatestFireRiskAssessmentChecklist = new FireRiskAssessmentChecklistDtoMapper().MapWithChecklist(entity.LatestFireRiskAssessmentChecklist);
            return dto;
        }

        public FireRiskAssessmentDto MapWithSignificantFindings(FireRiskAssessment entity)
        {
            var dto = new RiskAssessmentDtoMapper().MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor(entity) as FireRiskAssessmentDto;
            dto.SignificantFindings = new SignificantFindingsDtoMapper().Map(entity.LatestFireRiskAssessmentChecklist.SignificantFindings.Where(x => x.Deleted == false).ToList());
            return dto;
        }

        public FireRiskAssessmentDto MapWithEverything(FireRiskAssessment entity)
        {
            var dto = new RiskAssessmentDtoMapper().MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor(entity) as FireRiskAssessmentDto;
            dto.PeopleAtRisk = PeopleAtRiskDtoMapper.Map(entity.PeopleAtRisk.Select(x => x.PeopleAtRisk).ToList());
            dto.FireSafetyControlMeasures = new FireRiskAssessmentControlMeasureDtoMapper().MapWithControlMeasure(entity.FireSafetyControlMeasures);
            dto.FireRiskAssessmentSourcesOfFuel = new FireRiskAssessmentSourceOfFuelDtoMapper().MapWithSourceOfFuel(entity.FireRiskAssessmentSourcesOfFuel);
            dto.FireRiskAssessmentSourcesOfIgnition = new FireRiskAssessmentSourceOfIgnitionDtoMapper().MapWithSourceOfIgnition(entity.FireRiskAssessmentSourcesOfIgnition);
            dto.LatestFireRiskAssessmentChecklist = new FireRiskAssessmentChecklistDtoMapper().MapWithChecklist(entity.LatestFireRiskAssessmentChecklist);
            dto.SignificantFindings = new SignificantFindingsDtoMapper().Map(entity.LatestFireRiskAssessmentChecklist.SignificantFindings.Where(x => x.Deleted == false).ToList());
            return dto;
        }
    }
}