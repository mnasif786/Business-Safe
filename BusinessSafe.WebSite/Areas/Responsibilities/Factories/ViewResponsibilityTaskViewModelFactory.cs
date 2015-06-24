using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public class ViewResponsibilityTaskViewModelFactory : IViewResponsibilityTaskViewModelFactory
    {
        private readonly IResponsibilityTaskService _responsibilityTaskService;
        private readonly IExistingDocumentsViewModelFactory _existingDocumentsViewModelFactory;
        private long _responsibilityTaskId;
        private long _companyId;

        public ViewResponsibilityTaskViewModelFactory(
            IResponsibilityTaskService responsibilityTaskService,
            IExistingDocumentsViewModelFactory existingDocumentsViewModelFactory)
        {
            _responsibilityTaskService = responsibilityTaskService;
            _existingDocumentsViewModelFactory = existingDocumentsViewModelFactory;
        }

        public IViewResponsibilityTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IViewResponsibilityTaskViewModelFactory WithResponsibilityTaskId(long responsibilityTaskId)
        {
            _responsibilityTaskId = responsibilityTaskId;
            return this;
        }

        public ViewResponsibilityTaskViewModel GetViewModel()
        {
            var responsibilityTask = _responsibilityTaskService.GetByIdAndCompanyId(_responsibilityTaskId, _companyId);

            return ViewResponsibilityTaskViewModel.CreateFrom(responsibilityTask);
        }

    }
}