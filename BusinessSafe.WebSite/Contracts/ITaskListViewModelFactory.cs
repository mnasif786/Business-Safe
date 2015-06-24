using System;
using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Contracts
{
    public interface ITaskListViewModelFactory : IViewModelFactory<TaskListViewModel>
    {
        ITaskListViewModelFactory WithSiteId(long? siteId);
        ITaskListViewModelFactory WithSiteGroupId(long? siteGroupId);
        ITaskListViewModelFactory WithCompanyId(long companyId);
        ITaskListViewModelFactory WithEmployeeId(Guid? employeeId);
        ITaskListViewModelFactory WithCreatedFrom(string createdFrom);
        ITaskListViewModelFactory WithCreatedTo(string createdTo);
        ITaskListViewModelFactory WithCompletedFrom(string completedFrom);
        ITaskListViewModelFactory WithCompletedTo(string completedTo);
        ITaskListViewModelFactory WithTaskCategoryId(long taskCategoryId);
        ITaskListViewModelFactory WithTaskStatusId(int taskStatusId);
        ITaskListViewModelFactory WithUser(CustomPrincipal currentUser);
        ITaskListViewModelFactory WithShowDeleted(bool showDeleted);
        ITaskListViewModelFactory WithBulkReassignMode(bool isBulkReassign);
        ITaskListViewModelFactory WithShowCompleted(bool showCompleted);
        ITaskListViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds);
        ITaskListViewModelFactory WithUserEmployeeId(CustomPrincipal currentUser);
        ITaskListViewModelFactory WithTitle(string title);
        ITaskListViewModelFactory WithPage(int page);
        ITaskListViewModelFactory WithPageSize(int pageSize);
        ITaskListViewModelFactory WithOrderBy(string orderBy);
        TaskListSummaryViewModel GetSummaryViewModel();
    }
}