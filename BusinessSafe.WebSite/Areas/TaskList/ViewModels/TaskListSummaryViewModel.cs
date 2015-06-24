namespace BusinessSafe.WebSite.Areas.TaskList.ViewModels
{
    public class TaskListSummaryViewModel
    {
        public int TotalOverdueTasks { get; set; }
        public int TotalPendingTasks { get; set; }

        public int TotalTasks
        {
            get { return TotalOverdueTasks + TotalPendingTasks; }
            
        }
    }
}