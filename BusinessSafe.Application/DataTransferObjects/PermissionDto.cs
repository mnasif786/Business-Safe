using BusinessSafe.Domain.Entities;
using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.DataTransferObjects
{
    [CoverageExclude]
    public class PermissionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PermissionTargetDto PermissionTargetDto { get; set; }
        public PermissionActivity PermissionActivity;
        public int PermissionGroupId { get; set; }
        public string PermissionGroupName { get; set; }
    }
}