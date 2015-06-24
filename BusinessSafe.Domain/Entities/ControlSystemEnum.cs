using System.ComponentModel;

namespace BusinessSafe.Domain.Entities
{
    public enum ControlSystemEnum
    {
        [Description("None")]
        None = 0,

        [Description("Control System 1")]
        ControlSystem1 = 1,

        [Description("Control System 2")]
        ControlSystem2 = 2,

        [Description("Control System 3")]
        ControlSystem3 = 3,

        [Description("Control System 4")]
        ControlSystem4 = 4
    }
}