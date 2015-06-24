using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToDeleteRoleCurrentlyUsedByUsersException : Exception
    {
        public AttemptingToDeleteRoleCurrentlyUsedByUsersException(string name)
            : base(string.Format("Trying to delete role which is currently in use. Role deleting {0}", name))
        { }

        public AttemptingToDeleteRoleCurrentlyUsedByUsersException()
        {}
    }
}