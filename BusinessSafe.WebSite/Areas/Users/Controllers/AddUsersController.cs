using System;
using System.Configuration;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Controllers.AutoMappers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.PeninsulaOnline;
using NServiceBus;
using Peninsula.Online.Messages.Commands;

namespace BusinessSafe.WebSite.Areas.Users.Controllers
{
    public class AddUsersController : BaseController
    {
        private readonly IAddUsersViewModelFactory _addUsersViewModelFactory;
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;
        private readonly INewRegistrationRequestService _newRegistrationRequestService;
        private readonly ICacheHelper _cacheHelper;
        private readonly IBus _bus;

        public AddUsersController(
            IAddUsersViewModelFactory addUsersViewModelFactory, 
            IUserService userService, 
            IEmployeeService employeeService,
            INewRegistrationRequestService newRegistrationRequestService, 
            ICacheHelper cacheHelper,
            IBus bus)
        {
            _addUsersViewModelFactory = addUsersViewModelFactory;
            _userService = userService;
            _employeeService = employeeService;
            _newRegistrationRequestService = newRegistrationRequestService;
            _cacheHelper = cacheHelper;
            _bus = bus;
        }

        [PermissionFilter(Permissions.ViewUsers)]
        public ActionResult Index(long companyId, Guid? employeeId, bool? saveSuccess)
        {
            var canChangeEmployee = !employeeId.HasValue;
            var viewModel = _addUsersViewModelFactory
                .WithCurrentUser(CurrentUser)
                .GetViewModel(companyId, employeeId, saveSuccess, canChangeEmployee);
            return View(viewModel);
        }

        [PermissionFilter(Permissions.ViewUsers)]
        public PartialViewResult GetUserPermissionsEmployee(long companyId, Guid? employeeId)
        {
            var viewModel = _addUsersViewModelFactory
                .WithCurrentUser(CurrentUser)
                .GetViewModel(companyId, employeeId, false, true);

            return PartialView("_UserPermissionsSelection", viewModel);
        }

        [PermissionFilter(Permissions.AddUsers)]
        public ActionResult CreateUser(AddUsersViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", RehydrateViewModel(viewModel));
            }

            CreateEmployeeAsUserRequest registerEmployeeAsUserRequest = new RegisterEmployeeAsUserRequestMapper().Map(viewModel, CurrentUser.UserId);
            var employee = _employeeService.GetEmployee(viewModel.EmployeeId, CurrentUser.CompanyId);

            if (!RegistrationAttemptIsValid(employee, registerEmployeeAsUserRequest))
                return View("Index", RehydrateViewModel(viewModel));

            RegisterEmployeeAsUser(employee, registerEmployeeAsUserRequest);

            return viewModel.CanChangeEmployeeDdl
                ? RedirectToAction("Index", new { companyId = CurrentUser.CompanyId, saveSuccess = true })
                : RedirectToAction("Index", "ViewUsers", new { companyId = CurrentUser.CompanyId });
        }

        [PermissionFilter(Permissions.EditUsers)]
        public ActionResult UpdateUser(AddUsersViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", RehydrateViewModel(viewModel));
            }

            if (viewModel.IsUserDeleted)
            {
                var user = _userService.GetByIdAndCompanyIdIncludeDeleted(viewModel.UserId, viewModel.CompanyId);
                
                if (_newRegistrationRequestService.HasEmailBeenRegistered(user.Employee.MainContactDetails.Email))
                {
                    ModelState.AddModelError("User", "Sorry you are unable to reinstate this user: the email address has been registered to another user");
                    return View("Index", RehydrateViewModel(viewModel));
                }
                
                _userService.ReinstateUser(viewModel.UserId, CurrentUser.UserId);  
            }

            var request = new SetUserRoleAndSiteRequestMapper().Map(viewModel, CurrentUser.UserId);
            _userService.SetRoleAndSite(request);

            _cacheHelper.RemoveUser(viewModel.UserId); 

            if (viewModel.CanChangeEmployeeDdl)
                return RedirectToAction("Index", new { companyId = viewModel.CompanyId, saveSuccess = true });
            
            return RedirectToAction("Index", "ViewUsers", new { companyId = viewModel.CompanyId });
        }

        private AddUsersViewModel RehydrateViewModel(AddUsersViewModel viewModel)
        {
            return _addUsersViewModelFactory
                .WithCurrentUser(CurrentUser)
                .GetViewModel(viewModel.CompanyId, viewModel.EmployeeId, false, viewModel.CanChangeEmployeeDdl);
        }

        private bool RegistrationAttemptIsValid(EmployeeDto employee, CreateEmployeeAsUserRequest createEmployeeAsUserRequest)
        {

            var validationResult = _employeeService.ValidateRegisterAsUser(createEmployeeAsUserRequest);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return false;
            }

            if (_newRegistrationRequestService.HasEmailBeenRegistered(employee.MainContactDetails.Email))
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Sorry you are unable to create this User: the email address has already been registered"
                );
                return false;
            }
            return true;
        }

        private void RegisterEmployeeAsUser(EmployeeDto employee, CreateEmployeeAsUserRequest request)
        {
            var registerNonAdminUserRequest = new RegisterNonAdminUserRequest
            {
                ClientId = CurrentUser.CompanyId,
                PeninsulaApplicationId = Guid.Parse(ConfigurationManager.AppSettings["BSOGuid"]),
                RegistrationEmail = employee.MainContactDetails.Email,
                TelephoneNumber = employee.MainContactDetails.Telephone1 ?? employee.MainContactDetails.Telephone2
            };

            request.NewUserId = _newRegistrationRequestService.RegisterNonAdminUser(registerNonAdminUserRequest);

            _employeeService.CreateUser(request);
        }
    }
}