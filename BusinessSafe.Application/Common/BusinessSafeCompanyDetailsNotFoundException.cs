using System;

namespace BusinessSafe.Application.Common
{
    public class BusinessSafeCompanyDetailsNotFoundException : Exception
    {
        private long _requestedCompanyId { get; set; }

        public BusinessSafeCompanyDetailsNotFoundException(long companyId) : base(string.Format("Could not find BusinessSafeCompanyDetails with id {0} ", companyId))
        {
            _requestedCompanyId = companyId;
        }
    }
}