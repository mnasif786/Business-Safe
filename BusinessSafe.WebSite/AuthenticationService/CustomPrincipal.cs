using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.AuthenticationService
{
    public class CustomPrincipal : ICustomPrincipal
    {
        private readonly IEnumerable<string> _permissions;
        private readonly IList<long> _allowableSites;
 
        public IIdentity Identity { get; private set; }
        public Guid UserId { get; set; }
        public long CompanyId { get; set; }
        public string FullName { get; set; }
        public string Email{ get; set; }
        public bool IsImpersonatingUser { get; private set; }
        public string CompanyName { get; set; }

        public CustomPrincipal(UserDto userDto, CompanyDto companyDto)
        {
            Identity = new GenericIdentity(GetUserIdentity(userDto));
            UserId = userDto.Id;
            CompanyId = userDto.CompanyId;
            FullName = userDto.Employee != null ? userDto.Employee.FullName : null;
            Email = userDto.Employee != null && userDto.Employee.MainContactDetails != null && userDto.Employee.MainContactDetails.Email != null ? userDto.Employee.MainContactDetails.Email: string.Empty;
            CompanyName = companyDto.CompanyName;
            _allowableSites = userDto.AllowedSites;
            _permissions = userDto.Permissions;
        }
        
        private static string GetUserIdentity(UserDto userDto)
        {
            return userDto.Employee != null && userDto.Employee.MainContactDetails != null && !string.IsNullOrEmpty(userDto.Employee.MainContactDetails.Email) ? userDto.Employee.MainContactDetails.Email : userDto.Id.ToString();
        }

        public bool IsInRole(string role)
        {
            return _permissions.Any(x => x.ToLower() == role.ToString().ToLower());
        }

        public IList<long> GetSitesFilter()
        {
            return _allowableSites.ToList();
        }

        public void MarkAsImpersonatingUser()
        {
            IsImpersonatingUser = true;
        }
    }
}