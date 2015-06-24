using System;
using System.Web.Mvc;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Messages.Events;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Contracts.RiskAssessments;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Controllers
{
    public class GenericFurtherControlMeasureTaskController : BaseController
    {
        private readonly IFurtherControlMeasureTaskService _furtherControlMeasureTaskService;
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;
        private readonly IBus _bus;

        public GenericFurtherControlMeasureTaskController(
            IFurtherControlMeasureTaskService furtherControlMeasureTaskService,
            IBusinessSafeSessionManager businessSafeSessionManager,
            IBus bus
            )
        {
            _furtherControlMeasureTaskService = furtherControlMeasureTaskService;
            _businessSafeSessionManager = businessSafeSessionManager;
            _bus = bus;
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditRiskAssessmentTasks)]
        public JsonResult UpdateFurtherControlMeasureTask(AddEditFurtherControlMeasureTaskViewModel viewmodel, DocumentsToSaveViewModel documentsToSaveViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Errors = ModelState.GetErrorMessages() });

            var existingTask = _furtherControlMeasureTaskService.GetByIdAndCompanyId(viewmodel.FurtherControlMeasureTaskId,
                CurrentUser.CompanyId);

            var saveTaskRequest = UpdateFurtherControlMeasureTaskRequest.Create(
                viewmodel.Title,
                viewmodel.Description,
                viewmodel.Reference,
                viewmodel.TaskCompletionDueDate,
                viewmodel.TaskStatusId,
                viewmodel.CompanyId,
                viewmodel.FurtherControlMeasureTaskId,
                viewmodel.TaskAssignedToId.GetValueOrDefault(),
                viewmodel.TaskReoccurringTypeId,
                viewmodel.FirstDueDate,
                viewmodel.TaskReoccurringEndDate,
                CurrentUser.UserId,
                documentsToSaveViewModel.CreateDocumentRequests,
                documentsToSaveViewModel.DeleteDocumentRequests,
                !viewmodel.DoNotSendTaskNotification,
                !viewmodel.DoNotSendTaskCompletedNotification,
                !viewmodel.DoNotSendTaskOverdueNotification,
                !viewmodel.DoNotSendTaskDueTomorrowNotification);

            _furtherControlMeasureTaskService.Update(saveTaskRequest);

            if(existingTask.TaskAssignedTo.Id != viewmodel.TaskAssignedToId.GetValueOrDefault())
            {
                _bus.Publish(new TaskAssigned { TaskGuid = existingTask.TaskGuid });
            }

            return Json(new { Success = true, Id = viewmodel.FurtherControlMeasureTaskId });
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddRiskAssessmentTasks)]
        [ILookAfterOwnTransactionFilter]
        public JsonResult CreateFurtherControlMeasureTask(AddRiskAssessmentFurtherControlMeasureTaskViewModel viewmodel, DocumentsToSaveViewModel documentsToSave)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { Success = false, Errors = ModelState.GetErrorMessages() });
            }

            MultiHazardRiskAssessmentFurtherControlMeasureTaskDto result;
            Guid taskGuid = Guid.NewGuid();
            using (var session = _businessSafeSessionManager.Session)
            {
                if(!viewmodel.IsRecurring)
                {
                    viewmodel.TaskReoccurringTypeId = (int)TaskReoccurringType.None;
                }

                var saveTaskRequest = SaveFurtherControlMeasureTaskRequest.Create(
                    viewmodel.Title,
                    viewmodel.Description,
                    viewmodel.Reference,
                    viewmodel.TaskCompletionDueDate,
                    viewmodel.TaskStatusId,
                    viewmodel.CompanyId,
                    0,
                    viewmodel.RiskAssessmentHazardId,
                    viewmodel.TaskAssignedToId.Value,
                    (int)viewmodel.FurtherControlMeasureTaskCategory,
                    viewmodel.TaskReoccurringTypeId,
                    viewmodel.FirstDueDate,
                    viewmodel.TaskReoccurringEndDate,
                    !viewmodel.DoNotSendTaskNotification,
                    !viewmodel.DoNotSendTaskCompletedNotification,
                    !viewmodel.DoNotSendTaskOverdueNotification,
                    !viewmodel.DoNotSendTaskDueTomorrowNotification,
                    CurrentUser.UserId,
                    documentsToSave.CreateDocumentRequests,
                    taskGuid
                    );

                result = _furtherControlMeasureTaskService.AddFurtherControlMeasureTask(saveTaskRequest);

                _businessSafeSessionManager.CloseSession();
            }

            _bus.Publish(new TaskAssigned { TaskGuid = taskGuid });

            return Json(new { Success = true, result.Id, viewmodel.RiskAssessmentHazardId, CreatedOn = result.CreatedDate });
        }
    }
}
