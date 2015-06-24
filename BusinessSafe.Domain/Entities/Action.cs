using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.ParameterClasses.SafeCheck;

namespace BusinessSafe.Domain.Entities
{   
    public class Action : Entity<long>
    {
        public virtual long ActionPlanId { get; set; }
        public virtual ActionPlan ActionPlan { get; set; }      
        public virtual string Title { get; set; }
        public virtual string Reference { get; set; }      
        public virtual string AreaOfNonCompliance { get; set; }
        public virtual string ActionRequired { get; set; }
        public virtual string GuidanceNotes { get; set; }
        public virtual string TargetTimescale { get; set; }
        public virtual Employee AssignedTo { get; set; }
        public virtual DateTime? DueDate { get; set; }
        public virtual ActionQuestionStatus QuestionStatus { get; set; }
        public virtual ActionCategory Category { get; set; }
        public virtual IList<ActionTask> ActionTasks { get; set; }
        public virtual bool NoLongerRequired { get; set; }


        public Action()
        {
            ActionTasks = new List<ActionTask>();
        }

        public static Action Create(CreateUpdateActionParameters parameters)
        {
            var action = new Action();

            action.AreaOfNonCompliance = parameters.AreaOfNonCompliance;
            action.GuidanceNotes = parameters.GuidanceNotes;
            action.Reference = parameters.Reference;
            action.ActionRequired = parameters.ActionRequired;
            action.AssignedTo = parameters.AssignedTo;
            action.Category = parameters.Category;
            action.TargetTimescale = (parameters.TimeScale != null) ? parameters.TimeScale.Name : null;
            action.CreatedBy = parameters.CreatedBy;
            action.CreatedOn = parameters.CreatedOn;
            action.LastModifiedBy = parameters.CreatedBy; //parameters.LastModifiedBy;
            action.LastModifiedOn = parameters.CreatedOn; //parameters.LastModifiedOn;
            action.Title = parameters.Title;
           
            return action;
        }

        public virtual void SetLastModifiedBy(UserForAuditing user)
        {
            LastModifiedBy = user;
            LastModifiedOn = DateTime.Now;
        }

        public virtual void AddTask(ActionTask task)
        {
            ActionTasks.Add(task);
        }

        public virtual void SetDueDate(Timescale targetTimeScale)
        {
            DueDate = (targetTimeScale == null || targetTimeScale.Id == (long) TargetTimeScales.None)
                ? null
                : (targetTimeScale.Id == (long) TargetTimeScales.OneMonth)
                    ? DateTime.Now.AddMonths(1)
                    : (targetTimeScale.Id == (long) TargetTimeScales.ThreeMonths)
                        ? DateTime.Now.AddMonths(3)
                        : (targetTimeScale.Id == (long) TargetTimeScales.SixMonths)
                            ? DateTime.Now.AddMonths(6)
                            : (targetTimeScale.Id == (long) TargetTimeScales.SixWeeks) ? (DateTime?)DateTime.Now.AddDays(6*7):
                                (DateTime?) DateTime.Now;
        }

        public virtual DerivedTaskStatusForDisplay GetStatusFromTasks()
        {
   
            if (ActionTasks.Any(x =>
                                x.Deleted == false &&
                                x.TaskStatus == TaskStatus.Outstanding &&
                                (x.TaskCompletionDueDate.HasValue &&
                                 x.TaskCompletionDueDate.GetValueOrDefault().Date < DateTime.Today &&
                                 x.TaskCompletionDueDate != null)))
            {
                return DerivedTaskStatusForDisplay.Overdue;
            }

            if (ActionTasks.Any(x =>
                                x.Deleted == false &&
                                x.TaskStatus == TaskStatus.Outstanding &&
                                (x.TaskCompletionDueDate.HasValue &&
                                 x.TaskCompletionDueDate.GetValueOrDefault().Date >= DateTime.Today)))
            {
                return DerivedTaskStatusForDisplay.Outstanding;
            }
           

            if (ActionTasks.Any() && ActionTasks.Any(x =>
                                                     x.Deleted == false &&                                                     
                                                      x.TaskStatus == TaskStatus.Completed))
            {
                return DerivedTaskStatusForDisplay.Completed;
            }

            if (NoLongerRequired || 
                ActionTasks.Any() && ActionTasks.Any(x =>
                                                    x.Deleted == false &&
                                                    x.TaskStatus == TaskStatus.NoLongerRequired) )
            {
                return DerivedTaskStatusForDisplay.NoLongerRequired;
            }
            

            return DerivedTaskStatusForDisplay.None;
        }
    }
}