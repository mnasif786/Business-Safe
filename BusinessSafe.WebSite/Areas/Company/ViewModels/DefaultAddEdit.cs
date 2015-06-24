using System.Collections.Generic;

namespace BusinessSafe.WebSite.Areas.Company.ViewModels
{
    public abstract class DefaultAddEdit
    {
        public abstract string FormId { get; set; }
        public abstract IList<Defaults> Defaults { get; set; }
        public abstract string Label { get; set; }
        public abstract string ColumnHeaderText { get; set; }
        public abstract string SectionHeading { get; set; }
        public abstract string TextInputWaterMark { get; set; }
        public abstract string DefaultType { get; set; }

        public string AddHeaderViewName { get; set; }
        public string EditLinkClassName { get; set; }

        protected DefaultAddEdit()
        {
            AddHeaderViewName = "_AddingDefaultStandard";
            EditLinkClassName = "edit";
        }

        
    }
}