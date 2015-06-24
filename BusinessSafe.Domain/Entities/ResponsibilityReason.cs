using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class ResponsibilityReason : Entity<long>
    {
        public virtual string Reason { get; protected set; }

        public static ResponsibilityReason Create(long id, string reason)
        {
            var responsibilityReason = new ResponsibilityReason { Id = id, Reason = reason };

            return responsibilityReason;
        }
    }
}
