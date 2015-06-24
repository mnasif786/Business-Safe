using System.Collections.Generic;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class FireRiskAssessmentDto: RiskAssessmentDto
    {
        public string PersonAppointed { get; set; }
        public bool? PremisesProvidesSleepingAccommodation { get; set; }
        public bool? PremisesProvidesSleepingAccommodationConfirmed { get; set; }
        public string Location { get; set; }
        public string BuildingUse { get; set; }
        public int? NumberOfPeople { get; set; }
        public int? NumberOfFloors { get; set; }
        public string ElectricityEmergencyShutOff { get; set; }
        public string GasEmergencyShutOff { get; set; }
        public string WaterEmergencyShutOff { get; set; }
        public string OtherEmergencyShutOff { get; set; }
        public IEnumerable<PeopleAtRiskDto> PeopleAtRisk { get; set; }
        public IEnumerable<FireRiskAssessmentControlMeasureDto> FireSafetyControlMeasures { get; set; }
        public IList<FireRiskAssessmentChecklistDto> FireRiskAssessmentChecklists { get; set; }
        public IEnumerable<FireRiskAssessmentSourceOfIgnitionDto> FireRiskAssessmentSourcesOfIgnition { get; set; }
        public IEnumerable<FireRiskAssessmentSourceOfFuelDto> FireRiskAssessmentSourcesOfFuel { get; set; }
        public FireRiskAssessmentChecklistDto LatestFireRiskAssessmentChecklist { get; set; }
        public IEnumerable<SignificantFindingDto> SignificantFindings { get; set; }

        public FireRiskAssessmentDto()
        {
            PeopleAtRisk = new List<PeopleAtRiskDto>();
            FireSafetyControlMeasures = new List<FireRiskAssessmentControlMeasureDto>();
            FireRiskAssessmentSourcesOfIgnition = new List<FireRiskAssessmentSourceOfIgnitionDto>();
            FireRiskAssessmentSourcesOfFuel = new List<FireRiskAssessmentSourceOfFuelDto>();
            SignificantFindings = new List<SignificantFindingDto>();
        }
    }
}