using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string PreviousSurname { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public NationalityDto Nationality { get; set; }
        public string Sex { get; set; }
        public bool HasDisability { get; set; }
        public string DisabilityDescription { get; set; }
        public string NINumber { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public DateTime? DrivingLicenseExpirationDate { get; set; }
        public string WorkVisaNumber { get; set; }
        public DateTime? WorkVisaExpirationDate { get; set; }
        public string PPSNumber { get; set; }
        public string PassportNumber { get; set; }
        public bool HasCompanyVehicle { get; set; }
        public string CompanyVehicleRegistration { get; set; }
        public int CompanyVehicleTypeId { get; set; } //TODO: Change to company vehicle type DTO!
        public string CompanyVehicleType { get; set; } //TODO: Change to company vehicle type DTO!
        public long SiteId { get; set; } //TODO: CHange to site DTO!
        public string SiteName { get; set; }//TODO: CHange to site DTO!
        public string JobTitle { get; set; }
        public int EmploymentStatusId { get; set; } //TOFDO: Change to Employment Status DTO!
        public string EmployeeReference { get; set; }
        public EmployeeContactDetailDto MainContactDetails { get; set; }
        public string FullName { get; set; }
        public bool Deleted { get; set; }
        public IList<EmployeeEmergencyContactDetailDto> EmergencyContactDtos { get; set; }
        public UserDto User { get; set; }
        public bool CanChangeEmail { get; set; }
        public RiskAssessorDto RiskAssessor { get; set; }
        public bool UserPermissionsForAllSites { get; set; }

        public NotificationTypeDto NotificationType { get; set; }

        public int? NotificationFrequency { get; set; }

        //TODO: organisation unit ID missing?
        //TODO: Compnay ID missing?

        public EmployeeDto()
        {
            EmergencyContactDtos = new List<EmployeeEmergencyContactDetailDto>();
        }
    }
}