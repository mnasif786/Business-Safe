using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Application.Request.Users
{
    public class CreateUserRequest
    {
        public Guid UserId { get; set; }
        public long ClientId { get; set; }
        public String Forename { get; set; }
        public String Surname { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
    }
}
