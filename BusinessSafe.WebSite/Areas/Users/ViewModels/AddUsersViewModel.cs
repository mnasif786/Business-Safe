using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Users.ViewModels
{
    public class AddUsersViewModel : IValidatableObject
    {
        public long CompanyId { get; set; }
        public IEnumerable<AutoCompleteViewModel> Employees { get; set; }
        public IEnumerable<AutoCompleteViewModel> Roles { get; set; }
        public IEnumerable<AutoCompleteViewModel> SiteGroups { get; set; }
        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }
        public Guid UserId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeReference { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string ManagerName { get; set; }
        [Required(ErrorMessage = "User Role is required")]
        public Guid RoleId { get; set; }
        public string RoleDescription { get; set; }
        public bool PermissionsApplyToAllSites { get; set; }
        public long? SiteGroupId { get; set; }
        public long? SiteId { get; set; }
        public bool SaveCancelButtonsVisible { get; set; }
        public bool SaveSuccessNotificationVisible { get; set; }
        public bool CanChangeEmployeeDdl { get; set; }
        public bool EmployeeAlreadyExistsAsUser { get; set; }
        public long MainSiteId { get; set; }
        public bool CanChangeRoleDdl { get; set; }

        public bool IsUserDeleted { get; set; }
        public bool IsUserRegistered { get; set; }
        
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {                      
            if (!SiteId.HasValue && !SiteGroupId.HasValue && !PermissionsApplyToAllSites)
            {
                return new List<ValidationResult>
                       {
                           new ValidationResult( "Either the Site field or Site Group field must be selected, or the All Sites checkbox must be checked.", new List<string>{ "SiteId"}),
                           new ValidationResult( null, new List<string>{"SiteGroupId", "PermissionsApplyToAllSites"})
                       };
            }

            if (SiteId.HasValue && SiteGroupId.HasValue)
            {
                return new List<ValidationResult>
                       {
                           new ValidationResult( "The Site and Site Group fields cannot both be selected.", new List<string>{ "SiteId"}),
                           new ValidationResult( null, new List<string>{"SiteGroupId"})
                       };
            }

            if ((SiteId.HasValue && PermissionsApplyToAllSites) || (SiteGroupId.HasValue && PermissionsApplyToAllSites))
            {
                var validationResults = new List<ValidationResult>();

                validationResults.Add(
                    new ValidationResult(
                        "If the All Sites checkbox is checked, the Site and Site Group fields must be left unselected.",
                        new List<string> {"PermissionsApplyToAllSites"}));

                if(SiteId.HasValue)
                {
                    validationResults.Add(new ValidationResult(null, new List<string> { "SiteId" }));
                }

                if (SiteGroupId.HasValue)
                {
                    validationResults.Add(new ValidationResult(null, new List<string> { "SiteGroupId" }));
                }

                return validationResults;
            }

            return new List<ValidationResult>();
        }
    }
}