using BusinessSafe.Application.Implementations.Specification;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.RepositoryContracts;
using FluentValidation;

namespace BusinessSafe.Application.Implementations.RiskAssessments
{
    public class UpdateRiskAssessmentValidator <T>: AbstractValidator<ISaveRiskAssessmentSummaryRequest> where T:class
    {
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;
        public UpdateRiskAssessmentValidator(IRiskAssessmentRepository riskAssessmentRepository)
        {
            _riskAssessmentRepository = riskAssessmentRepository;

            RuleFor(riskAssessment => riskAssessment)
                .Must(x => new UniqueRiskAssessmentReferenceSpecification<T>(_riskAssessmentRepository).IsSatisfiedBy(x))
                .WithMessage("Reference already exists")
                .WithName("Reference");
        }
    }
}