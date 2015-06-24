using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Company.Factories;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.Company.Controllers
{
    public class AccidentRecordDistributionController : BaseController
    {
        private readonly IAccidentRecordDistributionListModelFactory _accidentRecordDistributionListModelFactory;
        private readonly ISiteService _siteService;
        private readonly IEmployeeService _employeeService;

        public AccidentRecordDistributionController(IAccidentRecordDistributionListModelFactory accidentRecordDistributionListModelFactory, ISiteService siteService, IEmployeeService employeeService)
        {
            _accidentRecordDistributionListModelFactory = accidentRecordDistributionListModelFactory;
            _siteService = siteService;
            _employeeService = employeeService;
        }

        //
        // GET: /Company/AccidentRecordDistribution/
        [PermissionFilter(Permissions.ViewCompanyDefaults)]
        public ActionResult Index(long companyId)
        {
            AccidentRecordDistributionListViewModel viewModel =
                _accidentRecordDistributionListModelFactory
                    .WithCompanyId(companyId)
                    .WithAllowedSites(CurrentUser.GetSitesFilter())
                    .GetViewModel();
                        
            return PartialView("_DistributionList", viewModel);            
        }

        [PermissionFilter(Permissions.EditCompanyDetails)]
        public PartialViewResult GetEmployeeMultiSelect(long companyId, long? siteId)
        {         
            var model = _accidentRecordDistributionListModelFactory
                  .WithCompanyId(CurrentUser.CompanyId)
                  .WithAllowedSites(CurrentUser.GetSitesFilter())
                  .WithSiteId(siteId.Value)
                  .GetViewModel();

            return PartialView("_EmployeeMultiSelect", model);
        }

        [PermissionFilter(Permissions.EditCompanyDetails)]
        public PartialViewResult AddSelectedEmployees(long siteId, Guid[] employeeIds)
        {          
            foreach (Guid id in employeeIds)
            {
                _siteService.AddAccidentRecordNotificationMemberToSite(siteId, id, CurrentUser.UserId);
            }

            var model = _accidentRecordDistributionListModelFactory
                    .WithCompanyId(CurrentUser.CompanyId)
                    .WithAllowedSites(CurrentUser.GetSitesFilter())
                    .WithSiteId(siteId)
                    .GetViewModel();            

            return PartialView("_EmployeeMultiSelect", model);
        }

        [PermissionFilter(Permissions.EditCompanyDetails)]
        public PartialViewResult AddNonEmployee(long siteId, string nonEmployeeName, string nonEmployeeEmail)
        {          
            _siteService.AddNonEmployeeToAccidentRecordNotificationMembers(siteId, nonEmployeeName, nonEmployeeEmail, CurrentUser.UserId);
            
            var model = _accidentRecordDistributionListModelFactory
                    .WithCompanyId(CurrentUser.CompanyId)
                    .WithAllowedSites(CurrentUser.GetSitesFilter())
                    .WithSiteId(siteId)
                    .GetViewModel();            

            return PartialView("_EmployeeMultiSelect", model);
        }
        

        [PermissionFilter(Permissions.EditCompanyDetails)]
        public PartialViewResult RemoveSelectedEmployee(long siteId, Guid employeeId, string nonEmployeeEmail)
        {
            // SGG: This should be refactored to use an id for the base class type, rather than deciding based on
            // whether the employeeId is defined
            if (employeeId != Guid.Empty)
            {
                _siteService.RemoveAccidentRecordNotificationMemberFromSite(siteId, employeeId, CurrentUser.UserId);
            }
            else
            {
                _siteService.RemoveNonEmployeeAccidentRecordNotificationMemberFromSite(siteId, nonEmployeeEmail,
                                                                                       CurrentUser.UserId);
            }
            var model = _accidentRecordDistributionListModelFactory
                    .WithCompanyId(CurrentUser.CompanyId)
                    .WithAllowedSites(CurrentUser.GetSitesFilter())
                    .WithSiteId(siteId)
                    .GetViewModel();

            return PartialView("_EmployeeMultiSelect", model);
        }      

        [PermissionFilter(Permissions.EditCompanyDetails)]
        public JsonResult UpdateEmployeeEmails(List<EmployeeEmailsViewModel> employeeEmailsViewModel )
        {
            if (employeeEmailsViewModel != null)
            {
                if (employeeEmailsViewModel.Any(x => !IsValidEmailAddress(x.Email)))
                {
                    return Json(new {Success = false, Error = "Valid email address is required"});
                }

                employeeEmailsViewModel
                    .Where(x => !string.IsNullOrEmpty(x.Email))
                    .ToList()
                    .ForEach(x =>
                                 {
                                     //update employee record                               
                                     UpdateEmployeeEmailAddressRequest request = new UpdateEmployeeEmailAddressRequest();
                                     request.CompanyId = CurrentUser.CompanyId;
                                     request.CurrentUserId = CurrentUser.UserId;
                                     request.Email = x.Email;
                                     request.EmployeeId = x.EmployeeId;

                                     _employeeService.UpdateEmailAddress(request);
                                 });

            }
           
            return Json(new { Success = true, Message = "Accident Distribution List Successfully Updated" });
        }

        private bool IsValidEmailAddress(string emailAddress)
        {
            if (String.IsNullOrEmpty(emailAddress))
                return false;

            return Regex.Match(emailAddress, @"^[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$").Success;
        }        
    }

    public class EmployeeEmailsViewModel
    {
        public Guid EmployeeId { get; set; }
        public string Email { get; set; }
    }
}