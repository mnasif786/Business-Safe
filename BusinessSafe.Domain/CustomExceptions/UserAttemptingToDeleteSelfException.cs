using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class UserAttemptingToDeleteSelfException : ApplicationException
    {
        public UserAttemptingToDeleteSelfException(Guid userId)
            : base(string.Format("Current user trying to delete self. Current User Id {0}", userId))
        { }
    }
}