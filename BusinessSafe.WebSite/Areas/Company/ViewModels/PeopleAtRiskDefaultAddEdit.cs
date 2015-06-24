using System.Collections.Generic;

namespace BusinessSafe.WebSite.Areas.Company.ViewModels
{
    public class PeopleAtRiskDefaultAddEdit : DefaultAddEdit
    {
        public PeopleAtRiskDefaultAddEdit(IList<Defaults> peopleAtRisk)
        {
            FormId = "people-at-risk";
            Label = "Enter details of people at risk.";
            ColumnHeaderText = "Person at Risk";
            SectionHeading = "People at Risk";
            TextInputWaterMark = "enter new person...";
            Defaults = peopleAtRisk;
            DefaultType = "PeopleAtRisk";
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