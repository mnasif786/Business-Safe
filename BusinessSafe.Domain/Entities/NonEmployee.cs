using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class NonEmployee : Entity<long>
    {
        public virtual long LinkToCompanyId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Position { get; set; }
        public virtual string Company { get; set; }

        public static NonEmployee Create(string name, string position, string companyName, long linkToCompanyId, UserForAuditing creatingUser)
        {
            var nonEmployee = new NonEmployee
            {
                Name = name,
                Position = position,
                Company = companyName,
                LinkToCompanyId = linkToCompanyId,
                CreatedOn = DateTime.Now,
                CreatedBy = creatingUser
            };
            return nonEmployee;
        }
        
        public virtual void Update(string name, string position, string companyName, UserForAuditing modifyingUser)
        {
            Name = name;
            Company = companyName;
            Position = position;
            SetLastModifiedDetails(modifyingUser);
        }

        public virtual string GetFormattedName()
        {
            return string.Format("{0}, {1}, {2}", Name, Position, Company);
        }
    }
}