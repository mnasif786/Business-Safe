using System.Collections.Generic;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.ViewModels
{
    public class SpecialistSuppliersDefaultAddEdit : DefaultAddEdit
    {
        public SpecialistSuppliersDefaultAddEdit(IList<Defaults> specialistSuppliers)
        {
            FormId = "specialist-suppliers";
            Label = "Enter your specialist suppliers.";
            ColumnHeaderText = "Specialist Supplier";
            SectionHeading = "Specialist Suppliers";
            TextInputWaterMark = "enter new supplier...";
            Defaults = specialistSuppliers;
            DefaultType = "SpecialistSuppliers";
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