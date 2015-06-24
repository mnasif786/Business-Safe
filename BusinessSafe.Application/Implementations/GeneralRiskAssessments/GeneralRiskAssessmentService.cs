using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Validators;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using FluentValidation;

namespace BusinessSafe.Application.Implementations.GeneralRiskAssessments
{
    public class GeneralRiskAssessmentService : IGeneralRiskAssessmentService
    {
        private readonly IGeneralRiskAssessmentRepository _generalRiskAssessmentRepository;
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IPeninsulaLog _log;
        private readonly ISiteRepository _siteRepository;
        private readonly IRiskAssessorRepository _riskAssessorRepository;

        public GeneralRiskAssessmentService(
            IGeneralRiskAssessmentRepository generalRiskAssessmentRepository, 
            IRiskAssessmentRepository riskAssessmentRepository, 
            IUserForAuditingRepository userForAuditingRepository, 
            IEmployeeRepository employeeRepository, 
            IPeninsulaLog log, 
            ISiteRepository siteRepository,
            IRiskAssessorRepository riskAssessorRepository)
        {
            _generalRiskAssessmentRepository = generalRiskAssessmentRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _log = log;
            _siteRepository = siteRepository;
            _riskAssessorRepository = riskAssessorRepository;
            _riskAssessmentRepository = riskAssessmentRepository;
        }

        public long CreateRiskAssessment(CreateRiskAssessmentRequest request)
        {
            _log.Add(request);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);

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

            new CreateRiskAssessmentValidator<GeneralRiskAssessment>(_riskAssessmentRepository).ValidateAndThrow(request);

            var riskAssessment = GeneralRiskAssessment.Create(request.Title,
                                                       request.Reference,
                                                       request.CompanyId,
                                                       user,
                                                       request.Location,
                                                       request.TaskProcessDescription,
                                                       site
                                                       ,request.AssessmentDate,riskAssessor);

            _generalRiskAssessmentRepository.SaveOrUpdate(riskAssessment);

            return riskAssessment.Id;

        }

