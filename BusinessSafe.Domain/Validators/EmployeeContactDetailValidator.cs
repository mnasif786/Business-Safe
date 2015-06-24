using BusinessSafe.Domain.Entities;
using FluentValidation;
using FluentValidation.Validators;

namespace BusinessSafe.Domain.Validators
{
    public class EmployeeContactDetailValidator : AbstractValidator<EmployeeContactDetail>
    {
        public EmployeeContactDetailValidator()
        {
            RuleFor(e => e.Email).SetValidator(new EmployeeContactDetailEmailValidator()).WithMessage("Invalid email"); 
        }
    }

    public class EmployeeContactDetailEmailValidator : EmailValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var employeeContactDetail = context.Instance as EmployeeContactDetail;
            if(employeeContactDetail.Employee != null && employeeContactDetail.Employee.CanChangeEmail && !string.IsNullOrEmpty(employeeContactDetail.Email))
                return base.IsValid(context);
            return true;
        }
    }
}
