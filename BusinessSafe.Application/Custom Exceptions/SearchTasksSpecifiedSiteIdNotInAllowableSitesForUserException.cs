using System;

namespace BusinessSafe.Application.Custom_Exceptions
{
    public class SearchTasksSpecifiedSiteIdNotInAllowableSitesForUserException : ArgumentException
    {
        public SearchTasksSpecifiedSiteIdNotInAllowableSitesForUserException() : base("Trying to search on site id not in users allowable sites.") { }
    }
}