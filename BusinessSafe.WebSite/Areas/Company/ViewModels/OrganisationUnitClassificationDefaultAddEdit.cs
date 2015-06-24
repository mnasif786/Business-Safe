using System.Collections.Generic;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.ViewModels
{
    public class OrganisationUnitClassificationDefaultAddEdit : DefaultAddEdit
    {
        public OrganisationUnitClassificationDefaultAddEdit(IList<Defaults> organisationUnitClassifications)
        {
            FormId = "organisational-unit-classification";
            Label = "Enter your organisation unit classifications.";
            ColumnHeaderText = "Classification";
            SectionHeading = "Organisational Unit Classification";
            TextInputWaterMark = "enter new classification...";
            Defaults = organisationUnitClassifications;
            DefaultType = "OrganisationalUnit";
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