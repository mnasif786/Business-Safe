using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Application.Mappers;
using FluentValidation;

namespace BusinessSafe.Application.Implementations.PersonalRiskAssessments
{
    public class PersonalRiskAssessmentService : IPersonalRiskAssessmentService
    {
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;
        private readonly IPersonalRiskAssessmentRepository _personalRiskAssessmentRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IChecklistRepository _checklistRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPeninsulaLog _log;
        private readonly ISiteRepository _siteRepository;
        private readonly IRiskAssessorRepository _riskAssessorRepository;
        private readonly IEmployeeChecklistRepository _employeeChecklistRepository;
        

        public PersonalRiskAssessmentService(
            IPersonalRiskAssessmentRepository personalRiskAssessmentRepository,
            IUserForAuditingRepository userRepo,
            IEmployeeRepository employeeRepository,
            IChecklistRepository checklistRepository,
            IPeninsulaLog log,
            IRiskAssessmentRepository riskAssessmentRepository, 
            ISiteRepository siteRepository, 
            IRiskAssessorRepository riskAssessorRepository,
            IEmployeeChecklistRepository employeeChecklistRepository)
        {
            _personalRiskAssessmentRepository = personalRiskAssessmentRepository;
            _userForAuditingRepository = userRepo;
            _checklistRepository = checklistRepository;
            _employeeRepository = employeeRepository;
            _log = log;
            _riskAssessmentRepository = riskAssessmentRepository;
            _siteRepository = siteRepository;
            _riskAssessorRepository = riskAssessorRepository;
            _employeeChecklistRepository = employeeChecklistRepository;
        }

        public long CreateRiskAssessment(CreatePersonalRiskAssessmentRequest request)
        {
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            new CreateRiskAssessmentValidator<PersonalRiskAssessment>(_riskAssessmentRepository).ValidateAndThrow(request);

            Site site = null;
            if (request.SiteId != null)
            {
                site = _siteRepository.GetByIdAndCompanyId(request.SiteId.Value, request.CompanyId);
            }

            RiskAssessor riskAssessor = null;
            if (request.RiskAssessorId.HasValue)
            {
                riskAssessor = _riskAssessorRepository.GetByIdAndCompanyId(request.RiskAssessorId.Value, request.CompanyId);
            }

            var riskAssessment = PersonalRiskAssessment.Create(request.Title,
                                                       request.Reference,
                                                       request.CompanyId,
                                                       user,
                                                       request.Location,
                                                       request.TaskProcessDescription,
                                                       site,
                                                       request.AssessmentDate,
                                                       riskAssessor
                                                       ,request.IsSensitive);

            _personalRiskAssessmentRepository.Save(riskAssessment);

            return riskAssessment.Id;
        }

        public long CreateRiskAssessmentWithChecklist(CreateRiskAssessmentRequest request, Guid employeeChecklistId)
        {
            var checklist = _employeeChecklistRepository.GetById(employeeChecklistId);
            if (checklist == null)
                throw new EmployeeChecklistNotFoundException(employeeChecklistId);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            
            

            new CreateRiskAssessmentValidator<PersonalRiskAssessment>(_riskAssessmentRepository).ValidateAndThrow(request);

            var riskAssessment = PersonalRiskAssessment.Create(request.Title,
                                                       request.Reference,
                                                       request.CompanyId,
                                                       user);

            riskAssessment.AddChecklist(checklist, user);
            
            _personalRiskAssessmentRepository.Save(riskAssessment);

            return riskAssessment.Id;
        }

