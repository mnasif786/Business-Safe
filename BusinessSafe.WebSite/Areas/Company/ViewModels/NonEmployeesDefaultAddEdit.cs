using System.Collections.Generic;

namespace BusinessSafe.WebSite.Areas.Company.ViewModels
{
    public class NonEmployeesDefaultAddEdit : DefaultAddEdit
    {
        public NonEmployeesDefaultAddEdit(IList<Defaults> nonEmployees)
        {
            FormId = "non-employees";
            Label = "Enter details of non-employees involved in r.a.";
            ColumnHeaderText = "Non-Employee";
            SectionHeading = "Non-Employees";
            TextInputWaterMark = "enter new non-employee...";
            Defaults = nonEmployees;
            AddHeaderViewName = "_AddingDefaultNonEmployee";
            EditLinkClassName = "editNonEmployees";
            DefaultType = "NonEmployees";
        }

        public override string FormId { get; set; }
        public override string Label { get; set; }
        public override string ColumnHeaderText { get; set; }
        public override string SectionHeading{ get; set; }
        public override string TextInputWaterMark { get; set; }
        public override string DefaultType { get; set; }

        public override IList<Defaults> Defaults { get; set; }

    }
}