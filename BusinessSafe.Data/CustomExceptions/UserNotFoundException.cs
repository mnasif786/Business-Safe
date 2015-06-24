using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class UserNotFoundException : ArgumentNullException
    {
        public UserNotFoundException(Guid userId)
            : base(string.Format("User Not Found. User not found for user id {0}", userId))
        {
        }
    }
}