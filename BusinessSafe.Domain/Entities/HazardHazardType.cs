using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class HazardType: Entity<long>
    {
        public virtual string Name { get; set; }
    }
}
