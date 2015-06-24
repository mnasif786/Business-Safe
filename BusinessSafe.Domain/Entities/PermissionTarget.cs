using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class PermissionTarget : Entity<long>
    {
        public virtual string Name { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual PermissionGroup PermissionGroup { get; set; }
    }
}
