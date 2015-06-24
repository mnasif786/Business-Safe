using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class User : Entity<Guid>
    {
        public virtual long CompanyId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Role Role { get; set; }
        public virtual bool? IsRegistered { get; set; }
        public virtual DateTime? DateDeleted { get; set; }
        public virtual IEnumerable<AuthenticationToken> AuthenticationTokens { get; set; }
        public virtual SiteStructureElement Site { get; set; }
        
        public User()
        {
            AuthenticationTokens = new List<AuthenticationToken>();
        }

        public static User CreateUser(Guid id, long companyId, Role role, SiteStructureElement site, Employee employee, UserForAuditing actioningUser)
        {
            var user = new User { Id = id, CompanyId = companyId, Role = role, Site = site, CreatedBy = actioningUser, CreatedOn = DateTime.Now, IsRegistered = false };

            if (employee.User == null)
            {
                user.Employee = employee;
            }

            return user;
        }

        public static User CreateUser(Guid id, long companyId, Role role, SiteStructureElement site, string forename, string surname, string email, UserForAuditing actioningUser)
        {
            var user = new User {Id = id, CompanyId = companyId, Role = role, Site = site, CreatedBy = actioningUser, CreatedOn = DateTime.Now, IsRegistered = false};

            if (user.Employee == null)
            {
                user.Employee = Employee.Create(new AddUpdateEmployeeParameters() { ClientId = companyId, Forename = forename, Surname = surname}, actioningUser);

                var contactDetails = EmployeeContactDetail.Create(new AddUpdateEmployeeContactDetailsParameters { Email = email }, actioningUser, user.Employee);
                user.Employee.AddContactDetails(contactDetails);
            }

            return user;
        }

        public virtual IEnumerable<string> GetPermissions()
        {
            return Role.Permissions.Select(permission => permission.Permission.Name).ToList();
        }

        public virtual void SetRoleAndSite(Role role, SiteStructureElement site, UserForAuditing actioningUser)
        {
            if (role != null && role.CompanyId != default(long) && CompanyId != role.CompanyId)
                throw new CompanyMismatchException<User, Role>();

            if (site != null && CompanyId != site.ClientId)
                throw new CompanyMismatchException<User, SiteStructureElement>();

            if (actioningUser != null && CompanyId != actioningUser.CompanyId)
                throw new CompanyMismatchException<User, User>();

            Role = role;
            Site = site;
            SetLastModifiedDetails(actioningUser);
        }

        public virtual void Delete(UserForAuditing user)
        {
            if (Id == user.Id)
            {
                throw new UserAttemptingToDeleteSelfException(Id);
            }

            Deleted = true;
            SetLastModifiedDetails(user);
            DateDeleted = DateTime.Now;
        }

        public virtual void Register(UserForAuditing actioningUser)
        {
            IsRegistered = true;
            SetLastModifiedDetails(actioningUser);
        }

        public virtual void DisabledAuthenticationTokens()
        {
            AuthenticationTokens.ToList().ForEach(x => x.IsEnabled = false);
        }

        public override void ReinstateFromDelete(UserForAuditing user)
        {
            base.ReinstateFromDelete(user);
            DateDeleted = null;

            if(Employee != null && Employee.Deleted)
            {
               Employee.ReinstateFromDelete(user); 
            }
        }

        
    }
}