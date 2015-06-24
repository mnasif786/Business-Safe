using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class UserForAuditing : Entity<Guid>
    {
        public virtual long CompanyId { get; set; }
        public virtual EmployeeForAuditing Employee { get; set; }
    }
}