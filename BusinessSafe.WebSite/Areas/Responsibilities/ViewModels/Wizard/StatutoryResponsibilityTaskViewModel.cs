using System.Web.Mvc;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Extensions;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard
{
    public class StatutoryResponsibilityTaskViewModel
    {
        public long Id;
        public string Title;
        public string Description;
        public TaskReoccurringType InitialFrequency;
        public SelectList Frequencies { get { return new TaskReoccurringType().ToSelectList(InitialFrequency); } }
        public bool IsCreated { get; set; }

        public string Assignee { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}