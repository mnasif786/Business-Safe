using System;
using BusinessSafe.Domain.Constants;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.ParameterClasses
{
    public class AddUpdateEmployeeParameters
    {
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string PreviousSurname { get; set; }
        public string MiddleName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Nationality Nationality { get; set; }
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
        public CompanyVehicleType CompanyVehicleType { get; set; }
        public long? SiteId { get; set; }
        public long? OrganisationalUnitId { get; set; }
        public string JobTitle { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public string EmployeeReference { get; set; }
        public long ClientId { get; set; }
        public Site Site { get; set; }
        public NotificationTypeParameters NotificationType { get; set; }
        public int? NotificationFrequency { get; set; }
    }
}