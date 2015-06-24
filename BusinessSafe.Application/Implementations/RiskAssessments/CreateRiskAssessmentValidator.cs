using BusinessSafe.Application.Implementations.Specification;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.RepositoryContracts;
using FluentValidation;

namespace BusinessSafe.Application.Implementations.RiskAssessments
{
    public class CreateRiskAssessmentValidator<T> : AbstractValidator<ICreateRiskAssessmentRequest> where T :class
    {
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;
        
        public CreateRiskAssessmentValidator(IRiskAssessmentRepository riskAssessmentRepository)
        {
            _riskAssessmentRepository = riskAssessmentRepository;

            RuleFor(riskAssessment => riskAssessment)
                .Must(x => new UniqueRiskAssessmentReferenceSpecification<T>(_riskAssessmentRepository).IsSatisfiedBy(x))
                .WithMessage(RiskAssessmentsErrorMessages.ReferenceExistsMessage)
                .WithName("Reference");

            //RuleFor(riskAssessment => riskAssessment)
            //    .Must(x => new UniqueRiskAssessmentTitleSpecification<T>(_riskAssessmentRepository).IsSatisfiedBy(x))
            //    .WithMessage(RiskAssessmentsErrorMessages.TitleExistsMessage)
            //    .WithName("Title");

            
        }
    }

    public static class RiskAssessmentsErrorMessages
    {
        public const string TitleExistsMessage = "Title already exists";
        public const string ReferenceExistsMessage = "Reference already exists";
    }
}