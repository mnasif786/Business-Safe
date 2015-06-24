using System.Security.Principal;

namespace BusinessSafe.WebSite.Tests.Filters
{
    public class FakePrincipal : IPrincipal
    {
        public bool IsInRole(string role)
        {
            throw new System.NotImplementedException();
        }

        public IIdentity Identity
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}