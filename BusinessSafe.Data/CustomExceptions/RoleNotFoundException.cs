using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class RoleNotFoundException : ArgumentException
    {
        public RoleNotFoundException()
        { }

        public RoleNotFoundException(Guid roleId)
            : base(string.Format("Role not for found. Role Id requested {0}", roleId))
        {

        }
    }
}