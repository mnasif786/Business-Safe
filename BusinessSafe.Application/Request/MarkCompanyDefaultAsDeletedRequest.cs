using System;

namespace BusinessSafe.Application.Request
{
    public class MarkCompanyDefaultAsDeletedRequest
    {
        public MarkCompanyDefaultAsDeletedRequest()
        {}
        
        public MarkCompanyDefaultAsDeletedRequest(long companyDefaultId, long companyId, Guid userId)
        {
            CompanyDefaultId = companyDefaultId;
            CompanyId = companyId;
            UserId = userId;
        }

        public long CompanyDefaultId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}