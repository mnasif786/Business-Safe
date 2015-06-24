using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class ChecklistPersonSeen : BaseEntity<Guid>
    {
        public static ChecklistPersonSeen Create(Guid id, string fullName, string email)
        {
            return new ChecklistPersonSeen(){ Id =id, FullName = fullName, EmailAddress = email, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now};
        }

        public static ChecklistPersonSeen Create(Employee employee)
        {
            return new ChecklistPersonSeen()
                       {
                           Id = Guid.NewGuid(),
                           Employee = employee,
                           FullName = employee.FullName,
                           EmailAddress = employee.GetEmail(),
                           CreatedOn = DateTime.Now,
                           LastModifiedOn = DateTime.Now
                       };
        }

        public virtual Checklist Checklist { get; set; }
        public virtual Employee Employee { get; set; }

        public virtual string FullName { get; set; }
        public virtual string EmailAddress { get; set; }
    }
}
