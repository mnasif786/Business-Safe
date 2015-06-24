using System;
using System.Collections.Generic;
using System.Web;

namespace BusinessSafe.Checklists.ViewModels
{
    public class EmployeeChecklistViewModel
    {
        public Guid EmployeeChecklistId { get; set; }
        public string ChecklistTitle { get; set; }
        public HtmlString ChecklistDescription { get; set; }
        public List<SectionViewModel> Sections { get; set; }
        public bool IsCompleted { get; set; }
	    public string FriendlyReference { get; set; }
        public bool CompletedOnEmployeesBehalf { get; set; }
    }
}
