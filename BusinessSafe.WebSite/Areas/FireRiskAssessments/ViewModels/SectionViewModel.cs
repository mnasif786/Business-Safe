using System.Collections.Generic;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels
{
    public class SectionViewModel
    {
        public bool Active { get; set; }
        public string ControlId { get; set; }
        public string Title { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
        public bool IsSectionValid { get; private set; }

        public SectionViewModel()
        {
            IsSectionValid = true;
        }

        public void MarkAsInvalid()
        {
            IsSectionValid = false;
        }
    }
}