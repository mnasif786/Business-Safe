using System;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class AuthenticationTokenDto
    {
        public Guid Id { get; set; }
        public Guid ApplicationToken { get; set; }
        public bool IsEnabled { get; set; }
        public string ReasonForDeauthorisation { get; set; }
        public UserDto User { get; set; }
    }
}
