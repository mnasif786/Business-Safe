using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Controllers
{
    public class InventoryController : BaseController
    {
        private readonly IInventoryViewModelFactory _inventoryViewModelFactory;
        private readonly IHazardousSubstancesService _hazardousSubstancesService;
        private readonly IHazardousSubstanceViewModelFactory _hazardousSubstanceViewModelFactory;

        public InventoryController(
            IInventoryViewModelFactory inventoryViewModelFactory,
            IHazardousSubstancesService hazardousSubstancesService,
            IHazardousSubstanceViewModelFactory hazardousSubstanceViewModelFactory)
        {
            _inventoryViewModelFactory = inventoryViewModelFactory;
            _hazardousSubstancesService = hazardousSubstancesService;
            _hazardousSubstanceViewModelFactory = hazardousSubstanceViewModelFactory;
        }

        [PermissionFilter(Permissions.ViewHazardousSubstanceInventory)]
        public ActionResult View(long companyId, long hazardousSubstanceId)
        {
            IsReadOnly = true;
            return Edit(companyId, hazardousSubstanceId);
        }

        [PermissionFilter(Permissions.ViewHazardousSubstanceInventory)]
        public ActionResult Index(long companyId, long? supplierId, string substanceNameLike, bool showDeleted = false)
        {
            var model = _inventoryViewModelFactory
                .WithCompanyId(companyId)
                .WithSupplierId(supplierId)
                .WithSubstanceNameLike(substanceNameLike)
                .WithShowDeleted(showDeleted)
                .GetViewModel();

            return View("Index",model);
        }

        [PermissionFilter(Permissions.AddHazardousSubstanceInventory)]
        public ViewResult Add()
        {
            var viewModel = _hazardousSubstanceViewModelFactory
                            .WithCompanyId(CurrentUser.CompanyId)
                            .GetViewModel();

            return View(viewModel);
        }

        [PermissionFilter(Permissions.EditHazardousSubstanceInventory)]
        public ActionResult Edit(long companyId, long hazardousSubstanceId)
        {
            var viewModel = _hazardousSubstanceViewModelFactory
                             .WithCompanyId(companyId)
                             .WithHazardousSubstanceId(hazardousSubstanceId)
                             .GetViewModel();

            return View("Edit",viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditHazardousSubstanceInventory)]
        public ActionResult Update(AddHazardousSubstanceRequest request)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = _hazardousSubstanceViewModelFactory
                                    .WithCompanyId(request.CompanyId)
                                    .GetViewModel(request);
                return View("Add", viewModel);
            }

            request.UserId = CurrentUser.UserId;
            request.CompanyId = CurrentUser.CompanyId;
            _hazardousSubstancesService.Update(request);

            if (request.IsAssessmentRequired())
            {
                return RedirectToAction("Create", "RiskAssessment", new { area = "HazardousSubstanceRiskAssessments", companyId = request.CompanyId, newHazardousSubstanceId = request.Id });
            }

            return RedirectToAction("Index", "Inventory", new { companyId = request.CompanyId });

        }

        [HttpPost]
        [PermissionFilter(Permissions.AddHazardousSubstanceInventory)]
        public ActionResult Add(AddHazardousSubstanceRequest request)
        {
            //server-side date validation as jQuery Datepicker fails with standard Range validation
            ValidateDates(request);
            if (!ModelState.IsValid)
            {
                var viewModel = _hazardousSubstanceViewModelFactory
                                    .WithCompanyId(request.CompanyId)
                                    .GetViewModel(request);
                return View("Add", viewModel);
            }

            request.UserId = CurrentUser.UserId;
            request.CompanyId = CurrentUser.CompanyId;


            var newHazardousSubstanceId = _hazardousSubstancesService.Add(request);

            if (request.IsAssessmentRequired())
            {
                return RedirectToAction("Create", "RiskAssessment", new { area = "HazardousSubstanceRiskAssessments", companyId = request.CompanyId, newHazardousSubstanceId });
            }

            return RedirectToAction("Index", "Inventory", new { companyId = request.CompanyId });


        }

        public void ValidateDates(AddHazardousSubstanceRequest request)
        {
            if (request.SdsDate <= (DateTime)SqlDateTime.MinValue || request.SdsDate >= (DateTime)SqlDateTime.MaxValue)
            {
                ModelState.AddModelError("SdsDate", "Please enter a valid date");
            }
        }

        [PermissionFilter(Permissions.ViewHazardousSubstanceInventory)]
        public ActionResult CheckForDuplicatedSubstance(CheckForDuplicatedSubstanceRequest model)
        {
            var searchResults = _hazardousSubstancesService.Search(new SearchHazardousSubstancesRequest()
            {
                CompanyId = model.CompanyId,
                SubstanceNameLike = model.NewSubstanceName
            });

            if (searchResults.Any())
            {
                return PartialView(searchResults);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [PermissionFilter(Permissions.ViewHazardousSubstanceInventory)]
        public JsonResult GetHazardousSubstances(string filter, long companyId, int pageLimit)
        {
            var suppliers = _hazardousSubstancesService.GetHazardousSubstancesForSearchTerm(filter, companyId, pageLimit);
            var result = suppliers.Select(AutoCompleteViewModel.ForHazardousSubstance).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteHazardousSubstanceInventory)]
        public JsonResult MarkHazardousSubstanceAsDeleted(long companyId, long hazardousSubstanceId)
        {
            if (companyId == 0 || hazardousSubstanceId == 0)
            {
                throw new ArgumentException("Invalid hazardousSubstanceId and companyId");
            }

            _hazardousSubstancesService.MarkForDelete(new MarkHazardousSubstanceAsDeleteRequest() { HazardousSubstanceId = hazardousSubstanceId, CompanyId = companyId, UserId = CurrentUser.UserId });
            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteHazardousSubstanceInventory)]
        public JsonResult MarkHazardousSubstanceAsNotDeleted(long companyId, long hazardousSubstanceId)
        {
            if (companyId == 0 || hazardousSubstanceId == 0)
            {
                throw new ArgumentException("Invalid hazardousSubstanceId and companyId");
            }

            _hazardousSubstancesService.Reinstate(new ReinstateHazardousSubstanceRequest() { HazardousSubstanceId = hazardousSubstanceId, CompanyId = companyId, UserId = CurrentUser.UserId });
            return Json(new { Success = true });
        }

        //This action does not require a permission filter ALP
        public JsonResult CanDeleteHazardousSubstance(CanDeleteHazardousSubstanceViewModel viewModel)
        {
            if (viewModel.CompanyId == 0 || viewModel.HazardousSubstanceId == 0)
            {
                throw new ArgumentException("Invalid hazardousSubstanceId and companyId");
            }

            var result = _hazardousSubstancesService.HasHazardousSubstanceGotRiskAssessments(viewModel.HazardousSubstanceId, viewModel.CompanyId);

            if (result)
            {
                return Json(new { CanDeleteHazardousSubstance = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { CanDeleteHazardousSubstance = true }, JsonRequestBehavior.AllowGet);
        }

        [PermissionFilter(Permissions.ViewHazardousSubstanceInventory)]
        public PartialViewResult SubstanceDetailsForRiskAssessment(int id)
        {
            var hazardousSubstance = _hazardousSubstancesService.GetByIdAndCompanyId(id, CurrentUser.CompanyId);
            return PartialView(new HazardousSubstanceSummaryViewModel()
                               {
                                   Name = hazardousSubstance.Name,
                                   Id = hazardousSubstance.Id,
                                   HazardSymbolStandard = hazardousSubstance
                                       .Standard
                                       .ToString(),
                                   Hazards = hazardousSubstance
                                       .Pictograms
                                       .Select(x => string.Format("{0}", x.Title))
                                       .ToArray(),
                                   RiskPhrases = hazardousSubstance
                                       .RiskPhrases
                                       .Select(x => string.Format("{0} {1}", x.ReferenceNumber, x.Title))
                                       .ToArray(),
                                   SafetyPhrases = hazardousSubstance
                                       .HazardousSubstanceSafetyPhrases
                                       .Select(x => string.Format("{0} {1}", x.SafetyPhase.ReferenceNumber, x.SafetyPhase.Title))
                                       .ToArray(),
                                   AdditionalInformationRecords = hazardousSubstance
                                       .HazardousSubstanceSafetyPhrases
                                       .Where(y => y.AdditionalInformation != null)
                                       .Select(x => string.Format("{0}: {1}", x.SafetyPhase.ReferenceNumber, x.AdditionalInformation))
                                       .ToArray()
                               });
        }
    }
}