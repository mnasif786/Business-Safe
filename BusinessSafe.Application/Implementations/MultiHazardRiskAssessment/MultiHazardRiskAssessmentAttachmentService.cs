using System;
using System.Linq;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Linq;

namespace BusinessSafe.Application.Implementations.MultiHazardRiskAssessment
{
    public class MultiHazardRiskAssessmentAttachmentService : IMultiHazardRiskAssessmentAttachmentService
    {
        private readonly IHazardRepository _hazardsRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IMultiHazardRiskAssessmentRepository _multiHazardRiskAssessmentRepository;
        private readonly IPeninsulaLog _log;

        public MultiHazardRiskAssessmentAttachmentService(
            IMultiHazardRiskAssessmentRepository multiHazardRiskAssessmentRepository,
            IHazardRepository hazardsRepository, IUserForAuditingRepository userForAuditingRepository, IPeninsulaLog log)
        {
            _multiHazardRiskAssessmentRepository = multiHazardRiskAssessmentRepository;
            _hazardsRepository = hazardsRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _log = log;
        }

        public void AttachHazardsToRiskAssessment(AttachHazardsToRiskAssessmentRequest request)
        {
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var riskAssessment = _multiHazardRiskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);
            var hazards = _hazardsRepository.GetByIds(request.Hazards.Select(x=>x.Id).ToList());

            riskAssessment.AttachHazardsToRiskAssessment(hazards, user);


            request.Hazards.ForEach(hazardOrder =>
            {
                var orderNumber = hazardOrder.OrderNumber;
                var hazard = hazards.First(x => x.Id == hazardOrder.Id);
                riskAssessment.UpdateHazardOrder(hazard, orderNumber, user);
            });



            _multiHazardRiskAssessmentRepository.SaveOrUpdate(riskAssessment);
        }
    }
}