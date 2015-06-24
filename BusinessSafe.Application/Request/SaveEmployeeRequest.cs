using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessSafe.Application.DataTransferObjects;


namespace BusinessSafe.Application.Request
{
    public class SaveEmployeeRequest
    {
        [Required(ErrorMessage = "Employee Reference is required")]
        public string EmployeeReference { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage = "Forename is required")]
        public string Forename { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        public string PreviousSurname { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Sex is required")]
        public string Sex { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public int? NationalityId { get; set; }
        public string NINumber { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public string PPSNumber { get; set; }
        public DateTime? DrivingLicenseExpirationDate { get; set; }
        public string PassportNumber { get; set; }
        public string WorkVisaNumber { get; set; }
        public DateTime? WorkVisaExpirationDate { get; set; }
        public bool HasDisability { get; set; }
        public string DisabilityDescription { get; set; }
        public bool HasCompanyVehicle { get; set; }
        public int? CompanyVehicleTypeId { get; set; }
        public string CompanyVehicleRegistration { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public int CountryId { get; set; }
        public string Postcode { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public long? SiteId { get; set; }
        public int? EmploymentStatusId { get; set; }
        public Guid UserId { get; set; }
        public long CompanyId { get; set; }
        
        public IEnumerable<CreateEmergencyContactRequest> EmergencyContactDetails { get; set; }
        public long ContactDetailId { get; set; }
        public bool IsRiskAssessor { get; set; }
        public bool DoNotSendTaskOverdueNotifications { get; set; }
        public bool DoNotSendTaskCompletedNotifications { get; set; }
        public bool DoNotSendReviewDueNotification { get; set; }
        public bool DoNotSendTaskDueTomorrowNotification { get; set; }
        public bool RiskAssessorHasAccessToAllSites { get; set; }
        public long RiskAssessorSiteId { get; set; }
        public NotificationTypeDto NotificationType { get; set; }
        public int? NotificiationFrequency { get; set; }
        public SaveEmployeeRequest()
        {
            EmergencyContactDetails = new List<CreateEmergencyContactRequest>();

            NotificationType = NotificationTypeDto.Daily;
        }
    }
}