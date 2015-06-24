using System;
using System.Web.Mvc;
using AutoMapper;

using BusinessSafe.Application.Common;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.Company.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly IBusinessSafeCompanyDetailService _businessSafeCompanyDetailService;

        public CompanyController(ICompanyDetailsService companyDetailsService, IBusinessSafeCompanyDetailService businessSafeCompanyDetailService)
        {
            _companyDetailsService = companyDetailsService;
            _businessSafeCompanyDetailService = businessSafeCompanyDetailService;
        }

        [PermissionFilter(Permissions.ViewCompanyDetails)]
        public ViewResult Index()
        {
            var model = BuildModel();

            ViewBag.Message = TempData["Message"];

            return View("Index", model);
        }

        private CompanyDetailsViewModel BuildModel()
        {
            var companyDetailsFromPeninsula = _companyDetailsService.GetCompanyDetails(CurrentUser.CompanyId);
            var companyDetailsFromBusinessSafe = _businessSafeCompanyDetailService.Get(CurrentUser.CompanyId);

            return MapCompanyDetailsDtoToCompanyDetailsViewModel(companyDetailsFromPeninsula, companyDetailsFromBusinessSafe);
        }

        private CompanyDetailsViewModel MapCompanyDetailsDtoToCompanyDetailsViewModel(CompanyDetailsDto companyDetailsFromPeninsula,
            BusinessSafeCompanyDetailDto companyDetailsFromBusinessSafe)
        {
            var viewModel =  new CompanyDetailsViewModel()
                   {
                       Id = CurrentUser.CompanyId,
                       AddressLine1 = companyDetailsFromPeninsula.AddressLine1,
                       AddressLine2 = companyDetailsFromPeninsula.AddressLine2,
                       AddressLine3 = companyDetailsFromPeninsula.AddressLine3,
                       AddressLine4 = companyDetailsFromPeninsula.AddressLine4,
                       CAN = companyDetailsFromPeninsula.CAN,
                       CompanyName = companyDetailsFromPeninsula.CompanyName,
                       MainContact = companyDetailsFromPeninsula.MainContact,
                       PostCode = companyDetailsFromPeninsula.PostCode,
                       Telephone = companyDetailsFromPeninsula.Telephone,
                       Website = companyDetailsFromPeninsula.Website,
                       BusinessSafeContactId = companyDetailsFromBusinessSafe != null ?  companyDetailsFromBusinessSafe.BusinessSafeContactEmployeeId : null,
                       BusinessSafeContact = companyDetailsFromBusinessSafe != null ? companyDetailsFromBusinessSafe.BusinessSafeContactEmployeeFullName : null
                   };

            return viewModel;
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditCompanyDetails)]
        public ActionResult SaveAndNotifyAdmin(CompanyDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var request = MapCompanyDetailsViewModelToCompanyDetailsRequest(model);

                _companyDetailsService.Update(request);
                _businessSafeCompanyDetailService.UpdateBusinessSafeContact(request);

                TempData["Message"] = "A member of Client Services has been notified and will be in contact in due course";

                return RedirectToAction("Index", new { id = model.Id });
            }

            ViewBag.LabelVisibility = "hide";
            ViewBag.TextBoxVisibility = string.Empty;

            return View("Index", model);
        }

        private CompanyDetailsRequest MapCompanyDetailsViewModelToCompanyDetailsRequest(CompanyDetailsViewModel model)
        {

            var existingCompanyDetailsFromPeninsula = _companyDetailsService.GetCompanyDetails(CurrentUser.CompanyId);
            var existingCompanyDetailsFromBusinessSafe = _businessSafeCompanyDetailService.Get(CurrentUser.CompanyId);

            var request = new CompanyDetailsRequest()
                          {
                              ActioningUserName = CurrentUser.FullName,
                              UserId = CurrentUser.UserId,
                              CAN = model.CAN,
                              Id = model.Id,
                              NewCompanyDetails = new CompanyDetailsInformation()
                              {
                                  AddressLine1 = model.AddressLine1,
                                  AddressLine2 = model.AddressLine2,
                                  AddressLine3 = model.AddressLine3,
                                  AddressLine4 = model.AddressLine4,
                                  CompanyName = model.CompanyName,
                                  MainContact = model.MainContact,
                                  Postcode = model.PostCode,
                                  Telephone = model.Telephone,
                                  Website = model.Website,
                                  BusinessSafeContactId = model.BusinessSafeContactId.HasValue ? model.BusinessSafeContactId.Value : new Guid(),
                                  BusinessSafeContactName = model.BusinessSafeContact
                              },
                              ExistingCompanyDetails = new CompanyDetailsInformation()
                              {
                                  AddressLine1 = existingCompanyDetailsFromPeninsula.AddressLine1,
                                  AddressLine2 = existingCompanyDetailsFromPeninsula.AddressLine2,
                                  AddressLine3 = existingCompanyDetailsFromPeninsula.AddressLine3,
                                  AddressLine4 = existingCompanyDetailsFromPeninsula.AddressLine4,
                                  CompanyName = existingCompanyDetailsFromPeninsula.CompanyName,
                                  MainContact = existingCompanyDetailsFromPeninsula.MainContact,
                                  Postcode = existingCompanyDetailsFromPeninsula.PostCode,
                                  Telephone = existingCompanyDetailsFromPeninsula.Telephone,
                                  Website = existingCompanyDetailsFromPeninsula.Website,
                                  BusinessSafeContactId = existingCompanyDetailsFromBusinessSafe != null && existingCompanyDetailsFromBusinessSafe.BusinessSafeContactEmployeeId.HasValue 
                                    ? existingCompanyDetailsFromBusinessSafe.BusinessSafeContactEmployeeId.Value
                                    : Guid.Empty,
                                  BusinessSafeContactName = existingCompanyDetailsFromBusinessSafe != null ? existingCompanyDetailsFromBusinessSafe.BusinessSafeContactEmployeeFullName : null
                              }
                          };
            return request;
        }
    }
}
