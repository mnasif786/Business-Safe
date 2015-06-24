using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class RolePermission : Entity<long>
    {
        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
