using System;

namespace BusinessSafe.Application.Request
{
    public class MarkNonEmployeeAsDeletedRequest
    {
        public MarkNonEmployeeAsDeletedRequest()
        {}
        public MarkNonEmployeeAsDeletedRequest(long nonEmployeeId, long companyId, Guid userId)
        {
            NonEmployeeId = nonEmployeeId;
            CompanyId = companyId;
            UserId = userId;
        }

        public long NonEmployeeId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}