using System.ComponentModel;

namespace BusinessSafe.Domain.Entities
{
    public enum WorkApproach
    {
        [Description("None")]
        None = 0,

        [Description("General Ventilation")]
        GeneralVentilation = 1,

        [Description("Engineering Controls")]
        EngineeringControls = 2,

        [Description("Containment")]
        Containment = 3,

        [Description("Special")]
        Special = 4
    }
}