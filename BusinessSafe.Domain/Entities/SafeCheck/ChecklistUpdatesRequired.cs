using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class ChecklistUpdatesRequired : BaseEntity<long>
    {
        public virtual Checklist Checklist { get; set; }
        public virtual QaAdvisor QaAdvisor { get; set; }
        public virtual DateTime UpdatesRequiredOn  { get; set; }
    }
}
