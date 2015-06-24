using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class ChecklistOtherEmail : BaseEntity<Guid>
    {
        public virtual Checklist Checklist { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string Name { get; set; }

        public static ChecklistOtherEmail Create(Guid id, string email, string name)
        {
            return new ChecklistOtherEmail()
            {
                Id =  id, 
                EmailAddress = email, 
                CreatedOn = DateTime.Now, 
                LastModifiedOn = DateTime.Now,
                Name = name
            };
        }

        public static ChecklistOtherEmail Create(string email, string name)
        {
            return new ChecklistOtherEmail()
            {
                Id = Guid.NewGuid(),
                EmailAddress = email,
                CreatedOn = DateTime.Now,
                LastModifiedOn = DateTime.Now,
                Name = name
            };
        }
    }
}
