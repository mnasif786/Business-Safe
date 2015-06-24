using System;
using BusinessSafe.WebSite.CustomValidators;

namespace BusinessSafe.WebSite.Areas.Employees.ViewModels
{
    public class ResendUserRegistrationEmailViewModel
    {
        public Guid EmployeeId { get; set; }
        public Guid UserId { get; set; }
        [Email(ErrorMessage = "Email address entered is invalid, please check and try again.")]
        public string Email { get; set; }
        public long CompanyId { get; set; }
    }
}