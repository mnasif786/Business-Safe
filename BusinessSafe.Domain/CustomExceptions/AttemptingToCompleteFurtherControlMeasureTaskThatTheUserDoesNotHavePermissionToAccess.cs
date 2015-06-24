using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToCompleteFurtherControlMeasureTaskThatTheUserDoesNotHavePermissionToAccess : Exception
    {
        public AttemptingToCompleteFurtherControlMeasureTaskThatTheUserDoesNotHavePermissionToAccess(long taskId)
            : base(string.Format("Trying to complete further action task that belongs to a site that the user does not have permission to access. Further Action Task Id {0}", taskId))
        { }

        public AttemptingToCompleteFurtherControlMeasureTaskThatTheUserDoesNotHavePermissionToAccess()
        { }
    }
}