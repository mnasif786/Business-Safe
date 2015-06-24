using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Application.Contracts
{
    public interface IUserRegistrationService
    {
        bool HasEmailBeenRegistered(string email);
    }

   
}
