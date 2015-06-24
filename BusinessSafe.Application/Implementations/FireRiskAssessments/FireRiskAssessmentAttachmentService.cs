using System;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.FireRiskAssessments
{
    public class FireRiskAssessmentAttachmentService : IFireRiskAssessmentAttachmentService
    {
        private readonly IPeopleAtRiskRepository _peopleAtRiskRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;
        private readonly IFireSafetyControlMeasureRepository _fireSafetyControlMeasureRepository;
        private readonly ISourceOfIgnitionRepository _sourceOfIgnitionRepository;
        private readonly ISourceOfFuelRepository _sourceOfFuelRepository;
        private readonly IPeninsulaLog _log;

        public FireRiskAssessmentAttachmentService(IFireRiskAssessmentRepository fireRiskAssessmentRepository, IPeopleAtRiskRepository peopleAtRiskRepository, IUserForAuditingRepository userForAuditingRepository, IFireSafetyControlMeasureRepository fireSafetyControlMeasureRepository, ISourceOfIgnitionRepository sourceOfIgnitionRepository, ISourceOfFuelRepository sourceOfFuelRepository, IPeninsulaLog log)
        {
            _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
            _peopleAtRiskRepository = peopleAtRiskRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _log = log;
            _fireSafetyControlMeasureRepository = fireSafetyControlMeasureRepository;
            _sourceOfIgnitionRepository = sourceOfIgnitionRepository;
            _sourceOfFuelRepository = sourceOfFuelRepository;
        }

        public void AttachPeopleAtRiskToRiskAssessment(AttachPeopleAtRiskToRiskAssessmentRequest request)
        {
            _log.Add(request);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessment = _fireRiskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);

            var peopleAtRisk = _peopleAtRiskRepository.GetByIds(request.PeopleAtRiskIds);

            riskAssessment.AttachPeopleAtRiskToRiskAssessment(peopleAtRisk, user);

            _fireRiskAssessmentRepository.SaveOrUpdate(riskAssessment);

        }

        public void AttachFireSafetyControlMeasuresToRiskAssessment(AttachFireSafetyControlMeasuresToRiskAssessmentRequest request)
        {
            _log.Add(request);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessment = _fireRiskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);

            var fireSafetyControlMeasures = _fireSafetyControlMeasureRepository.GetByIds(request.FireSafetyControlMeasureIds);

            riskAssessment.AttachFireSafetyControlMeasuresToRiskAssessment(fireSafetyControlMeasures, user);

            _fireRiskAssessmentRepository.SaveOrUpdate(riskAssessment);

        }

        public void AttachSourcesOfIgnitionToRiskAssessment(AttachSourceOfIgnitionToRiskAssessmentRequest request)
        {
            _log.Add(request);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessment = _fireRiskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);

            var sourceOfIgnitions = _sourceOfIgnitionRepository.GetByIds(request.SourceOfIgnitionIds);

            riskAssessment.AttachSourceOfIgnitionsToRiskAssessment(sourceOfIgnitions, user);

            _fireRiskAssessmentRepository.SaveOrUpdate(riskAssessment);

        }

        public void AttachSourcesOfFuelToRiskAssessment(AttachSourceOfFuelToRiskAssessmentRequest request)
        {
            _log.Add(request);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessment = _fireRiskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);

            var sourceOfFuels = _sourceOfFuelRepository.GetByIds(request.SourceOfFuelIds);

            riskAssessment.AttachSourceOfFuelsToRiskAssessment(sourceOfFuels, user);

            _fireRiskAssessmentRepository.SaveOrUpdate(riskAssessment);

        }
    }
}