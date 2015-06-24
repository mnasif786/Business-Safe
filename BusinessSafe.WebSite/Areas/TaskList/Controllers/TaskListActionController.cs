using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Request;
using BusinessSafe.Messages.Emails.Commands;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using TaskAssigned = BusinessSafe.Messages.Events.TaskAssigned;

namespace BusinessSafe.WebSite.Areas.TaskList.Controllers
{
    public class TaskListActionController : BaseController
    {
        private readonly ITaskService _taskService;
        private readonly IFurtherControlMeasureTaskService _furtherControlMeasureTaskService;
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;
        private readonly IBus _bus;
        public TaskListActionController(
            ITaskService taskService,
            IFurtherControlMeasureTaskService furtherControlMeasureTaskService,
            IBusinessSafeSessionManager businessSafeSessionManager,
            IBus bus)
        {
            _taskService = taskService;
            _furtherControlMeasureTaskService = furtherControlMeasureTaskService;
            _businessSafeSessionManager = businessSafeSessionManager;
            _bus = bus;
        }

        [HttpPost]
        [PermissionFilter(Permissions.ViewRiskAssessmentTasks)]
        [ILookAfterOwnTransactionFilter]
        public JsonResult CompleteTask(CompleteTaskViewModel completeTaskViewModel, DocumentsToSaveViewModel documentsToSaveViewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid Arguments To Complete Task");
            }

            var request = new CompleteTaskRequest
                              {
                                  CompanyId = completeTaskViewModel.CompanyId,
                                  FurtherControlMeasureTaskId = completeTaskViewModel.FurtherControlMeasureTaskId,
                                  CompletedComments = completeTaskViewModel.CompletedComments,
                                  UserId = CurrentUser.UserId,
                                  CreateDocumentRequests = documentsToSaveViewModel.CreateDocumentRequests,
                                  DocumentLibraryIdsToDelete = documentsToSaveViewModel.DeleteDocumentRequests,
                                  CompletedDate = DateTimeOffset.Now
                              };

            using (var session = _businessSafeSessionManager.Session)
            {
                _furtherControlMeasureTaskService.CompleteFurtherControlMeasureTask(request);
                _businessSafeSessionManager.CloseSession();
            }

            using (var session = _businessSafeSessionManager.Session)
            {
                _furtherControlMeasureTaskService.SendTaskCompletedEmail(request);
                 _businessSafeSessionManager.CloseSession();
            }

            return Json(new SaveTaskResultViewModel
            {
                Success = true,
                Id = completeTaskViewModel.FurtherControlMeasureTaskId,
            });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditRiskAssessmentTasks)]
        [ILookAfterOwnTransactionFilter]
        public JsonResult ReassignTask(ReassignTaskViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid Arguments To Reassign Task");
            }

            using (var session = _businessSafeSessionManager.Session)
            {
                var reassignTaskToEmployeeRequest = CreateReassignRequest(viewModel);
                _taskService.ReassignTask(reassignTaskToEmployeeRequest);

                _businessSafeSessionManager.CloseSession();
            }

            _bus.Publish(new TaskAssigned { TaskGuid = viewModel.TaskGuid });

            return Json(new ReassignTaskResultViewModel()
            {
                Success = true
            });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditRiskAssessmentTasks)]
        [ILookAfterOwnTransactionFilter]
        public JsonResult BulkReassignTask(IEnumerable<ReassignTaskViewModel> viewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid Arguments To Bulk Reassign Task");
            }

            if (HasMoreThanOneTaskAssignedToIdsSpecifiedInRequest(viewModel))
            {
                throw new MultipleReassignToIdsSpecifiedInBulkReassignRequestException();
            }

            var request = CreateBulkReassignRequest(viewModel);
            using (var session = _businessSafeSessionManager.Session)
            {
                _taskService.BulkReassignTasks(request);
                _businessSafeSessionManager.CloseSession();
            }

            if (request.ReassignRequests.Any())
            {
                request.ReassignRequests.ToList()
                    .ForEach(x => _bus.Publish(new TaskAssigned { TaskGuid = x.TaskGuid }));
            }

            return Json(new ReassignTaskResultViewModel()
            {
                Success = true
            });

        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteRiskAssessmentTasks)]
        public JsonResult MarkTaskAsDeleted(MarkTaskAsDeletedViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid Arguments To Mark Task As Deleted");
            }

            var request = new MarkTaskAsDeletedRequest()
                              {
                                  TaskId = viewModel.FurtherControlMeasureTaskId,
                                  CompanyId = viewModel.CompanyId,
                                  UserId = CurrentUser.UserId
                              };

            if (_taskService.HasCompletedTasks(request))
            {
                return Json(new MarkTaskForDeleteResultViewModel()
                {
                    Success = false,
                    Message = "This reoccurring task has previous tasks which have been completed, and can therefore not be deleted."
                });
            }

            _taskService.MarkTaskAsDeleted(request);

            return Json(new MarkTaskForDeleteResultViewModel()
            {
                Success = true
            });
        }

        [PermissionFilter(Permissions.EditRiskAssessmentTasks)]
        [HttpPost]
        public JsonResult MarkTaskAsNoLongerRequired(MarkTaskAsNoLongerRequiredViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid Arguments To Mark Task As No Longer Required");
            }

            _taskService.MarkTaskAsNoLongerRequired(new MarkTaskAsNoLongerRequiredRequest()
            {
                CompanyId = viewModel.CompanyId,
                TaskId = viewModel.FurtherControlMeasureTaskId,
                UserId = CurrentUser.UserId
            });
            return Json(new MarkTaskAsNoLongerRequiredResultViewModel()
            {
                Success = true
            });
        }

        private BulkReassignTasksToEmployeeRequest CreateBulkReassignRequest(IEnumerable<ReassignTaskViewModel> reassignTaskViewModel)
        {
            var reassignRequests = reassignTaskViewModel.Select(CreateReassignRequest).ToList();

            return new BulkReassignTasksToEmployeeRequest()
            {
                ReassignRequests = reassignRequests
            };
        }

        private ReassignTaskToEmployeeRequest CreateReassignRequest(ReassignTaskViewModel viewModel)
        {
            return new ReassignTaskToEmployeeRequest()
            {
                CompanyId = viewModel.CompanyId,
                TaskId = viewModel.FurtherControlMeasureTaskId,
                ReassignTaskToId = viewModel.ReassignTaskToId,
                UserId = CurrentUser.UserId
                ,
                TaskGuid = viewModel.TaskGuid
            };
        }

        private static bool HasMoreThanOneTaskAssignedToIdsSpecifiedInRequest(IEnumerable<ReassignTaskViewModel> viewModel)
        {
            return viewModel.GroupBy(x => x.ReassignTaskToId).Count() > 1;
        }
    }
}
