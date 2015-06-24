using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class EmployeeForAuditing : BaseEntity<Guid>
    {
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual long CompanyId { get; set; }

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