        public long CreateRiskAssessmentFromChecklist(Guid employeeChecklistId, Guid currentUserId)
        {
            var employeeChecklist = _employeeChecklistRepository.GetById(employeeChecklistId);
            var user = _userForAuditingRepository.GetByIdAndCompanyId(currentUserId, employeeChecklist.Employee.CompanyId);
            var createPRARequest = new CreateRiskAssessmentRequest()
                                       {
                                           CompanyId = employeeChecklist.Employee.CompanyId,
                                           Title = string.Format("{0}_{1}{2}", employeeChecklist.Employee.FullName, employeeChecklist.Checklist.Title, employeeChecklist.FriendlyReference),
                                           Reference = employeeChecklist.FriendlyReference,
                                           UserId = user.Id
                                       };
            new CreateRiskAssessmentValidator<PersonalRiskAssessment>(_riskAssessmentRepository).ValidateAndThrow(createPRARequest);

            var riskAssessment = PersonalRiskAssessment.Create(createPRARequest.Title,
                                                               createPRARequest.Reference,
                                                               createPRARequest.CompanyId,
                                                               user);

            riskAssessment.AddChecklist(employeeChecklist, user);

            _personalRiskAssessmentRepository.Save(riskAssessment);

            return riskAssessment.Id;

        }

        public PersonalRiskAssessmentDto GetRiskAssessment(long riskAssessmentId, long companyId, Guid currentUserId)
        {
            var riskAssessment = _personalRiskAssessmentRepository.GetByIdAndCompanyId(riskAssessmentId, companyId, currentUserId);

            if (!riskAssessment.CanUserAccess(currentUserId))
            {
                throw new Exception("Invalid attempt to access Personal Risk Assessment with Id " + riskAssessmentId);
            }

            return new RiskAssessmentDtoMapper().MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor(riskAssessment) as PersonalRiskAssessmentDto;
        }

        public PersonalRiskAssessmentDto GetWithReviews(long riskAssessmentId, long companyId, Guid currentUserId)
        {
            var riskAssessment = _personalRiskAssessmentRepository.GetByIdAndCompanyId(riskAssessmentId, companyId, currentUserId);

            if (!riskAssessment.CanUserAccess(currentUserId))
            {
                throw new Exception("Invalid attempt to access Personal Risk Assessment with Id " + riskAssessmentId);
            }

            return new RiskAssessmentDtoMapper().MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessorAndReviews(riskAssessment) as PersonalRiskAssessmentDto;
        }

        public PersonalRiskAssessmentDto GetRiskAssessmentWithHazards(long riskAssessmentId, long companyId, Guid currentUserId)
        {
            var riskAssessment = LoadRiskAssessment(riskAssessmentId, companyId, currentUserId);
            return PersonalRiskAssessmentDto.CreateFromWithHazards(riskAssessment);
        }

        public PersonalRiskAssessmentDto GetWithChecklistGeneratorEmployeesAndChecklists(long riskAssessmentId, long companyId, Guid currentUserId)
        {
            var riskAssessment = LoadRiskAssessment(riskAssessmentId, companyId, currentUserId);
            return new PersonalRiskAssessmentDtoMapper().MapWithChecklistGeneratorEmployeesAndChecklists(riskAssessment);
        }

        public PersonalRiskAssessmentDto GetWithEmployeeChecklists(long riskAssessmentId, long companyId, Guid currentUserId)
        {
            var riskAssessment = LoadRiskAssessment(riskAssessmentId, companyId, currentUserId);
            return new PersonalRiskAssessmentDtoMapper().MapWithEmployeeChecklists(riskAssessment);
        }

        public virtual IEnumerable<PersonalRiskAssessmentDto> Search(SearchRiskAssessmentsRequest request)
        {
            var riskAssessments = _personalRiskAssessmentRepository.Search(
                request.Title,
                request.CompanyId,
                request.AllowedSiteIds,
                request.CreatedFrom,
                request.CreatedTo,
                request.SiteGroupId,
                request.SiteId,
                request.CurrentUserId,
                request.ShowDeleted,
                request.ShowArchived,
                request.Page,
                request.PageSize,
                request.OrderBy, 
                request.OrderByDirection
                );

            var riskAssessmentDtos = new RiskAssessmentDtoMapper().MapWithSiteAndRiskAssessor(riskAssessments);
            var personalRiskAssessmentDto = riskAssessmentDtos.Select(riskAssessmentDto => riskAssessmentDto as PersonalRiskAssessmentDto);
            return personalRiskAssessmentDto;
        }