        public virtual IEnumerable<GeneralRiskAssessmentDto> Search(SearchRiskAssessmentsRequest request)
        {
            var riskAssessments = _generalRiskAssessmentRepository.Search(
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
            var generalRiskAssessmentDto = riskAssessmentDtos.Select(riskAssessmentDto => riskAssessmentDto as GeneralRiskAssessmentDto);
            return generalRiskAssessmentDto;
        }

        public virtual int Count(SearchRiskAssessmentsRequest request)
        {
            var count = _generalRiskAssessmentRepository.Count(
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

            return count;
        }

        public GeneralRiskAssessmentDto GetRiskAssessmentWithHazards(long riskAssessmentId, long companyId)
        {
            var riskAssessment = LoadRiskAssessment(riskAssessmentId, companyId);
            return GeneralRiskAssessmentDto.CreateFromWithHazards(riskAssessment);

        }

        public GeneralRiskAssessmentDto GetRiskAssessmentWithHazardsAndPeopleAtRisk(long riskAssessmentId, long companyId)
        {
            _log.Add(new object[] { riskAssessmentId, companyId });

            var riskAssessment = LoadRiskAssessment(riskAssessmentId, companyId);
            return GeneralRiskAssessmentDto.CreateFromWithHazardsAndPeopleAtRisk(riskAssessment);

        }

        public GeneralRiskAssessmentDto GetRiskAssessment(long riskAssessmentId, long companyId)
        {
            _log.Add(new object[] { riskAssessmentId, companyId });

            var riskAssessment = LoadRiskAssessment(riskAssessmentId, companyId);
            return GeneralRiskAssessmentDto.CreateFrom(riskAssessment);

        }

        private GeneralRiskAssessment LoadRiskAssessment(long riskAssessmentId, long companyId)
        {
            var riskAssessment = _generalRiskAssessmentRepository.GetByIdAndCompanyId(riskAssessmentId, companyId);
            return riskAssessment;
        }

        public IEnumerable<string> GetPeopleAtRiskDisplayWarningMessages(long riskAssessmentId, long companyId)
        {
            _log.Add(new object[] { riskAssessmentId, companyId });

            var riskAssessment = _generalRiskAssessmentRepository.GetByIdAndCompanyId(riskAssessmentId, companyId);
            return PeopleAtRiskWarningDisplayMessages.GetMessages(riskAssessment.PeopleAtRisk.Select(x => x.PeopleAtRisk), companyId);

        }

        public long CopyRiskAssessment(CopyRiskAssessmentRequest copyRiskAssessmentRequest)
        {
            new CopyRiskAssessmentRequestValidator<GeneralRiskAssessment>(_riskAssessmentRepository).ValidateAndThrow(copyRiskAssessmentRequest);

            var riskAssessment = _generalRiskAssessmentRepository.GetByIdAndCompanyId(copyRiskAssessmentRequest.RiskAssessmentToCopyId, copyRiskAssessmentRequest.CompanyId);
            var user = _userForAuditingRepository.GetByIdAndCompanyId(copyRiskAssessmentRequest.UserId, copyRiskAssessmentRequest.CompanyId);

            var copiedRiskAssessment = riskAssessment.Copy(copyRiskAssessmentRequest.Title, copyRiskAssessmentRequest.Reference, user) as GeneralRiskAssessment;
            _generalRiskAssessmentRepository.SaveOrUpdate(copiedRiskAssessment);
            return copiedRiskAssessment.Id;

        }

        public void UpdateRiskAssessmentSummary(SaveRiskAssessmentSummaryRequest request)
        {
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessor = request.RiskAssessorId.HasValue ? _riskAssessorRepository.GetByIdAndCompanyId(request.RiskAssessorId.Value, request.CompanyId) : null;
            var site = request.SiteId.HasValue ? _siteRepository.GetByIdAndCompanyId(request.SiteId.Value, request.CompanyId) : null;
            var riskAssessment = _generalRiskAssessmentRepository.GetByIdAndCompanyId(request.Id, request.CompanyId);
            new UpdateRiskAssessmentValidator<GeneralRiskAssessment>(_riskAssessmentRepository).ValidateAndThrow(request);
            riskAssessment.UpdateSummary(request.Title, request.Reference, request.AssessmentDate, riskAssessor, site, user);
            _generalRiskAssessmentRepository.SaveOrUpdate(riskAssessment);
        }

        /// <summary>
        /// this is used by the API
        /// </summary>
        /// <param name="request"></param>
        public void UpdateRiskAssessment(SaveGeneralRiskAssessmentRequest request)
        {
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessor = request.RiskAssessorId.HasValue ? _riskAssessorRepository.GetByIdAndCompanyId(request.RiskAssessorId.Value, request.CompanyId) : null;
            var site = request.SiteId.HasValue ? _siteRepository.GetByIdAndCompanyId(request.SiteId.Value, request.CompanyId) : null;
            var riskAssessment = _generalRiskAssessmentRepository.GetByIdAndCompanyId(request.Id, request.CompanyId);
            new UpdateRiskAssessmentValidator<GeneralRiskAssessment>(_riskAssessmentRepository).ValidateAndThrow(request);
            riskAssessment.UpdateSummary(request.Title, request.Reference, request.AssessmentDate, riskAssessor, site, user);
            riskAssessment.UpdatePremisesInformation(request.Location,request.TaskProcessDescription,user);
            _generalRiskAssessmentRepository.SaveOrUpdate(riskAssessment);
        }

        public void CopyForMultipleSites(CopyRiskAssessmentForMultipleSitesRequest request)
        {
            _log.Add(request);
            var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentToCopyId, request.CompanyId);
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var sites = _siteRepository.GetByIds(request.SiteIds);
            var copiedRiskAssessments = riskAssessment.CopyForMultipleSites(request.Title, sites, user);
            copiedRiskAssessments.ForEach(_riskAssessmentRepository.SaveOrUpdate);
        }
    }
}