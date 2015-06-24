using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Domain.Specification
{
    public class UniqueRiskAssessmentTitleSpecification: ISpecification<RiskAssessment>
    {
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;

        public UniqueRiskAssessmentTitleSpecification(IRiskAssessmentRepository riskAssessmentRepository)
        {
            _riskAssessmentRepository = riskAssessmentRepository;
        }

        public bool IsSatisfiedBy(RiskAssessment entity)
        {
            return _riskAssessmentRepository.DoesAssessmentExistWithTheSameTitle(entity.CompanyId, entity.Title, entity.Id) == false;
        }
    }
}
