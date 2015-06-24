using System;
namespace BusinessSafe.Application.Request
{
    public class UpdateFireRiskAssessmentPremisesInformationRequest
    {
        public long FireRiskAssessmentId { get; set; }
        public long CompanyId { get; set; }
        public bool? PremisesProvidesSleepingAccommodation { get; set; }
        public bool? PremisesProvidesSleepingAccommodationConfirmed { get; set; }
        public Guid CurrentUserId { get; set; }
        public string BuildingUse { get; set; }
        public string Location { get; set; }
        public int? NumberOfFloors { get; set; }
        public int? NumberOfPeople { get; set; }
        public string ElectricityEmergencyShutOff { get; set; }
        public string WaterEmergencyShutOff { get; set; }
        public string GasEmergencyShutOff { get; set; }
        public string OtherEmergencyShutOff { get; set; }
    }
}
