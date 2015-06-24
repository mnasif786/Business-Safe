using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Messages.Events;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;
using FluentValidation;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using Telerik.Web.Mvc;
using SaveResponsibilityTaskRequest = BusinessSafe.Application.Request.Responsibilities.SaveResponsibilityTaskRequest;
using BusinessSafe.Application.Response;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Controllers
{
    // todo: split this up into responsibility and resp. task!

    public class ResponsibilityController : BaseController
    {
        private readonly IResponsibilitiesService _responsibilitiesService;
        private readonly ITaskService _taskService;
        private readonly IResponsibilityTaskService _responsibilityTaskService;
        private readonly ISearchResponsibilityViewModelFactory _searchResponsibilityViewModelFactory;
        private readonly IResponsibilityViewModelFactory _responsibilityViewModelFactory;
        private readonly ICreateUpdateResponsibilityTaskViewModelFactory _createUpdateResponsibilityTaskViewModelFactory;
        private readonly ICompleteResponsibilityTaskViewModelFactory _completeResponsibilityTaskViewModelFactory;
        private IReassignResponsibilityTaskViewModelFactory _reassignResponsibilityTaskViewModelFactory;
        private readonly IViewResponsibilityTaskViewModelFactory _viewResponsibilityTaskViewModelFactory;

        private const int PAGE_SIZE = 10;

        private readonly IBus _bus;
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;
        
        public ResponsibilityController(IResponsibilitiesService responsibilitiesService,
            ITaskService taskService,
            IResponsibilityTaskService responsibilityTaskService,
            ISearchResponsibilityViewModelFactory searchResponsibilityViewModelFactory,
            IResponsibilityViewModelFactory createResponsibilityViewModelFactory,
            ICreateUpdateResponsibilityTaskViewModelFactory createUpdateResponsibilityTaskViewModelFactory,
            ICompleteResponsibilityTaskViewModelFactory completeResponsibilityTaskViewModelFactory,
            IReassignResponsibilityTaskViewModelFactory reassignResponsibilityTaskViewModelFactory,
            IViewResponsibilityTaskViewModelFactory viewResponsibilityTaskViewModelFactory,
            IBus bus,
            IBusinessSafeSessionManager businessSafeSessionManager)
        {
            _responsibilitiesService = responsibilitiesService;
            _taskService = taskService;
            _responsibilityTaskService = responsibilityTaskService;
            _searchResponsibilityViewModelFactory = searchResponsibilityViewModelFactory;
            _responsibilityViewModelFactory = createResponsibilityViewModelFactory;
            _createUpdateResponsibilityTaskViewModelFactory = createUpdateResponsibilityTaskViewModelFactory;
            _completeResponsibilityTaskViewModelFactory = completeResponsibilityTaskViewModelFactory;
            _reassignResponsibilityTaskViewModelFactory = reassignResponsibilityTaskViewModelFactory;
            _viewResponsibilityTaskViewModelFactory = viewResponsibilityTaskViewModelFactory;
            _bus = bus;
            _businessSafeSessionManager = businessSafeSessionManager;
        }

        [GridAction(EnableCustomBinding = true, GridName = "ResponsibilitiesGrid")]
        [PermissionFilter(Permissions.ViewResponsibilities)]
        public ActionResult Index(ResponsibilitiesIndexViewModel model)
        {
            var viewModel = GetResponsibilitiesIndexViewModel(model);

            return View("Index", viewModel);
        }

        [GridAction(EnableCustomBinding = true, GridName = "ResponsibilitiesTaskGrid")]
        [PermissionFilter(Permissions.ViewResponsibilities)]
        public ViewResult Find(ResponsibilitiesIndexViewModel model)
        {
            var viewModel = GetResponsibilitiesIndexViewModel(model);

            return View("Index", viewModel);
        }

        private ResponsibilitiesIndexViewModel GetResponsibilitiesIndexViewModel(ResponsibilitiesIndexViewModel model)
        {
            _searchResponsibilityViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithTitle(model.Title)
                .WithPageNumber(model.Page != default(int) ? model.Page : 1)
                .WithPageSize(model.PageSize != default(int) ? model.PageSize : PAGE_SIZE)
                .WithOrderBy(model.OrderBy)
                .WithShowDeleted(model.IsShowDeleted);
               
            if (model.CategoryId.HasValue)
                _searchResponsibilityViewModelFactory.WithCategoryId(model.CategoryId.Value);

            if (model.SiteId.HasValue)
                _searchResponsibilityViewModelFactory.WithSiteId(model.SiteId.Value);

            if (model.SiteGroupId.HasValue)
                _searchResponsibilityViewModelFactory.WithSiteGroupId(model.SiteGroupId.Value);

            if (!string.IsNullOrEmpty(model.CreatedFrom))
                _searchResponsibilityViewModelFactory.WithCreatedFrom(DateTime.Parse(model.CreatedFrom));

            if (!string.IsNullOrEmpty(model.CreatedTo))
                _searchResponsibilityViewModelFactory.WithCreatedTo(DateTime.Parse(model.CreatedTo));

            var allowedSites = CurrentUser.GetSitesFilter();

            if (allowedSites.Count > 0)
                _searchResponsibilityViewModelFactory.WithAllowedSiteIds(allowedSites);

            var viewModel = _searchResponsibilityViewModelFactory.GetViewModel();
            return viewModel;
        }


        [PermissionFilter(Permissions.AddResponsibilities)]
        public ActionResult Create(long companyId)
        {
           _responsibilityViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId);
            
            var allowedSites = CurrentUser.GetSitesFilter();

            if (allowedSites.Count > 0)
                _responsibilityViewModelFactory.WithAllowedSiteIds(allowedSites);

            var viewModel = _responsibilityViewModelFactory.GetViewModel();
            return View("Create", viewModel);
        }

        [PermissionFilter(Permissions.EditResponsibilities)]
        public ActionResult Edit(long? responsibilityId, long companyId, bool? showCreateResponsibilityTaskDialogOnLoad)
        {
            var viewModel = _responsibilityViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithResponsibilityId(responsibilityId)
                .WithShowCreateResponsibilityTaskDialogOnLoad(showCreateResponsibilityTaskDialogOnLoad)
                .GetViewModel();
            return View("Edit", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddResponsibilities)]
        public ActionResult Create(ResponsibilityViewModel viewModel)
        {
            Validate(viewModel);

            if (!ModelState.IsValid)
            {
                return ReturnInvalidCreateResponsibilityViewResult(viewModel);
            }

            try
            {
                var request = new SaveResponsibilityRequest
                                    {
                                        CompanyId = CurrentUser.CompanyId,
                                        ResponsibilityCategoryId = viewModel.CategoryId.HasValue ? viewModel.CategoryId.Value : default(long),
                                        Title = viewModel.Title,
                                        Description = viewModel.Description,
                                        SiteId = viewModel.SiteId.HasValue ? viewModel.SiteId.Value : default(long),
                                        ResponsibilityReasonId = viewModel.ReasonId.HasValue ? viewModel.ReasonId.Value : default(long),
                                        OwnerId = GetOwnerId(viewModel),
                                        TaskReoccurringType = (TaskReoccurringType)viewModel.FrequencyId,
                                        UserId = CurrentUser.UserId
                                    };

                var response = _responsibilitiesService.SaveResponsibility(request);

                if (response != default(long))
                {
                    return RedirectToEditAction(response, viewModel.CompanyId, viewModel.CreateResponsibilityTask);
                }

                return View("Create", viewModel);
            }
            catch (ValidationException validationException)
            {
                ModelState.AddValidationErrors(validationException);
            }
            return ReturnInvalidCreateResponsibilityViewResult(viewModel);
        }


        [HttpPost]
        [PermissionFilter(Permissions.EditResponsibilities)]
        public ActionResult Update(ResponsibilityViewModel viewModel)
        {
            Validate(viewModel);

            if (!ModelState.IsValid)
            {
                return ReturnInvalidEditResponsibilityViewResult(viewModel);
            }

            try
            {
                var request = new SaveResponsibilityRequest
                {
                    CompanyId = CurrentUser.CompanyId,
                    ResponsibilityId = viewModel.ResponsibilityId,
                    ResponsibilityCategoryId = viewModel.CategoryId.HasValue ? viewModel.CategoryId.Value : default(long),
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    SiteId = viewModel.SiteId.HasValue ? viewModel.SiteId.Value : default(long),
                    ResponsibilityReasonId = viewModel.ReasonId.HasValue ? viewModel.ReasonId.Value : default(long),
                    OwnerId = GetOwnerId(viewModel),
                    TaskReoccurringType = viewModel.HasMultipleFrequencies && viewModel.FrequencyId == null ? TaskReoccurringType.None : (TaskReoccurringType)viewModel.FrequencyId,
                    UserId = CurrentUser.UserId
                };

                _responsibilitiesService.SaveResponsibility(request);

                return RedirectToEditAction(viewModel.ResponsibilityId, viewModel.CompanyId, null);
            }
            catch (ValidationException validationException)
            {
                ModelState.AddValidationErrors(validationException);
            }
            return ReturnInvalidCreateResponsibilityViewResult(viewModel);
        }

        [PermissionFilter(Permissions.DeleteResponsibilities)]
        public JsonResult CheckCanBeDeleted(long responsibilityId)
        {
            var result = _responsibilitiesService.HasUndeletedTasks(responsibilityId, CurrentUser.CompanyId);
            return Json(new { hasUndeletedTasks = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteResponsibilities)]
        public JsonResult Delete(long responsibilityId)
        {
            _responsibilitiesService.Delete(responsibilityId, CurrentUser.CompanyId, CurrentUser.UserId);
            return Json(new { success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditResponsibilities)]
        public ActionResult Undelete(long responsibilityId)
        {
            _responsibilitiesService.Undelete(responsibilityId, CurrentUser.CompanyId, CurrentUser.UserId);
            return Json(new { success = true });
        }

        [PermissionFilter(Permissions.EditResponsibilities)]
        public JsonResult MarkTaskAsDeleted(MarkResponsibilityTaskAsDeletedViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid Arguments To Mark Task As Deleted");
            }

            var request = new MarkTaskAsDeletedRequest()
            {
                TaskId = viewModel.TaskId,
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
        public JsonResult MarkTaskAsNoLongerRequired(MarkResponsibilityTaskAsNoLongerRequiredViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid Arguments To Mark Task As No Longer Required");
            }

            _taskService.MarkTaskAsNoLongerRequired(new MarkTaskAsNoLongerRequiredRequest()
            {
                CompanyId = viewModel.CompanyId,
                TaskId = viewModel.TaskId,
                UserId = CurrentUser.UserId
            });
            return Json(new MarkTaskAsNoLongerRequiredResultViewModel()
            {
                Success = true
            });
        }

        
        private ActionResult RedirectToEditAction(long responsibilityId, long companyId, bool? showCreateResponsibilityTaskDialogOnLoad)
        {
            return RedirectToAction("Edit",
                                    new
                                        {
                                            responsibilityId = responsibilityId,
                                            companyId = companyId,
                                            showCreateResponsibilityTaskDialogOnLoad =
                                        showCreateResponsibilityTaskDialogOnLoad
                                        });
        }

        private Guid? GetOwnerId(ResponsibilityViewModel viewModel)
        {
            if (!viewModel.IsCreatorResponsibilityOwner && viewModel.OwnerId != null)
            {
                return viewModel.OwnerId;
            }
            return null;
        }


        private void Validate(ResponsibilityViewModel viewModel)
        {
            if (viewModel.CategoryId == null)
            {
                ModelState.AddModelError("CategoryId", "Please select a Category");
            }
            if (viewModel.SiteId == null)
            {
                ModelState.AddModelError("SiteId", "Please select a Site");
            }
            
            if (viewModel.HasMultipleFrequencies == false && viewModel.FrequencyId == null || viewModel.FrequencyId == default(int))
            {
                 ModelState.AddModelError("FrequencyId", "Please select a Frequency");
            }

            if (!viewModel.IsCreatorResponsibilityOwner && viewModel.OwnerId == null)
            {
                ModelState.AddModelError("OwnerId", "Please select a Responsibility Owner");
            }
        }


        [HttpGet]
        [PermissionFilter(Permissions.ViewResponsibilities)]
        public PartialViewResult GetResponsibilityTask(long companyId, long responsibilityId, long? taskId, bool? autoLaunchedAfterCreatingResponsibility)
        {
            var model = _createUpdateResponsibilityTaskViewModelFactory
                .WithCompanyId(companyId)
                .WithResponsibilityId(responsibilityId)
                .WithTaskId(taskId)
                .WithAutoLaunchedAfterCreatingResponsibility(autoLaunchedAfterCreatingResponsibility)
                .GetViewModel();

            return PartialView("_ResponsibilityTask", model);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditResponsibilities)]
        public JsonResult CreateResponsibilityTask(CreateUpdateResponsibilityTaskViewModel viewModel, DocumentsToSaveViewModel documentsToSave)
        {
            JsonResult result = Json(new { Success = false });

            if (!ModelState.IsValid)
            {
                result = Json(new { Success = false, Errors = ModelState.GetErrorMessages() });
            }
            else
            {
                try
                {
                    var taskGuid = Guid.NewGuid();
                    SaveResponsibilityTaskResponse response = null;

                    using (var session = _businessSafeSessionManager.Session)
                    {
                        // move this to Task base entity
                        if(!viewModel.IsRecurring)
                        {
                            viewModel.TaskReoccurringTypeId = (int)TaskReoccurringType.None;
                        }

                        var request = SaveResponsibilityTaskRequest.Create(
                            viewModel.CompanyId,
                            viewModel.ResponsibilityId,
                            viewModel.TaskId = viewModel.TaskId,
                            viewModel.Title,
                            viewModel.Description,
                            viewModel.TaskReoccurringTypeId,
                            viewModel.CompletionDueDate,
                            viewModel.ReoccurringEndDate,
                            CurrentUser.UserId,
                            viewModel.AssignedToId.Value,
                            (int) TaskStatus.Outstanding,
                            viewModel.ReoccurringStartDate,
                            !viewModel.DoNotSendTaskAssignedNotification,
                            !viewModel.DoNotSendTaskCompletedNotification,
                            !viewModel.DoNotSendTaskOverdueNotification,
                            !viewModel.DoNotSendTaskDueTomorrowNotification,
                            taskGuid,
                            viewModel.ResponsibilityTaskSiteId.Value,
                            documentsToSave.CreateDocumentRequests);

                        response = _responsibilitiesService.SaveResponsibilityTask(request);
                        _businessSafeSessionManager.CloseSession();
                    }
                        
                    _bus.Publish(new TaskAssigned {TaskGuid = taskGuid});
                    result = Json(new { Success = true, response.HasMultipleFrequencyChangeToTrue });

                }
                catch (ValidationException validationException)
                {
                    ModelState.AddValidationErrors(validationException);
                }
            }
                        
            return result;
        }

        [PermissionFilter(Permissions.ViewResponsibilities)]
        public ActionResult View(long? responsibilityId, long companyId)
        {
            IsReadOnly = true;
            return Edit(responsibilityId, companyId, null);
        }

        [PermissionFilter(Permissions.ViewResponsibilities)]
        public PartialViewResult ViewResponsibilityTask(long companyId, long taskId)
        {
            ViewBag.IsReadOnly = true;

            var viewModel = _viewResponsibilityTaskViewModelFactory
                .WithCompanyId(companyId)
                .WithResponsibilityTaskId(taskId)
                .GetViewModel();

            return PartialView("ViewResponsibilityTask", viewModel);
        }

        [PermissionFilter(Permissions.ViewResponsibilities)]
        public PartialViewResult Complete(long companyId, long taskId)
        {
            var viewModel = _completeResponsibilityTaskViewModelFactory
                .WithCompanyId(companyId)
                .WithResponsibilityTaskId(taskId)
                .GetViewModel();

            return PartialView("_CompleteResponsibilityTask", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.ViewResponsibilities)]
        public JsonResult Complete(CompleteResponsibilityTaskViewModel viewModel, DocumentsToSaveViewModel documentsToSaveViewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid Arguments To Complete Responsibility Task");
            }

            var request = new CompleteResponsibilityTaskRequest
                              {
                                  CompanyId = viewModel.CompanyId,
                                  ResponsibilityTaskId = viewModel.ResponsibilityTaskId,
                                  CompletedComments = viewModel.CompletedComments,
                                  UserId = CurrentUser.UserId,
                                  CreateDocumentRequests = documentsToSaveViewModel.CreateDocumentRequests,
                                  DocumentLibraryIdsToDelete = documentsToSaveViewModel.DeleteDocumentRequests,
                                  CompletedDate = DateTimeOffset.Now
                              };

            using (var session = _businessSafeSessionManager.Session)
            {
                _responsibilityTaskService.Complete(request);
                _businessSafeSessionManager.CloseSession();
            }

            using (var session = _businessSafeSessionManager.Session)
            {
                _responsibilityTaskService.SendTaskCompletedNotificationEmail(request);
                _businessSafeSessionManager.CloseSession();
            }

            return Json(new 
            {
                Success = true,
                Id = viewModel.ResponsibilityTaskId,
            });
        }

        private ActionResult ReturnInvalidCreateResponsibilityViewResult(ResponsibilityViewModel responsibilityViewModel)
        {
            var viewModel = _responsibilityViewModelFactory
                         .WithCompanyId(responsibilityViewModel.CompanyId)
                         .GetViewModel();

            viewModel.IsCreatorResponsibilityOwner = responsibilityViewModel.IsCreatorResponsibilityOwner;
            return View("Create", viewModel);
        }

        private ActionResult ReturnInvalidEditResponsibilityViewResult(ResponsibilityViewModel responsibilityViewModel)
        {
            var viewModel = _responsibilityViewModelFactory
                         .WithCompanyId(responsibilityViewModel.CompanyId)
                         .WithResponsibilityId(responsibilityViewModel.ResponsibilityId)
                         .GetViewModel();

            viewModel.IsCreatorResponsibilityOwner = responsibilityViewModel.IsCreatorResponsibilityOwner;
            return View("Edit", viewModel);
        }

        [PermissionFilter(Permissions.EditResponsibilities)]
        public PartialViewResult Reassign(long companyId, long taskId)
        {
            var model = _reassignResponsibilityTaskViewModelFactory
                .WithCompanyId(companyId)
                .WithResponsibilityTaskId(taskId)
                .GetViewModel();

            return PartialView("_ReassignResponsibilityTask", model);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditResponsibilities)]
        public JsonResult Reassign(ReassignTaskViewModel viewModel)
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
        [PermissionFilter(Permissions.AddResponsibilities)]
        public JsonResult CopyResponsibility(CopyResponsibilityViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorsAsJson();
            }

            var request = new CopyResponsibilityRequest()
            {
                CompanyId = CurrentUser.CompanyId,
                UserId = CurrentUser.UserId,
                OriginalResponsibilityId = viewModel.ResponsibilityId,
                Title = viewModel.Title
            };

            var result = _responsibilitiesService.CopyResponsibility(request);

            return Json(new { Success = true, Id = result });
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
    }
}
