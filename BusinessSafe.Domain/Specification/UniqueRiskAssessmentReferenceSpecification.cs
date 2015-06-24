using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Domain.Specification
{
    public class UniqueRiskAssessmentReferenceSpecification: ISpecification<RiskAssessment>
    {
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;

        public UniqueRiskAssessmentReferenceSpecification(IRiskAssessmentRepository riskAssessmentRepository)
        {
            _riskAssessmentRepository = riskAssessmentRepository;
        }

        public bool IsSatisfiedBy(RiskAssessment entity)
        {
            if (!string.IsNullOrEmpty(entity.Reference))
            {
                return _riskAssessmentRepository.DoesAssessmentExistWithTheSameReference(entity.CompanyId, entity.Reference, entity.Id) == false;
            }
            else
            {
                return true;
            }

        }
    }
}
