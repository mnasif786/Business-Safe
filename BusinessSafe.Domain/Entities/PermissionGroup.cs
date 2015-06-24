using System.ComponentModel;

namespace BusinessSafe.Domain.Entities
{
    public enum PermissionGroup
    {
        [Description("My Company Profile")]
        Profile = 1,

        [Description("My Employees")]
        Employees = 2,

        [Description("My BusinessSafe - Risk Assessments")]
        RiskAssessments = 3,

        [Description("My BusinessSafe - Miscellaneous")]
        Miscellaneous = 4,

        [Description("My Documentation")]
        MyDocumentation = 5,

        [Description("My Responsibilities")]
        MyResponsibilities = 6,

        [Description("My Reports")]
        MyReports = 7
    }
}