using System;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class BusinessSafeCompanyDetailDto
    {
        public long CompanyId { get; set; }
        public string BusinessSafeContactEmployeeFullName { get; set; }
        public Guid? BusinessSafeContactEmployeeId { get; set; }
    }
}