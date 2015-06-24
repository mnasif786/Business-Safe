using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class Permission : Entity<int>
    {
        public virtual PermissionTarget PermissionTarget { get; set; }
        public virtual PermissionActivity PermissionActivity { get; set; }

        public virtual string Name
        {
            get
            {
                var name = PermissionActivity + " " + PermissionTarget.Name; 
                return name.Replace(" ", string.Empty);
            }
        }
    }
}