        public int Count(SearchRiskAssessmentsRequest request)
        {
            return _personalRiskAssessmentRepository.Count(
                request.Title,
                request.CompanyId,
                request.AllowedSiteIds,
                request.CreatedFrom,
                request.CreatedTo,
                request.SiteGroupId,
                request.SiteId,
                request.CurrentUserId,
                request.ShowDeleted,
                request.ShowArchived
                );
        }

        private PersonalRiskAssessment LoadRiskAssessment(long riskAssessmentId, long companyId, Guid currentUserId)
        {
            var riskAssessment = _personalRiskAssessmentRepository.GetByIdAndCompanyId(riskAssessmentId, companyId, currentUserId);

            if (!riskAssessment.CanUserAccess(currentUserId))
            {
                throw new Exception("Invalid attempt to access Personal Risk Assessment with Id " + riskAssessmentId);
            }

            return riskAssessment;
        }

        public void UpdateRiskAssessmentSummary(UpdatePersonalRiskAssessmentSummaryRequest request)
        {
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessor = request.RiskAssessorId.HasValue ? _riskAssessorRepository.GetByIdAndCompanyId(request.RiskAssessorId.Value, request.CompanyId) : null;
            var site = request.SiteId.HasValue ? _siteRepository.GetByIdAndCompanyId(request.SiteId.Value, request.CompanyId) : null;
            var riskAssessment = _personalRiskAssessmentRepository.GetByIdAndCompanyId(request.Id, request.CompanyId, request.UserId);

            if (!riskAssessment.CanUserAccess(request.UserId))
            {
                throw new Exception("Invalid attempt to access Personal Risk Assessment with Id " + request.Id);
            }

            new UpdateRiskAssessmentValidator<PersonalRiskAssessment>(_riskAssessmentRepository).ValidateAndThrow(request);
            riskAssessment.UpdateSummary(request.Title, request.Reference, request.AssessmentDate, riskAssessor, request.Sensitive, site, user);
            _personalRiskAssessmentRepository.SaveOrUpdate(riskAssessment);
        }

        public void SaveChecklistGenerator(SaveChecklistGeneratorRequest request)
        {
            var checklists = _checklistRepository.GetByIds(request.ChecklistIds).ToList();
            var employeesParameters = new List<EmployeesWithNewEmailsParameters>();

            foreach (var employeeRequest in request.RequestEmployees)
            {
                employeesParameters.Add(
                    new EmployeesWithNewEmailsParameters
                        {
                            Employee = _employeeRepository.GetById(employeeRequest.EmployeeId),
                            NewEmail = employeeRequest.NewEmail
                        }
                    );
            }

            var user = _userForAuditingRepository.GetById(request.CurrentUserId);
            var personalRiskAssessment = _personalRiskAssessmentRepository.GetById(request.PersonalRiskAssessmentId);
            personalRiskAssessment.SaveChecklistGenerator(request.HasMultipleChecklistRecipients, employeesParameters, checklists, request.Message, user, request.SendCompletedChecklistNotificationEmail, request.CompletionDueDateForChecklists, request.CompletionNotificationEmailAddress);
            _personalRiskAssessmentRepository.Update(personalRiskAssessment);
        }

        public void AddEmployeesToChecklistGenerator(AddEmployeesToChecklistGeneratorRequest request)
        {
            var personalRiskAssessment = _personalRiskAssessmentRepository
                .GetByIdAndCompanyId(request.PersonalRiskAssessmentId, request.CompanyId, request.CurrentUserId);

            if (!personalRiskAssessment.CanUserAccess(request.CurrentUserId))
            {
                throw new Exception("Invalid attempt to access Personal Risk Assessment with Id " + request.PersonalRiskAssessmentId);
            }

            var employees = _employeeRepository.GetByIds(request.EmployeeIds);
            var currentUser = _userForAuditingRepository.GetById(request.CurrentUserId);
            personalRiskAssessment.AddEmployeesToChecklistGenerator(employees.ToList(), currentUser);
            _personalRiskAssessmentRepository.Update(personalRiskAssessment);
        }

