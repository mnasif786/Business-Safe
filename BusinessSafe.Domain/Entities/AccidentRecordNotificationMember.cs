using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.Entities
{
    public abstract class AccidentRecordNotificationMember : Entity<int>
    {
        public virtual SiteStructureElement Site { get; set; }
        public abstract bool IsInitialised();
        public abstract bool MatchesEmployee(Employee employee);

        public abstract string Email();
        public abstract string FullName();
        public abstract Guid EmployeeId(); // SGG: Look at refactoring this - NonEmployee shouldn't just return empty guid

        public abstract bool HasEmail();
    }

    public class AccidentRecordNotificationEmployeeMember : AccidentRecordNotificationMember
    {
        public virtual Employee Employee { get; set; }

        public override bool IsInitialised()
        {
            return (Employee != null && !Employee.Deleted);
        }

        public override bool MatchesEmployee(Employee employee)
        {
            return Employee == employee;        
        }

        public override string  Email()
        {
            return Employee.GetEmail();
        }

        public override string  FullName()
        {
            return Employee.FullName;
        }

        public override Guid  EmployeeId()
        {
            return Employee.Id;
        }

        public override bool HasEmail()
        {
            return Employee.HasEmail;
        }
    }

    public class AccidentRecordNotificationNonEmployeeMember : AccidentRecordNotificationMember
    {
        public virtual string NonEmployeeEmail { get; set; }
        public virtual string NonEmployeeName { get; set; }

        public override bool IsInitialised()
        {
            return ( !String.IsNullOrEmpty(NonEmployeeEmail) && !String.IsNullOrEmpty(NonEmployeeEmail) );
        }

        public override bool MatchesEmployee(Employee employee)
        {
            return false; 
        }

        public override string Email()
        {
            return NonEmployeeEmail;
        }

        public override string FullName()
        {
            return NonEmployeeName;
        }

        public override Guid EmployeeId()
        {
            return Guid.Empty;
        }

        public override bool HasEmail()
        {
            return !String.IsNullOrEmpty(NonEmployeeEmail);
        }
    }     
}
 