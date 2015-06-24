using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Extensions;

namespace BusinessSafe.WebSite.Areas.TaskList.ViewModels
{
    public class TaskViewModel
    {
        const string TaskcategoryGeneral = "General";

        public long Id { get; set; }
        public string Reference { get; set; }
        public string Title { get; set; }
        public string TaskCategory { get; set; }
        public string Description { get; set; }
        public string TaskAssignedTo { get; set; }
        public string TaskStatus { get; set; }
        public string CreatedDate { get; set; }
        public string TaskCompletionDueDate { get; set; }
        public string CompletedBy { get; set; }
        public string CompletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public TaskReoccurringType TaskReoccurringType { get; set; }
        public DateTime? TaskReoccurringEndDate { get; set; }
        public bool IsReoccurring { get; set; }
        public bool HasDocuments { get; set; }
        public bool HasCompletedDocuments { get; set; }
        public string TaskType;
        public string DerivedDisplayStatus { get; set; }
        public long RiskAssessmentId { get; set; }
        public Guid TaskGuid { get; set; }
        public static IEnumerable<TaskViewModel> CreateFrom(IEnumerable<TaskDto> tasks)
        {
            if (tasks == null || !tasks.Any())
                return new TaskViewModel[] { };

            return tasks.Select(CreateFrom).ToList();
        }

        private static TaskViewModel CreateFrom(TaskDto task)
        {
            var taskViewModel = new TaskViewModel
                                    {
                                        Id = task.Id,
                                        Reference = task.Reference,
                                        Title = task.Title,
                                        Description = task.Description,
                                        TaskAssignedTo = task.TaskAssignedTo.FullName,
                                        TaskCategory = task.TaskCategory.Category,
                                        IsDeleted = task.Deleted,
                                        IsReoccurring = task.IsReoccurring,
                                        TaskStatus = task.TaskStatusString.AddSpacesToName(),
                                        DerivedDisplayStatus = EnumHelper.GetEnumDescription(task.DerivedDisplayStatus),
                                        TaskReoccurringType = task.TaskReoccurringType,
                                        TaskReoccurringEndDate = task.TaskReoccurringEndDate,
                                        CreatedDate = task.CreatedDate,
                                        TaskCompletionDueDate = task.TaskCompletionDueDate,
                                        HasDocuments = task.Documents.Any(),
                                        HasCompletedDocuments = task.Documents.Any(d => d.DocumentOriginType == DocumentOriginType.TaskCompleted),
                                        TaskType = task.GetType().Name,
                                        TaskGuid = task.TaskGuid,
                                        CompletedBy = task.TaskCompletedBy != null ? task.TaskCompletedBy.FullName : string.Empty,
                                        CompletedOn = task.TaskCompletedDate.HasValue ? task.TaskCompletedDate.Value.ToLocalShortDateString() : string.Empty
                                    };

            if (taskViewModel.IsReviewTask())
            {
                var riskAssessmentReviewTaskDto = task as RiskAssessmentReviewTaskDto;
                
                taskViewModel.RiskAssessmentId = riskAssessmentReviewTaskDto != null && riskAssessmentReviewTaskDto.RiskAssessment != null
                                                     ? riskAssessmentReviewTaskDto.RiskAssessment.Id
                                                     : default(long);

                taskViewModel.Title = riskAssessmentReviewTaskDto.RiskAssessment.Title;
                taskViewModel.Reference = riskAssessmentReviewTaskDto.RiskAssessment.Reference;

            }

            return taskViewModel;
        }

        public string GetStatusIcons
        {
            get
            {
                string icons = string.Empty;

                if (IsReoccurring)
                    icons = string.Format("<span class='label label-important label-reoccurring-task' title='{0}' rel='tooltip'>R</span>&nbsp;", GetReoccurringFrequencyDetails());

                if (HasDocuments)
                    icons += string.Format("<i class='icon-tags'></i>");

                return icons;
            }
        }

        

        private string GetReoccurringFrequencyDetails()
        {
            return TaskReoccurringTypeHelper.GetReoccurringFrequencyDetails(TaskReoccurringType, TaskReoccurringEndDate, false);
        }

        public string GetRiskAssessmentArea()
        {
            if (TaskCategory.Contains(TaskcategoryGeneral))
                return "GeneralRiskAssessments";
            
            if (TaskCategory.Contains("Fire"))
                return "FireRiskAssessments";

            if (TaskCategory.Contains("Hazardous"))
                return "HazardousSubstanceRiskAssessments";

            if (TaskCategory.Contains("Personal"))
                return "PersonalRiskAssessments";

            throw new SystemException("Area not defined for task");
        }


        public string GetRiskAssessmentControllerForViewAction()
        {
            var result = "PremisesInformation";

            if (TaskCategory.Contains("Hazardous"))
                result =  "Description";

            return result;
        }


        public bool HasPermission(IPrincipal user)
        {
            if (TaskCategory.Contains("General") || TaskCategory.Contains("Hazardous"))
                return user.IsInRole(Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString());

            if (TaskCategory.Contains("Fire"))
                return user.IsInRole(Permissions.EditFireRiskAssessments.ToString());

            if (TaskCategory.Contains("Personal"))
                return user.IsInRole(Permissions.EditPersonalRiskAssessments.ToString());

            return true;
        }

        public bool IsPrintAvailable()
        {
            return !IsReviewTask(); 
        }

        public bool IsReassignEnabled(IPrincipal user)
        {
            return IsEditableTask(user) && IsReassignAvailable();
        }

        private bool IsReassignAvailable()
        {
            return !IsReviewTask(); 
        }

        public bool IsReviewTask()
        {
            return TaskType == "RiskAssessmentReviewTaskDto";
        }

        public bool IsResponsibilityTask()
        {
            return TaskType == "ResponsibilityTaskDto";
        }

        public bool IsCompleteEnabled(IPrincipal user)
        {
            return user.IsInRole(Permissions.ViewRiskAssessmentTasks.ToString()) &&
                   TaskStatus == "Outstanding" &&
                   IsDeleted == false;
        }

        private bool IsEditableTask(IPrincipal user)
        {
            return user.IsInRole(Permissions.EditRiskAssessmentTasks.ToString()) &&
                   TaskStatus == "Outstanding" &&
                   IsDeleted == false;
        }

        public bool IsDeleteEnabled(IPrincipal user)
        {
            return user.IsInRole(Permissions.DeleteRiskAssessmentTasks.ToString()) && CanDelete();
        }

        private bool CanDelete()
        {
            if(IsDeleted)
            {
                return false;
            }

            if (IsCompleted())
            {
                return false;
            }
                
            if(IsReviewTask())
            {
                return false;
            }

            return true;
        }

        private bool IsCompleted()
        {
            return TaskStatus == "Completed";
        }

        public string GetTaskType()
        {
            if (TaskCategory == "Hazardous Substance Risk Assessment")
                return "hsra";

            if (TaskCategory == "General Risk Assessment")
                return "gra";

            if (TaskCategory == "Personal")
                return "pra";

            if (TaskCategory == "Fire Risk Assessment")
                return "fra";

            if (TaskCategory == "Responsibility")
                return "responsibility";
            
            if (TaskCategory == "Action")
                return "action";

            throw new SystemException("Task Type not defined for task");
        }

        public bool IsViewAvailable()
        {
            if(IsCompleted() || IsDeleted)
            {
                return true;
            }

            return false;
        }

        public int GetIsReviewTask()
        {
            return Convert.ToInt32(IsReviewTask());
        }
    }
}