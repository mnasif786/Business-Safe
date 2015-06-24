using System.Collections.Generic;

using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class Checklist : Entity<long>
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<Section> Sections { get; set; }
        public virtual ChecklistRiskAssessmentType ChecklistRiskAssessmentType { get; set; }
    }
}
