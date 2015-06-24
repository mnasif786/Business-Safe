﻿using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.EscalationService.Entities
{
    public class EscalationOverdueTask: Entity<long>
    {
        public virtual  long TaskId { get; set; }
        public virtual DateTime OverdueEmailSentDate { get; set; }
    }
}
