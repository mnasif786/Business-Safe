using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class HazardousSubstanceGroup : Entity<long>
    {
        public virtual string Code { get; set; }
    }
}
