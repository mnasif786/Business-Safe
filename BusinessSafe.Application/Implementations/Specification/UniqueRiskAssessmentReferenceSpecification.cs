using BusinessSafe.Application.Request;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Specification
{
    public class UniqueRiskAssessmentReferenceSpecification<T> : ISpecification<ICreateRiskAssessmentRequest>, ISpecification<ISaveRiskAssessmentSummaryRequest> where T :class
    {
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;

        public UniqueRiskAssessmentReferenceSpecification(IRiskAssessmentRepository riskAssessmentRepository)
        {
            _riskAssessmentRepository = riskAssessmentRepository;
        }

        public bool IsSatisfiedBy(ICreateRiskAssessmentRequest request)
        {
            var reference = request.Reference;
            var companyId = request.CompanyId;
            const int riskAssessmentId = 0;

            return IsSatisfied(riskAssessmentId, companyId, reference);
        }

        public bool IsSatisfiedBy(ISaveRiskAssessmentSummaryRequest request)
        {
            var reference = request.Reference;
            var companyId = request.CompanyId;
            var riskAssessmentId = request.Id;

            return IsSatisfied(riskAssessmentId, companyId, reference);
        }

        private bool IsSatisfied(long riskAssessmentId, long companyId, string reference)
        {
            if (!string.IsNullOrEmpty(reference))
            {
                return _riskAssessmentRepository.DoesAssessmentExistWithTheSameReference<T>(companyId, reference, riskAssessmentId) == false;
            }
            return true;
        }
    }
}
