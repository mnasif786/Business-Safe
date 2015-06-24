using System;

namespace BusinessSafe.Application.Request.Checklists
{
    public class AuthenticateUserRequest
    {
        public string Password { get; set; }
        public Guid ChecklistId { get; set; }
    }
}