using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Application.Tests.Implementations.ResponsibilitiesServiceTests;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Application.Response;

namespace BusinessSafe.Application.Implementations.Responsibilities
{
    public class ResponsibilitiesService : IResponsibilitiesService
    {
        private readonly IResponsibilityRepository _responsibilityRepository;
        private readonly IResponsibilityCategoryRepository _responsibilityCategoryRepository;
        private readonly IResponsibilityReasonRepository _responsibilityReasonRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly ITaskCategoryRepository _taskCategoryRepository;
        private readonly IDocumentParameterHelper _documentParameterHelper;
        private readonly IStatutoryResponsibilityTemplateRepository _statutoryResponsibilityTemplateRepository;
        private readonly IStatutoryResponsibilityTaskTemplateRepository _statutoryResponsibilityTaskTemplateRepository;
        private readonly IPeninsulaLog _log;

        public ResponsibilitiesService(IResponsibilityRepository responsibilityRepository,
                                       IResponsibilityCategoryRepository responsibilityCategoryRepository,
                                       IResponsibilityReasonRepository responsibilityReasonRepository,
                                       IEmployeeRepository employeeRepository,
                                        ISiteRepository siteRepository,
                                       IUserForAuditingRepository userForAuditingRepository,
                                       ITaskCategoryRepository taskCategoryRepository,
                                       IDocumentParameterHelper documentParameterHelper,
                                       IStatutoryResponsibilityTemplateRepository statutoryResponsibilityTemplateRepository,
                                       IStatutoryResponsibilityTaskTemplateRepository
                                           statutoryResponsibilityTaskTemplateRepository, IPeninsulaLog log)
        {
            _responsibilityRepository = responsibilityRepository;
            _responsibilityCategoryRepository = responsibilityCategoryRepository;
            _responsibilityReasonRepository = responsibilityReasonRepository;
            _siteRepository = siteRepository;
            _employeeRepository = employeeRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _taskCategoryRepository = taskCategoryRepository;
            _documentParameterHelper = documentParameterHelper;
            _statutoryResponsibilityTemplateRepository = statutoryResponsibilityTemplateRepository;
            _statutoryResponsibilityTaskTemplateRepository = statutoryResponsibilityTaskTemplateRepository;
            _log = log;
        }

