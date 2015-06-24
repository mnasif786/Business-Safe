using System.ComponentModel;

namespace BusinessSafe.Domain.Entities
{
    public enum PersonInvolvedEnum
    {
        [Description("Employee")]
        Employee = 1,

        [Description("Visitor")]
        Visitor = 2,

        [Description("Person at work (not employee)")]
        PersonAtWork = 3,

        [Description("Other")]
        Other = 4
    }
}
