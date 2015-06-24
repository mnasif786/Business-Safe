using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class StatutoryResponsibilityTaskTemplate : Entity<long>
    {
        public virtual StatutoryResponsibilityTemplate StatutoryResponsibilityTemplate { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual TaskReoccurringType TaskReoccurringType { get; set; }
    }
}