        public IEnumerable<ResponsibilityCategoryDto> GetResponsibilityCategories()
        {
            _log.Add();

            try
            {
                var categories = _responsibilityCategoryRepository.GetAll();

                categories = categories
                    .OrderBy(x => x.Sequence)
                    .ThenBy(x => x.Category);

                return new ResponsibilityCategoryDtoMapper().Map(categories);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public IEnumerable<ResponsibilityReasonDto> GetResponsibilityReasons()
        {
            _log.Add();

            try
            {
                var reasons = _responsibilityReasonRepository.GetAll();
                return new ResponsibilityReasonDtoMapper().Map(reasons);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public IEnumerable<ResponsibilityDto> Search(SearchResponsibilitiesRequest request)
        {
            var responsibilities = _responsibilityRepository.Search(
                request.CurrentUserId,
                request.AllowedSiteIds,
                request.CompanyId,
                request.ResponsibilityCategoryId,
                request.SiteId,
                request.SiteGroupId,
                request.Title,
                request.CreatedFrom,
                request.CreatedTo,
                request.ShowDeleted,
                request.ShowCompleted,
                request.Page,
                request.PageSize,                
                request.OrderBy,
                request.Ascending
            );
            return responsibilities.Select(x => new ResponsibilityDtoMapper(x).WithTasks().Map());
        }

        public int Count(SearchResponsibilitiesRequest request)
        {
            var result = _responsibilityRepository.Count(
                request.CurrentUserId,
                request.AllowedSiteIds,
                request.CompanyId,
                request.ResponsibilityCategoryId,
                request.SiteId,
                request.SiteGroupId,
                request.Title,
                request.CreatedFrom,
                request.CreatedTo,
                request.ShowDeleted,
                request.ShowCompleted
            );

            return result;
        }

        public long SaveResponsibility(SaveResponsibilityRequest request)
        {
            _log.Add(request);
            long result;
            try
            {
                var category = _responsibilityCategoryRepository.GetById(request.ResponsibilityCategoryId);
                var site = _siteRepository.GetById(request.SiteId);
                var reason = _responsibilityReasonRepository.GetById(request.ResponsibilityReasonId);
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var owner = GetResponsibilityOwner(request, user);

                var responsibility = GetResponsibility(request, reason, owner, user, site, category);

                _responsibilityRepository.SaveOrUpdate(responsibility);

                result = responsibility.Id;
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
            return result;
        }

        public void CreateResponsibilitiesFromWizard(CreateResponsibilityFromWizardRequest request)
        {
            _log.Add(request);
            var templates = GetRequestedResponsibilityTemplates(request);
            var sites = _siteRepository.GetByIds(request.SiteIds);
            if (sites.Count() != request.SiteIds.Distinct().Count() || sites.Any(site => site.ClientId != request.CompanyId))
            {
                throw new SiteRequestedForStatutoryResponsibilityNotValidException();
            }

            var employees = GetRequestedOwnerEmployeeTemplates(request);
            if (employees.Count() != request.ResponsibilityFromTemplateDetails.Select(x => x.ResponsiblePersonEmployeeId).Distinct().Count() || employees.Any(employee => employee.CompanyId != request.CompanyId))
            {
                throw new EmployeeRequestedForStatutoryResponsibilityNotValidException();
            }

            var responsibilities = _responsibilityRepository.GetStatutoryByCompanyId(request.CompanyId);

            var creatingUser = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);

            foreach (var resp in request.ResponsibilityFromTemplateDetails)
            {
                foreach (var siteId in request.SiteIds)
                {
                    var currTemplate = templates.First(x => x.Id == resp.ResponsibilityTemplateId);

                    if (!responsibilities.Any(x => x.Site != null && x.Site.Id == siteId && x.StatutoryResponsibilityTemplateCreatedFrom == currTemplate))
                    {
                        _responsibilityRepository.Save(
                            Responsibility.Create(
                                request.CompanyId,
                                currTemplate.ResponsibilityCategory,
                                currTemplate.Title,
                                currTemplate.Description,
                                sites.Single(x => x.Id == siteId),
                                currTemplate.ResponsibilityReason,
                                employees.Single(x => x.Id == resp.ResponsiblePersonEmployeeId),
                                resp.FrequencyId,
                                currTemplate,
                                creatingUser));
                    }
                }
            }
        }

        public long CopyResponsibility(CopyResponsibilityRequest request)
        {
            _log.Add(request);

            try
            {
                var responsibility = _responsibilityRepository.GetByIdAndCompanyId(request.OriginalResponsibilityId , request.CompanyId);
                if (responsibility == null)
                {
                    throw (new ResponsibilityNotFoundException(request.OriginalResponsibilityId, request.CompanyId));
                }

                var creatingUser = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var newResponsibility = responsibility.CopyWithoutSiteAndOwner(request.Title, creatingUser);
                _responsibilityRepository.Save(newResponsibility);
                return newResponsibility.Id;
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public SaveResponsibilityTaskResponse SaveResponsibilityTask(SaveResponsibilityTaskRequest request)
        {
            _log.Add(request);

            try 
            {                
                var responsibility = _responsibilityRepository.GetByIdAndCompanyId(request.ResponsibilityId, request.CompanyId);
                var user = _userForAuditingRepository.GetById(request.UserId);
                var employee = _employeeRepository.GetByIdAndCompanyId(request.TaskAssignedToId, request.CompanyId);
                var taskCategory = _taskCategoryRepository.GetResponsibilityTaskCategory();
                var site = _siteRepository.GetByIdAndCompanyId(request.SiteId, request.CompanyId);
                var createDocumentParameterObjects = _documentParameterHelper.GetCreateDocumentParameters(request.CreateDocumentRequests, request.CompanyId);
                var hasMultipleFrequencyChangeToTrue = false;

                responsibility.HasMultipleFrequencyChangeToTrue += delegate(object sender, EventArgs e)
                {
                    hasMultipleFrequencyChangeToTrue = true;
                };

                var task = GetResponsibilityTask(request, employee, user, taskCategory, responsibility, site, createDocumentParameterObjects);

                if (request.StatutoryResponsibilityTaskTemplateId != default(long))
                {
                    SetResponsibilityTaskValuesFromTemplate(request.StatutoryResponsibilityTaskTemplateId, task);
                }

                _responsibilityRepository.SaveOrUpdate(responsibility);

                return new SaveResponsibilityTaskResponse
                           {
                               ResponsibilityTaskId = task != null ? task.Id : default(long),
                               HasMultipleFrequencyChangeToTrue = hasMultipleFrequencyChangeToTrue
                           };
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        private void SetResponsibilityTaskValuesFromTemplate(long statutoryResponsibilityTaskTemplateId, ResponsibilityTask task)
        {
            var template = _statutoryResponsibilityTaskTemplateRepository.GetById(statutoryResponsibilityTaskTemplateId);
            if (template != null)
            {
                task.Title = template.Title;
                task.Description = template.Description;
                task.StatutoryResponsibilityTaskTemplateCreatedFrom = template;
            }
        }

        public bool HasUndeletedTasks(long responsibilityId, long companyId)
        {
            var responsibility = _responsibilityRepository.GetByIdAndCompanyId(responsibilityId, companyId);
            if (responsibility == null)
            {
                var e = new ResponsibilityNotFoundException(responsibilityId, companyId);
                _log.Add(e);
                throw (e);
            }
            return responsibility.HasUndeletedTasks();
        }

        public void Delete(long responsibilityId, long companyId, Guid actioningUserId)
        {
            var responsibility = _responsibilityRepository.GetByIdAndCompanyId(responsibilityId, companyId);
            if (responsibility == null)
            {
                var e = new ResponsibilityNotFoundException(responsibilityId, companyId);
                _log.Add(e);
                throw (e);
            }

            var user = _userForAuditingRepository.GetByIdAndCompanyId(actioningUserId, companyId);
            responsibility.MarkForDelete(user);
        }

        public void Undelete(long responsibilityId, long companyId, Guid actioningUserId)
        {
            var responsibility = _responsibilityRepository.GetByIdAndCompanyId(responsibilityId, companyId);
            if (responsibility == null)
            {
                var e = new ResponsibilityNotFoundException(responsibilityId, companyId);
                _log.Add(e);
                throw (e);
            }

            var user = _userForAuditingRepository.GetByIdAndCompanyId(actioningUserId, companyId);
            responsibility.ReinstateFromDelete(user);
        }

        public ResponsibilityDto GetResponsibility(long id, long companyId)
        {
            _log.Add(new[] { id, companyId });

            try
            {
                var responsibility = _responsibilityRepository.GetByIdAndCompanyId(id, companyId);
                if (responsibility == null)
                {
                    throw (new ResponsibilityNotFoundException(id, companyId));
                }
                return new ResponsibilityDtoMapper(responsibility).WithTasks().Map();
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        private Employee GetResponsibilityOwner(SaveResponsibilityRequest request, UserForAuditing user)
        {
            Guid id = request.OwnerId == null ? user.Employee.Id : request.OwnerId.Value;
            return _employeeRepository.GetByIdAndCompanyId(id, request.CompanyId);
        }

        private Responsibility GetResponsibility(SaveResponsibilityRequest request, ResponsibilityReason reason, Employee owner, UserForAuditing user, Site site, ResponsibilityCategory category)
        {
            Responsibility responsibility;

            if (request.ResponsibilityId != default(long))
            {
                responsibility = _responsibilityRepository.GetById(request.ResponsibilityId);

                responsibility.Update(
                    request.CompanyId,
                    category,
                    request.Title,
                    request.Description,
                    site,
                    reason,
                    owner,
                    request.TaskReoccurringType,
                    user
                    );
            }
            else
            {
                responsibility = Responsibility.Create(
                    request.CompanyId,
                    category,
                    request.Title,
                    request.Description,
                    site,
                    reason,
                    owner,
                    request.TaskReoccurringType, null,
                    user
                    );
            }
            return responsibility;
        }

        private static ResponsibilityTask GetResponsibilityTask(
            SaveResponsibilityTaskRequest request,
            Employee employee,
            UserForAuditing user,
            TaskCategory taskCategory,
            Responsibility responsibility,
            Site site,
            IEnumerable<CreateDocumentParameters> createDocumentParameters)
        {
            ResponsibilityTask task;
            if (request.TaskId != default(long))
            {
                task = responsibility.ResponsibilityTasks.FirstOrDefault(t => t.Id == request.TaskId);
                if (task != null)
                {
                    task.Update(
                        request.Title,
                        request.Description,
                        request.TaskCompletionDueDate,
                        request.TaskStatus,
                        employee,
                        user,
                        createDocumentParameters,
                        taskCategory,
                        request.TaskReoccurringTypeId,
                        request.TaskReoccurringEndDate,
                        request.SendTaskNotification,
                        request.SendTaskCompletedNotification,
                        request.SendTaskOverdueNotification,
                        request.SendTaskDueTomorrowNotification,
                        request.TaskGuid,
                        site
                        );
                }
            }
            else
            {
                task = ResponsibilityTask.Create(
                    request.Title,
                    request.Description,
                    request.TaskCompletionDueDate,
                    request.TaskStatus,
                    employee,
                    user,
                    createDocumentParameters,
                    taskCategory,
                    request.TaskReoccurringTypeId,
                    request.TaskReoccurringEndDate,
                    request.SendTaskNotification,
                    request.SendTaskCompletedNotification,
                    request.SendTaskOverdueNotification,
                    request.SendTaskDueTomorrowNotification,
                    request.TaskGuid,
                    site,
                    responsibility
                    );
            }
            return task;
        }

        /// <summary>
        /// returns statutory responsibilities that have statutory tasks that haven't been created yet
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<ResponsibilityDto> GetStatutoryResponsibiltiesWithUncreatedStatutoryTasks(long companyId)
        {
            var responsibilities = _responsibilityRepository.GetStatutoryByCompanyId(companyId).ToList();

            var filteredResponsibilities = responsibilities
                .Where(x => x.GetUncreatedStatutoryResponsibilityTaskTemplates().Any())
                .Select(x => new ResponsibilityDtoMapper(x).WithUncreatedStatutoryResponsibilityTaskTemplates().Map())
                .ToList();

            return filteredResponsibilities;
        }

        // todo: refactor this to use CreateManyResponsibilityTaskFromWizard below, just with a single taskDetail - vl 310713
        public void CreateResponsibilityTaskFromWizard(CreateResponsibilityTasksFromWizardRequest request)
        {
            var saveResponsibilityTaskRequest = SaveResponsibilityTaskRequest.Create(
                            request.CompanyId,
                            request.ResponsibilityId,
                            default(long),
                            string.Empty,
                            string.Empty,
                            (int)request.Frequency,
                            request.StartDate.ToShortDateString(),
                            request.EndDate,
                            request.UserId,
                            request.AssigneeId,
                            (int) TaskStatus.Outstanding,
                            request.StartDate.ToShortDateString(),
                            true,
                            true,
                            true,
                            true,
                            request.TaskGuid,
                            request.SiteId,
                            new List<CreateDocumentRequest>(),
                            request.TaskTemplateId);

            SaveResponsibilityTask(saveResponsibilityTaskRequest);
        }

        public void CreateManyResponsibilityTaskFromWizard(CreateManyResponsibilityTaskFromWizardRequest request)
        {
            var responsibilitiesRequested = _responsibilityRepository.GetByIds(request.TaskDetails.Select(x => x.ResponsibilityId).Distinct().ToList());
           
            var creatingUser = _userForAuditingRepository.GetByIdAndCompanyId(request.CreatingUserId, request.CompanyId);
            var taskCategory = _taskCategoryRepository.GetResponsibilityTaskCategory();

            var requestedAssignees = GetRequestedAssignees(request);
            var requestedSites = GetRequestedSites(request);

            foreach (var responsibility in responsibilitiesRequested)
            {
                foreach (var detail in request.TaskDetails.Where(x => x.ResponsibilityId == responsibility.Id))
                {
                    var assignee = requestedAssignees.Single(x => x.Id == detail.AssigneeId);
                    var site = requestedSites.Single(x => x.Id == detail.SiteId);

                    var task = GetResponsibilityTask(
                        SaveResponsibilityTaskRequest.Create(
                            detail.CompanyId,
                            detail.ResponsibilityId,
                            default(long),
                            string.Empty,
                            string.Empty,
                            (int)detail.Frequency,
                            detail.StartDate.ToShortDateString(),
                            detail.EndDate,
                            detail.UserId,
                            detail.AssigneeId,
                            (int) TaskStatus.Outstanding,
                            detail.StartDate.ToShortDateString(),
                            true,
                            true,
                            true,
                            true,
                            detail.TaskGuid,
                            detail.SiteId,
                            new List<CreateDocumentRequest>(),
                            detail.TaskTemplateId),
                        assignee,
                        creatingUser,
                        taskCategory,
                        responsibility,
                        site,
                        new List<CreateDocumentParameters>()
                    );

                    if (detail.TaskTemplateId != default(long))
                    {
                        SetResponsibilityTaskValuesFromTemplate(detail.TaskTemplateId, task);
                    }
                }

                _responsibilityRepository.Save(responsibility);
            }
        }

        private IEnumerable<Employee> GetRequestedAssignees(CreateManyResponsibilityTaskFromWizardRequest request)
        {
            var requestedAssigneeEmployeeIds = request.TaskDetails.Select(x => x.AssigneeId).Distinct().ToList();
            var requestedAssignees = _employeeRepository.GetByIds(requestedAssigneeEmployeeIds);
            if (requestedAssignees.Count() != requestedAssigneeEmployeeIds.Count())
            {
                throw new ArgumentException("Requested Employees for generating Statutory Tasks could not be retrieved");
            }
            if (requestedAssignees.Any(x => x.CompanyId != request.CompanyId))
            {
                throw new ArgumentException("Requested Employees are not from the requested Company");
            }
            return requestedAssignees;
        }

        private IEnumerable<Site> GetRequestedSites(CreateManyResponsibilityTaskFromWizardRequest request)
        {
            var requestedSiteIds = request.TaskDetails.Select(x => x.SiteId).Distinct().ToList();
            var requestedSites = _siteRepository.GetByIds(requestedSiteIds);
            if (requestedSites.Count() != requestedSiteIds.Count())
            {
                throw new ArgumentException("Requested Sites for generating Statutory Tasks could not be retrieved");
            }
            if (requestedSites.Any(x => x.ClientId != request.CompanyId))
            {
                throw new ArgumentException("Requested Sites are not from the requested Company");
            }
            return requestedSites;
        }

        private IEnumerable<StatutoryResponsibilityTemplate> GetRequestedResponsibilityTemplates(CreateResponsibilityFromWizardRequest request)
        {
            var templateIds = request.ResponsibilityFromTemplateDetails.Select(req => req.ResponsibilityTemplateId).ToList();
            return _statutoryResponsibilityTemplateRepository.GetByIds(templateIds);
        }

        private IEnumerable<Employee> GetRequestedOwnerEmployeeTemplates(CreateResponsibilityFromWizardRequest request)
        {
            var ids = request.ResponsibilityFromTemplateDetails.Select(req => req.ResponsiblePersonEmployeeId).ToList();
            return _employeeRepository.GetByIds(ids);
        }

        public List<ResponsibilityDto> GetStatutoryResponsibilities(long companydId)
        {

            var statutoryResponsibilities = _responsibilityRepository.GetStatutoryByCompanyId(companydId);

            return statutoryResponsibilities
                .Select(x => new ResponsibilityDtoMapper(x)
                    .WithStatutoryResponsibilityTaskTemplates()
                    .WithTasks()
                    .Map())
                    .ToList();

        }
    }

    public class CreateResponsibilityResponse
    {
        public bool Success { get; set; }
        public long ResponsibilityId { get; set; }
    }
}
