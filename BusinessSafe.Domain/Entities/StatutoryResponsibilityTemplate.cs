using System.Collections.Generic;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class StatutoryResponsibilityTemplate : Entity<long>
    {
        public virtual ResponsibilityCategory ResponsibilityCategory { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string GuidanceNote { get; set; }
        public virtual ResponsibilityReason ResponsibilityReason { get; set; }
        public virtual TaskReoccurringType TaskReoccurringType { get; set; }
        public virtual IList<StatutoryResponsibilityTaskTemplate> ResponsibilityTasks { get; set; }
    }
}
