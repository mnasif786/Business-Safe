using System;
namespace BusinessSafe.Application.Request
{
    public class CreateUpdateSiteGroupRequest
    {  
        public long GroupId { get; set; }
        public long CompanyId { get; set; }
        public string Name { get; set; }        
        public long? LinkToGroupId { get; set; }
        public long? LinkToSiteId { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}