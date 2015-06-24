using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class TaskNotFoundException : ArgumentNullException
    {
        public TaskNotFoundException(long taskId)
            : base(string.Format("Task Not Found. Task not found for task id {0}", taskId))
        {
        }
    }
}