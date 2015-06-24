using BusinessSafe.Application.Implementations.Specification;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.RepositoryContracts;
using FluentValidation;

namespace BusinessSafe.Application.Validators
{
    public class CopyRiskAssessmentRequestValidator<T>: AbstractValidator<CopyRiskAssessmentRequest> where T :class
    {
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;
        public CopyRiskAssessmentRequestValidator(IRiskAssessmentRepository riskAssessmentRepository)
        {
            _riskAssessmentRepository = riskAssessmentRepository;

                RuleFor(request => new CreateRiskAssessmentRequest()
                                   {
                                       Reference = request.Reference,
                                       CompanyId = request.CompanyId
                                   })
                .Must(x => new UniqueRiskAssessmentReferenceSpecification<T>(_riskAssessmentRepository).IsSatisfiedBy(x))
                .WithMessage("Reference already exists")
                .WithName("Reference");

                //RuleFor(request => new CreateRiskAssessmentRequest()
                //{
                //    Title = request.Title,
                //    CompanyId = request.CompanyId
                //})
                //.Must(x => new UniqueRiskAssessmentTitleSpecification<T>(_riskAssessmentRepository).IsSatisfiedBy(x))
                //.WithMessage("Title already exists")
                //.WithName("Title");

        }
    }
}
