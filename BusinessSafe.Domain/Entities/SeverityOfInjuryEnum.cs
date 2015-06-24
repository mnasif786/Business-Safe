using System.ComponentModel;

namespace BusinessSafe.Domain.Entities
{
    public enum SeverityOfInjuryEnum
    {
        [Description("Fatal")]
        Fatal = 1,

        [Description("Major")]
        Major = 2,

        [Description("Minor")]
        Minor = 3,

        [Description("No Apparent Injury")]
        NoApparentInjury = 4
    }
}
