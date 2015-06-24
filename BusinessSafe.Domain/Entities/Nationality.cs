using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class Nationality : Entity<int>
    {
        public virtual string Name { get; protected set; }
    }
}