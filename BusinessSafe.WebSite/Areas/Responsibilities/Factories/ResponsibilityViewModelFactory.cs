using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;
using BusinessSafe.WebSite.Extensions;
using System;

using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public sealed class ResponsibilityViewModelFactory : IResponsibilityViewModelFactory
    {
        private readonly IResponsibilitiesService _responsibilitiesService;
        private readonly IEmployeeService _employeeService;
        private readonly ISiteService _siteService;

        private long _companyId;
        private long? _responsibilityId;
        private bool? _showCreateResponsibilityTaskDialogOnLoad;
        private IList<long> _allowedSiteIds;

        public ResponsibilityViewModelFactory(IResponsibilitiesService responsibilitiesService, ISiteService siteService, IEmployeeService employeeService)
        {
            _responsibilitiesService = responsibilitiesService;
            _siteService = siteService;
            _employeeService = employeeService;
        }

        public IResponsibilityViewModelFactory WithCompanyId(long id)
        {
            _companyId = id;
            return this;
        }

        public IResponsibilityViewModelFactory WithResponsibilityId(long? id)
        {
            _responsibilityId = id;
            return this;
        }

        public IResponsibilityViewModelFactory WithShowCreateResponsibilityTaskDialogOnLoad(bool? showCreateResponsibilityTaskDialogOnLoad)
        {
            _showCreateResponsibilityTaskDialogOnLoad = showCreateResponsibilityTaskDialogOnLoad;
            return this;
        }

        public IResponsibilityViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds)
        {
            _allowedSiteIds = allowedSiteIds;
            return this;
        }      

        public ResponsibilityViewModel GetViewModel()
        {
            var responsibility = GetResponsibility();
            var sites = GetSites();
            var employees = GetEmployees();
            var frequencyOptions = GetFrequencyOptions();
            var categories = GetResponsibilityCategories();
            var reasons = GetResponsibilityReasons();

            var viewModel = new ResponsibilityViewModel
            {
                CompanyId = _companyId,
                ResponsibilityId = _responsibilityId.HasValue ? _responsibilityId.Value : default(long),
                CategoryId = responsibility.ResponsibilityCategory != null ? responsibility.ResponsibilityCategory.Id : default(int),
                Category = responsibility.ResponsibilityCategory != null ? responsibility.ResponsibilityCategory.Category : string.Empty,
                Title = responsibility.Title,
                Description = responsibility.Description,
                SiteId = responsibility.Site != null ? responsibility.Site.Id : default(long),
                Site = responsibility.Site != null ? responsibility.Site.Name : string.Empty,
                ReasonId = responsibility.ResponsibilityReason != null ? responsibility.ResponsibilityReason.Id : default(long),
                Reason = responsibility.ResponsibilityReason != null ? responsibility.ResponsibilityReason.Reason : string.Empty,
                OwnerId = responsibility.Owner != null ? responsibility.Owner.Id : new Guid(),
                Owner = responsibility.Owner != null ? responsibility.Owner.FullName : string.Empty,
                FrequencyId = (int)responsibility.InitialTaskReoccurringType,
                Frequency = responsibility.InitialTaskReoccurringType.ToString(),
                HasMultipleFrequencies = responsibility.HasMultipleFrequencies,
                Sites = sites,
                Employees = employees,
                FrequencyOptions = frequencyOptions,
                Categories = categories,
                Reasons = reasons,
                IsCreatorResponsibilityOwner = true,
                ResponsibilityTasks = MapResponsibilityTasks(responsibility.ResponsibilityTasks.Where(task => task.TaskStatus == TaskStatus.Outstanding)),
                CompletedResponsibilityTasks =
                    MapResponsibilityTasks(
                        responsibility.ResponsibilityTasks.Where(
                            task =>
                            task.TaskStatus == TaskStatus.Completed || task.TaskStatus == TaskStatus.NoLongerRequired)),
                CreateResponsibilityTask = true,
                ShowCreateResponsibilityTaskDialogOnLoad = _showCreateResponsibilityTaskDialogOnLoad.HasValue && _showCreateResponsibilityTaskDialogOnLoad.Value
            };

            return viewModel;
        }

        private IEnumerable<ResponsibilityTasksViewModel> MapResponsibilityTasks(IEnumerable<TaskDto> responsibilityTasks)
        {
            var result = new List<ResponsibilityTasksViewModel>();

            if (responsibilityTasks != null)
            {
                result = responsibilityTasks.Select(task => new ResponsibilityTasksViewModel()
                                                                {
                                                                    Id = task.Id,
                                                                    Title = task.Title,
                                                                    Description = task.Description.TruncateWithEllipsis(50),
                                                                    AssignedTo =  task.TaskAssignedTo != null ? task.TaskAssignedTo.FullName : null,
                                                                    Site = task.Site != null ? task.Site.Name : null,
                                                                    Created = DateTime.Parse(task.CreatedDate),
                                                                    DueDate = task.TaskCompletionDueDate != null ? DateTime.Parse(task.TaskCompletionDueDate) : (DateTime?) null,
                                                                    Status = EnumHelper.GetEnumDescription(task.DerivedDisplayStatus),
                                                                    IsCompleted = task.TaskStatusString==TaskStatus.Completed.ToString(),
                                                                    IsNoLongerRequired = task.TaskStatusString == TaskStatus.NoLongerRequired.ToString(),
                                                                    IsReoccurring = task.TaskReoccurringType!=TaskReoccurringType.None,
                                                                    TaskReoccurringType = task.TaskReoccurringType,
                                                                    TaskReoccurringEndDate = task.TaskReoccurringEndDate,
                                                                }).OrderByDescending(x=>x.Created).ToList();
            }
            return result;
        }

        private ResponsibilityDto GetResponsibility()
        {
            var responsibilityDto = new ResponsibilityDto();
            if (_responsibilityId.HasValue && _responsibilityId.Value != default(long))
            {
                responsibilityDto = _responsibilitiesService.GetResponsibility(_responsibilityId.Value, _companyId);
            }
            return responsibilityDto;
        }

        private IEnumerable<AutoCompleteViewModel> GetResponsibilityReasons()
        {
            var responsibilityReasonDtos = _responsibilitiesService.GetResponsibilityReasons();
            return
                responsibilityReasonDtos.Select(AutoCompleteViewModel.ForResponsibilityReason).OrderBy(x => x.label).
                    AddDefaultOption();
        }

        private IEnumerable<AutoCompleteViewModel> GetResponsibilityCategories()
        {
            var responsibilityCategoryDtos = _responsibilitiesService.GetResponsibilityCategories();
            return
                responsibilityCategoryDtos.Select(AutoCompleteViewModel.ForResponsibilityCategory).AddDefaultOption();
        }

        private IEnumerable<AutoCompleteViewModel> GetFrequencyOptions()
        {
            var taskReoccuringTypes = Enum.GetValues(typeof(TaskReoccurringType)).Cast<TaskReoccurringType>().ToList();
            var result = taskReoccuringTypes.Select(AutoCompleteViewModel.ForTaskReoccurringType).ToList();
            return result;
        }

        private IEnumerable<AutoCompleteViewModel> GetSites()
        {
            var siteDtos = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = _companyId,
                AllowedSiteIds = _allowedSiteIds
            });
            return siteDtos.Select(AutoCompleteViewModel.ForSite).AddDefaultOption();
        }

        private IEnumerable<AutoCompleteViewModel> GetEmployees()
        {
            var employeeDtos = _employeeService.GetEmployeeNames(_companyId).ToList();
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
}