using System;
using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;

namespace BusinessSafe.Application.Contracts.TaskList
{
    public interface ITaskService
    {
        IEnumerable<TaskCategoryDto> GetTaskCategories();
        TaskDetailsSummaryDto GetTaskDetailsSummary(TaskDetailsSummaryRequest request);
        IEnumerable<TaskDto> Search(SearchTasksRequest request);
        int Count(SearchTasksRequest searchTasksRequest);
        bool HasEmployeeGotOutstandingTasks(Guid employeeId, long companyId);
        bool HasCompletedTasks(MarkTaskAsDeletedRequest request);
        void MarkTaskAsDeleted(MarkTaskAsDeletedRequest request);
        void ReassignTask(ReassignTaskToEmployeeRequest request);
        void BulkReassignTasks(BulkReassignTasksToEmployeeRequest request);
        void MarkTaskAsNoLongerRequired(MarkTaskAsNoLongerRequiredRequest request);
        TaskListSummaryResponse GetOutstandingTasksSummary(SearchTasksRequest request);
        
    }
}