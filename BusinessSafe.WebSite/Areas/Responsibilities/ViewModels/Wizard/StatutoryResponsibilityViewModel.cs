using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Extensions;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard
{
    public class StatutoryResponsibilityViewModel
    {
        public long Id;
        public string Title;
        public string ResponsibilityReason;
        public bool HasMultipleTasks { get { return Tasks.Count() > 1; } }
        public string Description;
        public TaskReoccurringType RecommendedFrequency
        {
            get
            {
                return Tasks!= null && Tasks.Any() 
                    ? Tasks.OrderByDescending(x => x.InitialFrequency).First().InitialFrequency 
                    : TaskReoccurringType.None;
            }
        }
        public string Category;
        public SelectList Frequencies { get { return new TaskReoccurringType().ToSelectList(RecommendedFrequency); } }

        public IEnumerable<StatutoryResponsibilityTaskViewModel> Tasks;
        public bool IsSelected { get; set; }

        public StatutoryResponsibilityViewModel()
        {
            Tasks = new List<StatutoryResponsibilityTaskViewModel>();
        }
    }

    public class ResponsibilityUncreatedTasksViewModel
    {
        public long Id;
        public string Title;
        public string Description;
        public IEnumerable<StatutoryResponsibilityTaskViewModel> StatutoryResponsibilityTasks;

        public ResponsibilityUncreatedTasksViewModel()
        {
            StatutoryResponsibilityTasks = new List<StatutoryResponsibilityTaskViewModel>();
        }
    }
}
