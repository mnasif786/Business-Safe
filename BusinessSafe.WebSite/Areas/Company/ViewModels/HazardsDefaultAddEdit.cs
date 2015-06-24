using System.Collections.Generic;

namespace BusinessSafe.WebSite.Areas.Company.ViewModels
{
    public class HazardsDefaultAddEdit : DefaultAddEdit
    {
        public HazardsDefaultAddEdit(IList<Defaults> hazards)
        {
            FormId = "hazards";
            Label = "Enter your health and safety hazards.";
            ColumnHeaderText = "Hazard";
            SectionHeading = "Hazards";
            TextInputWaterMark = "enter new hazard...";
            Defaults = hazards;
            DefaultType = "Hazards";
            AddHeaderViewName = "_AddingDefaultHazard";
        }

        public override string FormId { get; set; }
        public override string Label { get; set; }
        public override string ColumnHeaderText { get; set; }
        public override string SectionHeading { get; set; }
        public override string TextInputWaterMark { get; set; }
        public override string DefaultType{ get; set; }
        
        public override IList<Defaults> Defaults { get; set; }
    }
}