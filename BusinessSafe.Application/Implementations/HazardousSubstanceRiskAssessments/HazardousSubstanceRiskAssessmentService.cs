using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Validators;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using FluentValidation;

namespace BusinessSafe.Application.Implementations.HazardousSubstanceRiskAssessments
{
    public class HazardousSubstanceRiskAssessmentService : IHazardousSubstanceRiskAssessmentService
    {
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;
        private readonly IHazardousSubstancesRepository _hazardousSubstancesRepository;
        private readonly IHazardousSubstanceRiskAssessmentRepository _hazardousSubstanceRiskAssessmentRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IPeninsulaLog _log;
        private readonly IControlSystemRepository _controlSystemRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IRiskAssessorRepository _riskAssessorRepository;

        public HazardousSubstanceRiskAssessmentService(
            IHazardousSubstanceRiskAssessmentRepository hazardousSubstanceRiskAssessmentRepository,
            IUserForAuditingRepository userForAuditingRepository,
            IHazardousSubstancesRepository hazardousSubstancesRepository,
            IPeninsulaLog log,
            IRiskAssessmentRepository riskAssessmentRepository,
            IControlSystemRepository controlSystemRepository, 
            ISiteRepository siteRepository,
            IRiskAssessorRepository riskAssessorRepository)
        {
            _hazardousSubstanceRiskAssessmentRepository = hazardousSubstanceRiskAssessmentRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _log = log;
            _hazardousSubstancesRepository = hazardousSubstancesRepository;
            _riskAssessmentRepository = riskAssessmentRepository;
            _controlSystemRepository = controlSystemRepository;
            _siteRepository = siteRepository;
            _riskAssessorRepository = riskAssessorRepository;
        }

        public long CreateRiskAssessment(SaveHazardousSubstanceRiskAssessmentRequest request)
        {
            _log.Add(request);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var hazardousSubstance = _hazardousSubstancesRepository.GetByIdAndCompanyId(request.HazardousSubstanceId, request.CompanyId);

            new CreateRiskAssessmentValidator<HazardousSubstanceRiskAssessment>(_riskAssessmentRepository).ValidateAndThrow(request);

            var hazardousSubstanceAssessment = HazardousSubstanceRiskAssessment.Create(request.Title,
                                                       request.Reference,
                                                       request.CompanyId,
                                                       user,
                                                       hazardousSubstance);


            _hazardousSubstanceRiskAssessmentRepository.SaveOrUpdate(hazardousSubstanceAssessment);

            return hazardousSubstanceAssessment.Id;
        }


        public HazardousSubstanceRiskAssessmentDto GetRiskAssessment(long hazardousSubstanceAssessmentId, long companyId)
        {
            _log.Add(new object[] { hazardousSubstanceAssessmentId, companyId });

            var riskAssessment = _hazardousSubstanceRiskAssessmentRepository.GetByIdAndCompanyId(hazardousSubstanceAssessmentId, companyId);
            return new HazardousSubstanceRiskAssessmentDtoMapper().Map(riskAssessment);
        }

        public void UpdateRiskAssessmentSummary(SaveHazardousSubstanceRiskAssessmentRequest request)
        {
            var hazardousSubstanceAssessment = _hazardousSubstanceRiskAssessmentRepository.GetByIdAndCompanyId(request.Id, request.CompanyId);
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessor = request.RiskAssessorId.HasValue ? _riskAssessorRepository.GetByIdAndCompanyId(request.RiskAssessorId.Value, request.CompanyId) : null;
            var site = request.SiteId.HasValue ? _siteRepository.GetByIdAndCompanyId(request.SiteId.Value, request.CompanyId) : null;
            var hazardousSubstance = _hazardousSubstancesRepository.GetByIdAndCompanyId(request.HazardousSubstanceId, request.CompanyId);
            new UpdateRiskAssessmentValidator<HazardousSubstanceRiskAssessment>(_riskAssessmentRepository).ValidateAndThrow(request);

            hazardousSubstanceAssessment.UpdateSummary(
                request.Title,
                request.Reference,
                request.AssessmentDate,
                riskAssessor,
                hazardousSubstance,
                site, user);

            _hazardousSubstanceRiskAssessmentRepository.SaveOrUpdate(hazardousSubstanceAssessment);

        }

        public void UpdateRiskAssessmentDescription(SaveHazardousSubstanceRiskAssessmentRequest request)
        {
            _log.Add(request);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessment = _hazardousSubstanceRiskAssessmentRepository.GetByIdAndCompanyId(request.Id, request.CompanyId);
            riskAssessment.Update(
                user,
                request.IsInhalationRouteOfEntry,
                request.IsIngestionRouteOfEntry,
                request.IsAbsorptionRouteOfEntry,
                request.WorkspaceExposureLimits);

            _hazardousSubstanceRiskAssessmentRepository.SaveOrUpdate(riskAssessment);

        }
        
