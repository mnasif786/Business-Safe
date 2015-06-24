using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Application.Custom_Exceptions
{
    public class EmailRegisteredToOtherUserException : Exception
    {
        public EmailRegisteredToOtherUserException()
            : base(string.Format("Sorry you are unable to update this user: the email address has been registered to another user")){}
    }
}
