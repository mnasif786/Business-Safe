using System;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.PeninsulaOnline;
using NHibernate;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using Peninsula.Online.Messages.Commands;

namespace BusinessSafe.WebSite.Areas.Users.Controllers
{
    public class ViewUsersController : BaseController
    {
        private readonly IUserSearchViewModelFactory _userSearchViewModelFactory;
        private readonly IViewUserViewModelFactory _viewUserViewModelFactory;
        private readonly IUserService _userService;
        private readonly IBus _bus;
        private readonly INewRegistrationRequestService _newRegistrationRequestService;
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;
        private readonly IEmployeeService _employeeService;

        public ViewUsersController(IUserSearchViewModelFactory userSearchViewModelFactory, IViewUserViewModelFactory viewUserViewModelFactory
            , IUserService userService, IBus bus, INewRegistrationRequestService newRegistrationRequestService, IBusinessSafeSessionManager businessSafeSessionManager
            , IEmployeeService employeeService)
        {
            _userSearchViewModelFactory = userSearchViewModelFactory;
            _viewUserViewModelFactory = viewUserViewModelFactory;
            _userService = userService;
            _bus = bus;
            _newRegistrationRequestService = newRegistrationRequestService;
            _businessSafeSessionManager = businessSafeSessionManager;
            _employeeService = employeeService;
        }

        [PermissionFilter(Permissions.ViewUsers)]
        public ActionResult Index(long companyId, string forename = "", string surname = "", long siteId = 0, long siteGroupId = 0, bool showDeleted = false)
        {
            using (var session = _businessSafeSessionManager.Session)
            {
                session.FlushMode = FlushMode.Never;

                var viewModel = _userSearchViewModelFactory
                    .WithCompanyId(companyId)
                    .WithForeName(forename)
                    .WithSurname(surname)
                    .WithSiteId(siteId)
                    .WithSiteGroupId(siteGroupId)
                    .WithShowDeleted(showDeleted)
                    .WithAllowedSiteIds(CurrentUser.GetSitesFilter())
                    .WithCurrentUser(CurrentUser)
                    .GetViewModel();

                return View(viewModel);
            }
        }

        public ViewResult ViewUser(long companyId, Guid employeeId)
        {
            var viewModel = _viewUserViewModelFactory
                .WithCompanyId(companyId)
                .WithEmployeeId(employeeId)
                .GetViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteUsers)]
        public JsonResult MarkUserAsEnabledOrDisabled(long companyId, string userId, bool disabled, string email)
        {
            if (companyId == 0 || string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Invalid userId and companyId.");
            }
            
            if (disabled)
            {
                DeleteUser(companyId, userId);
            }
            else
            {
                ReinstateUser(companyId, userId);
            }

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteUsers)]
        public JsonResult DeleteUser(long companyId, string userId)
        {
            if (companyId == 0 || string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Invalid userId and companyId.");
            }

            _userService.DeleteUser(Guid.Parse(userId), companyId, CurrentUser.UserId);
            _bus.Send(new DeleteUser { UserId = Guid.Parse(userId), ActioningUserId = CurrentUser.UserId});

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteUsers)]
        public JsonResult ReinstateUser(long companyId, string userId)
        {
            if (companyId == 0 || string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Invalid userId and companyId.");
            }

            var user = _userService.GetByIdAndCompanyIdIncludeDeleted(Guid.Parse(userId), companyId);
            if (_newRegistrationRequestService.HasEmailBeenRegistered(user.Employee.MainContactDetails.Email))
            {
                return Json(new { Success = false, Data = "Sorry you are unable to reinstate this user: the email address has been registered to another user" });
            }

            _userService.ReinstateUser(Guid.Parse(userId), companyId, CurrentUser.UserId);

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditUsers)]
        public JsonResult ResetUserRegistration(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Invalid userId.");
            }

            if (Guid.Parse(userId) == Guid.Empty)
            {
                 throw new ArgumentException("Invalid userId.");
            }

            _bus.Send(new ResetUserRegistration { UserId = Guid.Parse(userId), ActioningUserId = CurrentUser.UserId });

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditUsers)]
        public JsonResult UpdateUserRegistration(string userId, string securityAnswer, string email)
        {
            if (string.IsNullOrEmpty(userId.ToString()))
            {
                throw new ArgumentException("Invalid userId.");
            }
            
            using (var session = _businessSafeSessionManager.Session)
            {
                try
                {
                    _userService.UpdateEmailAddress(Guid.Parse(userId), CurrentUser.CompanyId, email, CurrentUser.UserId);
                    _bus.Send(new UpdateUserRegistration
                    {
                        UserId = Guid.Parse(userId),
                        ActioningUserId = CurrentUser.UserId,
                        SecurityAnswer = securityAnswer,
                        Email = email
                    });

                }
                catch (Exception ex) 
                {
                    return Json(new{Success = false,Errors = ex.Message});
                }

                _businessSafeSessionManager.CloseSession();
            }

            return Json(new {Success = true});
        }
    }
}