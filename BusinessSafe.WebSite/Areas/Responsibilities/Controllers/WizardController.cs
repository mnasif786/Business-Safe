using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Messages.Events;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Controllers
{
    public class WizardController : BaseController
    {
        private readonly ISelectResponsibilitiesViewModelFactory _selectResponsibilitiesViewModelFactory;
        private readonly IGenerateResponsibilitiesViewModelFactory _generateResponsibilitiesViewModelFactory;
        private readonly IGenerateResponsibilityTasksViewModelFactory _generateResponsibilityTasksViewModelFactory;
        private readonly IResponsibilitiesService _responsibilitiesService;
        private readonly IBus _bus;
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;

        private const string alertMessageKey = "alertMessage";
        private const string noResponsibilitiesSelectedAlertMessage = "Please select at least one Responsibility to generate";

        public WizardController(
            ISelectResponsibilitiesViewModelFactory selectResponsibilitiesViewModelFactory,
            IGenerateResponsibilitiesViewModelFactory generateResponsibilitiesViewModelFactory,
            IGenerateResponsibilityTasksViewModelFactory generateResponsibilityTasksViewModelFactory,
            IResponsibilitiesService responsibilitiesService,
            IBus bus,
            IBusinessSafeSessionManager businessSafeSessionManager)
        {
            _selectResponsibilitiesViewModelFactory = selectResponsibilitiesViewModelFactory;
            _generateResponsibilitiesViewModelFactory = generateResponsibilitiesViewModelFactory;
            _generateResponsibilityTasksViewModelFactory = generateResponsibilityTasksViewModelFactory;
            _responsibilitiesService = responsibilitiesService;
            _bus = bus;
            _businessSafeSessionManager = businessSafeSessionManager;

        }

        [ResponsibilityWizardTabActionFilter(ResponsibilityWizardTabs.SelectResponsibilities)]
        [PermissionFilter(Permissions.AddResponsibilities)]
        public ActionResult SelectResponsibilities(string selectedResponsibilityTemplateIds)
        {
            var model = _selectResponsibilitiesViewModelFactory
                .WithSelectedResponsibilityTemplates(CommaSeparatedStringHelper.ConvertToLongArray(selectedResponsibilityTemplateIds))
                .GetViewModel();
            return View(model);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddResponsibilities)]
        public ActionResult SelectResponsibilities(long[] selectedResponsibilities)
        {
            if (selectedResponsibilities == null || selectedResponsibilities.Length == 0)
            {
                TempData[alertMessageKey] = noResponsibilitiesSelectedAlertMessage;
                return RedirectToAction("SelectResponsibilities", new {});
            }

            return RedirectToAction("GenerateResponsibilities", new { selectedResponsibilityTemplateIds = string.Join(",",selectedResponsibilities) });
        }

        [ResponsibilityWizardTabActionFilter(ResponsibilityWizardTabs.GenerateResponsibilities)]
        [PermissionFilter(Permissions.AddResponsibilities)]
        public ActionResult GenerateResponsibilities(string selectedResponsibilityTemplateIds)
        {
            if (string.IsNullOrEmpty(selectedResponsibilityTemplateIds))
            {
                TempData[alertMessageKey] = noResponsibilitiesSelectedAlertMessage;
                return RedirectToAction("SelectResponsibilities", new { });
            }

            var model = _generateResponsibilitiesViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithResponsibilityTemplateIds(CommaSeparatedStringHelper.ConvertToLongArray(selectedResponsibilityTemplateIds))
                .WithAllowedSiteIds(CurrentUser.GetSitesFilter())
                .GetViewModel();

            return View(model);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddResponsibilities)]
        public JsonResult GenerateResponsibilities(CreateResponsibilityFromSiteAndResponsibilityTemplateModel model)
        {
            ValidateGenerateResponsibilities(model);
            if (!ModelState.IsValid)
            {
                return ModelStateErrorsAsJson();
            }

            var request = new CreateResponsibilityFromWizardRequest
                                {
                                    CompanyId = CurrentUser.CompanyId,
                                    SiteIds = model.SiteIds,
                                    UserId = CurrentUser.UserId,
                                    ResponsibilityFromTemplateDetails = model.Responsibilities
                                        .Select(x => new ResponsibilityFromTemplateDetail()
                                                    {
                                                        ResponsibilityTemplateId = x.ResponsibilityTemplateId,
                                                        FrequencyId = x.FrequencyId,
                                                        ResponsiblePersonEmployeeId = x.ResponsiblePersonEmployeeId.Value
                                                    })
                                        .ToList()
                                };

            _responsibilitiesService.CreateResponsibilitiesFromWizard(request);
            return Json(new { Success = true });
        }

        private void ValidateGenerateResponsibilities(CreateResponsibilityFromSiteAndResponsibilityTemplateModel model)
        {
            if (model.SiteIds == null || model.SiteIds.Any() == false)
            {
                ModelState.AddModelError("SiteIds", "Please select the Sites you wish to add Responsibilities to");
            }

            if (model.Responsibilities == null || model.Responsibilities.Any() == false)
            {
                ModelState.AddModelError("Responsibilities", "Please select at least one Responsibility you wish to generate");
            }
            else
            {
                var responsibilityTemplateIdInError = false;
                var frequencyIdInError = false;
                var employeeIdInError = false;
                
                foreach (var resp in model.Responsibilities)
                {
                    if (resp.ResponsibilityTemplateId == default(long) && !responsibilityTemplateIdInError)
                    {
                        ModelState.AddModelError("Responsibilities", "Please select the Responsibility you wish to generate");
                        responsibilityTemplateIdInError = true;
                    }
                    if (resp.FrequencyId == TaskReoccurringType.None && !frequencyIdInError)
                    {
                        ModelState.AddModelError("Responsibilities", "Please select the Monitoring Frequency for each of the selected Responsibilities");
                        frequencyIdInError = true;
                    }
                    if ((resp.ResponsiblePersonEmployeeId == Guid.Empty || resp.ResponsiblePersonEmployeeId == null) && !employeeIdInError)
                    {
                        ModelState.AddModelError("Responsibilities", "Please select a Responsible Person for the selected Responsibilities");
                        employeeIdInError = true;
                    }
                }
            }


        }

        /// <summary>
        /// Displays Statutory Task Templates from those Responsiblities created from Statutory Responsibility Templates 
        /// where the related tasks are yet to be created.
        /// </summary>
        /// <param name="selectedResponsibilityTemplateIds">used by ResponsibilityWizardTabActionFilterAttribute to setup tabs</param>
        /// <returns></returns>
        [ResponsibilityWizardTabActionFilter(ResponsibilityWizardTabs.GenerateTasks)]
        [PermissionFilter(Permissions.AddResponsibilities)]
        public ActionResult GenerateTasks(string selectedResponsibilityTemplateIds)
        {
            var viewModel = _generateResponsibilityTasksViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithAllowedSiteIds(CurrentUser.GetSitesFilter())
                .GetViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddResponsibilities)]
        public JsonResult GenerateTask(GenerateResponsibilityTaskViewModel viewModel)
        {
            JsonResult result;

            ValidateGenerateResponsibilityTasks(viewModel);
            if (!ModelState.IsValid)
            {
                result = ModelStateErrorsAsJson();
            }
            else
            {
                var taskGuid = Guid.NewGuid();
                using (var session = _businessSafeSessionManager.Session)
                {
                    var request = CreateResponsibilityTasksFromWizardRequest.Create(CurrentUser.CompanyId,
                                                                                    CurrentUser.UserId, viewModel.SiteId,
                                                                                    viewModel.ResponsibilityId,
                                                                                    viewModel.TaskId,
                                                                                    viewModel.Frequency,
                                                                                    viewModel.Owner, viewModel.StartDate,
                                                                                    viewModel.EndDate,
                                                                                    taskGuid);

                    _responsibilitiesService.CreateResponsibilityTaskFromWizard(request);
                    _businessSafeSessionManager.CloseSession();
                }

                _bus.Publish(new TaskAssigned { TaskGuid = taskGuid });

                result = Json(new {Success = true});
            }

            return result;
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddResponsibilities)]
        public JsonResult BulkGenerateTasks(List<GenerateResponsibilityTaskViewModel> viewModel)
        {
            JsonResult result;

            ValidateGenerateResponsibilityTasks(viewModel);

            if (!ModelState.IsValid)
            {
                result = ModelStateErrorsAsJson();
            }
            else
            {
                var taskDetails = new List<CreateResponsibilityTasksFromWizardRequest>();
                var createdTaskGuids = new List<Guid>();

                foreach (var model in viewModel)
                {
                    var taskGuid = Guid.NewGuid();
                    createdTaskGuids.Add(taskGuid);
                   
                    taskDetails.Add(CreateResponsibilityTasksFromWizardRequest.Create(CurrentUser.CompanyId,
                        CurrentUser.UserId, 
                        model.SiteId,
                        model.ResponsibilityId,
                        model.TaskId, 
                        model.Frequency,
                        model.Owner, 
                        model.StartDate,
                        model.EndDate,
                        taskGuid));
                }

                var request = new CreateManyResponsibilityTaskFromWizardRequest
                              {
                                  TaskDetails = taskDetails,
                                  CreatingUserId = CurrentUser.UserId,
                                  CompanyId = CurrentUser.CompanyId
                              };
                
                using (var session = _businessSafeSessionManager.Session)
                {
                    _responsibilitiesService.CreateManyResponsibilityTaskFromWizard(request);
                    _businessSafeSessionManager.CloseSession();
                }

                foreach (var taskGuid in createdTaskGuids)
                {
                    _bus.Publish(new TaskAssigned { TaskGuid = taskGuid });
                }

                result = Json(new { Success = true });
            }

            return result;
        }

        private void ValidateGenerateResponsibilityTasks(IEnumerable<GenerateResponsibilityTaskViewModel> viewModel)
        {
            foreach (var model in viewModel)
            {
                ValidateGenerateResponsibilityTasks(model);
            }
        }

        private void ValidateGenerateResponsibilityTasks(GenerateResponsibilityTaskViewModel model)
        {
            // todo: use data annotations???
            if (model.Frequency == TaskReoccurringType.None)
            {
                ModelState.AddModelError("FrequencyId", "Please select a frequency for the selected task.");
            }

            DateTime startDate = DateTime.MinValue;
            if(model.StartDate==null || !DateTime.TryParse(model.StartDate, out startDate))
            {
                ModelState.AddModelError("StartDate", "Please select a valid start date for the selected task.");
            }

            if (startDate < DateTime.Now.Date)
            {
                ModelState.AddModelError("StartDate", "Start date can not be in the past.");
            }

            DateTime endDate = DateTime.MaxValue;
            if (model.EndDate != null && !DateTime.TryParse(model.EndDate, out endDate))
            {
                ModelState.AddModelError("EndDate", "Please select a valid end date for the selected task.");
            }

            if (model.EndDate != null && endDate < startDate)
            {
                ModelState.AddModelError("EndDate", "Please select an end date after the start date");
            }

            if (model.Owner == null || model.Owner == Guid.Empty)
            {
                ModelState.AddModelError("Assignee", "Please select an assignee for the selected task.");
            }
        }
    }
}
