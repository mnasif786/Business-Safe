using System;

namespace BusinessSafe.WebSite.Custom_Exceptions
{

    public class SitePermissionsInvalidForUserException : ArgumentNullException
    {
        public SitePermissionsInvalidForUserException(Guid userId, long riskAssessmentId) : base(string.Format("Site not found for User. Risk Assessment Id {0}. User Id {1}", riskAssessmentId, userId.ToString())) { }
    }
}