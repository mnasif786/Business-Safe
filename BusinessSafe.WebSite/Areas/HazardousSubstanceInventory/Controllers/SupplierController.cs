using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Controllers
{
    public class SupplierController : BaseController
    {
        private readonly ISuppliersService _suppliersService;
        private readonly IHazardousSubstancesService _hazardousSubstancesService;

        public SupplierController(ISuppliersService suppliersService, IHazardousSubstancesService hazardousSubstancesService)
        {
            _suppliersService = suppliersService;
            _hazardousSubstancesService = hazardousSubstancesService;
        }

        [HttpGet]
        [PermissionFilter(Permissions.ViewHazardousSubstanceInventory)] 
        public JsonResult GetSuppliers( long companyId,string filter= "", int pageLimit = 100)
        {
            var suppliers = _suppliersService.Search(filter, companyId, pageLimit);
            var result = suppliers.Select(AutoCompleteViewModel.ForSupplier).AddDefaultOption().ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [PermissionFilter(Permissions.AddHazardousSubstanceInventory)]
        public PartialViewResult CreateNewSupplier(long companyId)
        {
            return PartialView("_AddSupplier", new SupplierViewModel()
            {
                CompanyId = companyId
            });
        }

        [PermissionFilter(Permissions.DeleteHazardousSubstanceInventory)]
        public JsonResult CanDeleteSupplier(long companyDefaultId)
        {
            var hasSupplierGotHazardousSubstances = _hazardousSubstancesService.HasSupplierGotHazardousSubstances(companyDefaultId, CurrentUser.CompanyId);
            return Json(new { CanDelete = hasSupplierGotHazardousSubstances == false}, JsonRequestBehavior.AllowGet);
        }
    }
}