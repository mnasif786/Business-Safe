using System;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.GeneralRiskAssessments
{
    public class GeneralRiskAssessmentAttachmentService : IGeneralRiskAssessmentAttachmentService
    {
        private readonly IGeneralRiskAssessmentRepository _riskAssessmentRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IPeopleAtRiskRepository _peopleAtRiskRepository;
        private readonly IPeninsulaLog _log;

        public GeneralRiskAssessmentAttachmentService(IGeneralRiskAssessmentRepository riskAssessmentRepository, IUserForAuditingRepository userForAuditingRepository, IPeopleAtRiskRepository peopleAtRiskRepository, IPeninsulaLog log) 
            
        {
            _riskAssessmentRepository = riskAssessmentRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _peopleAtRiskRepository = peopleAtRiskRepository;
            _log = log;
        }

        public void AttachPeopleAtRiskToRiskAssessment(AttachPeopleAtRiskToRiskAssessmentRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);
                var peopleAtRisk = _peopleAtRiskRepository.GetByIds(request.PeopleAtRiskIds);

                riskAssessment.AttachPeopleAtRiskToRiskAssessment(peopleAtRisk, user);

                _riskAssessmentRepository.SaveOrUpdate(riskAssessment);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

       
    }
}