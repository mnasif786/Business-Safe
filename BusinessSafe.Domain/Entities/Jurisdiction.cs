    using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class Jurisdiction : Entity<long>
    {
        public virtual string Name { get; set; }
        public virtual int Order { get; set; }
    }
}
