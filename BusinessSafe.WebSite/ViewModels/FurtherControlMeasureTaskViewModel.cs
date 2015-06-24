using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.ViewModels
{
    public class FurtherControlMeasureTaskViewModel
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
            if(IsReoccurring)
            {
                return TaskReoccurringTypeHelper.GetReoccurringFrequencyDetails(TaskReoccurringType, TaskReoccurringEndDate);    
            }
            return string.Empty;
        }

        public static IEnumerable<FurtherControlMeasureTaskViewModel> CreateFrom(IEnumerable<TaskDto> furtherControlMeasureTasks)
        {
            if (furtherControlMeasureTasks == null)
            {
                return  new List<FurtherControlMeasureTaskViewModel>();
            }
            return furtherControlMeasureTasks.Select(CreateFrom);
        }

        private static FurtherControlMeasureTaskViewModel CreateFrom(TaskDto furtherControlMeasureTask)
        {
            return new FurtherControlMeasureTaskViewModel()
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