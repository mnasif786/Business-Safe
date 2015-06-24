using System.Security.Principal;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.ViewModels
{
    public class SupplierViewModel
    {
        public string Name { get; set; }
        public long SupplierId { get; set; }
        public long CompanyId { get; set; }

        public bool IsSaveButtonEnabled(IPrincipal user)
        {
            return user.IsInRole(SupplierId == 0 ? Permissions.AddCompanyDefaults.ToString() : Permissions.EditCompanyDefaults.ToString());
        }
    }
}