using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Domain.Entities
{
    public enum ActionPlanStatus
    {
        Outstanding = 1,
        Completed = 2,
        Overdue = 3,
        Archived  = 4
    }
}
