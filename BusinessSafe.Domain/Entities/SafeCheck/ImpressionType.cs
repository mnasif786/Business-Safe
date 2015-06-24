using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class ImpressionType : BaseEntity<Guid>
    {
        public virtual string Title { get; set; }

        public virtual string Comments { get; set; }

    }
}
