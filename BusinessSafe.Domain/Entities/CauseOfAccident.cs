using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class CauseOfAccident : Entity<long>
    {
        public virtual string Description { get; set; }
    }
}
