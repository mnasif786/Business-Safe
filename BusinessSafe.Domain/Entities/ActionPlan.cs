using System;
using System.Linq;
using BusinessSafe.Domain.Common;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.ParameterClasses.SafeCheck;


namespace BusinessSafe.Domain.Entities
{
    public class ActionPlan : Entity<long>
    {
        public virtual long CompanyId { get; set; }
        public virtual string Title { get; set; }
        public virtual Site Site { get; set; }
        public virtual DateTime? DateOfVisit { get; set; }
        public virtual string VisitBy { get; set; }
        public virtual DateTime SubmittedOn { get; set; }
        public virtual string AreasVisited { get; set; }
        public virtual string AreasNotVisited { get; set; }
        public virtual SafeCheck.Checklist Checklist { get; set; }

        public virtual long? ExecutiveSummaryDocumentLibraryId { get; set; }

        public virtual IList<Action> Actions { get; set; }

        public virtual bool NoLongerRequired { get; set; }
        
        public static ActionPlan Create(SafeCheck.Checklist checklist, string title, Site site)
        {
            var actionPlan = new ActionPlan();
            actionPlan.NoLongerRequired = false;
            actionPlan.Checklist = checklist;

            //todo: can we just refactor these out so that it references the checklist? will these values need to change if checkilist changes?
            actionPlan.CompanyId = checklist.ClientId.Value;
            actionPlan.DateOfVisit = checklist.VisitDate;
            actionPlan.AreasVisited = checklist.AreasVisited;
            actionPlan.AreasNotVisited = checklist.AreasNotVisited;
            actionPlan.Title = title;
            actionPlan.CreatedBy = checklist.CreatedBy;
            actionPlan.CreatedOn = checklist.CreatedOn;
            actionPlan.Site = site;
            actionPlan.SubmittedOn = DateTime.Now;
            actionPlan.VisitBy = checklist.ChecklistCompletedBy; //.VisitBy;
            actionPlan.LastModifiedBy = checklist.CreatedBy;
            actionPlan.LastModifiedOn = checklist.CreatedOn;
            return actionPlan;
        }

        public virtual void CreateActions(List<ChecklistAnswer> checklistAnswers)
        {
            Actions  = Actions ?? new List<Action>();
            foreach (var checklistAnswer in checklistAnswers)
            {
                if (checklistAnswer.Response != null && 
                    (checklistAnswer.Response.Title.ToUpper() == ResponseType.ImprovementRequired.ToDescription().ToUpper()
                    || checklistAnswer.Response.Title.ToUpper() == ResponseType.Unacceptable.ToDescription().ToUpper()))
                {
                    var actionparameters = new CreateUpdateActionParameters()
                    {
                        Category = ActionCategory.Action,
                        ActionRequired = checklistAnswer.ActionRequired,
                        GuidanceNotes = checklistAnswer.GuidanceNotes,
                        TimeScale = checklistAnswer.Timescale,
                        AssignedTo = checklistAnswer.AssignedTo,
                        DueDate = DateTime.Now.Date,
                        CreatedOn = CreatedOn,
                        CreatedBy = CreatedBy,
                        AreaOfNonCompliance = checklistAnswer.AreaOfNonCompliance,
                        LastModifiedBy = LastModifiedBy,
                        LastModifiedOn = LastModifiedOn,
                        
                        // Status = checklistAnswer.Response.
                    };

                    var action = Action.Create(actionparameters);
                    action.SetDueDate(checklistAnswer.Timescale);
                    Actions.Add(action);
                }
            }
        }

        public virtual void CreateImmediateRiskNotifications()
        {
            Actions = Actions ?? new List<Action>();
            foreach (var notifications in Checklist.ImmediateRiskNotifications)
            {
                var actionparameters = new CreateUpdateActionParameters()
                {
                    Category = ActionCategory.ImmediateRiskNotification,
                    ActionRequired = notifications.RecommendedImmediateAction,
                    Reference = notifications.Reference,
                    Title = notifications.Title,
                    AssignedTo = Checklist.MainPersonSeen ?? null,
                    DueDate = DateTime.Now.Date,
                    CreatedOn = CreatedOn,
                    CreatedBy = CreatedBy,
                    LastModifiedBy = LastModifiedBy,
                    LastModifiedOn = LastModifiedOn,
                    AreaOfNonCompliance = notifications.SignificantHazardIdentified,
                    // Status = checklistAnswer.Response
                };
                var action = Action.Create(actionparameters);
                Actions.Add(action);

            }
        }

        public virtual DerivedTaskStatusForDisplay GetStatusFromActions()
        {
            if(NoLongerRequired)
                return DerivedTaskStatusForDisplay.NoLongerRequired;

            if (Actions != null)
            {
                if (Actions.Any(x =>
                                x.Deleted == false &&
                                x.GetStatusFromTasks() == DerivedTaskStatusForDisplay.Overdue))
                {
                    return DerivedTaskStatusForDisplay.Overdue;
                }

                if (Actions.Any(x =>
                                x.Deleted == false &&
                                x.GetStatusFromTasks() == DerivedTaskStatusForDisplay.Outstanding))
                {
                    return DerivedTaskStatusForDisplay.Outstanding;
                }

                if (Actions.Any() && Actions.Any(x =>
                                                 x.Deleted == false &&
                                                 (x.GetStatusFromTasks() == DerivedTaskStatusForDisplay.Completed)))
                {
                    return DerivedTaskStatusForDisplay.Completed;
                }

                if (Actions.Any() && Actions.Any(x =>
                                                x.Deleted == false &&
                                                (x.GetStatusFromTasks() == DerivedTaskStatusForDisplay.NoLongerRequired)))
                {
                    return DerivedTaskStatusForDisplay.NoLongerRequired;
                }

                if (Actions.Any() == false && NoLongerRequired)
                    return DerivedTaskStatusForDisplay.NoLongerRequired;
            }
            else
            {
                if (NoLongerRequired)
                    return DerivedTaskStatusForDisplay.NoLongerRequired;
            }

            return DerivedTaskStatusForDisplay.None;
        }

        public virtual bool HasAnyActionAssigned()
        {
            return Actions.Any(a => a.AssignedTo != null && a.ActionTasks.Any());
        }
    }
}
