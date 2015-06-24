using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.EscalationService.Entities
{
    public class EscalationTaskDueTomorrow : Entity<long>
    {
        public virtual long TaskId { get; set; }
        public virtual DateTime TaskDueTomorrowEmailSentDate { get; set; }
    }
}
