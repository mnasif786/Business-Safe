using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Contracts.Responsibilities;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public class CreateUpdateResponsibilityTaskViewModelFactory : ICreateUpdateResponsibilityTaskViewModelFactory
    {
        private readonly IResponsibilitiesService _responsibilitiesService;
        private readonly IExistingDocumentsViewModelFactory _existingDocumentsViewModelFactory;
        private long _companyId;
        private long _responsibilityId;
        private long? _taskId;
        private bool? _autoLaunchedAfterCreatingResponsibility;

        public CreateUpdateResponsibilityTaskViewModelFactory(
            IResponsibilitiesService responsibilitiesService,
            IExistingDocumentsViewModelFactory existingDocumentsViewModelFactory)
        {
            _responsibilitiesService = responsibilitiesService;
            _existingDocumentsViewModelFactory = existingDocumentsViewModelFactory;
        }

        public ICreateUpdateResponsibilityTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ICreateUpdateResponsibilityTaskViewModelFactory WithResponsibilityId(long responsibilityId)
        {
            _responsibilityId = responsibilityId;
            return this;
        }

        public ICreateUpdateResponsibilityTaskViewModelFactory WithTaskId(long? taskId)
        {
            _taskId = taskId;
            return this;
        }

        public ICreateUpdateResponsibilityTaskViewModelFactory WithAutoLaunchedAfterCreatingResponsibility(bool? autoLaunchedAfterCreatingResponsibility)
        {
            _autoLaunchedAfterCreatingResponsibility = autoLaunchedAfterCreatingResponsibility;
            return this;
        }

        public CreateUpdateResponsibilityTaskViewModel GetViewModel()
        {
            var responsibility = _responsibilitiesService.GetResponsibility(_responsibilityId, _companyId);
            var task = responsibility.ResponsibilityTasks.FirstOrDefault(t => t.Id == _taskId);

            var model = CreateUpdateResponsibilityTaskViewModel.CreateFrom(_companyId, responsibility, task);

            if(!_taskId.HasValue && _autoLaunchedAfterCreatingResponsibility.HasValue && _autoLaunchedAfterCreatingResponsibility.Value)
            {
                model.Title = responsibility.Title;
                model.Description = responsibility.Description;
            }

            model.ExistingDocuments = _existingDocumentsViewModelFactory
                .WithCanDeleteDocuments(false)
                .WithCanEditDocumentType(true)
                .WithDefaultDocumentType(DocumentTypeEnum.Responsibility)
                .WithDocumentOriginType(DocumentOriginType.TaskCreated)
                .GetViewModel(task!=null ? task.Documents : new List<TaskDocumentDto>());

            return model;
        }
    }
}