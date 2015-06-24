using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Messages.Events;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using System.Transactions;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers
{
    public class FurtherControlMeasureTaskController : BaseController
    {
        private readonly IHazardousSubstanceRiskAssessmentFurtherControlMeasureTaskService _furtherControlMeasureTaskService;
        private readonly IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory _addTaskViewModelFactory;
        private readonly ICompleteFurtherControlMeasureTaskViewModelFactory _completeTaskViewModelFactory;
        private readonly IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory _editTaskViewModelFactory;
        private readonly IReassignFurtherControlMeasureTaskViewModelFactory _reassignTaskViewModelFactory;
        private readonly IViewFurtherControlMeasureTaskViewModelFactory _viewTaskViewModelFactory;
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;
        private readonly IBus _bus;

        public FurtherControlMeasureTaskController(
            IHazardousSubstanceRiskAssessmentFurtherControlMeasureTaskService furtherControlMeasureTaskService, 
            IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory addTaskViewModelFactory, 
            ICompleteFurtherControlMeasureTaskViewModelFactory completeTaskViewModelFactory, 
            IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory editTaskViewModelFactory, 
            IReassignFurtherControlMeasureTaskViewModelFactory reassignTaskViewModelFactory, 
            IViewFurtherControlMeasureTaskViewModelFactory viewTaskViewModelFactory,
            IBusinessSafeSessionManager businessSafeSessionManager, IBus bus)
        {
            _furtherControlMeasureTaskService = furtherControlMeasureTaskService;
            _addTaskViewModelFactory = addTaskViewModelFactory;
            _completeTaskViewModelFactory = completeTaskViewModelFactory;
            _editTaskViewModelFactory = editTaskViewModelFactory;
            _reassignTaskViewModelFactory = reassignTaskViewModelFactory;
            _viewTaskViewModelFactory = viewTaskViewModelFactory;
            _businessSafeSessionManager = businessSafeSessionManager;
            _bus = bus;
        }

        [PermissionFilter(Permissions.AddRiskAssessmentTasks)]
        public PartialViewResult New(long companyId, long riskAssessmentId)
        {
            var viewModel = _addTaskViewModelFactory
                .WithCompanyId(companyId)
                .WithRiskAssessmentId(riskAssessmentId)
                .GetViewModel();

            return PartialView("_AddHazardousSubstanceRiskAssessmentFurtherControlMeasureTask", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddRiskAssessmentTasks)]
        [ILookAfterOwnTransactionFilter]
        public JsonResult NewFurtherControlMeasureTask(AddHazardousSubstanceFurtherControlMeasureTaskViewModel viewModel, DocumentsToSaveViewModel documentsToSave)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Errors = ModelState.GetErrorMessages() });

            if (!viewModel.IsRecurring)
            {
                viewModel.TaskReoccurringTypeId = (int)TaskReoccurringType.None;
            }

            var taskGuid = Guid.NewGuid();
            var request = SaveFurtherControlMeasureTaskRequest.Create(
                viewModel.Title,
                viewModel.Description,
                viewModel.Reference,
                viewModel.TaskCompletionDueDate,
                viewModel.TaskStatusId,
                viewModel.CompanyId,
                viewModel.RiskAssessmentId,
                viewModel.TaskAssignedToId.Value,
                viewModel.TaskReoccurringTypeId,
                viewModel.FirstDueDate, 
                viewModel.TaskReoccurringEndDate, 
                CurrentUser.UserId, 
                documentsToSave.CreateDocumentRequests,
                documentsToSave.DeleteDocumentRequests,
                !viewModel.DoNotSendTaskNotification,
                !viewModel.DoNotSendTaskCompletedNotification,
                !viewModel.DoNotSendTaskOverdueNotification,
                viewModel.DoNotSendTaskDueTomorrowNotification,
                taskGuid);

            HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskDto result;
            using (var session = _businessSafeSessionManager.Session)
            {
                result = _furtherControlMeasureTaskService.AddFurtherControlMeasureTask(request);
                _businessSafeSessionManager.CloseSession();
            }

            _bus.Publish(new TaskAssigned { TaskGuid = taskGuid });
            
            return Json(new { Success = true, result.Id, CreatedOn = result.CreatedDate });
        }

        [PermissionFilter(Permissions.ViewRiskAssessmentTasks)]
        public PartialViewResult Complete(long companyId, long furtherControlMeasureTaskId)
        {
            var viewModel = _completeTaskViewModelFactory
                                            .WithCompanyId(companyId)
                                            .WithFurtherControlMeasureTaskId(furtherControlMeasureTaskId)
                                            .GetViewModel();

            return PartialView("_CompleteFurtherControlMeasureTask", viewModel);
        }

        [PermissionFilter(Permissions.EditRiskAssessmentTasks)]
        public PartialViewResult Edit(long furtherControlMeasureTaskId)
        {
            var viewModel = _editTaskViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithFurtherControlMeasureTaskId(furtherControlMeasureTaskId)
                .WithCanDeleteDocuments(true)
                .GetViewModel();

            return PartialView("_EditFurtherControlMeasureTask", viewModel);
        }

        [PermissionFilter(Permissions.EditRiskAssessmentTasks)]
        public PartialViewResult Reassign(long companyId, long furtherControlMeasureTaskId)
        {
            var viewModel = _reassignTaskViewModelFactory
                                .WithCompanyId(companyId)
                                .WithFurtherControlMeasureTaskId(furtherControlMeasureTaskId)
                                .GetViewModel();

            return PartialView("_ReassignFurtherControlMeasureTask", viewModel);
        }

        [PermissionFilter(Permissions.ViewRiskAssessmentTasks)]
        public PartialViewResult View(long furtherControlMeasureTaskId)
        {
            var viewModel = _viewTaskViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithFurtherControlMeasureTaskId(furtherControlMeasureTaskId)
                .GetViewModel();

            IsReadOnly = true;

            return PartialView("_ViewFurtherControlMeasureTask", viewModel);
        }


        [PermissionFilter(Permissions.ViewRiskAssessmentTasks)]
        public PartialViewResult Print(long companyId, long furtherControlMeasureTaskId)
        {
            var viewModel = _viewTaskViewModelFactory
                .WithCompanyId(companyId)
                .WithFurtherControlMeasureTaskId(furtherControlMeasureTaskId)
                .GetViewModel();

            IsReadOnly = true;

            return PartialView("_PrintFurtherControlMeasureTask", viewModel);
        }
    }
}
