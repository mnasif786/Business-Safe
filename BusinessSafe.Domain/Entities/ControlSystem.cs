using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class ControlSystem : Entity<long>
    {
        public virtual string Description { get; set; }
        public virtual long? DocumentLibraryId { get; set; }
    }
}
