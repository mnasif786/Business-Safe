using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.EscalationService.Entities
{
    public class EscalationOffWorkReminder : Entity<long>
    {
        public virtual long AccidentRecordId { get; set; }
        public virtual DateTime OffWorkReminderEmailSentDate { get; set; }
    }
}

