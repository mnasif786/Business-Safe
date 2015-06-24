using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Messages.Commands;
using BusinessSafe.Messages.Commands.GenerateEmployeeChecklistEmailsParameters;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using NServiceBus;
using System.Transactions;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using System.Text.RegularExpressions;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers
{
    [PersonalRiskAssessmentCurrentTabActionFilter(PersonalRiskAssessmentTabs.ChecklistGenerator)]
    [PersonalRiskAssessmentContextFilter]
    public class ChecklistGeneratorController : BaseController
    {
        private readonly IEmployeeChecklistGeneratorViewModelFactory _checklistGeneratorViewModelFactory;
        private readonly IEmployeeService _employeeService;
        private readonly IPersonalRiskAssessmentService _personalRiskAssessmentService;
        private readonly ISelectedEmployeeViewModelFactory _selectedEmployeeViewModelFactory;
        private readonly IBus _bus;
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;

        public ChecklistGeneratorController(IEmployeeChecklistGeneratorViewModelFactory checklistGeneratorViewModelFactory,
                                            IEmployeeService employeeService,
                                            IPersonalRiskAssessmentService personalRiskAssessmentService,
                                            ISelectedEmployeeViewModelFactory selectedEmployeeViewModelFactory,
                                            IBus bus,
                                            IBusinessSafeSessionManager businessSafeSessionManager)
        {
            _checklistGeneratorViewModelFactory = checklistGeneratorViewModelFactory;
            _employeeService = employeeService;
            _personalRiskAssessmentService = personalRiskAssessmentService;
            _selectedEmployeeViewModelFactory = selectedEmployeeViewModelFactory;
            _bus = bus;
            _businessSafeSessionManager = businessSafeSessionManager;
        }

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public ActionResult Index(long riskAssessmentId, long companyId)
        {
            var model = _checklistGeneratorViewModelFactory
                .WithCompanyId(companyId)
                .WithRiskAssessmentId(riskAssessmentId)
                .WithRiskAssessorEmail(CurrentUser.Email)
                .WithCurrentUserId(CurrentUser.UserId)
                .GetViewModel();

            return View("Index", model);
        }

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public PartialViewResult GetEmployeeMultiSelect(long companyId, long? siteId)
        {
            var model = _employeeService.Search(new SearchEmployeesRequest
                                                    {
                                                        CompanyId = companyId,
                                                        SiteIds = siteId.HasValue ? new long[] {siteId.Value} : null
                                                    });

            return PartialView("_EmployeeMultiSelect", model);
        }

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public PartialViewResult AddSelectedEmployees(long riskAssessmentId, Guid[] employeeIds)
        {
            var request = new AddEmployeesToChecklistGeneratorRequest()
                              {
                                  CompanyId = CurrentUser.CompanyId,
                                  CurrentUserId = CurrentUser.UserId,
                                  EmployeeIds = employeeIds.ToList(),
                                  PersonalRiskAssessmentId = riskAssessmentId
                              };
           
            _personalRiskAssessmentService.AddEmployeesToChecklistGenerator(request);

            var model = _selectedEmployeeViewModelFactory.WithCompanyId(CurrentUser.CompanyId).WithRiskAssessmentId(riskAssessmentId).WithUserId(CurrentUser.UserId)
                .GetViewModel();

            return PartialView("_EmployeeSelected", model);

        }
            
        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public ActionResult View(long riskAssessmentId, long companyId)
        {
            IsReadOnly = true;
            return Index(riskAssessmentId, companyId);
        }

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public JsonResult GetEmail(Guid employeeId)
        {
            var employee = _employeeService.GetEmployee(employeeId, CurrentUser.CompanyId);
            var email = employee.MainContactDetails != null ? employee.MainContactDetails.Email : null;
            return Json(new { email }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
        public JsonResult ValidateGenerate(EmployeeChecklistGeneratorViewModel viewModel)
        {
            var validationErrors = viewModel.Validate(new ValidationContext(viewModel, null, null), ModelState);

            var errors = validationErrors == null ? null : validationErrors.Select(x => x.ErrorMessage).ToList();

            return Json(new { success = !validationErrors.Any(), errors = errors });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
        [ILookAfterOwnTransactionFilter]
        public ActionResult Generate(EmployeeChecklistGeneratorViewModel viewModel)
        {
            viewModel.Validate(new ValidationContext(viewModel, null, null), ModelState);

            if (!ModelState.IsValid)
            {
                var invalidViewModel = _checklistGeneratorViewModelFactory
                                           .WithRiskAssessmentId(viewModel.RiskAssessmentId)
                                           .WithCompanyId(viewModel.CompanyId)
                                           .WithCurrentUserId(CurrentUser.UserId)
                                           .WithRiskAssessorEmail(CurrentUser.Email)
                                           .GetViewModel(viewModel);

                return View("Index", invalidViewModel);
            }

            
            using (var session = _businessSafeSessionManager.Session)
            {
                _personalRiskAssessmentService.SetAsGenerating(viewModel.RiskAssessmentId);
                _personalRiskAssessmentService.ResetChecklistAfterGenerate(new ResetAfterChecklistGenerateRequest()
                {
                    CompanyId = viewModel.CompanyId,
                    CurrentUserId = CurrentUser.UserId,
                    PersonalRiskAssessmentId =
                        viewModel.RiskAssessmentId
                });
                
                _businessSafeSessionManager.CloseSession();
            }
              
            
            // Temp measure till we get DTC going correctly
            var createGenerateEmployeeChecklistEmailsCommand = CreateGenerateEmployeeChecklistEmailsCommand(viewModel);
            _bus.Send(createGenerateEmployeeChecklistEmailsCommand);
            
            
            TempData["Notice"] = "Employee Checklists Sent";
            return RedirectToAction("Index", new { viewModel.RiskAssessmentId, viewModel.CompanyId });
        }

        private GenerateEmployeeChecklistEmails CreateGenerateEmployeeChecklistEmailsCommand(EmployeeChecklistGeneratorViewModel viewModel)
        {
            var command = new GenerateEmployeeChecklistEmails
                              {
                                  GeneratingUserId = CurrentUser.UserId,
                                  Message = viewModel.Message,
                                  RiskAssessmentId = viewModel.RiskAssessmentId,
                                  RequestEmployees = viewModel.ChecklistsToGenerate.RequestEmployees.Select(x => new EmployeeWithNewEmail
                                                                                                                     {
                                                                                                                         EmployeeId = x.EmployeeId,
                                                                                                                         NewEmail = x.NewEmail
                                                                                                                     }).ToList(),
                                  ChecklistIds = viewModel.ChecklistsToGenerate.ChecklistIds.ToList(),
                                  SendCompletedChecklistNotificationEmail = viewModel.SendCompletedChecklistNotificationEmail,
                                  CompletionDueDateForChecklists = viewModel.CompletionDueDateForChecklists,
                                  CompletionNotificationEmailAddress = viewModel.CompletionNotificationEmailAddress
                              };

            if (viewModel.NewEmployeeEmailVisible)
            {
                command.RequestEmployees[0].NewEmail = viewModel.NewEmployeeEmail;
            }
            return command;
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
        public ActionResult Save(EmployeeChecklistGeneratorViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var returnedViewModel = _checklistGeneratorViewModelFactory
                                                .WithRiskAssessmentId(viewModel.RiskAssessmentId)
                                                .WithCompanyId(viewModel.CompanyId)
                                                .WithCurrentUserId(CurrentUser.UserId)
                                                .GetViewModel(viewModel);

                return View("Index", returnedViewModel);
            }

            UpdateChecklistGenerator(viewModel);
            TempData["Notice"] = "Checklist Generator Successfully Updated";
            return RedirectToAction("Index", new { viewModel.RiskAssessmentId, viewModel.CompanyId });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
        public JsonResult SaveAndNext(EmployeeChecklistGeneratorViewModel viewModel)
        {
            UpdateChecklistGenerator(viewModel);
            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
        public JsonResult ResendEmployeeChecklist(ResendEmployeeChecklistViewModel viewModel)
        {
            _bus.Send(new ResendEmployeeChecklistEmail
                         {
                             EmployeeChecklistId = viewModel.EmployeeChecklistId,
                             ResendUserId = CurrentUser.UserId,
                             RiskAssessmentId = viewModel.RiskAssessmentId
                         });
            return Json(new { Success = true });
        }

        private void UpdateChecklistGenerator(EmployeeChecklistGeneratorViewModel viewModel)
        {
            var request = new SaveChecklistGeneratorRequest()
            {
                Message = viewModel.Message,
                PersonalRiskAssessmentId = viewModel.RiskAssessmentId,
                CurrentUserId = CurrentUser.UserId,
                RequestEmployees = viewModel.ChecklistsToGenerate.RequestEmployees,
                HasMultipleChecklistRecipients = viewModel.ChecklistsToGenerate.HasMultipleChecklistRecipients,
                ChecklistIds = viewModel.ChecklistsToGenerate.ChecklistIds,
                SendCompletedChecklistNotificationEmail = viewModel.SendCompletedChecklistNotificationEmail,
                CompletionDueDateForChecklists = viewModel.CompletionDueDateForChecklists,
                CompletionNotificationEmailAddress = viewModel.CompletionNotificationEmailAddress
            };
            _personalRiskAssessmentService.SaveChecklistGenerator(request);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
        public JsonResult RemoveEmployee(long riskAssessmentId, Guid employeeId)
        {
            _personalRiskAssessmentService.RemoveEmployeeFromCheckListGenerator(riskAssessmentId,CurrentUser.CompanyId, employeeId, CurrentUser.UserId);
            return Json(new {Success = true});
        }
    }
}
