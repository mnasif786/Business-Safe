using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.DataTransferObjects
{
    [CoverageExclude]
    public class RolePermissionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoleDto Role { get; set; }
        public PermissionDto Permission { get; set; }
    }
}