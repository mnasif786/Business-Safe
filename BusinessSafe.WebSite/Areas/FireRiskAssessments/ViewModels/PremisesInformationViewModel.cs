using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels
{
    public class PremisesInformationViewModel
    {
        public bool? PremisesProvidesSleepingAccommodation { get; set; }
        public bool? PremisesProvidesSleepingAccommodationConfirmed { get; set; }
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public bool IsSaveButtonEnabled { get; set; }
        
        [Required(ErrorMessage = "Location is required")]
        [StringLength(250, ErrorMessage = "Max length is 250 characters")]
        public string Location { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500 characters")]
        public string BuildingUse { get; set; }
        
        [Range(1,1000,ErrorMessage = "Number of floors must be between 1 and 1000")]
        [Required(ErrorMessage = "Number of floors is required")]
        public int? NumberOfFloors { get; set; }

        [Range(1, 1000000, ErrorMessage = "Number of people must be between 1 and 1000000")]
        [Required(ErrorMessage = "Number of people is required")]
        public int? NumberOfPeople { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500 characters")]
        public string OtherEmergencyShutOff { get; set; }
        
        [Required(ErrorMessage = "Water emergency shutoff is required")]
        [StringLength(500, ErrorMessage = "Max length is 500 characters")]
        public string WaterEmergencyShutOff { get; set; }
        
        [Required(ErrorMessage = "Gas emergency shutoff is required")]
        [StringLength(500, ErrorMessage = "Max length is 500 characters")]
        public string GasEmergencyShutOff { get; set; }
        
        [Required(ErrorMessage = "Electricity emergency shutoff is required")]
        [StringLength(500, ErrorMessage = "Max length is 500 characters")]
        public string ElectricityEmergencyShutOff { get; set; }
        
    }
}