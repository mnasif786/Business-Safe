using System;

using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class ApplicationToken : BaseEntity<Guid>
    {
        public virtual bool IsEnabled { get; set; }
        public virtual string AppName { get; set; }
    }
}
