using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.ParameterClasses
{
    public class RegisterEmployeeAsUserParameters
    {
        public Guid NewUserId { get; set; } 
        public Role Role { get; set; } 
        public SiteStructureElement Site { get; set; }
        public UserForAuditing ActioningUser { get; set; }
        public long CompanyId { get; set; }
        public SiteStructureElement MainSite { get; set; }
    }
}
