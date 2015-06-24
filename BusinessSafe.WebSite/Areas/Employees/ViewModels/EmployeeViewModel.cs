using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.CustomValidators;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Employees.ViewModels
{
    public enum NotificationTypeViewModel
    {
        Daily = 1,
        Weekly = 2,
        Monthly = 3
    }
    public class EmployeeViewModel : IValidatableObject
    {
        public Guid? EmployeeId { get; set; }
        public long CompanyId { get; set; }
        public long? SiteId { get; set; }
        public int? EmploymentStatusId { get; set; }
        public int? CountryId { get; set; }
        public string EmployeeReference { get; set; }
        public string NameTitle { get; set; }

        [Required(ErrorMessage = "Forename is required")]
        public string Forename { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        public string PreviousSurname { get; set; }
        public string MiddleName { get; set; }

        //[Required(ErrorMessage = "Sex is required")]
        [RegularExpression(@"Male|Female", ErrorMessage = "Gender is required")]
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
        public string Postcode { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public long ContactDetailId { get; set; }
        public Guid UserId { get; set; }

        [Email(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        public string JobTitle { get; set; }
        public bool IsExistingUser { get; set; }
        public bool CanChangeEmail { get; set; }
        public bool IsPendingRegistration { get; set; }

        public IEnumerable<EmergencyContactViewModel> EmergencyContactDetails { get; set; }

        public IEnumerable<AutoCompleteViewModel> Titles { get; set; }
        public IEnumerable<AutoCompleteViewModel> Nationalities { get; set; }
        public IEnumerable<AutoCompleteViewModel> Sexes { get; set; }
        public IEnumerable<AutoCompleteViewModel> Countries { get; set; }
        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }
        public IEnumerable<AutoCompleteViewModel> EmploymentStatuses { get; set; }
        public IEnumerable<AutoCompleteViewModel> CompanyVehicleTypes { get; set; }        

        public IEnumerable<AutoCompleteViewModel> UserRoles { get; set; }
        public Guid? UserRoleId { get; set; }
        public bool CanChangeRoleDdl { get; set; }
        public string UserRoleDescription { get; set; }

        public IEnumerable<AutoCompleteViewModel> UserSiteGroups { get; set; }
        public long? UserSiteGroupId { get; set; }

        public IEnumerable<AutoCompleteViewModel> UserSites { get; set; }
        public long? UserSiteId { get; set; }

        public bool UserPermissionsApplyToAllSites { get; set; }

        public NotificationTypeViewModel NotificationType { get; set; }

        public int? NotificationFrequency { get; set; }
        public bool IsRiskAssessor { get; set; }
        public bool DoNotSendTaskOverdueNotifications { get; set; }
        public bool DoNotSendTaskCompletedNotifications { get; set; }
        public bool DoNotSendReviewDueNotification { get; set; }
        public bool DoNotSendDueTomrrowNotification { get; set; }
        public bool IsRiskAssessorAssignedToRiskAssessments { get; set; }
        public string RiskAssessorSite { get; set; }
        public long? RiskAssessorSiteId { get; set; }
        public bool RiskAssessorHasAccessToAllSites { get; set; }
        public bool SaveUserSuccessNotificationVisible { get; set; }

        public EmployeeViewModel()
        {
            EmployeeReference = string.Empty;
            NameTitle = string.Empty;
            Forename = string.Empty;
            Surname = string.Empty;
            PreviousSurname = string.Empty;
            MiddleName = string.Empty;
            Sex = string.Empty;
            NINumber = string.Empty;
            DrivingLicenseNumber = string.Empty;
            PPSNumber = string.Empty;
            PassportNumber = string.Empty;
            WorkVisaNumber = string.Empty;
            DisabilityDescription = string.Empty;
            CompanyVehicleRegistration = string.Empty;
            Address1 = string.Empty;
            Address2 = string.Empty;
            Address3 = string.Empty;
            County = string.Empty;
            Postcode = string.Empty;
            Telephone = string.Empty;
            Mobile = string.Empty;
            Email = string.Empty;
            JobTitle = string.Empty;
            EmergencyContactDetails = new List<EmergencyContactViewModel>();
            NotificationType = NotificationTypeViewModel.Daily;

           // UserRoleId = Guid.Empty;
            CanChangeRoleDdl = true;
            SaveUserSuccessNotificationVisible = false;
        }

        public string GetDrivingLicenseExpirationDate()
        {
            return !DrivingLicenseExpirationDate.HasValue ? string.Empty : DrivingLicenseExpirationDate.Value.ToShortDateString();
        }

        public string GetWorkingVisaExpirationDate()
        {
            return !WorkVisaExpirationDate.HasValue ? string.Empty : WorkVisaExpirationDate.Value.ToShortDateString();
        }

        public string GetDateOfBirth()
        {
            if (!DateOfBirth.HasValue || DateOfBirth.Value == DateTime.MinValue)
                return string.Empty;

            return DateOfBirth.Value.ToShortDateString();
        }

        public string GetCalculatedAge()
        {
            if (!DateOfBirth.HasValue || DateOfBirth.Value == DateTime.MinValue)
                return "";

            var now = DateTime.Today;
            var age = now.Year - DateOfBirth.Value.Year;

            if (DateOfBirth > now.AddYears(-age)) age--;
            
            return age.ToString();
        }

        public string GetEmployeeId()
        {
            return EmployeeId.HasValue ? EmployeeId.Value.ToString() : string.Empty;
        }

        public string GetTitle()
        {
            return IsExistingEmployee() ? "Update Employee" : "Create Employee";
        }

        public string GetPostAction()
        {
            return IsExistingEmployee() ? "Update" : "Create";
        }

        private bool IsExistingEmployee()
        {
            return EmployeeId.HasValue && EmployeeId.Value != Guid.Empty;
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (!UserSiteId.HasValue && !UserSiteGroupId.HasValue && !UserPermissionsApplyToAllSites && !UserRoleId.HasValue)
            {
                return new List<ValidationResult>();
            }

            if (!UserRoleId.HasValue)
            {
                validationResults.Add(
                    new ValidationResult(
                        "The User Role must be selected for the user", new List<string> { "UserRoleId" }));
            }

            if (!UserSiteId.HasValue && !UserSiteGroupId.HasValue && !UserPermissionsApplyToAllSites)
            {
                return new List<ValidationResult>
                {
                    new ValidationResult( "Either the Site field or Site Group field must be selected, or the All Sites checkbox must be checked.", new List<string>{ "UserSiteId"}),
                    new ValidationResult( null, new List<string>{"UserSiteGroupId", "PermissionsApplyToAllSites"})
                };
            }

            if (UserSiteId.HasValue && UserSiteGroupId.HasValue)
            {
                return new List<ValidationResult>
                       {
                           new ValidationResult( "The Site and Site Group fields cannot both be selected.", new List<string>{ "UserSiteId"}),
                           new ValidationResult( null, new List<string>{"UserSiteGroupId"})
                       };
            }

            if ((UserSiteId.HasValue && UserPermissionsApplyToAllSites) || (UserSiteGroupId.HasValue && UserPermissionsApplyToAllSites))
            {
                validationResults.Add(
                    new ValidationResult(
                        "If the All Sites checkbox is checked, the Site and Site Group fields must be left unselected.",
                        new List<string> { "PermissionsApplyToAllSites" }));

                if (UserSiteId.HasValue)
                {
                    validationResults.Add(new ValidationResult(null, new List<string> { "UserSiteId" }));
                }

                if (UserSiteGroupId.HasValue)
                {
                    validationResults.Add(new ValidationResult(null, new List<string> { "UserSiteGroupId" }));
                }
    
                return validationResults;
            }

            return validationResults;
        }
    }


}