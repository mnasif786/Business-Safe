using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class ClientQuestion : BaseEntity<Guid>
    {
        public virtual long ClientId { get; set; }
        public virtual string ClientAccountNumber { get; set; }
        public virtual Question Question { get; set; }
    }
}
