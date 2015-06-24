using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.EscalationService.Entities
{
    public class EscalationNextReoccurringLiveTask : Entity<long>
    {
        public virtual long TaskId { get; set; }
        public virtual DateTime NextReoccurringLiveTaskEmailSentDate { get; set; }
    }
}