using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels
{
    public class PremisesInformationViewModel
    {
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        [Required(ErrorMessage = "Location/Area/Department is required")]
        public string LocationAreaDepartment { get; set; }
        [Required(ErrorMessage = "Task/Process Description is required")]
        public string TaskProcessDescription { get; set; }
        public IEnumerable<Tuple<long, string>> NonEmployees { get; set; }
        public IEnumerable<Tuple<Guid, string>> Employees { get; set; }
        
        public bool IsSaveButtonEnabled(IPrincipal user)
        {
            return user.IsInRole(RiskAssessmentId == 0 ? Permissions.AddPersonalRiskAssessments.ToString() : Permissions.EditPersonalRiskAssessments.ToString());
        }    
    }
}