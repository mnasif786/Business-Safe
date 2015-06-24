using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class Supplier : Entity<long>
    {
        public virtual string Name { get; set; }
        public virtual long CompanyId { get; set; }

        public static Supplier Create(string name, long companyId, UserForAuditing creatingUser)
        {
            return new Supplier()
            {
                Name = name,
                CompanyId = companyId,
                CreatedOn = DateTime.Now,
                CreatedBy = creatingUser
            };
        }

        public virtual void Update(string name, UserForAuditing user)
        {
            Name = name;
            SetLastModifiedDetails(user);
        }
        
    }
}
