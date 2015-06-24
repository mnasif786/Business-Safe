using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Company
{
    public class RiskAssessorService : IRiskAssessorService
    {
        private readonly IRiskAssessorRepository _riskAssessorRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IPeninsulaLog _log;
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;
        private readonly ISiteStructureElementRepository _siteStructureElementRepository;

        public RiskAssessorService(
            IRiskAssessorRepository riskAssessorRepository,
            IEmployeeRepository employeeRepository,
            ISiteRepository siteRepository,
            IUserForAuditingRepository userForAuditingRepository,
            IRiskAssessmentRepository riskAssessmentRepository,
            ISiteStructureElementRepository siteStructureElementRepository,
            IPeninsulaLog log)
        {
            _riskAssessorRepository = riskAssessorRepository;
            _employeeRepository = employeeRepository;
            _siteRepository = siteRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _riskAssessmentRepository = riskAssessmentRepository;
            _log = log;
            _siteStructureElementRepository = siteStructureElementRepository;
        }

        public RiskAssessorDto GetByIdAndCompanyId(long id, long companyId)
        {
            var riskAssessor = _riskAssessorRepository.GetByIdAndCompanyId(id, companyId);
            return new RiskAssessorDtoMapper().MapWithEmployeeAndSite(riskAssessor);
        }

        public IEnumerable<RiskAssessorDto> GetByCompanyId(long companyId)
        {
            var riskAssessors = _riskAssessorRepository.GetByCompanyId(companyId);
            return new RiskAssessorDtoMapper().MapWithEmployeeAndSite(riskAssessors);
        }

        public long Create(CreateEditRiskAssessorRequest request)
        {
            _log.Add(request);

            var employee = _employeeRepository.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId);
            employee.UpdateRiskAssessorDetails(true,
                request.DoNotSendTaskOverdueNotifications,
                request.DoNotSendTaskCompletedNotifications,
                request.DoNotSendReviewDueNotification,
                GetUser(request.CreatingUserId, request.CompanyId));

            employee.RiskAssessor.HasAccessToAllSites = request.HasAccessToAllSites;
            employee.RiskAssessor.Site = GetSiteStructureElement(request.SiteId, request.CompanyId);

            _siteStructureElementRepository.Initialize(employee.RiskAssessor.Site);

            _riskAssessorRepository.Save(employee.RiskAssessor);
            return employee.RiskAssessor.Id;


        }

        public void Update(CreateEditRiskAssessorRequest request)
        {
            _log.Add(request);

            var riskAssessor = _riskAssessorRepository.GetByIdAndCompanyId(request.RiskAssessorId, request.CompanyId);

            riskAssessor.Update(
                GetSiteStructureElement(request.SiteId, request.CompanyId),
                request.HasAccessToAllSites,
                request.DoNotSendReviewDueNotification,
                request.DoNotSendTaskOverdueNotifications,
                request.DoNotSendReviewDueNotification,
                GetUser(request.CreatingUserId, request.CompanyId)
                );

            _riskAssessorRepository.Save(riskAssessor);

        }

        public IEnumerable<RiskAssessorDto> Search(SearchRiskAssessorRequest request)
        {
            var parentSitesIds = new long[]{};
            if (request.SiteId > 0)
            {
                var site = _siteStructureElementRepository.GetByIdAndCompanyId(request.SiteId, request.CompanyId);
                parentSitesIds = site.GetThisAndAllAncestors().Select(x => x.Id).ToArray();
            }

            var riskAssessors = _riskAssessorRepository.Search(
                request.SearchTerm,
                parentSitesIds,
                request.CompanyId,
                request.MaximumResults,
                request.IncludeDeleted,
                request.ExcludeActive,
                false
            );

            return new RiskAssessorDtoMapper().MapWithEmployeeAndSite(riskAssessors);
        }

        public void MarkDeleted(MarkRiskAssessorAsDeletedAndUndeletedRequest request)
        {
            _log.Add(request);

            bool hasOutstandingRiskAssessments = _riskAssessmentRepository.DoesAssessmentExistForRiskAssessor(request.RiskAssessorId, request.CompanyId);
            if (hasOutstandingRiskAssessments)
            {
                throw new TryingToDeleteRiskAssessorWithOutstandingRiskAssessmentsException(request.RiskAssessorId);
            }

            var riskAssessor = _riskAssessorRepository.GetByIdAndCompanyId(request.RiskAssessorId, request.CompanyId);
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            riskAssessor.MarkForDelete(user);
            _riskAssessorRepository.SaveOrUpdate(riskAssessor);
        }

        public void MarkUndeleted(MarkRiskAssessorAsDeletedAndUndeletedRequest request)
        {
            _log.Add(request);

            var riskAssessor = _riskAssessorRepository.GetByIdAndCompanyIdIncludingDeleted(request.RiskAssessorId, request.CompanyId);
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            riskAssessor.ReinstateFromDelete(user);
            _riskAssessorRepository.SaveOrUpdate(riskAssessor);
        }

        public bool HasRiskAssessorGotOutstandingRiskAssessments(long riskAssessorId, long companyId)
        {
            return _riskAssessmentRepository.DoesAssessmentExistForRiskAssessor(riskAssessorId, companyId);
        }

        private Site GetSite(long? siteId, long companyId)
        {
            return siteId.HasValue
                       ? _siteRepository.GetByIdAndCompanyId(siteId.Value, companyId)
                       : null;
        }

        private SiteStructureElement GetSiteStructureElement(long? siteId, long companyId)
        {
            return siteId.HasValue
                       ? _siteStructureElementRepository.GetByIdAndCompanyId(siteId.Value, companyId)
                       : null;
        }

        private UserForAuditing GetUser(Guid userId, long companyId)
        {
            return _userForAuditingRepository
                .GetByIdAndCompanyId(userId, companyId);
        }
    }
}
