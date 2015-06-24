using System.ComponentModel;

namespace BusinessSafe.Domain.Entities
{
    public enum TaskReoccurringType
    {
        [Description("--Select Option--")]
        None = 0,

        [Description("Weekly")]
        Weekly = 1,

        [Description("Monthly")]
        Monthly = 2,

        [Description("3 Monthly")]
        ThreeMonthly = 3,

        [Description("6 Monthly")]
        SixMonthly = 4,

        [Description("Annually")]
        Annually = 5,

        [Description("24 Monthly")]
        TwentyFourMonthly = 6,

        [Description("26 Monthly")]
        TwentySixMonthly = 7,

        [Description("3 Yearly")]
        ThreeYearly = 8,

        [Description("5 Yearly")]
        FiveYearly = 9,
    }
}