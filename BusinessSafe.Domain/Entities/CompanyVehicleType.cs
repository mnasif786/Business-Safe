using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class CompanyVehicleType : Entity<int>
    {
        public virtual string Name { get; set; }
    }
}