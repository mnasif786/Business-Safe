using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class MaintenanceUser : BaseEntity<Guid>
    {
        public virtual string Username { get; set; }
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Email { get; set; }

        public virtual string FullName
        {
            get
            {
                string fullName = Forename;
                if (!String.IsNullOrEmpty(Forename) && !String.IsNullOrEmpty(Surname))
                    fullName += " ";

                fullName += Surname;
                return fullName;
            }
        }
    }
}
