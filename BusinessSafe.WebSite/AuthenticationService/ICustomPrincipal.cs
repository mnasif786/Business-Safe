using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace BusinessSafe.WebSite.AuthenticationService
{
    public interface ICustomPrincipal : IPrincipal
    {
        Guid UserId { get; }
        long CompanyId { get; set; }
        IList<long> GetSitesFilter();
        string Email { get; set; }
        bool IsImpersonatingUser { get; }
        string CompanyName { get; }
    }
}