        public virtual IEnumerable<HazardousSubstanceRiskAssessmentDto> Search(SearchHazardousSubstanceRiskAssessmentsRequest request)
        {
            var riskAssessments = _hazardousSubstanceRiskAssessmentRepository.Search(
                request.Title,
                request.CompanyId,
                request.CreatedFrom,
                request.CreatedTo,
                request.HazardousSubstanceId,
                request.AllowedSiteIds,
                request.ShowDeleted,
                request.ShowArchived,
                request.CurrentUserId,
                request.SiteId,
                request.SiteGroupId,
                request.Page,
                request.PageSize,
                request.OrderBy,
                request.OrderByDirection
                );

            var riskAssessmentDtos = new HazardousSubstanceRiskAssessmentDtoMapper().Map(riskAssessments);
            return riskAssessmentDtos;

            //TODO: HazardousSubstanceRiskAssessmentDto must be refactored to inherit from RiskAssessmentDto, and then mapped using RiskAssessmentDtoMapper,
            //The same way as the other risk assessments, until this is done, HSRA will possibly show performance problems.

            //var riskAssessmentDtos = new RiskAssessmentDtoMapper().MapWithSiteAndRiskAssessor(riskAssessments);
            //var hazardousSubstanceRiskAssessmentDtos = riskAssessmentDtos.Select(riskAssessmentDto => riskAssessmentDto as HazardousSubstanceRiskAssessmentDto);
            //return hazardousSubstanceRiskAssessmentDtos;
        }

        public virtual int Count(SearchHazardousSubstanceRiskAssessmentsRequest request)
        {
            var count = _hazardousSubstanceRiskAssessmentRepository.Count(
                request.Title,
                request.CompanyId,
                request.CreatedFrom,
                request.CreatedTo,
                request.HazardousSubstanceId,
                request.AllowedSiteIds,
                request.ShowDeleted,
                request.ShowArchived,
                request.CurrentUserId,
                request.SiteId,
                request.SiteGroupId
                );

            return count;
        }

        public void UpdateHazardousSubstanceRiskAssessmentAssessmentDetails(
            UpdateHazardousSubstanceRiskAssessmentAssessmentDetailsRequest request)
        {
            _log.Add();

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.CurrentUserId, request.CompanyId);
            var riskAssessment = _hazardousSubstanceRiskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);

            riskAssessment.UpdateAssessmentDetails(
                request.Quantity,
                request.MatterState,
                request.DustinessOrVolatility,
                request.HealthSurveillanceRequired,
                user);

            var controlSystem = request.ControlSystemId > 0 ? _controlSystemRepository.LoadById(request.ControlSystemId) : null;
            riskAssessment.SetLastRecommendedControlSystem(controlSystem, user);

            _hazardousSubstanceRiskAssessmentRepository.SaveOrUpdate(riskAssessment);
        }

        public long AddControlMeasureToRiskAssessment(AddControlMeasureRequest request)
        {
            _log.Add(request);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessment = _hazardousSubstanceRiskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);

            var riskAssessmentControlMeasure = HazardousSubstanceRiskAssessmentControlMeasure.Create(request.ControlMeasure, riskAssessment, user);

            riskAssessment.AddControlMeasure(riskAssessmentControlMeasure, user);

            _hazardousSubstanceRiskAssessmentRepository.SaveOrUpdate(riskAssessment);
            _hazardousSubstanceRiskAssessmentRepository.Flush();

            return riskAssessmentControlMeasure.Id;

        }

        public void UpdateControlMeasure(UpdateControlMeasureRequest request)
        {
            _log.Add(request);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessment = _hazardousSubstanceRiskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);

            riskAssessment.UpdateControlMeasure(request.ControlMeasureId, request.ControlMeasure, user);

            _hazardousSubstanceRiskAssessmentRepository.SaveOrUpdate(riskAssessment);

        }

        public void RemoveControlMeasureFromRiskAssessment(RemoveControlMeasureRequest request)
        {
            _log.Add(request);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessment = _hazardousSubstanceRiskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);

            riskAssessment.RemoveControlMeasure(request.ControlMeasureId, user);

            _hazardousSubstanceRiskAssessmentRepository.SaveOrUpdate(riskAssessment);

        }

        public long CopyRiskAssessment(CopyRiskAssessmentRequest copyRiskAssessmentRequest)
        {
            new CopyRiskAssessmentRequestValidator<HazardousSubstanceRiskAssessment>(_riskAssessmentRepository).ValidateAndThrow(copyRiskAssessmentRequest);

            var riskAssessment = _hazardousSubstanceRiskAssessmentRepository.GetByIdAndCompanyId(copyRiskAssessmentRequest.RiskAssessmentToCopyId, copyRiskAssessmentRequest.CompanyId);
            var user = _userForAuditingRepository.GetByIdAndCompanyId(copyRiskAssessmentRequest.UserId, copyRiskAssessmentRequest.CompanyId);

            var copiedRiskAssessment = riskAssessment.Copy(copyRiskAssessmentRequest.Title, copyRiskAssessmentRequest.Reference, user)as HazardousSubstanceRiskAssessment;
            _hazardousSubstanceRiskAssessmentRepository.SaveOrUpdate(copiedRiskAssessment);
            return copiedRiskAssessment.Id;

        }

        public void SaveLastRecommendedControlSystem(SaveLastRecommendedControlSystemRequest request)
        {
            var riskAssessment = _hazardousSubstanceRiskAssessmentRepository.GetByIdAndCompanyId(request.Id, request.CompanyId);
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var controlSystem = _controlSystemRepository.LoadById(request.ControlSystemId);

            riskAssessment.SetLastRecommendedControlSystem(controlSystem, user);

            _hazardousSubstanceRiskAssessmentRepository.SaveOrUpdate(riskAssessment);
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