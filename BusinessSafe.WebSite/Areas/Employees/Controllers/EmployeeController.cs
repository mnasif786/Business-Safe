using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Employees.Factories;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Controllers.AutoMappers;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.PeninsulaOnline;

namespace BusinessSafe.WebSite.Areas.Employees.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeViewModelFactory _employeeViewModelFactory;
        private readonly INewRegistrationRequestService _newRegistrationRequestService;
        private readonly IRiskAssessorService _riskAssessorService;
        private readonly IUserService _userService;
      
        public EmployeeController(IEmployeeService employeeService, IEmployeeViewModelFactory employeeViewModelFactory,  INewRegistrationRequestService newRegistrationRequestService, IUserService userService)
        {
            _employeeService = employeeService;
            _employeeViewModelFactory = employeeViewModelFactory;
            _newRegistrationRequestService = newRegistrationRequestService;
            _userService = userService;
        }

        [PermissionFilter(Permissions.ViewEmployeeRecords)]
        public ActionResult Index(Guid? employeeId, long companyId)
        {
            var viewModel = _employeeViewModelFactory
                                .WithEmployeeId(employeeId)
                                .WithCompanyId(companyId)
                                .WithCurrentUser(CurrentUser)
                                .GetViewModel();            
            return View(viewModel);
        }

        [PermissionFilter(Permissions.ViewEmployeeRecords)]
        public ActionResult View(Guid? employeeId, long companyId)
        {
            IsReadOnly = true;
            var viewModel = _employeeViewModelFactory
                                .WithEmployeeId(employeeId)
                                .WithCompanyId(companyId)
                                .WithCurrentUser(CurrentUser)
                                .GetViewModel();
            return View("Index", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddEmployeeRecords)]
        public ActionResult Create(EmployeeViewModel employeeViewModel)
        {               
            if (!ModelState.IsValid)
            {
                return ReturnInvalidSaveEmployeeViewResult(employeeViewModel);
            }
            
            var addEmployeeRequest = new AddEmployeeRequestMapper().Map(employeeViewModel, CurrentUser.UserId);
            var response =_employeeService.Add(addEmployeeRequest);

            if(response.Success)
            {          
                return RedirectToEmployeeIndexViewResult(response.EmployeeId, employeeViewModel.CompanyId);
            }
            {
                response.Errors.ToList()
                    .ForEach(
                        err => ModelState.AddModelError(err.PropertyName, err.ErrorMessage));
                return ReturnInvalidSaveEmployeeViewResult(employeeViewModel);
            }
        }
        
        [HttpPost]
        [PermissionFilter(Permissions.EditEmployeeRecords)]
        public ActionResult Update(EmployeeViewModel employeeViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetErrorMessagesWithPropertyName().Distinct().ToArray();
                return ReturnInvalidSaveEmployeeViewResult(employeeViewModel);
            }
            
            var updateEmployeeRequest = new UpdateEmployeeRequestMapper().Map(employeeViewModel, CurrentUser.UserId);

            var response = _employeeService.Update(updateEmployeeRequest);
            
            if (response.Success)
            {
                if (employeeViewModel.UserId == Guid.Empty)
                {
                        // attempt to create user
                        if (!CreateUpdateReinstateUser(employeeViewModel))
                            return ReturnInvalidSaveEmployeeViewResult(employeeViewModel);
                }

                return RedirectToEmployeeIndexViewResult(employeeViewModel.EmployeeId.GetValueOrDefault(), employeeViewModel.CompanyId);
            }
            {
                response.Errors.ToList()
                    .ForEach(
                        err => ModelState.AddModelError(err.PropertyName, err.ErrorMessage));
                return ReturnInvalidSaveEmployeeViewResult(employeeViewModel);
            }
        }

        private bool CreateUpdateReinstateUser(EmployeeViewModel employeeViewModel)
        {        
            if ( (!employeeViewModel.UserRoleId.HasValue  || employeeViewModel.UserRoleId.Value == Guid.Empty ) 
                || (!employeeViewModel.UserSiteId.HasValue || employeeViewModel.UserSiteId.Value == 0 )
                && (!employeeViewModel.UserSiteGroupId.HasValue || employeeViewModel.UserSiteGroupId.Value == 0)
                && (!employeeViewModel.UserPermissionsApplyToAllSites)
                )
            {
                // assume we aren't trying to create a userCreateUser
                return true;
            }

            EmployeeDto employee = _employeeService.GetEmployee(employeeViewModel.EmployeeId.Value, employeeViewModel.CompanyId);

            if (employee.User == null)
            {
                //CREATING NEW USER
                var registerEmployeeAsUserRequest = AddUserForEmployeeMapper.Map(employeeViewModel, CurrentUser.UserId);
                
                if (!RegistrationAttemptIsValid(employee, registerEmployeeAsUserRequest))
                    return false;

                RegisterEmployeeAsUser(employee, registerEmployeeAsUserRequest);
            } else if (employee.User.Deleted)
            {
                // REINSTATE USER
                var user = _userService.GetByIdAndCompanyIdIncludeDeleted(employee.User.Id, employee.User.CompanyId);

                if (_newRegistrationRequestService.HasEmailBeenRegistered(user.Employee.MainContactDetails.Email))
                {
                    ModelState.AddModelError("User", "Sorry you are unable to reinstate this user: the email address has been registered to another user");
                    return true;
                }
                
                _userService.ReinstateUser(employee.User.Id, CurrentUser.CompanyId, CurrentUser.UserId);
            }
            else
            {
                //UPDATING USER
                //set role and site
                var roleSiteRequest = new SetUserRoleAndSiteRequest();
                roleSiteRequest.ActioningUserId = CurrentUser.UserId;
                roleSiteRequest.CompanyId = CurrentUser.CompanyId;
                roleSiteRequest.RoleId = employeeViewModel.UserRoleId.Value;
                roleSiteRequest.SiteId = employeeViewModel.UserSiteId ?? employeeViewModel.UserSiteGroupId ?? 0;
                roleSiteRequest.UserToUpdateId = employee.User != null ? employee.User.Id : new Guid();
                roleSiteRequest.PermissionsApplyToAllSites = employeeViewModel.UserPermissionsApplyToAllSites;
               
                _userService.SetRoleAndSite(roleSiteRequest);
            }
            
            return true;
        }

        //TODO: refactor - move to common area and share with AddUsersController
        /*
         *  Lifted from AddUsersController         
         */
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

        //TODO: refactor - move to common area and share with AddUsersController
        /*
         *  Lifted from AddUsersController         
         */
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
        
        [HttpPost]
        [PermissionFilter(Permissions.DeleteEmployeeRecords)]
        public JsonResult MarkEmployeeAsDeleted(long companyId, string employeeId)
        {
            if (companyId == 0 || string.IsNullOrEmpty(employeeId))
            {
                throw new ArgumentException("Invalid employeeId or companyId when trying to mark employee as deleted.");
            }

           _employeeService.MarkEmployeeAsDeleted(new MarkEmployeeAsDeletedRequest() { EmployeeId = Guid.Parse(employeeId), CompanyId = companyId, UserId = CurrentUser.UserId});

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditEmployeeRecords)]
        public JsonResult ReinstateDeletedEmployee(long companyId, string employeeId)
        {
            if (companyId == 0 || string.IsNullOrEmpty(employeeId))
            {
                throw new ArgumentException("Invalid employeeId or companyId when trying to reinstate employee.");
            }

            _employeeService.ReinstateEmployeeAsNotDeleted(new ReinstateEmployeeAsNotDeleteRequest(){ EmployeeId = Guid.Parse(employeeId), CompanyId = companyId, UserId = CurrentUser.UserId});

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditEmployeeRecords)]
        public JsonResult UpdateOnlineRegistrationDetails(long companyId, string employeeId, string email, string telephone, string mobile )
        {
            if (companyId == 0 || string.IsNullOrEmpty(employeeId))
            {
                throw new ArgumentException("Invalid employeeId or companyId when trying to update online registration details.");
            }
            
            _employeeService.UpdateOnlineRegistrationDetails(new UpdateOnlineRegistrationDetailsRequest() { EmployeeId = Guid.Parse(employeeId), CompanyId = companyId,  
                                                                  CurrentUserId = CurrentUser.UserId, Email = email, Mobile = mobile,
                                                                  Telephone = telephone}); 
            return Json(new { Success = true });
        }

        private ActionResult ReturnInvalidSaveEmployeeViewResult(EmployeeViewModel employeeViewModel)
        {
            var viewModel = _employeeViewModelFactory
                .WithEmployeeId(employeeViewModel.EmployeeId)
                .WithCompanyId(employeeViewModel.CompanyId)
                .GetViewModel();

            return View("Index", viewModel);
        }

        private ActionResult RedirectToEmployeeIndexViewResult(Guid employeeId, long companyId)
        {
            return RedirectToAction("Index", "Employee", new {employeeId, companyId });
        }
    }
}