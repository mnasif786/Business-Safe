using System;
using System.Collections.Generic;
using BusinessSafe.Infrastructure.Attributes;
using System.Runtime.Serialization;

namespace BusinessSafe.Application.DataTransferObjects
{
    [CoverageExclude]
    [KnownType(typeof(Guid[]))]
    [KnownType(typeof(Array))]
    public class UserDto
    {
        public Guid Id { get; set; }
        public long CompanyId { get; set; }
        public EmployeeDto Employee { get; set; }
        public RoleDto Role { get; set; }
        public bool Deleted { get; set; } 
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public IEnumerable<string> Permissions { get; set; }
        public SiteStructureElementDto SiteStructureElement { get; set; }
        public IList<long> AllowedSites { get; set; }
        public bool IsRegistered { get; set; }
    }
    
}