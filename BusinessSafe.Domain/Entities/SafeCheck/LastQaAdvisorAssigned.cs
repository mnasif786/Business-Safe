using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class LastQaAdvisorAssigned: BaseEntity<int>
    {
        public virtual QaAdvisor QaAdvisor { get; set; }
    }
}
