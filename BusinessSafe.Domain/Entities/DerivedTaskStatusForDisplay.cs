using System.ComponentModel;

namespace BusinessSafe.Domain.Entities
{
    public enum DerivedTaskStatusForDisplay
    {
        [Description("Outstanding")]
        Outstanding = 0,

        [Description("Completed")]
        Completed = 1,

        [Description("No Longer Required")]
        NoLongerRequired = 2,

        [Description("Overdue")]
        Overdue = 3,

        [Description("")]
        None = 4
    }
}