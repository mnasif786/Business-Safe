using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.VisitRequest;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.VisitRequest.Factories;
using BusinessSafe.WebSite.Areas.VisitRequest.ViewModels;

using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using NHibernate.Linq;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Areas.VisitRequest.Controllers
{
    public class VisitRequestController :  BaseController
    {
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;        
        private readonly IVisitRequestService _visitRequestService;
        private readonly IVisitRequestViewModelFactory _visitRequestViewModelFactory;

        public VisitRequestController(IBusinessSafeSessionManager businessSafeSessionManager, IVisitRequestService visitRequestService, IVisitRequestViewModelFactory VisitRequestViewModelFactory)
        {
            _businessSafeSessionManager = businessSafeSessionManager;
           _visitRequestService = visitRequestService;
            _visitRequestViewModelFactory = VisitRequestViewModelFactory;
        }

        [PermissionFilter(Permissions.ViewCompanyDefaults)]
        public ActionResult Index()
        {
            var viewModel = _visitRequestViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithAllowedSiteIds(CurrentUser.GetSitesFilter())
                .WithPersonToVisit(CurrentUser.FullName)
                .WithEmailAddress(CurrentUser.Email)
                .WithVisitTimePreference("AM") //Default
                .GetViewModel();

            ViewBag.Message = TempData["Message"];

            return View("Index", viewModel);
        }
        
      
        [PermissionFilter(Permissions.ViewCompanyDefaults)]
        public ActionResult Create(VisitRequestViewModel postedViewModel)
        {
          var viewModel = _visitRequestViewModelFactory
                                       .WithCompanyId(CurrentUser.CompanyId)
                                       .WithAllowedSiteIds(CurrentUser.GetSitesFilter())
                                       .GetViewModel(postedViewModel);

           postedViewModel.Validate(new ValidationContext(this, null, null), ModelState);

            if (ModelState.IsValid)
            {
                var request = MapVisitRequestViewModelToRequestVisitRequest(viewModel);
                SentVisitRequestNotification(request);
                TempData["Message"] = "Thank you for your request. A member of our BusinessSafe diary booking team will be in contact with you shortly to confirm your booking.";
                return RedirectToAction("Index");  
            }

            return View("Index", viewModel);
        }

        private void SentVisitRequestNotification(RequestVisitRequest request)
        {
            using (var session = _businessSafeSessionManager.Session)
            {
                _visitRequestService.SendVisitRequestedEmail(request);
                _businessSafeSessionManager.CloseSession();
            }
        }

        private RequestVisitRequest MapVisitRequestViewModelToRequestVisitRequest(VisitRequestViewModel model)
        {
            return  new RequestVisitRequest()
            {
                CompanyName = model.CompanyName,
                CAN = model.CAN,
                ContactName = model.PersonNameToVisit,
                ContactEmail = model.EmailAddress,
                ContactPhone = model.ContactNumber,
                DateTo = model.VisitTo,
                DateFrom = model.VisitFrom,
                RequestTime = model.VisitTimePreference,
                Comments = model.Comments,
                SiteName = model.SiteName,
                Postcode = model.Postcode
            };
        }

       
    }
}


         
     