using System;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Employees.Factories;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.Employees.Controllers
{
    public class EmployeeEmergencyContactController : BaseController
    {
        private readonly IEmployeeEmergencyContactDetailService _employeeEmergencyContactDetailService;
        private readonly IEmployeeViewModelFactory _employeeViewModelFactory;
        private readonly IViewEmergencyContactViewModelFactory _viewEmergencyContactDetailViewModelFactory;

        public EmployeeEmergencyContactController(
            IEmployeeEmergencyContactDetailService employeeEmergencyContactDetailService, 
            IEmployeeViewModelFactory employeeViewModelFactory,
            IViewEmergencyContactViewModelFactory viewEmergencyContactDetailViewModelFactory)
        {
            _employeeEmergencyContactDetailService = employeeEmergencyContactDetailService;
            _employeeViewModelFactory = employeeViewModelFactory;
            _viewEmergencyContactDetailViewModelFactory = viewEmergencyContactDetailViewModelFactory;
        }

        [PermissionFilter(Permissions.EditEmployeeRecords)]
        public PartialViewResult GetEmergencyContactDetail(Guid employeeId, long emergencyContactId, long companyId)
        {
            var viewModel = _employeeViewModelFactory
                .WithEmployeeId(employeeId)
                .WithCompanyId(companyId)
                .GetViewModel();

            var emergencyContact = viewModel.EmergencyContactDetails.FirstOrDefault(x => x.EmergencyContactId == emergencyContactId);
            var returnViewModel = EmergencyContactDetailViewModel.CreateFrom(emergencyContact, viewModel);
            return PartialView("_EmergencyContactDetails", returnViewModel);
        }

        [PermissionFilter(Permissions.EditEmployeeRecords)]
        public PartialViewResult ViewEmergencyContactDetail(Guid employeeId, int emergencyContactId, long companyId)
        {
            var viewModel = _viewEmergencyContactDetailViewModelFactory
                .WithEmployeeEmergencyContactDetailsId(emergencyContactId)
                .WithCompanyId(companyId)
                .WithEmployeeId(employeeId)
                .GetViewModel();

            return PartialView("_ViewEmergencyContactDetails", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditEmployeeRecords)]
        public JsonResult AddEmergencyContactDetail(EmergencyContactViewModel emergencyContactViewModel)
        {
            _employeeEmergencyContactDetailService.CreateEmergencyContactDetailForEmployee(new CreateEmergencyContactRequest()
                                                                         {
                                                                             EmployeeId = emergencyContactViewModel.EmployeeId,
                                                                             CompanyId = emergencyContactViewModel.CompanyId,
                                                                             Title = emergencyContactViewModel.Title,
                                                                             Forename = emergencyContactViewModel.Forename,
                                                                             Surname = emergencyContactViewModel.Surname,
                                                                             Relationship = emergencyContactViewModel.Relationship,
                                                                             WorkTelephone = emergencyContactViewModel.WorkTelephone,
                                                                             MobileTelephone = emergencyContactViewModel.MobileTelephone,
                                                                             HomeTelephone = emergencyContactViewModel.HomeTelephone,
                                                                             PreferredContactNumber = emergencyContactViewModel.PreferredContactNumber,
                                                                             SameAddressAsEmployee = emergencyContactViewModel.SameAddressAsEmployee,
                                                                             Address1 = emergencyContactViewModel.Address1,
                                                                             Address2 = emergencyContactViewModel.Address2,
                                                                             Address3 = emergencyContactViewModel.Address3,
                                                                             Town = emergencyContactViewModel.Town,
                                                                             County = emergencyContactViewModel.County,
                                                                             CountryId = emergencyContactViewModel.EmergencyContactCountryId.GetValueOrDefault(),
                                                                             PostCode = emergencyContactViewModel.PostCode,
                                                                             UserId = CurrentUser.UserId
                                                                         });
            return Json(new { Success = true });

        }

        [HttpPost]
        [PermissionFilter(Permissions.EditEmployeeRecords)]
        public JsonResult UpdateEmergencyContactDetail(EmergencyContactViewModel emergencyContactViewModel)
        {
            _employeeEmergencyContactDetailService.UpdateEmergencyContactDetailForEmployee(new UpdateEmergencyContactRequest()
                                                                         {
                                                                             EmployeeId = emergencyContactViewModel.EmployeeId,
                                                                             CompanyId = emergencyContactViewModel.CompanyId,
                                                                             EmergencyContactId = emergencyContactViewModel.EmergencyContactId,
                                                                             Title = emergencyContactViewModel.Title,
                                                                             Forename = emergencyContactViewModel.Forename,
                                                                             Surname = emergencyContactViewModel.Surname,
                                                                             Relationship = emergencyContactViewModel.Relationship,
                                                                             WorkTelephone = emergencyContactViewModel.WorkTelephone,
                                                                             MobileTelephone = emergencyContactViewModel.MobileTelephone,
                                                                             HomeTelephone = emergencyContactViewModel.HomeTelephone,
                                                                             PreferredContactNumber = emergencyContactViewModel.PreferredContactNumber,
                                                                             SameAddressAsEmployee = emergencyContactViewModel.SameAddressAsEmployee,
                                                                             Address1 = emergencyContactViewModel.Address1,
                                                                             Address2 = emergencyContactViewModel.Address2,
                                                                             Address3 = emergencyContactViewModel.Address3,
                                                                             Town = emergencyContactViewModel.Town,
                                                                             County = emergencyContactViewModel.County,
                                                                             CountryId = emergencyContactViewModel.EmergencyContactCountryId.GetValueOrDefault(),
                                                                             PostCode = emergencyContactViewModel.PostCode,
                                                                             UserId = CurrentUser.UserId
                                                                         });
            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteEmployeeRecords)]
        public JsonResult MarkEmergencyContactAsDeleted(long companyId, string employeeId , long emergencyContactId)
        {
            if (companyId == 0 || emergencyContactId == 0 || string.IsNullOrEmpty(employeeId))
            {
                throw new ArgumentException("Invalid employeeId, emergencyContactId or companyId when trying to mark emergency contact as deleted.");
            }

            _employeeEmergencyContactDetailService.MarkEmployeeEmergencyContactAsDeleted(new MarkEmployeeEmergencyContactAsDeletedRequest() { EmployeeId = Guid.Parse(employeeId), EmergencyContactId = emergencyContactId, CompanyId = companyId, UserId = CurrentUser.UserId });

            return Json(new { Success = true });
        }
    }
}