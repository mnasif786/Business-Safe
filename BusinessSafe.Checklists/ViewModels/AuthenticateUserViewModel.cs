using System;

namespace BusinessSafe.Checklists.ViewModels
{
    public class AuthenticateUserViewModel
    {
        public string Password { get; set; }
        public Guid ChecklistId { get; set; }
    }
}