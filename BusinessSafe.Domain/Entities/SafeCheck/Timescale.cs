using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class Timescale : BaseEntity<long>
    {
        public virtual string Name { get; set; }
    }
}