using System.Collections.Generic;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.ViewModels
{
    public class EmployeeGroupsDefaultAddEdit : DefaultAddEdit
    {
        public EmployeeGroupsDefaultAddEdit(IList<Defaults> employeeGroups)
        {
            FormId = "employee-groups";
            Label = "Enter your employee groups.";
            ColumnHeaderText = "Employee Group";
            SectionHeading = "Employee Groups";
            TextInputWaterMark = "enter new group...";
            Defaults = employeeGroups;
            DefaultType = "EmployeeGroups";
        }

        public override string FormId { get; set; }
        public override string Label { get; set; }
        public override string ColumnHeaderText { get; set; }
        public override string SectionHeading { get; set; }
        public override string TextInputWaterMark { get; set; }
        public override string DefaultType { get; set; }

        public override IList<Defaults> Defaults { get; set; }

    }
}