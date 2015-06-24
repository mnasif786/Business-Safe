using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.ActionPlans.Factories;
using BusinessSafe.WebSite.Areas.ActionPlans.Tasks;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Areas.ActionPlans.Controllers
{
    public class ImmediateRiskNotificationsActionsController : BaseController
    {
        private readonly ISearchActionViewModelFactory _searchActionViewModelFactory;
        private readonly IAssignActionPlanTaskCommand _assignActionPlanTaskCommand;
        private readonly ICompleteActionTaskViewModelFactory _completeActionTaskViewModelFactory;
        private readonly IReassignActionTaskViewModelFactory _reassignActionTaskViewModelFactory;
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;
        private readonly IActionTaskService _actionTaskService;
        private readonly IReassignFurtherControlMeasureTaskViewModelFactory _reassignTaskViewModelFactory;

        public ImmediateRiskNotificationsActionsController(ISearchActionViewModelFactory searchActionViewModelFactory,
                                                           ICompleteActionTaskViewModelFactory
                                                               completeActionTaskViewModelFactory,
                                                           IAssignActionPlanTaskCommand assignActionPlanTaskCommand,
                                                           IReassignActionTaskViewModelFactory reassignActionTaskViewModelFactory,
                                                           IActionTaskService actionTaskService,
                                                           IReassignFurtherControlMeasureTaskViewModelFactory reassignTaskViewModelFactory,
                                                           IBusinessSafeSessionManager businessSafeSessionManager)
        {
            _searchActionViewModelFactory = searchActionViewModelFactory;
            _assignActionPlanTaskCommand = assignActionPlanTaskCommand;
            _completeActionTaskViewModelFactory = completeActionTaskViewModelFactory;
            _reassignActionTaskViewModelFactory = reassignActionTaskViewModelFactory;
            _reassignTaskViewModelFactory = reassignTaskViewModelFactory;
            _businessSafeSessionManager = businessSafeSessionManager;

            _actionTaskService = actionTaskService;
        }

        //
        // GET: /ImmediateRiskNotificationsAndActions/Actions/
        
        [PermissionFilter(Permissions.ViewActionPlan)]
        public ActionResult Index()
        {
            return View();
        }

        [PermissionFilter(Permissions.ViewActionPlan)]
        public ActionResult View(long actionPlanId, long companyId)
        {
            var viewModel = _searchActionViewModelFactory
                                .WithCompanyId(CurrentUser.CompanyId)
                                .WithActionPlanId(actionPlanId)
                                .GetViewModel();

            return View("Index", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditActionPlan)]
        public JsonResult AssignTask(AssignActionPlanTaskViewModel model)
        {
            JsonResult result;

            ValidateAssignActionPlanTaskViewModel(model);

            if (!ModelState.IsValid)
            {
                result = ModelStateErrorsAsJson();
            }
            else
            {
                result = Json(new {Success = true});

                ExecuteAssignActionTaskCommand(model);
            }
            return result;
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditActionPlan)]
        public JsonResult BulkAssignTask(List<AssignActionPlanTaskViewModel> models)
        {
            JsonResult result;

            ValidateAssignActionPlanTaskViewModel(models);

            if (!ModelState.IsValid)
            {
                result = ModelStateErrorsAsJson();
            }
            else
            {
                result = Json(new { Success = true });

                foreach (var model in models)
                {
                    ExecuteAssignActionTaskCommand(model);
                }
            }
            return result;
        }


        [PermissionFilter(Permissions.ViewActionPlan)]
        public PartialViewResult Complete(long companyId, long taskId)
        {
            var viewModel = _completeActionTaskViewModelFactory
                .WithCompanyId(companyId)
                .WithActionTaskId(taskId)
                .GetViewModel();

            return PartialView("_CompleteActionTask", viewModel);
        }

        [PermissionFilter(Permissions.ViewActionPlan)]
        public PartialViewResult CompletedTaskView(long companyId, long taskId)
        {
            var viewModel = _completeActionTaskViewModelFactory
                .WithCompanyId(companyId)
                .WithActionTaskId(taskId)
                .GetViewModel();

            return PartialView("_ViewCompletedActionTask", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.ViewResponsibilities)]
        public JsonResult Complete(CompleteActionTaskViewModel viewModel, DocumentsToSaveViewModel documentsToSaveViewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid Arguments To Complete Action Task");
            }

            var request = new CompleteActionTaskRequest
            {
                CompanyId = viewModel.CompanyId,
                ActionTaskId = viewModel.ActionTaskId,
                CompletedComments = viewModel.CompletedComments,
                UserId = CurrentUser.UserId,
                CreateDocumentRequests = documentsToSaveViewModel.CreateDocumentRequests,
                DocumentLibraryIdsToDelete = documentsToSaveViewModel.DeleteDocumentRequests,
                CompletedDate = DateTimeOffset.Now
            };

            using (var session = _businessSafeSessionManager.Session)
            {
                _actionTaskService.Complete(request);
                _businessSafeSessionManager.CloseSession();
            }

            using (var session = _businessSafeSessionManager.Session)
            {
                _actionTaskService.SendTaskCompletedNotificationEmail(request.ActionTaskId,request.CompanyId);
                _businessSafeSessionManager.CloseSession();
            }

            return Json(new
            {
                Success = true,
                Id = viewModel.ActionTaskId
            });
        }

        private void ExecuteAssignActionTaskCommand(AssignActionPlanTaskViewModel model)
        {
            var command =
                _assignActionPlanTaskCommand
                    .WithCompanyId(CurrentUser.CompanyId)
                    .WithUserId(CurrentUser.UserId)
                    .WithActionId(model.ActionId)
                    .WithAssignedTo(model.AssignedTo)
                    .WithDueDate(model.DueDate)
                    .WithSendTaskNotification(true)
                    .WithSendTaskCompletedNotification(true)
                    .WithSendTaskOverdueNotification(true);
            
                command.Execute();
        }

        private void ValidateAssignActionPlanTaskViewModel(IEnumerable<AssignActionPlanTaskViewModel> model)
        {
            foreach (var action in model)
            {
                ValidateAssignActionPlanTaskViewModel(action);
            }
        }
        private void ValidateAssignActionPlanTaskViewModel(AssignActionPlanTaskViewModel model)
        {
            if (model.ActionId == default(long))
            {
                ModelState.AddModelError("ActionId", "Invalid ActionId.");
            }

            if (model.AssignedTo == null || model.AssignedTo == Guid.Empty)
            {
                ModelState.AddModelError("AssignedTo", "Please select an assignee for the selected task.");
            }

            DateTime dueDate = DateTime.MinValue;
            if (model.DueDate == null || !DateTime.TryParse(model.DueDate, out dueDate))
            {
                ModelState.AddModelError("DueDate", "Please select a valid due date for the selected task.");
            }
            else
            {
                if (dueDate < DateTime.Now.Date)
                {
                    ModelState.AddModelError("DueDate", "Due date cannot be in the past.");
                }
            }
        }

        [PermissionFilter(Permissions.EditActionPlan)]
        public PartialViewResult Reassign(long actionplanId, long actionId)
        {
            var viewModel =
                _reassignActionTaskViewModelFactory.
                    WithActionPlanId(actionplanId).
                    WithActionId(actionId).
                    WithCompanyId(CurrentUser.CompanyId).
                    GetViewModel();

            return PartialView("_ReassignActionTask", viewModel);
        }

        [PermissionFilter(Permissions.EditActionPlan)]
        public PartialViewResult ReassignWithTaskId(long companyId, long taskId, string taskType)
        {
            var model = _reassignActionTaskViewModelFactory
                .WithCompanyId(companyId)
                .WithTaskId(taskId)
                .GetViewModelByTask();

            return PartialView("_ReassignActionTask", model);
        }

        [PermissionFilter(Permissions.EditActionPlan)]
        public PartialViewResult Edit(long actionplanId, long actionId)
        {
            var viewModel =
                _reassignActionTaskViewModelFactory.
                    WithActionPlanId(actionplanId).
                    WithActionId(actionId).
                    WithCompanyId(CurrentUser.CompanyId).
                    GetViewModel();

            return PartialView("_ViewEditActionTask", viewModel);
        }

        [PermissionFilter(Permissions.ViewActionPlan)]
        public PartialViewResult ViewIrnActionTask(long actionplanId, long actionId)
        {
            IsReadOnly = true;
            var viewModel =
                _reassignActionTaskViewModelFactory.
                    WithActionPlanId(actionplanId).
                    WithActionId(actionId).
                    WithCompanyId(CurrentUser.CompanyId).
                    GetViewModel();

            return PartialView("_ViewEditActionTask", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditActionPlan)]
        public JsonResult Reassign(ReassignActionTaskViewModel model, DocumentsToSaveViewModel documentsToSave)
        {
            JsonResult result;
            
            ValidateReassignViewModel(model);

            if (ModelState.IsValid)
            {
                var command =
                    _assignActionPlanTaskCommand
                        .WithCompanyId(CurrentUser.CompanyId)
                        .WithUserId(CurrentUser.UserId)
                        .WithActionId(model.ActionId)
                        .WithAssignedTo(model.ActionTaskAssignedToId)
                        .WithDueDate(model.DueDate)
                  		.WithSendTaskNotification(!model.DoNotSendTaskAssignedNotification)
                  		.WithSendTaskCompletedNotification(!model.DoNotSendTaskCompletedNotification)
                  		.WithSendTaskOverdueNotification(!model.DoNotSendTaskOverdueNotification)
                        .WithDocuments(documentsToSave.CreateDocumentRequests)
                        .WithAreaOfNonCompliance(model.Title)
                        .WithActionRequired(model.Description);

                command.Execute();
               
                result = Json(new { Success = true });       
            }
            else
            {
                result = ModelStateErrorsAsJson();
            }
            return result;
        }
        
        private void ValidateReassignViewModel(ReassignActionTaskViewModel model)
        {
            if (model.ActionTaskAssignedToId == null || model.ActionTaskAssignedToId == Guid.Empty)
            {
                ModelState.AddModelError("AssignedTo", "Please select an assignee for the selected task.");
            }

            DateTime dueDate = DateTime.MinValue;
            if (model.DueDate == null || !DateTime.TryParse(model.DueDate, out dueDate))
            {
                ModelState.AddModelError("DueDate", "Please select a valid due date for the selected task.");
            }
            else
            {
                if (dueDate < DateTime.Now.Date)
                {
                    ModelState.AddModelError("DueDate", "Due date cannot be in the past.");
                }
            }
        }
    }
}
