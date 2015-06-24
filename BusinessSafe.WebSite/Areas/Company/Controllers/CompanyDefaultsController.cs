using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Company.Factories;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.Controllers
{
    public class CompanyDefaultsController : BaseController
    {
        private readonly ICompanyDefaultService _companyDefaultService;
        private readonly INonEmployeeService _nonEmployeeService;
        private readonly ICompanyDefaultsTaskFactory _companyDefaultsTaskFactory;
        private readonly ISuppliersService _suppliersService;
        private readonly ISiteService _siteService;

        public CompanyDefaultsController(
            INonEmployeeService nonEmployeeService, 
            ICompanyDefaultService companyDefaultService, 
            ICompanyDefaultsTaskFactory companyDefaultsTaskFactory, 
            ISuppliersService suppliersService,
            ISiteService siteService)
        {
            _nonEmployeeService = nonEmployeeService;
            _companyDefaultService = companyDefaultService;
            _companyDefaultsTaskFactory = companyDefaultsTaskFactory;
            _suppliersService = suppliersService;
            _siteService = siteService;
        }

        [PermissionFilter(Permissions.ViewCompanyDefaults)]
        public ActionResult Index(long companyId)
        {

            var hazards = _companyDefaultService.GetAllHazardsForCompany(companyId);
            var peopleAtRisk = _companyDefaultService.GetAllPeopleAtRiskForCompany(companyId);
            var nonEmployees = _nonEmployeeService.GetAllNonEmployeesForCompany(companyId);
            var suppliers = _suppliersService.GetForCompany(companyId);
            var sites = _siteService.Search(new SearchSitesRequest
                                                {
                                                    CompanyId = companyId,
                                                    AllowedSiteIds = CurrentUser.GetSitesFilter()
                                                }).Select(AutoCompleteViewModel.ForSite).AddDefaultOption();


            var viewModel = new CompanyDefaultsViewModel(new List<Defaults>(),
                                                         suppliers,
                                                         new List<Defaults>(),
                                                         nonEmployees,
                                                         hazards,
                                                         peopleAtRisk) {CompanyId = companyId};
            viewModel.Sites = sites;

            return View(viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditCompanyDefaults)]
        [ActionName("UpdateCompanyDefaults")]
        public JsonResult UpdateCompanyDefaults(SaveCompanyDefaultViewModel viewModel)
        {
            return SaveCompanyDefaults(viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddCompanyDefaults)]
        [ActionName("CreateCompanyDefaults")]
        public JsonResult CreateCompanyDefaults(SaveCompanyDefaultViewModel viewModel)
        {
            return SaveCompanyDefaults(viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.ViewRiskAssessmentTasks)]
        [ActionName("CreateRiskAssessmentDefaults")]
        public JsonResult CreateRiskAssessmentDefaults(SaveCompanyDefaultViewModel viewModel)
        {
            if (CurrentUser.IsInRole(Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString())
                || CurrentUser.IsInRole(Permissions.EditFireRiskAssessments.ToString())
                || CurrentUser.IsInRole(Permissions.EditPersonalRiskAssessments.ToString()))
            {
                if (viewModel.RiskAssessmentId == null || viewModel.RiskAssessmentId == default(long))
                {
                    return Json(new {Success = false, Message = "Must be associated with a Risk Assessment."});
                }
                return SaveCompanyDefaults(viewModel);
            }
            
            return Json(new { Success = false, Message = "Unauthorised." });
        }


        private JsonResult SaveCompanyDefaults(SaveCompanyDefaultViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.CompanyDefaultValue))
            {
                return Json(new { Success = false, Message = "Value must be provided." });
            }

            var saveTask = _companyDefaultsTaskFactory.CreateSaveTask(viewModel.CompanyDefaultType);

            var result = saveTask.Execute(viewModel, CurrentUser.UserId);

            if (!result.Success)
            {
                return Json(new { Success = false, result.Message, result.Matches });
            }

            return Json(new { Success = true, result.Id });
        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteCompanyDefaults)]
        public JsonResult DeleteCompanyDefault(DeleteCompanyDefaultViewModel viewModel)
        {
            var deleteTask = _companyDefaultsTaskFactory.CreateMarkForDeletedTask(viewModel.CompanyDefaultType);
            deleteTask.Execute(viewModel.CompanyDefaultId, viewModel.CompanyId, CurrentUser.UserId);
            return Json(new { Id = viewModel.CompanyDefaultId, Success = true });
        }

        [PermissionFilter(Permissions.EditCompanyDefaults)]
        public JsonResult GetDefaultRiskAssessmentTypes(long companyId, long companyDefaultId)
        {
            var hazardForCompany = _companyDefaultService.GetHazardForCompany(companyId, companyDefaultId);
            return Json(new {hazardForCompany.IsGra, hazardForCompany.IsPra,hazardForCompany.IsFra, Success = true }, JsonRequestBehavior.AllowGet);
        }

        [PermissionFilter(Permissions.DeleteCompanyDefaults)]
        public JsonResult CanDeleteHazard(long companyDefaultId, long companyId)
        {
            var canDeleteHazard = _companyDefaultService.CanDeleteHazard(companyDefaultId, companyId);
            return Json(new { CanDelete = canDeleteHazard }, JsonRequestBehavior.AllowGet);
        }
    }
}