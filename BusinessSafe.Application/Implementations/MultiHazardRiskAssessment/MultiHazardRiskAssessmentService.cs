using System;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.MultiHazardRiskAssessment
{
    public class MultiHazardRiskAssessmentService : IMultiHazardRiskAssessmentService
    {
        private readonly IPeninsulaLog _log;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IMultiHazardRiskAssessmentRepository _multiHazardRiskAssessmentRepository;
        private readonly IMultiHazardRiskAssessmentHazardRepository _multiHazardRiskAssessmentHazardRepository;


        public MultiHazardRiskAssessmentService(
            IMultiHazardRiskAssessmentRepository multiHazardRiskAssessmentRepository, 
            IMultiHazardRiskAssessmentHazardRepository multiHazardRiskAssessmentHazardRepository,
            IUserForAuditingRepository userForAuditingRepository, 
            IPeninsulaLog log)
        {
            _log = log;
            _userForAuditingRepository = userForAuditingRepository;
            _multiHazardRiskAssessmentRepository = multiHazardRiskAssessmentRepository;
            _multiHazardRiskAssessmentHazardRepository = multiHazardRiskAssessmentHazardRepository;
        }


        public void UpdateRiskAssessmentPremisesInformation(SaveRiskAssessmentPremisesInformationRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _multiHazardRiskAssessmentRepository.GetByIdAndCompanyId(request.Id, request.CompanyId);
                
                riskAssessment.UpdatePremisesInformation(
                    request.LocationAreaDepartment,
                    request.TaskProcessDescription,
                    user);

                _multiHazardRiskAssessmentRepository.SaveOrUpdate(riskAssessment);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }


        }

       

        public bool CanRemoveRiskAssessmentHazard(long companyId, long riskAssessmentId, long hazardId)
        {
            _log.Add(new object[] { companyId, riskAssessmentId, hazardId });

            try
            {
                var riskAssessmentHazard =
                    _multiHazardRiskAssessmentHazardRepository.GetByRiskAssessmentIdAndHazardIdAndCompanyId(
                        riskAssessmentId, hazardId, companyId);

                if (riskAssessmentHazard == null)
                {
                    return true;
                }

                return riskAssessmentHazard.CanDeleteHazard();
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }

        }
    }
}