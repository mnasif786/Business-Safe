using System.ComponentModel;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public enum SummaryReportHeaderType
    {
        [Description("None")]
        None = 0,

        [Description("GB")]
        GB = 1,

        [Description("ROI")]
        ROI = 2,

        [Description("NI")]
        NI = 3
    }
}