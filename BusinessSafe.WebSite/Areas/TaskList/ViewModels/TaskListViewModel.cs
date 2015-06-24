using System.Collections.Generic;
using System;
using System.Web;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.TaskList.ViewModels
{
    public class TaskListViewModel
    {
        public string Title { get; set; }
        public long CompanyId { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public DateTime? CompletedFrom { get; set; }
        public DateTime? CompletedTo { get; set; }
        public int TaskCategoryId { get; set; }
        public int TaskStatusId { get; set; }
        public bool IsBulkReassign { get; set; }
        public IEnumerable<AutoCompleteViewModel> TaskCategories { get; set; }
        public IEnumerable<TaskViewModel> Tasks { get; set; }
        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }
        public IEnumerable<AutoCompleteViewModel> SiteGroups { get; set; }
        public IEnumerable<AutoCompleteViewModel> Employees { get; set; }
        public long? SiteId { get; set; }
        public long? SiteGroupId { get; set; }
        public bool IsShowDeleted { get; set; }
        public bool IsShowCompleted { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    
        public HtmlString GetAdditionalTitle()
        {
            if (IsShowDeleted)
            {
                return new HtmlString("<strong>Deleted</strong>");
            }

            if(IsShowCompleted)
            {
                return new HtmlString("<strong>Completed</strong>");
            }

            if(IsBulkReassign)
            {
                return new HtmlString("<strong>Bulk Reassign</strong>");
            }

            return new HtmlString("<strong>Outstanding</strong>");
        }

        public bool IsShowOutsandingVisible()
        {
            if (IsShowDeleted || IsShowCompleted || IsBulkReassign)
            {
                return true;
            }

            return false;
        }
    }
}