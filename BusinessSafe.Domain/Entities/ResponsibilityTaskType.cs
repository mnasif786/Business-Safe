using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class ResponsibilityTaskType : Entity<long>
    {
        public virtual string Type { get; protected set; }
    }
}