using System;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;

namespace BusinessSafe.Application.Implementations.RiskAssessments
{
    public class RiskAssessmentHazardService : IRiskAssessmentHazardService
    {
        private readonly IMultiHazardRiskAssessmentHazardRepository _riskAssessmentHazardRepository;
        private readonly IMultiHazardRiskAssessmentRepository _riskAssessmentRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IPeninsulaLog _log;

        public RiskAssessmentHazardService(
            IMultiHazardRiskAssessmentHazardRepository riskAssessmentHazardRepository,
            IMultiHazardRiskAssessmentRepository riskAssessmentRepository,
            IUserForAuditingRepository userForAuditingRepository, 
            IPeninsulaLog log)
        {
            _riskAssessmentHazardRepository = riskAssessmentHazardRepository;
            _riskAssessmentRepository = riskAssessmentRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _log = log;
        }

        public void UpdateRiskAssessmentHazardDescription(UpdateRiskAssessmentHazardDescriptionRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);
                var riskAssessmentHazard = riskAssessment.FindRiskAssessmentHazard(request.RiskAssessmentHazardId);

                riskAssessmentHazard.UpdateDescription(request.Description, user);
                _riskAssessmentRepository.SaveOrUpdate(riskAssessment);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void UpdateRiskAssessmentHazardTitle(UpdateRiskAssessmentHazardTitleRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);
                var riskAssessmentHazard = riskAssessment.FindRiskAssessmentHazard(request.RiskAssessmentHazardId);
                riskAssessmentHazard.UpdateTitle(request.Title, user);
                _riskAssessmentRepository.SaveOrUpdate(riskAssessment);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public long AddControlMeasureToRiskAssessmentHazard(AddControlMeasureRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);
                var riskAssessmentHazard = riskAssessment.FindRiskAssessmentHazard(request.RiskAssessmentHazardId);
                var riskAssessmentControlMeasure = MultiHazardRiskAssessmentControlMeasure.Create(request.ControlMeasure, riskAssessmentHazard, user);

                riskAssessmentHazard.AddControlMeasure(riskAssessmentControlMeasure, user);
                _riskAssessmentRepository.SaveOrUpdate(riskAssessment);
                _riskAssessmentRepository.Flush();

                return riskAssessmentControlMeasure.Id;
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void RemoveControlMeasureFromRiskAssessmentHazard(RemoveControlMeasureRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);
                var riskAssessmentHazard = riskAssessment.FindRiskAssessmentHazard(request.RiskAssessmentHazardId);

                riskAssessmentHazard.RemoveControlMeasure(request.ControlMeasureId, user);
                _riskAssessmentRepository.SaveOrUpdate(riskAssessment);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
            

        }

        public void UpdateControlMeasureForRiskAssessmentHazard(UpdateControlMeasureRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);

                riskAssessment.UpdateControlMeasureForRiskAssessmentHazard(request.RiskAssessmentHazardId, request.ControlMeasureId, request.ControlMeasure, user);
                _riskAssessmentRepository.SaveOrUpdate(riskAssessment);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public RiskAssessmentHazardDto GetByIdAndCompanyId(long id, long companyId)
        {
            _log.Add(new object[] { id, companyId });

            try
            {
                var riskAssessmentHazard = _riskAssessmentHazardRepository.GetByIdAndCompanyId(id, companyId);
                return new RiskAssessmentHazardDtoMapper().Map(riskAssessmentHazard);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }
    }
}