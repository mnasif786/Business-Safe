using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class SafeCheckChecklistAlreadySubmittedException : Exception
    {
        public SafeCheckChecklistAlreadySubmittedException(long actionPlanId)
            : base(string.Format("Checklist has already been submitted and an Action Plan {0} has previously been created.", actionPlanId))
        { }

        public SafeCheckChecklistAlreadySubmittedException()
        { }
    }
}