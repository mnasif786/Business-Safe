using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts;

namespace BusinessSafe.Application.Implementations.Users
{
    public class RegistrationServiceStub : IUserRegistrationService
    {

        public bool HasEmailBeenRegistered(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}
