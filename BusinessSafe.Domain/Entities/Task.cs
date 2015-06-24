using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public abstract class Task : Entity<long>
    {
        public virtual Guid TaskGuid { get; set; }
        public virtual TaskCategory Category { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string Reference { get; set; }
        public virtual TaskStatus TaskStatus { get; set; }
        public virtual DerivedTaskStatusForDisplay DerivedDisplayStatus { get { return GenerateDerivedDisplayStatus(); } }

        private DerivedTaskStatusForDisplay GenerateDerivedDisplayStatus()
        {
            if (TaskStatus == TaskStatus.NoLongerRequired)
                return DerivedTaskStatusForDisplay.NoLongerRequired;

            if (TaskStatus == TaskStatus.Completed)
                return DerivedTaskStatusForDisplay.Completed;

            if (TaskCompletionDueDate.HasValue)
            {
                if (TaskCompletionDueDate.Value.Date < DateTime.Today)
                    return DerivedTaskStatusForDisplay.Overdue;
            }
            return DerivedTaskStatusForDisplay.Outstanding;
        }

        public virtual Employee TaskAssignedTo { get; set; }
        public virtual DateTime? TaskCompletionDueDate { get; set; }

        public virtual string TaskCompletedComments { get; set; }
        public virtual UserForAuditing TaskCompletedBy { get; set; }
        public virtual DateTimeOffset? TaskCompletedDate { get; set; }

        public virtual IList<TaskDocument> Documents { get; set; }

        public virtual TaskReoccurringType TaskReoccurringType { get; set; }
        public virtual DateTime? TaskReoccurringEndDate { get; set; }
        public virtual Task PrecedingTask { get; set; }
        public virtual Task FollowingTask { get; set; }
        public virtual Task OriginalTask { get; set; }

        public virtual bool? SendTaskNotification { get; set; }
        /// <summary>
        /// Send task completed to risk assessor
        /// </summary>
        public virtual bool? SendTaskCompletedNotification { get; set; }
        /// <summary>
        /// Send task overdue to risk assessor
        /// </summary>
        public virtual bool? SendTaskOverdueNotification { get; set; }
        public virtual bool? SendTaskDueTomorrowNotification { get; set; }
        public virtual SiteStructureElement Site { get; set; }
        public virtual IList<EmployeeTaskNotification> EmployeeTaskNotificationHistory { get; set; }

        protected Task()
        {
            Documents = new List<TaskDocument>();
            EmployeeTaskNotificationHistory = new List<EmployeeTaskNotification>();
        }

        public abstract void MarkAsNoLongerRequired(UserForAuditing user);

        public virtual bool HasCompletedTasks()
        {
            if (TaskReoccurringType == TaskReoccurringType.None)
            {
                return false;
            }

            if (TaskStatus == TaskStatus.Completed)
            {
                return true;
            }

            if (PrecedingTask != null)
            {
                return PrecedingTask.HasCompletedTasks();
            }

            return false;
        }

        public virtual void Complete(
            string completedComments,
            IEnumerable<CreateDocumentParameters> createDocumentParameterObjects,
            IEnumerable<long> documentLibraryIdsToRemove,
            UserForAuditing userForAuditing,
            User user)
        {
            Complete(
                completedComments,
                createDocumentParameterObjects,
                documentLibraryIdsToRemove,
                userForAuditing,
                user,
                DateTime.Now);
        }

        public virtual void Complete(
            string completedComments,
            IEnumerable<CreateDocumentParameters> createDocumentParameterObjects,
            IEnumerable<long> documentLibraryIdsToRemove,
            UserForAuditing userForAuditing,
            User user,
            DateTimeOffset completedDate)
        {
            if (TaskStatus == TaskStatus.Completed)
            {
                throw new AttemptingToCompleteTaskThatIsCompletedException(Id);
            }

            TaskStatus = TaskStatus.Completed;
            TaskCompletedComments = completedComments;
            TaskCompletedDate = completedDate;
            TaskCompletedBy = userForAuditing;
            AddDocumentsToTask(createDocumentParameterObjects, userForAuditing);
            RemoveDocumentsFromTask(documentLibraryIdsToRemove, userForAuditing);
            SetLastModifiedDetails(userForAuditing);

            if (IsReoccurring)
            {
                var completionDueDate = GetNextReoccurringCompletionDueDate(completedDate);

                if (IsNewReoccurringTaskRequired(completionDueDate))
                {
                    var newReoccurringTask = CloneForReoccurring(userForAuditing, completionDueDate);
                    FollowingTask = newReoccurringTask;
                }
            }
        }

        public virtual Task CloneForReoccurring(UserForAuditing user, DateTime completionDueDate)
        {
            var cloneForReoccurring = GetBasisForClone();
            cloneForReoccurring.Reference = Reference;
            cloneForReoccurring.Title = Title;
            cloneForReoccurring.Description = Description;
            cloneForReoccurring.TaskCompletionDueDate = completionDueDate;
            cloneForReoccurring.TaskAssignedTo = TaskAssignedTo;
            cloneForReoccurring.TaskStatus = TaskStatus.Outstanding;
            cloneForReoccurring.CreatedOn = DateTime.Now;
            cloneForReoccurring.CreatedBy = user;
            cloneForReoccurring.Category = Category;
            cloneForReoccurring.TaskReoccurringType = TaskReoccurringType;
            cloneForReoccurring.TaskReoccurringEndDate = TaskReoccurringEndDate;
            cloneForReoccurring.PrecedingTask = this;
            cloneForReoccurring.OriginalTask = OriginalTask;
            cloneForReoccurring.TaskGuid = Guid.NewGuid();
            cloneForReoccurring.SendTaskCompletedNotification = SendTaskCompletedNotification;
            cloneForReoccurring.SendTaskNotification = SendTaskNotification;
            cloneForReoccurring.SendTaskOverdueNotification = SendTaskOverdueNotification;
            cloneForReoccurring.SendTaskDueTomorrowNotification = SendTaskDueTomorrowNotification;
            cloneForReoccurring.Site = Site;

            foreach (var document in DocumentsExcludingDocumentsAddedAtTaskCompletion())
            {
                var documentToAdd = document.CloneForReoccurring(user);
                cloneForReoccurring.Documents.Add(documentToAdd);
            }

            return cloneForReoccurring;
        }

        protected abstract Task GetBasisForClone();
        public abstract RiskAssessment RiskAssessment { get; }

        protected virtual void SetValuesForCreate(
            string reference,
            string title,
            string description,
            DateTime? taskCompletionDueDate,
            TaskStatus taskStatus,
            Employee assignedTo,
            UserForAuditing user,
            IEnumerable<CreateDocumentParameters> createDocumentParameterObjects,
            TaskCategory furtherControlMeasureTaskCategory,
            int taskReoccurringTypeId,
            DateTime? taskReoccurringEndDate,
            bool sendTaskNotification,
            bool sendTaskCompletedNotification,
            bool sendTaskOverdueNotification,
            bool sendTaskDueTomorrowNotification,
            Guid taskGuid,
            Site site
            )
        {
            //todo: ptd - task category - could even access repository from domain (eek) - it will always be outstanding.

            Reference = reference;
            Title = title;
            Description = description;
            TaskCompletionDueDate =
                taskCompletionDueDate.HasValue
                    ? taskCompletionDueDate.Value
                    : (DateTime?)null;
            TaskAssignedTo = assignedTo;
            TaskStatus = taskStatus;
            CreatedOn = DateTime.Now;
            CreatedBy = user;
            Category = furtherControlMeasureTaskCategory;
            TaskReoccurringType = (TaskReoccurringType)taskReoccurringTypeId;
            TaskReoccurringEndDate = taskReoccurringEndDate;
            OriginalTask = this;
            LastModifiedBy = user;
            LastModifiedOn = DateTime.Now;
            if (createDocumentParameterObjects != null)
            {
                AddDocumentsToTask(createDocumentParameterObjects, user);
            }
            TaskGuid = taskGuid;
            SendTaskNotification = sendTaskNotification;
            Site = site;
            SendTaskCompletedNotification = sendTaskCompletedNotification;
            SendTaskOverdueNotification = sendTaskOverdueNotification;
            SendTaskDueTomorrowNotification = sendTaskDueTomorrowNotification;
        }

        public virtual void AddDocumentsToTask(IEnumerable<CreateDocumentParameters> createDocumentParameterObjects, UserForAuditing user)
        {
            foreach (var createDocumentParameters in createDocumentParameterObjects)
            {
                if (createDocumentParameters.CreatedBy == null)
                {
                    createDocumentParameters.CreatedBy = user;
                }

                if (createDocumentParameters.CreatedOn == null)
                {
                    createDocumentParameters.CreatedOn = DateTime.Now;
                }
                var document = TaskDocument.Create(createDocumentParameters, this);
                Documents.Add(document);
            }

            SetLastModifiedDetails(user);
        }

        public virtual bool IsReoccurring
        {
            get { return TaskReoccurringType != TaskReoccurringType.None; }
        }

        protected void RemoveDocumentsFromTask(IEnumerable<long> documentLibraryIdsToRemove, UserForAuditing user)
        {
            foreach (var documentLibraryIdToRemove in documentLibraryIdsToRemove)
            {
                //Documents.Remove(Documents.Single(x => x.DocumentLibraryId == documentLibraryIdToRemove));
                var taskDocument = Documents.Single(x => x.DocumentLibraryId == documentLibraryIdToRemove);
                taskDocument.Deleted = true;
                taskDocument.SetLastModifiedDetails(user);
            }
        }

        protected IEnumerable<TaskDocument> DocumentsExcludingDocumentsAddedAtTaskCompletion()
        {
            return Documents.Where(d => d.DocumentOriginType != DocumentOriginType.TaskCompleted);
        }

        protected bool IsOnGoing()
        {
            return TaskReoccurringEndDate.HasValue == false;
        }

        protected DateTime GetNextReoccurringCompletionDueDate(DateTimeOffset now)
        {
            DateTimeOffset nextReoccurringCompletionDueDate = TaskCompletionDueDate.HasValue ? TaskCompletionDueDate.Value : now;

            switch (TaskReoccurringType)
            {
                case TaskReoccurringType.Weekly:
                    nextReoccurringCompletionDueDate = nextReoccurringCompletionDueDate.AddDays(7);
                    break;
                case TaskReoccurringType.Monthly:
                    nextReoccurringCompletionDueDate = nextReoccurringCompletionDueDate.AddMonths(1);
                    break;
                case TaskReoccurringType.ThreeMonthly:
                    nextReoccurringCompletionDueDate = nextReoccurringCompletionDueDate.AddMonths(3);
                    break;
                case TaskReoccurringType.SixMonthly:
                    nextReoccurringCompletionDueDate = nextReoccurringCompletionDueDate.AddMonths(6);
                    break;
                case TaskReoccurringType.Annually:
                    nextReoccurringCompletionDueDate = nextReoccurringCompletionDueDate.AddYears(1);
                    break;
                case TaskReoccurringType.TwentyFourMonthly:
                    nextReoccurringCompletionDueDate = nextReoccurringCompletionDueDate.AddYears(2);
                    break;
                case TaskReoccurringType.TwentySixMonthly:
                    nextReoccurringCompletionDueDate = nextReoccurringCompletionDueDate.AddYears(2).AddMonths(2);
                    break;
                case TaskReoccurringType.ThreeYearly:
                    nextReoccurringCompletionDueDate = nextReoccurringCompletionDueDate.AddYears(3);
                    break;
                case TaskReoccurringType.FiveYearly:
                    nextReoccurringCompletionDueDate = nextReoccurringCompletionDueDate.AddYears(5);
                    break;
            }
            return nextReoccurringCompletionDueDate.DateTime;
        }

        protected bool IsNewReoccurringTaskRequired(DateTime completionDueDate)
        {
            return IsOnGoing() || completionDueDate <= TaskReoccurringEndDate;
        }

        public virtual ReoccurringSchedule GetReoccurringSchedule()
        {
            return new ReoccurringSchedule(this);
        }

        public virtual IEnumerable<TaskHistoryRecord> GetPreviousHistory()
        {
            return GetHistory(this.PrecedingTask);
        }

        private IEnumerable<TaskHistoryRecord> GetHistory(Task previousTask)
        {
            var result = new List<TaskHistoryRecord>();
            if (previousTask != null)
            {
                result.Add(new TaskHistoryRecord()
                {
                    CompletedDate = previousTask.TaskCompletedDate.GetValueOrDefault().ToString("dd/MM/yyyy"),
                    DueDate = previousTask.TaskCompletionDueDate.GetValueOrDefault(),
                    CompletedBy = previousTask.TaskAssignedTo.FullName
                });

                var precedingFurtherControlMeasureTask = previousTask.PrecedingTask;
                result.AddRange(GetHistory(precedingFurtherControlMeasureTask));
            }
            return result.OrderByDescending(x => x.DueDate).Take(10).OrderBy(x => x.DueDate);
        }

        public virtual Task Self
        {
            get { return this; }
        }

        public virtual void ReassignTask(Employee reassigningTaskTo, UserForAuditing user)
        {
            if (TaskStatus == TaskStatus.Completed)
            {
                throw new AttemptingToReassignFurtherActionTaskThatIsCompletedException(Id);
            }

            TaskAssignedTo = reassigningTaskTo;
            SetLastModifiedDetails(user);

        }

        public abstract bool IsTaskCompletedNotificationRequired();

        public virtual void AddEmployeeTaskNotificationHistory(Employee employee, TaskNotificationEventEnum eventType, DateTime eventDateTime, UserForAuditing user)
        {
            var employeeTaskNotification = EmployeeTaskNotification.Create(employee, this, eventType, eventDateTime, user);
            EmployeeTaskNotificationHistory.Add(employeeTaskNotification);

            //to prevent last update not set error
            if (employee.LastModifiedBy == null)
            {
                employee.SetLastModifiedDetails(user);
            }

            if (LastModifiedBy == null)
            {
                SetLastModifiedDetails(user);
            }
        }

        public virtual bool DoesEmployeeTaskNotificationExist(Employee employee, TaskNotificationEventEnum eventType)
        {
            return EmployeeTaskNotificationHistory.Any(x => x.Employee.Id == employee.Id && x.TaskEvent == eventType && x.Deleted == false);
        }
    }
}