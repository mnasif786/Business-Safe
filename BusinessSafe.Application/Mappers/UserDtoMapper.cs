using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class UserDtoMapper
    {
        public IEnumerable<UserDto> Map(IEnumerable<User> users)
        {
            return users.Select(Map);
        }

        public IEnumerable<UserDto> MapIncludingRole(IEnumerable<User> users)
        {
            return users.Select(MapIncludingRole);
        }

        public UserDto Map(User user)
        {
            if (user == null)
            {
                return new UserDto();
            }
            var userDto = new UserDto();
            userDto.Id = user.Id;
            userDto.CreatedOn = user.CreatedOn;
            userDto.Deleted = user.Deleted;
            userDto.LastModifiedOn = user.LastModifiedOn;
            userDto.CompanyId = user.CompanyId;
            userDto.IsRegistered = user.IsRegistered.HasValue ? user.IsRegistered.Value : true;
            return userDto;
        }

        public UserDto MapIncludingRole(User user)
        {
            var userDto = Map(user);

            ////PTD: shouldn't be here and I don't think it's needed.
            //if(user.Site != null)
            //{
            //    if(user.Site.Self as Site != null)
            //        userDto.SiteStructureElement = new SiteDto();
            //    else if (user.Site.Self as SiteGroup != null)
            //        userDto.SiteStructureElement = new SiteGroupDto();

            //    if (userDto.SiteStructureElement != null)
            //    {
            //        userDto.SiteStructureElement.Id = user.Site.Id;
            //        userDto.SiteStructureElement.Name = user.Site.Name;
            //        userDto.SiteStructureElement.IsMainSite = user.Site.IsMainSite;
            //    }
            //}

            userDto.Role = RoleDto.CreateFrom(user.Role);
            userDto.Permissions = user.GetPermissions();
            
            return userDto;
        }

        public UserDto MapIncludingRoleAndSite(User user)
        {
            var userDto = Map(user);

            if (user.Site != null)
                userDto.SiteStructureElement = new SiteStructureElementDtoMapper().Map(user.Site);

            userDto.Role = RoleDto.CreateFrom(user.Role);
            userDto.Permissions = user.GetPermissions();

            return userDto;
        }

        public IEnumerable<UserDto> MapIncludingEmployeeAndSite(IEnumerable<User> entities)
        {
            return entities.Select(MapIncludingEmployeeAndSite);
        }

        public UserDto MapIncludingEmployeeAndSite(User user)
        {
            var userDto = MapIncludingRole(user);

            if (user.Employee != null)
                userDto.Employee = new EmployeeDtoMapper().MapWithNationalityAndContactDetailsAndEmergencyContactDetails(user.Employee);

            if (user.Site != null)
                userDto.SiteStructureElement = new SiteStructureElementDtoMapper().Map(user.Site);

            return userDto;
        }

        public UserDto MapIncludingAllowedSitesAndEmployee(User user)
        {
            var userDto = MapIncludingRole(user);
            if (user.Site != null)
            {
                userDto.AllowedSites = user.Site.GetThisAndAllDescendants().Select(x => x.Id).ToList();
            }

            if (user.Employee != null)
                userDto.Employee = new EmployeeDtoMapper().MapWithNationalityAndContactDetailsAndEmergencyContactDetailsAndUser(user.Employee);

            return userDto;
        }
    }
}