using System;
using System.Collections.Generic;
using System.Security.Principal;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Tests.Areas.AccidentReports
{
    public class FakeCustomPrincipal : ICustomPrincipal
    {
        private readonly long _companyId;
        private IList<long> _sitesFilter;

        public FakeCustomPrincipal(long companyId)
        {
            _companyId = companyId;
        }

        public long CompanyId
        {
            get { return _companyId; }
            set { throw new NotImplementedException(); }
        }

        public void SetSitesFilter(IList<long> sitesFilter)
        {
            _sitesFilter = sitesFilter;
        }

        public IList<long> GetSitesFilter()
        {
            return _sitesFilter;
        }

        #region Unimplemented_Stuff
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public IIdentity Identity
        {
            get { throw new NotImplementedException(); }
        }

        public Guid UserId
        {
            get { throw new NotImplementedException(); }
        }

        

        public string Email
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool IsImpersonatingUser
        {
            get { throw new NotImplementedException(); }
        }

        public string CompanyName
        {
            get { throw new NotImplementedException(); }
        }
        #endregion
    }
}