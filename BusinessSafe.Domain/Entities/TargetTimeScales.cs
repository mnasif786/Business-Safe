using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Domain.Entities
{
    public enum TargetTimeScales
    {
        None = 0,
        OneMonth = 1,
        ThreeMonths = 2,
        SixMonths = 3,
        UrgentActionRequired = 4,
        SixWeeks = 5
    }
}
