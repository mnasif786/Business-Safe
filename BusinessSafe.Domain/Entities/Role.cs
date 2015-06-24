using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.CustomExceptions;

namespace BusinessSafe.Domain.Entities
{

    public class Role : Entity<Guid>
    {
        public virtual string Description { get; set; }
        public virtual string Name { get; set; }
        public virtual long CompanyId { get; set; }
        public virtual IList<RolePermission> Permissions { get; set; }
        public virtual IList<User> Users { get; set; }


        public Role()
        {
            Permissions = new List<RolePermission>();
            Users = new List<User>();
        }

        public static Role Create(string roleName, long companyId, IList<Permission> permissions, UserForAuditing user)
        {
            var now = DateTime.Now;

            var role = new Role()
                       {
                           Id = Guid.NewGuid(),
                           Description = roleName,
                           Name = roleName.Replace(" ", ""),
                           CompanyId = companyId,
                           CreatedBy = user,
                           CreatedOn = now
                       };

            role.Permissions = permissions.Select(permission => new RolePermission
                                                                    {
                                                                        Role = role,
                                                                        Permission = permission,
                                                                        CreatedBy = user,
                                                                        CreatedOn = now,
                                                                        Deleted = false
                                                                    }).ToList();

            return role;
        }

        public virtual void Amend(string name, IEnumerable<Permission> permissions, UserForAuditing user)
        {
            Name = name;

            var permissionsToAdd = permissions.Where(x => !Permissions.Select(y => y.Permission).Contains(x));
            var permissionsToDelete = Permissions.Where(x => !permissions.Contains(x.Permission));

            foreach (var permission in permissionsToAdd)
            {
                Permissions.Add(new RolePermission
                                    {
                                        Permission = permission,
                                        Role = this,
                                        CreatedBy = user,
                                        CreatedOn = DateTime.Now,
                                        Deleted = false
                                    });
            }

            foreach (var permission in permissionsToDelete)
            {
                permission.MarkForDelete(user);
            }

            SetLastModifiedDetails(user);
        }

        public override void MarkForDelete(UserForAuditing user)
        {
            if (IsSystemDefault())
            {
                throw new AttemptingToDeleteSystemRoleException(Name);
            }

            if (Users.Any(x => x.Role.Id == Id && x.Deleted == false))
            {
                throw new AttemptingToDeleteRoleCurrentlyUsedByUsersException(Name);
            }

            base.MarkForDelete(user);
        }

        private bool IsSystemDefault()
        {
            return CompanyId == 0;
        }
    }
}