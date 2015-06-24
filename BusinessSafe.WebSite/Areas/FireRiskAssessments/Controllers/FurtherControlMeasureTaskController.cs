using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers
{
    public class FurtherControlMeasureTaskController : BaseController
    {
        private readonly IAddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory _addTaskViewModelFactory;
        private readonly IEditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory _editTaskViewModelFactory;
        private readonly IFireRiskAssessmentFurtherControlMeasureTaskService _furtherControlMeasureTaskService;
        private readonly IReassignFurtherControlMeasureTaskViewModelFactory _reassignTaskViewModelFactory;
        private readonly ICompleteFurtherControlMeasureTaskViewModelFactory _completeTaskWithHazardSummaryViewModelFactory;
        private readonly IViewFurtherControlMeasureTaskViewModelFactory _viewTaskViewModelFactory;
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;
        private readonly IBus _bus;
        private readonly EventPublisher _eventPublisher;

        public FurtherControlMeasureTaskController(
            IAddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory addTaskViewModelFactory,
            IFireRiskAssessmentFurtherControlMeasureTaskService furtherControlMeasureTaskService,
            IEditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory editTaskViewModelFactory,
            IReassignFurtherControlMeasureTaskViewModelFactory reassingTaskViewModelFactory,
            ICompleteFurtherControlMeasureTaskViewModelFactory completeTaskWithHazardSummaryViewModelFactory,
            IViewFurtherControlMeasureTaskViewModelFactory viewTaskViewModelFactory,
            IBusinessSafeSessionManager businessSafeSessionManager,
            IBus bus)
        {
            _addTaskViewModelFactory = addTaskViewModelFactory;
            _furtherControlMeasureTaskService = furtherControlMeasureTaskService;
            _editTaskViewModelFactory = editTaskViewModelFactory;
            _reassignTaskViewModelFactory = reassingTaskViewModelFactory;
            _completeTaskWithHazardSummaryViewModelFactory = completeTaskWithHazardSummaryViewModelFactory;
            _viewTaskViewModelFactory = viewTaskViewModelFactory;
            _businessSafeSessionManager = businessSafeSessionManager;
            _bus = bus;
            _eventPublisher = new EventPublisher(_bus);
        }

        [PermissionFilter(Permissions.AddRiskAssessmentTasks)]
        public PartialViewResult New(long companyId, long riskAssessmentId, long significantFindingId)
        {
            var viewModel = _addTaskViewModelFactory
                .WithCompanyId(companyId)
                .WithRiskAssessmentId(riskAssessmentId)
                .WithSignificantFindingId(significantFindingId)
                .GetViewModel();

            return PartialView("_AddFireRiskAssessmentFurtherControlMeasureTask", viewModel);
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

        [HttpPost]
        [PermissionFilter(Permissions.AddRiskAssessmentTasks)]
        [ILookAfterOwnTransactionFilter]
        public JsonResult NewFurtherControlMeasureTask(AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel viewModel, DocumentsToSaveViewModel documentsToSave)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Errors = ModelState.GetErrorMessages() });

            if(!viewModel.IsRecurring)
            {
                viewModel.TaskReoccurringTypeId = (int)TaskReoccurringType.None;
            }
            
            var taskGuid = Guid.NewGuid();
            SaveFurtherControlMeasureTaskRequest request = SaveFurtherControlMeasureTaskRequest.Create(
                                                                                                        viewModel.Title,
                                                                                                        viewModel.Description,
                                                                                                        viewModel.Reference,
                                                                                                        viewModel.TaskCompletionDueDate,
                                                                                                        viewModel.TaskStatusId,
                                                                                                        viewModel.CompanyId,
                                                                                                        viewModel.RiskAssessmentId,
                                                                                                        viewModel.SignificantFindingId,
                                                                                                        viewModel.TaskAssignedToId.GetValueOrDefault(),
                                                                                                        viewModel.TaskReoccurringTypeId,
                                                                                                        viewModel.FirstDueDate, 
                                                                                                        viewModel.TaskReoccurringEndDate, 
                                                                                                        CurrentUser.UserId,
                                                                                                        documentsToSave.CreateDocumentRequests,
                                                                                                        documentsToSave.DeleteDocumentRequests,
                                                                                                        !viewModel.DoNotSendTaskNotification,
                                                                                                        !viewModel.DoNotSendTaskCompletedNotification,
                                                                                                        !viewModel.DoNotSendTaskOverdueNotification,
                                                                                                        !viewModel.DoNotSendTaskDueTomorrowNotification,
                                                                                                        taskGuid);

            FireRiskAssessmentFurtherControlMeasureTaskDto result;
            using (var session = _businessSafeSessionManager.Session)
            {
                result = _furtherControlMeasureTaskService.AddFurtherControlMeasureTask(request);
                _businessSafeSessionManager.CloseSession();
            }


            _eventPublisher.PublishTaskAssigned(taskGuid);
            return Json(new { Success = true, result.Id, CreatedOn = result.CreatedDate });
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
        public PartialViewResult Complete(long companyId, long furtherControlMeasureTaskId)
        {
            var viewModel = _completeTaskWithHazardSummaryViewModelFactory
                .WithCompanyId(companyId)
                .WithFurtherControlMeasureTaskId(furtherControlMeasureTaskId)
                .GetViewModel();

            return PartialView("_CompleteFurtherControlMeasureTask", viewModel);
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
