using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public class GenerateResponsibilitiesViewModelFactory : IGenerateResponsibilitiesViewModelFactory
    {
        private readonly ISiteService _siteService;
        private readonly IStatutoryResponsibilityTemplateService _statutoryResponsibilityTemplateService;
        private readonly IEmployeeService _employeeService;

        private IList<long> _siteIds;
        private long _companyId;
        private long[] _selectedResponsibilityIds;

        public GenerateResponsibilitiesViewModelFactory(
            ISiteService siteService,
            IStatutoryResponsibilityTemplateService statutoryResponsibilityTemplateService,
            IEmployeeService employeeService,
            IStatutoryResponsibilityTaskTemplateService statutoryResponsibilityTaskTemplateService)
        {
            _siteService = siteService;
            _statutoryResponsibilityTemplateService = statutoryResponsibilityTemplateService;
            _employeeService = employeeService;
        }

        public IGenerateResponsibilitiesViewModelFactory WithResponsibilityTemplateIds(long[] selectedResponsibilityIds)
        {
            _selectedResponsibilityIds = selectedResponsibilityIds;
            return this;
        }

        public IGenerateResponsibilitiesViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IGenerateResponsibilitiesViewModelFactory WithAllowedSiteIds(IList<long> siteIds)
        {
            _siteIds = siteIds;
            return this;
        }

        public GenerateResponsibilitiesViewModel GetViewModel()
        {
            var templates =
                _statutoryResponsibilityTemplateService.GetStatutoryResponsibilityTemplatesByIds(
                    _selectedResponsibilityIds);

            return new GenerateResponsibilitiesViewModel
                       {
                           Sites = GetRequestedSites(),
                           Employees = GetEmployees(),
                           FrequencyOptions = GetFrequencyOptions(),
                           ResponsibilityTemplates = templates.OrderBy(x => x.ResponsibilityCategory.Category)
                               .Select(
                                   t =>
                                   new StatutoryResponsibilityViewModel
                                       {
                                           Id = (int) t.Id,
                                           Category = t.ResponsibilityCategory.Category,
                                           Title = t.Title,
                                           Description = t.Description,
                                           ResponsibilityReason = t.ResponsibilityReason.Reason,
                                           Tasks = MapResponsibilityTasks(t.ResponsibilityTasks)
                                       })
                       };
        }

        private IEnumerable<AutoCompleteViewModel> GetEmployees()
        {
            var employeeSearchRequest = CreateEmployeeSearchRequest();
            var employeeDtos = _employeeService.Search(employeeSearchRequest).ToList();
            return employeeDtos.Select(AutoCompleteViewModel.ForEmployeeNoJobTitle).AddDefaultOption();
        }


        private SearchEmployeesRequest CreateEmployeeSearchRequest()
        {
            var employeeSearchRequest = new SearchEmployeesRequest()
            {
                CompanyId = _companyId,
                ShowDeleted = false
            };
            return employeeSearchRequest;
        }

        private IEnumerable<AutoCompleteViewModel> GetFrequencyOptions()
        {
            var taskReoccuringTypes = Enum.GetValues(typeof(TaskReoccurringType)).Cast<TaskReoccurringType>().ToList();
            var result = taskReoccuringTypes.Select(AutoCompleteViewModel.ForTaskReoccurringType).ToList();
            return result;
        }

        private IEnumerable<StatutoryResponsibilityTaskViewModel> MapResponsibilityTasks(
            IEnumerable<StatutoryResponsibilityTaskTemplateDto> responsibilityTasks)
        {
            var result = new List<StatutoryResponsibilityTaskViewModel>();

            if (responsibilityTasks != null)
            {
                result = responsibilityTasks.Select(x => new StatutoryResponsibilityTaskViewModel
                                                             {
                                                                 Id = x.Id,
                                                                 Title = x.Title,
                                                                 Description = x.Description,
                                                                 InitialFrequency = x.TaskReoccurringType
                                                             }).ToList();
            }

    return result;

        }

        private IEnumerable<LookupDto> GetRequestedSites()
        {
            var request = new SearchSitesRequest
                              {
                                  CompanyId = _companyId,
                                  AllowedSiteIds = _siteIds
                              };
            var result = _siteService.Search(request);
            return result.Select(x => new LookupDto
                                          {
                                              Id = x.Id,
                                              Name = x.Name
                                          }).ToList();
        } 
    }
}
