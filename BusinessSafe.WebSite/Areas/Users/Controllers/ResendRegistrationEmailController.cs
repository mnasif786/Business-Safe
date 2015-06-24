using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.PeninsulaOnline;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using Peninsula.Online.Messages.Commands;

namespace BusinessSafe.WebSite.Areas.Users.Controllers
{
    public class ResendRegistrationEmailController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private IBus _bus;
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;
        private readonly INewRegistrationRequestService _newRegistrationRequestService;

        public ResendRegistrationEmailController(
            IEmployeeService employeeService,
            IBus bus,
            IBusinessSafeSessionManager businessSafeSessionManager, 
            INewRegistrationRequestService newRegistrationRequestService)
        {
            _employeeService = employeeService;
            _bus = bus;
            _businessSafeSessionManager = businessSafeSessionManager;
            _newRegistrationRequestService = newRegistrationRequestService;
        }

        [PermissionFilter(Permissions.AddUsers)]
        [ILookAfterOwnTransactionFilter]
        public JsonResult Index(ResendUserRegistrationEmailViewModel viewModel)
        {
            return Json(new { Success = false, Errors = @"Not implemented. Superceded by ViewUsersController\UpdateUserRegistration" });

            if (!ModelState.IsValid)
                return Json(new { Success = false, Errors = ModelState.GetErrorMessages() });

            if (_newRegistrationRequestService.HasEmailBeenRegistered(viewModel.Email))
            {
                ModelState.AddModelError(string.Empty, "Sorry you are unable to create this User: the email address has already been registered");
                return Json(new { Success = false, Errors = ModelState.GetErrorMessages() });
            }
            
            using (var session = _businessSafeSessionManager.Session)
            {
                _employeeService.UpdateEmailAddress(new UpdateEmployeeEmailAddressRequest
                                                        {
                                                            EmployeeId = viewModel.EmployeeId,
                                                            Email = viewModel.Email,
                                                            CompanyId = CurrentUser.CompanyId,
                                                            CurrentUserId = CurrentUser.UserId
                                                        });
                _businessSafeSessionManager.CloseSession();
            }

            _bus.Send(new ResetPendingUsersUserName
            {
                UserId = viewModel.UserId,
                Email = viewModel.Email
            });

            return Json(new { Success = true });
        }

  
   }}