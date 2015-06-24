using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BusinessSafe.Domain.Entities
{
    public enum LengthOfTimeUnableToCarryOutWorkEnum
    {
        [Description("0 days")]
        ZeroDays = 0,

        [Description("3 or less days")]
        ThreeOrLessDays = 1,

        [Description("4 to 7 days")]
        FourToSevenDays = 2,

        [Description("More than 7 days")]
        MoreThanSevenDays = 3
    }
}
