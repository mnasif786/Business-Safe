using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class SignificantFinding : Entity<long>
    {
        public virtual string Title { get; set; }
        public virtual FireAnswer FireAnswer { get; set; }
        public virtual IList<FireRiskAssessmentFurtherControlMeasureTask> FurtherControlMeasureTasks { get; set; }

        public SignificantFinding()
        {
            FurtherControlMeasureTasks = new List<FireRiskAssessmentFurtherControlMeasureTask>();
        }

        public static SignificantFinding Create(
            FireAnswer fireAnswer,
            UserForAuditing getNsbSystemUser)
        {
            return new SignificantFinding
            {
                FireAnswer = fireAnswer,
                CreatedOn = DateTime.Now,
                CreatedBy = getNsbSystemUser
            };
        }

        public virtual void AddFurtherControlMeasureTask(FireRiskAssessmentFurtherControlMeasureTask task, UserForAuditing user)
        {
            FurtherControlMeasureTasks.Add(task);
            SetLastModifiedDetails(user);
        }

        public override void MarkForDelete(UserForAuditing user)
        {
            base.MarkForDelete(user);

            foreach (var task in FurtherControlMeasureTasks)
            {
                if(task.TaskStatus != TaskStatus.Completed)
                    task.MarkForDelete(user);
            }
        }
    }
}
