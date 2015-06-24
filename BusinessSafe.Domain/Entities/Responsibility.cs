using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class Responsibility : Entity<long>
    {
        public virtual long CompanyId { get; set; }
        public virtual ResponsibilityCategory ResponsibilityCategory { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual SiteStructureElement Site { get; set; }
        public virtual ResponsibilityReason ResponsibilityReason { get; set; }
        public virtual Employee Owner { get; set; }
        public virtual TaskReoccurringType InitialTaskReoccurringType { get; set; }
        public virtual IList<ResponsibilityTask> ResponsibilityTasks { get; set; }
        public virtual event EventHandler HasMultipleFrequencyChangeToTrue;
        public virtual StatutoryResponsibilityTemplate StatutoryResponsibilityTemplateCreatedFrom { get; set; }

        protected void OnHasMultipleFrequencyChangeToTrue(EventArgs e)
        {
            EventHandler handler = HasMultipleFrequencyChangeToTrue;
            if (handler != null) handler(this, e);
        }

        public Responsibility()
        {
            ResponsibilityTasks = new List<ResponsibilityTask>();
        }

        public virtual DateTime? NextDueDate
        {
            get 
            { 
                return ResponsibilityTasks
                    .Where(responsibilityTask => responsibilityTask.TaskStatus == TaskStatus.Outstanding && !responsibilityTask.Deleted)
                    .Min(responsibilityTask => responsibilityTask.TaskCompletionDueDate); 
            }
        }

        public virtual bool HasMultipleFrequencies
        {
            get
            {
                return ResponsibilityTasks
                    .Where(r=>r.TaskStatus!=TaskStatus.Completed && r.TaskStatus != TaskStatus.NoLongerRequired)
                           .Select(responsibilityTask => responsibilityTask.TaskReoccurringType)
                           .Distinct()
                           .Count() > 1;
            }
        }

        public static Responsibility Create(
            long companyId, 
            ResponsibilityCategory category, 
            string title, 
            string description, 
            Site site, 
            ResponsibilityReason reason, 
            Employee owner, 
            TaskReoccurringType frequency,
            StatutoryResponsibilityTemplate statutoryResponsibilityTemplate,
            UserForAuditing user)
        {
            var responsibility = new Responsibility
                                     {
                                         CompanyId = companyId,
                                         ResponsibilityCategory = category,
                                         Title = title,
                                         Description =  description,
                                         Site = site,
                                         ResponsibilityReason = reason,
                                         Owner = owner,
                                         InitialTaskReoccurringType = frequency,
                                         CreatedBy = user,
                                         CreatedOn = DateTime.Now,
                                         StatutoryResponsibilityTemplateCreatedFrom = statutoryResponsibilityTemplate
                                     };
            return responsibility;
        }

        public virtual Responsibility CopyWithoutSiteAndOwner(string newTitle, UserForAuditing creatingUser)
        {
            var newResponsibility = Responsibility.Create(
                  CompanyId,
                  ResponsibilityCategory,
                  newTitle,
                  Description,
                  null,
                  ResponsibilityReason,
                  null,
                  InitialTaskReoccurringType,
                  StatutoryResponsibilityTemplateCreatedFrom,
                  creatingUser);

            var responsibilityTasksWithoutCompleted = ResponsibilityTasks.Where(x => x.TaskStatus == TaskStatus.Outstanding);
            foreach (var resp in responsibilityTasksWithoutCompleted)
            {
                ResponsibilityTask.Create(resp.Title,
                    resp.Description,
                    null,
                    TaskStatus.Outstanding, 
                    null,
                    creatingUser,
                    new List<CreateDocumentParameters>(),
                    resp.Category,
                    (int)resp.TaskReoccurringType,
                    resp.TaskReoccurringEndDate,
                    resp.SendTaskNotification.HasValue && resp.SendTaskNotification.Value,
                    resp.SendTaskCompletedNotification.HasValue && resp.SendTaskCompletedNotification.Value,
                    resp.SendTaskOverdueNotification.HasValue && resp.SendTaskOverdueNotification.Value,
                    resp.SendTaskDueTomorrowNotification.HasValue && resp.SendTaskDueTomorrowNotification.Value,
                    resp.TaskGuid,
                    null,
                    newResponsibility
                    );
            }

            return newResponsibility;
        }

        public virtual void Update(long companyId,
            ResponsibilityCategory category,
            string title,
            string description,
            Site site,
            ResponsibilityReason reason,
            Employee owner,
            TaskReoccurringType taskReoccurringType,
            UserForAuditing user)
        {
            ResponsibilityCategory = category;
            Title = title;
            Description = description;
            Site = site;
            ResponsibilityReason = reason;
            Owner = owner;
            InitialTaskReoccurringType = taskReoccurringType;
            LastModifiedBy = user;
            LastModifiedOn = DateTime.Now;
        }

        public virtual void SetLastModifiedBy(UserForAuditing user)
        {
            LastModifiedBy = user;
            LastModifiedOn = DateTime.Now;
        }

        public virtual bool HasUndeletedTasks()
        {
            return ResponsibilityTasks.Any(x => x.Deleted == false); // can not delete if any tasks have not been deleted
        }

        public virtual DerivedTaskStatusForDisplay GetStatusDerivedFromTasks()
        {            
            if (ResponsibilityTasks.Any(x =>
                x.Deleted == false &&
                x.TaskStatus == TaskStatus.Outstanding &&
                (x.TaskCompletionDueDate.HasValue && x.TaskCompletionDueDate.GetValueOrDefault().Date < DateTime.Today &&
                // extra check for copied responsibilities, which haven't yet a completion date assigned
                 x.TaskCompletionDueDate != null)))
            {
                return DerivedTaskStatusForDisplay.Overdue;
            }

            if (ResponsibilityTasks.Any(x =>
              x.Deleted == false &&
              x.TaskStatus == TaskStatus.Outstanding &&
              ( x.TaskCompletionDueDate.HasValue && x.TaskCompletionDueDate.GetValueOrDefault().Date >= DateTime.Today)))
            {
                return DerivedTaskStatusForDisplay.Outstanding;
            }
           
            if (ResponsibilityTasks.Any() && ResponsibilityTasks.Any(x =>
                x.Deleted == false &&
                (x.TaskStatus == TaskStatus.NoLongerRequired ||
                 x.TaskStatus == TaskStatus.Completed)))
            {
                return DerivedTaskStatusForDisplay.Completed;
            }

            return DerivedTaskStatusForDisplay.None;
        }

        public virtual void AddTask(ResponsibilityTask task)
        {
            var hasMultipleFreqenciesBefore = HasMultipleFrequencies;
            ResponsibilityTasks.Add(task);
            var hasMultipleFreqenciesAfter = HasMultipleFrequencies;

            if(hasMultipleFreqenciesBefore != hasMultipleFreqenciesAfter)
            {
                OnHasMultipleFrequencyChangeToTrue(new EventArgs());
            }
        }

        protected internal virtual void RaiseOnHasMultipleFrequencyChangeToTrue()
        {
            OnHasMultipleFrequencyChangeToTrue(new EventArgs());
        }

        public virtual IEnumerable<StatutoryResponsibilityTaskTemplate> GetUncreatedStatutoryResponsibilityTaskTemplates()
        {
            if (StatutoryResponsibilityTemplateCreatedFrom == null)
            {
                return new List<StatutoryResponsibilityTaskTemplate>();
            }

            //get a list of statutory tasks that have been created
            var templatesWhichHaveTasksCreatedFromThem = ResponsibilityTasks
                .Where(x => x.IsStatutoryResponsibilityTask)
                .Select(x => x.StatutoryResponsibilityTaskTemplateCreatedFrom);

            //get the list of statutory tasks from the template and filter out the tasks that have already been created
            return StatutoryResponsibilityTemplateCreatedFrom.ResponsibilityTasks.Where(x => !templatesWhichHaveTasksCreatedFromThem.Contains(x));
        }
    }
}
