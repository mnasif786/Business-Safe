using System.Collections.Generic;

namespace BusinessSafe.Checklists.ViewModels
{
    public class SectionViewModel
    {
        public string Title { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}