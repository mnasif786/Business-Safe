using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToDeleteSystemRoleException : Exception
    {
        public AttemptingToDeleteSystemRoleException(string name)
            : base(string.Format("Trying to delete system role. System role deleting {0}", name))
        { }
    }
}