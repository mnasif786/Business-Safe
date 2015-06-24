using System.Collections.Generic;

namespace BusinessSafe.WebSite.Areas.Company.ViewModels
{
    public class RiskAssessorsDefaultAddEdit 
    {
        public string FormId { get; set; }
        public IList<RiskAssessorsDefaults> RiskAssessors { get; set; }
        public string Label { get; set; }
        public string ColumnHeaderText { get; set; }
        public string SectionHeading { get; set; }
        public string TextInputWaterMark { get; set; }
        public string DefaultType { get; set; }
        public string AddHeaderViewName { get; set; }
        public string EditLinkClassName { get; set; }
        public bool ShowingDeleted { get; set; }

    }
}