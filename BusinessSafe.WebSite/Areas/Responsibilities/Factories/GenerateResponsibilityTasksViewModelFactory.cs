using System;
using System.Collections;
using System.Linq;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

using NHibernate.Linq;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public class GenerateResponsibilityTasksViewModelFactory : IGenerateResponsibilityTasksViewModelFactory
    {
        private readonly IStatutoryResponsibilityTaskTemplateService _statutoryResponsibilityTaskTemplateService;
        private readonly IEmployeeService _employeeService;
        private readonly IResponsibilitiesService _responsibilityService;
        public long _companyId;
        private IList<long> _siteIds;

        public GenerateResponsibilityTasksViewModelFactory(
            IStatutoryResponsibilityTaskTemplateService statutoryResponsibilityTaskTemplateService, IEmployeeService employee, IResponsibilitiesService responsibilityService)
        {
            _statutoryResponsibilityTaskTemplateService = statutoryResponsibilityTaskTemplateService;
            _employeeService = employee;
            _responsibilityService = responsibilityService;
        }

        public IGenerateResponsibilityTasksViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IGenerateResponsibilityTasksViewModelFactory WithAllowedSiteIds(IList<long> siteIds)
        {
            _siteIds = siteIds;
            return this;
        }

        public GenerateResponsibilityTasksViewModel GetViewModel()
        {
            var responsibilitiesWithUncreatedStatutoryTasks = _responsibilityService.GetStatutoryResponsibilities(_companyId).ToList();
            
            var viewModel = new GenerateResponsibilityTasksViewModel
                            {
                                FrequencyOptions = GetFrequencyOptions(),
                                Employees = GetEmployees(),
                            };

            var selectedSites = responsibilitiesWithUncreatedStatutoryTasks
                .Where(x => x.Site != null);

            if (_siteIds != null && _siteIds.Count > 0)
            {
                selectedSites = selectedSites
                    .Where(x => _siteIds.Contains(x.Site.Id));
            }

            viewModel.SelectedSites = selectedSites
                .Select(x => new ResponsibilityWizardSite() {Id = x.Site.Id, Name = x.Site.Name})
                .Distinct(new SiteComparer())
                .OrderBy(x => x.Name)
                .ToList();


            //set the sites
            viewModel.SelectedSites
                .ForEach(x =>
                        {
                            x.Responsibilities = responsibilitiesWithUncreatedStatutoryTasks
                                .Where(r =>  r.Site != null && r.Site.Id == x.Id)
                                .Select(r => MapToResponsibilityUncreatedTasksViewModel(r));
                        });


            return viewModel;
        }

        private static ResponsibilityUncreatedTasksViewModel MapToResponsibilityUncreatedTasksViewModel(
            ResponsibilityDto responsibilityDto)
            //TODO: Need to get a list of sites for all responsibilities that have the StatutoryResponsibilityTemplateSet that have a CompanyId = _companyId
            //The go through each of them, adding each one to SelectedSites, and adding the srecords from statutoryResponsibilitesTaskTemplates that matches the current site.
        {
            return new ResponsibilityUncreatedTasksViewModel()
                       {
                           Id = responsibilityDto.Id,
                           Description = responsibilityDto.Description,
                           Title = responsibilityDto.Title,
                           StatutoryResponsibilityTasks =
                               responsibilityDto.StatutoryResponsibilityTaskTemplates.Select(
                                   t =>
                                   MapToStatutoryResponsibilityTaskViewModel(responsibilityDto.ResponsibilityTasks, t,
                                                                             responsibilityDto.
                                                                                 InitialTaskReoccurringType))
                       };
        }

        private static StatutoryResponsibilityTaskViewModel MapToStatutoryResponsibilityTaskViewModel(
            IEnumerable<ResponsibilityTaskDto> responsibilityTasks,
            StatutoryResponsibilityTaskTemplateDto taskTemplateDto, TaskReoccurringType responsibilityRecurringType)
        {
            var task =
                responsibilityTasks.FirstOrDefault(
                    t =>
                    t.StatutoryResponsibilityTaskTemplateCreatedFrom != null &&
                    t.StatutoryResponsibilityTaskTemplateCreatedFrom.Id == taskTemplateDto.Id);

            return new StatutoryResponsibilityTaskViewModel()
                       {
                           Id = taskTemplateDto.Id,
                           Description = taskTemplateDto.Description,
                           InitialFrequency = task == null ? taskTemplateDto.TaskReoccurringType : task.TaskReoccurringType,
                           Assignee = task == null ? "--Select Option--" : task.TaskAssignedTo.FullName,
                           StartDate = task == null ? string.Empty : task.TaskCompletionDueDate,
                           EndDate = task != null &&  task.TaskReoccurringEndDate.HasValue ? task.TaskReoccurringEndDate.Value.ToShortDateString() : string.Empty,
                           Title = taskTemplateDto.Title,
                           IsCreated =task!=null
                       };
        }

        public virtual IEnumerable<AutoCompleteViewModel> GetFrequencyOptions()
        {
            var taskReoccuringTypes = Enum.GetValues(typeof(TaskReoccurringType)).Cast<TaskReoccurringType>().ToList();
            var result = taskReoccuringTypes.Select(AutoCompleteViewModel.ForTaskReoccurringType).ToList();
            return result;
        }

        public virtual IEnumerable<AutoCompleteViewModel> GetEmployees()
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
    }

    public class SiteComparer : IEqualityComparer<ResponsibilityWizardSite>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        /// <param name="x">The first object of type <paramref name="T"/> to compare.</param><param name="y">The second object of type <paramref name="T"/> to compare.</param>
        public bool Equals(ResponsibilityWizardSite x, ResponsibilityWizardSite y)
        {
            return x.Id == y.Id;
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param><exception cref="T:System.ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
        public int GetHashCode(ResponsibilityWizardSite obj)
        {
            return (int)obj.Id;
        }
    }
}