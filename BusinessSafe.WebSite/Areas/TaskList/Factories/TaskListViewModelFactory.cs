using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Contracts;
using BusinessSafe.WebSite.Extensions;
using System;
using BusinessSafe.WebSite.ViewModels;
using NHibernate;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Areas.TaskList.Factories
{
    public sealed class TaskListViewModelFactory : ITaskListViewModelFactory
    {
        private readonly IUserService _userService;
        private readonly ISiteGroupService _siteGroupService;
        private readonly ISiteService _siteService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaskService _taskService;
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;
        private long _companyId;
        private Guid? _employeeId;
        private DateTime? _createdFrom;
        private DateTime? _createdTo;
        private DateTime? _completedFrom;
        private DateTime? _completedTo;
        private long _taskCategoryId;
        private int _taskStatusId;
        private CustomPrincipal _user;
        private bool _isBulkReassign;
        private bool _showDeleted;
        private bool _showCompleted;
        private long? _siteId;
        private long? _siteGroupId;
        private IList<long> _allowedSiteIds;
        private string _title;
        private const int DEFAULT_PAGE_SIZE = 10;

        public TaskListViewModelFactory(IEmployeeService employeeService, ITaskService taskService,
                                        IUserService userService, ISiteGroupService siteGroupService,
                                        ISiteService siteService, IBusinessSafeSessionManager businessSafeSessionManager)
        {
            _employeeService = employeeService;
            _taskService = taskService;
            _userService = userService;
            _siteGroupService = siteGroupService;
            _siteService = siteService;
            _businessSafeSessionManager = businessSafeSessionManager;
        }

        public ITaskListViewModelFactory WithSiteId(long? siteId)
        {
            _siteId = siteId;
            return this;
        }

        public ITaskListViewModelFactory WithSiteGroupId(long? siteGroupId)
        {
            _siteGroupId = siteGroupId;
            return this;
        }

        public ITaskListViewModelFactory WithCompanyId(long Id)
        {
            _companyId = Id;
            return this;
        }

        public ITaskListViewModelFactory WithEmployeeId(Guid? employeeId)
        {
            _employeeId = employeeId;
            return this;
        }

        public ITaskListViewModelFactory WithCreatedFrom(string createdFrom)
        {
            _createdFrom = createdFrom.ParseAsDateTime();
            return this;
        }

        public ITaskListViewModelFactory WithCreatedTo(string createdTo)
        {
            _createdTo = createdTo.ParseAsDateTime();
            return this;
        }

        public ITaskListViewModelFactory WithCompletedFrom(string completedFrom)
        {
            _completedFrom = completedFrom.ParseAsDateTime();
            return this;
        }

        public ITaskListViewModelFactory WithCompletedTo(string completedTo)
        {
            _completedTo = completedTo.ParseAsDateTime();
            return this;
        }

        public ITaskListViewModelFactory WithTaskCategoryId(long taskCategoryId)
        {
            _taskCategoryId = taskCategoryId;
            return this;
        }

        public ITaskListViewModelFactory WithTaskStatusId(int taskStatusId)
        {
            _taskStatusId = taskStatusId;
            return this;
        }

        public ITaskListViewModelFactory WithUser(CustomPrincipal currentUser)
        {
            _user = currentUser;
            return this;
        }

        public ITaskListViewModelFactory WithShowDeleted(bool showDeleted)
        {
            _showDeleted = showDeleted;
            return this;
        }

        public ITaskListViewModelFactory WithBulkReassignMode(bool isBulkReassign)
        {
            _isBulkReassign = isBulkReassign;
            return this;
        }

        public ITaskListViewModelFactory WithShowCompleted(bool showCompleted)
        {
            _showCompleted = showCompleted;
            return this;
        }

        public ITaskListViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds)
        {
            _allowedSiteIds = allowedSiteIds;
            return this;
        }

        private Guid? _userEmployeeId;
        private int _page;
        private int _pageSize;
        private string _orderBy;

        public ITaskListViewModelFactory WithUserEmployeeId(CustomPrincipal currentUser)
        {
            var user = _userService.GetIncludingEmployeeAndSiteByIdAndCompanyId(currentUser.UserId, currentUser.CompanyId);
            if (user != null && user.Employee != null)
            {
                _userEmployeeId = user.Employee.Id;
            }
            return this;
        }

        public TaskListViewModel GetViewModel()
        {
            using (var session = _businessSafeSessionManager.Session)
            {
                session.FlushMode = FlushMode.Never;

                var employeeIds = GetEmployeeIdsToSearchFor();
                    // If the user wants to view tasks for ALL employees in there company, then retrieve the list of employeeids to find tasks for
                var sites = GetSites();
                var siteGroups = GetSiteGroups();
                var employees = GetEmployees();

                var request = new SearchTasksRequest()
                                  {
                                      CompanyId = _companyId,
                                      EmployeeIds = employeeIds,
                                      CreatedFrom = _createdFrom,
                                      CreatedTo = _createdTo,
                                      CompletedFrom = _completedFrom,
                                      CompletedTo = _completedTo,
                                      TaskCategoryId = _taskCategoryId,
                                      TaskStatusId = _taskStatusId,
                                      ShowDeleted = _showDeleted,
                                      ShowCompleted = _showCompleted,
                                      SiteId = _siteId,
                                      SiteGroupId = _siteGroupId,
                                      AllowedSiteIds = _allowedSiteIds,
                                      Title = _title,
                                      Page = _page != default(int) ? _page : 1,
                                      PageSize = _pageSize != default(int) ? _pageSize : DEFAULT_PAGE_SIZE,
                                      OrderBy = GetOrderBy(),
                                      Ascending = Ascending()
                                  };

                var total = _taskService.Count(request);
                var employeeTasksDto = _taskService.Search(request);

                var viewModel = new TaskListViewModel
                                    {
                                        CompanyId = _companyId,
                                        EmployeeId = (_userEmployeeId.HasValue ? _userEmployeeId : _employeeId),
                                        Employees = employees,
                                        CreatedFrom = _createdFrom,
                                        CreatedTo = _createdTo,
                                        CompletedFrom = _completedFrom,
                                        CompletedTo = _completedTo,
                                        TaskCategories = GetTaskCategories(),
                                        Tasks = TaskViewModel.CreateFrom(employeeTasksDto),
                                        IsBulkReassign = _isBulkReassign,
                                        Sites = sites,
                                        SiteGroups = siteGroups,
                                        SiteId = _siteId,
                                        SiteGroupId = _siteGroupId,
                                        IsShowDeleted = _showDeleted,
                                        IsShowCompleted = _showCompleted,
                                        PageSize = _pageSize != default(int) ? _pageSize : DEFAULT_PAGE_SIZE,
                                        Total = total
                                    };

                return viewModel;
            }
        }

        private IEnumerable<AutoCompleteViewModel> GetSiteGroups()
        {
            var linkedGroupsDtos = _siteGroupService.GetByCompanyId(_companyId);
            return linkedGroupsDtos.Select(AutoCompleteViewModel.ForSiteGroup).AddDefaultOption();
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

        private IEnumerable<Guid> GetEmployeeIdsToSearchFor()
        {

            if (IsSearchingForCurrentUser())
            {
                return new[] {_userEmployeeId.Value};
            }

            if (IsSearchingForAllUsersInCompany())
            {
                return _employeeService.GetAll(_companyId).Select(e => e.Id).ToList();
            }


            return new[] { _employeeId.Value };

        }

        private bool IsSearchingForAllUsersInCompany()
        {
            return _employeeId == null;
        }

        private bool IsSearchingForCurrentUser()
        {
            return _userEmployeeId != null;
        }

        private IEnumerable<AutoCompleteViewModel> GetEmployees()
        {
            var employees = _employeeService.GetEmployeeNames(_companyId)
                .OrderBy(x=> x.Surname);
            return employees.Select(AutoCompleteViewModel.ForEmployee)
                .OrderBy(x=> x.label)
                .AddDefaultOption(String.Empty);
        }

        private IEnumerable<AutoCompleteViewModel> GetTaskCategories()
        {
            var taskCategoryDtos = _taskService.GetTaskCategories();
            return taskCategoryDtos.Select(AutoCompleteViewModel.ForTaskCategory).AddDefaultOption();
        }

        public ITaskListViewModelFactory WithOrderBy(string orderBy)
        {
            _orderBy = orderBy;
            return this;
        }

        public TaskListSummaryViewModel GetSummaryViewModel()
        {
            var employeeIds = GetEmployeeIdsToSearchFor();

            var request = new SearchTasksRequest
                              {
                                  CompanyId = _companyId,
                                  SiteGroupId = _siteGroupId,
                                  SiteId = _siteId,
                                  TaskCategoryId = _taskCategoryId,
                                  EmployeeIds = employeeIds,
                                  AllowedSiteIds = _allowedSiteIds
                              };

            var response = _taskService.GetOutstandingTasksSummary(request);

            return new TaskListSummaryViewModel()
                       {
                           TotalOverdueTasks = response.TotalOverdueTasks,
                           TotalPendingTasks = response.TotalPendingTasks
                       };
        }

        public ITaskListViewModelFactory WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public ITaskListViewModelFactory WithPage(int page)
        {
            _page = page;
            return this;
        }

        public ITaskListViewModelFactory WithPageSize(int pageSize)
        {
            _pageSize = pageSize;
            return this;
        }



        private TaskOrderByColumn GetOrderBy()
        {
            var orderBy = string.Empty;
            if (!string.IsNullOrEmpty(_orderBy))
            {
                string[] parts = _orderBy.Split('-');
                if (parts.Length == 2)
                {
                    orderBy = parts[0];
                }
            }

            TaskOrderByColumn orderyByColumn;
            Enum.TryParse(orderBy, out orderyByColumn);

            return orderyByColumn;

        }

        private bool Ascending()
        {
            var isAscending = true;

            if (!string.IsNullOrEmpty(_orderBy))
            {
                string[] parts = _orderBy.Split('-');
                if (parts.Length == 2)
                {
                    isAscending = parts[1] == "asc";
                }
            }
            return isAscending;
        }


    }
}