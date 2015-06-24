using System;

namespace BusinessSafe.Application.Request.Users
{
    public class CreateAdminUserRequest
    {
        public Guid UserId { get; set; }
        public long ClientId { get; set; }
        public String Forename { get; set; }
        public String Surname { get; set; }
        public string Email { get; set; }
    }
}
