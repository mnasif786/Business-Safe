using BusinessSafe.Application.Request;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Specification
{
    public class UniqueRiskAssessmentTitleSpecification<T> : ISpecification<ICreateRiskAssessmentRequest>, ISpecification<ISaveRiskAssessmentSummaryRequest> where T :class
    {
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;

        public UniqueRiskAssessmentTitleSpecification(IRiskAssessmentRepository riskAssessmentRepository)
        {
            _riskAssessmentRepository = riskAssessmentRepository;
        }

        public bool IsSatisfiedBy(ICreateRiskAssessmentRequest request)
        {
            return _riskAssessmentRepository.DoesAssessmentExistWithTheSameTitle<T>(request.CompanyId, request.Title, 0) == false;
        }

        public bool IsSatisfiedBy(ISaveRiskAssessmentSummaryRequest request)
        {
            return _riskAssessmentRepository.DoesAssessmentExistWithTheSameTitle<T>(request.CompanyId, request.Title, request.Id) == false;
        }
    }
}
