using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class OthersInvolvedAccidentDetails : Entity<int>
    {
        public virtual string Name { get; protected set; }
    }
}
