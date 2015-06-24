using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.ParameterClasses.SafeCheck;

namespace BusinessSafe.Domain.Entities
{   
    public class ActionPlanIndex 
    {
        public virtual string Title { get; set; }
        public virtual string SiteName { get; set; }
        public virtual DateTime? DateOfVisit { get; set; }
        public virtual string VisitBy { get; set; }
        public virtual DateTime SubmittedOn { get; set; }
        public virtual bool NoLongerRequired { get; set; }
        public virtual bool AnyActionsOverdue { get; set; }
        public virtual bool AnyActionsOutstanding { get; set; }
        public virtual bool AnyActionsCompleted { get; set; }
        public virtual bool AnyActionsNoLongerRequired { get; set; }

        public virtual DerivedTaskStatusForDisplay Status
        {
            get
            {
                if (NoLongerRequired)
                {
                    return DerivedTaskStatusForDisplay.NoLongerRequired;
                }

                if (AnyActionsOverdue)
                {
                    return DerivedTaskStatusForDisplay.Overdue;
                }

                if (AnyActionsOutstanding)
                {
                    return DerivedTaskStatusForDisplay.Outstanding;
                }

                if (AnyActionsCompleted)
                {
                    return DerivedTaskStatusForDisplay.Completed;
                }

                if (AnyActionsNoLongerRequired)
                {
                    return DerivedTaskStatusForDisplay.NoLongerRequired;
                }

                return DerivedTaskStatusForDisplay.None;
            }
        }

    }
}