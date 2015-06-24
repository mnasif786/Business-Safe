using System;
using System.Linq;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Application.Mappers;

namespace BusinessSafe.Application.Implementations.TaskList
{
    public class TaskService : ITaskService
    {
        private readonly ITaskCategoryRepository _responsibilityTaskCategoryRepository;
        private readonly IPeninsulaLog _log;
        private readonly ITasksRepository _taskRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISiteGroupRepository _siteGroupRepository;
        
        public TaskService(
            ITaskCategoryRepository responsibilityTaskCategoryRepository,
            IPeninsulaLog log,
            ITasksRepository taskRepository,
            IUserForAuditingRepository userForAuditingRepository,
            IEmployeeRepository employeeRepository, 
            ISiteGroupRepository siteGroupRepository)
        {
            _responsibilityTaskCategoryRepository = responsibilityTaskCategoryRepository;
            _log = log;
            _taskRepository = taskRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _employeeRepository = employeeRepository;
            _siteGroupRepository = siteGroupRepository;
        }

        public IEnumerable<TaskCategoryDto> GetTaskCategories()
        {
            _log.Add();

            try
            {
                var taskCategories = _responsibilityTaskCategoryRepository.GetAll();
                return TaskCategoryDto.CreateFrom(taskCategories);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public TaskDetailsSummaryDto GetTaskDetailsSummary(TaskDetailsSummaryRequest request)
        {
            _log.Add(request);

            try
            {
                var riskAssessmentFurtherControlMeasureTask = _taskRepository.GetById(request.RiskAssessmentFurtherControlMeasureId);

                return TaskDetailsSummaryDto.CreateFrom(riskAssessmentFurtherControlMeasureTask);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public IEnumerable<TaskDto> Search(SearchTasksRequest request)
        {
            var siteIdsToSearch = request.AllowedSiteIds;

            if (request.SiteGroupId.HasValue)
            {
                var allSiteIdsForSiteGroup = GetAllSiteIdsForSiteGroup(request);

                siteIdsToSearch = allSiteIdsForSiteGroup.Where(siteId => SiteIdIsInUsersAllowableSites(request, siteId)).ToList();
            }

            if (request.SiteId.HasValue)
            {
                if (RequestedSiteNotInUsersAllowableSites(request))
                {
                    throw new SearchTasksSpecifiedSiteIdNotInAllowableSitesForUserException();
                }

                siteIdsToSearch = new List<long> {request.SiteId.Value};
            }

            var tasks = _taskRepository.Search(
                request.CompanyId,
                request.EmployeeIds,
                request.CreatedFrom,
                request.CreatedTo,
                request.CompletedFrom,
                request.CompletedTo,
                request.TaskCategoryId,
                request.TaskStatusId,
                request.ShowDeleted,
                request.ShowCompleted,
                siteIdsToSearch,
                request.Title,
                request.Page,
                request.PageSize,
                request.OrderBy,
                request.Ascending
                );

            return new TaskDtoMapper().MapWithAssignedTo(tasks);

        }

        public int Count(SearchTasksRequest request)
        {
            var siteIdsToSearch = request.AllowedSiteIds;

            if (request.SiteGroupId.HasValue)
            {
                var allSiteIdsForSiteGroup = GetAllSiteIdsForSiteGroup(request);

                siteIdsToSearch = allSiteIdsForSiteGroup.Where(siteId => SiteIdIsInUsersAllowableSites(request, siteId)).ToList();
            }

            if (request.SiteId.HasValue)
            {
                if (RequestedSiteNotInUsersAllowableSites(request))
                {
                    throw new SearchTasksSpecifiedSiteIdNotInAllowableSitesForUserException();
                }

                siteIdsToSearch = new List<long> { request.SiteId.Value };
            }

            var result = _taskRepository.Count(
                request.CompanyId,
                request.EmployeeIds,
                request.CreatedFrom,
                request.CreatedTo,
                request.CompletedFrom,
                request.CompletedTo,
                request.TaskCategoryId,
                request.TaskStatusId,
                request.ShowDeleted,
                request.ShowCompleted,
                siteIdsToSearch,
                request.Title);

            return result;
        }

        private static bool SiteIdIsInUsersAllowableSites(SearchTasksRequest request, long siteGroupSiteId)
        {
            return request.AllowedSiteIds == null || request.AllowedSiteIds.Contains(siteGroupSiteId);
        }

        private IEnumerable<long> GetAllSiteIdsForSiteGroup(SearchTasksRequest request)
        {
            var siteGroup = _siteGroupRepository.GetByIdAndCompanyId(request.SiteGroupId.GetValueOrDefault(), request.CompanyId);
            return siteGroup.GetThisAndAllDescendants().Where(x => x is Site).Select(x => x.Id);
        }

        private static bool RequestedSiteNotInUsersAllowableSites(SearchTasksRequest request)
        {
            return request.AllowedSiteIds != null && !request.AllowedSiteIds.Contains(request.SiteId.GetValueOrDefault());
        }

        public bool HasEmployeeGotOutstandingTasks(Guid employeeId, long companyId)
        {
            _log.Add(new object[] { employeeId, companyId });

            try
            {
                var searchTasksRequest = CreateSearchOutstandingTasksRequest(employeeId, companyId);
                var outstandingTasks = Search(searchTasksRequest);
                return outstandingTasks.Any();
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public bool HasCompletedTasks(MarkTaskAsDeletedRequest request)
        {
            try
            {
                var task = _taskRepository.GetById(request.TaskId);
                return task.HasCompletedTasks();
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void MarkTaskAsDeleted(MarkTaskAsDeletedRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var task = _taskRepository.GetByIdAndCompanyId(request.TaskId, request.CompanyId);

                task.MarkForDelete(user);

                _taskRepository.SaveOrUpdate(task);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public virtual void ReassignTask(ReassignTaskToEmployeeRequest request)
        {
            _log.Add(request);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var employeeReassigningTaskTo = _employeeRepository.GetByIdAndCompanyId(request.ReassignTaskToId,
                                                                                    request.CompanyId);
            var task = _taskRepository.GetById(request.TaskId);
            task.ReassignTask(employeeReassigningTaskTo, user);
            _taskRepository.Save(task);

            _taskRepository.Flush();
        }

        public void BulkReassignTasks(BulkReassignTasksToEmployeeRequest request)
        {
            _log.Add(request);

            foreach (var reassignRequest in request.ReassignRequests)
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(reassignRequest.UserId, reassignRequest.CompanyId);
                var employeeReassigningTaskTo = _employeeRepository.GetByIdAndCompanyId(
                    reassignRequest.ReassignTaskToId, reassignRequest.CompanyId);
                var task = _taskRepository.GetById(reassignRequest.TaskId);
                task.ReassignTask(employeeReassigningTaskTo, user);
                _taskRepository.Save(task);
            }

            _taskRepository.Flush();
        }

        public void MarkTaskAsNoLongerRequired(MarkTaskAsNoLongerRequiredRequest request)
        {
            _log.Add(request);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);

            var task = _taskRepository.GetById(request.TaskId);
            task.MarkAsNoLongerRequired(user);

            _taskRepository.SaveOrUpdate(task);
        }

        public TaskListSummaryResponse GetOutstandingTasksSummary(SearchTasksRequest request)
        {
            var siteIdsToSearch = request.AllowedSiteIds;

            if (request.SiteGroupId.HasValue)
            {
                var allSiteIdsForSiteGroup = GetAllSiteIdsForSiteGroup(request);

                siteIdsToSearch = allSiteIdsForSiteGroup.Where(siteId => SiteIdIsInUsersAllowableSites(request, siteId)).ToList();
            }

            if (request.SiteId.HasValue)
            {
                if (RequestedSiteNotInUsersAllowableSites(request))
                {
                    throw new SearchTasksSpecifiedSiteIdNotInAllowableSitesForUserException();
                }

                siteIdsToSearch = new List<long> {request.SiteId.Value};
            }

            var tasks = _taskRepository.Search(
                request.CompanyId,
                request.EmployeeIds,
                request.CreatedFrom,
                request.CreatedTo,
                request.CompletedFrom,
                request.CompletedTo,
                request.TaskCategoryId,
                request.TaskStatusId,
                request.ShowDeleted,
                request.ShowCompleted,
                siteIdsToSearch,
                request.Title, 0, 0, TaskOrderByColumn.None, true);

            var today = DateTime.Today.Date;

            return new TaskListSummaryResponse()
                       {
                           TotalOverdueTasks = tasks.Count(t => t.TaskCompletionDueDate.Value.Date < today),
                           TotalPendingTasks = tasks.Count(t => t.TaskCompletionDueDate.Value.Date >= today)
                       };

        }

        private static SearchTasksRequest CreateSearchOutstandingTasksRequest(Guid employeeId, long companyId)
        {
            var searchTasksRequest = new SearchTasksRequest()
                                         {
                                             CompanyId = companyId,
                                             EmployeeIds = new List<Guid>()
                                                               {
                                                                   employeeId
                                                               },
                                             TaskStatusId = (int)TaskStatus.Outstanding
                                         };
            return searchTasksRequest;
        }
    }
}