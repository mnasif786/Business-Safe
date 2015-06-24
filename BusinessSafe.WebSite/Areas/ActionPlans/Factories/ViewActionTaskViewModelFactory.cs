using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Helpers;

namespace BusinessSafe.WebSite.Areas.ActionPlans.Factories
{
    public class ViewActionTaskViewModelFactory : IViewActionTaskViewModelFactory
    {
        private long _actionTaskId;
        private long _companyId;

        private readonly IActionTaskService _actionTaskService;
        private readonly IExistingDocumentsViewModelFactory _existingDocumentsViewModelFactory;      

        public ViewActionTaskViewModelFactory( IActionTaskService actionTaskService,
                        IExistingDocumentsViewModelFactory existingDocumentsViewModelFactory)
        {
            _actionTaskService = actionTaskService;
            _existingDocumentsViewModelFactory = existingDocumentsViewModelFactory;
        }

        public IViewActionTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IViewActionTaskViewModelFactory WithActionTaskId(long actionTaskId)
        {
            _actionTaskId = actionTaskId;
            return this;
        }

        public ViewModels.ViewActionTaskViewModel GetViewModel()
        {
            var actionTask = _actionTaskService.GetByIdAndCompanyId(_actionTaskId, _companyId);

            var viewModel = new ViewActionTaskViewModel();
            viewModel.CompanyId = _companyId;
            viewModel.ActionTaskId = _actionTaskId;
            viewModel.Title = actionTask.Title;
            viewModel.Description = actionTask.Description;
            viewModel.IsRecurring = actionTask.IsReoccurring;
            viewModel.TaskReoccurringType = actionTask.TaskReoccurringType;
            viewModel.TaskReoccurringTypeId = (int)actionTask.TaskReoccurringType;
            viewModel.ReoccurringStartDate = actionTask.TaskCompletionDueDate;
            viewModel.ReoccurringEndDate = actionTask.TaskReoccurringEndDate;
            viewModel.CompletionDueDate = actionTask.TaskCompletionDueDate;

            viewModel.ActionTaskSite = actionTask.Site != null
                                                   ? actionTask.Site.Name
                                                   : string.Empty;

            viewModel.ActionTaskSiteId = actionTask.Site != null
                                                   ? actionTask.Site.Id
                                                   : default(long);

            viewModel.AssignedTo = actionTask.TaskAssignedTo != null
                                       ? actionTask.TaskAssignedTo.FullName
                                       : string.Empty;

            viewModel.AssignedToId = actionTask.TaskAssignedTo != null
                                         ? actionTask.TaskAssignedTo.Id
                                         : Guid.Empty;

            viewModel.DoNotSendTaskAssignedNotification = !actionTask.SendTaskNotification;
            viewModel.DoNotSendTaskCompletedNotification = !actionTask.SendTaskCompletedNotification;
            viewModel.DoNotSendTaskOverdueNotification = !actionTask.SendTaskOverdueNotification;
            viewModel.DoNotSendTaskDueTomorrowNotification = !actionTask.SendTaskDueTomorrowNotification;
            viewModel.TaskStatusId = actionTask.TaskStatusId;

            viewModel.TaskCompletedDate =
                (string)
                (actionTask.TaskCompletedDate.HasValue
                     ? actionTask.TaskCompletedDate.GetValueOrDefault().ToLocalTime().ToString("g")
                     : null);

            viewModel.TaskCompletedComments = actionTask.TaskCompletedComments;

            viewModel.ExistingDocuments = _existingDocumentsViewModelFactory
                .WithCanDeleteDocuments(false)
                .WithDefaultDocumentType(DocumentTypeEnum.Responsibility)
                .GetViewModel(actionTask.Documents);


            viewModel.ActionSummary = new ActionSummaryViewModel
                                          {
                                              Id = _actionTaskId,
                                              Reference = actionTask.Action.Reference,
                                              AreaOfNonCompliance = actionTask.Action.AreaOfNonCompliance,
                                              ActionRequired = actionTask.Action.ActionRequired,
                                              GuidanceNotes = actionTask.Action.GuidanceNote,
                                              TargetTimescale = actionTask.Action.TargetTimescale,
                                              AssignedTo = actionTask.Action.AssignedTo,
                                              DueDate = actionTask.Action.DueDate,
                                              Status = EnumHelper.GetEnumDescription(actionTask.Action.Status),
                                              Category = actionTask.Action.Category,
                                              
                                          };




      
            return viewModel;
        }
    }
}