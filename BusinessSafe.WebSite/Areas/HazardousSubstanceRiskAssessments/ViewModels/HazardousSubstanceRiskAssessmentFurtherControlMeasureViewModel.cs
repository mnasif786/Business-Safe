using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels
{
    public class HazardousSubstanceRiskAssessmentFurtherControlMeasureViewModel
    {
        public string ActionTitle;
        public string ActionDescription { get; set; }
        public string AssignedTo { get; set; }
        public string TaskCompletionDueDate { get; set; }
        public string TaskStatus { get; set; }
        public long Id { get; set; }
        public TaskReoccurringType TaskReoccurringType { get; set; }
        public DateTime? TaskReoccurringEndDate { get; set; }
        public bool IsReoccurring { get { return TaskReoccurringType > 0; } }
        public string CreatedOn { get; set; }
        public Guid TaskGuid { get; set; }

        public string GetReoccurringFrequencyDetails()
        {
            if (IsReoccurring)
            {
                return TaskReoccurringTypeHelper.GetReoccurringFrequencyDetails(TaskReoccurringType, TaskReoccurringEndDate);
            }
            return string.Empty;
        }

        public static IEnumerable<HazardousSubstanceRiskAssessmentFurtherControlMeasureViewModel> CreateFrom(IEnumerable<TaskDto> furtherControlMeasureTasks)
        {
            return furtherControlMeasureTasks.Select(CreateFrom);
        }

        private static HazardousSubstanceRiskAssessmentFurtherControlMeasureViewModel CreateFrom(TaskDto furtherControlMeasureTask)
        {
            return new HazardousSubstanceRiskAssessmentFurtherControlMeasureViewModel()
                       {
                           Id = furtherControlMeasureTask.Id,
                           ActionTitle = furtherControlMeasureTask.Title,
                           ActionDescription = furtherControlMeasureTask.Description,
                           AssignedTo =
                               furtherControlMeasureTask.TaskAssignedTo != null
                                   ? furtherControlMeasureTask.TaskAssignedTo.FullName
                                   : string.Empty,
                           TaskCompletionDueDate = furtherControlMeasureTask.TaskCompletionDueDate,
                           TaskStatus = EnumHelper.GetEnumDescription(furtherControlMeasureTask.DerivedDisplayStatus),
                           TaskReoccurringType = furtherControlMeasureTask.TaskReoccurringType,
                           TaskReoccurringEndDate = furtherControlMeasureTask.TaskReoccurringEndDate,
                           CreatedOn = furtherControlMeasureTask.CreatedDate,
                           TaskGuid = furtherControlMeasureTask.TaskGuid
                       };
        }
    }
}