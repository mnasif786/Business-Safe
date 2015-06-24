using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class EmploymentStatus : Entity<int>
    {
        public virtual string Name { get; set; }
    }
}