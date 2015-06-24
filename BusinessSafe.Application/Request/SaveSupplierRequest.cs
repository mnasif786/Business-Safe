using System;

namespace BusinessSafe.Application.Request
{
    public class SaveSupplierRequest
    {
        public long Id { get;  set; }
        public string Name { get;  set; }
        public long CompanyId { get;  set; }
        public Guid UserId { get; set; } 
    }
}