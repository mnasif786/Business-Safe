using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class AccidentType : Entity<long>
    {
        public virtual string Description { get; set; }
        public virtual long? CompanyId { get; set;  }
    }
}
