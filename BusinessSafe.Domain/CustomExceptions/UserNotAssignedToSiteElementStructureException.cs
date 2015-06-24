using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class UserNotAssignedToSiteElementStructureException : ApplicationException
    {
        public UserNotAssignedToSiteElementStructureException(string userId)
            : base(string.Format("User is not assigned to a site element structure. User Id is {0}",userId))
        {}
    }
}