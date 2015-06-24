using System;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Builder
{
    public class SaveResponsibilityTaskRequestBuilder
    {
        private static string _description;
        private static string _title;
        private static DateTime _completionDueDate;
        private static long _taskCategoryId;

        public static SaveResponsibilityTaskRequestBuilder Create()
        {
            var responsiblityTask = new SaveResponsibilityTaskRequestBuilder();
            _title = "Title";
            _taskCategoryId = 1;
            _completionDueDate = DateTime.Now.AddYears(1);
            _description = null;
            return responsiblityTask;
        }
        
        public SaveResponsibilityTaskRequestBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public SaveResponsibilityTaskRequestBuilder WithTaskCategory(long taskCategoryId)
        {
            _taskCategoryId = taskCategoryId;
            return this;
        }

        public SaveResponsibilityTaskRequest Build()
        {
            var saveResponsibilityTaskRequest = new SaveResponsibilityTaskRequest
            {Title = _title, Description = _description, CompletionDueDate = _completionDueDate,TaskCategoryId = _taskCategoryId};
            return saveResponsibilityTaskRequest;
        }

        public SaveResponsibilityTaskRequestBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public SaveResponsibilityTaskRequestBuilder WithCompletionDueDate(DateTime completionDueDate)
        {
            _completionDueDate = completionDueDate;
            return this;
        }
    }
}
