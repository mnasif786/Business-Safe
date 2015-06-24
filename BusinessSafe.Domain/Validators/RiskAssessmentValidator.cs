using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using FluentValidation;

namespace BusinessSafe.Domain.Validators
{
    public class RiskAssessmentValidator : AbstractValidator<RiskAssessment>
    {
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;
        public RiskAssessmentValidator(IRiskAssessmentRepository riskAssessmentRepository)
        {
            _riskAssessmentRepository = riskAssessmentRepository;

            RuleFor(riskAssessment => riskAssessment)
                .Must(x => new UniqueRiskAssessmentReferenceSpecification(_riskAssessmentRepository).IsSatisfiedBy(x))
                .WithMessage("Reference already exists")
                .WithName("Reference");

            RuleFor(riskAssessment => riskAssessment)
               .Must(x => new UniqueRiskAssessmentTitleSpecification(_riskAssessmentRepository).IsSatisfiedBy(x))
               .WithMessage("Title already exists")
               .WithName("Title");
        }
    }
}