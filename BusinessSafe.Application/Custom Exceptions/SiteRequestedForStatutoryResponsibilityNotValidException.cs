using System;

namespace BusinessSafe.Application.Custom_Exceptions
{
    public class SiteRequestedForStatutoryResponsibilityNotValidException : Exception
    {
        public SiteRequestedForStatutoryResponsibilityNotValidException() : base(string.Format("Site requested when creating a Statutory Responsibility not valid")) { }
    }
}