        public void RemoveEmployeeFromCheckListGenerator(long riskAssessmentId, long companyId, Guid employeeId, Guid userId)
        {

            var riskAssessment = _personalRiskAssessmentRepository.GetByIdAndCompanyId(riskAssessmentId, companyId, userId);

            if (!riskAssessment.CanUserAccess(userId))
            {
                throw new Exception("Invalid attempt to access Personal Risk Assessment with Id " + riskAssessmentId);
            }

            var user = _userForAuditingRepository.GetById(userId);

            riskAssessment.RemoveCheckListGeneratorForEmployee(employeeId,user);
            _personalRiskAssessmentRepository.Update(riskAssessment);
        }

        public void ResetChecklistAfterGenerate(ResetAfterChecklistGenerateRequest request)
        {
            var riskAssessment = _personalRiskAssessmentRepository.GetByIdAndCompanyId(request.PersonalRiskAssessmentId, request.CompanyId, request.CurrentUserId);

            if (!riskAssessment.CanUserAccess(request.CurrentUserId))
            {
                throw new Exception("Invalid attempt to access Personal Risk Assessment with Id " + request.PersonalRiskAssessmentId);
            }

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.CurrentUserId, request.CompanyId);
            riskAssessment.ResetAfterGeneratingEmployeeChecklists(user);
            _personalRiskAssessmentRepository.SaveOrUpdate(riskAssessment);
        }

        public void SetAsGenerating(long personalRiskAssessmentId)
        {
            var personalRiskAssessment = _personalRiskAssessmentRepository.GetById(personalRiskAssessmentId);
            personalRiskAssessment.PersonalRiskAssessementEmployeeChecklistStatus = PersonalRiskAssessementEmployeeChecklistStatusEnum.Generating;

            _personalRiskAssessmentRepository.SaveOrUpdate(personalRiskAssessment);
        }

        /// <summary>
        /// this is used by the API
        /// </summary>
        /// <param name="request"></param>
        public void UpdateRiskAssessment(SavePersonalRiskAssessmentRequest request)
        {
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessor = request.RiskAssessorId.HasValue ? _riskAssessorRepository.GetByIdAndCompanyId(request.RiskAssessorId.Value, request.CompanyId) : null;
            var site = request.SiteId.HasValue ? _siteRepository.GetByIdAndCompanyId(request.SiteId.Value, request.CompanyId) : null;
            var riskAssessment = _personalRiskAssessmentRepository.GetByIdAndCompanyId(request.Id, request.CompanyId, request.UserId);

            if (!riskAssessment.CanUserAccess(request.UserId))
            {
                throw new Exception("Invalid attempt to access Personal Risk Assessment with Id " + request.Id);
            }

            new UpdateRiskAssessmentValidator<PersonalRiskAssessment>(_riskAssessmentRepository).ValidateAndThrow(request);
            riskAssessment.UpdateSummary(request.Title, request.Reference, request.AssessmentDate, riskAssessor, request.Sensitive, site, user);
            riskAssessment.UpdatePremisesInformation(request.Location,request.TaskProcessDescription,user);
            _personalRiskAssessmentRepository.SaveOrUpdate(riskAssessment);
        }

        public bool CanUserAccess(long personelRiskAssessmentId,  long companyId, Guid currentUserId)
        {
            var riskAssessment = _personalRiskAssessmentRepository.GetByIdAndCompanyId(personelRiskAssessmentId, companyId, currentUserId);

            return riskAssessment.CanUserAccess(currentUserId);
        }
    }
}
